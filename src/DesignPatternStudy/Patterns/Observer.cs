using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Console;

namespace DesignPatternStudy.Patterns  
{
    // 观察者 Observer 1. set订阅者；2. 调用订阅者attach方法 加入列表中
    // 发布者 Subject 1. 发布消息 setState 并通知 所有订阅者 notifyAllObservers
    public class Subject
    {
        private List<Observer> observers = new List<Observer>();
 
        private int state;

        public int getState()
        {
            return state;
        }
        // 状态改变，通知观察者
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

        // 不同的观察者，做出不同的行为 
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

        // 不同的观察者，做出不同的行为 
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

        // 不同的观察者，做出不同的行为 
        public override void update()
        {
            WriteLine("Hex String: " + Convert.ToString(subject.getState(), 16));
        }
    }
}
