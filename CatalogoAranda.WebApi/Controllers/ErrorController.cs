using CatalogoAranda.ApplicationCore.ApplicationExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace CatalogoAranda.WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context is null)
            {
                return Problem();
            }

            if(context.Error is CatalogoDbUpdateException)
            {
                return Conflict(new DetalleError(409, context.Error.Message));
            }
            else if(context.Error is CatalogoNullReferenceException)
            {
                return NotFound(new DetalleError(404, context.Error.Message));
            }

            logger.LogError(context.Error, "UnexpectedError");

            return Problem();
        }

        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment(
            [FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            if (context is null)
            {
                return Problem();
            }

            if (context.Error is CatalogoDbUpdateException)
            {
                return Conflict(new DetalleError(409, context.Error.Message));
            }
            else if (context.Error is CatalogoNullReferenceException)
            {
                return NotFound(new DetalleError(404, context.Error.Message));
            }

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

    }
}
