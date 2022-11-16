using FluentAssertions;
using FluentAssertions.Primitives;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq.Expressions;

namespace Fsc.ExpressionBuilder.Test.Unit.Helpers
{
    public static class ExtensionMethods
    {
        public static AndConstraint<ObjectAssertions> BeNullChecking(this ObjectAssertions should, string propertyName, bool not = false)
        {
            Assert.That(should.Subject, Is.AssignableTo<Expression>());
            var nullCheck = (BinaryExpression)should.Subject;
            Assert.That(nullCheck.Left, Is.AssignableTo<MemberExpression>());
            Assert.That((nullCheck.Left as MemberExpression).Member.Name, Is.EqualTo(propertyName));
            Assert.That(nullCheck.NodeType, Is.EqualTo(not ? ExpressionType.Equal : ExpressionType.NotEqual));
            Assert.That(nullCheck.Right, Is.AssignableFrom<ConstantExpression>());
            Assert.That((nullCheck.Right as ConstantExpression).Value, Is.EqualTo(null));

            return new AndConstraint<ObjectAssertions>(should);
        }

        public static AndConstraint<ObjectAssertions> BeAnExpressionCheckingIf(this ObjectAssertions should, string propertyName, ExpressionType expressionType, object value)
        {
            Assert.That(should.Subject, Is.AssignableTo<Expression>());
            var expression = (BinaryExpression)should.Subject;
            Assert.That(expression.Left, Is.AssignableTo<MemberExpression>());
            Assert.That((expression.Left as MemberExpression).Member.Name, Is.EqualTo(propertyName));
            Assert.That(expression.NodeType, Is.EqualTo(expressionType));
            Assert.That(expression.Right, Is.AssignableFrom<ConstantExpression>());
            Assert.That((expression.Right as ConstantExpression).Value, Is.EqualTo(value));

            return new AndConstraint<ObjectAssertions>(should);
        }

        public static AndConstraint<ObjectAssertions> BeAStringExpressionCheckingIf(this ObjectAssertions should, string propertyName, ExpressionType expressionType, object value, bool trimToLowerProperty, bool trimToLowerValue)
        {
            Assert.That(should.Subject, Is.AssignableTo<Expression>());
            var expression = (BinaryExpression)should.Subject;

            var property = (MemberExpression)(trimToLowerProperty ? expression.Left.ExtractTrimToLowerProperty(propertyName) : expression.Left);
            property.Member.Name.Should().Be(propertyName);

            Assert.That(expression.NodeType, Is.EqualTo(expressionType));

            var constant = (ConstantExpression)(trimToLowerValue ? expression.Right.ExtractTrimToLowerConstant() : expression.Right);
            constant.Value.Should().Be(value);

            return new AndConstraint<ObjectAssertions>(should);
        }

        public static MemberExpression ExtractTrimToLowerProperty(this Expression expression, string propertyName)
        {
            var toLower = (MethodCallExpression)expression;
            var trim = (MethodCallExpression)toLower.Object;
            return (MemberExpression)trim.Object;
        }

        public static ConstantExpression ExtractTrimToLowerConstant(this Expression expression)
        {
            var toLower = (MethodCallExpression)expression;
            var trim = (MethodCallExpression)toLower.Object;
            return (ConstantExpression)trim.Object;
        }
    }
}