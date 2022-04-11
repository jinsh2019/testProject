using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DesignPatternStudy.Patterns.Bridge
{
    public interface DrawAPI
    {
        public void drawCircle(int radius, int x, int y);
    }
    // 具体画法1
    public class RedCircle : DrawAPI
    {
        public void drawCircle(int radius, int x, int y)
        {
            WriteLine("Drawing Circle[ color: red, radius: "
               + radius + ", x: " + x + ", " + y + "]");
        }
    }
    // 具体画法2
    public class GreenCircle : DrawAPI
    {
        public void drawCircle(int radius, int x, int y)
        {
            WriteLine("Drawing Circle[ color: green, radius: "
               + radius + ", x: " + x + ", " + y + "]");
        }
    }
    // 抽象的画
    public abstract class Shape
    {
        protected DrawAPI drawAPI;
        protected Shape(DrawAPI drawAPI)
        {
            this.drawAPI = drawAPI;
        }
        public abstract void draw();
    }

    // bridge 红/绿圆 + 大小的实现
    public class Circle : Shape
    {
        private int x, y, radius;

        public Circle(int x, int y, int radius, DrawAPI drawAPI) : base(drawAPI)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public override void draw()
        {
            drawAPI.drawCircle(radius, x, y);
        }
    }
}
