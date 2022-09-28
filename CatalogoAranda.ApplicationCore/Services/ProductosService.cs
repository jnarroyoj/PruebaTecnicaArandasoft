using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories.Actions;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Dtos.ProductosDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using System.Linq.Expressions;

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
                                                  categoriasRepository.GetAsync)
            };

            await productosRepository.CreateAsync(producto);

            await unitOfWorkAdapter.SaveChangesAsync();

            return await ReadProductoAsync(validId);
        }

        public async Task DeleteProductoAsync(Guid Id)
        {
            var producto = await RetrieveProductoAsync(Id);

            await productosRepository.DeleteAsync(producto);

            await unitOfWorkAdapter.SaveChangesAsync();
        }

        public async Task<PagedDetailsProductoDto> ReadPagedProductoAsync(
            string? filtroNombre, string? filtroDescripcion, string? filtroCategoria, 
            bool? ordenAscendente, bool? ordenarPorNombre, int page = 1, 
            int productosPerPage = 20)
        {
            
            var filtros = ObtenerFiltros(filtroNombre, filtroDescripcion,
                filtroCategoria);

            var orderBy = ObtenerOrderBy(ordenAscendente, ordenarPorNombre);

            var productos = await productosRepository.GetManyAsync(filtros,
                orderBy, page, productosPerPage);

            List<DetailsProductoDto> detailsProductos = new();

            foreach(var producto in productos)
            {
                detailsProductos.Add(await ProductToDetailsProductoDtoAsync(producto));
            }

            return new PagedDetailsProductoDto(detailsProductos,
                await GetTotalOfProductos());

        }

        public async Task<DetailsProductoDto> ReadProductoAsync(Guid Id)
        {
            var producto = await RetrieveProductoAsync(Id);

            return await ProductToDetailsProductoDtoAsync(producto);
        }

        public async Task UpdateProductoAsync(UpdateProductoDto updateProductoDto)
        {
            var producto = await RetrieveProductoAsync(updateProductoDto.Id);

            producto.Nombre = updateProductoDto.Nombre;
            producto.Descripcion = updateProductoDto.Descripcion;
            producto.Categoria = await GetObjectsFromIds(updateProductoDto.Categorias,
                categoriasRepository.GetAsync);

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

        private IEnumerable<Expression<Func<Producto, bool>>> ObtenerFiltros(
            string? filtroNombre, string? filtroDescripcion, string? filtroCategoria
            )
        {
            var filtros = new List<Expression<Func<Producto, bool>>>();

            if (filtroNombre is not null)
            {
                Expression<Func<Producto, bool>> expresion = 
                    producto => producto.Nombre.ToLower().Contains(filtroNombre.ToLower());
                filtros.Add(expresion);
            }

            if (filtroDescripcion is not null)
            {
                Expression<Func<Producto, bool>> expresion =
                    producto => producto.Descripcion.ToLower().Contains(filtroDescripcion.ToLower());
                filtros.Add(expresion);
            }

            if (filtroCategoria is not null)
            {
                Expression<Func<Producto, bool>> expresion =
                    producto => producto.Categoria.Select(
                        categoria => categoria.Nombre.ToLower()
                        .Contains(filtroCategoria.ToLower())
                        ).Contains(true);
                filtros.Add(expresion);
            }

            return filtros;
        }

        private async Task<DetailsProductoDto> ProductToDetailsProductoDtoAsync(Producto producto)
        {
            var categoriasId = producto.Categoria.Select(x => x.Id);

            var imagenesId = producto.Imagenes.Select(x => x.Id);

            var categorias = (await categoriasService.ReadAllCategoriaAsync())
                .Where(x => categoriasId.Contains(x.Id)).ToArray();

            var detailsProductoDto = new DetailsProductoDto(producto.Id,
                producto.Nombre, producto.Descripcion,
                categorias, imagenesId.ToArray());

            return detailsProductoDto;
        }
        private Func<IQueryable<Producto>, IOrderedQueryable<Producto>>? ObtenerOrderBy(bool? ordenAscendente, bool? ordenarPorNombre)
        {
            Func<IQueryable<Producto>, IOrderedQueryable<Producto>>? orderBy = null;
            bool ascendente = ordenAscendente is not null;
            if (ascendente)
            {
                ascendente = ordenAscendente.Value;
            }

            if (ordenarPorNombre is not null)
            {
                if (ordenarPorNombre.Value)
                {
                    orderBy = productos => ascendente ?
                    productos.OrderBy(producto => producto.Nombre) :
                    productos.OrderByDescending(producto => producto.Nombre);
                }
                else
                {
                    orderBy = productos => ascendente ?
                    productos.OrderBy(producto => producto.Categoria.OrderBy(
                        categoria => categoria.Nombre
                        ).Select(x => x.Nombre).FirstOrDefault())
                    :
                    productos.OrderByDescending(producto => producto.Categoria.OrderByDescending(
                        categoria => categoria.Nombre
                        ).Select(x => x.Nombre).FirstOrDefault());
                }
            }
            return orderBy;
        }

        public async Task<int> GetTotalOfProductos()
        {
            return await productosRepository.GetTotalOfRecordsAsync();
        }
    }
}
