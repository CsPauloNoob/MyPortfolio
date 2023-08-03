using CurriculumWebAPI.Domain.Models.ComplexTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CurriculumWebAPI.Domain.Models.CurriculumBody
{
    public class Contato
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public PhoneNumber Telefone { get; set; }
        public Address Endereco { get; set; }

        [ForeignKey("Curriculum")]
        [Required]
        public string CurriculumId { get; set; }

        public override string ToString()
        {
            var emailRow = $"E-mail: {Email}";
            var telefoneRow = Telefone.ToString();
            var enderecoRow = Endereco.ToString();

            return $"{emailRow}\n{telefoneRow}\n{enderecoRow}";
        }
       
    }
}