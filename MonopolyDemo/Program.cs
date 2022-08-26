using MonopolyDemo.NodeTypes;
using System;
using System.Collections.Generic;

namespace MonopolyDemo
{
    internal class Program
    {
        // 使用hashmap类型来扩展参赛选手
        public static Dictionary<int, string> dic = new Dictionary<int, string>();
        // 使用List，来扩展路径
        public static List<PathNode> path = new List<PathNode>();


        // 洗牌，业务需要
        public static List<Player> listPlayer = new List<Player>();
        // 统计E规则，业务需要
        public static List<int> pathE = new List<int>();
        // 当前用户 业务字段
        public static int curPlayer = 0;

        static void Main(string[] args)
        {
            #region 初始化 人 路径
            //  人
            dic.Add(1, "甲");
            dic.Add(2, "乙");
            dic.Add(3, "丙");
            Console.Write("参与人:");
            foreach (var item in dic)
            {
                Console.Write(item.Value + ",");
            }
            Console.WriteLine();

            // 路径
            string[] pathTypes = new string[] { "A", "A","B","A", "C",
                                            "A","A", "D","A",
                                            "A","E","A",
                                            "A","A","A",
                                            "E","B",
                                            "A","C",
                                            "A"};
            Console.Write($"赛道循序:");
            for (int i = 0; i < pathTypes.Length; i++)
            {
                var pathNode = new PathNode() { Type = pathTypes[i], strategy = getStrategy(pathTypes[i]) };
                path.Add(pathNode);
                if (pathNode.Type == "E")
                {
                    pathE.Add(i);
                }
                Console.Write($"{pathTypes[i]}->");
            }
            Console.WriteLine();
            #endregion

            #region 初始化业务参数
            int length = dic.Count; // 参与的人数
            int round = 1;
            bool bflag = true;
            Random random1 = new Random();
            #endregion

            // 洗牌选手
            setOperationSequence();
            //listPlayer.ForEach(x => x.setPathE(pathE[0]));
            // 开始游戏
            while (bflag)
            {
                Console.WriteLine($"第{round}轮:");
                for (int i = 0; i < length; i++)
                {
                    curPlayer = i % length;
                    #region 设置投掷范围
                    int maxValue = 0;
                    if (path.Count - 1 - listPlayer[curPlayer].Position < 6)
                        maxValue = path.Count - listPlayer[curPlayer].Position;
                    else
                        maxValue = random1.Next(1, 6);
                    Console.Write(listPlayer[curPlayer].Name + ":投掷选择" + $"{maxValue}");
                    setRange(maxValue);
                    #endregion

                    #region 运行rule3 rule4 连续运用规则
                    cycleRules();
                    #endregion

                    #region 运行 rule3 这个点存在其他选手时， 其他选手回到起点
                    CheckOtherItemBackStart();
                    #endregion
                    // 胜利条件
                    if (listPlayer[curPlayer].Position == path.Count - 1)
                    {
                        Console.Write($"已经到达终点");
                        Console.Write($"{listPlayer[curPlayer].Name} 胜利！");
                        bflag = false;
                    }
                    Console.WriteLine();
                }
                round++;
                Console.ReadKey();
            }

            Console.WriteLine("退出游戏！");
            Console.ReadKey();

        }

        private static IStrategy getStrategy(string v)
        {
            switch (v)
            {
                case "A":
                    return new TypeA();
                case "B":
                    return new TypeB();
                case "C":
                    return new TypeC();
                case "D":
                    return new TypeD();
                case "E":
                    return new TypeE();
                default:
                    return null;
            }
        }

        #region 设置投掷顺序
        // Rule1 
        public static void setOperationSequence()
        {
            List<int> listSeq = new List<int>();
            for (int i = 0; i < dic.Count; i++)
            {
                listSeq.Add(i + 1);
            }
            int[] nums = listSeq.ToArray();
            shuttle(nums);
            Console.Write("规则1：打乱投掷循序:");
            for (int i = 0; i < nums.Length; i++)
            {
                listPlayer.Add(new Player() { Name = dic[nums[i]] });
                Console.Write(dic[nums[i]] + ",");
            }
            Console.WriteLine();
        }


        private static void shuttle(int[] nums)
        {
            int n = nums.Length;
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                int r = rand.Next(n - i); // 0~n
                swap(nums, i, r);
            }

        }

        private static void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        #endregion

        #region 设置投掷范围
        // 1.设置投掷范围
        // 2.随机返回
        public static void setRange(int maxValue)
        {
            Random rand = new Random();
            int step = rand.Next(1, maxValue); // 1~n
            Console.Write($"结果{step},");

            //  在path上时
            if (listPlayer[curPlayer].canThrow == false)
            {
                Console.Write($"投掷无效一次，");
                listPlayer[curPlayer].canThrow = true;
                listPlayer[curPlayer].throwCount++;
            }
            else
            {
                int curPos = listPlayer[curPlayer].Position;
                if (curPos + step > 19)
                {
                    Console.WriteLine("超出终点，下轮再投");
                }
                else
                {
                    listPlayer[curPlayer].Position += step;
                    listPlayer[curPlayer].throwCount = 0;
                    listPlayer[curPlayer].canThrow = true;
                    listPlayer[curPlayer].setStrategy(path[listPlayer[curPlayer].Position].strategy).Play();
                }

            }
            Console.Write($"前进至节点{listPlayer[curPlayer].Position + 1}");
        }
        #endregion


        // rule3 这个点存在其他选手时， 其他选手回到起点
        public static void CheckOtherItemBackStart()
        {
            foreach (var item in listPlayer)
            {
                if (item.Name != listPlayer[curPlayer].Name &&
                    item.Position == listPlayer[curPlayer].Position &&
                    item.Position != 0)
                {
                    item.Position = 0;
                    item.canThrow = true;
                    item.throwCount = 0;
                    Console.Write($"将{item.Name}踢回至节点{1},");
                }
            }
        }

        // rule4 连续使用规则
        public static void cycleRules()
        {
            if (listPlayer[curPlayer].Position == path.Count - 1) return;
            bool bContinue = true;
            while (bContinue)
            {
                int position = listPlayer[curPlayer].Position;
                bContinue = listPlayer[curPlayer].setStrategy(path[position].strategy).Play();
            }
        }
    }
}
