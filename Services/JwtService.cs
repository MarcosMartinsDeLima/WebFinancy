using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebFinancy.Model;
using WebFinancy.Model.Context;

namespace WebFinancy.Services
{
    public class JwtService
    {

        private readonly MysqlContext _context;

        public JwtService(MysqlContext context){
            _context = context;
        }
        public string GerarToken(User user){
            // criando uma instancia do jwtSecurityHandler
            var handler = new JwtSecurityTokenHandler();

            //convertendo a chave para um array de bites
            var key = Encoding.ASCII.GetBytes("secretosecretosecretoseraqueagoravaiesperoquesim@34");
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]{
                    new Claim("UserId",user.IdUser.ToString())
                })

            };

            //gerando token
            var token = handler.CreateToken(tokenDescriptor);
            //gera uma string do token
            return handler.WriteToken(token);
        }

        public async Task<Financy> PegarUsuarioPorToken(string token){
            var handler = new JwtSecurityTokenHandler();
            var Token = handler.ReadToken(token);
            var financy = await  _context.Financy.Where(f => f.id == Convert.ToInt16(Token.Id)).FirstOrDefaultAsync();
            return financy;
        }
        
    }
}