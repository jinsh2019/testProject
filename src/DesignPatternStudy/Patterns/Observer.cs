using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Console;

namespace DesignPatternStudy.Patterns
{
    // Observer 把订阅者的指针传到订阅者身上，把订阅者自己挂到订阅者列表上
    // 发布者
    public class Subject
    {
        private List<Observer> observers = new List<Observer>();

        private int state;

        public int getState()
        {
            return state;
        }

        public void setState(int state)
        {
            this.state = state;
            notifyAllObservers();
        }

        public void attach(Observer observer)
        {
            observers.Add(observer);
        }

        private void notifyAllObservers()
        {
            foreach (var observer in observers)
            {
                observer.update();
            }
        }
    }
    // 观察者
    public abstract class Observer
    {
        protected Subject subject;
        public abstract void update();
    }
    // 观察者实例
    public class BinaryObserver : Observer
    {

        public BinaryObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            WriteLine("Binary String: " + Convert.ToString(subject.getState(), 2));
        }
    }
    public class OctalObserver : Observer
    {

        public OctalObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            WriteLine("Octal String: " + Convert.ToString(subject.getState(), 10));
        }
    }

    public class HexaObserver : Observer
    {

        public HexaObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            WriteLine("Hex String: " + Convert.ToString(subject.getState(), 16));
        }
    }
}
