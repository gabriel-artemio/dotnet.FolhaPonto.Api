using estoque_api.DAL;
using estoque_api.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace estoque_api.BLL
{
    public class SaidaProdutoBLL
    {
        private DbConnection cn;
        private readonly SaidaProdutoDAL saidaProdutoDAL;
        private readonly ProdutoDAL produtoDAL;

        public SaidaProdutoBLL(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MySql");
            cn = new MySqlConnection(connectionString);
            saidaProdutoDAL = new SaidaProdutoDAL();
            produtoDAL = new ProdutoDAL();
        }

        public List<SaidaProduto> GetAll()
        {
            List<SaidaProduto> list = new();

            try
            {
                cn.Open();
                list = saidaProdutoDAL.GetAll(cn);
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
        public SaidaProduto GetById(int id)
        {
            SaidaProduto? retorno;

            try
            {
                cn.Open();
                retorno = saidaProdutoDAL.GetById(cn, id);
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
        public void Insert(SaidaProduto saidaProduto)
        {
            try
            {
                cn.Open();
                Produto produto = produtoDAL.GetById(cn, saidaProduto.id_produto);
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
                if (saidaProduto.qtde > estoqueMaximo)
                {
                    throw new ArgumentException("Quantidade de saida é maior que o limite do estoque!");
                }
                //se estiver ok, insira a saida
                saidaProdutoDAL.Insert(cn, saidaProduto);
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
        public void Update(SaidaProduto saidaProduto)
        {
            SaidaProduto? retornoBanco;
            try
            {
                cn.Open();
                retornoBanco = saidaProdutoDAL.GetById(cn, saidaProduto.id);
                if (retornoBanco != null)
                {
                    retornoBanco.id_produto = saidaProduto.id_produto;
                    retornoBanco.qtde = saidaProduto.qtde;
                    retornoBanco.data_saida = saidaProduto.data_saida;

                    if (saidaProduto.qtde < 0)
                    {
                        throw new ArgumentException("Insira uma quantidade válida!");
                    }

                    saidaProdutoDAL.Update(cn, retornoBanco);
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
                saidaProdutoDAL.Delete(cn, id);
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