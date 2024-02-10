
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebFinancy.Data;
using WebFinancy.Model;
using WebFinancy.Model.Context;
using WebFinancy.Services;

namespace WebFinancy.Repository
{
    public class FinancyRepository : IFinancyRepository
    {
        private readonly MysqlContext _context;
        private readonly IMapper _mapper;
        private readonly JwtService jwtService;

        public FinancyRepository(MysqlContext context,IMapper mapper,JwtService jwtServices){
            _context = context;
            _mapper = mapper;
            jwtService = jwtServices;
        }

        public async Task<Financy> AtualizarFinancy(Financy financy)
        {
            _context.Update(financy);
            await _context.SaveChangesAsync();
            return financy;
        }

        public async Task<Financy> CriarFinancy(FinancyRecord financyDto,string jwt)
        {
            var user = await jwtService.PegarUsuarioPorToken(jwt);
            Financy financy = _mapper.Map<Financy>(financyDto);
            financy.User = user;
            financy.IdUser = user.IdUser;
            financy.User.financies = new List<Financy>();
            _context.Add(financy);
            await _context.SaveChangesAsync();
            return financy;
        }

        public async Task<IEnumerable<Financy>> ListarFinancies()
        {
           List<Financy> financies = await _context.Financy.ToListAsync();
           return financies;
        }

        public async Task<Financy> ListarFinancyPorId(int id)
        {
            Financy financy = await _context.Financy.Where(f => f.id == id).FirstOrDefaultAsync();
            return financy;
        }

        public async Task<bool> RemoverFinancy(int id)
        {
            Financy financy = await _context.Financy.Where(f => f.id == id).FirstOrDefaultAsync();
            if(financy == null) return false;
            _context.Remove(financy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<float> ListarValorAoTodo(){
            List<Financy> financies = await _context.Financy.ToListAsync();
            float ValorTotal= 0;
            foreach(var i in financies){
               ValorTotal+= i.Valor; 
            }
            return ValorTotal;
        }

        public async Task<Financy> ListarMaiorFinancy(){
            List<Financy> financies = await _context.Financy.ToListAsync();
            Financy financy = await _context.Financy.FirstOrDefaultAsync();
            float valor = 0;
            foreach(var i in financies){
                if(i.Valor >= valor){
                    financy = i;
                    valor = i.Valor;
                }
            }
            return financy;
        }
    }
}