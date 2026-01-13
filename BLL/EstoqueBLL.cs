using estoque_api.DAL;
using estoque_api.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace estoque_api.BLL
{
    public class EstoqueBLL
    {
        private DbConnection cn;
        private readonly EstoqueDAL estoqueDAL;

        public EstoqueBLL(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MySql");
            cn = new MySqlConnection(connectionString);
            estoqueDAL = new EstoqueDAL();
        }

        public List<Estoque> GetAll()
        {
            List<Estoque> list = new();

            try
            {
                cn.Open();
                list = estoqueDAL.GetAll(cn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                cn.Close();
            }
            return list;
        }
        public Estoque GetById(int id)
        {
            Estoque? retorno;

            try
            {
                cn.Open();
                retorno = estoqueDAL.GetById(cn, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                cn.Close();
            }
            return retorno;
        }
        public void Insert(Estoque estoque)
        {
            try
            {
                cn.Open();
                estoqueDAL.Insert(cn, estoque);
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
        public void Update(Estoque estoque)
        {
            Estoque? retornoBanco;
            try
            {
                cn.Open();
                retornoBanco = estoqueDAL.GetById(cn, estoque.id);
                if (retornoBanco != null)
                {
                    retornoBanco.id_produto = estoque.id_produto;
                    retornoBanco.valor_unitario = estoque.valor_unitario;
                    retornoBanco.qtde = estoque.qtde;
                    estoqueDAL.Update(cn, retornoBanco);
                }
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
        public void Delete(int id)
        {
            try
            {
                cn.Open();
                estoqueDAL.Delete(cn, id);
            }
            catch (Exception)
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