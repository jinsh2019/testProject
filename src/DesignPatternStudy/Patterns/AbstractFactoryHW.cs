using System;

namespace DesignPatternStudy.Patterns
{
    public interface IProduct
    {
        string GetName();
    }
    #region 系列产品

    public class P30Phone : IProduct
    {
        public string GetName()
        {
            return "P30 手机";
        }
    }

    public class P40Phone : IProduct
    {
        public string GetName()
        {
            return "P40 手机";
        }
    }

    public class MateBook14 : IProduct
    {
        public string GetName()
        {
            return "MateBook 14 电脑";
        }
    }

    public class MateBook15 : IProduct
    {
        public string GetName()
        {
            return "MateBook 15 电脑";
        }
    }
    #endregion
    public abstract class AbstractFactoryHW
    {
        public abstract IProduct CreatePhone(string type);
        public abstract IProduct CreateComputer(string type);

    }

    public class HuaWeiFactory : AbstractFactoryHW
    {
        public override IProduct CreateComputer(string type)
        {
            if (type.Equals("MateBook14", System.StringComparison.OrdinalIgnoreCase))
            {
                return new MateBook14();
            }
            else if (type.Equals("MateBook15", StringComparison.OrdinalIgnoreCase))
            {
                return new MateBook15();
            }

            return null;
        }

        public override IProduct CreatePhone(string type)
        {
            if (type.Equals("P40", System.StringComparison.OrdinalIgnoreCase))
            {
                return new P40Phone();
            }
            else if (type.Equals("P30", StringComparison.OrdinalIgnoreCase))
            {
                return new P30Phone();
            }

            return null;
        }
    }
}