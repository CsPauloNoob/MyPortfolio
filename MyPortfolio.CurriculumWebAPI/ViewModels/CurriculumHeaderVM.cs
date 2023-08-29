using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.App.ViewModels
{
    public class CurriculumHeaderVM
    {
        public string Nome { get; set; }

        public string PerfilProgramador { get; set; }

        public string SobreMim { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}