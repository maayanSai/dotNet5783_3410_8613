using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace Simulator;
public class ReportAcceptingObject
{
    public DateTime AcceptTime;
    public DateTime TimeEndTreetment;
   public  BO.Order? Order;
}
public class ReportEndTreetment
{
    public string? TreemenMassage;
    public BO.Order? Order;
}

public static class Simulator
{
    readonly static BlApi.IBl? bl = BlApi.Factory.Get();
    private static volatile bool active;
    private static readonly Random _rnd = new();
    public delegate void reportAccepting( object? repAccpting );
    private static event Action<int, object?>? reportAccept1;
    public static event Action<int, object?>? ReportAccept1
    {
        add => reportAccept1 += value;
        remove => reportAccept1 -= value;
    }
    public delegate void reportEndTreetment( object? repEndTreeting);
    private static event Action<int, object?>? reportEndTreetment2;
    public static event Action<int, object?>? ReportEndTreetment2
    {
        add => reportEndTreetment2 += value;
        remove => reportEndTreetment2 -= value;
    }
    public delegate void end( object?    repAccpting);//
    private static event Action<int, object?>? end3;
    public static event Action<int, object?>? End3
    {
        add => end3+= value;
        remove=> end3 -= value;
    }
    private static void invokeList(Delegate[] delegates, params object[] objects)
    {
        foreach (var item in delegates)
        {
            item?.DynamicInvoke(objects);
        }
    }
        public static void activate()
        {
        Thread simo = new Thread(() =>
        {
            BO.Order? order=null;
            active=true;
            while (active)
            {
                string treemenMassage ;
                 order = bl?.Order.OrderEldest();
                if (order?.ID!=null)
                {
                    DateTime acceptTime = DateTime.Now;
                    int delay = _rnd.Next(3, 11);//לחזור
                    DateTime timeEndTreetment = (acceptTime.AddSeconds(delay));
                    ReportAcceptingObject repAccpt = new ReportAcceptingObject { AcceptTime=acceptTime,Order=order,TimeEndTreetment=timeEndTreetment };
                    invokeList(reportAccept1.GetInvocationList(), 1,repAccpt);
                    Thread.Sleep(delay*1000);
                    if (order.ShipDate==null)
                        order= bl?.Order.Updateshipping(order.ID);
                    else
                    {
                        if (order.DeliveryrDate==null)
                        {
                            try
                            {
                                order=bl?.Order.Updatesupply(order.ID);
                            }
                            catch { }
                        }    
                    }
                    treemenMassage= "end treetment for order: "+order.ID+" the order "+ order.Status.ToString()+"!";
                    ReportEndTreetment rptEndTreetment = new ReportEndTreetment { Order=order, TreemenMassage= treemenMassage };
                    reportEndTreetment2(2, rptEndTreetment);
                }
                else
                {
                    Thread.Sleep(1000);
                    active=false;
                    break;
                }
                Thread.Sleep(1000);
            }
            if (order?.ID==null)
                end3(3, "end all orders");
            else
                end3(3, "close");
        }
         );
        simo.Start();
        }
        public static void stopSimulator()
        {
            active=false;   
        }
}
