namespace Dal;
using DalApi;
using DO;

/// <summary>
/// Implementation of product functions
/// </summary>
internal class DalProduct : IProduct
{
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Add(Product p)
    {
        if ((DataSource.s_productsList.Exists(x => x?.ID == p.ID)))
            throw new DalAlreadyExistsException(p.ID, "product");
        else
        {
            DataSource.s_productsList.Add(p);
            return p.ID;
        }
    }

    public Product? GetById(int id)
    {
        return DataSource.s_productsList.FirstOrDefault(x => x?.ID == id);
    }

    /// <summary>
    /// Returns a collection of products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter)
    {
        if (DataSource.s_productsList.Count == 0)
            throw new Exception("the list is empty"); // לשנות שגיאה
        return filter is null ? DataSource.s_productsList.Select(order => order) : DataSource.s_productsList.Where(filter);
    }
    /// <summary>
    /// Product deletion
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalMissingIdException"></exception>
    public void Delete(int id)
    {
        if (DataSource.s_productsList.RemoveAll(x => x?.ID == id)==0)
            throw new DalMissingIdException(id, "product");
    }
    /// <summary>
    /// Product update
    /// </summary>
    /// <param name="p"></param>
    public void Update(Product p)
    {
        Delete(p.ID); // if not found - exception is thrown from this method
        DataSource.s_productsList.Add(p);
    }

    /// <summary>
    /// Returns a product by terms of
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    public Product? GetById(Func<Product?, bool>? filter)
    {
        // לשנות שגיאה
        return filter is null ? throw new Exception("there is no func"): DataSource.s_productsList.First(x => filter(x));
    }
}
