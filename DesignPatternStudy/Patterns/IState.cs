using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DesignPatternStudy.Patterns.State
{

    public interface IState
    {
        public void doAction(Context context);
    }

    public class StartState : IState
    {
        public void doAction(Context context)
        {
            WriteLine("Player is in start state");
            context.setState(this);
        }

        public override string ToString()
        {
            return "Start State";
        }
    }

    public class StopState : IState
    {
        public void doAction(Context context)
        {
            WriteLine("Player is in stop state");
            context.setState(this);
        }

        public override string ToString()
        {
            return "Stop State";
        }
    }

    public class Context
    {
        private IState state;

        public Context()
        {
            state = null;
        }

        public void setState(IState state)
        {
            this.state = state;
        }

        public IState getState()
        {
            return state;
        }
    }
}
