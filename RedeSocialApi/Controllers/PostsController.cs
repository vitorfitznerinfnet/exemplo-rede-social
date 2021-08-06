using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using RedeSocialCore;
using static System.String;
using static System.Guid;
using RedeSocialCore.Data;

namespace RedeSocialApi.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public BancoDeDados BancoDeDados { get; }

        public PostsController(BancoDeDados bancoDeDados)
        {
            BancoDeDados = bancoDeDados;
        }

        [HttpGet]
        public ActionResult Get( [FromQuery] string author)
        {
            if (IsNullOrWhiteSpace(author))
            {
                return Ok(BancoDeDados.Post.ToList());
            }

            return Ok(BancoDeDados.Post.Where(x => x.Author == author).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] Guid id)
        {
            var app = new Aplicacao(BancoDeDados);

            var post = app.GetPost(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreatePost create)
        {
            var app = new Aplicacao(BancoDeDados);

            var post = app.CreatePost(create);

            return Created("api/posts", post);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]Guid id)
        {
            var app = new Aplicacao(BancoDeDados);
            
            app.DeletePost(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, UpdatePost update)
        {
            var post = BancoDeDados.Post.Find(id);

            post.Author = update.Author;
            post.UpdatedAt = DateTime.UtcNow;

            return Ok(post);
        }

        [HttpPost("{id}/comments")]
        public ActionResult PostComment([FromRoute]Guid id, [FromBody]CreateComment create)
        {
            var comment = new Comment();
            comment.Id = NewGuid();
            comment.PostId = id;
            comment.Author = create.Author;
            comment.Text = create.Text;
            comment.CreatedAt = DateTime.UtcNow;

            BancoDeDados.Comment.Add(comment);
            BancoDeDados.SaveChanges();

            return Created("", comment);
        }

        [HttpGet("{id}/comments")]
        public ActionResult GetComments([FromRoute]Guid id, [FromQuery(Name = "id")] Guid commentId)
        {
            var app = new Aplicacao(BancoDeDados);

            var comments = app.GetComments(id, commentId);

            return Ok(comments);
        }

        [HttpGet("{id}/comments/{commentId}")]
        public ActionResult GetComments2([FromRoute] Guid id, [FromRoute] Guid commentId)
        {
            var comment = BancoDeDados.Comment.FirstOrDefault(x => x.PostId == id && x.Id == commentId);

            return Ok(comment);
        }
    }
}
