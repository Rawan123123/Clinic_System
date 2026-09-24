namespace clinic_system.Exeptions
{
    public class ValidationException : Exception
    {
        public object Details { get; set; }
        public ValidationException() : base("Validation Failed") {}
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message ,object details) : base(message)
        {
            Details = details;
        }

    }
}
