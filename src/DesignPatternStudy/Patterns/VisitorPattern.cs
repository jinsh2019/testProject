using System.Reflection;

namespace DesignPatternStudy.Patterns
{
    // 接口定义
    public interface IVisitor
    {
        void VisitCPU(CPU visitor);
        void visitDisk(HardDisk disk);
    }
    // 更新包=> 可以进行扩展
    public class UpdateVisitor : IVisitor
    {
        public void VisitCPU(CPU cpu)
        {
            cpu.command += "; remember 1+1=2"; // 具体的业务逻辑
        }

        public void visitDisk(HardDisk disk)
        {
            disk.command += "; remember 1+1=2"; // 具体的业务逻辑
        }
    }

    // 主类
    public class EggRobbot
    {
        private HardDisk disk;
        private CPU cpu;

        public EggRobbot()
        {
            disk = new HardDisk("记住 1+1=1");
            cpu = new CPU("1+1=1");
        }

        public void calc()
        {
            disk.run();
            cpu.run();
        }

        public void accept(IVisitor visitor)
        {
            cpu.accept(visitor);
            disk.accept(visitor);
        }
    }

    public abstract class Hardware
    {
        public string command;
        public Hardware(string command)
        {
            this.command = command;
        }
        public void run()
        {
            System.Console.WriteLine(command);
        }

        public abstract void accept(IVisitor visitor);
    }
    // 硬件具有一个"接收"插件访问者的方法，
    // 将当前硬件组为参数，用以调用访问者的具体业务逻辑
    public class CPU : Hardware
    {
        public CPU(string command) : base(command) { }

        public override void accept(IVisitor visitor)
        {
            visitor.VisitCPU(this); // visitor 的实例执行VisitCPU 方法
        }
    }

    public class HardDisk : Hardware
    {
        public HardDisk(string command) : base(command) { }
        public override void accept(IVisitor visitor)
        {
            visitor.visitDisk(this);
        }
    }
}