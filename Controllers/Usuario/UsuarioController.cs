using Microsoft.AspNetCore.Mvc;
using Persistencia.Models;
using EasyOrderAPI.ViewModel;
using Microsoft.AspNetCore.Cors;
using Persistencia.Service;
using System.Security.Cryptography;

namespace EasyOrderAPI.Controllers.Usuario
{
    [ApiController]
    [EnableCors]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordHash _passwordHash = new PasswordHash(SHA512.Create());

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException();
        }


        // TODO: Será necessário fazer uma verificação se o usuário é existente quando for fazer um novo registro, caso for, será retornado um ERRO.
        [HttpPost]
        [Route("api/registrarUsuario")]
        public IActionResult RegistrarUsuario(UsuarioViewModel usuarioView)
        {
            // TODO: Será aplicado aqui o conceito de Service;

            string passwordEncrypted = _passwordHash.CriptografarSenha(usuarioView.SenhaHash);
            var usuario = new Persistencia.Models.Usuario(usuarioView.Nome, usuarioView.Email, usuarioView.DataNascimento, passwordEncrypted);

            bool usuarioAdicionado = _usuarioRepository.Add(usuario);

            if (!usuarioAdicionado)
                return BadRequest("Já existe um usuário com esse email cadastrado.");

            return Ok();
        }

        [HttpGet]
        [Route("api/LoginUsuario")]
        public IActionResult Login(string email, string senhaString)
        {
            // TODO: Será aplicado aqui o conceito de Service;
            string passwordEncrypted = _passwordHash.CriptografarSenha(senhaString);


            // TODO: Colocar uma maneira de criar um Object Generic para passar os dados;

            bool usuarioLogado = _usuarioRepository.Logar(email, senhaString);
            // TODO: Entender a lógica que será utilizada aqui para fazer o Login, será necessário utilizar o repository?
            // Talvez não seja necessário, pois o Repository é utilizado apenas para fazer um CRUD no banco de dados;

            return Ok();
        }

        [HttpGet]
        [Route("api/pegarListaUsuario")]
        public IActionResult Get()
        {
            var usuario = _usuarioRepository.Get();

            return Ok(usuario);
        }
    }
}
