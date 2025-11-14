using FuturoDoTrabalho.Api.DTOs;
using FuturoDoTrabalho.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuturoDoTrabalho.Api.Controllers
{
    [ApiController]
    [Route("api/v1/trilhas")]
    public class TrilhasController : ControllerBase
    {
        private readonly ITrilhaService _trilhaService;

        public TrilhasController(ITrilhaService trilhaService)
        {
            _trilhaService = trilhaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TrilhaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var trilhas = await _trilhaService.GetAllAsync();
            return Ok(trilhas);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(TrilhaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var trilha = await _trilhaService.GetByIdAsync(id);
            if (trilha == null) return NotFound();
            return Ok(trilha);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TrilhaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TrilhaCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var created = await _trilhaService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(TrilhaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] TrilhaCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var updated = await _trilhaService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _trilhaService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
