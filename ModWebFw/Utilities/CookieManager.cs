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
    public static class CookieManager
    {
        public static void Save(Controller Controller, string key, object entity)
        {
            HttpCookie cookie = Controller.Request.Cookies[key];
            if (cookie != null)
                cookie.Value = JsonConvert.SerializeObject(entity);
            else
            {
                cookie = new HttpCookie(key);
                cookie.Value = JsonConvert.SerializeObject(entity);
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Controller.Response.Cookies.Add(cookie);
        }
        public static void Save(Controller Controller, string key, string value)
        {
            HttpCookie cookie = Controller.Request.Cookies[key];
            if (cookie != null)
                cookie.Value = value;
            else
            {
                cookie = new HttpCookie(key);
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Controller.Response.Cookies.Add(cookie);
        }
        public static void Save(HttpContextBase Context, string key, string value)
        {
            HttpCookie cookie = Context.Request.Cookies[key];
            if (cookie != null)
                cookie.Value = value;
            else
            {
                cookie = new HttpCookie(key);
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Context.Response.Cookies.Add(cookie);
        }
        public static string Get(Controller Controller, string key)
        {
            HttpCookie cookie = Controller.Request.Cookies[key];
            if (cookie != null)
                return cookie.Value;
            else
            {
                return "";
            }
        }
        public static string Get(HttpContextBase Context, string key)
        {
            HttpCookie cookie = Context.Request.Cookies[key];
            if (cookie != null)
                return cookie.Value;
            else
            {
                return "";
            }
        }
        public static List<KeyValuePair<string, string>> GetAll(HttpRequestBase Request, string startsWith)
        {
            var keys = Request.Cookies.AllKeys.Where(k => k.StartsWith(startsWith));
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (var key in keys)
            {
                list.Add(new KeyValuePair<string, string>(key, Request.Cookies[key].Value));
            }
            return list;
        }
        public static List<KeyValuePair<string, string>> GetAll(HttpContextBase Context, string startsWith)
        {
            var keys = Context.Request.Cookies.AllKeys.Where(k => k.StartsWith(startsWith));
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (var key in keys)
            {
                list.Add(new KeyValuePair<string, string>(key, Context.Request.Cookies[key].Value));
            }
            return list;
        }
        public static void Delete(Controller Controller, string key)
        {
            try
            {
                HttpCookie cookie = Controller.Request.Cookies[key];
                if (cookie != null)
                {
                    Controller.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
                }
            }
            catch (Exception)
            {

            }
        }
        public static void Delete(HttpContextBase Context, string key)
        {
            try
            {
                HttpCookie cookie = Context.Request.Cookies[key];
                if (cookie != null)
                {
                    Context.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
