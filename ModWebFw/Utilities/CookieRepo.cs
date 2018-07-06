using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ModWebFw
{
    public static class CookieRepo
    {
        public static T CSelectById<T>(this HttpContextBase Context, string list, int id)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(CookieManager.Get(Context, list)).SingleOrDefault(e => e.GetValue<int>("id") == id);
            }
            catch (Exception)
            {
                return Activator.CreateInstance<T>();
            }
        }
        public static T CSelectById<T>(this Controller Context, string list, int id)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(CookieManager.Get(Context, list)).SingleOrDefault(e => e.GetValue<int>("id") == id);
            }
            catch (Exception)
            {
                return Activator.CreateInstance<T>();
            }
        }

        public static List<T> CSelectByAll<T>(this HttpContextBase Context, string list)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(CookieManager.Get(Context, list));
            }
            catch (Exception)
            {
                return Activator.CreateInstance<List<T>>();
            }
        }
        public static List<T> CSelectByAll<T>(this Controller Context, string list)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(CookieManager.Get(Context, list));
            }
            catch (Exception)
            {
                return Activator.CreateInstance<List<T>>();
            }
        }

        public static void CAdd<T>(this HttpContextBase Context, string list, T entity)
        {
            try
            {
                List<T> entities = JsonConvert.DeserializeObject<List<T>>(CookieManager.Get(Context, list));
                entities.Add(entity);
                CookieManager.Save(Context, list, JsonConvert.SerializeObject(entities));
            }
            catch (Exception)
            {

            }
        }
        public static void CAdd<T>(this Controller Context, string list, T entity)
        {
            try
            {
                List<T> entities = JsonConvert.DeserializeObject<List<T>>(CookieManager.Get(Context, list));
                entities.Add(entity);
                CookieManager.Save(Context, list, JsonConvert.SerializeObject(entities));
            }
            catch (Exception)
            {

            }
        }

        public static void CUpdate<T>(this HttpContextBase Context, string list, T entity)
        {
            try
            {
                CDelete(Context, list, entity);
                CAdd(Context, list, entity);
            }
            catch (Exception)
            {

            }
        }
        public static void CUpdate<T>(this Controller Context, string list, T entity)
        {
            try
            {
                CDelete(Context, list, entity);
                CAdd(Context, list, entity);
            }
            catch (Exception)
            {

            }
        }

        public static void CDelete<T>(this HttpContextBase Context, string list, T entity)
        {
            try
            {
                List<T> entities = JsonConvert.DeserializeObject<List<T>>(CookieManager.Get(Context, list));
                List<T> newList = new List<T>();
                foreach (T ent in entities)
                {
                    if (!ent.GetType().GetProperty("id").GetValue(ent).Equals(entity.GetType().GetProperty("id").GetValue(entity)))
                    {
                        newList.Add(ent);
                    }
                }
                CookieManager.Save(Context, list, JsonConvert.SerializeObject(newList));
            }
            catch (Exception)
            {

            }
        }
        public static void CDelete<T>(this Controller Context, string list, T entity)
        {
            try
            {
                List<T> entities = JsonConvert.DeserializeObject<List<T>>(CookieManager.Get(Context, list));
                List<T> newList = new List<T>();
                foreach (T ent in entities)
                {
                    if (!ent.GetType().GetProperty("id").GetValue(ent).Equals(entity.GetType().GetProperty("id").GetValue(entity)))
                    {
                        newList.Add(ent);
                    }
                }
                CookieManager.Save(Context, list, JsonConvert.SerializeObject(newList));
            }
            catch (Exception)
            {

            }
        }
    }
}
