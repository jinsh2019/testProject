using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.algorithms.day4
{
    // subset,permutation,combination
    public class MySet
    {
        IList<IList<int>> res = new List<IList<int>>();
        List<int> track = new List<int>();

        public IList<IList<int>> Subsets(int[] nums)
        {
            backtrack(nums, 0);
            return res;
        }
        private void backtrack(int[] nums, int k)
        {

            if (k == nums.Length)
            {
                res.Add(new List<int>(track));
                return; 
            }

            backtrack(nums, k + 1);

            track.Add(nums[k]);
            backtrack(nums, k + 1); // trace 狗
            track.RemoveAt(track.Count - 1); // backing
        }
    }
}
