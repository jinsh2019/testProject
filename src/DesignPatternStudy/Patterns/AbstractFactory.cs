using System;
using static System.Console;
namespace DesignPatternStudy.Patterns.AbstractFactory
{
    // https://www.runoob.com/design-pattern/abstract-factory-pattern.html
    // 抽象工厂
    #region Shape serial 画形状
    public interface Shape
    {
        void draw();
    }

    // 具体形状
    // 长方形，
    public class Rectangle : Shape
    {
        public void draw()
        {
            WriteLine("Inside Rectangle::draw() method.");
        }
    }
    // 正方形
    public class Square : Shape
    {
        public void draw()
        {
            WriteLine("Inside Square::draw() method.");
        }
    }
    // 圆形
    public class Circle : Shape
    {
        public void draw()
        {
            WriteLine("Inside Circle::draw() method.");
        }
    }

    #endregion

    #region Color Serial 填充颜色
    public interface Color
    {
        void fill();
    }
    // 具体颜色
    // 红色
    public class Red : Color
    {
        public void fill()
        {
            WriteLine("Inside Red::fill() method.");
        }
    }

    public class Green : Color
    {

        public void fill()
        {
            WriteLine("Inside Green::fill() method.");
        }
    }

    public class Blue : Color
    {

        public void fill()
        {
            WriteLine("Inside Blue::fill() method.");
        }
    }
    #endregion

    #region AbstractFactory
    // 抽象工厂封装:两个动作::勾画形状,填充颜色;
    // 返回操作接口
    public abstract class AbstractFactory
    {
        // 设置颜色
        public abstract Color SetColor(String color);
        // 设置形状
        public abstract Shape SetShape(String shape);
    }
    // 具体工厂shape
    public class ShapeFactory : AbstractFactory
    {
        public override Shape SetShape(String shapeType)
        {
            if (shapeType == null)
            {
                return null;
            }
            if (shapeType.Equals("CIRCLE", StringComparison.OrdinalIgnoreCase))
            {
                return new Circle();
            }
            else if (shapeType.Equals("RECTANGLE", StringComparison.OrdinalIgnoreCase))
            {
                return new Rectangle();
            }
            else if (shapeType.Equals("SQUARE", StringComparison.OrdinalIgnoreCase))
            {
                return new Square();
            }
            return null;
        }

        public override Color SetColor(String color)
        {
            return null;
        }
    }
    /// <summary>
    /// 具体工厂color
    /// </summary>
    public class ColorFactory : AbstractFactory
    {
        // 重新1
        public override Shape SetShape(String shapeType)
        {
            return null;
        }
        // 重写2
        public override Color SetColor(String color)
        {
            if (color == null)
            {
                return null;
            }
            if (color.Equals("RED", StringComparison.OrdinalIgnoreCase))
            {
                return new Red();
            }
            else if (color.Equals("GREEN", StringComparison.OrdinalIgnoreCase))
            {
                return new Green();
            }
            else if (color.Equals("BLUE", StringComparison.OrdinalIgnoreCase))
            {
                return new Blue();
            }
            return null;
        }
    }
    /// <summary>
    ///  封装工厂的生产者
    /// </summary>
    public class FactoryProducer
    {
        public static AbstractFactory getFactory(String choice)
        {
            if (choice.Equals("SHAPE", StringComparison.OrdinalIgnoreCase))
            {
                return new ShapeFactory();
            }
            else if (choice.Equals("COLOR", StringComparison.OrdinalIgnoreCase))
            {
                return new ColorFactory();
            }
            return null;
        }
    }

    #endregion

}
