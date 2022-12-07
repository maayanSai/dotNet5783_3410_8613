namespace DO;

public class DalMissingIdException: Exception
{
    public int EntityId;
    public string EntityName;
    public DalMissingIdException(int id, string name) : base()
    {
        EntityId = id;
        EntityName = name;
    }
    public DalMissingIdException(int id, string name,string message) : base(message)
    {
        EntityId = id;
        EntityName = name;
    }
    public DalMissingIdException(int id, string name, string message,Exception innerException) : base(message, innerException)
    {
        EntityId = id;
        EntityName = name;
    }
}
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
