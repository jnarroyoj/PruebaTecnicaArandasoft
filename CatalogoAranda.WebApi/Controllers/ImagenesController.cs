using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;
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
    public class ImagenesController : ControllerBase
    {
        private readonly IImagenesService imagenesService;

        public ImagenesController(IImagenesService imagenesService)
        {
            this.imagenesService = imagenesService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DetailsImagenDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DetalleError),StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateImagen([FromBody] CreateImagenDto createImagen)
        {
            var result = await imagenesService.CreateImagenAsync(createImagen);

            return CreatedAtAction(nameof(ReadImagenAsync), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailsImagenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ReadImagenAsync(Guid id)
        {
            var result = await imagenesService.ReadImagenAsync(id);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DetailsImagenDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ReadAllImagenFromProductoAsync(Guid ProductoId)
        {
            var result = await imagenesService.ReadAllImagenOfProductoAsync(ProductoId);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DetalleError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteImagenAsync(Guid id)
        {
            await imagenesService.DeleteImagenAsync(id);
            return NoContent();
        }
    }
}
