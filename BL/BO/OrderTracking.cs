
namespace BO;

internal class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { set; get; }

    public List<Tuple<DataTime,string> >? Tracking { set; get; }
}
