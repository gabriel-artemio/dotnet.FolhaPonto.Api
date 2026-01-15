using Dapper;
using FolhaPonto.Api.Models;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Text;

namespace FolhaPonto.Api.DAL
{
    public class FuncionarioDAL
    {
        private readonly string _connectionString;

        public FuncionarioDAL(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public Funcionario? ObterPorEmail(string email)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = "SELECT * FROM Funcionarios WHERE Email = @Email";

            return connection.QueryFirstOrDefault<Funcionario>(sql, new { Email = email });
        }
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
            sb.Append("nome, email, senha ");
            sb.Append("FROM funcionarios f ");
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
                            senha = dr.GetString(3)
                        });
                    }
                }
            }
            return list;
        }

        public void Inserir(Funcionario funcionario)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = """
            INSERT INTO Funcionarios (Nome, Email, Senha)
            VALUES (@Nome, @Email, @Senha)
            """;

            connection.Execute(sql, funcionario);
        }
    }
}
