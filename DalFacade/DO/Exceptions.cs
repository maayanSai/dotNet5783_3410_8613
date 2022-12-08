namespace DO;

public class DalMissingIdException: Exception
{
    public int Entity1Id;
    public int ?Entity2Id;
    public string EntityName;
    int num;
    string Message;
    public DalMissingIdException(int id, string name,int a=0) : base()
    {
        Entity1Id = id;
        EntityName = name;
        num=a;
    }
   
    public DalMissingIdException(int id1,int id2, string name, string message,int b=1) : base(message)
    {
        Entity1Id = id1;
        Entity2Id=id2;
        EntityName = name;
        Message=message;
        num =1;
    }
    public override string ToString()
    {
        if (num==0)
            return Entity1Id+EntityName;
        else
            return EntityName+"not exsist for the id's :"+Entity1Id+ " "+Entity2Id  +$"{Message}";

    }
    //public DalMissingIdException(int id, string name, string message,Exception innerException) : base(message, innerException)
    //{
    //    EntityId = id;
    //    EntityName = name;
    //}
    
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
