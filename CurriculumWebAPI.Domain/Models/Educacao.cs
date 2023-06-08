using System.Diagnostics.CodeAnalysis;

namespace CurriculumWebAPI.Domain.Models
{
    public class Educacao
    {
        [AllowNull]
        public int? Id { get; set; }
        [AllowNull]
        public string? Instituicao { get; set; }
        [AllowNull]
        public string? Curso { get; set; }
        [AllowNull]
        public int? AnoConclusao { get; set; }
    }
}