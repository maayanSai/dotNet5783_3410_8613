using DO;
namespace Dal;

public class DalProduct
{
    int x = DataSource.productArr.Length;
    public static void AddToProduct(Product p)
    {
        for (int i = 0; i < DataSource.Config.ArrProductIndex; i++)
        {
            if (p.ID == DataSource.productArr[i].ID)
            {
                throw new Exception("the ID is already exists");
            }
        }

        DataSource.productArr[DataSource.Config.ArrProductIndex] = p;
        DataSource.Config.ArrProductIndex++;

    }
    public static Product getProduct(int id)
    {
        int x = DataSource.productArr.Length;
        for (int i = 0; i < DataSource.Config.ArrProductIndex; i++)
            if (DataSource.productArr[i].ID == id)
                return DataSource.productArr[i];
        throw new Exception("the object was not found");
    }
    public static Product[] allProduct()
    {
        int x = DataSource.productArr.Length;
        Product[] Arr = new Product[DataSource.Config.ArrProductIndex];

        for (int i = 0; i < DataSource.Config.ArrProductIndex; i++)
            Arr[i] = DataSource.productArr[i];
        return Arr;
    }
    public static void deleteProcuct(int id)
    {
        int x = DataSource.productArr.Length;
        bool isFind = false;
        for (int i = 0; i < DataSource.Config.ArrProductIndex; i++)
        {
            if (DataSource.productArr[i].ID == id)
            {
                DataSource.productArr[i] = DataSource.productArr[DataSource.Config.ArrProductIndex];
                DataSource.Config.ArrProductIndex--;
                isFind = true;
            }
        }
        if (!isFind)
            throw new Exception("the object was not found");
    }
    public static void updateProcuct(Product p)
    {
        int x = DataSource.productArr.Length;
        bool isFind = false;
        for (int i = 0; i < DataSource.Config.ArrProductIndex; i++)
        {
            if (DataSource.productArr[i].ID == p.ID)
            {
                isFind = true;
                DataSource.productArr[i] = p;
            }
        }
        if (!isFind)
            throw new Exception("the object was not found");
    }
}
