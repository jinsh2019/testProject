using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ExpressionTest;
using GaiaWorks.HumanResource.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class ExprSocial
    {
        /// <summary>
        /// SimpleExpression
        /// </summary>
        [TestMethod]
        public void SimpleExpression()
        {
            //  Message�����ݱ��ʽ��ȡ��Ӧ���Ե�ֵ  
            PersonModel model = new PersonModel();
            model.ID = "1";
            model.Name = "����";
            model.Value = 90;
            model.InCome = 100;
            model.Pay = 200;
            model.Age = 33;

            var result = this.GetPropertyValue(model, l => l.Name);

            Debug.WriteLine($"��ʾ���ƣ�{result.Item1}-ֵ:{result.Item2}");
        }
        /// <summary>
        /// ͨ��Linq���ʽ��ȡ��Ա����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        private Tuple<string, string> GetPropertyValue<T>(T instance, Expression<Func<T, string>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            string propertyName = memberExpression.Member.Name;

            var obj = memberExpression.Member.GetCustomAttributes(false)[0] as System.ComponentModel.DescriptionAttribute;
            string attributeName = obj.Description;

            var property = typeof(T).GetProperties().Where(l => l.Name == propertyName).First();

            return new Tuple<string, string>(attributeName, property.GetValue(instance).ToString());

        }

        [TestMethod]
        public void CustomizeReport()
        {
            //  Message�����ݱ��ʽ��ȡ��Ӧ���Ե�ֵ  
            List<PersonModel> models = new List<PersonModel>();

            Random r = new Random();

            string[] names = { "��ѧ��", "����", "���»�", "������", "������", "�����" };

            //  Message�������������
            for (int i = 0; i < 80; i++)
            {
                PersonModel model = new PersonModel();
                model.ID = i.ToString();
                model.Name = names[r.Next(6)];
                model.Value = r.Next(20, 100);
                model.InCome = r.Next(20, 100);
                model.Pay = r.Next(20, 100);
                model.Age = r.Next(20, 100);
                models.Add(model);
            }


            //  Message�������Զ��屨��
            DataTable dt = this.GetSum(models.AsQueryable(),
                l => l.Name,
                l => l.Max(k => k.InCome),
                l => l.Min(k => k.InCome),
                l => l.Sum(k => k.InCome));

            WriteTable(dt);
        }

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="groupby"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        private DataTable GetSum<T>(IQueryable<T> collection, Expression<Func<T, string>> groupby, params Expression<Func<IQueryable<T>, double>>[] expressions)

        {
            DataTable table = new DataTable();

            //  Message�����ñ��ʽ���������� 

            MemberExpression memberExpression = groupby.Body as MemberExpression;

            var displayName = (memberExpression.Member.GetCustomAttributes(false)[0] as System.ComponentModel.DescriptionAttribute).Description;

            table.Columns.Add(new DataColumn(displayName));

            foreach (var expression in expressions)
            {
                MethodCallExpression dynamicExpression = expression.Body as MethodCallExpression;

                string groupName = dynamicExpression.Method.Name;

                UnaryExpression unaryexpression = dynamicExpression.Arguments[1] as UnaryExpression;

                LambdaExpression LambdaExpression = unaryexpression.Operand as LambdaExpression;


                memberExpression = LambdaExpression.Body as MemberExpression;

                displayName = (memberExpression.Member.GetCustomAttributes(false)[0] as System.ComponentModel.DescriptionAttribute).Description;

                table.Columns.Add(new DataColumn(displayName + $"({groupName})"));
            }

            //  Message��ͨ�����ʽ���������� 
            var groups = collection.GroupBy(groupby);

            foreach (var group in groups)
            {
                //  Message�����÷�����ͷ
                DataRow dataRow = table.NewRow();
                dataRow[0] = group.Key;

                //  Message�����÷����������
                for (int i = 0; i < expressions.Length; i++)
                {
                    var expression = expressions[i];

                    Func<IQueryable<T>, double> fun = expression.Compile();

                    dataRow[i + 1] = fun(group.AsQueryable());
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }

        [TestMethod]
        public void CustomizeReportABC()
        {
            //  Message�����ݱ��ʽ��ȡ��Ӧ���Ե�ֵ  
            List<PersonModel> models = new List<PersonModel>();

            Random r = new Random();

            string[] names = { "��ѧ��", "����", "���»�", "������", "������", "�����" };

            //  Message�������������
            for (int i = 0; i < 80; i++)
            {
                PersonModel model = new PersonModel();
                model.ID = i.ToString();
                model.Name = names[r.Next(6)];
                model.Value = r.Next(20, 100);
                model.InCome = r.Next(20, 100);
                model.Pay = r.Next(20, 100);
                model.Age = r.Next(20, 100);
                models.Add(model);
            }

            Func<DesignerCategoryAttribute, string> toHeader = l => l.Category;

            //  Message�������Զ��屨��
            DataTable dt = this.CustomizeQuery(models.AsQueryable(), toHeader, l => l.Name, l => l.Value);

            WriteTable(dt);
        }

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="groupby"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public DataTable CustomizeQuery<TModel, TAttr>(IQueryable<TModel> collection, Func<TAttr, string> toHeader,
            Expression<Func<TModel, String>> groupby, params Expression<Func<TModel, double>>[] expressions)
        {
            DataTable table = new DataTable();

            //  Message�����ñ��ʽ����������
            MemberExpression memberExpression = groupby.Body as MemberExpression;

            var displayName =
                toHeader((TAttr)memberExpression.Member.GetCustomAttributes(typeof(TAttr), false).First());

            table.Columns.Add(new DataColumn(displayName));

            foreach (var expression in expressions)
            {
                memberExpression = expression.Body as MemberExpression;

                displayName =
                    toHeader((TAttr)memberExpression.Member.GetCustomAttributes(typeof(TAttr), false).First());

                table.Columns.Add(new DataColumn(displayName));
            }

            //  Message��ͨ�����ʽ���������� 
            var groups = collection.GroupBy(groupby);

            foreach (var group in groups)
            {
                //  Message�����÷�����ͷ
                DataRow dataRow = table.NewRow();
                dataRow[0] = group.Key;

                //  Message�����÷����������
                for (int i = 0; i < expressions.Length; i++)
                {
                    var expression = expressions[i];

                    Func<TModel, double> fun = expression.Compile();

                    dataRow[i + 1] = group.Sum(fun);
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }

        private void WriteTable(DataTable dt)
        {
            string colums = string.Empty;
            ;
            foreach (DataColumn item in dt.Columns)
            {
                colums += item.ColumnName.PadRight(5, ' ') + " ";
            }

            Debug.WriteLine(colums);

            foreach (DataRow item in dt.Rows)
            {
                string rows = string.Empty;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    rows += item[i].ToString().PadRight(5, ' ') + " ";
                }

                Debug.WriteLine(rows);
            }
        }

        [TestMethod]
        public void OverrideJsonConverter()
        {
            var model = new Model();
            model.ID = 1;
            var json = JsonConvert.SerializeObject(model);//����IDֵΪ1���õ�jsonΪ{"ID":ture}

            Console.WriteLine(json);
            model.ID = 2;
            json = JsonConvert.SerializeObject(model);//����IDֵ��Ϊ1���õ�jsonΪ{"ID":false}
            Console.WriteLine(json);

        }

        [TestMethod]
        public void JsonTestDynamicModelConverter()
        {
            var view = new FullView();
            view.person = new Personal() { Name = "peter", BirthDate = DateTime.Now };
            view.Properties = new Dictionary<string, object>();
            view.Properties.Add("keyTest", "xx");
            var json = JsonConvert.SerializeObject(view);//����IDֵΪ1���õ�jsonΪ{"ID":ture}

            Console.WriteLine(json);

            var fullView = JsonConvert.DeserializeObject<FullView>(json);//����IDֵ��Ϊ1���õ�jsonΪ{"ID":false}
            //Console.WriteLine(json);

        }

        [JsonConverter(typeof(DynamicModelConverter<FullView, Personal>))]
        public class FullView
        {
            public Personal person { get; set; }
            public IDictionary<string, object> Properties { get; set; }
        }

        public class Personal
        {
            public string Name { get; set; }

            public DateTime BirthDate { get; set; }
        }

        [TestMethod]
        public void ExpressonTree()
        {

            Expression<Func<int, int>> expr = x => x + 1;
            Console.WriteLine(expr.ToString());  // x=> (x + 1)

            var lambdaExpr = expr as LambdaExpression;
            Console.WriteLine(lambdaExpr.Body);   // (x + 1)
            Console.WriteLine(lambdaExpr.ReturnType.ToString());  // System.Int32

            foreach (var parameter in lambdaExpr.Parameters)
            {
                Console.WriteLine("Name:{0}, Type:{1}, ", parameter.Name, parameter.Type.ToString());
            }
        }
        // ���� loop���ʽ��������������Ҫִ�еĴ���
        [TestMethod]
        public void LoopExpressonTree()
        {
            LabelTarget labelBreak = Expression.Label();
            ParameterExpression loopIndex = Expression.Parameter(typeof(int), "index");

            BlockExpression block = Expression.Block(
                new[] { loopIndex },
                // ��ʼ��loopIndex =1
                Expression.Assign(loopIndex, Expression.Constant(1)),
                Expression.Loop(
                    Expression.IfThenElse(
                        // if ���ж��߼�
                        Expression.LessThanOrEqual(loopIndex, Expression.Constant(10)),
                        // �ж��߼�ͨ���Ĵ���
                        Expression.Block(
                            Expression.Call(
                                null,
                                typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
                                Expression.Constant("Hello")),
                            Expression.PostIncrementAssign(loopIndex)),
                        // �жϲ�ͨ���Ĵ���
                        Expression.Break(labelBreak)
                    ), labelBreak));

            // ����������Ĵ������ʽ
            Expression<Action> lambdaExpression = Expression.Lambda<Action>(block);
            lambdaExpression.Compile().Invoke();
        }

        // ���� loop���ʽ��������������Ҫִ�еĴ���
        [TestMethod]
        public void BlockExpressonTree()
        {
            //ParameterExpression number = Expression.Parameter(typeof(int), "number");

            //BlockExpression myBlock = Expression.Block(
            //    new[] { number },
            //    Expression.Assign(number, Expression.Constant(2)),
            //    Expression.AddAssign(number, Expression.Constant(6)),
            //    Expression.DivideAssign(number, Expression.Constant(2)));

            //Expression<Func<int>> myAction = Expression.Lambda<Func<int>>(myBlock);
            //Console.WriteLine(myAction.Compile()());

            ParameterExpression param2 = Expression.Parameter(typeof(int));
            var expre = Expression.AddAssign(param2, Expression.Constant(20));
            BlockExpression block2 = Expression.Block(
                new[] { param2 }, expre

            );
            Expression<Func<int>> expr2 = Expression.Lambda<Func<int>>(block2);
            Console.WriteLine(expr2.Compile().Invoke());
        }

        [TestMethod]
        public void GotoExpression()
        {
            LabelTarget returnTarget = Expression.Label(typeof(Int32));
            LabelExpression returnLabel = Expression.Label(returnTarget, Expression.Constant(10, typeof(Int32)));

            // Ϊ����μ�+10֮�󷵻�
            ParameterExpression inParam3 = Expression.Parameter(typeof(int));
            BlockExpression block3 = Expression.Block(
                Expression.AddAssign(inParam3, Expression.Constant(10)),
                Expression.Return(returnTarget, inParam3),
                returnLabel);

            Expression<Func<int, int>> expr3 = Expression.Lambda<Func<int, int>>(block3, inParam3);
            Console.WriteLine(expr3.Compile().Invoke(20));
            // 30
        }

        [TestMethod]
        public void SwitchExpression()
        {
            //�򵥵�switch case ���
            ParameterExpression genderParam = Expression.Parameter(typeof(int));
            SwitchExpression swithExpression = Expression.Switch(
                genderParam,
                Expression.Constant("����"), //Ĭ��ֵ
                Expression.SwitchCase(Expression.Constant("��"), Expression.Constant(1)),
                Expression.SwitchCase(Expression.Constant("Ů"), Expression.Constant(0))
            //����Խ������Expression.Constant�滻���������ӵı��ʽ,ParameterExpression, BinaryExpression��, ��Ҳ�Ǳ��ʽ���ĵط�, ��Ϊ���������Ƕ��Ǽ̳���Expression, �������������õ��ĵط������Ի�����Ϊ�������ͽ��ܵ�,�������ǿ��Դ����������͵ı��ʽ��
            );

            Expression<Func<int, string>> expr4 = Expression.Lambda<Func<int, string>>(swithExpression, genderParam);
            Console.WriteLine(expr4.Compile().Invoke(1)); //��
            Console.WriteLine(expr4.Compile().Invoke(0)); //Ů
            Console.WriteLine(expr4.Compile().Invoke(11)); //����

            List<Model> myUsers = new List<Model>();
            myUsers.AsQueryable().Where(x => x.ID > 2);
        }

        [TestMethod]
        public void ExpressionVisitor()
        {
            List<User> myUsers = new List<User>();
            var userSql = myUsers.AsQueryable().Where(u => u.Age > 2);
            Console.WriteLine(userSql);
            // SELECT * FROM (SELECT * FROM User) AS T WHERE (Age>2)

            List<User> myUsers2 = new List<User>();
            var userSql2 = myUsers.AsQueryable().Where(u => u.Name == "Jesse");
            Console.WriteLine(userSql2);
            // SELECT * FROM (SELECT * FROM USER) AS T WHERE (Name='Jesse')

        }

        [TestMethod]
        public void ConvertExpr()
        {
            // Add the following directive to your file:
            // using System.Linq.Expressions;  

            // This expression represents a type conversion operation. 
            Expression convertExpr = Expression.Convert(
                Expression.Constant(5.5),
                typeof(Int16)
            );

            // Print out the expression.
            Console.WriteLine(convertExpr.ToString());

            // The following statement first creates an expression tree,
            // then compiles it, and then executes it.
            Console.WriteLine(Expression.Lambda<Func<Int16>>(convertExpr).Compile()());

            // This code example produces the following output:
            //
            // Convert(5.5)
            // 5
        }

        [TestMethod]
        public void LeftOuterJoinExample()
        {
            Person magnus = new Person { FirstName = "Magnus", LastName = "Hedlund", GetDate = DateTime.Today.AddDays(-10), departCode = "A" };
            Person terry = new Person { FirstName = "Terry", LastName = "Adams", GetDate = DateTime.Today.AddDays(-10), departCode = "A" };
            Person charlotte = new Person { FirstName = "Charlotte", LastName = "Weiss", GetDate = DateTime.Today.AddMonths(1), departCode = "b" };
            //Person arlene = new Person { FirstName = "Arlene", LastName = "Huff", GetDate = DateTime.Today.AddYears(1) };

            Pet barley = new Pet { Name = "Barley", Owner = terry, Birthday = DateTime.Today.AddDays(-7) };
            Pet boots = new Pet { Name = "Boots", Owner = terry, Birthday = DateTime.Today.AddDays(-14) };
            Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte, Birthday = DateTime.Today.AddDays(-30) };
            Pet bluemoon = new Pet { Name = "Blue Moon", Owner = terry };
            //Pet daisy = new Pet { Name = "Daisy", Owner = magnus, Birthday = DateTime.Today.AddDays(-180) };
            Pet daisy = new Pet { Name = "Daisy", Owner = magnus };


            Dept dp = new Dept() { name = "A Club", effectDate = DateTime.Today.AddYears(1), dpCode = "A" };
            Dept dp1 = new Dept() { name = "B Club", effectDate = DateTime.Today.AddYears(2), dpCode = "b" };
            // Create two lists.
            //List<Person> people = new List<Person> { magnus, terry, charlotte, arlene };
            List<Person> people = new List<Person> { magnus, terry, charlotte };
            List<Pet> pets = new List<Pet> { barley, boots, whiskers, bluemoon, daisy };

            List<Dept> dps = new List<Dept> { dp, dp1 };
            var dpq = dps.AsQueryable();
            var query = from person in people
                        join departs in dps on person.departCode equals departs.dpCode
                        join pet in pets on person equals pet.Owner into gj
                        from subpet in gj.DefaultIfEmpty()
                        where subpet.Birthday < departs.effectDate && (subpet.Birthday == null || subpet.Birthday < departs.effectDate)
                        select new { person.FirstName, PetName = subpet?.Name ?? String.Empty, person.GetDate };

            foreach (var v in query)
            {
                Console.WriteLine($"{v.FirstName + ":",-15}{v.PetName + ":",-15}{v.GetDate}");
            }
        }


        [TestMethod]
        public void CreateToken()
        {
            StringBuilder msg = new StringBuilder();
            var pwd = "123456";

            String timeStamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + "";
            var nonce = new Random().Next().ToString();
            msg.Append(Constant.TENANT).Append("wfmd3").Append(Constant.TIMESTAMP).Append(timeStamp).Append(Constant.NONCE).Append(nonce);
            Console.WriteLine("nonce"+ nonce);
            Console.WriteLine("timeStamp"+ timeStamp);
            Console.WriteLine(msg);
            Console.WriteLine(CreateToken(msg.ToString(), pwd));

        }
        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return ByteTostrign(hashmessage);
            }
        }
        private  string ByteTostrign(byte[] data)
        {
            StringBuilder sb =new StringBuilder();
            foreach (var b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }

            return sb.ToString();

        }
    }
    [JsonConverter(typeof(MyConverter))]
    public class Model
    {
        public int ID { get; set; }
    }
    public class MyConverter : JsonConverter
    {
        //�Ƿ����Զ��巴���л���ֵΪtrueʱ�������л�ʱ����ReadJson������ֵΪfalseʱ������ReadJson����������Ĭ�ϵķ����л�
        public override bool CanRead => false;
        //�Ƿ����Զ������л���ֵΪtrueʱ�����л�ʱ����WriteJson������ֵΪfalseʱ������WriteJson����������Ĭ�ϵ����л�
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(Model) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //newһ��JObject����,JObject�������������������json
            var jobj = new JObject();
            //value����ʵ��������Ҫ���л���Model�������Դ˴�ֱ��ǿת
            var model = value as Model;
            if (model.ID != 1)
            {
                //���IDֵΪ1�����һ����λ"ID"��ֵΪfalse
                jobj.Add("ID", false);
            }
            else
            {
                jobj.Add("ID", true);
            }
            //ͨ��ToString()������JObject����ת����json
            var jsonstr = jobj.ToString();
            //���ø÷�������json�Ž�ȥ���������л�Model�����json����jsonstr���ɴˣ����Ǿ����Զ�������л�������
            writer.WriteValue(jsonstr);
        }
    }

    class Dept
    {
        public string name { get; set; }
        public DateTime effectDate { get; set; }
        public string dpCode { get; set; }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime GetDate { get; set; }
        public string departCode { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
        public DateTime? Birthday { get; set; }
    }

    /// <summary> 
    /// ��дIQuerable��Where���������� MethodCallExpression �������ǵı��ʽ�����ࡣ
    /// �����ǵı��ʽ����������д��Ӧ�ľ�����ʷ���
    /// �ھ�����ʷ����У����ͱ��ʽ�������SQL��䡣
    /// </summary>
    public static class QueryExtensions
    {
        public static string Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            var expression = Expression.Call(null,
                                            ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(new Type[] { typeof(TSource) }),
                                    new Expression[] { source.Expression, Expression.Quote(predicate) });

            var translator = new QueryTranslator();
            return translator.Translate(expression);
        }
    }
    class QueryTranslator : ExpressionVisitor
    {
        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        public StringBuilder sb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        internal string Translate(Expression expression)
        {
            this.sb = new StringBuilder();
            this.Visit(expression);
            return this.sb.ToString();
        }
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(QueryExtensions) && m.Method.Name == "Where")
            {
                sb.Append("SELECT * FROM (");
                this.Visit(m.Arguments[0]);
                sb.Append(") AS T WHERE ");
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                this.Visit(lambda.Body);
                return m;
            }
            throw new NotSupportedException(string.Format("����{0}��֧��", m.Method.Name));
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            sb.Append("(");
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.And:
                    sb.Append(" AND ");
                    break;
                case ExpressionType.Or:
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
                    throw new NotSupportedException(string.Format("��Ԫ�����{0}��֧��", b.NodeType));
            }
            this.Visit(b.Right);
            sb.Append(")");
            return b;
        }


        protected override Expression VisitConstant(ConstantExpression c)
        {
            IQueryable q = c.Value as IQueryable;
            if (q != null)
            {
                // ���Ǽ��������Ǹ�Queryable���Ƕ�Ӧ�ı�
                sb.Append("SELECT * FROM ");
                sb.Append(q.ElementType.Name);
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
                        sb.Append("'");
                        sb.Append(c.Value);
                        sb.Append("'");
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
                sb.Append(m.Member.Name);
                return m;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }
    }



    //public static class Queryable
    //{
    //    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    //    {
    //        if (source == null)
    //        {
    //            throw new ArgumentNullException("source");
    //        }
    //        if (predicate == null)
    //        {
    //            throw new ArgumentNullException("predicate");
    //        }

    //        var expr = Expression.Call(null,
    //            ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(new Type[] { typeof(TSource) }),
    //            new Expression[] { source.Expression, Expression.Quote(predicate) });
    //        return source.Provider.CreateQuery<TSource>(expr);
    //    }
    //}
    internal class User
    {
        private DateTime _birthDate;
        public int Age { get; set; }
        public string Name { get; set; }

        public void SetBirthDate(DateTime value)
        {
            _birthDate = value;
        }

        public DateTime GetBirthDate()
        {
            return _birthDate;
        }
    }

}
