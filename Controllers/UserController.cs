using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserRepository userRepository, JwtService jwtService){
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        [HttpPost]
        public async Task<ActionResult<User>> CriarUser(User user){
            var User = await _userRepository.CriarUser(user);
            var token = _jwtService.GerarToken(user);
            //implementar token
            //implementar cookie
            Response.Headers.Authorization.Append(token);
            return User;
        }
    }
}