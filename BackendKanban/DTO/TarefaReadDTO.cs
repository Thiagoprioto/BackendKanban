namespace BackendKanban.DTO
{
    public class TarefaReadDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}
