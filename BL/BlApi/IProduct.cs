namespace BlApi;
using BO;
public interface IProduct
{
    IEnumerable<ProductForList?> GetProducts();
    Product? ItemProduct(int id);
    Product? ItemProduct(int id, Cart cart);
    void Add(Product p);
    void Delete(int id);
    void Update(Product p);
}
