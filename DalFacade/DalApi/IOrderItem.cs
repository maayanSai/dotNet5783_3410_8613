using DO;
namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        OrderItem? GetByTwoId(int idProduct, int idOrder);
        IEnumerable<OrderItem?> GetByOrderId(int idOrder);
    }
}
