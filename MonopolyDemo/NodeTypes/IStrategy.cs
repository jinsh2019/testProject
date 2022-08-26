using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyDemo
{
    internal interface IStrategy
    {
        int Root { get; set; }
        int PathE { get; set; }
        IStrategy setPathE(int pathE);
        void setRoot();
        bool Execute(Player player);
    }
}
