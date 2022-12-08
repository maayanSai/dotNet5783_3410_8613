namespace BO;
using System.Reflection;

/// <summary>
/// To String Property
/// </summary>
public static class Tools
{
    /// <summary>
    /// to string property
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public static string ToStringProperty<T>(this T t)
    {
        string str = " ";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            str += "\n" + item.Name + ": ";
            if (item.GetValue(t, null) is IEnumerable<object>)
            {
                IEnumerable<object> list = (IEnumerable<object>)item.GetValue(obj: t, null);
                string s = String.Join(" ", list);
                str+=s;
            }
            else
                str+=item.GetValue(t, null);
        }
        return str+"\n";
    }
}
