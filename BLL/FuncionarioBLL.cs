using FolhaPonto.Api.DAL;
using FolhaPonto.Api.Models;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace FolhaPonto.Api.BLL
{
    public class FuncionarioBLL
    {
        private DbConnection cn;
        private readonly FuncionarioDAL funcionarioDAL;

        public FuncionarioBLL(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            cn = new SqliteConnection(connectionString);
            funcionarioDAL = new FuncionarioDAL();
        }
        public Funcionario GetByEmail(string email)
        {
            Funcionario? retorno;

            try
            {
                cn.Open();
                retorno = funcionarioDAL.GetByEmail(cn, email);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
            return retorno;
        }
        public void Insert(Funcionario funcionario)
        {
            try
            {
                cn.Open();
                funcionarioDAL.Insert(cn, funcionario);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
