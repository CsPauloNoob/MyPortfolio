using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Exceptions
{
    public class NotFoundInDatabaseException : Exception
    {
        public NotFoundInDatabaseException(string message) : base(message) { }

        public NotFoundInDatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
