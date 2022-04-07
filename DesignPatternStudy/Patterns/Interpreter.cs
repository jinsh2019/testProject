using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DesignPatternStudy.Patterns
{
    // 解释是抽象的实例化出： Or,And
    public interface Expression
    {
        public bool interpret(String context);
    }

    // 解释输出：
    // 1. 存储数据；
    // 2. 终端的输出表达
    public class TerminalExpression : Expression
    {

        private String data;

        public TerminalExpression(String data)
        {
            this.data = data;
        }


        public bool interpret(String context)
        {
            if (context.Contains(data))
            {
                return true;
            }
            return false;
        }
    }

    public class OrExpression : Expression
    {
        private Expression expr1 = null;
        private Expression expr2 = null;

        public OrExpression(Expression expr1, Expression expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }

        public bool interpret(String context)
        {
            return expr1.interpret(context) || expr2.interpret(context);
        }
    }

    public class AndExpression : Expression
    {

        private Expression expr1 = null;
        private Expression expr2 = null;

        public AndExpression(Expression expr1, Expression expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }

        public bool interpret(String context)
        {
            return expr1.interpret(context) && expr2.interpret(context);
        }
    }

}
