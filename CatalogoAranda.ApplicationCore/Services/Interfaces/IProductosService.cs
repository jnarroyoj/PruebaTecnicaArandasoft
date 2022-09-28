using CatalogoAranda.ApplicationCore.Dtos.ProductosDtos;

namespace CatalogoAranda.ApplicationCore.Services.Interfaces
{
    public interface IProductosService
    {
        Task<DetailsProductoDto> CreateProductoAsync(CreateProductoDto createProductoDto);

        Task UpdateProductoAsync(UpdateProductoDto updateProductoDto);

        Task<DetailsProductoDto> ReadProductoAsync(Guid Id);
        Task<int> GetTotalOfProductos();

        Task<IEnumerable<DetailsProductoDto>> ReadPagedProductoAsync(
            string? filtroNombre, string? filtroDescripcion,
            string? filtroCategoria, bool? ordenAscendente,
            bool? ordenarPorNombre,
            int Page = 1, int ProductosPerPage = 20
            );

        Task DeleteProductoAsync(Guid Id);
    }
}
