namespace CurriculumWebAPI.App.InputModels
{
    public class CurriculumInputModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public string? ExperienciaProfissional { get; set; }
        public string? Habilidades { get; set; }
        public string? SobreMim { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}