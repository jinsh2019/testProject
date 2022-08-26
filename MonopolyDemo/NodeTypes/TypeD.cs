using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyDemo.NodeTypes
{
    internal class TypeD : IStrategy
    {
        public int Root { get; set; } = 0;
        public int PathE { get; set; }
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool Execute(Player player)
        {
            player.Position = 0;
            player.canThrow = true;
            player.throwCount = 0;
            Console.Write($"踩雷了，回归1位置");
            return false;
        }
        public IStrategy setPathE(int pathE)
        {
            this.PathE = PathE;
            return this;
        }
        public void setRoot()
        {
            return;
        }
    }
}
