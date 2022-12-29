namespace BlImplementation;
using BlApi;

/// <summary>
/// Realization of BL
/// </summary>
sealed internal class BL : IBl
{
    private static readonly DalApi.IDal  dal = DalApi.Factory.Get()!;
    internal BL() { }
    /// <summary>
    /// Main logical entity - order
    /// </summary>
    public IOrder Order { get; } = new Order();
    /// <summary>
    /// Main logical entity - order
    /// </summary>
    public IProduct Product { get; } = new Product();
    /// <summary>
    /// Main logical entity - cart
    /// </summary>
    public ICart Cart { get; } = new Cart();
}
