using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyDemo.NodeTypes
{
    internal class TypeE : IStrategy
    {
        public int Root { get; set; } = 0;
        public int PathE { get; set; }
        public IStrategy setPathE(int pathE)
        {
            this.PathE = PathE;
            return this;
        }

        public void setRoot()
        {
            return;
        }

        public bool Execute(Player player)
        {
            player.Position = PathE;
            player.canThrow = true;
            player.throwCount = 0;
            Console.WriteLine($"遇到E节点，返回到第一个E节点{player.Position + 1}");
            return false;
        }
    }
}
