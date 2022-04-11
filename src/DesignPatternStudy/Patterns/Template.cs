using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DesignPatternStudy.Patterns
{
    public abstract class Game
    {
        public abstract void initialize();
        public abstract void startPlay();
        public abstract void endPlay();

        //模板
        public void play()
        {

            //初始化游戏
            initialize();

            //开始游戏
            startPlay();

            //结束游戏
            endPlay();
        }
    }


    public class Cricket : Game
    {

        public override void endPlay()
        {
            WriteLine("Cricket Game Finished!");
        }

        public override void initialize()
        {
            WriteLine("Cricket Game Initialized! Start playing.");
        }

        public override void startPlay()
        {
            WriteLine("Cricket Game Started. Enjoy the game!");
        }
    }


    public class Football : Game
    {

        public override void endPlay()
        {
            WriteLine("Football Game Finished!");
        }

        public override void initialize()
        {
            WriteLine("Football Game Initialized! Start playing.");
        }

        public override void startPlay()
        {
            WriteLine("Football Game Started. Enjoy the game!");
        }
    }
}
