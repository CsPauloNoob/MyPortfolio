namespace CurriculumWebAPI.App.InputModels
{
    public class FormacaoInputModel
    {
        public Guid Id { get; set; }
        public string Instituicao { get; set; }
        public string Curso { get; set; }
        public int AnoConclusao { get; set; }
    }
}