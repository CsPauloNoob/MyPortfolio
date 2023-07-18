using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Models.ComplexTypes
{
    public class PhoneNumber
    {
        public string Codigo { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }

        public override string ToString()
        {
            return $"Telefone: +{Codigo} {DDD} {Numero}";
        }
    }
}