
namespace BO;

public class Exceptions
{
    public class BODoesNotExistException : Exception
    {
        public BODoesNotExistException(string? message) : base(message) { }
    }
    public class BOAlreadyExistsException : Exception
    {
        public BOAlreadyExistsException(string? message) : base(message) { }

      
    }
   public class BlInvalidInputException:Exception
    {
        public BlInvalidInputException(string? message) : base(message) { } 
    }
    public class BlNullPropertyException : Exception
    {
        public BlNullPropertyException(string? message) : base(message) { }
    }
}




