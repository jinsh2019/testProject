using DesignPatternStudy.Patterns;
using DesignPatternStudy.Patterns.AbstractFactory;
using DesignPatternStudy.Patterns.State;
using System;
using static System.Console;
namespace DesignPatternStudy
{
    class Program
    {

        private static AbstractLogger getChainOfLoggers()
        {
            AbstractLogger errorLogger = new ErrorLogger(AbstractLogger.ERROR);
            AbstractLogger fileLogger = new FileLogger(AbstractLogger.DEBUG);
            AbstractLogger consoleLogger = new ConsoleLogger(AbstractLogger.INFO);

            errorLogger.setNextLogger(fileLogger);
            fileLogger.setNextLogger(consoleLogger);

            return errorLogger;
        }

        //规则：Robert 和 John 是男性
        public static Expression getMaleExpression()
        {
            Expression robert = new TerminalExpression("Robert");
            Expression john = new TerminalExpression("John");
            return new OrExpression(robert, john);
        }

        //规则：Julie 是一个已婚的女性
        public static Expression getMarriedWomanExpression()
        {
            Expression julie = new TerminalExpression("Julie");
            Expression married = new TerminalExpression("Married");
            return new AndExpression(julie, married);
        }

        static void Main(string[] args)
        {
            { // Strategy Pattern  面向接口编程
                Patterns.Strategy.Context context = new Patterns.Strategy.Context(new Patterns.Strategy.OperationAdd());
                WriteLine("10 + 5 = " + context.executeStrategy(10, 5));

                context = new Patterns.Strategy.Context(new Patterns.Strategy.OperationSubtract());
                WriteLine("10 - 5 = " + context.executeStrategy(10, 5));
            }
            {  // Visitor Pattern 
                // 1. 初始化一个集合体,集合体的接受方法传入访问者实例
                // 2. 将访问者的实例传入集合体的接受函数中
                // 3. 接收后立即执行相关方法
                IComputerPart computer = new Computer(); // Computer 数据 接收操作，ComputerPartDisplayVisitor 操作 具体执行visit： 数据与操作的分离
                computer.accept(new ComputerPartDisplayVisitor()); // 可以传入 1.电脑部件访问者实现1； 2. 电脑部件访问者实现2
            }
            { // Bridge Pattern
              // 将具体实现传入，执行时，根据具体的类执行
                var redCircle = new Patterns.Bridge.RedCircle();
                Patterns.Bridge.Shape redCircleInst = new Patterns.Bridge.Circle(100, 100, 10, redCircle);
                redCircleInst.draw();

                var greenCircle = new Patterns.Bridge.GreenCircle();
                Patterns.Bridge.Shape greenCircleInst = new Patterns.Bridge.Circle(100, 100, 10, greenCircle);
                greenCircleInst.draw();

            }
            { // ChainofResponsibility
                AbstractLogger loggerChain = getChainOfLoggers(); // 设置链表

                loggerChain.logMessage(AbstractLogger.INFO, "This is an information.");// stardard console

                loggerChain.logMessage(AbstractLogger.DEBUG, "This is a debug level information."); // console + file

                loggerChain.logMessage(AbstractLogger.ERROR, "This is an error information."); // error console + file + stardard console
            }
            { // Interpreter
                Expression isMale = getMaleExpression();
                Expression isMarriedWoman = getMarriedWomanExpression();

                WriteLine("John is male? " + isMale.interpret("John"));
                WriteLine("Julie is a married women? " + isMarriedWoman.interpret("Married Julie"));
            }
            { // Iterator
                NameRepository namesRepository = new NameRepository();

                for (Iterator iter = namesRepository.getIterator(); iter.hasNext();)
                {
                    String name = (String)iter.next();
                    WriteLine("Name : " + name);
                }
            }
            { // Composite

                Employee CEO = new Employee("John", "CEO", 30000);

                Employee headSales = new Employee("Robert", "Head Sales", 20000);

                Employee headMarketing = new Employee("Michel", "Head Marketing", 20000);

                Employee clerk1 = new Employee("Laura", "Marketing", 10000);
                Employee clerk2 = new Employee("Bob", "Marketing", 10000);

                Employee salesExecutive1 = new Employee("Richard", "Sales", 10000);
                Employee salesExecutive2 = new Employee("Rob", "Sales", 10000);

                CEO.add(headSales);
                CEO.add(headMarketing);

                headSales.add(salesExecutive1);
                headSales.add(salesExecutive2);

                headMarketing.add(clerk1);
                headMarketing.add(clerk2);

                //打印该组织的所有员工
                WriteLine(CEO);
                foreach (var headEmployee in CEO.getSubordinates())
                {
                    WriteLine(headEmployee);
                    foreach (var employee in headEmployee.getSubordinates())
                    {
                        WriteLine(employee);
                    }
                }
            }
            {// Template

                Game game = new Cricket();
                game.play();
                WriteLine();
                game = new Football();
                game.play();
            }
            {
                // NullObject
                AbstractCustomer customer1 = CustomerFactory.getCustomer("Rob");
                AbstractCustomer customer2 = CustomerFactory.getCustomer("Bob");
                AbstractCustomer customer3 = CustomerFactory.getCustomer("Julie");
                AbstractCustomer customer4 = CustomerFactory.getCustomer("Laura");

                WriteLine("Customers");
                WriteLine(customer1.getName());
                WriteLine(customer2.getName());
                WriteLine(customer3.getName());
                WriteLine(customer4.getName());
            }
            {
                // State 1.做事情；2.设置状态
                /// 1. 初始化不同种类的状态
                /// 2. 做不同的事情，同时设置状态机
                /// 扩展： 根据状态机的不同，可以进行流动
                Patterns.State.Context context = new Patterns.State.Context(); // 初始化状态机

                StartState startState = new StartState();
                startState.doAction(context); // 设置当前状态

                WriteLine(context.getState().ToString()); // 获取当前状态

                StopState stopState = new StopState();
                stopState.doAction(context); // 设置当前状态

                WriteLine(context.getState().ToString()); // 获取当前状态
            }

            {// 发布者/订阅者
                // 观察者去订阅主题
                // 当主题进行变更时，同时所有的主题
                Subject subject = new Subject();

                new HexaObserver(subject);
                new OctalObserver(subject);
                new BinaryObserver(subject);

                WriteLine("First state change: 15");
                subject.setState(15);
                WriteLine("Second state change: 10");
                subject.setState(10);
            }
            {
                Builder builder = new MacBookBuilder();
                Director pcDirector = new Director(builder);
                pcDirector.construct("英特尔主板", "Retina显示器");

                Pad pad = builder.build();
                WriteLine(pad.ToString());
            }
            { // Singleton

                var singleton = Singleton.getInstance();
            }
            { // Simple factory
                Patterns.Factory.ShapeFactory shapeFactory = new Patterns.Factory.ShapeFactory();

                //获取 Circle 的对象，并调用它的 draw 方法
                Patterns.Factory.Shape shape1 = shapeFactory.getShape("CIRCLE");

                //调用 Circle 的 draw 方法
                shape1.draw();

                //获取 Rectangle 的对象，并调用它的 draw 方法
                Patterns.Factory.Shape shape2 = shapeFactory.getShape("RECTANGLE");

                //调用 Rectangle 的 draw 方法
                shape2.draw();

                //获取 Square 的对象，并调用它的 draw 方法
                Patterns.Factory.Shape shape3 = shapeFactory.getShape("SQUARE");

                //调用 Square 的 draw 方法
                shape3.draw();
            }
            {
                //abstract factory

                //获取形状工厂
                AbstractFactory shapeFactory = FactoryProducer.getFactory("SHAPE");

                //获取形状为 Circle 的对象
                Patterns.AbstractFactory.Shape shape1 = shapeFactory.getShape("CIRCLE");

                //调用 Circle 的 draw 方法
                shape1.draw();

                //获取形状为 Rectangle 的对象
                Patterns.AbstractFactory.Shape shape2 = shapeFactory.getShape("RECTANGLE");

                //调用 Rectangle 的 draw 方法
                shape2.draw();

                //获取形状为 Square 的对象
                Patterns.AbstractFactory.Shape shape3 = shapeFactory.getShape("SQUARE");

                //调用 Square 的 draw 方法
                shape3.draw();

                //获取颜色工厂
                AbstractFactory colorFactory = FactoryProducer.getFactory("COLOR");

                //获取颜色为 Red 的对象
                Color color1 = colorFactory.getColor("RED");

                //调用 Red 的 fill 方法
                color1.fill();

                //获取颜色为 Green 的对象
                Color color2 = colorFactory.getColor("Green");

                //调用 Green 的 fill 方法
                color2.fill();

                //获取颜色为 Blue 的对象
                Color color3 = colorFactory.getColor("BLUE");

                //调用 Blue 的 fill 方法
                color3.fill();

            }
            {
                // Adapter
                AudioPlayer audioPlayer = new AudioPlayer();

                audioPlayer.play("mp3", "beyond the horizon.mp3");
                audioPlayer.play("mp4", "alone.mp4");
                audioPlayer.play("vlc", "far far away.vlc");
                audioPlayer.play("avi", "mind me.avi");
            }
            {
                // 深浅拷贝Demo
                DemoClass a = new DemoClass();
                a.i = 10;
                a.iArr = new int[] { 8, 9, 10 };
                DemoClass b = a.Clone1();
                DemoClass c = a.Clone2();

                // 更改 a 对象的iArr[0], 导致 b 对象的iArr[0] 也发生了变化 而 c不会变化
                a.iArr[0] = 88;

                WriteLine("MemberwiseClone");
                WriteLine(b.i);

                foreach (var item in b.iArr)
                {
                    WriteLine(item);
                }

                WriteLine("Clone2");
                WriteLine(c.i);
                foreach (var item in c.iArr)
                {
                    WriteLine(item);
                }
            }
            {
                //  Prototype
                // Create two instances and clone each
                ConcretePrototype1 p1 = new ConcretePrototype1("I");
                ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
                WriteLine("Cloned: {0}", c1.Id);

                ConcretePrototype2 p2 = new ConcretePrototype2("II");
                ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
                WriteLine("Cloned: {0}", c2.Id);
            }
        }

    }
}
