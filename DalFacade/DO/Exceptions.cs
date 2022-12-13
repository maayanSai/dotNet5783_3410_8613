namespace DO;

/// <summary>
/// Exception
/// </summary>
public class DalMissingIdException : Exception
{
    public int Entity1Id;
    public int? Entity2Id;
    public string EntityName;
    int num;
    string? Message1;
    /// <summary>
    /// Missing Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>


    public DalMissingIdException(int id, string name, int a = 0) : base()
    {
        Entity1Id = id;
        EntityName = name;
        num=a;
    }

    public DalMissingIdException(int idPro, int idOrder, string name, string message, int a = 1) : base(message)
    {
        Entity1Id = idPro;
        Entity2Id=idOrder;
        EntityName = name;
        Message1=message;
        num =a;
    }
    public override string ToString()
    {
        if (num==0)
            return EntityName+" id: "+Entity1Id+ " is missing";
        else
            return EntityName+"not exsist for the id's of product:"+Entity1Id+ "and order: "+Entity2Id  +$"{Message1}";

    }
}
public class DalMissingException:Exception
{
    public DalMissingException(int idPro, int idOrder, string name, string message, int a = 1) : base(message)
    {

    }

}

/// <summary>
/// Already Exists
/// </summary>
public class DalAlreadyExistsException : Exception
{
    public int EntityId;
    public string EntityName;
    public DalAlreadyExistsException(int id, string name) : base()
    {
        EntityId = id;
        EntityName = name;
    }

    public override string ToString()
    {
        return EntityName +"whith the id: " +EntityId+" is allredy exist";

    }
}
