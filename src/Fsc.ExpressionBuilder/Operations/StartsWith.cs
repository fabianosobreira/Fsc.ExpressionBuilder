using Fsc.ExpressionBuilder.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace Fsc.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a string "StartsWith" method call.
    /// </summary>
    public class StartsWith : OperationBase
    {
        private readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

        /// <inheritdoc />
        public StartsWith()
            : base("StartsWith", 1, TypeGroup.Text) { }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Call(member, startsWithMethod, constant1)
                   .AddNullCheck(member);
        }
    }
}