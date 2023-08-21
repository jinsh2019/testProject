using System.Collections.Generic;

namespace DesignPatternStudy.Patterns
{
    // Memento 包含了要被恢复的对象的状态
    // 需要保存的对象的副本
    public class Memento
    {
        public string State { get; private set; }
        public Memento(string state)
        {
            this.State = state;
        }
    }
    // Originator 创建并在 Memento 对象中存储状态
    public class Originator
    {
        public string State { get; set; }

        public Memento saveStateToMemento()
        {
            return new Memento(State);
        }

        public void getStateFromMemento(Memento Memento)
        {
            State = Memento.State;
        }
    }
    // Caretaker 对象负责从 Memento 中恢复对象的状态
    // 用一个列表来还原对象
    public class CareTaker
    {
        private List<Memento> mementoList = new List<Memento>();
        /// <summary>
        /// 添加保存对象
        /// </summary>
        /// <param name="state"></param>
        public void add(Memento state)
        {
            mementoList.Add(state);
        }
        /// <summary>
        /// 获取指定版本
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Memento get(int index)
        {
            return mementoList[index];
        }
    }
}