using Microsoft.AspNetCore.Mvc;
using WebFinancy.Data;
using WebFinancy.Model;
using WebFinancy.Repository;

namespace WebFinancy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancyBalanceController : ControllerBase
    {
        private readonly IFinancyRepository _financyRepository;
        
        public FinancyBalanceController(IFinancyRepository financyRepository){
            _financyRepository = financyRepository;
        }

        //pmostrar valor total de gastos
        [HttpGet("total")]
        public async Task<ActionResult<float>> ValorAoTodo(){
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);
            float ValorAoTodo = await  _financyRepository.ListarValorAoTodo(jwt);
            return Ok($"{ValorAoTodo:C}");
        }

        [HttpGet("maior")]
        public async Task<ActionResult<Financy>> MostrarFinancyComMaiorValor(){
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);
            var financy = await _financyRepository.ListarMaiorFinancy(jwt);
            return Ok(financy);
        }
        
        [HttpGet("menor")]
        public async Task<ActionResult<Financy>> MostrarFinancyComMenorValor(){
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);
            var financy = await _financyRepository.ListarMenorFinancy(jwt);
            return Ok(financy);
        }

        [HttpGet("dispesas")]
        public async Task<ActionResult<List<ShowFinancyDto>>> MostrarDispesas(){
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);
            List<ShowFinancyDto> financies = await _financyRepository.ListarDispesas(jwt);
            return Ok(financies);
        }

        [HttpGet("receitas")]
        public async Task<ActionResult<List<ShowFinancyDto>>> MostrarReceitas(){
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);
            List<ShowFinancyDto> financies = await _financyRepository.ListarReceitas(jwt);
            return Ok(financies);
        }
    }
}