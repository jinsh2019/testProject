using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class AssemblyMS
    {

        [TestMethod]
        public void AssemblyTest()
        {
            //// Gets the mscorlib assembly in which the object is defined.
            //Assembly a = typeof(object).Module.Assembly;

            // Loads an assembly using its file name.
            Assembly a = Assembly.LoadFrom("GaiaWorks.HumanResource.WebApi.dll");
            // Gets the type names from the assembly.
            var inst = a.GetType();
            Type[] types2 = a.GetTypes();
            foreach (Type t in types2)
            {
                Console.WriteLine(t.FullName);
            }
        }
        [TestMethod]
        public void test(){ 
           string[] name1 = new string[]{"张三","ids"};
           var basedataFilter = "id,ids,dd";
           //Console.WriteLine(basedataFilter.ToArray().Contains("id"));
        }
    }
}