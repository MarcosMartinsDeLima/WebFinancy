using WebFinancy.Model;

namespace WebFinancy.Repository
{
    public interface IFinancyRepository
    {
        Task<IEnumerable<Financy>> ListarFinancies();
        Task<Financy> ListarFinancyPorId(int id);
        Task<Financy> CriarFinancy(Financy financy);
        Task<Financy> AtualizarFinancy(Financy financy);
        Task<bool> RemoverFinancy(int id);
        Task<float> ListarValorAoTodo();
        Task<Financy> ListarMaiorFinancy();

    }
}