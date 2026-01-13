using estoque_api.DAL;
using estoque_api.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace estoque_api.BLL
{
    public class UsuarioBLL
    {
        private DbConnection cn;
        private readonly UsuarioDAL usuarioDAL;

        public UsuarioBLL(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MySql");
            cn = new MySqlConnection(connectionString);
            usuarioDAL = new UsuarioDAL();
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> list = new();

            try
            {
                cn.Open();
                list = usuarioDAL.GetAll(cn);
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
        public Usuario GetUser(string? nm_usuario, string? senha)
        {
            Usuario? retorno;

            try
            {
                cn.Open();
                retorno = usuarioDAL.GetUser(cn, nm_usuario, senha);
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
        public Usuario GetById(int id)
        {
            Usuario? retorno;

            try
            {
                cn.Open();
                retorno = usuarioDAL.GetById(cn, id);
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
    }
}