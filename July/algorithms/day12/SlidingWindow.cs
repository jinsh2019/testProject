namespace July.algorithms.day12
{
    internal class SlidingWindow
    {
        Dictionary<char, int> need = new Dictionary<char, int>();
        Dictionary<char, int> window = new Dictionary<char, int>();
        public string MinWindow(string s, string t)
        {
            foreach (char c in t)
            {
                if (need.ContainsKey(c)) need[c]++;
                else need.Add(c, 1);
            }

            int left = 0, right = 0;// 左右指针
            int valid = 0;// is valid for logic
            int start = 0, len = int.MaxValue; // subString start, len
            // [l,r)
            while (right < s.Length)
            {
                char c = s[right];
                right++;
                if (need.ContainsKey(c) && need[c] > 0)
                {
                    if (window.ContainsKey(c)) window[c]++;
                    else window.Add(c, 1);

                    if (window[c] == need[c]) valid++;
                }

                // shrink window till invalid
                while (valid == need.Count)
                {
                    // update the start/len for the shortest len
                    if ((right - left) < len)
                    {
                        start = left;
                        len = right - left;
                    }

                    char d = s[left];
                    left++;
                    // 包含才进行操作，不包含不操作 
                    if (need.ContainsKey(d) && need[d] > 0)
                    {
                        if (window[d] == need[d]) valid--;
                        window[d]--;
                    }
                }
            }
            return len == int.MaxValue ? "" : s.Substring(start, len);
        }

        int res = 0;
        public int LengthOfLongestSubstring(string s) // 3. 无重复字符的最长子串
        {
            int left = 0, right = 0;
            while (right < s.Length) {
                char c = s[right];
                if (window.ContainsKey(c)) window[c]++;
                else window.Add(c, 1);
                right++;

                //shrink
                while (window[c]> 1)
                {
                    char d = s[left];
                    left++;
                    window[d]--;
                }
                res = Math.Max(res, right - left);
            }
            return res;
        }
    }
}
