using estoque_api.Models;
using System.Data.Common;
using System.Text;

namespace estoque_api.DAL
{
    public class UsuarioDAL
    {
        public Usuario? GetUser(DbConnection cn, string? nm_usuario, string? senha)
        {
            return GetAll(cn, 0, nm_usuario, senha).FirstOrDefault();
        }
        public Usuario? GetById(DbConnection cn, int id)
        {
            return GetAll(cn, id, string.Empty, string.Empty).FirstOrDefault();
        }
        public List<Usuario> GetAll(DbConnection cn)
        {
            return GetAll(cn, 0, string.Empty, string.Empty);
        }
        private List<Usuario> GetAll(DbConnection cn, int id, string? nm_usuario, string? senha)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("id, nome, nm_usuario, senha, permissao ");
            sb.Append("FROM usuario ");
            sb.Append("WHERE 1 = 1 ");

            List<DbParameter> p = new List<DbParameter>();
            if (id > 0)
            {
                sb.Append("AND id = " + id);
            }
            if (!string.IsNullOrEmpty(nm_usuario) && !string.IsNullOrEmpty(senha))
            {
                sb.AppendFormat(" AND nm_usuario = '{0}'", nm_usuario);
                sb.AppendFormat(" AND senha = '{0}'", senha);
            }

            List<Usuario> list = new List<Usuario>();
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(p.ToArray());
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new Usuario
                        {
                            id = dr.GetInt32(0),
                            nome = dr.GetString(1),
                            nm_usuario = dr.GetString(2),
                            senha = dr.GetString(3),
                            permissao = dr.GetString(4)
                        });
                    }
                }
            }
            return list;
        }
    }
}