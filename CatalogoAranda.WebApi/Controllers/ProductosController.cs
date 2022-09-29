using CatalogoAranda.ApplicationCore.ApplicationExceptions;
using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;
using CatalogoAranda.ApplicationCore.Dtos.ProductosDtos;
using CatalogoAranda.ApplicationCore.Services;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;

namespace CatalogoAranda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService productosService;

        public ProductosController(IProductosService productosService)
        {
            this.productosService = productosService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DetailsProductoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateProductoAsync([FromBody] CreateProductoDto createProducto)
        {
            var result = await productosService.CreateProductoAsync(createProducto);

            return CreatedAtAction(nameof(ReadProductoAsync), new { id = result.Id }, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DetailsProductoDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> ReadProductosAsync(string? nombre, string? descripcion, string? categoria,
            bool? ordenarPorNombre, bool? ascendente, int Pagina = 1, int ElementosPorPagina = 20)
        {
            return Ok(
                await productosService.ReadPagedProductoAsync(nombre, descripcion,
                categoria, ascendente, ordenarPorNombre, Pagina, ElementosPorPagina)
                );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailsProductoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ReadProductoAsync(Guid id)
        {
            return Ok(await productosService.ReadProductoAsync(id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateProductoAsync(Guid id, [FromBody] UpdateProductoDto updateProductoDto)
        {
            if (id != updateProductoDto.Id) return BadRequest();

            await productosService.UpdateProductoAsync(updateProductoDto);
            return Accepted();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProductoAsync(Guid id)
        {
            await productosService.DeleteProductoAsync(id);
            return NoContent();
        }
    }
}
