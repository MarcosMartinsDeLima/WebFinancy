using Microsoft.EntityFrameworkCore;
using WebFinancy.Model;
using WebFinancy.Model.Context;
using WebFinancy.Services;

namespace WebFinancy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MysqlContext _context;
        private readonly JwtService _jwtService;

        public UserRepository(MysqlContext context,JwtService jwtService){
            _context = context;
            _jwtService = jwtService;
        }
        public async Task<User> AcharUserPorId(int id)
        {
            var user = await _context.User.Where(u => u.IdUser == id ).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> ApagarUser(int id)
        {
            var user = await _context.User.Where(u => u.IdUser == id ).FirstOrDefaultAsync();
            if(user == null) return false;
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> AtualizarUser(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> CriarUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}