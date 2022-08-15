namespace August.day9
{
    // 正则表达式 使用dp来做
    internal class Reg
    {
        // 备忘录，-1 代表还未计算，0 代表 false，1 代表 true
        List<List<int>> memo = null;
        public bool IsMatch(string s, string p)
        {
            if (p.Length == 0)
            {
                return p.Length == 0;
            }

            string pp = remove_adj_star(p);
            int m = s.Length, n = pp.Length;

            memo = new List<List<int>>();
            for (int i = 0; i < m; i++)
            {
                List<int> item = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    item.Add(-1);
                }
                memo.Add(item);
            }

            return dp(s, 0, pp, 0);
        }

        string remove_adj_star(string p)
        {
            if (p.Length == 0)
                return "";
            List<char> list = new List<char>();
            list.Add(p[0]);
            for (int i = 1; i < p.Length; i++)
            {
                if (p[i] == '*' && p[i - 1] == '*')
                {
                    continue;
                }
                list.Add(p[i]);
            }

            return new String(list.ToArray());
        }
        // 定义：判断 s[i..] 是否能被 p[j..] 匹配
        bool dp(string s, int i, string p, int j)
        {
            // base Case 1
            if (j == p.Length && i == s.Length)
                return true;
            // base Case 2
            if (i == s.Length)
            {
                for (int k = j; k < p.Length; k++)
                {
                    if (p[k] != '*')
                        return false;
                }
                return true;
            }
            // base Case 3
            if (j == p.Length)
                return false;
            // 剪枝
            if (memo[i][j] != -1)
            {
                return memo[i][j] == 1 ? true : false;
            }
            bool res = false;
            if (s[i] == p[j] || p[j] == '?')
            {
                res = dp(s, i + 1, p, j + 1);
            }
            else if (p[j] == '*')
            {
                // s[i] 和 p[j] 不匹配，但 p[j] 是通配符 *
                // 可以匹配 0 个或多个 s 中的字符，
                // 只要有一种情况能够完成匹配即可
                res = dp(s, i + 1, p, j) // n个
                   || dp(s, i, p, j + 1);// 0个
            }

            memo[i][j] = (res == true ? 1 : 0);

            return res;
        }

    }
}
