using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MonopolyDemo.NodeTypes
{
    internal class TypeC : IStrategy
    {
        public int Root { get; set; } = 0;
        public int PathE { get; set; }

        public bool Execute(Player player)
        {
            if (player.throwCount == 0 && player.canThrow == false)
            {
                player.canThrow = false;
                player.throwCount = 0;
                //Console.Write($"设置投掷无效状态，");
                return false;
            }
            if (player.canThrow == false)
            {
                Console.Write($"投掷无效一次，");
                player.canThrow = true;
                player.throwCount++;
                return false;
            }
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
