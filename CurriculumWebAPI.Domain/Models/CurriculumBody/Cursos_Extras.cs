using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Models.CurriculumBody
{
    public class Cursos_Extras
    {
        public int Id { get; set; }
        public string Nome_Curso { get; set; }
        public string Organizacao { get; set; }

        [ForeignKey("Curriculum")]
        [Required]
        public string CurriculumId { get; set; }

        public Curriculum Curriculum { get; set; }


        public override string ToString()
        {
            return $"{Nome_Curso} - {Organizacao}";
        }
    }
}