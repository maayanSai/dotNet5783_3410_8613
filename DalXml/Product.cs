
using DO;
using DalApi;

namespace Dal;

internal class Product : IProduct
{
    string s_products = "Products";
    public int Add(DO.Product pro)
    { 
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if((listProducts.FirstOrDefault(x => x?.ID == pro.ID)!=null))
            throw new DalAlreadyExistsException(pro.ID, "product");
        listProducts.Add(pro);
        XMLTools.SaveListToXMLSerializer(listProducts, s_products);
        return pro.ID;
    }

    public void Delete(int id)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (listProducts.RemoveAll(x => x?.ID == id)==0)
            throw new UnFoundException("the product for the id: " + id + " does not exsist");
        XMLTools.SaveListToXMLSerializer(listProducts, s_products);
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (listProducts.Count == 0)
            throw new UnFoundException("the list is empty");
        return filter is null ? listProducts.Select(product => product) : listProducts.Where(filter);
    }

    public DO.Product? GetById(Func<DO.Product?, bool>? filter)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        return filter is null ? throw new UnFoundException("there is no func") : listProducts.First(x => filter(x));
    }

    public DO.Product? GetById(int id)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        
            return listProducts.FirstOrDefault(x => x?.ID == id)??throw new DO.UnFoundException(" the product for that id:"+id+"doesnot exsist");

       
    }

    public void Update(DO.Product pro)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Delete(pro.ID); // if not found - exception is thrown from this method
        listProducts.Add(pro);
        var v = from item in listProducts
                orderby item?.ID
                select item;
        XMLTools.SaveListToXMLSerializer(listProducts, s_products);
    }
}
