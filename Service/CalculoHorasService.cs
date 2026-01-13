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
                if (r.Tipo == TipoRegistroPonto.Entrada)
                    entrada = r.DataHora;

                if (r.Tipo == TipoRegistroPonto.Saida && entrada.HasValue)
                {
                    total += r.DataHora - entrada.Value;
                    entrada = null;
                }
            }

            return total;
        }
    }
}