using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CurriculumWebAPI.Domain.Models.CurriculumBody
{

    public class Formacao
    {
        public int Id { get; set; }

        public string Instituicao { get; set; }

        public string Curso { get; set; }

        public int AnoConclusao { get; set; }

        [ForeignKey("Curriculum")]
        [Required]
        public string CurriculumId { get; set; }

        public Curriculum Curriculum { get; set; }
    }
}