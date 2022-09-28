using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories.Actions;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Dtos.ProductosDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services.Interfaces;


namespace CatalogoAranda.ApplicationCore.Services
{
    public class ProductosService : BaseService, IProductosService
    {
        private readonly IUnitOfWorkAdapter unitOfWorkAdapter;
        private readonly IProductosRepository productosRepository;
        private readonly ICategoriasRepository categoriasRepository;
        private readonly IImagenesRepository imagenesRepository;
        private readonly ICategoriasService categoriasService;

        public ProductosService(IUnitOfWork unitOfWork, ICategoriasService categoriasService)
        {
            unitOfWorkAdapter = unitOfWork.Create();
            productosRepository = unitOfWorkAdapter.Repositories.ProductosRepository;
            categoriasRepository = unitOfWorkAdapter.Repositories.CategoriasRepository;
            imagenesRepository = unitOfWorkAdapter.Repositories.ImagenesRepository;
            this.categoriasService = categoriasService;
        }
        public async Task<DetailsProductoDto> CreateProductoAsync(CreateProductoDto createProductoDto)
        {
            var validId = await GetValidGuidAsync(productosRepository.IdNotExistsAsync);

            var producto = new Producto
            {
                Id = validId,
                Descripcion = createProductoDto.Descripcion,
                Nombre = createProductoDto.Nombre,
                Categoria = await GetObjectsFromIds(createProductoDto.Categorias,
                                                  categoriasRepository.GetAsync),
                Imagenes = await GetObjectsFromIds(createProductoDto.Imagenes,
                                                  imagenesRepository.GetAsync)
            };

            await productosRepository.CreateAsync(producto);

            await unitOfWorkAdapter.SaveChangesAsync();

            return await ReadProductoAsync(validId);
        }

        public async Task DeleteProductoAsync(Guid Id)
        {
            var producto = await RetrieveProductoAsync(Id);

            await productosRepository.DeleteAsync(producto);
        }

        public Task<IEnumerable<DetailsProductoDto>> ReadPagedProductoAsync(string? filtroNombre, string? filtroDescripcion, string? filtroCategoria, bool? ordenAscendente, bool? ordenarPorNombre, int Page = 1, int ProductosPerPage = 20)
        {
            throw new NotImplementedException();
        }

        public async Task<DetailsProductoDto> ReadProductoAsync(Guid Id)
        {
            var producto = await RetrieveProductoAsync(Id);

            var categoriasId = producto.Categoria.Select(x => x.Id);

            var imagenesId = producto.Imagenes.Select(x => x.Id);

            var categorias = (await categoriasService.ReadAllCategoriaAsync())
                .Where(x => categoriasId.Contains(x.Id)).ToArray();

            var detailsProductoDto = new DetailsProductoDto(producto.Id,
                producto.Nombre, producto.Descripcion,
                categorias, imagenesId.ToArray());

            return detailsProductoDto;
        }

        public async Task UpdateProductoAsync(UpdateProductoDto updateProductoDto)
        {
            var producto = await RetrieveProductoAsync(updateProductoDto.Id);

            producto.Nombre = updateProductoDto.Nombre;
            producto.Descripcion = updateProductoDto.Descripcion;
            producto.Categoria = await GetObjectsFromIds(updateProductoDto.Categorias,
                categoriasRepository.GetAsync);
            producto.Imagenes = await GetObjectsFromIds(updateProductoDto.Imagenes,
                imagenesRepository.GetAsync);

            await productosRepository.UpdateAsync(producto);

            await unitOfWorkAdapter.SaveChangesAsync();
        }

        private async Task<ICollection<T>> GetObjectsFromIds<T>(Guid[] Ids, 
            Func<Guid,Task<T?>> getAsync)
        {
            var elementos = new List<T>();

            foreach (var elementoId in Ids)
            {
                var elemento = await getAsync(elementoId);

                if (elemento is null)
                    throw new NullReferenceException("Error al crear Producto.");
                    
                elementos.Add(elemento);
            }

            return elementos;
        }

        private async Task<Producto> RetrieveProductoAsync(Guid Id)
        {
            var producto = await productosRepository.GetAsync(Id);

            if (producto is null)
                throw new NullReferenceException("El producto no existe.");

            return producto;
        }
    }
}
