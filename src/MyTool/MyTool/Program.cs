using System;
using System.ComponentModel;

namespace MyTool
{
    class Program
    {
        static void Main(string[] args)
        {


        }
    }

    public enum TypeName
    {
        [Description("人事资料")]
        employee,
        [Description("组织资料")]
        orgnization
    }


}
