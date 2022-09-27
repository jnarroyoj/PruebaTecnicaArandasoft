using System;
using System.Collections.Generic;

namespace CatalogoAranda.ApplicationCore.Entities
{
    public class Producto
    {
        public Producto()
        {
            Imagenes = new HashSet<Imagen>();
            Categoria = new HashSet<Categoria>();
        }

        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public ICollection<Imagen> Imagenes { get; set; }
        public ICollection<Categoria> Categoria { get; set; }
    }
}
