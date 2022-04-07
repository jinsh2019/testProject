using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

using LaYumba.Functional;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using static LaYumba.Functional.F;
namespace UnitTestProject_HOF
{


    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void subStringPhoto()
        {
            var photo = "https://wfm-d.obs.cn-east-3.myhuaweicloud.com/PersonHeadImg/HeadImage_8e1ed067-462c-4995-994a-4ab07332414b?AccessKeyId=SUP8U4B7OBV6KKX1YRD5&Expires=1624265664&Signature=1nrVoRMCdxtHdWt9t5tFu1QQJa4%3D";
            photo = photo.Substring(photo.LastIndexOf("/")+1, photo.IndexOf("?")-1 - photo.LastIndexOf("/"));
            Console.WriteLine(photo);
        }
        /// <summary>
        /// 测试正则表达式
        /// </summary>
        [TestMethod]
        public void TestMatch()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                sb.Append(Guid.NewGuid().ToString() + ",");
            }

            var arrlookup = sb.ToString();
            string pat = @"\w{8}(-\w{4}){3}-\w{12}?";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(arrlookup);
        }
        [TestMethod]
        public void TestMethod9()
        {
            string str1 = @"[
	{
		""tableName"": ""identities"",
		""columns"": [
			{
				""key"": ""isPrimary"",
				""resourceKey"": ""common.label.primary"",
				""i18n"": ""主要""
			},
			{
				""key"": ""country"",
				""resourceKey"": ""m.conf.pers.customField.common.country"",
				""i18n"": ""国家／地区""
			},
			{
				""key"": ""identitieType"",
				""resourceKey"": ""m.conf.pers.customField.idCard.idType"",
				""i18n"": ""证件类型""
			},
			{
				""key"": ""identitieNumber"",
				""resourceKey"": ""m.conf.pers.customField.idCard.idNumber"",
				""i18n"": ""证件号码""
			},
			{
				""key"": ""issuingAuthority"",
				""resourceKey"": ""HRCC.setting.customField.idCard.issuingAuthority"",
				""i18n"": ""签发机关""
			},
			{
				""key"": ""effectDate"",
				""resourceKey"": ""m.conf.pers.customField.idCard.beginDate"",
				""i18n"": ""签发日期""
			},
			{
				""key"": ""expireDate"",
				""resourceKey"": ""m.conf.pers.customField.common.expireDate"",
				""i18n"": ""过期日期""
			}
		]
	}
]";

            // var rsCls =JsonConvert.DeserializeObject<cls>(str1);

        }
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var empty = new NameValueCollection();
                var green = empty["green"];
                WriteLine("green");

                var alsoEmpty = new Dictionary<string, string>();
                var blue = alsoEmpty["blue"];
                WriteLine("blue");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Namespace);
                throw;
            }

            Option<string> _ = None;
            Option<string> john = Some("John");
        }

        string greet(Option<string> greetee)
            => greetee.Match(None: () => "Sorry, who?", (name) => $"Hello,{name}");

        [TestMethod]
        public void greet()
        {
            greet(None);
            greet(Some("John"));
        }

        //public static Option<R> Map<T, R>
        //    (this Option<T> optT, Func<T, R> f)
        //    => optT.Match(
        //        () => None,
        //        (t) => Some(f(t))
        //        );

        Func<Apple, ApplePie> makePie = apple => new ApplePie(apple);


        [TestMethod]
        public void MakePieTest()
        {
            //Option<ApplePie> full = Some(new Apple());
            //Option<ApplePie> empty = None;
            //full.Map(makePie);
            //empty.Map(makePie);
        }
        [TestMethod]
        public void MapTest()
        {
            //Option<ApplePie> full = Some(new Apple());
            //Option<ApplePie> empty = None;
            //full.Map(makePie);
            //empty.Map(makePie);
            //new List<int>() {1, 2, 3}.ForEach(Write);
        }

        //public static IEnumerable<Unit> ForEach<T>
        //    (this IEnumerable<T> ts, Action<T> action)
        //    => ts.Map(action.ToFunc()).ToImmutableList();

        public IEnumerable Power(int number, int exponent)
        {
            int counter = 0;
            int result = 1;
            while (counter++ < exponent)
            {
                result = result * number;
                yield return result;
            }
        }
        public static IEnumerable<int> enumerableFuc()
        {
            yield return 1;
            yield return 2;
            yield break;
            yield return 3;
        }

        [TestMethod]
        public void YieldTest()
        {
            //foreach (var i in Power(2, 10))
            //{
            //    Write("{0} ", i);
            //}

            foreach (int item in enumerableFuc())
            {
                WriteLine(item);
            }
        }

        public List<Student> GetStudents()
        {
            // Use a collection initializer to create the data source. Note that each element
            //  in the list contains an inner sequence of scores.
            List<Student> students = new List<Student>
            {
                new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 72, 81, 60}},
                new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
                new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {99, 89, 91, 95}},
                new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {72, 81, 65, 84}},
                new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {97, 89, 85, 82}}
            };

            return students;

        }

        // 按布尔进行分组
        [TestMethod]
        public void GroupByBooleanTest()
        {
            // Obtain the data source.
            List<Student> students = GetStudents();

            // Group by true or false.
            // Query variable is an IEnumerable<IGrouping<bool, Student>>
            var booleanGroupQuery =
                from student in students
                group student by student.Scores.Average() >= 80; //pass or fail!

            // Execute the query and access items in each group
            foreach (var studentGroup in booleanGroupQuery)
            {
                WriteLine(studentGroup.Key == true ? "High averages" : "Low averages");
                foreach (var student in studentGroup)
                {
                    WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }
        }
        // 按数值范围进行分组
        [TestMethod]
        public void GroupByScopeTest()
        {
            // Obtain the data source.
            List<Student> students = GetStudents();

            // Write the query.
            var studentQuery =
                from student in students
                let avg = (int)student.Scores.Average()
                group student by (avg == 0 ? 0 : avg / 10) into g
                orderby g.Key
                select g;

            // Execute the query.
            foreach (var studentGroup in studentQuery)
            {
                int temp = studentGroup.Key * 10;
                WriteLine("Students with an average between {0} and {1}", temp, temp + 10);
                foreach (var student in studentGroup)
                {
                    WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }
        }

        //按复合键进行分组
        [TestMethod]
        public void GroupByMultiWordsTest()
        {
            //// Create a data source.
            //string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };

            //// Create the query.
            //var wordGroups =
            //    from w in words
            //    group w by w[0];

            //// Execute the query.
            //foreach (var wordGroup in wordGroups)
            //{
            //    WriteLine("Words that start with the letter '{0}':", wordGroup.Key);
            //    foreach (var word in wordGroup)
            //    {
            //        WriteLine(word);
            //    }
            //}

            // Create the data source.
            string[] words2 = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese", "elephant", "umbrella", "anteater" };

            // Create the query.
            var wordGroups2 =
                from w in words2
                group w by w[0] into grps
                where (grps.Key == 'a' || grps.Key == 'e' || grps.Key == 'i'
                       || grps.Key == 'o' || grps.Key == 'u')
                select grps;

            // Execute the query.
            foreach (var wordGroup in wordGroups2)
            {
                WriteLine("Groups that start with a vowel: {0}", wordGroup.Key);
                foreach (var word in wordGroup)
                {
                    WriteLine("   {0}", word);
                }
            }
        }

        [TestMethod]
        public void ZipTest()
        {
            var array1 = new int[] { 1, 2, 3, 4, 5 };
            var array2 = new int[] { 6, 7, 8, 9, 10 };

            // Add elements at each position together.
            var zip = array1.Zip(array2, (a, b) => (a + b));
            // Look at results.
            foreach (var value in zip)
            {
                WriteLine(value);
            }
        }

    }
    public class Student
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public List<int> Scores;
    }


    class Apple
    {

    }
    class ApplePie
    {
        public ApplePie(Apple apple)
        {
        }
    }

}
