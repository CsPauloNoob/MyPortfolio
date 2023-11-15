using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CurriculumWebAPI.App.InputModels
{
    public class ExpProfissionalInputModel
    {
        [Required]
        [MaxLength(40)]
        public string Nome_Organizacao { get; set; }
        [Required]
        [MaxLength(50)]
        public string Funcao { get; set; }
        [MaxLength(300)]
        public string Descricao { get; set; }
    }
}