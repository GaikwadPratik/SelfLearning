using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExtensionUtils
{    
    public static class MembersOf<T>
    {
        /// <summary>
        /// If we don't have an actual object we can still refer to its type with this generic static class
        /// </summary>
        public static string GetName<R>(Expression<Func<T, R>> expr)
        {
            var node = expr.Body as MemberExpression;
            if (object.ReferenceEquals(null, node))
                throw new InvalidOperationException("Expression must be of member access");
            return node.Member.Name;
        }
    }
}
