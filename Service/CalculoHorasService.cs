using FolhaPonto.Api.Models;

namespace FolhaPonto.Api.Service
{
    public class CalculoHorasService
    {
        public TimeSpan CalcularHoras(List<RegistroPonto> registros)
        {
            DateTime? entrada = null;
            TimeSpan total = TimeSpan.Zero;

            foreach (var r in registros)
            {
                //1 - entrada, 2 - saida, 3 - inicio intervalo, 4 - fim intervalo
                if (r.tipo == 1)
                {
                    entrada = r.datahora;
                }

                if (r.tipo == 2 && entrada.HasValue)
                {
                    total += r.datahora - entrada.Value;
                    entrada = null;
                }
            }

            return total;
        }
    }
}