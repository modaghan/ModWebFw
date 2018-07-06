using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModWebFw
{
    public static class ObjUtils
    {
        /// <summary>
        /// Get approprite value of a property in object
        /// </summary>
        /// <typeparam name="T">Object type of property</typeparam>
        /// <param name="obj">Parent object of property</param>
        /// <param name="PropertyName">Name of the property</param>
        /// <returns></returns>
        public static T GetValue<T>(this object obj, string PropertyName)
        {
            try
            {
                return (T)obj.GetType().GetType().GetProperty(PropertyName).GetValue(obj);
            }
            catch (Exception)
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
}
