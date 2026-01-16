namespace FolhaPonto.Api.Models
{
    public class HorasCalculadas
    {
        public string? horasCalculadas { get; set; }
        public string? horasAlmocoCalculadas { get; set; }
        public List<RegistroPonto>? registros { get; set; }
    }
    public class HorasExtrasCalculadas
    {
        public string? horasExtrasCalculadas { get; set; }
        public List<RegistroPonto>? registros { get; set; }
    }
}