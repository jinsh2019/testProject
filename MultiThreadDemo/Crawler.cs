using System.Threading;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace MultiThreadDemo
{
    internal class Crawler
    {
        Hashtable htSyn = Hashtable.Synchronized(new Hashtable());            // 线程安全的Hashtable
        ConcurrentBag<string> list = new ConcurrentBag<string>();             // 线程安全的List
        ConcurrentQueue<string> requestQueue = new ConcurrentQueue<string>(); // 线程安全的Queue
        HtmlParser html; // 全局接口
        int running = 0; // 正在运行的数量，按道理这个也要改为线程安全，但是记得好像int是原子操作，不需要加锁，望大家解答
        string hostUrl;  // 全局缓存的url
        Regex r;         // 全局的编译过的正则表达式
        string getHost(string url)
        { // 方法： 返回host地址
            return r.Match(url).Groups[2].Value;
        }

        bool parse(string url)
        { // 判断地址的host是否和全局储存的一致
            return getHost(url) == hostUrl;
        }

        async void request(string url)
        { // 核心方法
            running++;                   // 函数入点，先对running自增
            await Task.Delay(1);         // 欺骗编译器这是一个async方法
            var urls = html.GetUrls(url);// 请求url
            foreach (string u in urls)
            {
                if (htSyn[u] != null)
                {
                    continue;            // 已经存在 放弃该url抓取
                }
                htSyn[u] = true;         // 否则加入hashtable
                if (parse(u))
                {          // 然后判断是否和host一致，一致的话推入List和Queue
                    list.Add(u);
                    requestQueue.Enqueue(u);
                }
            }
            running--;                   // 出点，running自减
        }

        public IList<string> Crawl(string startUrl, HtmlParser htmlParser)
        {
            html = htmlParser; // 全局缓存
            string Pattern = @"^(http|https|ftp)\://([a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3})(:[a-zA-Z0-9]*)?/?(.*?)$"; // 对url的正则表达式匹配
            r = new Regex(Pattern);      // 编译正则表达式
            hostUrl = getHost(startUrl); // 全局缓存host

            list.Add(startUrl);    // 将startUrl推入答案
            htSyn[startUrl] = true;// 加入hashtable
            request(startUrl);     // 执行第一次请求
            while (running > 0 || !requestQueue.IsEmpty)
            { // 主循环
                if (requestQueue.IsEmpty)
                {
                    Thread.Sleep(1); // 如果为空，阻塞休眠1ms，防止占用资源
                    continue;
                }
                string front;
                if (requestQueue.TryDequeue(out front))
                { // 尝试提取，线程安全的Queue只有TryDequeue
                    request(front);  // 请求网址
                }
            }
            return list.ToList();    // 将线程安全的List转回普通的List
        }
    }

    class HtmlParser
    {
        public List<string> GetUrls(string url)
        {
            throw new NotImplementedException();
        }
    }
}
