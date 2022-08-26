using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyDemo.NodeTypes
{
    internal class TypeA : IStrategy
    {
        public int Root { get; set; } = 0;
        public int PathE { get; set; }

        public bool Execute(Player player)
        {
            player.canThrow = true;
            player.throwCount = 0;
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
