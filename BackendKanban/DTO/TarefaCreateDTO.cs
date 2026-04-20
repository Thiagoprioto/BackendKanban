namespace BackendKanban.DTO
{
    public class TarefaCreateDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string Status { get; set; } = "Todo";
    }
}
