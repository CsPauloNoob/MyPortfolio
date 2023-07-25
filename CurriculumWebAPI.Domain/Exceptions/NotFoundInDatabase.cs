using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Exceptions
{
    public class NotFoundInDatabase : Exception
    {
        public NotFoundInDatabase(string message) : base(message) { }

        public NotFoundInDatabase(string message, Exception innerException) : base(message, innerException) { }
    }
}
