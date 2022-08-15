namespace August.day9
{
    public class Twitter
    {
        private static int timestamp = 0;

        private Dictionary<int, User> userMap = new Dictionary<int, User>();
        public Twitter()
        {

        }

        public void PostTweet(int userId, int tweetId)
        {
            if (!userMap.ContainsKey(userId))
            {
                userMap.Add(userId, new User(userId));
            }
            User u = userMap[userId];
            u.Post(tweetId);
        }

        public IList<int> GetNewsFeed(int userId)
        {
            List<int> res = new List<int>();

            if (!userMap.ContainsKey(userId))
                return res;
            HashSet<int> users = userMap[userId].followed;
            IComparer<int> comperator = new MyTwitComparator();
            PriorityQueue<Tweet, int> pq = new PriorityQueue<Tweet, int>(users.Count, comperator);

            // 根据user建树
            foreach (int id in users)
            {
                Tweet twt = userMap[id].head;
                if (twt == null)
                    continue;
                pq.Enqueue(twt, twt.time);
            }

            // 生成结果
            while (pq.Count != 0)
            {
                if (res.Count == 10)
                    break;
                Tweet twt = pq.Dequeue(); // 取最大twt
                res.Add(twt.id);
                if (twt.next != null)
                    pq.Enqueue(twt.next, twt.next.time);
            }
            return res;
        }

        public void Follow(int followerId, int followeeId)
        {
            // 若 follower 不存在，则新建
            if (!userMap.ContainsKey(followerId))
            {
                User u = new User(followerId);
                userMap.Add(followerId, u);
            }
            // 若 followee 不存在，则新建
            if (!userMap.ContainsKey(followeeId))
            {
                User u = new User(followeeId);
                userMap.Add(followeeId, u);
            }
            userMap[followerId].Follow(followeeId);
        }

        public void Unfollow(int followerId, int followeeId)
        {
            if (userMap.ContainsKey(followerId))
            {
                User flower = userMap[followerId];
                flower.Unfollow(followeeId);
            }
        }

        private class Tweet
        {
            public int id { get; set; }
            public int time { get; set; }
            public Tweet next { get; set; }

            public Tweet(int id, int time)
            {
                this.id = id;
                this.time = time;
                this.next = null;
            }
        }

        private class User
        {
            private int id;
            public HashSet<int> followed;

            public Tweet head;

            public User(int userId)
            {
                followed = new HashSet<int>();
                this.id = userId;
                this.head = null;
                // 关注一下自己
                followed.Add(id);
            }

            public void Follow(int userId)
            {
                followed.Add(userId);
            }

            public void Unfollow(int userId)
            {
                if (userId != this.id)
                {
                    followed.Remove(userId);
                }
            }

            public void Post(int tweetId)
            {
                Tweet twt = new Tweet(tweetId, timestamp);
                timestamp++;

                twt.next = head;
                head = twt;
            }
        }
        private class MyTwitComparator : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y.CompareTo(x);
            }
        }
    }
}
