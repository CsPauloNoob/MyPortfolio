using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.App.InputModels
{
    public class FormacaoInputModel
    {
        [MaxLength(30)]
        public string Instituicao { get; set; }
        [MaxLength(35)]
        public string Curso { get; set; }
        [MaxLength(4)]
        public int AnoConclusao { get; set; }
    }
}