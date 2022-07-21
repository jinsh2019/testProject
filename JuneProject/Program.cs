// See https://aka.ms/new-console-template for more information
using CDS;
using JuneProject;
using JuneProject.cls;

Class1 cls = new Class1();
cls.Generate(5);
char[] str = new char[] { 'a', 'b', 'c' };
Class1.printReverse(str);
{
    TreeNode node3 = new TreeNode(3);
    TreeNode node1 = new TreeNode(1, null, node3);
    cls.RightSideView(node1);
}
{
    int[] nums = new int[] { 2, 1, 2, 4, 3 };
    Class1.nextGreaterElement(nums);
}

{
    TreeNode root = new TreeNode();
    int[] nums = new int[] { 2, 1, 2, 4, 3 };
    // root.Build(nums);
}

{
    Class3 cls3 = new Class3();
    TreeNode node1 = new TreeNode(1);
    TreeNode node2 = new TreeNode(2);
    TreeNode node3 = new TreeNode(3);
    node1.left = node3;
    node3.right = node2;
    cls3.RecoverTree(node1);
}
{
    TreeNode node1 = new TreeNode(1);
    TreeNode node2 = new TreeNode(2);
    TreeNode node3 = new TreeNode(3);
    TreeNode node4 = new TreeNode(4);
    TreeNode node5 = new TreeNode(5);
    TreeNode node6 = new TreeNode(6);
    TreeNode node7 = new TreeNode(7);
    node5.left = node2;
    node2.left = node1;
    node2.right = node4;
    node4.left = node3;
    node5.right = node6;
    node6.right = node7;
    Class3 cls3 = new Class3();
    cls3.IsValidBST(node5);

    Class3.sort(new int[] { 3, 2, 1 });
}
{
    int n = 5;
    int[] nums = new int[n];
    Array.Fill(nums, -1);
    List<int> res = new List<int>(nums);
}
{
    Class5 cls5 = new Class5();
    ListNode node1 = new ListNode(1);
    ListNode node2 = new ListNode(2);
    ListNode node3 = new ListNode(3);
    ListNode node4 = new ListNode(4);
    ListNode node5 = new ListNode(5);
    node1.next = node2;
    node2.next = node3;
    node3.next = node4;
    node4.next = node5;
    cls5.ReverseBetween(node1, 2, 4);
}
{
    Class5 cls5 = new Class5();
    cls5.LengthOfLongestSubstring("abcabcbb");
}
{
    Class5 cls5 = new Class5();
    ListNode node1 = new ListNode(1);
    ListNode node2 = new ListNode(2);
    ListNode node3 = new ListNode(3);
    ListNode node4 = new ListNode(4);
    ListNode node5 = new ListNode(5);
    node1.next = node2;
    node2.next = node3;
    node3.next = node4;
    node4.next = node5;
    cls5.RemoveNthFromEnd(node1, 2);
}
{
    Class5 cls5 = new Class5();
    ListNode l1_1 = new ListNode(1);
    ListNode l1_2 = new ListNode(2);
    ListNode l1_4 = new ListNode(4);
    l1_1.next = l1_2;
    l1_2.next = l1_4;
    ListNode l2_1 = new ListNode(1);

    ListNode l2_3 = new ListNode(3);
    ListNode l2_4 = new ListNode(4);
    l2_1.next = l2_3;
    l2_3.next = l2_4;
    cls5.MergeTwoLists(l1_1, l2_1);
}

{
    Class5 cls5 = new Class5();
    ListNode node1 = new ListNode(1);
    ListNode node2 = new ListNode(2);
    ListNode node3 = new ListNode(3);
    ListNode node4 = new ListNode(4);
    ListNode node5 = new ListNode(5);
    node1.next = node2;
    node2.next = node3;
    node3.next = node4;
    node4.next = node5;
    PriorityQueue<ListNode, int> pq = new PriorityQueue<ListNode, int>();
    while (node1 != null)
    {
        pq.Enqueue(node1, node1.val);
        node1 = node1.next;
    }
    while (pq.Count != 0)
    {
        Console.WriteLine(pq.Dequeue().val);
    }
}

{
    Class5 cls5 = new Class5();
    ListNode node0 = new ListNode(0);
    ListNode node2 = new ListNode(2);
    ListNode node3 = new ListNode(3);
    ListNode node4 = new ListNode(4);
    ListNode node5 = new ListNode(5);
    // 3,2,0,4 --> 1
    node3.next = node2;
    node2.next = node0;
    node0.next = node4;
    node4.next = node2;
    cls5.HasCycle(node3);

}
{
    ListNode node1 = new ListNode(1);
    ListNode node2 = new ListNode(2);
    ListNode node3 = new ListNode(3);
    ListNode node4 = new ListNode(4);
    ListNode node5 = new ListNode(5);
    node1.next = node2;
    node2.next = node3;
    node3.next = node4;
    node4.next = node5;
    Class5 cls5 = new Class5();
    cls5.ReverseKGroup(node1, 2);
}
{
    ListNode node1 = new ListNode(1);
    ListNode node2 = new ListNode(2);
    ListNode node3 = new ListNode(2);
    ListNode node4 = new ListNode(1);
    ListNode node5 = new ListNode(5);
    node1.next = node2;
    node2.next = node3;
    node3.next = node4;
    //node4.next = node5;
    Class5 cls5 = new Class5();
    cls5.IsPalindrome(node1);
}
{
    Class6 cls6 = new Class6();
    cls6.SolveNQueens(4);
}
{
    Class6 cls6 = new Class6();
    int[] nums = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
    cls6.MaxSubArray(nums);
}

{
    CodecBST cc = new CodecBST();
    TreeNode treeNode1 = new TreeNode(1);
    TreeNode treeNode2 = new TreeNode(2);
    TreeNode treeNode3 = new TreeNode(3);
    TreeNode treeNode4 = new TreeNode(4);
    TreeNode treeNode5 = new TreeNode(5);
    TreeNode treeNode6 = new TreeNode(6);
    TreeNode treeNode7 = new TreeNode(7);
    TreeNode treeNode8 = new TreeNode(8);
    TreeNode treeNode9 = new TreeNode(9);
    TreeNode treeNode10 = new TreeNode(10);

    treeNode5.left = treeNode3;
    // treeNode3.left = treeNode1; // for rootVal < min
    treeNode3.right = treeNode4;
    treeNode5.right = treeNode9;
    treeNode9.left = treeNode7;
    treeNode9.right = treeNode10;

    var serializeTree = cc.serialize(treeNode5);
    TreeNode deserializeTree = cc.deserialize(serializeTree);
}
{
    CodecCommonTree cc1 = new CodecCommonTree();
    TreeNode treeNode = new TreeNode(1);
    TreeNode treeNode1 = new TreeNode(2);
    TreeNode treeNode2 = new TreeNode(3);
    treeNode1.left = treeNode;
    treeNode1.right = treeNode2;

    var serializeTree = cc1.serialize(treeNode1);
    TreeNode? deserializeTree = cc1.deserialize(serializeTree);
}
{
    Class6 cls6 = new Class6();
    int[] nums = { 1, 2, 3, 4 };
    cls6.ProductExceptSelf(nums);
}
{
    Class6 cls6 = new Class6();
    int[] nums = { 3, 3 };
    cls6.TwoSum(nums, 6);
}
{
    Class6 cls6 = new Class6();
    int[] nums1 = new int[] { 1, 2, 3, 0, 0, 0 };
    int[] nums2 = new int[] { 2, 5, 6 };

    cls6.Merge(nums1, 3, nums2, 3);
}
{
    Class6 cls6 = new Class6();
    int[] nums = new int[] { 2, 0, 2, 1, 1, 0 };
    cls6.SortColors(nums);
}
{
    Class7 class7 = new Class7();
    class7.MyPow(1.01, 77);
}
{
    Class7 class7 = new Class7();
    int[] nums = new int[] { 6, 3, 1, 9 };
    //MergeSort mergeSort = new MergeSort();
    //mergeSort.sort(nums);
    // class7.sortArray(nums);
    QuickSort qs = new QuickSort();
    qs.sort(nums);
}
{
    Class7 class7 = new Class7();
    char[][] board = new char[9][];

    for (int i = 0; i < board.Length; i++)
    {
        char[] row = new char[9];
        for (int j = 0; j < 9; j++)
        {
            row[j] = '.';
        }
        board[i] = row;
    }
    // class7.backtrack(board, 0, 0);
}

{
    Class7 class7 = new Class7();
    char[][] board = new char[9][];
    board[0] = new char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' };
    board[1] = new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
    board[2] = new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
    board[3] = new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
    board[4] = new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
    board[5] = new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };
    board[6] = new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
    board[7] = new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
    board[8] = new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };
    CommonHelper.Print2DMatrix(board);
    class7.IsValidSudoku(board);
}
{
    Class7 cls7 = new Class7();
    ListNode l1 = new ListNode(1);
    ListNode l2 = new ListNode(2);
    ListNode l3 = new ListNode(3);
    l1.next = l2;
    l2.next = l3;
    cls7.ReverseList1(l1);
}
{
    Class7 class7 = new Class7();
    ListNode l1 = new ListNode(1);
    ListNode l2 = new ListNode(2);
    ListNode l3 = new ListNode(3);
    l1.next = l2;
    l2.next = l3;
    class7.RemoveElements(l1, 1); ;
}

{
    var t1 = Math.Pow(2, 0);
    var t2 = Math.Pow(3, 40);
    var result = t1 * t2;
}
