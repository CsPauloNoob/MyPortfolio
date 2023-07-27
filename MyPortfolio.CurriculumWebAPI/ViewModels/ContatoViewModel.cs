using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.App.ViewModels
{
    public class ContatoViewModel
    {
        public string Email { get; set; }

        public string Rua { get; set; }

        public string Bairro { get; set; }

        public string NumeroCasa { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Codigo { get; set; }

        public string DDD { get; set; }

        public int NumeroTelefone_Celular { get; set; }
    }
}