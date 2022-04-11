using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternStudy.Patterns
{
    // NullObject 提供默认行为
    public abstract class AbstractCustomer
    {
        protected String name;
        public abstract bool isNil();
        public abstract String getName();
    }

    public class RealCustomer : AbstractCustomer
    {
        public RealCustomer(String name)
        {
            this.name = name;
        }

        public override String getName()
        {
            return name;
        }

        public override bool isNil()
        {
            return false;
        }
    }

    public class NullCustomer : AbstractCustomer
    {

        public override String getName()
        {
            return "Not Available in Customer Database";
        }

        public override bool isNil()
        {
            return true;
        }
    }

    public class CustomerFactory
    {

        public static readonly String[] names = { "Rob", "Joe", "Julie" };

        public static AbstractCustomer getCustomer(String name)
        {
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Equals(name))
                {
                    return new RealCustomer(name);
                }
            }
            return new NullCustomer();
        }
    }
}
