using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyExpression
{
    class Program
    {
        static void Main(string[] args)
        {

            //// Add the following directive to the file:
            //// using System.Linq.Expressions;  

            //// Creating a parameter for the expression tree.
            ParameterExpression param = Expression.Parameter(typeof(int));

            // Creating an expression for the method call and specifying its parameter.
            MethodCallExpression methodCall = Expression.Call(
                typeof(Console).GetMethod("WriteLine", new Type[] {typeof(int)}),
                param
            );

            // The following statement first creates an expression tree,
            // then compiles it, and then runs it.
            Expression.Lambda<Action<int>>(
                methodCall,
                new ParameterExpression[] {param}
            ).Compile()(10);

            //// This code example produces the following output:
            ////
            //// 10
            // Create a UnaryExpression that represents a
            // conversion of an int to an int?.


            //System.Linq.Expressions.UnaryExpression typeAsExpression =
            //    System.Linq.Expressions.Expression.TypeAs(
            //        System.Linq.Expressions.Expression.Constant(34, typeof(int)),
            //        typeof(int?));

            //Console.WriteLine(typeAsExpression.ToString());

            // This code produces the following output:
            //
            // (34 As Nullable`1)

            //// Create a TypeBinaryExpression that represents a
            //// type test of the string "spruce" against the 'int' type.
            //System.Linq.Expressions.TypeBinaryExpression typeBinaryExpression =
            //    System.Linq.Expressions.Expression.TypeIs(
            //        System.Linq.Expressions.Expression.Constant("spruce"),
            //        typeof(int));

            //Console.WriteLine(typeBinaryExpression.ToString());

            //// This code produces the following output:
            ////
            //// ("spruce" Is Int32)

            // Add the following directive to your file:
            // using System.Linq.Expressions;  

            // The block expression allows for executing several expressions sequentually.
            // When the block expression is executed,
            // it returns the value of the last expression in the sequence.
            BlockExpression blockExpr = Expression.Block(
                Expression.Call(
                    null,
                    typeof(Console).GetMethod("Write", new Type[] {typeof(String)}),
                    Expression.Constant("Hello ")
                ),
                Expression.Call(
                    null,
                    typeof(Console).GetMethod("WriteLine", new Type[] {typeof(String)}),
                    Expression.Constant("World!")
                ),
                Expression.Constant(42)
            );

            Console.WriteLine("The result of executing the expression tree:");
            // The following statement first creates an expression tree,
            // then compiles it, and then executes it.           
            var result = Expression.Lambda<Func<int>>(blockExpr).Compile()();

            // Print out the expressions from the block expression.
            Console.WriteLine("The expressions from the block expression:");
            foreach (var expr in blockExpr.Expressions)
                Console.WriteLine(expr.ToString());

            // Print out the result of the tree execution.
            Console.WriteLine("The return value of the block expression:");
            Console.WriteLine(result);

            // This code example produces the following output:
            //
            // The result of executing the expression tree:
            // Hello World!

            // The expressions from the block expression:
            // Write("Hello ")
            // WriteLine("World!")
            // 42

            // The return value of the block expression:
            // 42
        }

        public static void CreateMemberInitExpression()
        {
            System.Linq.Expressions.NewExpression newAnimal =
                System.Linq.Expressions.Expression.New(typeof(Animal));

            System.Reflection.MemberInfo speciesMember =
                typeof(Animal).GetMember("Species")[0];
            System.Reflection.MemberInfo ageMember =
                typeof(Animal).GetMember("Age")[0];

            // Create a MemberBinding object for each member
            // that you want to initialize.
            System.Linq.Expressions.MemberBinding speciesMemberBinding =
                System.Linq.Expressions.Expression.Bind(
                    speciesMember,
                    System.Linq.Expressions.Expression.Constant("horse"));
            System.Linq.Expressions.MemberBinding ageMemberBinding =
                System.Linq.Expressions.Expression.Bind(
                    ageMember,
                    System.Linq.Expressions.Expression.Constant(12));

            // Create a MemberInitExpression that represents initializing
            // two members of the 'Animal' class.
            System.Linq.Expressions.MemberInitExpression memberInitExpression =
                System.Linq.Expressions.Expression.MemberInit(
                    newAnimal,
                    speciesMemberBinding,
                    ageMemberBinding);

            Console.WriteLine(memberInitExpression.ToString());

            // This code produces the following output:
            //
            // new Animal() {Species = "horse", Age = 12}


            List<Person> PersonLists = new List<Person>()
            {
                new Person
                {
                    Name = "张三", Age = 20, Gender = "男",
                    Phones = new List<Phone>
                    {
                        new Phone {Country = "中国", City = "北京", Name = "小米"},
                        new Phone {Country = "中国", City = "北京", Name = "华为"},
                        new Phone {Country = "中国", City = "北京", Name = "联想"},
                        new Phone {Country = "中国", City = "台北", Name = "魅族"},
                    }
                },
                new Person
                {
                    Name = "松下", Age = 30, Gender = "男",
                    Phones = new List<Phone>
                    {
                        new Phone {Country = "日本", City = "东京", Name = "索尼"},
                        new Phone {Country = "日本", City = "大阪", Name = "夏普"},
                        new Phone {Country = "日本", City = "东京", Name = "松下"},
                    }
                },
                new Person
                {
                    Name = "克里斯", Age = 40, Gender = "男",
                    Phones = new List<Phone>
                    {
                        new Phone {Country = "美国", City = "加州", Name = "苹果"},
                        new Phone {Country = "美国", City = "华盛顿", Name = "三星"},
                        new Phone {Country = "美国", City = "华盛顿", Name = "HTC"}
                    }
                }
            };


            //Expression<Func<Person, bool>> expression = ex => ex.Age == 30;
            //expression = expression.ExpressionAnd(t => t.Name.Equals("松下"));
            //var Lists = PersonLists.Where(expression.Compile());
            //foreach (var List in Lists)
            //{
            //    Console.WriteLine(List.Name);
            //}
            //Console.Read();

            Expression<Func<Person, bool>> expression = ex => ex.Age == 20;
            expression = expression.ExpressionOr(t => t.Name.Equals("松下"));
            var Lists = PersonLists.Where(expression.Compile());
            foreach (var List in Lists)
            {
                Console.WriteLine(List.Name);
            }

            Console.Read();
        }

        class Animal
        {
            public string Species { get; set; }
            public int Age { get; set; }
        }

    }
}
