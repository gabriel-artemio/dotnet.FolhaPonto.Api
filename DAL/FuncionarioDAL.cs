using FolhaPonto.Api.Models;
using System.Data.Common;
using System.Text;

namespace FolhaPonto.Api.DAL
{
    public class FuncionarioDAL
    {
        public Funcionario? GetByEmail(DbConnection cn, string email)
        {
            return GetAll(cn, 0, email).FirstOrDefault();
        }
        public List<Funcionario> GetAll(DbConnection cn)
        {
            return GetAll(cn, 0, string.Empty);
        }
        private List<Funcionario> GetAll(DbConnection cn, int id, string email)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("nome, email, senha, role ");
            sb.Append("FROM funcionario f ");
            sb.Append("WHERE 1 = 1 ");

            List<DbParameter> p = new List<DbParameter>();
            if (id > 0)
            {
                sb.Append("AND f.id = " + id);
            }

            if (!String.IsNullOrEmpty(email))
            {
                sb.AppendFormat("AND f.email = '{0}'", email);
            }

            List<Funcionario> list = new List<Funcionario>();
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(p.ToArray());
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new Funcionario
                        {
                            id = dr.GetInt32(0),
                            nome = dr.GetString(1),
                            email = dr.GetString(2),
                            senha = dr.GetString(3),
                            role = dr.GetInt32(4)
                        });
                    }
                }
            }
            return list;
        }
        public void Insert(DbConnection cn, Funcionario funcionario)
        {
            var sql = @"INSERT INTO funcionario(nome, email, senha, role) VALUES (@nome, @email, @senha, @role)";

            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sql;

                var pNome = cmd.CreateParameter();
                pNome.ParameterName = "@nome";
                pNome.Value = funcionario.nome;
                cmd.Parameters.Add(pNome);

                var pEmail = cmd.CreateParameter();
                pEmail.ParameterName = "@email";
                pEmail.Value = funcionario.email;
                cmd.Parameters.Add(pEmail);

                var pSenha = cmd.CreateParameter();
                pSenha.ParameterName = "@senha";
                pSenha.Value = funcionario.senha;
                cmd.Parameters.Add(pSenha);

                var pRole = cmd.CreateParameter();
                pRole.ParameterName = "@role";
                pRole.Value = funcionario.role;
                cmd.Parameters.Add(pRole);

                cmd.ExecuteNonQuery();
            }
        }
    }
}