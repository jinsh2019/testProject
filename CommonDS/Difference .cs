namespace CDS
{
    public class Difference
    {
        private int[] diff;

        public Difference(int[] nums)
        {
            diff = new int[nums.Length];
            // 构造差分数组
            diff[0] = nums[0];
            for (int i = 1; i < nums.Length; i++)
                diff[i] = nums[i] - nums[i - 1];
        }

        /* 给闭区间 [i, j] 增加 val（可以是负数）*/
        public void increment(int i, int j, int val)
        {
            diff[i] += val;
            if (j + 1 < diff.Length) 
                diff[j + 1] -= val;
    
        }

        public int[] result()
        {
            int[] res = new int[diff.Length];
            // 根据差分数组构造结果数组
            res[0] = diff[0];
            for (int i = 1; i < diff.Length; i++) 
                res[i] = res[i - 1] + diff[i];
            return res;
        }
    }
}