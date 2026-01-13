namespace FolhaPonto.Api.Models
{
    public class RegistroPonto
    {
        public int id { get; set; }
        public int funcionario_id { get; set; }
        public DateTime datahora { get; set; }
        public int tipo { get; set; }
    }
}
