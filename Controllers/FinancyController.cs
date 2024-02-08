using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebFinancy.Model;
using WebFinancy.Repository;
using WebFinancy.Services;

namespace WebFinancy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancyController : ControllerBase
    {
        private IFinancyRepository _financyRepository;
        private JwtService jwtService;

        public FinancyController(IFinancyRepository financyRepository,JwtService service){
            _financyRepository = financyRepository;
            jwtService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Financy>>> ListarFinancies(){
            var Financy = await _financyRepository.ListarFinancies();
            return Ok(Financy);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Financy>> ListarFinancyPorId(int id){
            var financy = await _financyRepository.ListarFinancyPorId(id);
            if(financy == null) return NotFound("Não foi encontrado um controle financeiro!");
            return Ok(financy);
        }

        [HttpPost]
        public async Task<ActionResult<Financy>> CriarFinancy([FromBody]Financy financy){
            //validações
            if(financy.Nome == null) return BadRequest("O nome do controle financeiro é obrigatório!");
            if(financy.Descricao == null) return BadRequest("A Descrição do controle financeiro é obrigatório!");
            if(financy.Valor == null) return BadRequest("O valor do controle financeiro é obrigatório!");
            if(financy.Data == null) return BadRequest("A data do controle financeiro é obrigatório!");
            
            await _financyRepository.CriarFinancy(financy);
            Response.StatusCode = 201;
           
            return Ok(financy);
            //return new JsonResult(JsonSerializer.Serialize(financy));
        }

        [HttpPut]
        public async Task<ActionResult<Financy>> AtualizarFinancy([FromBody]Financy financy){
            await _financyRepository.AtualizarFinancy(financy);
            return Ok(financy);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarFinancy(int id){
            var status = await _financyRepository.RemoverFinancy(id);
            if(!status) return BadRequest("Não foi possivel apagar esse financy");
            return Ok("Financy apagada");
        }
    }
}