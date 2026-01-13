using estoque_api.Models;
using System.Data.Common;
using System.Text;

namespace estoque_api.DAL
{
    internal class EstoqueDAL
    {
        public Estoque? GetById(DbConnection cn, int id)
        {
            return GetAll(cn, id).FirstOrDefault();
        }
        public List<Estoque> GetAll(DbConnection cn)
        {
            return GetAll(cn, 0);
        }
        private List<Estoque> GetAll(DbConnection cn, int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("id, id_produto, qtde, valor_unitario ");
            sb.Append("FROM estoque e ");
            sb.Append("WHERE 1 = 1 ");

            List<DbParameter> p = new List<DbParameter>();
            if (id > 0)
            {
                sb.Append("AND e.id = " + id);
            }

            List<Estoque> list = new List<Estoque>();
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(p.ToArray());
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new Estoque
                        {
                            id = dr.GetInt32(0),
                            id_produto = dr.GetInt32(1),
                            qtde = dr.GetInt32(2),
                            valor_unitario = dr.GetDecimal(3)
                        });
                    }
                }
            }
            return list;
        }
        public void Insert(DbConnection cn, Estoque estoque)
        {
            StringBuilder sb = new StringBuilder();
            List<DbParameter> p = new List<DbParameter>();
            sb.Append("INSERT INTO estoque ");
            sb.Append("(id_produto, qtde, valor_unitario) ");
            sb.AppendFormat("VALUES ({0})", "id_produto, qtde, valor_unitario".ParameterName());
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();

                DbParameter idProdutoParam = cmd.CreateParameter();
                idProdutoParam.ParameterName = "@id_produto";
                idProdutoParam.Value = estoque.id_produto;
                cmd.Parameters.Add(idProdutoParam);

                DbParameter qtdeParam = cmd.CreateParameter();
                qtdeParam.ParameterName = "@qtde";
                qtdeParam.Value = estoque.qtde;
                cmd.Parameters.Add(qtdeParam);

                DbParameter valorUnitarioParam = cmd.CreateParameter();
                valorUnitarioParam.ParameterName = "@valor_unitario";
                valorUnitarioParam.Value = estoque.valor_unitario;
                cmd.Parameters.Add(valorUnitarioParam);
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(DbConnection cn, Estoque estoque)
        {
            List<DbParameter> p = new List<DbParameter>();
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE estoque e SET ");
            sb.AppendFormat("e.id_produto={0},", "id_produto".ParameterName());
            sb.AppendFormat("e.qtde={0},", "qtde".ParameterName());
            sb.AppendFormat("e.valor_unitario={0}", "valor_unitario".ParameterName());
            sb.AppendFormat(" WHERE id={0}", estoque.id);
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();

                DbParameter idParam = cmd.CreateParameter();
                idParam.ParameterName = "@id";
                idParam.Value = estoque.id;
                cmd.Parameters.Add(idParam);

                DbParameter idProdutoParam = cmd.CreateParameter();
                idProdutoParam.ParameterName = "@id_produto";
                idProdutoParam.Value = estoque.id_produto;
                cmd.Parameters.Add(idProdutoParam);

                DbParameter qtdeParam = cmd.CreateParameter();
                qtdeParam.ParameterName = "@qtde";
                qtdeParam.Value = estoque.qtde;
                cmd.Parameters.Add(qtdeParam);

                DbParameter valorUnitarioParam = cmd.CreateParameter();
                valorUnitarioParam.ParameterName = "@valor_unitario";
                valorUnitarioParam.Value = estoque.valor_unitario;
                cmd.Parameters.Add(valorUnitarioParam);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(DbConnection cn, int id)
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("DELETE FROM estoque WHERE id={0}", id);
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = str.ToString();
                cmd.ExecuteNonQuery();
            }
        }
    }
}