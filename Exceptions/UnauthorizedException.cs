namespace clinic_system.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Unauthorized access") { }
        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception innerExeption) : base(message, innerExeption) { }
        
    }
}
