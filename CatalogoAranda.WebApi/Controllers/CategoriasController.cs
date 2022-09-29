using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAranda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService categoriasService;

        public CategoriasController(ICategoriasService categoriasService)
        {
            this.categoriasService = categoriasService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DetailsCategoriaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DetalleError),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCategoriaAsync([FromBody] CreateCategoriaDto createCategoria)
        {
           
            var result = await categoriasService.CreateCategoriaAsync(createCategoria);

            return CreatedAtAction(nameof(ReadCategoriaAsync), new { id = result.Id }, result);
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailsCategoriaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ReadCategoriaAsync(Guid id)
        {
            var result = await categoriasService.ReadCategoriaAsync(id);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DetailsCategoriaDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> ReadAllCategoriaAsync()
        {
            var result = await categoriasService.ReadAllCategoriaAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateCategoriaAsync(Guid id, [FromBody] UpdateCategoriaDto updateCategoriaDto)
        {
            if (id != updateCategoriaDto.Id) return BadRequest();

            await categoriasService.UpdateCategoriaAsync(updateCategoriaDto);
            return Accepted();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategoriaAsync(Guid id)
        {
            await categoriasService.DeleteCategoriaAsync(id);
            return NoContent();
        }
    }
}
