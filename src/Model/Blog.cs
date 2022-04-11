using System;
using System.Collections.Generic;
using EFCoreDemo;

namespace Model
{
    public class Blog:IUpdatedable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }

    }
}
