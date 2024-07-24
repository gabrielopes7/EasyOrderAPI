using Microsoft.AspNetCore.Mvc;
using Persistencia.Models;
using EasyOrderAPI.ViewModel;
using Microsoft.AspNetCore.Cors;

namespace EasyOrderAPI.Controllers
{
    [ApiController]
    [EnableCors]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [Route("api/registrarUsuario")]
        public IActionResult RegistrarUsuario(UsuarioViewModel usuarioView)
        {
            var usuario = new Usuario(usuarioView.Nome, usuarioView.Email, usuarioView.DataNascimento, usuarioView.Senha);
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
    }
}
