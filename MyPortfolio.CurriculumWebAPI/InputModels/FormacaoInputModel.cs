using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.App.InputModels
{
    public class FormacaoInputModel
    {
        [MaxLength(50)]
        public string Instituicao { get; set; }

        [MaxLength(50)]
        public string Curso { get; set; }

        [MaxLength(4)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ano de conclusão deve conter somente números.")]
        public string AnoConclusao { get; set; }
    }
}