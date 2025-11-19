using FuturoDoTrabalho.Api.DTOs;
using FuturoDoTrabalho.Api.Exceptions;
using FuturoDoTrabalho.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuturoDoTrabalho.Api.Controllers
{
    [ApiController]
    [Route("api/v1/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var created = await _usuarioService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] UsuarioCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var updated = await _usuarioService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _usuarioService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        [HttpGet("{id:long}/trilhas")]
        public async Task<IActionResult> GetTrilhasDoUsuario(long id)
        {
            var trilhas = await _usuarioService.GetTrilhasDoUsuarioAsync(id);
            return Ok(trilhas);
        }

        [HttpPost("{usuarioId:long}/trilhas/{trilhaId:long}")]
        public async Task<IActionResult> Matricular(long usuarioId, long trilhaId)
        {
            try
            {
                await _usuarioService.MatricularAsync(usuarioId, trilhaId);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("{usuarioId:long}/trilhas/{trilhaId:long}")]
        public async Task<IActionResult> RemoverTrilhaDoUsuario(long usuarioId, long trilhaId)
        {
            try
            {
                await _usuarioService.RemoverTrilhaAsync(usuarioId, trilhaId);
                return NoContent(); 
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
