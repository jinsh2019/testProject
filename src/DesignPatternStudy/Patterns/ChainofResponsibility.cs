using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DesignPatternStudy.Patterns
{
    public abstract class AbstractLogger
    {
        public static int INFO = 1;
        public static int DEBUG = 2;
        public static int ERROR = 3;

        protected int level;

        //责任链中的下一个元素
        protected AbstractLogger nextLogger;

        public void setNextLogger(AbstractLogger nextLogger)
        {
            this.nextLogger = nextLogger;
        }

        public void logMessage(int level, String message)
        {
            if (this.level <= level)
            {
                write(message);
            }
            if (nextLogger != null)
            {
                nextLogger.logMessage(level, message);
            }
        }

        abstract protected void write(String message);

    }

    public class ConsoleLogger : AbstractLogger
    {
        public ConsoleLogger(int level)
        {
            this.level = level;
        }

        protected override void write(String message)
        {
            WriteLine("Standard Console::Logger: " + message);
        }
    }

    public class ErrorLogger : AbstractLogger
    {
        public ErrorLogger(int level)
        {
            this.level = level;
        }


        protected override void write(String message)
        {
            WriteLine("Error Console::Logger: " + message);
        }
    }

    public class FileLogger : AbstractLogger
    {
        public FileLogger(int level)
        {
            this.level = level;
        }

        protected override void write(String message)
        {
            WriteLine("File::Logger: " + message);
        }
    }

}
