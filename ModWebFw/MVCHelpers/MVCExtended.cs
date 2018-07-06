using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModWebFw
{
    public static class MVCExtended
    {
        public static MvcHtmlString DisplayRowFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string title = "")
        {
            try
            {
                ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
                var value = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model;
                string val = "";
                if (value == null)
                    val = "-";
                else if (value is decimal || value is double || value is float)
                    val = string.Format("{0:0.00}", value);
                else if (value is DateTime)
                    val = Convert.ToDateTime(value).ToString("dd.MM.yyyy");
                else if (value is bool)
                    val = (bool)value ? "Evet" : "Hayır";
                else
                    val = value.ToString();
                return new MvcHtmlString("<tr><th class=\"col-xs-6\">" + title + "</th><td class=\"col-xs-6\">" + val + "</td></tr>");
            }
            catch (Exception e)
            {
                return new MvcHtmlString("");
            }
        }

        public static MvcHtmlString DisplayIndexValue<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string title = "")
        {
            try
            {
                ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
                var value = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model;
                string val = "";
                if (value == null)
                    val = "-";
                else if (value is decimal || value is double || value is float)
                    val = string.Format("{0:0.00}", value);
                else if (value is DateTime)
                    val = Convert.ToDateTime(value).ToString("dd.MM.yyyy");
                else if (value is bool)
                    val = (bool)value ? "<div class=\"checkbox\"><input type=\"checkbox\" value=\"true\"/></div>" : "<div class=\"checkbox\"><input type=\"checkbox\" value=\"false\"/></div>";
                else
                    val = value.ToString();
                return new MvcHtmlString(val);
            }
            catch (Exception e)
            {
                return new MvcHtmlString("");
            }
        }
        public static MvcHtmlString DisplayProp(this HtmlHelper html, object property)
        {
            try
            {
                string result;
                if (property == null)
                    result = "-";
                else if (property is decimal || property is double || property is float)
                    result = string.Format("{0:0.00}", property);
                else if (property is DateTime)
                    result = Convert.ToDateTime(property).ToString("dd.MM.yyyy");
                else
                    result = property.ToString();
                return new MvcHtmlString(result);
            }
            catch (Exception e)
            {
                return new MvcHtmlString("");
            }
        }
        public static MvcHtmlString DisplayPrice(this HtmlHelper html, decimal? money, string CurrencySign = "₺")
        {
            try
            {
                string result = "";
                decimal price = money ?? 0;
                result = string.Format("{0:0.00}", price) + " " + CurrencySign;
                return new MvcHtmlString(result);
            }
            catch (Exception e)
            {
                return new MvcHtmlString("");
            }
        }
    }
}
