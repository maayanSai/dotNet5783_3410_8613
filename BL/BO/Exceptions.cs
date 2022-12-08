namespace BO;

/// <summary>
/// the Entity is Already Exist
/// </summary>
public class BlAlreadyExistEntityException : Exception
{
    public BlAlreadyExistEntityException(string message) : base(message) { }
    public BlAlreadyExistEntityException(string message, Exception innerException) : base(message, innerException) { }
    public override string ToString() => $"Entity is already exist." + base.ToString();
}

/// <summary>
/// if the entity is missing throw an exception
/// </summary>
[Serializable]
public class BlMissingEntityException : Exception
{
    public BlMissingEntityException(string message, Exception innerException) : base(message, innerException) { }
    public BlMissingEntityException(string message) : base(message) { }
    public override string ToString()=> base.ToString() + "missing entitiy";
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
}

/// <summary>
/// ןif the data in correct throw an exception
/// </summary>
public class BlInCorrectException : Exception
{
    //string Message1;
    public BlInCorrectException(string? message) : base(message) { } // { //Message1=message; }
    public override string ToString() => " bl-incorrect" + base.ToString();
}








