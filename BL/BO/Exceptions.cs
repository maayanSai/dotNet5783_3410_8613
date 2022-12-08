namespace BO;

    public class BlAlreadyExistEntityException : Exception
    {
        public BlAlreadyExistEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public override string ToString() => base.ToString() + $"Entity is already exist.";
    }

/// <summary>
/// if the entity is missing throw an exception
/// </summary>
[Serializable]
public class BlMissingEntityException : Exception
{
    string Message;
    int a;
    public BlMissingEntityException(string message, DO.DalMissingIdException innerException, int b = 0) : base(message, innerException)
    {
        a=b;
    }
    public BlMissingEntityException(string message, int b = 1) : base()
    {
        Message=message;
        a=b;
    }
    public override string ToString()
    {
        if (a==0)
            return InnerException.ToString() +Message +$"Missing Entity";
        else
            return $"{Message}";

    }
}
    /// <summary>
    /// if the Property null throw an exception
    /// </summary>
    [Serializable]
    public class BlNullPropertyException : Exception
    {
        public BlNullPropertyException(string message) : base(message) { }
    }
    /// <summary>
    /// if its not the category throw an exception
    /// </summary>
    [Serializable]
    public class BlWorngCategoryException : Exception
    {
        public BlWorngCategoryException(string message) : base(message) { }
    }
    /// <summary>
    /// if the date in correct throw an exception
    /// </summary>
    [Serializable]
    public class BlIncorrectDatesException : Exception
    {
        public BlIncorrectDatesException(string message) : base(message) { }
        /// <summary>
        /// if the Property null throw an exception
        /// </summary>
        [Serializable]
        public class BlNullPropertyException : Exception
        {
            public BlNullPropertyException(string message) : base(message) { }
        }
        /// <summary>
        /// if its not the category throw an exception
        /// </summary>
        [Serializable]
        public class BlWorngCategoryException : Exception
        {
           public BlWorngCategoryException(string message) : base(message) { }
        }
       

}
/// <summary>
/// if there is an empty string throw an exception
/// </summary>

[Serializable]
    public class BlEmptyStringException : Exception
    {
        public BlEmptyStringException(string? message) : base(message)
        {

        }
    }
    /// <summary>
    /// if its in correct throw an exception
    /// </summary>
    [Serializable]
    public class BlInCorrectException : Exception
    {
        public BlInCorrectException(string? message) : base(message) { }
    }






