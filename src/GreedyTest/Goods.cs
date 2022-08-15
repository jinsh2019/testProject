using System.Collections.Generic;

namespace GreedyTest
{
    // 贪心算法
    public class Goods
    {
        private int capacity;
        private int value;
        // 物品的性价比
        private double ratio;

        public Goods(int capacity, int value)
        {
            this.capacity = capacity;
            this.value = value;
            this.ratio = (double)value / (double)capacity;
        }
        public double getRadio()
        {
            return ratio;
        }

        public static double getHighestValue(int capacity, int[] weights, int[] values)
        {
            // 创建物品列表并按照性价比倒序
            List<Goods> itemList = new List<Goods>();
            for (int i = 0; i < weights.Length; i++)
            {
                itemList.Add(new Goods(weights[i], values[i]));
            }
            itemList.Sort((x, y) => x.ratio - y.ratio > 0 ? -1 : 1); // 因为是升序，所以反过来

            // 背包剩余容量
            int resCapacity = capacity;
            // 当前背包物品的最大价值
            double highestValue = 0;

            // 按照性价比从高到低选择物品
            foreach (var item in itemList) // 从底到顶
            {
                if (item.capacity <= resCapacity)
                {
                    highestValue += item.value;
                    resCapacity -= item.capacity;
                }
                else
                {
                    // 背包装不下完整物品时，选择该件物品的一部分
                    highestValue += (double)resCapacity / (double)item.capacity * item.value;
                    break;
                }
            }

            return highestValue;
        }
    }
}
