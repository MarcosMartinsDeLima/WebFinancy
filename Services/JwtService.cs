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
                    new Claim("UserId",user.IdUser.ToString()),
                    new Claim(ClaimTypes.Name,user.Nome)
                })

            };

            if (user == null || user.IdUser <= 0)
                {
                    throw new ArgumentException("O ID do usuário é inválido ou não está definido.");
                }
                
            //gerando token
            var token = handler.CreateToken(tokenDescriptor);
            //gera uma string do token
            return handler.WriteToken(token);
        }
        
        public async Task<User> PegarUsuarioPorToken(string token){
            //instanciando o HandlerJwt
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("secretosecretosecretoseraqueagoravaiesperoquesim@34");
            var Token = handler.ValidateToken(token, new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
            },out var validatedToken);
            
            var UserId = ((JwtSecurityToken)validatedToken).Claims.FirstOrDefault(c => c.Type == "UserId") ?? throw new ArgumentException("Token não contém a reivindicação do ID do usuário.");
            int.TryParse(UserId.Value, out int userId);
            var user = await  _context.User.Where(f => f.IdUser == userId).FirstOrDefaultAsync();

            return user;
        }
        
    }
}