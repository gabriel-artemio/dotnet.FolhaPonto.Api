using estoque_api.Models;
using System.Data.Common;
using System.Text;

namespace estoque_api.DAL
{
    internal class SaidaProdutoDAL
    {
        public SaidaProduto? GetById(DbConnection cn, int id)
        {
            return GetAll(cn, id).FirstOrDefault();
        }
        public List<SaidaProduto> GetAll(DbConnection cn)
        {
            return GetAll(cn, 0);
        }
        private List<SaidaProduto> GetAll(DbConnection cn, int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("id, id_produto, qtde, valor_unitario, data_saida ");
            sb.Append("FROM saida_produto sp ");
            sb.Append("WHERE 1 = 1 ");

            List<DbParameter> p = new List<DbParameter>();
            if (id > 0)
            {
                sb.Append("AND sp.id = " + id);
            }

            List<SaidaProduto> list = new List<SaidaProduto>();
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(p.ToArray());
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new SaidaProduto
                        {
                            id = dr.GetInt32(0),
                            id_produto = dr.GetInt32(1),
                            qtde = dr.GetInt32(2),
                            valor_unitario = dr.GetDecimal(3),
                            data_saida = dr.GetDateTime(4)
                        });
                    }
                }
            }
            return list;
        }
        public void Insert(DbConnection cn, SaidaProduto saidaProduto)
        {
            StringBuilder sb = new StringBuilder();
            List<DbParameter> p = new List<DbParameter>();
            sb.Append("INSERT INTO saida_produto ");
            sb.Append("(id_produto, qtde, valor_unitario, data_saida) ");
            sb.AppendFormat("VALUES ({0})", "id_produto, qtde, valor_unitario, data_saida".ParameterName());
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();

                DbParameter idProdutoParam = cmd.CreateParameter();
                idProdutoParam.ParameterName = "@id_produto";
                idProdutoParam.Value = saidaProduto.id_produto;
                cmd.Parameters.Add(idProdutoParam);

                DbParameter qtdeParam = cmd.CreateParameter();
                qtdeParam.ParameterName = "@qtde";
                qtdeParam.Value = saidaProduto.qtde;
                cmd.Parameters.Add(qtdeParam);

                DbParameter valorUnitarioParam = cmd.CreateParameter();
                valorUnitarioParam.ParameterName = "@valor_unitario";
                valorUnitarioParam.Value = saidaProduto.valor_unitario;
                cmd.Parameters.Add(valorUnitarioParam);

                DbParameter dataSaidaParam = cmd.CreateParameter();
                dataSaidaParam.ParameterName = "@data_saida";
                dataSaidaParam.Value = saidaProduto.data_saida;
                cmd.Parameters.Add(dataSaidaParam);
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(DbConnection cn, SaidaProduto saidaProduto)
        {
            List<DbParameter> p = new List<DbParameter>();
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE saida_produto sp SET ");
            sb.AppendFormat("sp.id_produto={0},", "id_produto".ParameterName());
            sb.AppendFormat("sp.qtde={0},", "qtde".ParameterName());
            sb.AppendFormat("sp.valor_unitario={0},", "valor_unitario".ParameterName());
            sb.AppendFormat("sp.data_saida={0}", "data_saida".ParameterName());
            sb.AppendFormat(" WHERE id={0}", saidaProduto.id);
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();

                DbParameter idProdutoParam = cmd.CreateParameter();
                idProdutoParam.ParameterName = "@id_produto";
                idProdutoParam.Value = saidaProduto.id_produto;
                cmd.Parameters.Add(idProdutoParam);

                DbParameter qtdeParam = cmd.CreateParameter();
                qtdeParam.ParameterName = "@qtde";
                qtdeParam.Value = saidaProduto.qtde;
                cmd.Parameters.Add(qtdeParam);

                DbParameter valorUnitarioParam = cmd.CreateParameter();
                valorUnitarioParam.ParameterName = "@valor_unitario";
                valorUnitarioParam.Value = saidaProduto.valor_unitario;
                cmd.Parameters.Add(valorUnitarioParam);

                DbParameter dataSaidaParam = cmd.CreateParameter();
                dataSaidaParam.ParameterName = "@data_saida";
                dataSaidaParam.Value = saidaProduto.data_saida;
                cmd.Parameters.Add(dataSaidaParam);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(DbConnection cn, int id)
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("DELETE FROM saida_produto WHERE id={0}", id);
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = str.ToString();
                cmd.ExecuteNonQuery();
            }
        }
    }
}