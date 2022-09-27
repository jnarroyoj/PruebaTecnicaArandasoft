using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAranda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService categoriasService;

        public CategoriasController(ICategoriasService categoriasService)
        {
            this.categoriasService = categoriasService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DetailsCategoriaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void),StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateCategoria([FromBody] CreateCategoriaDto createCategoria)
        {
            try
            {
                var result = await categoriasService.CreateCategoriaAsync(createCategoria);

                return CreatedAtAction(nameof(ReadCategoriaAsync), new { id = result.Id }, result);
            }
            catch (DbUpdateException) {
                return Conflict();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailsCategoriaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadCategoriaAsync(Guid id)
        {
            try
            {
                var result = await categoriasService.ReadCategoriaAsync(id);

                return Ok(result);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DetailsCategoriaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllCategoriaAsync()
        {
            var result = await categoriasService.ReadAllCategoriaAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateCategoriaAsync(Guid id, [FromBody] UpdateCategoriaDto updateCategoriaDto)
        {
            if (id != updateCategoriaDto.Id) return BadRequest();

            try
            {
                await categoriasService.UpdateCategoriaAsync(updateCategoriaDto);
                return Accepted();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategoriaAsync(Guid id)
        {
            try
            {
                await categoriasService.DeleteCategoriaAsync(id);
                return NoContent();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
