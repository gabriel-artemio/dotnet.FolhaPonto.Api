using estoque_api.Models;
using System.Data.Common;
using System.Text;

namespace estoque_api.DAL
{
    internal class EntradaProdutoDAL
    {
        public EntradaProduto? GetById(DbConnection cn, int id)
        {
            return GetAll(cn, id).FirstOrDefault();
        }
        public List<EntradaProduto> GetAll(DbConnection cn)
        {
            return GetAll(cn, 0);
        }
        private List<EntradaProduto> GetAll(DbConnection cn, int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("id, id_produto, qtde, valor_unitario, data_entrada ");
            sb.Append("FROM entrada_produto ep ");
            sb.Append("WHERE 1 = 1 ");

            List<DbParameter> p = new List<DbParameter>();
            if (id > 0)
            {
                sb.Append("AND ep.id = " + id);
            }

            List<EntradaProduto> list = new List<EntradaProduto>();
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(p.ToArray());
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new EntradaProduto
                        {
                            id = dr.GetInt32(0),
                            id_produto = dr.GetInt32(1),
                            qtde = dr.GetInt32(2),
                            valor_unitario = dr.GetDecimal(3),
                            data_entrada = dr.GetDateTime(4)
                        });
                    }
                }
            }
            return list;
        }
        public void Insert(DbConnection cn, EntradaProduto entradaProduto)
        {
            StringBuilder sb = new StringBuilder();
            List<DbParameter> p = new List<DbParameter>();
            sb.Append("INSERT INTO entrada_produto ");
            sb.Append("(id_produto, qtde, valor_unitario, data_entrada) ");
            sb.AppendFormat("VALUES ({0})", "id_produto, qtde, valor_unitario, data_entrada".ParameterName());
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();

                DbParameter idProdutoParam = cmd.CreateParameter();
                idProdutoParam.ParameterName = "@id_produto";
                idProdutoParam.Value = entradaProduto.id_produto;
                cmd.Parameters.Add(idProdutoParam);

                DbParameter qtdeParam = cmd.CreateParameter();
                qtdeParam.ParameterName = "@qtde";
                qtdeParam.Value = entradaProduto.qtde;
                cmd.Parameters.Add(qtdeParam);

                DbParameter valorUnitarioParam = cmd.CreateParameter();
                valorUnitarioParam.ParameterName = "@valor_unitario";
                valorUnitarioParam.Value = entradaProduto.valor_unitario;
                cmd.Parameters.Add(valorUnitarioParam);

                DbParameter dataEntradaParam = cmd.CreateParameter();
                dataEntradaParam.ParameterName = "@data_entrada";
                dataEntradaParam.Value = entradaProduto.data_entrada;
                cmd.Parameters.Add(dataEntradaParam);
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(DbConnection cn, EntradaProduto entradaProduto)
        {
            List<DbParameter> p = new List<DbParameter>();
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE entrada_produto ep SET ");
            sb.AppendFormat("ep.id_produto={0},", "id_produto".ParameterName());
            sb.AppendFormat("ep.qtde={0},", "qtde".ParameterName());
            sb.AppendFormat("ep.valor_unitario={0},", "valor_unitario".ParameterName());
            sb.AppendFormat("ep.data_entrada={0}", "data_entrada".ParameterName());
            sb.AppendFormat(" WHERE id={0}", entradaProduto.id);
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();

                DbParameter idProdutoParam = cmd.CreateParameter();
                idProdutoParam.ParameterName = "@id_produto";
                idProdutoParam.Value = entradaProduto.id_produto;
                cmd.Parameters.Add(idProdutoParam);

                DbParameter qtdeParam = cmd.CreateParameter();
                qtdeParam.ParameterName = "@qtde";
                qtdeParam.Value = entradaProduto.qtde;
                cmd.Parameters.Add(qtdeParam);

                DbParameter valorUnitarioParam = cmd.CreateParameter();
                valorUnitarioParam.ParameterName = "@valor_unitario";
                valorUnitarioParam.Value = entradaProduto.valor_unitario;
                cmd.Parameters.Add(valorUnitarioParam);

                DbParameter dataEntradaParam = cmd.CreateParameter();
                dataEntradaParam.ParameterName = "@data_entrada";
                dataEntradaParam.Value = entradaProduto.data_entrada;
                cmd.Parameters.Add(dataEntradaParam);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(DbConnection cn, int id)
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("DELETE FROM entrada_produto WHERE id={0}", id);
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = str.ToString();
                cmd.ExecuteNonQuery();
            }
        }
    }
}