using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FuncTest
{
    /// <summary>
    /// 表达式树映射关系
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class ExpressionMapper<TIn, TOut>
    {
        private static Func<TIn, TOut> _func = null;
        static ExpressionMapper()
        {
            ParameterExpression paramExp = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            // 绑定属性
            foreach (var item in typeof(TOut).GetProperties())
            {
                MemberExpression member = Expression.Property(paramExp, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, member);
                memberBindingList.Add(memberBinding);
            }
            // 绑定字段
            foreach (var item in typeof(TOut).GetFields())
            {
                MemberExpression member = Expression.Field(paramExp, typeof(TIn).GetField(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, member);
                memberBindingList.Add(memberBinding);
            }
            // 创建新对象并初始化
            MemberInitExpression memberInitExp = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());

            Expression<Func<TIn, TOut>> funcExp = Expression.Lambda<Func<TIn, TOut>>(memberInitExp, new ParameterExpression[] { paramExp });
            _func = funcExp.Compile();

        }

        public static TOut Mapper(TIn t)
        {
            return _func(t);
        }
    }
}
