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
            throw new DalAlreadyExistsException(p.ID,"product");
        else
        {
            DataSource.s_productsList.Add(p);
            return p.ID;
        }
    }
    /// <summary>
    /// Returns a product by ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    public Product GetById(int id)=> DataSource.s_productsList.Find(x => x?.ID == id)
        ?? throw new DalMissingIdException(id,"product");
    /// <summary>
    /// Returns a collection of products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll()
    {
        IEnumerable<Product?> prod = (DataSource.s_productsList);
        return prod;
    }
    /// <summary>
    /// Product deletion
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalMissingIdException"></exception>
    public void Delete(int id)
    {
        if (DataSource.s_productsList.RemoveAll(x => x?.ID == id)==0)
            throw new DalMissingIdException(id,"product");
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
}
