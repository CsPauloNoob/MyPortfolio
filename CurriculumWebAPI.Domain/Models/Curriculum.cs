using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using CurriculumWebAPI.Domain.Models.CurriculumBody;

namespace CurriculumWebAPI.Domain.Models
{
    public class Curriculum
    {
        [MinLength(32)]
        public string Id { get; set; }

        public string Nome { get; set; }

        [MaxLength(50)]
        public string PerfilProgramador { get; set; }

        public string SobreMim { get; set; }


        public Contato Contato { get; set; }
        public List<Formacao> Formacao { get; set; }
        public List<Experiencia_Profissional> Experiencia_Profissional { get; set; }
        public List<Habilidades> Habilidade { get; set; }
        public List<Cursos_Extras> Cursos { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}