using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.App.InputModels
{
    public class CurriculumInputModel
    {

        [MaxLength(60)]
        public string Nome { get; set; }

        [MaxLength(50)]
        public string PerfilProgramador { get; set; }
        [MaxLength(250)]
        public string SobreMim { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}