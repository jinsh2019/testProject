namespace DesignPatternStudy.Patterns
{
    interface Robot
    {
        void doSomething();
    }

    class FirstRobot : Robot
    {
        public void doSomething()
        {
            System.Console.WriteLine("Speak");
            System.Console.WriteLine("Sing");
        }
    }

    // can set a
    class RobertDecorator : Robot
    {
        private Robot robot;
        public RobertDecorator(Robot robot)
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