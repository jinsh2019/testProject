using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CSharpScript1
{

    class Program
    {
        static void Main(string[] args)
        {

            // 1. Evaluate a C# expression
            //  object result = CSharpScript.EvaluateAsync("1 + 2").Result;

            // 2. Evaluate a C# expression (strongly-typed)
            //  int result = CSharpScript.EvaluateAsync<int>("1 + 2").Result;

            // 3. Evaluate a C# expression with error handling
            //try
            //{
            //    Console.WriteLine( CSharpScript.EvaluateAsync("2+21").Result);
            //}
            //catch (CompilationErrorException e)
            //{
            //    Console.WriteLine(string.Join(Environment.NewLine, e.Diagnostics));
            //}

            // 4. Add references
            //  var result =  CSharpScript.EvaluateAsync("System.Net.Dns.GetHostName()", ScriptOptions.Default.WithReferences(typeof(System.Net.Dns).Assembly)).Result;


            // 5. Add namespace and type imports
            //var result = CSharpScript.EvaluateAsync("Directory.GetCurrentDirectory()",
            //    ScriptOptions.Default.WithImports("System.IO")).Result;

            // 6. Parameterize a script
            //var dics = new Dictionary<string, object> {{"myKey", 1}};
            //var instance = new ExpandoObject();
            //foreach (var item in dics)
            //{
            //    ((IDictionary<string, object>) instance)[item.Key] = item.Value;
            //}

            //foreach (var item in dics)
            //{
            //    var myScript = "(int)myKey+1";
            //    ((IDictionary<string, object>) instance)[item.Key] =
            //        CSharpScript.EvaluateAsync(myScript, globals: instance).Result;
            //}
            string resKeys = "m.conf.pers.customField.idCard.idType,m.conf.pers.customField.common.country";
            Dictionary<string,string> dic = new Dictionary<string, string>();
            dic.Add("m.conf.pers.customField.idCard.idType","111");
            dic.Add("m.conf.pers.customField.common.country","222");
            dic.Add("m.conf.pers.customField.common.country1","222");

            //dic.ForEach(x => { resKeys.Split(',').Contains(x.Key); });

                // Script that will use dynamic
            var scriptContent = "data.myKey + data.antherProperty";

            // data to be sent into the script
            dynamic expando = new ExpandoObject();
            //expando.X = 34;
            //expando.Y = 45;
            var dics = new Dictionary<string, object> {{"myKey", 1}, {"antherProperty", 2}};
            foreach (var item in dics)
            {
                ((IDictionary<string, object>)expando)[item.Key] = item.Value;
            }

            // setup references needed
            var refs = new List<MetadataReference>{
                        MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException).GetTypeInfo().Assembly.Location),
                        MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.DynamicAttribute).GetTypeInfo().Assembly.Location)};
            var script = CSharpScript.Create(scriptContent, options: ScriptOptions.Default.AddReferences(refs), globalsType: typeof(Globals));

            script.Compile();

            // create new global that will contain the data we want to send into the script
            var g = new Globals() { data = expando };

            //Execute and display result
            var r = script.RunAsync(g).Result;
            Console.WriteLine(r.ReturnValue);

            //Console.WriteLine(CSharpScript.EvaluateAsync(script, globals: globals).Result);

            /// start
            ///backup code for something

            //private void CustomizeProperty(DynamicValueEntity entity)
            //{
            //    if (!string.IsNullOrWhiteSpace(_configuration[Consts.HrBlacklistLevel]))
            //    {
            //        var myGlobals = entity;
            //        var myScript = _configuration[Consts.HrBlacklistLevel];
            //        var result = CSharpScript.EvaluateAsync(myScript, globals: myGlobals).Result;
            //        entity.PropertyValue = result?.ToString();
            //    }
            //    return;
            //}

            /// end
            // 7. Create & build a C# script and execute it multiple times
            //var script = CSharpScript.Create<int>("X*Y", globalsType: typeof(Globals));
            //script.Compile();
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine((script.RunAsync(new Globals { X = i, Y = i })).Result);
            //}

            // 8 Run a C# snippet and inspect defined script variables
            //var state = CSharpScript.RunAsync<int>("int answer = 42;").Result;
            //foreach (var variable in state.Variables)
            //    Console.WriteLine($"{variable.Name} = {variable.Value} of type {variable.Type}");
            // 
            // 9 Create a delegate to a script
            //var script = CSharpScript.Create<int>("X*Y", globalsType: typeof(Globals));
            //ScriptRunner<int> runner = script.CreateDelegate();
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine( runner(new Globals { X = i, Y = i }).Result);
            //}


            // 10 Chain code snippets to form a script
            //var script = CSharpScript.
            //    Create<int>("int x = 1;").
            //    ContinueWith("int y = 2;").
            //    ContinueWith("x + y");

            //Console.WriteLine(script.RunAsync().Result);



            // var  script = @"int Add(int x, int y) { return x+y; } Add(1, 4)";
            //note: we block here, because we are in Main method, normally we could await as scripting APIs are async

            // var result = CSharpScript.EvaluateAsync<int>(script).Result;
            ////result is now 5
            //Console.WriteLine(result);
        }
    }
    public class Globals
    {
        //public string myKey;
        //public object myValue;
        public dynamic data;
    }

    public class MetadataPropertyEntity
    {
        [MaxLength(50)]
        public string Tenant { get; set; }

        [MaxLength(50)]
        public string TypeName { get; set; }

        [MaxLength(50)]
        public string PropertyName { get; set; }

        [MaxLength(250)]
        public string DisplayName { get; set; }

        public int PropertyKind { get; set; }

        public int PropertyType { get; set; }
        /// <summary>
        /// 用于权限
        /// </summary>
        public bool Permissions { get; set; }
        /// <summary>
        /// 自定义字段Code
        /// </summary>
        public string Code { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        [MaxLength(50)]
        public string DataSource { get; set; }

        public string Remark { get; set; }

        public string Settings { get; set; }

        public string Dependency { get; set; }

        [MaxLength(50)]
        public string PropertyId { get; set; }
    }


}
