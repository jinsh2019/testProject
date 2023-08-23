using System;

namespace DesignPatternStudy.Patterns.State
{
    #region MyRegion

    //public interface IState 
    //{
    //    public void doAction(Context context);
    //}

    //public class StartState : IState
    //{
    //    public void doAction(Context context)
    //    {
    //        WriteLine("Player is in start state");
    //        context.setState(this);
    //    }

    //    public override string ToString()
    //    {
    //        return "Start State";
    //    }
    //}

    //public class StopState : IState
    //{
    //    public void doAction(Context context)
    //    {
    //        WriteLine("Player is in stop state");
    //        context.setState(this);
    //    }

    //    public override string ToString()
    //    {
    //        return "Stop State";
    //    }
    //}

    //public class Context
    //{
    //    private IState state;

    //    public Context()
    //    {
    //        state = null;
    //    }

    //    public void setState(IState state)
    //    {
    //        this.state = state;
    //    }

    //    public IState getState()
    //    {
    //        return state;
    //    }
    //} 
    #endregion

    interface State
    {
        void doWork();
    }

    class Happy : State
    {
        public void doWork()
        {
            Console.WriteLine("work happily.");
        }
    }

    class Angry : State
    {
        public void doWork()
        {
            Console.WriteLine("work angrily.");
        }
    }
    class Sad : State
    {
        public void doWork()
        {
            Console.WriteLine("Go Home.");
        }
    }

    // A Person/A Story has one state then doSomething according the state.
    class Context
    {
        private State state;
        public void changeState(State state)
        {
            this.state = state;
        }
        public void doSomething()
        {
            state.doWork();
        }
    }
}

