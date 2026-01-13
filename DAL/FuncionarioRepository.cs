using Dapper;
using FolhaPonto.Api.Models;
using Microsoft.Data.Sqlite;

namespace FolhaPonto.Api.DAL
{
    public class FuncionarioRepository
    {
        private readonly string _connectionString;

        public FuncionarioRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public Funcionario? ObterPorEmail(string email)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = "SELECT * FROM Funcionarios WHERE Email = @Email";

            return connection.QueryFirstOrDefault<Funcionario>(sql, new { Email = email });
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
