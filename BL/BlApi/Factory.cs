using System.Xml;

namespace BlApi;

/// <summary>
/// static class Factory
/// </summary>
public static class Factory
{
    /// <summary>
    /// static method Get. The method returns an instance of class Bl
    /// </summary>
    /// <returns></returns>
    public static IBl? Get()
    {
        return new BlImplementation.BL();
    }
}
