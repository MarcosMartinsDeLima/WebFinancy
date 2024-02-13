using WebFinancy.Data;
using WebFinancy.Model;

namespace WebFinancy.Repository
{
    public interface IFinancyRepository
    {
        Task<IEnumerable<Financy>> ListarFinancies(string jwt);
        Task<Financy> ListarFinancyPorId(int id,string jwt);
        Task<Financy> CriarFinancy(FinancyRecord financy,string jwt);
        Task<Financy> AtualizarFinancy(Financy financy);
        Task<bool> RemoverFinancy(int id,string jwt);
        Task<float> ListarValorAoTodo(string jwt);
        Task<ShowFinancyDto> ListarMaiorFinancy(string jwt);
        Task<ShowFinancyDto> ListarMenorFinancy(string jwt);
        Task<List<ShowFinancyDto>> ListarDispesas(string jwt);
        Task<List<ShowFinancyDto>> ListarReceitas(string jwt);

    }
}