using estoque_api.DAL;
using estoque_api.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace estoque_api.BLL
{
    public class EntradaProdutoBLL
    {
        private DbConnection cn;
        private readonly EntradaProdutoDAL entradaProdutoDAL;
        private readonly ProdutoDAL produtoDAL;

        public EntradaProdutoBLL(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MySql");
            cn = new MySqlConnection(connectionString);
            entradaProdutoDAL = new EntradaProdutoDAL();
            produtoDAL = new ProdutoDAL();
        }

        public List<EntradaProduto> GetAll()
        {
            List<EntradaProduto> list = new();

            try
            {
                cn.Open();
                list = entradaProdutoDAL.GetAll(cn);
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
        public EntradaProduto GetById(int id)
        {
            EntradaProduto? retorno;

            try
            {
                cn.Open();
                retorno = entradaProdutoDAL.GetById(cn, id);
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
        public void Insert(EntradaProduto entradaProduto)
        {
            try
            {
                cn.Open();
                Produto produto = produtoDAL.GetById(cn, entradaProduto.id_produto);
                if (produto == null)
                {
                    throw new ArgumentException("Produto não encontrado ou cadastrado!");
                }
                //verificar se o produto esta ativo
                if (produto.status == "I")
                {
                    throw new ArgumentException("Este produto não disponível!");
                }
                //verificar o estoque maximo do produto
                int estoqueMaximo = produto.estoque_maximo;
                if (entradaProduto.qtde > estoqueMaximo)
                {
                    throw new ArgumentException("Quantidade de entrada é maior que o limite do estoque!");
                }
                //se estiver ok, insira a entrada
                entradaProdutoDAL.Insert(cn, entradaProduto);
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
        public void Update(EntradaProduto entradaProduto)
        {
            EntradaProduto? retornoBanco;
            try
            {
                cn.Open();
                retornoBanco = entradaProdutoDAL.GetById(cn, entradaProduto.id);
                if (retornoBanco != null)
                {
                    retornoBanco.id_produto = entradaProduto.id_produto;
                    retornoBanco.qtde = entradaProduto.qtde;
                    retornoBanco.data_entrada = entradaProduto.data_entrada;

                    if (entradaProduto.qtde < 0)
                    {
                        throw new ArgumentException("Insira uma quantidade válida!");
                    }

                    entradaProdutoDAL.Update(cn, retornoBanco);
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
                entradaProdutoDAL.Delete(cn, id);
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