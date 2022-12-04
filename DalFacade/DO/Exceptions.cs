namespace DO
{
    internal class DalDoesNotExistException: Exception
    {
        public DalDoesNotExistException(string? message) : base(message) { }
    }
    internal class DalAlreadyExistsException: Exception
    {
        public DalAlreadyExistsException(string? message) : base(message) { }
    }
}
