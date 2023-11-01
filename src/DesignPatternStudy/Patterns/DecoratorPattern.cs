namespace DesignPatternStudy.Patterns
{
    interface IRobot
    {
        void doSomething();
    }

    class FirstRobot : IRobot
    {
        public void doSomething()
        {
            System.Console.WriteLine("Speak");
            System.Console.WriteLine("Sing");
        }
    }

    // can set a
    class RobertDecorator : IRobot
    {
        private IRobot robot;
        public RobertDecorator(IRobot robot)
        {
            this.robot = robot;
        }

        public void doSomething()
        {
           robot.doSomething();
        }

        public void doMoreThing() {
            System.Console.WriteLine("Dance");
            System.Console.WriteLine("Clean");
        }
    }
}