using WebFinancy.Data;
using WebFinancy.Model;

namespace WebFinancy.Repository
{
    public interface IFinancyRepository
    {
        Task<IEnumerable<Financy>> ListarFinancies();
        Task<Financy> ListarFinancyPorId(int id);
        Task<Financy> CriarFinancy(FinancyRecord financy,string jwt);
        Task<Financy> AtualizarFinancy(Financy financy);
        Task<bool> RemoverFinancy(int id);
        Task<float> ListarValorAoTodo();
        Task<Financy> ListarMaiorFinancy();

    }
}