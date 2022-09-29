using CatalogoAranda.ApplicationCore.ApplicationExceptions;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace CatalogoAranda.ApplicationCore.Services
{
    public abstract class BaseService
    {
        protected IUnitOfWorkAdapter unitOfWorkAdapter;
        public async Task<Guid> GetValidGuidAsync(Func<Guid, Task<bool>> IdNotExists)
        {
            Guid Id = Guid.Empty;
            for (int i = 0; i < 5; i++)
            {
                Id = Guid.NewGuid();

                if (await IdNotExists(Id))
                {
                    break;
                }
                else if (i >= 4)
                {
                    throw new Exception("No se encontró un GUID válido luego de 5 intentos.");
                }
            }
            return Id;
        }

        protected async Task SaveChangesToDb(string nombreServicio, string? explicacion = null)
        {
            try
            {
                await unitOfWorkAdapter.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                string mensaje = $"Error en {nombreServicio}";
                if (explicacion is not null)
                {
                    mensaje += ", esto puede ser por:"
                        + Environment.NewLine + $" {explicacion}";
                }
                else
                {
                    mensaje += ".";
                }
                throw new CatalogoDbUpdateException(mensaje, ex);
            }
        }

        protected async Task SaveChangesToDb()
        {
            try
            {
                await unitOfWorkAdapter.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                string mensaje = $"Error en al guardar base de datos.";

                throw new CatalogoDbUpdateException(mensaje, ex);
            }
        }
    }
}
