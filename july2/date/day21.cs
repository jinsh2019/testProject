using System.Collections;

namespace july2.date
{
    internal static class day21
    {

        public static string LongestPalindrome(string s)
        {
            string res = "";
            for (int i = 0; i < s.Length; i++)
            {
                string s1 = palindrome(s, i, i); // 不停返回是palindrome的字符串
                string s2 = palindrome(s, i, i + 1);
                res = res.Length > s1.Length ? res : s1;
                res = res.Length > s2.Length ? res : s2;
            }
            return res;
        }
        public static string palindrome(string s, int l, int r)
        {
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                l--;
                r++;
            }
            return s.Substring(l + 1, r - l - 1);

        }


        // 记录所有合法的括号组合
        static List<string> res = new List<string>();
        // 回溯过程中的路径
        static LinkedList<char> track = new LinkedList<char>();
        public static IList<string> GenerateParenthesis(int n)
        {
            if (n == 0)
                return new List<string>();

            backtrack(n, n);
            return res;
        }
        // left 左括号数， right 有括号数
        static void backtrack(int left, int right)
        {

            // base Case
            if (right < left) return;
            if (left < 0 || right < 0) return;

            if (left == 0 && right == 0)
            {
                res.Add(new string(track.ToArray()));
                return;
            }

            track.AddLast('(');
            backtrack(left - 1, right);
            track.RemoveLast();

            track.AddLast(')');
            backtrack(left, right - 1);
            track.RemoveLast();
        }
    }
}
