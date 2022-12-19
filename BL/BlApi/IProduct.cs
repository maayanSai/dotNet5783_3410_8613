namespace BlApi;
using BO;

/// <summary>
/// interface IProduct
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Product list request 
    /// </summary>
    /// <returns></returns>
    IEnumerable<ProductForList?> GetProducts(Func<ProductForList?, bool>? func = null);
    /// <summary>
    /// Product details request- Gets a product ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Product ItemProduct(int id);
    /// <summary>
    /// Product details request- Gets a product ID and Cart
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    ProductItem ItemProduct(int id, Cart cart);
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="p"></param>
    void Add(Product p);
    /// <summary>
    /// Product deletion
    /// </summary>
    /// <param name="id"></param>
    void Delete(int id);
    /// <summary>
    /// Update product datas
    /// </summary>
    /// <param name="p"></param>
    void Update(Product p);
    IEnumerable<ProductForList?> GetListedProductByCategory(BO.Category c);
}
