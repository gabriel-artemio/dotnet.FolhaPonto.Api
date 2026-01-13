using estoque_api.DAL;
using estoque_api.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace estoque_api.BLL
{
    public class ProdutoBLL
    {
        private DbConnection cn;
        private readonly ProdutoDAL produtoDAL;

        public ProdutoBLL(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MySql");
            cn = new MySqlConnection(connectionString);
            produtoDAL = new ProdutoDAL();
        }

        public List<Produto> GetAll()
        {
            List<Produto> list = new();

            try
            {
                cn.Open();
                list = produtoDAL.GetAll(cn);
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
        public Produto GetById(int id)
        {
            Produto? retorno;

            try
            {
                cn.Open();
                retorno = produtoDAL.GetById(cn, id);
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
        public void Insert(Produto produto)
        {
            try
            {
                cn.Open();
                produtoDAL.Insert(cn, produto);
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
        public void Update(Produto produto)
        {
            Produto? retornoBanco;
            try
            {
                cn.Open();
                retornoBanco = produtoDAL.GetById(cn, produto.id);
                if (retornoBanco != null)
                {
                    retornoBanco.descricao = produto.descricao;
                    retornoBanco.status = produto.status;
                    retornoBanco.estoque_minino = produto.estoque_minino;
                    retornoBanco.estoque_maximo = produto.estoque_maximo;
                    produtoDAL.Update(cn, retornoBanco);
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
                produtoDAL.Delete(cn, id);
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