namespace estoque_api.Models
{
    public class EntradaProduto
    {
        public int id { get; set; }
        public int id_produto { get; set; }
        public int qtde { get; set; }
        public decimal valor_unitario { get; set; }
        public DateTime data_entrada { get; set; }
    }
}