namespace MonopolyDemo
{
    // 路径节点
    internal class PathNode
    {
        public string Type { get; set; }
        public PathNode _next { get; set; }
        public IStrategy strategy { get; set; }
    }
}
