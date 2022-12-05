namespace DalApi;

/// <summary>
/// Interface to define CRUD actions for all the DAL entities:
///    C - Create request
///    R - Read request - for a single entity or collection of entities
///    U - Update request
///    D - Delete request
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface ICrud<T> where T : class
{
    /// <summary>
    /// Request creating a new instance of an entity
    /// </summary>
    /// <param name="item">the new instance to be added</param>
    /// <returns>id of the new instance</returns>
    int Add(T item);
    void Delete(int id);
    void Update(T item);
    T GetById(int id);
    //שם חוקי??
    IEnumerable<T?> GetAll();
}



