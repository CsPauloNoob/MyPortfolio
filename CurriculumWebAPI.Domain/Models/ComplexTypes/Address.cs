using Microsoft.EntityFrameworkCore;

namespace CurriculumWebAPI.Domain.Models.ComplexTypes
{
    [Owned]
    public class Address
    {
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string NumeroCasa { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public override string ToString()
        {
            return $"{Rua}, N° {NumeroCasa}, {Bairro}, {Cidade}-{Estado}";
        }
    }
}