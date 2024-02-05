using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
           float ValorAoTodo = await  _financyRepository.ListarValorAoTodo();
            return Ok($"{ValorAoTodo:C}");
        }

        [HttpGet("maior")]
        public async Task<ActionResult<Financy>> MostrarFinancyComMaiorValor(){
            var financy = await _financyRepository.ListarMaiorFinancy();
            return Ok(financy);
        }
    }
}