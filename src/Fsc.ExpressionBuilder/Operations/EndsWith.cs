using Fsc.ExpressionBuilder.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace Fsc.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a string "EndsWith" method call.
    /// </summary>
    public class EndsWith : OperationBase
    {
        private readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        /// <inheritdoc />
        public EndsWith()
            : base("EndsWith", 1, TypeGroup.Text) { }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Call(member, endsWithMethod, constant1)
                   .AddNullCheck(member);
        }
    }
}