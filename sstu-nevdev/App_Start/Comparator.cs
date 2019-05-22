using System;
using System.Linq;
using System.Reflection;

namespace sstu_nevdev.App_Start
{
    public static class Comparator
    {
        public static bool PropertiesThatContainText<T>(T obj, string text, 
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
               .Where(p => (p.PropertyType == typeof(string) || p.PropertyType == typeof(int) || 
                    p.PropertyType == typeof(int?)) && p.CanRead);
            foreach (PropertyInfo prop in properties)
            {
                string propVal = prop.GetValue(obj, null)?.ToString(); 
                if (propVal != null && propVal.IndexOf(text, 0, comparison) != -1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}