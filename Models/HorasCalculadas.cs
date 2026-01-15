namespace FolhaPonto.Api.Models
{
    public class HorasCalculadas
    {
        public TimeSpan horasCalculadas { get; set; }
        public TimeSpan horasAlmocoCalculadas { get; set; }
        public List<RegistroPonto>? registros { get; set; }
    }
}