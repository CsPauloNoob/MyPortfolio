using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.App.InputModels
{
    public class CursosExtraInputModel
    {

        [Required]
        [MaxLength(50)]
        public string Nome_Curso { get; set; }

        [MaxLength(50)]
        public string Organizacao { get; set; }
    }
}
