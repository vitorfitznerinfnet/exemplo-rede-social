using Microsoft.AspNetCore.Mvc;

namespace RedeSocialApi.Controllers
{
    [Route("api/pessoas")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public Pessoa Get()
        {
            var pessoa = new Pessoa();
            pessoa.Nome = "vitor";
            pessoa.Email = "vitor@teste.com";

            return pessoa;
        }
    }

    public class Pessoa
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}