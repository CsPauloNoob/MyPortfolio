using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Models.ComplexTypes
{
    public class Adress
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