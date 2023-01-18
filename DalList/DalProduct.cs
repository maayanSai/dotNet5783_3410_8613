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
        Product? P = DataSource.s_productsList.FirstOrDefault(x => x?.ID == id);
        
        return P;
    }
    /// <summary>
    /// Returns a collection of products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter)
    {
        if (DataSource.s_productsList.Count == 0)
            throw new UnFoundException("the list is empty"); 
        return filter is null ? DataSource.s_productsList.Select(product => product) : DataSource.s_productsList.Where(filter);
    }
    /// <summary>
    /// Product deletion
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalMissingIdException"></exception>
    public void Delete(int id)
    {
        if (DataSource.s_productsList.RemoveAll(x => x?.ID == id)==0)
            throw new UnFoundException( "the product for the id: "+id+" does not exsist");
    }
    /// <summary>
    /// Product update
    /// </summary>
    /// <param name="p"></param>
    public void Update(Product p)
    {
        Delete(p.ID); // if not found - exception is thrown from this method
        DataSource.s_productsList.Add(p);
        var v = from item in DataSource.s_productsList
                orderby item?.ID
                select item;
        
    }
    /// <summary>
    /// Returns a product by terms of
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    public Product? GetById(Func<Product?, bool>? filter)=> filter is null ? throw new UnFoundException("there is no func") : DataSource.s_productsList.First(x => filter(x));
}
