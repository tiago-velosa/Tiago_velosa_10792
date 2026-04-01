using Microsoft.AspNetCore.Mvc;
using ApiAlunosProxy.Services;

namespace ApiAlunosProxy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly AlunosService _service;

        public AlunosController(AlunosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] object aluno)
        {
            var response = await _service.Create(aluno);
            return StatusCode((int)response.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] object aluno)
        {
            var response = await _service.Update(id, aluno);
            return StatusCode((int)response.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromHeader] string Role)
        {
            var isAdmin = Role == "Admin";
            var response = await _service.Delete(id, isAdmin);
            return StatusCode((int)response.StatusCode);
        }
    }
}
