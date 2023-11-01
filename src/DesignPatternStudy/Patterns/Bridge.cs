using static System.Console;

namespace DesignPatternStudy.Patterns.Bridge
{
    public interface IDraw
    {
        public void drawCircle(int radius, int x, int y);
    }
    // 红色圆
    public class RedCircle : IDraw
    {
        public void drawCircle(int radius, int x, int y)
        {
            WriteLine("Drawing Circle[ color: red, radius: "
               + radius + ", x: " + x + ", " + y + "]");
        }
    }
    // 绿色圆
    public class GreenCircle : IDraw
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
        protected IDraw drawAPI;
        protected Shape(IDraw drawAPI)
        {
            this.drawAPI = drawAPI;
        }
        public abstract void draw();
    }

    // bridge 红/绿圆 + 大小的实现
    public class Circle : Shape
    {
        private int x, y, radius;

        public Circle(int x, int y, int radius, IDraw drawAPI) : base(drawAPI)
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
