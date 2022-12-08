namespace BlApi;

/// <summary>
/// interface IBl
/// </summary>
public interface IBl
{
    /// <summary>
    /// Main logical entity - order
    /// </summary>
    public IOrder Order { get; }
    /// <summary>
    /// Main logical entity - product
    /// </summary>
    public IProduct Product { get; }
    /// <summary>
    /// Main logical entity - cart
    /// </summary>
    public ICart Cart { get; }
}
