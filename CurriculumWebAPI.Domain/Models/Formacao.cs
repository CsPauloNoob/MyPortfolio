using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CurriculumWebAPI.Domain.Models
{
    
    public class Formacao
    {
        public Guid Id { get; set; }

        public string Instituicao { get; set; }

        public string Curso { get; set; }

        public int AnoConclusao { get; set; }

        [ForeignKey("Curriculum")]
        [Required]
        public Guid CurriculumId { get; set; }

        public Curriculum Curriculum { get; set; }
    }
}