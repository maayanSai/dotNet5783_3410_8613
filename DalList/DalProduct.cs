using DO;

namespace Dal;

public class DalProduct
{
     public static void AddToProduct(Product p)
    {
        for(int i=0; i< DataSource.Config.ArrProductIndex; i++)
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
}
