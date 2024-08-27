using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.App.InputModels
{
    public class CursosExtraInputModel
    {

        [Required]
        [MaxLength(35)]
        public string Nome_Curso { get; set; }

        [MaxLength(35)]
        public string Organizacao { get; set; }
    }
}
