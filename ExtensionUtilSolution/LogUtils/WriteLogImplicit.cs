using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogUtils
{
    public static class WriteLogImplicit
    {
        public static void WriteFromList<T>(this List<T> lst, List<String> ListOfProperties = null, String strDebugLevel = "WriteInfo")
        {
            Type type = typeof(T);
            PropertyInfo[] props = null;
            if (ListOfProperties != null && ListOfProperties.Count > 0)
            {
                props = (from dat in type.GetProperties()
                         where (from h in ListOfProperties select h).Contains(dat.Name)
                         select dat).ToArray();
            }
            else
                props = type.GetProperties();
            lst.ForEach(x =>
            {
                WriteFromObject<T>(x, props, null, strDebugLevel);
            });
        }

        public static void WriteFromObject<T>(this T obj, PropertyInfo[] props = null, List<String> ListOfProperties = null, String strDebugLevel = "WriteInfo")
        {
            Type type = typeof(T);

            if (ListOfProperties != null && ListOfProperties.Count > 0)
            {
                props = (from dat in type.GetProperties()
                         where (from h in ListOfProperties select h).Contains(dat.Name)
                         select dat).ToArray();
            }

            if (props == null)
                props = type.GetProperties();

            Type tyep = typeof(ApplicationLog);
            MethodInfo theMedhod = tyep.GetMethod(strDebugLevel);

            foreach (PropertyInfo propinfo in props)
            {
                Object value = propinfo.GetValue(obj, null);
                Object[] param = new List<Object>() { String.Format("{0}: {1}", propinfo.Name, value) }.ToArray();
                theMedhod.Invoke(ApplicationLog.Instance, param);
            }
        }
    }
}
