using DO;
namespace Dal;
public class DalProduct
{
    public static int Add(Product p)
    {
        if ((DataSource.ProductsList.Exists(x => x?.ID == p.ID)))
            throw new Exception("the ID is already exists");
        else
        {
            DataSource.ProductsList.Add(p);
            return p.ID;
        }
    }
    public static Product GetProduct(int id)
    {
        return DataSource.ProductsList.Find(x => x?.ID == id) ?? throw new Exception("product doesnt exist");
    }
    public static IEnumerable<Product?> AllProduct()
    {
        List<Product?> list = new List<Product?>();
        foreach (var product in DataSource.ProductsList)
            list.Add(product);
        return list;
    }
    public static void DeleteProcuct(int id)
    {
        if (DataSource.ProductsList.Exists(x => x?.ID == id))
            DataSource.ProductsList.RemoveAll(x => x?.ID == id);
        else
            throw new Exception("product doesnt exist");
    }

    public static void UpdateProcuct(Product p)
    {
        if (DataSource.ProductsList.Exists(x => x?.ID == p.ID))
        {
            p.ID = GetProduct(p.ID).ID;
            DataSource.ProductsList.RemoveAll(x => x?.ID == p.ID);
            DataSource.ProductsList.Add(p);
        }
        else
            throw new Exception("product doesnt exist");
    }
}
