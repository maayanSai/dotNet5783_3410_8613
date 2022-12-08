namespace BlImplementation;
using BlApi;

/// <summary>
/// Realization of BL
/// </summary>
sealed public class Bl: IBl
{
    DalApi.IDal Dal = new Dal.DalList();
    /// <summary>
    /// Main logical entity - order
    /// </summary>
    public IOrder Order => new Order();
    /// <summary>
    /// Main logical entity - order
    /// </summary>
    public IProduct Product => new Product();
    /// <summary>
    /// Main logical entity - cart
    /// </summary>
    public ICart Cart => new Cart();
}
