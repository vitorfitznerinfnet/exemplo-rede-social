using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedeSocialCore.Data
{
    public class BancoDeDados : DbContext, IBancoDeDados
    {
        public BancoDeDados(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }

        public Post Find(Guid id)
        {
            return this.Post.Find(id);
        }

        public Comment FindComment(Guid id)
        {
            return this.Comment.Find(id);
        }

        public List<Comment> GetComments()
        {
            return this.Comment.ToList();
        }

        public void Remove(Guid id)
        {
            var post = this.Find(id);
            this.Post.Remove(post);
        }

        public void Save(Post post)
        {
            this.Post.Add(post);
            this.SaveChanges();
        }
    }
}
