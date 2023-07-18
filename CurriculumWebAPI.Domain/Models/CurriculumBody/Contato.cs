using CurriculumWebAPI.Domain.Models.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Models.CurriculumBody
{
    public class Contato
    {
        public string Email { get; set; }
        public PhoneNumber Telefone { get; set; }
        public Adress Endereco { get; set; }


        public override string ToString()
        {
            var emailRow = $"E-mail: {Email}";
            var telefoneRow = Telefone.ToString();
            var enderecoRow = Endereco.ToString();

            return $"{emailRow}\n{telefoneRow}\n{enderecoRow}";
        }
    }
}
