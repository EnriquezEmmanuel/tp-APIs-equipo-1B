using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ImagenNegocio
    {
        public Dictionary<int, List<Imagen>> lista()
        {
            // Diccionario para almacenar las imágenes agrupadas por IdArticulo
            Dictionary<int, List<Imagen>> imagenesPorArticulo = new Dictionary<int, List<Imagen>>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta para obtener las imágenes y su IdArticulo correspondiente
                datos.setearConsulta("SELECT I.IdArticulo, I.ImagenUrl FROM IMAGENES I");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idArticulo = (int)datos.Lector["IdArticulo"];
                    string urlImagen = (string)datos.Lector["ImagenUrl"];

                    // Crear la imagen
                    Imagen aux = new Imagen
                    {
                        Id = idArticulo,
                        Url = urlImagen
                    };

                    // Si el artículo ya tiene imágenes asociadas, las agregamos a la lista
                    if (!imagenesPorArticulo.ContainsKey(idArticulo))
                    {
                        imagenesPorArticulo[idArticulo] = new List<Imagen>();
                    }

                    // Agregar la imagen a la lista correspondiente al artículo
                    imagenesPorArticulo[idArticulo].Add(aux);
                }

                return imagenesPorArticulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



    } 
}
