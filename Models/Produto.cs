namespace estoque_api.Models
{
    public class Produto
    {
        public int id { get; set; }
        public string? status { get; set; }
        public string? descricao { get; set; }
        public int estoque_minino { get; set; }
        public int estoque_maximo { get; set; }
    }
}