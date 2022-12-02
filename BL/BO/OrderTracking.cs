
namespace BO;

internal class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { set; get; }

    public List<Tuple<DateTime,string> >? Tracking { set; get; }
}
