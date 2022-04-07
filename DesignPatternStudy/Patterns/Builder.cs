using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternStudy.Patterns
{
    public abstract class Pad
    {
        protected string mBoard;
        protected string mDisplay;
        protected string mOs;


        protected Pad()
        {
        }

        public void setBoard(string board)
        {
            this.mBoard = board;
        }

        public void setDisplay(string display)
        {
            this.mDisplay = display;
        }


        public abstract void setOs();


        public override string ToString()
        {
            return "Pad{" +
                    "mBoard='" + mBoard + '\'' +
                    ", mDisplay='" + mDisplay + '\'' +
                    ", mOs='" + mOs + '\'' +
                    '}';
        }
    }

    public class MacBook : Pad
    {
        public MacBook()
        {
        }

        public override void setOs()
        {
            mOs = "Mac OS X 12";
        }
    }

    public abstract class Builder
    {
        public abstract void buildBoard(String board);
        public abstract void buildDisplay(String display);
        public abstract void buildOs();
        public abstract Pad build();

    }

    public class MacBookBuilder : Builder
    {
        private Pad mComputer = new MacBook();

        public override void buildBoard(String board)
        {
            mComputer.setBoard(board);
        }

        public override void buildDisplay(String display)
        {
            mComputer.setDisplay(display);
        }

        public override void buildOs()
        {
            mComputer.setOs();
        }

        public override Pad build()
        {
            return mComputer;
        }
    }

    public class Director
    {
        Builder mBuilser = null;
        public Director(Builder builer)
        {
            this.mBuilser = builer;
        }

        public void construct(String board, String display)
        {
            mBuilser.buildDisplay(display);
            mBuilser.buildBoard(board);
            mBuilser.buildOs();
        }
    }
}
