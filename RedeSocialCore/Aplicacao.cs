using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RedeSocialCore
{
    public class Aplicacao
    {
        public Aplicacao(IBancoDeDados bancoDeDados)
        {
            BancoDeDados = bancoDeDados;
        }

        public IBancoDeDados BancoDeDados { get; }

        public Post GetPost(Guid id)
        {
            var post = BancoDeDados.Find(id);

            return post;
        }

        public Post CreatePost(CreatePost create)
        {
            var post = new Post();
            post.Id = Guid.NewGuid();
            post.Author = create.Author;
            post.Subject = create.Subject;
            post.CreatedAt = DateTime.Now;

            BancoDeDados.Save(post);

            return post;
        }

        public void DeletePost(Guid id)
        {
            BancoDeDados.Remove(id);
        }

        public List<Comment> GetComments(Guid postId, Guid commentId)
        {
            var list = new List<Comment>();

            if (commentId != default)
            {
                var comment = BancoDeDados.FindComment(commentId);
                list.Add(comment);
            }
            else
            {
                var comments = BancoDeDados.GetComments().Where(x => x.PostId == postId).ToList();
                list.AddRange(comments);
            }

            return list;
        }
    }

    public interface IBancoDeDados
    {
        void Save(Post post);
        void Remove(Guid id);
        Post Find(Guid id);
        Comment FindComment(Guid id);
        List<Comment> GetComments();
    }
}
