using Microsoft.AspNetCore.Mvc;
using Persistencia.Models;
using EasyOrderAPI.ViewModel;
using Microsoft.AspNetCore.Cors;
using Persistencia.Service;
using System.Security.Cryptography;

namespace EasyOrderAPI.Controllers
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
            string passwordEncrypted = _passwordHash.CriptografarSenha(usuarioView.SenhaHash);

            var usuario = new Usuario(usuarioView.Nome, usuarioView.Email, usuarioView.DataNascimento, passwordEncrypted);
            _usuarioRepository.Add(usuario);

            return Ok();
        }

        [HttpGet]
        [Route("api/pegarListaUsuario")]
        public IActionResult Get()
        {
            var usuario = _usuarioRepository.Get();

            return Ok(usuario);
        }

        // TODO: Próximo passo será criar um método para que faça o Login do Usuário e nessa será preciso Verificar a senha quando for logar;
    }
}
