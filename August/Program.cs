using August.day9;
using CDS;

namespace August
{
    internal class Program
    {
        static List<string> list = new List<string>();

        static void Main(string[] args)
        {
            {
                var sum = Sum(10, 11, 12);
                Console.WriteLine(sum);
            }
            {
                var P = new string('U', 5);
            }

            {
                //["Twitter","postTweet","postTweet","postTweet","postTweet","postTweet","postTweet","postTweet","postTweet","postTweet","postTweet","getNewsFeed","follow","getNewsFeed"]
                //[[],[2,5],[1,3],[1,101],[2,13],[2,10],[1,2],[2,94],[2,505],[1,333],[1,22],[2],[2,1],[2]]

                Twitter twitter = new Twitter();
                twitter.PostTweet(2, 5);
                twitter.PostTweet(1, 3);
                twitter.PostTweet(1, 101);
                twitter.PostTweet(2, 13);
                twitter.PostTweet(2, 10);
                twitter.PostTweet(1, 2);
                twitter.PostTweet(2, 94);
                twitter.PostTweet(2, 505);
                twitter.PostTweet(1, 333);
                twitter.PostTweet(1, 22);
                // 5,13,10,94,505
                twitter.GetNewsFeed(2);
                // 3,101,2,333,22
                twitter.Follow(2, 1);
                twitter.GetNewsFeed(2);
            }

            {
                List<int> list = new List<int>(10);
                list.ForEach(x => x = -1);
            }
            {
                August.day9.Reg reg = new Reg();
                reg.IsMatch("aa", "a");
            }
            {
                int i = 2;
                int r1 = i * 3;
                int r2 = i * 12;
                int r3 = i * 64;
            }
            {
                //Task.Run(() =>
                //{
                //    for (int i = 0; i < int.MaxValue; i++)
                //    {
                //        list.Add(String.Join(",", Enumerable.Range(0, 1000)));
                //        Console.WriteLine(i);
                //    }
                //});
                //Console.ReadLine();
            }

            {
                August.day5 day5 = new August.day5();
                TreeNode treeNode1 = new TreeNode(1);
                TreeNode treeNode2 = new TreeNode(2);
                TreeNode treeNode3 = new TreeNode(3);
                TreeNode treeNode4 = new TreeNode(4);
                TreeNode treeNode5 = new TreeNode(5);
                TreeNode treeNode6 = new TreeNode(6);
                treeNode5.left = treeNode3;
                treeNode5.right = treeNode6;
                treeNode3.left = treeNode2;
                treeNode3.right = treeNode4;
                treeNode2.left = treeNode1;
                day5.InorderSuccessor(treeNode5, treeNode4);
            }
        }

        private static object Sum(int a, int b, int c)
        {
            return a + b + c;
        }
    }
}