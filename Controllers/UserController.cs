using Microsoft.AspNetCore.Mvc;
using WebFinancy.Data;
using WebFinancy.Model;
using WebFinancy.Repository;
using WebFinancy.Services;

namespace WebFinancy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly HttpClient _httpClient;

        public UserController(IUserRepository userRepository, JwtService jwtService,HttpClient httpClient){
            _userRepository = userRepository;
            _jwtService = jwtService;
            _httpClient = httpClient;
        }
        [HttpPost("criar")]
        public async Task<ActionResult<User>> CriarUser(User user){
            var User = await _userRepository.CriarUser(user);
            var token = _jwtService.GerarToken(user);
            //implementar token
            //implementar cookie
            Response.Headers.Authorization.Append(token);
            return User;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginDto loginDto){
            //validação se veio email e senha
            if(loginDto.email == null) return BadRequest("Email é obrigatório");
            if(loginDto.senha == null) return BadRequest("Senha é obrigatória");

            //validação se a conta já existe
            var conta = _userRepository.AcharUserPorEmail(loginDto.email);
            if(conta == null) return NotFound("Não existe uma conta com esse nome!");
            
            //validação se a senha bate
            if(conta.Result.Senha != loginDto.senha)  return UnprocessableEntity("Senha invalida");
           
            var token = _jwtService.GerarToken(conta.Result);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bareare",token);
            return Ok(token);
        }

        [HttpPut]
        public async Task<ActionResult<User>> AtualizarUser(User user){
            var User = await _userRepository.AtualizarUser(user);
            return User;
        }

        [HttpDelete("id")]
        public async Task<ActionResult<bool>> DeletarUser([FromBody] int id){
            var User = await _userRepository.ApagarUser(id);
            
            if(!User) return BadRequest("Não foi possivel apagar User!");

            return NoContent();
        }
    }
}