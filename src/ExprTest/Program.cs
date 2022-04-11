using System;
using System.Linq;
using System.Linq.Expressions;
using static System.Console;
using static System.Math;
namespace ExprTest
{
    class Program
    {

        #region 执行表达式的步骤
        /// <summary>
        /// 1. ready
        /// 2. create 
        /// 3. Compile
        /// 4. Execute
        /// </summary>
        public static void ExecuteExp()
        {
            // The expression tree to execute.  
            BinaryExpression be = Expression.Power(Expression.Constant(2D), Expression.Constant(3D));

            // Create a lambda expression.  
            Expression<Func<double>> le = Expression.Lambda<Func<double>>(be);

            // Compile the lambda expression.  
            Func<double> compiledExpression = le.Compile();

            // Execute the lambda expression.  
            double result = compiledExpression();

            // Display the result.  
            WriteLine(result);

            // This code produces the following output:  
            // 8  
        }

        public static void ExecuteExpwithParams()
        {
            // Add the following using directive to your code file:  
            // using System.Linq.Expressions;  

            // 1. ready
            // Manually build the expression tree for
            // the lambda expression num => num < 5.  
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
            // 2. create
            Expression<Func<int, bool>> lambda1 =
                Expression.Lambda<Func<int, bool>>(
                    numLessThanFive,
                    new ParameterExpression[] { numParam });

            // 3.complie and 4. execute
            WriteLine(lambda1.Compile()(99));
        }

        public static void complexExpression()
        {
            // Creating a parameter expression.  
            ParameterExpression value = Expression.Parameter(typeof(int), "value");

            // Creating an expression to hold a local variable.
            ParameterExpression result = Expression.Parameter(typeof(int), "result");

            // Creating a label to jump to from a loop.  
            LabelTarget label = Expression.Label(typeof(int));

            // Creating a method body.  
            BlockExpression block = Expression.Block(
                // Adding a local variable.  
                new[] { result },
                // Assigning a constant to a local variable: result = 1  
                Expression.Assign(result, Expression.Constant(1)),
                    // Adding a loop.  
                    Expression.Loop(
                       // Adding a conditional block into the loop.  
                       Expression.IfThenElse(
                           // Condition: value > 1  
                           Expression.GreaterThan(value, Expression.Constant(1)),
                           // If true: result *= value --  
                           Expression.MultiplyAssign(result,
                               Expression.PostDecrementAssign(value)),
                           // If false, exit the loop and go to the label.  
                           Expression.Break(label, result)
                       ),
                   // Label to jump to.  
                   label
                )
            );

            // Compile and execute an expression tree.  
            int factorial = Expression.Lambda<Func<int, int>>(block, value).Compile()(5);

            WriteLine(factorial);
            // Prints 120.  
        }


        /// <summary>
        /// 解析表达式树
        /// </summary>
        public static void ParsingExpressionTrees()
        {
            // Add the following using directive to your code file:  
            // using System.Linq.Expressions;  

            // Create an expression tree.  
            Expression<Func<int, bool>> exprTree = num => num < 5;

            // Decompose the expression tree.  
            ParameterExpression param = (ParameterExpression)exprTree.Parameters[0];
            BinaryExpression operation = (BinaryExpression)exprTree.Body;
            ParameterExpression left = (ParameterExpression)operation.Left;
            ConstantExpression right = (ConstantExpression)operation.Right;

            Console.WriteLine("Decomposed expression: {0} => {1} {2} {3}",
                              param.Name, left.Name, operation.NodeType, right.Value);

            // This code produces the following output:  

            // Decomposed expression: num => num LessThan 5  
        }
        #endregion

        #region 修改表达式树
        /// <summary>
        /// Modify Expression from  AndAlso to AndOr 
        /// </summary>
        public static void ModifyExpression()
        {
            Expression<Func<string, bool>> expr = name => name.Length > 10 && name.StartsWith("G");
            WriteLine(expr);
            WriteLine(expr.Body);
            AndAlsoModifier treeModifier = new AndAlsoModifier();
            Expression modifiedExpr = treeModifier.Modify((Expression)expr);

            WriteLine(modifiedExpr);

            /*  This code produces the following output:  

                name => ((name.Length > 10) && name.StartsWith("G"))  
                name => ((name.Length > 10) || name.StartsWith("G"))  
            */
        }
        /// <summary>
        /// Customize ExpressionVisitor
        /// </summary>
        public class AndAlsoModifier : ExpressionVisitor
        {
            public Expression Modify(Expression expression)
            {
                return Visit(expression);
            }

            protected override Expression VisitBinary(BinaryExpression b)
            {
                if (b.NodeType == ExpressionType.AndAlso)
                {
                    Expression left = this.Visit(b.Left);
                    Expression right = this.Visit(b.Right);

                    // Make this binary expression an OrElse operation instead of an AndAlso operation.  
                    return Expression.MakeBinary(ExpressionType.OrElse, left, right, b.IsLiftedToNull, b.Method);
                }

                return base.VisitBinary(b);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            ExecuteExp();
            ExecuteExpwithParams();
            ModifyExpression();

            ReadKey();
        }
    }
}
