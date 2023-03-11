namespace DO;

/// <summary>
/// Un Found
/// </summary>
public class UnFoundException : Exception
{
    public UnFoundException (string msg):base(msg) { }
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

    public override string ToString()=> EntityName + "whith the id: " + EntityId + " is allredy exist";

}
//public class DalMissingIdException : Exception
//{
//    public int EntityId;
//    public string EntityName;
//    public DalMissingIdException(int id, string name) : base()
//    {
//        EntityId = id;
//        EntityName = name;
//    }

//    public override string ToString()
//    {
//        return EntityName + "whith the id: " + EntityId + " is allredy exist";

//    }

//}
[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

