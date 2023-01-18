using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
using Dal;

namespace Dal;

 sealed internal class DalXml:IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IOrder Order => new Dal.Order();
    public IProduct Product => new Dal.Product();
    public IOrderItem OrderItem => new Dal.OrderItem();
}
