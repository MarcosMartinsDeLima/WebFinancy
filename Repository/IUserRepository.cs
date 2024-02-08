using WebFinancy.Model;

namespace WebFinancy.Repository
{
    public interface IUserRepository
    {
        Task<User> AcharUserPorId(int id);
        Task<User> CriarUser(User user);
        Task<User> AtualizarUser(User user);
        Task<bool> ApagarUser(int id);
    }
}