namespace BlApi;

/// <summary>
/// interface IProduct
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Product list request 
    /// </summary>
    /// <returns></returns>
    IEnumerable<BO.ProductForList?> GetProducts(Func<BO.ProductForList?, bool>? func = null);
    BO.ProductForList? GetProduct(int id);
    /// <summary>
    /// Product details request- Gets a product ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    BO.Product ItemProduct(int id);
    /// <summary>
    /// Product details request- Gets a product ID and Cart
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    BO.ProductItem ItemProduct(int id, BO.Cart cart);
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="p"></param>
    void Add(BO.Product p);
    /// <summary>
    /// Product deletion
    /// </summary>
    /// <param name="id"></param>
    void Delete(int id);
    /// <summary>
    /// Update product datas
    /// </summary>
    /// <param name="p"></param>
    void Update(BO.Product p);
    IEnumerable<BO.ProductForList?> GetListedProductByCategory(BO.Category c);
    public IEnumerable<BO.ProductItem?> GetProductItem(BO.Cart cart, Func<BO.ProductItem, bool>? fanc = null);
}
