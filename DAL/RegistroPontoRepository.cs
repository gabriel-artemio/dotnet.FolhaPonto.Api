using Dapper;
using FolhaPonto.Api.Models;
using Microsoft.Data.Sqlite;

namespace FolhaPonto.Api.DAL
{
    public class RegistroPontoRepository
    {
        private readonly string _connectionString;

        public RegistroPontoRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public void Inserir(RegistroPonto registro)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = """
            INSERT INTO RegistrosPonto (FuncionarioId, DataHora, Tipo)
            VALUES (@FuncionarioId, @DataHora, @Tipo)
            """;

            connection.Execute(sql, registro);
        }

        public List<RegistroPonto> ObterPorDia(Guid funcionarioId, DateTime data)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = """
            SELECT * FROM RegistrosPonto
            WHERE FuncionarioId = @FuncionarioId
            AND date(DataHora) = date(@Data)
            ORDER BY DataHora
            """;

            return connection.Query<RegistroPonto>(sql,
                new { FuncionarioId = funcionarioId.ToString(), Data = data }).ToList();
        }
    }
}
