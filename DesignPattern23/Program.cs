using DesignPattern23.Visitor;
using System;
using DesignPattern23.Builder;
namespace DesignPattern23
{
    class Program
    {
        static void Main(string[] args)
        {
            // 双重分发  visitor/Accept
            ShapeVisitor visitor = new CustomVisitor();
            AppStructure app = new AppStructure(visitor);

            Shape shape = new Rectangle();
            shape.Draw(); //执行自己的操作
            app.Process(shape); //执行新的操作

            shape = new Circle();
            shape.Draw(); //执行自己的操作
            app.Process(shape); //执行新的操作


            shape = new Line();
            shape.Draw(); //执行自己的操作
            app.Process(shape); //执行新的操作


            new Computer().Builder("intel", "sumsing").setDisplay("changhong").setKeyboard("蜘蛛").setUsbCount(5);
            Console.ReadLine();

        }
    }
}
