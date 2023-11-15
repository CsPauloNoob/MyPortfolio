using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CurriculumWebAPI.App.InputModels
{
    public class HabilidadeInputModel
    {
        [Required]
        [MaxLength(35)]
        public string Nome_Habilidade { get; set; }

        [MaxLength(180)]
        public string? Descricao { get; set; }
    }
}