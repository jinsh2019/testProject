using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DesignPatternStudy.Patterns
{


    [Serializable]
    class DemoClass
    {
        public int i = 0;
        public int[] iArr = { 1, 2, 3 };

        public DemoClass Clone1() //浅CLONE
        {
            return this.MemberwiseClone() as DemoClass;
        }

        public DemoClass Clone2() //深clone
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as DemoClass;
        }
    }
}
