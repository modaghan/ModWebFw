using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModWebFw
{
    public static class Parser
    {
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
        public static T Parse<T>(this object obj)
        {
            try
            {
                if (obj == null)
                    return Activator.CreateInstance<T>();
                else if (obj.IsNumber())
                    return (T)Convert.ChangeType(obj, typeof(T));
                else if (obj is string)
                    return (T)Convert.ChangeType(obj.ToString().Replace('.', ','), typeof(T));
                else
                    return Activator.CreateInstance<T>();
            }
            catch (Exception)
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
}
