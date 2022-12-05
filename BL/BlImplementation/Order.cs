

namespace BlImplementation;
using BlApi;
internal class Order:IOrder
{
    DalApi.IDal Dal = new Dal.DalList();
}
