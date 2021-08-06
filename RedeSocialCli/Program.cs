using RedeSocialApi.Controllers;
using RedeSocialCore;
using RedeSocialCore.Data;
using System;
using System.Collections.Generic;

namespace RedeSocialCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rede social de tela preta! rsrs");

            Console.WriteLine("1 - para cadastrar post");
            Console.WriteLine("2 - para buscar comentário");
            Console.WriteLine("3 - remover post");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CriarPost();
                    break;

                case "2":
                    BuscarComentario();
                    break;

                case "3":
                    RemoverPost();
                    break;
            }
        }

        private static void RemoverPost()
        {
            Console.WriteLine("Entre com o identificador do post:");
            
            var postid = Guid.Parse(Console.ReadLine());

            var app = new Aplicacao(null);

            app.DeletePost(postid);
        }

        private static void BuscarComentario()
        {
            Console.WriteLine("Entre com o identificador do post:");
            var postId = Guid.Parse(Console.ReadLine());

            var app = new Aplicacao(null);

            List<Comment> comentarios = app.GetComments(default, default);

            foreach (var com in comentarios)
            {
                Console.WriteLine("text:" + com.Text);
                Console.WriteLine("autor:" + com.Author);
                Console.WriteLine("");
            }
        }

        private static void CriarPost()
        {
            var createPost = new CreatePost();

            Console.WriteLine("Entre com o titulo:");
            createPost.Subject = Console.ReadLine();

            Console.WriteLine("Entre com o nome do autor do post:");
            createPost.Author = Console.ReadLine();

            BancoDeDados bd = null;

            var app = new Aplicacao(bd);

            app.CreatePost(createPost);
        }
    }
}
