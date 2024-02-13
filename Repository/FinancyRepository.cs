
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

        public async Task<IEnumerable<Financy>> ListarFinancies(string jwt)
        {
            var user = await jwtService.PegarUsuarioPorToken(jwt);
            List<Financy> financies = await _context.Financy.Where(f => f.IdUser == user.IdUser).ToListAsync();
            return financies;
        }

        public async Task<Financy> ListarFinancyPorId(int id,string jwt)
        {
            var user = await jwtService.PegarUsuarioPorToken(jwt);
            Financy financy = await _context.Financy.Where(f => f.id == id && f.IdUser == user.IdUser).FirstOrDefaultAsync();
            return financy;
        }

        public async Task<bool> RemoverFinancy(int id,string jwt)
        {
            var user = await jwtService.PegarUsuarioPorToken(jwt);
            Financy financy = await _context.Financy.Where(f => f.id == id && f.IdUser == user.IdUser).FirstOrDefaultAsync();
            if(financy == null) return false;
            _context.Remove(financy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<float> ListarValorAoTodo(string jwt){

            var user = await jwtService.PegarUsuarioPorToken(jwt);
            List<Financy> financies = await _context.Financy.Where(f => f.IdUser == user.IdUser).ToListAsync();
            float ValorTotal= 0;
            foreach(var i in financies){
               ValorTotal+= i.Valor; 
            }
            return ValorTotal;
        }

        public async Task<ShowFinancyDto> ListarMaiorFinancy(string jwt){

            var user = await jwtService.PegarUsuarioPorToken(jwt);
            List<Financy> financies = await _context.Financy.Where(f => f.IdUser == user.IdUser).ToListAsync();
            Financy financy = await _context.Financy.FirstOrDefaultAsync();
            float valor = 0;

            foreach(var i in financies){
                if(i.Valor >= valor){
                    financy = i;
                    valor = i.Valor;
                }
            }

            ShowFinancyDto financyDto = new ShowFinancyDto(financy.Nome,financy.Descricao,financy.Valor,financy.Data);
            return financyDto;
        }

        public async Task<ShowFinancyDto> ListarMenorFinancy(string jwt){

            var user = await jwtService.PegarUsuarioPorToken(jwt);
            List<Financy> financies = await _context.Financy.Where(f => f.IdUser == user.IdUser).ToListAsync();
            Financy financy = await _context.Financy.FirstOrDefaultAsync();
            float valor = financy.Valor;

            foreach(var i in financies){
                if(i.Valor <= valor){
                    financy = i;
                    valor = i.Valor;
                }
            }

            ShowFinancyDto financyDto = new ShowFinancyDto(financy.Nome,financy.Descricao,financy.Valor,financy.Data);
            return financyDto;
        }

        public async Task<List<ShowFinancyDto>> ListarDispesas(string jwt){

            var user = await jwtService.PegarUsuarioPorToken(jwt);
            List<Financy> financies = await _context.Financy.Where(f => f.IdUser == user.IdUser).ToListAsync();
            List<ShowFinancyDto> financiesDto = new List<ShowFinancyDto>();

            foreach(var i in financies){
                if(i.Valor < 0){
                    //instanciamos a record show financyDto para podemos adicionar ela na lista 
                    ShowFinancyDto showFinancyDto = new ShowFinancyDto(i.Nome,i.Descricao,i.Valor,i.Data);
                    financiesDto.Add(showFinancyDto);
                }
            }
        
            return financiesDto;

        }

        public async Task<List<ShowFinancyDto>> ListarReceitas(string jwt){
            var user = await jwtService.PegarUsuarioPorToken(jwt);
            List<Financy> financies = await _context.Financy.Where(f => f.IdUser == user.IdUser).ToListAsync();
            List<ShowFinancyDto> financiesDto = new List<ShowFinancyDto>();

            foreach(var i in financies){
                if(i.Valor > 0){
                    //instanciamos a record show financyDto para podemos adicionar ela na lista 
                    ShowFinancyDto showFinancyDto = new ShowFinancyDto(i.Nome,i.Descricao,i.Valor,i.Data);
                    financiesDto.Add(showFinancyDto);
                }
            }
            return financiesDto;

        }
    }
}