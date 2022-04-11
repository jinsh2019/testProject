using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class ExprTreeFromMS
    {
        [TestMethod]
        public void InitSimpleExpressionTree()
        {
            // Add the following using directive to your code file:  
            // using System.Linq.Expressions;  

            // Manually build the expression tree for
            // the lambda expression num => num < 5.  
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
            Expression<Func<int, bool>> lambda1 =
                Expression.Lambda<Func<int, bool>>(
                    numLessThanFive,
                    new ParameterExpression[] { numParam });
        }


        [TestMethod]
        public void InitMathExpressionTree()
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

            Console.WriteLine(factorial);
            // Prints 120.  
        }

        [TestMethod]
        public void BreakExpressionTree()
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

        [TestMethod]
        public void CompileExpressionTree()
        {
            // Creating an expression tree.  
            Expression<Func<int, bool>> expr = num => num < 5;

            // Compiling the expression tree into a delegate.  
            Func<int, bool> result = expr.Compile();

            // Invoking the delegate and writing the result to the console.  
            Console.WriteLine(result(4));

            // Prints True.  

            // You can also use simplified syntax  
            // to compile and run an expression tree.  
            // The following line can replace two previous statements.  
            Console.WriteLine(expr.Compile()(4));

            // Also prints True.
        }

        [TestMethod]
        public void ExecuteExpressionTree()
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
            Console.WriteLine(result);

            // This code produces the following output:  
            // 8  
        }

        [TestMethod]
        public void ModifyExpressionTree()
        {
            Expression<Func<string, bool>> expr = name => name.Length > 10 && name.StartsWith("G");
            Console.WriteLine(expr);

            AndAlsoModifier treeModifier = new AndAlsoModifier();
            Expression modifiedExpr = treeModifier.Modify((Expression)expr);

            Console.WriteLine(modifiedExpr);

            /*  This code produces the following output:  

                name => ((name.Length > 10) && name.StartsWith("G"))  
                name => ((name.Length > 10) || name.StartsWith("G"))  
            */
        }

        [TestMethod]
        public void DynamicQueryExpressionTree()
        {
            // Add a using directive for System.Linq.Expressions.  

            string[] companies = { "Consolidated Messenger", "Alpine Ski House", "Southridge Video", "City Power & Light",
                   "Coho Winery", "Wide World Importers", "Graphic Design Institute", "Adventure Works",
                   "Humongous Insurance", "Woodgrove Bank", "Margie's Travel", "Northwind Traders",
                   "Blue Yonder Airlines", "Trey Research", "The Phone Company",
                   "Wingtip Toys", "Lucerne Publishing", "Fourth Coffee" };

            // The IQueryable data to query.  
            IQueryable<String> queryableData = companies.AsQueryable<string>();

            // Compose the expression tree that represents the parameter to the predicate.  
            ParameterExpression pe = Expression.Parameter(typeof(string), "company");

            // ***** Where(company => (company.ToLower() == "coho winery" || company.Length > 16)) *****  
            // Create an expression tree that represents the expression 'company.ToLower() == "coho winery"'.  
            Expression left = Expression.Call(pe, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            Expression right = Expression.Constant("coho winery");
            Expression e1 = Expression.Equal(left, right);

            // Create an expression tree that represents the expression 'company.Length > 16'.  
            left = Expression.Property(pe, typeof(string).GetProperty("Length"));
            right = Expression.Constant(16, typeof(int));
            Expression e2 = Expression.GreaterThan(left, right);

            // Combine the expression trees to create an expression tree that represents the  
            // expression '(company.ToLower() == "coho winery" || company.Length > 16)'.  
            Expression predicateBody = Expression.OrElse(e1, e2);

            // Create an expression tree that represents the expression  
            // 'queryableData.Where(company => (company.ToLower() == "coho winery" || company.Length > 16))'  
            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { queryableData.ElementType },
                queryableData.Expression,
                Expression.Lambda<Func<string, bool>>(predicateBody, new ParameterExpression[] { pe }));
            // ***** End Where *****  

            // ***** OrderBy(company => company) *****  
            // Create an expression tree that represents the expression  
            // 'whereCallExpression.OrderBy(company => company)'  
            MethodCallExpression orderByCallExpression = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                new Type[] { queryableData.ElementType, queryableData.ElementType },
                whereCallExpression,
                Expression.Lambda<Func<string, string>>(pe, new ParameterExpression[] { pe }));
            // ***** End OrderBy *****  

            // Create an executable query from the expression tree.  
            IQueryable<string> results = queryableData.Provider.CreateQuery<string>(orderByCallExpression);

            // Enumerate the results.  
            foreach (string company in results)
                Console.WriteLine(company);

            /*  This code produces the following output:  

                Blue Yonder Airlines  
                City Power & Light  
                Coho Winery  
                Consolidated Messenger  
                Graphic Design Institute  
                Humongous Insurance  
                Lucerne Publishing  
                Northwind Traders  
                The Phone Company  
                Wide World Importers  
            */
        }


        [TestMethod]
        public void myTest()
        {
            Expression<Func<int>>  exp = () => 1 + 2;
            myTest(exp);
        }
        /// <summary>
        /// <c>myTest</c>
        /// <code>(a,b)=>a+b</code>
        /// <see cref="voidTest"/>
        /// <seealso cref="voidTest"/>
        /// </summary>
        private void myTest(Expression<Func<int>> s)
        {

        }
        private  void voidTest(){ }
          
    }

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
}