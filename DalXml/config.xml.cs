using System;
using System.Collections.Generic;
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

    public static int nextOrderId()
    {

        List <int?> config  = XMLTools.LoadListFromXMLSerializer<int>("config");
        int? id = config?.First();

        premontOrderNumber();
        return id??throw new Exception();
    }
    static void premontOrderNumber()
    {
        List<int?> config = XMLTools.LoadListFromXMLSerializer<int>("config");
        int? id = config?.First();
        List<int?> config1 =new() { id+1, config?.Last() };
        XMLTools.SaveListToXMLSerializer(config1, "config");
    }
    public static int nextOrderItemId()
    {
        List<int?> config = XMLTools.LoadListFromXMLSerializer<int>("config");
        int? id = config?.Last();

        premontOrderItemNumber();
        return id??throw new Exception();
    }
    static void premontOrderItemNumber()
    {
        List<int?> config = XMLTools.LoadListFromXMLSerializer<int>("config");
        int? id = config?.Last();
        List<int?> config1 =new() { config?.First(), id+1 };
        XMLTools.SaveListToXMLSerializer(config1, "config");
    }
    



}
