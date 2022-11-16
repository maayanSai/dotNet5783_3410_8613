using DO;
namespace Dal;
public class DalProduct
{
    /// <summary>
    /// A function that adds a new product to the array of products
    /// </summary>
    public static void AddToProduct(Product p)
    {
        for (int i = 0; i < DataSource._arrProductIndex; i++) // A loop that goes through the entire array of products
        {
            if (p.ID == DataSource._productArr[i].ID) // If the product already exists in the array
                throw new Exception("the ID is already exists"); // throws an error
        }
        DataSource._productArr[DataSource._arrProductIndex] = p; // If it is not found, enter it
        DataSource._arrProductIndex++; // We will increase the number of existing products
    }
    /// <summary>
    /// A function that receives a product number and returns it
    /// </summary>
    public static Product GetProduct(int id)
    {
        for (int i = 0; i < DataSource._arrProductIndex; i++) // A loop that goes through the entire array of products
            if (DataSource._productArr[i].ID == id) // If the product exists
                return DataSource._productArr[i]; // He returns it
        throw new Exception("the object was not found"); // If it is not found, throws an error
    }
    /// <summary>
    /// A function that returns the entire array of products
    /// </summary>
    public static Product[] AllProduct()
    {
        Product[] Arr = new Product[DataSource._arrProductIndex]; // We will create a new array in the size of the number of products
        for (int i = 0; i < DataSource._arrProductIndex; i++) // A loop that goes through the entire array of products
            Arr[i] = DataSource._productArr[i]; // We will copy all the products to the new array
        return Arr; // We will return the copy array
    }
    /// <summary>
    /// A function that deletes a product from the array of products
    /// </summary>
    public static void DeleteProcuct(int id)
    {
        bool isFind = false; // Checks if the order is found
        for (int i = 0; i < DataSource._arrProductIndex; i++) // A loop that goes through the entire array of products
        {
            if (DataSource._productArr[i].ID == id) // If the product exists 
            {
                // Inserts the last product in the array in place of the deleted product
                DataSource._productArr[i] = DataSource._productArr[DataSource._arrProductIndex];
                DataSource._arrProductIndex--; // We will reduce the number of products
                isFind = true; // The product is found
            }
        }
        if (!isFind) // if not
            throw new Exception("the object was not found"); // throws an error
    }
    /// <summary>
    /// A function that updates a new product
    /// </summary>
    public static void UpdateProcuct(Product p)
    {
        bool isFind = false; // Checks if the order is found
        for (int i = 0; i < DataSource._arrProductIndex; i++) // A loop that goes through the entire array of products
        {
            if (DataSource._productArr[i].ID == p.ID) // If the product exists 
            {
                isFind = true; // The product is found
                DataSource._productArr[i] = p; // We will insert the new product in place of the existing product
            }
        }
        if (!isFind) // If the product does not exist, throws an error
            throw new Exception("the object was not found");
    }
}
