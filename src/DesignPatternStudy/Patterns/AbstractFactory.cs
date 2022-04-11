using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
namespace DesignPatternStudy.Patterns.AbstractFactory
{
    #region Shape serial
    public interface Shape
    {
        void draw();
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

    #endregion

    #region Color Serial
    public interface Color
    {
        void fill();
    }

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


    public abstract class AbstractFactory
    {
        public abstract Color getColor(String color);
        public abstract Shape getShape(String shape);
    }

    public class ShapeFactory : AbstractFactory
    {
        public override Shape getShape(String shapeType)
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

        public override Color getColor(String color)
        {
            return null;
        }
    }

    public class ColorFactory : AbstractFactory
    {

        public override Shape getShape(String shapeType)
        {
            return null;
        }

        public override Color getColor(String color)
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
