using DO;
namespace DalApi;
//צריך להיות פאבליק?
public interface ICrud<T>
{
    int Add(T entity);
    void Delete (T entity); 
    void Update(T entity);
    T Get(int id);
    //שם חוקי??
    IEnumerable<T> GetAll();   
}
public interface IProduct:ICrud<Product>
{

}
public interface IOrder: ICrud<Order>
{

}
public interface IOrderItem: ICrud<OrderItem>
{
    // איזה 2 פעולות נוספות יש לו?
}

public interface IDal 
{//לשאול על זה שזה רק גט ולא סט
    Product Product { get; }
    OrderItem Item { get; }
    OrderItem OrderItem { get; }
}