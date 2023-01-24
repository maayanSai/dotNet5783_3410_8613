using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal static class config
{
    readonly static string s_config = "config";
    internal const int s_startOrderNumber = 1000;
    private static int s_nextOrderNumber = s_startOrderNumber;
    internal const int s_startOrderItemNumber = 1000;
    private static int s_nextOrderItemNumber = s_startOrderItemNumber;

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static int nextOrderId()
    {

        List <int?> config  = XMLTools.LoadListFromXMLSerializer<int>("config");
        int? id = config?.First();

        premontOrderNumber();
        return id??throw new Exception();
    }

    private static void premontOrderNumber()
    {
        List<int?> config = XMLTools.LoadListFromXMLSerializer<int>("config");
        int? id = config?.First();
        List<int?> config1 =new() { id+1, config?.Last() };
        XMLTools.SaveListToXMLSerializer(config1, "config");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static int nextOrderItemId()
    {
        List<int?> config = XMLTools.LoadListFromXMLSerializer<int>("config");
        int? id = config?.Last();

        premontOrderItemNumber();
        return id??throw new Exception();
    }
     private static void premontOrderItemNumber()
    {
        List<int?> config = XMLTools.LoadListFromXMLSerializer<int>("config");
        int? id = config?.Last();
        List<int?> config1 =new() { config?.First(), id+1 };
        XMLTools.SaveListToXMLSerializer(config1, "config");
    }
    



}
