using System;

namespace RedeSocialCore
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Removed { get; set; }
    }
}
