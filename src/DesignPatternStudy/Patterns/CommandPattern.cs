using System;

namespace DesignPatternStudy.Patterns
{
    public interface Command
    {
        public void execute();
    }

    public class PrintService
    {
        public void print(string text)
        {
            Console.WriteLine(text);
        }
    }

    public class SaveButton
    {
        private Command command;

        public void bindCommand(Command command)
        {
            command.execute();
        }

        public void doPrint()
        {
            if (command == null)
                throw new Exception();
            command.execute();
        }
    }
}