using System;
using System.Collections.Generic;

namespace DesignPatternStudy.Patterns
{
    // 享元
    //https://www.bilibili.com/video/BV1Ka4y1L7jg/?spm_id_from=pageDriver&vd_source=4e306a9c5b741e5f5039fefb051598ff

    public abstract class BikeFlyWeight
    {
        protected int state = 0;
        public abstract void ride(string userName);
        public abstract void back();

        public int getState()
        {
            return state;
        }
    }

    public class MoBikeFlyWeight : BikeFlyWeight
    {

        private string bikeId;
        public MoBikeFlyWeight(string bikeId)
        {
            this.bikeId = bikeId;
        }

        public override void back()
        {
            state = 0;
        }

        public override void ride(string userName)
        {
            state = 1;
            Console.WriteLine(userName + " is riding bike No. " + bikeId + ".");
        }
    }

    public class BikeFlyWeightFactory
    {
        private static readonly BikeFlyWeightFactory instance = new BikeFlyWeightFactory();
        private List<BikeFlyWeight> pool = new List<BikeFlyWeight>();

        public static BikeFlyWeightFactory getInstance()
        {
            return instance;
        }

        private BikeFlyWeightFactory()
        {
            for (int i = 0; i < 2; i++)
            {
                pool.Add(new MoBikeFlyWeight(i + ""));
            }
        }

        public BikeFlyWeight getBike()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].getState() == 0)
                    return pool[i];
            }
            return null;
        }
    }
}