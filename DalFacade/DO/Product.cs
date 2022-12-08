namespace DO;

/// <summary>
/// struct of product
/// </summary>
public struct Product
{
    /// <summary>
    /// The ID number of the product
    /// </summary>
    public int ID { get; set; } 
    /// <summary>
    /// The name of the product
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The price of the product
    /// </summary>
    public double Price { get; set; } 
    /// <summary>
    /// The kind of the product
    /// </summary>
    public Category Category { get; set; }  
    /// <summary>
    /// The amount of the product in stock
    /// </summary>
    public int InStock { get; set; } 
    /// <summary>
    /// An action that prints the order
    /// </summary>
    /// <returns></returns>
    public override string ToString()=> this.ToStringProperty();
}
