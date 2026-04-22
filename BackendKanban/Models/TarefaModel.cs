using BackendKanban.DTO;
using System.ComponentModel.DataAnnotations;

namespace BackendKanban.Models
{
    public class TarefaModel : TarefaReadDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }

        [Required]
        public string Status { get; set; } = "Todo";
        public DateTime DataCriacao { get; set; }
    }
}
