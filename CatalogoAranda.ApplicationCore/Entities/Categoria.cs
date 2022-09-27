using System;
using System.Collections.Generic;

namespace CatalogoAranda.ApplicationCore.Entities
{
    public class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}
