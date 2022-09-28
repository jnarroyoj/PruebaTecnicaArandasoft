namespace CatalogoAranda.ApplicationCore.Services
{
    public abstract class BaseService
    {
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
    }
}
