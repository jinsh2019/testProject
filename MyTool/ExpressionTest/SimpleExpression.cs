using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionTest
{
    public class SimpleExpression
    {


    }

    [Description("唯一标识")]
    public class PersonModel
    {
        [Description("唯一标识")]
        public string ID { get; set; }

        [Description("名称")] 
        public string Name { get; set; }

        [Description("值")] 
        public double Value { get; set; }

        [Description("年齡")] 
        public double Age { get; set; }

        [Description("收入")]
        public double InCome { get; set; }

        [Description("支出")]
        public double Pay { get; set; }


    }


}

