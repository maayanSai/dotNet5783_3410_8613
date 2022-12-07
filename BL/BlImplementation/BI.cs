namespace BlImplementation;
using BlApi;

sealed public class BI: IBl
{
    DalApi.IDal Dal = new Dal.DalList();
    public IOrder Order => new Order();

    public IProduct Product => new Product();

    public ICart Cart => new Cart();
}
