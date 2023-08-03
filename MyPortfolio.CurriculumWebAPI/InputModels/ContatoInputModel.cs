using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.App.InputModels
{
    public class ContatoInputModel
    {
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Rua { get; set; }

        [MaxLength(50)]
        public string Bairro { get; set; }

        [MaxLength(7)]
        public string NumeroCasa { get; set; }

        [MaxLength(30)]
        public string Cidade { get; set; }

        [MaxLength(2)]
        public string Estado { get; set; }

        [MaxLength(3)]
        public string Codigo { get; set; }

        [MaxLength(2)]
        public string DDD { get; set; }

        [MaxLength(10)]
        public string NumeroTelefone_Celular { get; set; }
    }
}
