using System;

namespace RedeSocialCore
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
