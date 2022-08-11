using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OrientalMedical.Shared.Utilities.Helpers
{
    public static class ExpressionHelper
    {
        public static string PathOf<T>(this T @object, Expression<Func<T, object>> expression)
        {
            return PathOf<T>(expression);
        }

        public static string PathOf<T>(Expression<Func<T, object>> expr)
        {
            MemberExpression memberExpression;

            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var unaryExpression = expr.Body as UnaryExpression;
                    memberExpression = ((unaryExpression != null) ? unaryExpression.Operand : null) as MemberExpression;
                    break;
                default:
                    memberExpression = expr.Body as MemberExpression;
                    break;
            }

            var path = string.Empty;
            while (memberExpression != null)
            {
                string propertyName = memberExpression.Member.Name;
                path += "." + propertyName;
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return path.Substring(path.IndexOf('.') + 1);
        }
    }
}
