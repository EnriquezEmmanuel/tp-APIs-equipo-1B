using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIs.Models;
using Dominio;
using Negocio;
using System.Text.RegularExpressions;

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
        public Articulo Get(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> productos = negocio.lista();

            return productos.Find(x => x.Id == id);
        }


        // POST: api/Articulo
        public HttpResponseMessage Post([FromBody] ArticuloDto articuloDto)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo nuevo = new Articulo();

                /////////validaciones de marca, categoría y cantidad minima de imágenes/////////
                MarcaNegocio mrcaNeg = new MarcaNegocio();

                Marca mrca = mrcaNeg.listar().Find(x => x.Id == articuloDto.IdMarca);
                if (mrca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca no existe.");

                CategoriaNegocio catNeg = new CategoriaNegocio();

                Categoria cat = catNeg.listar().Find(x => x.Id == articuloDto.IdCategoria);
                if (cat == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría no existe.");

                if (
                    articuloDto.Codigo == "" ||
                    articuloDto.Codigo == null ||
                    articuloDto.Nombre == "" ||
                    articuloDto.Nombre == null ||
                    articuloDto.Descripcion == "" ||
                    articuloDto.Descripcion == null
                    )
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Se debe proporcionar obligatoriamente un código, nombre y descripción");
                }

                string NoNumero = @"^\d+$";
                Regex regex = new Regex(NoNumero);
                if (regex.IsMatch(articuloDto.Precio.ToString()))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El precio debe contener un valor numérico.");
                }
                ///////////////////////////////////////////////////////

                nuevo.Codigo = articuloDto.Codigo;
                nuevo.Nombre = articuloDto.Nombre;
                nuevo.Descripcion = articuloDto.Descripcion;
                nuevo.Marca = new Marca { Id = articuloDto.IdMarca };
                nuevo.Categoria = new Categoria { Id = articuloDto.IdCategoria };
                nuevo.Precio = articuloDto.Precio;

                negocio.agregar(nuevo);
                return Request.CreateResponse(HttpStatusCode.OK, "Artículo agregado correctamente.");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }



        }
        public void Post(int id, [FromBody] ImagenDto dto)
        {
            ImagenNegocio negocio = new ImagenNegocio();
            negocio.agregarImagen(id, dto.Url);
        }


        // PUT: api/Articulo/5
        public HttpResponseMessage Put(int id, [FromBody] ArticuloDto articuloDto)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo nuevo = new Articulo();

                MarcaNegocio mrcaNeg = new MarcaNegocio();

                Marca mrca = mrcaNeg.listar().Find(x => x.Id == articuloDto.IdMarca);
                if (mrca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca no existe.");

                CategoriaNegocio catNeg = new CategoriaNegocio();

                Categoria cat = catNeg.listar().Find(x => x.Id == articuloDto.IdCategoria);
                if (cat == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría no existe.");

                if (
                    articuloDto.Codigo == "" ||
                    articuloDto.Codigo == null ||
                    articuloDto.Nombre == "" ||
                    articuloDto.Nombre == null ||
                    articuloDto.Descripcion == "" ||
                    articuloDto.Descripcion == null
                    )
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Se debe proporcionar obligatoriamente un código, nombre y descripción");
                }

                string NoNumero = @"^\d+$";
                Regex regex = new Regex(NoNumero);
                if (regex.IsMatch(articuloDto.Precio.ToString()))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El precio debe contener un valor numérico.");
                }

                nuevo.Codigo = articuloDto.Codigo;
                nuevo.Nombre = articuloDto.Nombre;
                nuevo.Descripcion = articuloDto.Descripcion;
                nuevo.Marca = new Marca { Id = articuloDto.IdMarca };
                nuevo.Categoria = new Categoria { Id = articuloDto.IdCategoria };
                nuevo.Precio = articuloDto.Precio;
                nuevo.Id = id;

                negocio.Modificar(nuevo);
                return Request.CreateResponse(HttpStatusCode.OK, "Modificación correcta.");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }


        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.Eliminar(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Eliminación correcta.");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }


        }
    }
}
