using System;
using System.Collections.Generic;

namespace CatalogoAranda.Infrastructure.Data
{
    public class Imagen
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Url { get; set; }
        public string? Base64 { get; set; }
        public Guid ProductoId { get; set; }

        public Producto Producto { get; set; } = null!;
    }
}
