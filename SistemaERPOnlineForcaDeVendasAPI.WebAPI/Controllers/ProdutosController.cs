using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;

namespace SistemaERPOnlineForcaDeVendasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ProdutoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ProdutoResponse>>> GetAll(CancellationToken ct)
        {
            var list = await _service.GetAllAsync(ct);
            return Ok(list);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProdutoResponse>> GetById(int Id, CancellationToken ct)
        {
            var projeto = await _service.GetByIdAsync(Id, ct);
            if (projeto is null) return NotFound();
            return Ok(projeto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponse>> Create([FromBody] ProdutoCreateRequest request, CancellationToken ct)
        {
            var created = await _service.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { Id = created.Id }, created);
        }

        [HttpPut("{Id:int}")]
        [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponse>> Update(int Id, [FromBody] ProdutoUpdateRequest request, CancellationToken ct)
        {
            var updated = await _service.UpdateAsync(Id, request, ct);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int Id, CancellationToken ct)
        {
            var deleted = await _service.DeleteAsync(Id, ct);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}

