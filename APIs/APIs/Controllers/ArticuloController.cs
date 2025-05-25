using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIs.Models;
using Dominio;
using Negocio;
//using APIs.Models;

namespace APIs.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            return negocio.lista();
        }

        // GET: api/Articulo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Articulo
        public void Post([FromBody] ArticuloDto articuloDto)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();


            nuevo.Codigo = articuloDto.Codigo;
            nuevo.Nombre = articuloDto.Nombre;
            nuevo.Descripcion = articuloDto.Descripcion;
            nuevo.Marca = new Marca { Id = articuloDto.IdMarca };
            nuevo.Categoria = new Categoria { Id = articuloDto.IdCategoria };
            nuevo.Precio = articuloDto.Precio;
            nuevo.Imagenes = articuloDto.Imagenes;
            
            negocio.agregar(nuevo);

        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
        }
    }
}
