namespace estoque_api.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string? nome { get; set; }
        public string? nm_usuario { get; set; }
        public string? senha { get; set; }
        public string? permissao { get; set; }
    }
    public class Login
    {
        public string? nm_usuario { get; set; }
        public string? senha { get; set; }
    }
}