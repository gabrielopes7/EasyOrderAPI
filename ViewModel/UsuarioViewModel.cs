using System.Net.Mail;

namespace EasyOrderAPI.ViewModel
{
    public class UsuarioViewModel
    {
        public string Nome {  get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string SenhaHash { get; set; }
    }
}
