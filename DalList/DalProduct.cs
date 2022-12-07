using DalApi;
using DO;
namespace Dal;
internal class DalProduct : IProduct
{
    public int Add(Product p)
    {
        if ((DataSource.ProductsList.Exists(x => x?.ID == p.ID)))
            throw new DalAlreadyExistsException(p.ID,"product");
        else
        {
            DataSource.ProductsList.Add(p);
            return p.ID;
        }
    }
    public Product GetById(int id)=> DataSource.ProductsList.Find(x => x?.ID == id)
        ?? throw new DalMissingIdException(id,"product");
    public IEnumerable<Product?> GetAll() => new List<Product?>(DataSource.ProductsList);

    public void Delete(int id)
    {
        if (DataSource.ProductsList.RemoveAll(x => x?.ID == id)==0)
            throw new DalMissingIdException(id,"product");
    }

    public void Update(Product p)
    {
        Delete(p.ID); // if not found - exception is thrown from this method
        DataSource.ProductsList.Add(p);
    }
}
