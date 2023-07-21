using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System;


namespace CurriculumWebAPI.Domain.Models.CurriculumBody
{
    public class Habilidades
    {
        public int Id { get; set; }

        public string Nome_Habilidade { get; set; }

        [AllowNull]
        public string Descricao { get; set; }

        [ForeignKey("Curriculum")]
        [Required]
        public string CurriculumId { get; set; }

        public Curriculum Curriculum { get; set; }



        public override string ToString()
        {
            if (string.IsNullOrEmpty(Descricao))
                return $"{Nome_Habilidade} \n {Descricao}";

            else return $"{Nome_Habilidade}";
        }
    }
}
