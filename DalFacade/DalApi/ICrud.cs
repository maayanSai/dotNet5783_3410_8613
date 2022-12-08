namespace DalApi;

/// <summary>
/// Interface to define CRUD actions for all the DAL entities:
///    C - Create request
///    R - Read request - for a single entity or collection of entities
///    U - Update request
///    D - Delete request
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface ICrud<T> where T : struct
{
    /// <summary>
    /// Request creating a new instance of an entity
    /// </summary>
    /// <param name="item">the new instance to be added</param>
    /// <returns>id of the new instance</returns>
    int Add(T item);
    /// <summary>
    /// Request to delete an instance of an entity
    /// </summary>
    /// <param name="id"></param>
    void Delete(int id);
    /// <summary>
    /// A request to update an instance of an entity
    /// </summary>
    /// <param name="item"></param>
    void Update(T item);
    /// <summary>
    /// A request to return an instance of an entity by ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    T GetById(int id);
    /// <summary>
    /// A request to return all instances of an entity
    /// </summary>
    /// <returns></returns>
    IEnumerable<T?> GetAll();
}



