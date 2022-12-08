namespace DO;

/// <summary>
/// Exception
/// </summary>
public class DalMissingIdException: Exception
{
    public int Entity1Id;
    public int ?Entity2Id;
    public string EntityName;

    /// <summary>
    /// Missing Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public DalMissingIdException(int id, string name) : base()
    {
        Entity1Id = id;
        EntityName = name;
    }
    /// <summary>
    /// missing two id
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <param name="name"></param>
    /// <param name="message"></param>
    public DalMissingIdException(int id1,int id2, string name, string message) : base(message)
    {
        Entity1Id = id1;
        Entity2Id=id2;
        EntityName = name;
    }
    public override string ToString() => Entity1Id.ToString()+ Entity2Id?.ToString()+ base.ToString();
}

/// <summary>
/// Already Exists
/// </summary>
public class DalAlreadyExistsException: Exception
{
    public int EntityId;
    public string EntityName;
    public DalAlreadyExistsException(int id,string name) : base() 
    {
        EntityId = id;
        EntityName = name;  
    }
    public DalAlreadyExistsException(int id, string name,string message) : base(message)
    {
        EntityId = id;
        EntityName = name;
    }
    public DalAlreadyExistsException(int id, string name, string message, Exception innerException) : base(message, innerException)
    {
        EntityId = id;
        EntityName = name;
    }
}
