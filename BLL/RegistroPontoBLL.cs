using FolhaPonto.Api.DAL;
using FolhaPonto.Api.Models;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace FolhaPonto.Api.BLL
{
    public class RegistroPontoBLL
    {
        private DbConnection cn;
        private readonly RegistroPontoDAL registroPontoDAL;

        public RegistroPontoBLL(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            cn = new SqliteConnection(connectionString);
            registroPontoDAL = new RegistroPontoDAL();
        }

        public List<RegistroPonto> GetAll()
        {
            List<RegistroPonto> list = new();

            try
            {
                cn.Open();
                list = registroPontoDAL.GetAll(cn);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
            return list;
        }
        public RegistroPonto GetById(int id)
        {
            RegistroPonto? retorno;

            try
            {
                cn.Open();
                retorno = registroPontoDAL.GetById(cn, id);
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
        public List<RegistroPonto> GetByFuncionario(int funcionario_id)
        {
            List<RegistroPonto> list = new();

            try
            {
                cn.Open();
                list = registroPontoDAL.GetByFuncionario(cn, funcionario_id);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
            return list;
        }
        public void Insert(RegistroPonto registroPonto)
        {
            try
            {
                cn.Open();
                registroPontoDAL.Insert(cn, registroPonto);
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