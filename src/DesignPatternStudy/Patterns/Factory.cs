using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
namespace DesignPatternStudy.Patterns.Factory
{
    // https://www.runoob.com/design-pattern/factory-pattern.html
    public interface Shape
    {
        public void draw(); 
    }

    public class Rectangle : Shape
    {
        public void draw()
        {
            WriteLine("Inside Rectangle::draw() method.");
        }
    }
    public class Square : Shape
    {
        public void draw()
        {
            WriteLine("Inside Square::draw() method.");
        }
    }

    public class Circle : Shape
    {

        public void draw()
        {
            WriteLine("Inside Circle::draw() method.");
        }
    }

    public class ShapeFactory
    {
        //使用 getShape 方法获取形状类型的对象
        public Shape getShape(String shapeType)
        {
            if (shapeType == null)
            {
                return null;
            }
            if (shapeType.Contains("CIRCLE", StringComparison.OrdinalIgnoreCase))
            {
                return new Circle();
            }
            else if (shapeType.Contains("RECTANGLE", StringComparison.OrdinalIgnoreCase))
            {
                return new Rectangle();
            }
            else if (shapeType.Contains("SQUARE", StringComparison.OrdinalIgnoreCase))
            {
                return new Square();
            }
            return null;
        }
    }
}
