using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFinancy.Data;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<Financy>>> ListarFinancies(){
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);
            var Financy = await _financyRepository.ListarFinancies(jwt);
            return Ok(Financy);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Financy>> ListarFinancyPorId(int id){
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);
            var financy = await _financyRepository.ListarFinancyPorId(id,jwt);
            if(financy == null) return NotFound("Não foi encontrado um controle financeiro!");
            return Ok(financy);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Financy>> CriarFinancy([FromBody]FinancyRecord financy){
            //validações
            if(financy.Nome == null) return BadRequest("O nome do controle financeiro é obrigatório!");
            if(financy.Descricao == null) return BadRequest("A Descrição do controle financeiro é obrigatório!");
            if(financy.Valor == null) return BadRequest("O valor do controle financeiro é obrigatório!");
            if(financy.Data == null) return BadRequest("A data do controle financeiro é obrigatório!");
            
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);

            await _financyRepository.CriarFinancy(financy,jwt);
            Response.StatusCode = 201;
           
            return new JsonResult(JsonSerializer.Serialize(financy));
        }

        [HttpPut]
        public async Task<ActionResult<Financy>> AtualizarFinancy([FromBody]Financy financy){
            await _financyRepository.AtualizarFinancy(financy);
            return Ok(financy);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarFinancy(int id){
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ",string.Empty);
            var status = await _financyRepository.RemoverFinancy(id,jwt);
            if(!status) return BadRequest("Não foi possivel apagar esse financy");
            return Ok("Financy apagada");
        }
    }
}