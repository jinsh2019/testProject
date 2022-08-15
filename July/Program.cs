// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using CDS;
using July.algorithms.day2;
using July.algorithms.day22;
using July.algorithms.day4;
using July.daily;
using System.Collections;
using System.Text;
using Node = CDS.Node;

{
    day1 d1 = new day1();
    int[] nums = { };
    d1.findKthLargest(nums, 2);

}

{
    day1 day1 = new day1();
    int[][] edges = new int[3][];
    edges[0] = new int[] { 0, 1 };
    edges[1] = new int[] { 1, 2 };
    edges[2] = new int[] { 3, 4 };
    day1.CountComponents(5, edges);
}
{
    // 4,2,6,1,3
    TreeNode t4 = new TreeNode(4);
    TreeNode t2 = new TreeNode(2);
    TreeNode t6 = new TreeNode(6);
    TreeNode t3 = new TreeNode(3);
    TreeNode t1 = new TreeNode(1);
    t4.left = t2;
    t4.right = t6;
    t2.right = t3;
    t2.left = t1;
    day1 day1 = new day1();
    day1.GetMinimumDifference(t4);
}
{
    TreeNode t5 = new TreeNode(5);
    TreeNode t55 = new TreeNode(5);
    TreeNode t8 = new TreeNode(8);

    t5.left = t55;
    t5.right = t8;
    day1 day1 = new day1();
    day1.FindSecondMinimumValue(t5);
}

{
    July.algorithms.day2.MergeSort mergeSort = new July.algorithms.day2.MergeSort();
    int[] nums = new int[] { 5, 2, 3, 1 };
    mergeSort.sort(nums);
}

{

    //1,2,3,null,null,4,5
    List<TreeNode> nodes = new List<TreeNode>();
    for (int i = 0; i < 10; i++)
    {
        nodes.Add(new TreeNode(i));
    }
    nodes[1].left = nodes[2];
    nodes[1].right = nodes[3];
    nodes[3].left = nodes[4];
    nodes[3].right = nodes[5];
    July.algorithms.day4.SerializeTree serializeTree = new July.algorithms.day4.SerializeTree();
    string data = serializeTree.Serialize(nodes[1]);
    Console.WriteLine(data);
    TreeNode root = serializeTree.Deserialize(data);

}

{
    // [5,3,6,2,4,null,8,1,null,null,null,7,9]
    BST bST = new BST();
    List<TreeNode> nodes = new List<TreeNode>();
    for (int i = 0; i < 10; i++)
    {
        nodes.Add(new TreeNode(i));
    }
    nodes[5].left = nodes[3];
    nodes[5].right = nodes[6];
    nodes[3].left = nodes[2];
    nodes[3].right = nodes[4];
    nodes[6].left = nodes[8];
    nodes[8].left = nodes[7];
    nodes[8].right = nodes[9];
    bST.IncreasingBST(nodes[5]);
}

{
    IList<IList<int>> lists = new List<IList<int>>();
    lists.Reverse().ToList();
    LinkedList<int> ints = new LinkedList<int>();
    lists.Add(ints.ToList());
}

{
    day2 day2 = new day2();
    TreeNode node1 = new TreeNode(1);
    TreeNode node2 = new TreeNode(2);
    node1.left = node2;
    day2.ZigzagLevelOrder(node1);
}

{
    List<TreeNode> nodes = new List<TreeNode>();
    for (int i = 0; i < 30; i++)
    {
        nodes.Add(new TreeNode(i));
    }
    nodes[3].left = nodes[9];
    nodes[3].right = nodes[20];
    nodes[20].left = nodes[15];
    nodes[20].right = nodes[7];
    day2 day2 = new day2();
    day2.ZigzagLevelOrder(nodes[9]);
}
{

    Patterns patterns = new Patterns();
    int[] nums = new int[] { 1, 3, 4, 2 };
    patterns.NextGreaterElement(nums);
}
{
    int[] arr = { 1, 2, 3, 4, 5 };
    int n = arr.Length, index = 0, level = 0;
    while (true)
    {
        if (index == 0)
        {
            ++level;
            if (level > 5)
                break;
            Console.WriteLine();
        }
        Console.Write(arr[index]);
        index = (index + 1) % n;
    }
}
{
    int[] nums = new int[] { 1, 2, 3 };
    July.algorithms.day22.MySet mySet = new July.algorithms.day22.MySet();
    mySet.Subsets(nums);
}
{
    Console.WriteLine();
    July.algorithms.day5.Patterns patterns = new July.algorithms.day5.Patterns();
    patterns.findTheWinner(10, 3);
}
{
    July.algorithms.day5.MySet mySet = new July.algorithms.day5.MySet();

    mySet.Combine(3, 3);
}
{

    StringBuilder sb = new StringBuilder();
    //sb.Remove()
    //sb.Length
}
{
    July.algorithms.day6.Patterns patterns = new July.algorithms.day6.Patterns();
    int[][] connections = new int[3][];
    connections[0] = new int[] { 1, 2, 5 };
    connections[1] = new int[] { 1, 3, 6 };
    connections[2] = new int[] { 2, 3, 1 };
    patterns.MinimumCost(3, connections);
}

{
    July.algorithms.day6.Patterns patterns = new July.algorithms.day6.Patterns();
    int[] nums = { 1, 2, 3 };
    patterns.NextPermutation(nums);
    nums.Where(x => x == 3).Count();
}
{
    July.algorithms.day6.Patterns patterns = new July.algorithms.day6.Patterns();
    patterns.FirstUniqChar("leetcode");
}
{
    string str = "abc  edf";
    string[] res = str.Trim().Split(" ");
    StringBuilder sb = new StringBuilder();
    for (int i = res.Length - 1; i >= 0; i--)
    {
        sb.Append(res[i]).Append(" ");
    }
    //return sb.ToString
}
{
    //string s = "x";
    //s.IndexOf("")

    July.algorithms.day11.MyStack myStack = new July.algorithms.day11.MyStack();
    myStack.Push(2);
    myStack.Pop();
    myStack.Top();
    myStack.Empty();
}
{
    string str = "2[abc]3[cd]ef";// "3[a]2[bc]";
    July.algorithms.day11.Patterns patterns = new July.algorithms.day11.Patterns();
    patterns.DecodeString(str);
}
{
    //int[][] people = new int[3][];
    //List<int[]> res = new List<int[]>();
    //var map = people.GroupBy(x => x[1]).OrderBy(x => x).ToDictionary(k => k, v => v);
    //foreach (var key in map.Keys)
    //{
    //    foreach (var item in map[key])
    //    {
    //        res.Add(item);
    //    }
    //}
}

{
    int[] nums = new int[5];

    string[] strs = new string[nums.Length];
    for (int i = 0; i < nums.Length; i++)
    {
        strs[i] = nums[i].ToString();
    }
    Array.Sort(strs, (x, y) => (int.Parse((x + y))).CompareTo(int.Parse(y + x)));
    StringBuilder res = new StringBuilder();
    foreach (string s in strs)
    {
        res.Append(s);
    }
    //return res.ToString();
}
{
    int[] nums1 = new int[1];
    int[] nums2 = new int[2];
    nums1.Intersect(nums2).ToArray();
}
{
    July.algorithms.day12.MySet mySet = new July.algorithms.day12.MySet();
    mySet.Subsets(new int[] { 1, 2, 3 });
}
{

    July.algorithms.day12.MySet mySet = new July.algorithms.day12.MySet();
    mySet.Combine(3, 2);
}
{
    day2 day2 = new day2();
    int[] nums3 = new int[] { 1, 4, 3, 2, 5, 2 };
    ListNode dummy = new ListNode(); // 占位符 dummy
    ListNode p = dummy;// p指向dummy
    foreach (var num in nums3)
    {
        p.next = new ListNode(num); // p.next(dummy.next，新node指向新新node)指向新node
        p = p.next;                 // p指向新node，新新node
    }
    day2.Partition(dummy.next, 3);
}

{
    day2 day2 = new day2();
    int[] nums3 = new int[] { 1, 2, 3, 4, 5 };
    ListNode dummy = new ListNode(); // 占位符 dummy
    ListNode p = dummy;// p指向dummy
    foreach (var num in nums3)
    {
        p.next = new ListNode(num); // p.next(dummy.next，新node指向新新node)指向新node
        p = p.next;                 // p指向新node，新新node
    }
    day2.RotateRight(dummy.next, 2);
}
{
    day12 day12 = new day12();
    // [1,2,3,4,null,2,4,null,null,4]
    List<TreeNode> nodes = new List<TreeNode>();
    for (int i = 0; i < 30; i++)
    {
        nodes.Add(new TreeNode(i));
    }
    nodes[1].left = nodes[2];
    nodes[1].right = nodes[3];
    nodes[2].left = nodes[4];
    nodes[5].val = 2;
    nodes[3].left = nodes[5];
    nodes[3].right = nodes[4];
    nodes[6].val = 4;
    nodes[5].right = nodes[6];
    day12.FindDuplicateSubtrees(nodes[1]);
}
{
    Dictionary<int, int> map = new Dictionary<int, int>();
    map[1] = 2;
    map[1] = 3;
    map[2] = 2;
    LinkedList<int> nodes = new LinkedList<int>();

    IComparer<int> mycom = new mycompare<int>();
    PriorityQueue<int, int> pq = new PriorityQueue<int, int>(new mycompare<int>());
    int[] nums3 = new int[] { 1, 2, 3, 4, 5 };
    Array.Sort(nums3, (a, b) => a - b);

}

{
    day12 day12 = new day12();
    day12.SolveNQueens(4);
    List<char> list = new List<char>();
    list.Add('a');
    list.Add('h');
    var p = new string(list.ToArray());
}
{
    day13 day13 = new day13();
    day13.GenerateParenthesis(3);
}
{
    //int[] nums = new int[10];
    //nums.Skip(i).ToArray()

    day13 day13 = new day13();
    int[] nums = new int[] { 2, 0, 1 };
    day13.SortColors(nums);

}
{
    day13 day13 = new day13();
    day13.GetRow(3);
}
{
    day18 day18 = new day18();
    int[][] nums = new int[2][];
    nums[0] = new int[] { -2147483646, -2147483645 };
    nums[1] = new int[] { 2147483646, 2147483647 };
    day18.FindMinArrowShots(nums);
}
{
    day18 day18 = new day18();
    int[] nums = new int[] { 1, 2, 3, 4, 5 };
    day18.IncreasingTriplet(nums);
}
{
    day18 day18 = new day18();
    int[] nums = new int[] { 1, 3, 6, 7, 9, 4, 10, 5, 6 };
    day18.LengthOfLIS(nums);
    Array.Sort(nums, (a, b) => a.CompareTo(b));
}
{
    StringBuilder sb = new StringBuilder();
    var ans = sb.ToString();
    ans.Reverse().ToString();



}
{
    day18 day18 = new day18();
    day18.AddStrings("11", "123");
}
{
    day18 day18 = new day18();
    day18.LongestPalindrome("abccccdd");
}
{
    day18 day18 = new day18();
    int[] heights = new int[] { 2, 1, 5, 6, 2, 3 };
    day18.LargestRectangleArea(heights);
}
{
    Dictionary<int, int> map = new Dictionary<int, int>()
    {
        { 1,1}
    };
    Console.Write(Math.Pow(2, 3));
}
{
    day23 day23 = new day23();
    int[] nums = { 76, 76, 76 };
    day23.DailyTemperatures(nums);
    Console.WriteLine();
    for (int i = 0; i < 2 * nums.Length - 1; i++)
    {
        Console.Write(nums[i % nums.Length]);
    }
}

{
    day23 day23 = new day23();
    int[] nums = { 2, 6, 4, 8, 10, 9, 15 };
    day23.FindUnsortedSubarray(nums);
}

{
    day23 day23 = new day23();
    int[] nums = { 1, 1, 1, -2, 1 };
    day23.SubarraySum(nums, 1);
}
{

    List<TreeNode> nodes = new List<TreeNode>();
    for (int i = 0; i < 10; i++)
    {
        nodes.Add(new TreeNode(i));
    }

    nodes[1].left = nodes[2];
    nodes[1].right = nodes[3];
    nodes[2].left = nodes[4];
    nodes[2].right = nodes[5];
    day23 day23 = new day23();
    day23.DiameterOfBinaryTree(nodes[1]);
}
{
    Console.WriteLine();


    List<TreeNode> nodes = new List<TreeNode>();
    for (int i = 0; i < 20; i++)
    {
        nodes.Add(new TreeNode(i));
    }
    nodes[5].left = nodes[4];
    nodes[5].right = nodes[8];
    nodes[4].left = nodes[11];
    nodes[11].left = nodes[7];
    nodes[11].right = nodes[2];
    nodes[8].left = nodes[13];
    nodes[8].right = nodes[14];
    nodes[14].val = 4;
    nodes[15].val = 5;
    nodes[14].left = nodes[15];
    nodes[14].right = nodes[1];


    day23 day23 = new day23();
    day23.PathSum(nodes[5], 22);
}
{
    List<TreeNode> nodes = new List<TreeNode>();
    for (int i = 0; i < 20; i++)
    {
        nodes.Add(new TreeNode(i));
    }
    nodes[10].left = nodes[5];
    nodes[10].right = nodes[15];
    nodes[5].left = nodes[3];
    nodes[5].right = nodes[7];
    nodes[15].left = nodes[18];
    day24 day24 = new day24();
    day24.RangeSumBST(nodes[10], 7, 15);
}
{

    day24 day24 = new day24();
    int[][] prerequisites = new int[4][];
    prerequisites[0] = new int[] { 1, 0 };
    prerequisites[1] = new int[] { 2, 0 };
    prerequisites[2] = new int[] { 3, 1 };
    prerequisites[3] = new int[] { 3, 2 };
    day24.CanFinish(4, prerequisites);
}

{
    int[] nums = { 1, 2, 3, 4, 5 };
    day25 day25 = new day25();
    List<ListNode> lnodes = new List<ListNode>();
    for (int i = 0; i < 10; i++)
    {
        lnodes.Add(new ListNode(i));
    }
    lnodes[1].next = lnodes[2];
    lnodes[2].next = lnodes[3];
    lnodes[3].next = lnodes[4];
    lnodes[4].next = lnodes[5];

    day25.ReverseKGroup(lnodes[1], 2);
}
{
    Console.WriteLine();
    day25 day25 = new day25();
    day25.Permutation(new int[] { 1, 2, 3 });
}

{
    Console.WriteLine();
    day25 day25 = new day25();
    day25.Subsets(new int[] { 1, 2, 3 });
}
{
    Console.WriteLine();
    day25 day25 = new day25();
    day25.Combine(3, 2);
}
{
    Console.WriteLine();
    day25 day25 = new day25();

    day25.CombinationSum2(new int[] { 10, 1, 2, 7, 6, 1, 5 }, 8);
}
{
    day25 day25 = new day25();
    day25.NextPermutation(new int[] { 1, 2, 3 });
}
{
    day25 day25 = new day25();
    day25.LengthOfLongestSubstring("abcabcbb");
}
{
    day25 day25 = new day25();
    day25.LengthOfLongestSubstringTwoDistinct("eceba");
}
{
    day25 day25 = new day25();
    day25.LengthOfLongestSubstringKDistinct("eceba", 2);
}
{
    day25 day25 = new day25();
    day25.MaxSubArray(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 });
}
{
    day25 day25 = new day25();
    int[][] matrix = new int[3][];
    matrix[0] = new int[3] { 1, 2, 3 };
    matrix[1] = new int[3] { 4, 5, 6 };
    matrix[2] = new int[3] { 7, 8, 9 };
    day25.Rotate(matrix);
}
{
    day25 day25 = new day25();
    day25.LongestPalindrome("babad");
}
{
    day25 day25 = new day25();
    day25.ReverseWords("example   good a");
}
{
    day25 day25 = new day25();
    day25.Search(new int[] { 5, 1, 3 }, 3);
}

{
    StringBuilder sb = new StringBuilder();

    //int[][] intervals = new int[2][];
    //Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
    //List<int[]> total = new List<int[]>();
}
{
    day25 day25 = new day25();
    day25.SearchRange(new int[] { 5, 7, 7, 8, 8, 10 }, 8);
}

{
    day25 day25 = new day25();
    string[] matrix = new string[3] { "ABCE", "SFCS", "ADEE" };
    day25.searchWord(matrix, "ABCCED");
}

{

    int[] nums = new int[] { 5, 2, 6 };//{2,3,8,1,4,9,10,7,16,14};
    HeapSort heapSort = new HeapSort();
    heapSort.sort(nums);

}
{
    var P = new string('U', 5);
    StringBuilder sb = new StringBuilder(); 
    //sb.Remove()
}
{
    HashSet<int> hashset = new HashSet<int>();
    hashset.Add(1);
}