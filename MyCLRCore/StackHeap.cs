using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCLRCore
{
    internal class StackHeap
    {
        public static void show()
        {
            // 创建对象
            // 1. 分配内存
            // 2. 把对象传入到构造函数
            // 3. 构造函数使用当前实例
            // 4. 返回
            //{

            //}
            {
                string student = "大山";
                string student2 = student;

                Console.WriteLine(student);
                Console.WriteLine(student2);

                student2 = "APP";
                Console.WriteLine(student); // "大山"
                Console.WriteLine(student2);// "APP"

            }
            {
                string student = "大山";
                string student2 = "APP";
                student2 = "大山";
                // true
                Console.WriteLine(Object.ReferenceEquals(student, student2));
                // false
                string student3 = string.Format("大{0}", "山");
                Console.WriteLine(Object.ReferenceEquals(student, student3));
                // true
                string student4 = "大" + "山";
                Console.WriteLine(Object.ReferenceEquals(student, student4));

                string halfStudent = "山";
                string student5 = "大" + halfStudent;
                Console.WriteLine(Object.ReferenceEquals(student, student5));
            }
        }
    }

    public class ReferecePoint
    {

        public int x;
        public ReferecePoint(int x)
        {
            this.x = x; // # this 代表构造函数中已经存在
        }
    }

    // 结构类型
    public struct ValuePoint // : System.ValueType 结构不能有父类，因为隐式继承了ValueType
    {
        public int x;
        public ValuePoint(int x)
        {
            this.x = x;
            this.Text = "1234";
        }
        // 引用类型，存储在堆里面
        public string Text; //  在堆中还是栈中？
    }
}
