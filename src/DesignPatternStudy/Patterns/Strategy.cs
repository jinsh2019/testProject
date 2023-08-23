using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 策略模式
/// https://www.runoob.com/design-pattern/strategy-pattern.html
/// </summary>
namespace DesignPatternStudy.Patterns.Strategy  
{
    // Base
    public interface IStrategy
    {
        public int doOperation(int num1, int num2);
    }

    #region Imp Strategy
    public class OperationAdd : IStrategy
    {
        public int doOperation(int num1, int num2)
        {
            return num1 + num2;
        }
    }

    public class OperationSubtract : IStrategy
    {
        public int doOperation(int num1, int num2)
        {
            return num1 - num2;
        }
    }
    #endregion

    public class Context
    {
        private IStrategy strategy;

        public Context(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public int executeStrategy(int num1, int num2)
        {
            return strategy.doOperation(num1, num2);
        }
    }

}
