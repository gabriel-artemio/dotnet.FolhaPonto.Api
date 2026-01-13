using Microsoft.Data.Sqlite;

namespace FolhaPonto.Api.DAL
{
    public static class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var sql = """
            CREATE TABLE IF NOT EXISTS Funcionarios (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Email TEXT NOT NULL UNIQUE,
                Senha TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS RegistrosPonto (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FuncionarioId TEXT NOT NULL,
                DataHora TEXT NOT NULL,
                Tipo INTEGER NOT NULL,
                FOREIGN KEY (FuncionarioId) REFERENCES Funcionarios(Id)
            );
            """;

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
    }
}