namespace CurriculumWebAPI.Domain.Exceptions
{
    public class SaveFailedException  : Exception
    {
        public SaveFailedException () { }

        public SaveFailedException (string message) : base(message) { }

        public SaveFailedException (string message, Exception innerException) : base(message, innerException) { }
    }
}