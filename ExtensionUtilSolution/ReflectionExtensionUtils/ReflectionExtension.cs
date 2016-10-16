using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExtensionUtils
{
    public static partial class ReflectionExtensions
    {
        /// <summary>
        /// An extentension method on an actual object to get its member names
        /// </summary>
        public static string MemberName<T, R>(this T obj, Expression<Func<T, R>> expr)
        {
            var node = expr.Body as MemberExpression;
            if (object.ReferenceEquals(null, node))
                throw new InvalidOperationException("Expression must be of member access");
            return node.Member.Name;
        }
    }
}
