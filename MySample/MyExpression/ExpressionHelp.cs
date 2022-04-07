using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyExpression
{
    public static class ExpressionHelp
    {
        private static Expression<T> Combine<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            MyExpressionVisitor visitor = new MyExpressionVisitor(first.Parameters[0]);
            Expression bodyone = visitor.Visit(first.Body);
            Expression bodytwo = visitor.Visit(second.Body);
            return Expression.Lambda<T>(merge(bodyone, bodytwo), first.Parameters[0]);
        }
        public static Expression<Func<T, bool>> ExpressionAnd<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Combine(second, Expression.And);
        }
        public static Expression<Func<T, bool>> ExpressionOr<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Combine(second, Expression.Or);
        }
    }

    public class MyExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _Parameter { get; set; }

        public MyExpressionVisitor(ParameterExpression Parameter)
        {
            _Parameter = Parameter;
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return _Parameter;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node); //Visit会根据VisitParameter()方法返回的Expression修改这里的node变量
        }
    }


    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Phone> Phones { get; set; }

    }

    public class Phone
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Name { get; set; }

    }
}
