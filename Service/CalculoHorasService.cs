using FolhaPonto.Api.Models;

namespace FolhaPonto.Api.Service
{
    public class CalculoHorasService
    {
        public string CalcularHoras(List<RegistroPonto> registros)
        {
            DateTime? entrada = null;
            TimeSpan total = TimeSpan.Zero;

            foreach (var r in registros)
            {
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

            return FormatarHoras(total);
        }
        public string CalcularHorasAlmoco(List<RegistroPonto> registros)
        {
            DateTime? entrada = null;
            TimeSpan total = TimeSpan.Zero;

            foreach (var r in registros)
            {
                if (r.tipo == 3)
                {
                    entrada = r.datahora;
                }

                if (r.tipo == 4 && entrada.HasValue)
                {
                    total += r.datahora - entrada.Value;
                    entrada = null;
                }
            }

            return FormatarHoras(total);
        }

        public string CalcularHorasExtrasAprovadas(List<RegistroPonto> registros)
        {
            DateTime? entrada = null;
            TimeSpan total = TimeSpan.Zero;

            foreach (var r in registros)
            {
                if (r.tipo == 5 && r.status == 1)
                {
                    entrada = r.datahora;
                }

                if (r.tipo == 6 && entrada.HasValue && r.status == 1)
                {
                    total += r.datahora - entrada.Value;
                    entrada = null;
                }
            }

            return FormatarHoras(total);
        }

        public string FormatarHoras(TimeSpan total)
        {
            int horas = (int)total.TotalHours;
            int minutos = total.Minutes;

            return $"{horas:D2}:{minutos:D2}";
        }
    }
}