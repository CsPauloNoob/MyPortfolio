namespace CurriculumWebAPI.Domain.Models
{
    public class Educacao
    {
        public int Id { get; set; }
        public string Instituicao { get; set; }
        public string Curso { get; set; }
        public int AnoConclusao { get; set; }
    }
}