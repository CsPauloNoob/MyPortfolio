using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Models
{
    [NotMapped]
    public class Token
    {
        public string MyToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
