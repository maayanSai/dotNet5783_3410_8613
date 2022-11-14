using DO;
using System.Diagnostics.Contracts;

namespace Dal;

public class DalProduct
{
    public static void AddToProduct(Product p)
    {
        for (int i = 0; i < DataSource.Config.ArrProductIndex; i++)
        {
            if (p.ID != DataSource.productArr[i].ID)
            {
                DataSource.productArr[DataSource.Config.ArrProductIndex] = p;
                DataSource.Config.ArrProductIndex++;
            }
            else
                throw new Exception("the ID is already exists");
        }
    }
    public static Product getProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.ArrProductIndex; i++)
            if (DataSource.productArr[i].ID == id)
                return DataSource.productArr[i];
        throw new Exception("the object was not found");
    }
    public static Product[] allProduct()
    {
        Product[] Arr = new Product[DataSource.Config.ArrProductIndex];
        for (int i = 0; i<DataSource.Config.ArrProductIndex; i++)
          Arr[i]=DataSource.productArr[i];
        return Arr;
    }
    public static void deleteProcuct(int id)
    {
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
        if(!isFind)
            throw new Exception("the object was not found");
    }
    public static void updateProcuct(Product p)
    {
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
