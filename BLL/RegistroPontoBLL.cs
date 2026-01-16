using FolhaPonto.Api.DAL;
using FolhaPonto.Api.Models;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Net.NetworkInformation;

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
        public List<RegistroPonto> GetHorasExtras(int id, int status)
        {
            List<RegistroPonto> list = new();

            try
            {
                cn.Open();
                list = registroPontoDAL.GetHorasExtras(cn, id, status);
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

                DateTime hoje = DateTime.Today;
                DateTime dataRegistro = registroPonto.datahora.Date;

                //bloquear ponto retroativo
                if (dataRegistro < hoje)
                {
                    throw new Exception("Não é permitido registrar ponto retroativo.");
                }

                //bloquear ponto futuro
                if (dataRegistro > hoje)
                {
                    throw new Exception("Não é permitido registrar ponto em data futura.");
                }

                //pegando o ultimo registro do dia
                var ultimoPonto = registroPontoDAL.GetUltimoRegistroDia(
                    cn,
                    registroPonto.funcionario_id,
                    registroPonto.datahora
                );

                //se for o primeiro ponto do dia
                if (ultimoPonto == null)
                {
                    if (registroPonto.tipo != 1)
                    {
                        throw new Exception("O primeiro registro do dia deve ser ENTRADA.");
                    }
                }
                else
                {
                    //não pode repetir o mesmo tipo
                    if (ultimoPonto.tipo == registroPonto.tipo)
                    {
                        throw new Exception(
                            $"Não é permitido registrar {registroPonto.tipo} duas vezes seguidas."
                        );
                    }
                }

                //regra de almoço, ter no mínimo 1 hora
                if (ultimoPonto != null &&
                    ultimoPonto.tipo == 3 &&
                    registroPonto.tipo == 4)
                {
                    var intervalo = registroPonto.datahora - ultimoPonto.datahora;

                    if (intervalo.TotalMinutes < 60)
                    {
                        throw new Exception("O intervalo de almoço deve ser de no mínimo 1 hora.");
                    }
                }

                //Regras de hora extra
                bool novoEhHoraExtra = registroPonto.tipo == 5 || registroPonto.tipo == 6;

                bool ultimoEhHoraExtra = ultimoPonto != null && (ultimoPonto.tipo == 5 || ultimoPonto.tipo == 6);

                if (novoEhHoraExtra)
                {
                    //só pode iniciar hora extra após finalizar expediente
                    if (registroPonto.tipo == 5)
                    {
                        if (ultimoPonto == null || ultimoPonto.tipo != 2)
                        {
                            throw new Exception("Só é permitido iniciar hora extra após finalizar o expediente.");
                        }
                    }

                    //sequência da hora extra
                    if (registroPonto.tipo == 6)
                    {
                        if (ultimoPonto == null || ultimoPonto.tipo != 5)
                        {
                            throw new Exception("A saída de hora extra só pode ocorrer após a entrada de hora extra.");
                        }
                    }
                }
                else
                {
                    //não permitir voltar ao expediente normal após hora extra
                    if (ultimoEhHoraExtra)
                    {
                        throw new Exception("Não é permitido registrar ponto normal após iniciar hora extra.");
                    }
                }

                //regra ok? -> insere
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