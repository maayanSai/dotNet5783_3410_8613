using DalApi;
using DO;
namespace Dal;
internal class DalProduct : IProduct
{
    public int Add(Product p)
    {
        if ((DataSource.ProductsList.Exists(x => x?.ID == p.ID)))
            throw new Exception("the ID is already exists");
        else
        {
            DataSource.ProductsList.Add(p);
            return p.ID;
        }
    }
    public Product GetById(int id)
    {
        return DataSource.ProductsList.Find(x => x?.ID == id) ?? throw new Exception("product doesnt exist");
    }
    public IEnumerable<Product?> GetAll()
    {
        List<Product?> list = new List<Product?>();
        foreach (var product in DataSource.ProductsList)
            list.Add(product);
        return list;
    }
    public void Delete(int id)
    {
        if (DataSource.ProductsList.Exists(x => x?.ID == id))
            DataSource.ProductsList.RemoveAll(x => x?.ID == id);
        else
            throw new Exception("product doesnt exist");
    }

    public void Update(Product p)
    {
        if (DataSource.ProductsList.Exists(x => x?.ID == p.ID))
        {
            p.ID = GetById(p.ID).ID;
            DataSource.ProductsList.RemoveAll(x => x?.ID == p.ID);
            DataSource.ProductsList.Add(p);
        }
        else
            throw new Exception("product doesnt exist");
    }
}
