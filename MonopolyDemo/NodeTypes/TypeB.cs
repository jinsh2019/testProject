using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyDemo.NodeTypes
{
    internal class TypeB : IStrategy
    {
        public int Root { get; set; } = 0;
        public int PathE { get; set; }


        public bool Execute(Player player)
        {
            player.Position--;
            Console.Write($"退回至节点{player.Position + 1}");
            return true;
        }

        public IStrategy setPathE(int pathE)
        {
            this.PathE = pathE;
            return this;
        }

        public void setRoot()
        {
            throw new NotImplementedException();
        }
    }
}
