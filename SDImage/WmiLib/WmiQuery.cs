using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    internal class WmiQuery<TData> : IWmiQueryable<TData>
        where TData : WmiObject<TData>, new()
    {
        private Expression m_Expression;
        private WmiContext m_Context;

        public WmiQuery(WmiContext context)
        {
            m_Context = context;
            m_Expression = Expression.Constant(this);
        }

        public WmiQuery(WmiContext context, Expression expression)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (expression == null)
                throw new ArgumentNullException("expression");

            m_Context = context;
            m_Expression = expression;
        }

        public IEnumerator<TData> GetEnumerator()
        {
            return m_Context.Execute<TData>(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Type ElementType
        {
            get { return typeof(TData); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return m_Expression; }
        }

        public WmiContext Context
        {
            get { return m_Context; }
        }
    }
}
