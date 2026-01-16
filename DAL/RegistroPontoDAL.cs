using FolhaPonto.Api.Models;
using System.Data.Common;
using System.Text;

namespace FolhaPonto.Api.DAL
{
    internal class RegistroPontoDAL
    {
        public RegistroPonto? GetById(DbConnection cn, int id)
        {
            return GetAll(cn, id, 0, null, 0).FirstOrDefault();
        }
        public RegistroPonto? GetUltimoRegistroDia(DbConnection cn, int id, DateTime dataAtual)
        {
            return GetAll(cn, id, 0, dataAtual, 0).FirstOrDefault();
        }
        public List<RegistroPonto> GetByFuncionario(DbConnection cn, int funcionario_id)
        {
            return GetAll(cn, 0, funcionario_id, null, 0);
        }
        public List<RegistroPonto> GetHorasExtras(DbConnection cn, int id, int status)
        {
            return GetAll(cn, 0, id, null, status);
        }
        public List<RegistroPonto> GetAll(DbConnection cn)
        {
            return GetAll(cn, 0, 0, null, 0);
        }
        private List<RegistroPonto> GetAll(DbConnection cn, int id, int funcionario_id, DateTime? dataAtual, int status)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("id, funcionario_id, datahora, tipo, status ");
            sb.Append("FROM registro_ponto rp ");
            sb.Append("WHERE 1 = 1 ");

            List<DbParameter> p = new List<DbParameter>();
            if (id > 0)
            {
                sb.Append(" AND rp.id = " + id);
            }

            if (funcionario_id > 0)
            {
                sb.Append(" AND rp.funcionario_id = " + funcionario_id);
            }

            if (dataAtual != null)
            {
                sb.AppendFormat("AND date(rp.datahora) = date('{0}') ", dataAtual);
                sb.Append(" ORDER BY rp.datahora DESC");
            }

            if (status > 0)
            {
                sb.Append(" AND rp.status = " + status);
            }

            List<RegistroPonto> list = new List<RegistroPonto>();
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(p.ToArray());
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new RegistroPonto
                        {
                            id = dr.GetInt32(0),
                            funcionario_id = dr.GetInt32(1),
                            datahora = dr.GetDateTime(2),
                            tipo = dr.GetInt32(3),
                            status = dr.GetInt32(4)
                        });
                    }
                }
            }
            return list;
        }
        public void Insert(DbConnection cn, RegistroPonto registroPonto)
        {
            var sql = @"INSERT INTO registro_ponto(funcionario_id, datahora, tipo) VALUES (@funcionario_id, @datahora, @tipo)";

            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sql;

                var pFuncId = cmd.CreateParameter();
                pFuncId.ParameterName = "@funcionario_id";
                pFuncId.Value = registroPonto.funcionario_id;
                cmd.Parameters.Add(pFuncId);

                var pDataHora = cmd.CreateParameter();
                pDataHora.ParameterName = "@datahora";
                pDataHora.Value = registroPonto.datahora;
                cmd.Parameters.Add(pDataHora);

                var pTipo = cmd.CreateParameter();
                pTipo.ParameterName = "@tipo";
                pTipo.Value = registroPonto.tipo;
                cmd.Parameters.Add(pTipo);

                cmd.ExecuteNonQuery();
            }
        }
        public void Update(DbConnection cn, RegistroPonto registroPonto)
        {
            var sql = @"UPDATE registro_ponto SET status = @status WHERE id = @id";

            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sql;

                var pId = cmd.CreateParameter();
                pId.ParameterName = "@id";
                pId.Value = registroPonto.id;
                cmd.Parameters.Add(pId);

                var pStatus = cmd.CreateParameter();
                pStatus.ParameterName = "@status";
                pStatus.Value = registroPonto.status;
                cmd.Parameters.Add(pStatus);

                cmd.ExecuteNonQuery();
            }
        }
    }
}