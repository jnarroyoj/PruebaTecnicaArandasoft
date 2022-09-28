using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAranda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private readonly IImagenesService imagenesService;

        public ImagenesController(IImagenesService imagenesService)
        {
            this.imagenesService = imagenesService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DetailsImagenDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void),StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateImagen([FromBody] CreateImagenDto createImagen)
        {
            try
            {
                var result = await imagenesService.CreateImagenAsync(createImagen);

                return CreatedAtAction(nameof(ReadImagenAsync), new { id = result.Id }, result);
            }
            catch (DbUpdateException) {
                return Conflict();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailsImagenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadImagenAsync(Guid id)
        {
            try
            {
                var result = await imagenesService.ReadImagenAsync(id);

                return Ok(result);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DetailsImagenDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadAllImagenFromProductoAsync(Guid ProductoId)
        {
            try
            {
                var result = await imagenesService.ReadAllImagenOfProductoAsync(ProductoId);

                return Ok(result);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteImagenAsync(Guid id)
        {
            try
            {
                await imagenesService.DeleteImagenAsync(id);
                return NoContent();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
