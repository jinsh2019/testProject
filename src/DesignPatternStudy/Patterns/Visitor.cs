using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

/// <summary>
/// 访问者模式
/// https://www.runoob.com/design-pattern/visitor-pattern.html
/// 
/// 适用于拥有不同的构造函数的情况
/// </summary>
namespace DesignPatternStudy
{
    // base 
    public interface IComputerPart
    {
        public void accept(IComputerPartVisitor computerPartVisitor);
    }

    #region 具体实现
    public class Keyboard : IComputerPart
    {

        public void accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }

    public class Monitor : IComputerPart
    {
        public void accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }

    public class Mouse : IComputerPart
    {
        public void accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }
    #endregion

    // impl
    public class Computer : IComputerPart
    {

        IComputerPart[] parts;

        public Computer()
        {
            parts = new IComputerPart[] { new Mouse(), new Keyboard(), new Monitor() };
        }

        public void accept(IComputerPartVisitor computerPartVisitor)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].accept(computerPartVisitor);
            }
            computerPartVisitor.visit(this);
        }
    }

    // Visitor
    public interface IComputerPartVisitor
    {
        // 总
        public void visit(Computer computer);
        // part
        public void visit(Mouse mouse);
        // part
        public void visit(Keyboard keyboard);
        // part
        public void visit(Monitor monitor);
    }
    // Impl Visitor
    public class ComputerPartDisplayVisitor : IComputerPartVisitor
    {
        public void visit(Computer computer)
        {
            WriteLine("Displaying Computer.");
        }

        public void visit(Mouse mouse)
        {
            WriteLine("Displaying Mouse.");
        }
        public void visit(Keyboard keyboard)
        {
            WriteLine("Displaying Keyboard.");
        }

        public void visit(Monitor monitor)
        {
            WriteLine("Displaying Monitor.");
        }
    }
}
