using estoque_api.Models;
using System.Data.Common;
using System.Text;

namespace estoque_api.DAL
{
    internal class ProdutoDAL
    {
        public Produto? GetById(DbConnection cn, int id)
        {
            return GetAll(cn, id).FirstOrDefault();
        }
        public List<Produto> GetAll(DbConnection cn)
        {
            return GetAll(cn, 0);
        }
        private List<Produto> GetAll(DbConnection cn, int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("id, status, descricao, estoque_minimo, estoque_maximo ");
            sb.Append("FROM produto p ");
            sb.Append("WHERE 1 = 1 ");

            List<DbParameter> p = new List<DbParameter>();
            if (id > 0)
            {
                sb.Append("AND p.id = " + id);
            }

            List<Produto> list = new List<Produto>();
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(p.ToArray());
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new Produto
                        {
                            id = dr.GetInt32(0),
                            status = dr.GetString(1),
                            descricao = dr.GetString(2),
                            estoque_minino = dr.GetInt32(3),
                            estoque_maximo = dr.GetInt32(4)
                        });
                    }
                }
            }
            return list;
        }
        public void Insert(DbConnection cn, Produto produto)
        {
            StringBuilder sb = new StringBuilder();
            List<DbParameter> p = new List<DbParameter>();
            sb.Append("INSERT INTO produto ");
            sb.Append("(status, descricao, estoque_minimo, estoque_maximo) ");
            sb.AppendFormat("VALUES ({0})", "status, descricao, estoque_minimo, estoque_maximo".ParameterName());
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                
                DbParameter statusParam = cmd.CreateParameter();
                statusParam.ParameterName = "@status";
                statusParam.Value = produto.status;
                cmd.Parameters.Add(statusParam);

                DbParameter descricaoParam = cmd.CreateParameter();
                descricaoParam.ParameterName = "@descricao";
                descricaoParam.Value = produto.descricao;
                cmd.Parameters.Add(descricaoParam);

                DbParameter estoqueMinimoParam = cmd.CreateParameter();
                estoqueMinimoParam.ParameterName = "@estoque_minimo";
                estoqueMinimoParam.Value = produto.estoque_minino;
                cmd.Parameters.Add(estoqueMinimoParam);

                DbParameter estoqueMaximoParam = cmd.CreateParameter();
                estoqueMaximoParam.ParameterName = "@estoque_maximo";
                estoqueMaximoParam.Value = produto.estoque_maximo;
                cmd.Parameters.Add(estoqueMaximoParam);
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(DbConnection cn, Produto produto)
        {
            List<DbParameter> p = new List<DbParameter>();
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE produto p SET ");
            sb.AppendFormat("p.status={0},", "status".ParameterName());
            sb.AppendFormat("p.descricao={0},", "descricao".ParameterName());
            sb.AppendFormat("p.estoque_minimo={0},", "estoque_minimo".ParameterName());
            sb.AppendFormat("p.estoque_maximo={0}", "estoque_maximo".ParameterName());
            sb.AppendFormat(" WHERE id={0}", produto.id);
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();

                DbParameter idParam = cmd.CreateParameter();
                idParam.ParameterName = "@id";
                idParam.Value = produto.status;
                cmd.Parameters.Add(idParam);

                DbParameter statusParam = cmd.CreateParameter();
                statusParam.ParameterName = "@status";
                statusParam.Value = produto.status;
                cmd.Parameters.Add(statusParam);

                DbParameter descricaoParam = cmd.CreateParameter();
                descricaoParam.ParameterName = "@descricao";
                descricaoParam.Value = produto.descricao;
                cmd.Parameters.Add(descricaoParam);

                DbParameter estoqueMinimoParam = cmd.CreateParameter();
                estoqueMinimoParam.ParameterName = "@estoque_minimo";
                estoqueMinimoParam.Value = produto.estoque_minino;
                cmd.Parameters.Add(estoqueMinimoParam);

                DbParameter estoqueMaximoParam = cmd.CreateParameter();
                estoqueMaximoParam.ParameterName = "@estoque_maximo";
                estoqueMaximoParam.Value = produto.estoque_maximo;
                cmd.Parameters.Add(estoqueMaximoParam);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(DbConnection cn, int id)
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("DELETE FROM produto WHERE id={0}", id);
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = str.ToString();
                cmd.ExecuteNonQuery();
            }
        }
    }
}