using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Precio { get; set; }

        // Nueva propiedad: Lista de imágenes asociadas
        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();


    }
}