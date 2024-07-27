using Microsoft.IdentityModel.Tokens;
using Persistencia.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyOrderAPI.Service
{
    public class Token
    {
        public static string TokenSecret = "b4CGYTkIMRBl3FoY2oChde5GfCfgrlA4prWdlTAvAKQ";

        public string GerarToken(Usuario usuario)
        {
            var salt = Encoding.ASCII.GetBytes(TokenSecret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("idUsuario", usuario.Id.ToString()),
                    new Claim("nomeUsuario", usuario.Nome)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(salt), SecurityAlgorithms.Sha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
