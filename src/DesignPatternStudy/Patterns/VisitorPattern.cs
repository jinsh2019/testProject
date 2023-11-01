using System.Reflection;
using System;

namespace DesignPatternStudy.Patterns
{
    // 接口定义
    public interface IVisitor
    {
        void VisitCPU(CPU visitor);
        void visitDisk(Disk disk);
    }
    // 补丁包
    public class UpdateVisitor : IVisitor
    {
        public void VisitCPU(CPU cpu)
        {
            cpu.command += "; remember 1+1=2";  // hardware  cpu的command属性
        }

        public void visitDisk(Disk disk)
        {
            disk.command += "; remember 1+1=2"; // hardware disk的command属性
        }
    }

    // 主类
    public class EggRobbot
    {
        private Disk disk;
        private CPU cpu;

        public EggRobbot()
        {
            disk = new Disk("记住 1+1=1");
            cpu = new CPU("1+1=1");
        }

        public void calc()
        {
            disk.run();                         // cpu和disk都执run方法，run方法打印了暴露的command属性
            cpu.run();
        }

        public void accept(IVisitor visitor)
        {
            cpu.accept(visitor);                // cpu和disk接收访问者
            disk.accept(visitor);
        }
    }

    public abstract class Hardware
    {
        public string command { get; set; } // 暴露了command
        public Hardware(string command)         // 构造函数，用以初始化
        {
            this.command = command;
        }
        public void run()                       // 硬件run方法
        {
            Console.WriteLine(command);
        }

        public abstract void accept(IVisitor visitor);  // 接受访问真， 抽象函数，需要重写
    }

    // 硬件具有一个"接收"插件访问者的方法，
    // 将当前硬件组为参数，用以调用访问者的具体业务逻辑
    public class CPU : Hardware
    {
        public CPU(string command) : base(command) { }

        public override void accept(IVisitor visitor)
        {
            visitor.VisitCPU(this);         // visitor 的实例执行【VisitCPU】方法
        }
    }

    public class Disk : Hardware
    {
        public Disk(string command) : base(command) { }
        public override void accept(IVisitor visitor)
        {
            visitor.visitDisk(this);
        }
    }
}