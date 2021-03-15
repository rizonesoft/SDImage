using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    internal interface IWmiQueryable<TData> : IEnumerable<TData>, IWmiQueryable 
    {
        
    }

    internal interface IWmiQueryable : IEnumerable
    {
        Type ElementType { get; }
        Expression Expression { get; }
        WmiContext Context { get; }
    }

    internal static class WmiQueryable
    {
        public static IEnumerable<T> Where<T>(this IWmiQueryable<T> source, Expression<Func<T, bool>> predicate)
            where T: WmiObject<T>, new()
        {
            return new WmiQuery<T>(source.Context, Expression.Call(((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(typeof(T)), 
                source.Expression, Expression.Quote(predicate)));
        }

        public static IEnumerable<TResult> Associators<TResult>(this WmiObject source)
            where TResult : WmiObject<TResult>, new()
        {
            var q = new WmiQuery<TResult>(source.CreationContext, Expression.Call(((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(
                typeof(TResult)), Expression.Constant(source, typeof(WmiObject))));
            return q;
        }
    }
}
