using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace OSX.WmiLib
{
    internal class WmiQueryBuilder : ExpressionVisitor
    {
        private WmiContext m_Context;
        private bool m_FetchAllFields;
        private StringBuilder sb;

        public WmiQueryBuilder(WmiContext context)
        {
            m_Context = context;
        }

        public string Translate(IWmiQueryable query, bool fetchAllFields = false)
        {
            m_FetchAllFields = fetchAllFields;
            var a = query.ElementType.GetCustomAttribute<WmiClassAttribute>(false);
            if (a == null)
                throw new InvalidOperationException();

            sb = new StringBuilder();
            var e = Evaluator.PartialEval(query.Expression);
            Visit(e);
            return sb.ToString();
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
                e = ((UnaryExpression)e).Operand;
            return e;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            switch (m.Method.Name)
            {
                case "Where":
                    this.Visit(m.Arguments[0]);
                    sb.Append(" WHERE ");
                    LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                    this.Visit(lambda.Body);
                    return m;
                case "Associators":
                    var arg = (ConstantExpression)m.Arguments[0];
                    sb.Append("ASSOCIATORS OF {");
                    sb.Append(WmiObject.GetClassName(arg.Value.GetType()));
                    sb.Append("=\"");
                    sb.Append(WmiObject.GetWmiQueryValue(((WmiObject)arg.Value).ID));
                    sb.Append("\"} WHERE ResultClass=");
                    sb.Append(WmiObject.GetClassName(m.Method.GetGenericArguments()[0]));
                    return m;
                case "Contains":
                    this.Visit(m.Object);
                    sb.Append(" LIKE \"%");
                    var oldSB = sb;
                    sb = new StringBuilder();
                    this.Visit(m.Arguments[0]);
                    var s = sb.ToString();
                    sb = oldSB;
                    sb.Append(s.Substring(1, s.Length - 2));
                    sb.Append("%\"");
                    return m;
            }
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            sb.Append("(");
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    sb.Append(" AND ");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    sb.Append(" OR");
                    break;
                case ExpressionType.Equal:
                    sb.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    sb.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    sb.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
            }
            this.Visit(b.Right);
            sb.Append(")");
            return b;
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    sb.Append(" NOT ");
                    this.Visit(u.Operand);
                    break;
                case ExpressionType.Convert:
                    this.Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            }
            return u;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            IWmiQueryable q = c.Value as IWmiQueryable;
            if (q != null)
            {
                // assume constant nodes w/ IQueryables are table references
                if (sb.Length == 0)
                {
                    sb.Append("SELECT ");
                    if (m_FetchAllFields)
                        sb.Append("*");
                    else
                        sb.Append(string.Join(",", WmiObject.GetWmiPropertyNames(q.ElementType)));
                    sb.Append(" FROM ");
                }
                sb.Append(WmiObject.GetClassName(q.ElementType));
            }
            else if (c.Value == null)
            {
                sb.Append("NULL");
            }
            else
            {
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        sb.Append(((bool)c.Value) ? 1 : 0);
                        break;
                    case TypeCode.String:
                        sb.AppendFormat("\"{0}\"", WmiObject.GetWmiQueryValue(c.Value));
                        break;
                    case TypeCode.Object:
                        throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", c.Value));
                    default:
                        sb.Append(c.Value);
                        break;
                }
            }
            return c;
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                if (m.Member.Name == "ID")
                    sb.Append(WmiObject.GetKeyPropertyName(m.Expression.Type));
                else
                {
                    var a = m.Member.GetCustomAttribute<WmiPropertyAttribute>();
                    if (a == null)
                        throw new MemberAccessException(string.Format("'{0}' is not a WMI property of class '{1}'", m.Member.Name, m.Type.Name));
                    sb.Append(a.Property ?? m.Member.Name);
                }
                return m;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }

    }
}
