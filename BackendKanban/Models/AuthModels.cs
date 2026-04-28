using System.ComponentModel.DataAnnotations;

namespace BackendKanban.Models
{
    public class AuthModels
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string Senha { get; set; } = string.Empty;
    }
}
