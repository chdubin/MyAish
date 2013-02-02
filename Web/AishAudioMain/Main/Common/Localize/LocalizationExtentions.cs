using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.ComponentModel;
using System.Reflection;

namespace Common.Localize
{
    public static class LocalizationExtensions
    {
        /*
         * Html.Resource("global, string")
         * Html.Resource("local")
         * Html.Resource("both", "Bill") // both == "Hello, {0}{1}!"
         */
        public static string Resource(this HtmlHelper html, string expr, params object[] args)
        {
            string path = ((WebFormView)html.ViewContext.View).ViewPath;

            ResourceExpressionFields fields = (ResourceExpressionFields)(new ResourceExpressionBuilder()).ParseExpression(
                expr,
                typeof(string),
                new ExpressionBuilderContext(path)
                );

            return (!string.IsNullOrEmpty(fields.ClassKey))
                ? string.Format((string)html.ViewContext.HttpContext.GetGlobalResourceObject(
                    fields.ClassKey,
                    fields.ResourceKey,
                    CultureInfo.CurrentUICulture),
                    args)

                : string.Format((string)html.ViewContext.HttpContext.GetLocalResourceObject(
                    path,
                    fields.ResourceKey,
                    CultureInfo.CurrentUICulture),
                    args);
        }

        public static string Resource2(this HtmlHelper html, object o, string expr, params object[] args)
        {
            string path = ((WebFormView)html.ViewContext.View).ViewPath;

            ResourceExpressionFields fields = (ResourceExpressionFields)(new ResourceExpressionBuilder()).ParseExpression(
                expr,
                typeof(string),
                new ExpressionBuilderContext(path)
                );

            return (!string.IsNullOrEmpty(fields.ClassKey))
                ? string.Format((string)html.ViewContext.HttpContext.GetGlobalResourceObject(
                    fields.ClassKey,
                    fields.ResourceKey,
                    CultureInfo.CurrentUICulture),
                    args)

                : string.Format((string)html.ViewContext.HttpContext.GetLocalResourceObject(
                    path,
                    fields.ResourceKey,
                    CultureInfo.CurrentUICulture),
                    args);
        }


        public static SelectList ToSelectList<T>(this T enumeration)
        {

            var source = Enum.GetValues(typeof(T));
            var items = new Dictionary<object, string>();
            var displayAttributeType = typeof(DisplayNameAttribute);

            foreach (var value in source)
            {
                FieldInfo field = value.GetType().GetField(value.ToString());
                DisplayNameAttribute attrs = (DisplayNameAttribute)field.
                    GetCustomAttributes(displayAttributeType, false).First();
                items.Add(value, attrs.DisplayName);
            }

            return new SelectList(items, "Key", "Value", enumeration);

        }
    }
}
