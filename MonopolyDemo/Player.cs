using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyDemo
{
    internal class Player
    {
        public string Name { get; set; }
        public int Sequence { get; set; }

        public int Position { get; set; } = 0;
        public bool canThrow { get; set; } = true;
        public int throwCount { get; set; } = 0;
        public IStrategy strategy { get; set; }

        public Player setStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
            return this;
        }
        public Player setPathE(int pathE)
        {
            this.strategy.setPathE(pathE);
            return this;
        }
        public bool Play()
        {
            return strategy.Execute(this);
        }
    }
}
