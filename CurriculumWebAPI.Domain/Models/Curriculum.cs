using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace CurriculumWebAPI.Domain.Models
{
    public class Curriculum
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public string? ExperienciaProfissional { get; set; }
        public string? Habilidades { get; set; }
        public string? SobreMim { get; set; }

        public List<Formacao> Formacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}