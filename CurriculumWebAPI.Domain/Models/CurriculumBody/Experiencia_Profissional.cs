using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Models.CurriculumBody
{
    public class Experiencia_Profissional
    {
        public int Id { get; set; }

        public string Nome_Organizacao { get; set; }

        public string Funcao { get; set; }

        [AllowNull]
        public string Descricao { get; set; }

        [ForeignKey("Curriculum")]
        [Required]
        public string CurriculumId { get; set; }

        public Curriculum Curriculum { get; set; }



        public override string ToString()
        {
            if (string.IsNullOrEmpty(Descricao))
                return $"{Funcao} em {Nome_Organizacao} \n {Descricao}";

            else return $"{Funcao} em {Nome_Organizacao}";
        }
    }
}