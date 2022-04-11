﻿using System;

/// <summary>
/// https://www.cnblogs.com/PatrickLiu/p/8135083.html 一般用法
/// https://blog.csdn.net/u012124438/article/details/70537203 高级用法 -- 可为访问者分类
/// </summary>
namespace DesignPattern23.Visitor
{
    /// <summary>
    /// 抽象图形定义---相当于“抽象节点角色”Element
    /// </summary>
    public abstract class Shape 
    {
        public abstract void Accept(ShapeVisitor visitor);
        public abstract void Draw();
    }

    /// <summary>
    /// 抽象访问者 Visitor
    /// </summary>
    public abstract class ShapeVisitor
    {
        public abstract void Visit(Rectangle shape);

        public abstract void Visit(Circle shape);

        public abstract void Visit(Line shape);


        //这里有一点要说：Visit方法的参数可以写成Shape吗？就是这样 Visit(Shape shape)，当然可以，但是ShapeVisitor子类Visit方法就需要判断当前的Shape是什么类型，是Rectangle类型，是Circle类型，或者是Line类型。
    }

    /// <summary>
    /// 具体访问者  ConcreteVisitor
    /// </summary>
    public sealed class CustomVisitor : ShapeVisitor
    {
        //针对Rectangle对象
        public override void Visit(Rectangle shape)
        {
            Console.WriteLine("针对Rectangle新的操作！");
        }
        //针对Circle对象
        public override void Visit(Circle shape)
        {
            Console.WriteLine("针对Circle新的操作！");
        }
        //针对Line对象
        public override void Visit(Line shape)
        {
            Console.WriteLine("针对Line新的操作！");
        }
    }

    //矩形----相当于“具体节点角色” ConcreteElement
    public sealed class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("矩形我已经画好！");
        }

        public override void Accept(ShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    //圆形---相当于“具体节点角色”ConcreteElement
    public sealed class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("圆形我已经画好！");
        }

        public override void Accept(ShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    //直线---相当于“具体节点角色” ConcreteElement
    public sealed class Line : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("直线我已经画好！");
        } 

        public override void Accept(ShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    /// <summary>
    /// 结构对象角色 ObjectStructure
    /// </summary>
    internal class AppStructure
    {
        private ShapeVisitor _visitor;

        public AppStructure(ShapeVisitor visitor)
        {
            this._visitor = visitor;
        }

        public void Process(Shape shape)
        {
            shape.Accept(_visitor);
        }

    }
}
