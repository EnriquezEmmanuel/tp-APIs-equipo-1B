using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> lista()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setearConsulta("Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS MarcaDescripcion,C.Descripcion AS CategoriaDescripcion, A.Precio from  ARTICULOS A left join MARCAS M on A.IdMarca=M.Id left join CATEGORIAS c on A.IdCategoria=C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    //Info Marca
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["MarcaDescripcion"];
                    //Info Catagoria
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["CategoriaDescripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    lista.Add(aux);
                }

                return lista;
            }

            catch (Exception ex) { throw ex; }

            finally { datos.cerrarConexion(); }
        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"BEGIN TRANSACTION; INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio); DECLARE @IdArticulo INT = SCOPE_IDENTITY(); INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl1); INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl2); INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl3); COMMIT TRANSACTION; ";

                datos.setearConsulta(consulta);

                // Parámetros del artículo
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);


                datos.ejecutarAccion();
            }

            catch (Exception ex) { throw ex; }

            finally { datos.cerrarConexion(); }
        }
        public void Modificar(Articulo art)
        {
            AccesoDatos datosModificados = new AccesoDatos();
            try
            {
                datosModificados.setearConsulta("UPDATE ARTICULOS SET Codigo=@cod, Nombre=@nom, Descripcion=@desc, IdMarca=@Mrca, IdCategoria=@Ctgria, Precio=@Prec WHERE Id=@id");
                datosModificados.setearParametro("@id", art.Id);
                datosModificados.setearParametro("@cod", art.Codigo);
                datosModificados.setearParametro("@nom", art.Nombre);
                datosModificados.setearParametro("@desc", art.Descripcion);
                datosModificados.setearParametro("@Mrca", art.Marca.Id);
                datosModificados.setearParametro("@Ctgria", art.Categoria.Id);
                datosModificados.setearParametro("@Prec", art.Precio);
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();

            }
            catch (Exception ex)
            { throw ex; }
            finally { datosModificados.cerrarConexion(); }

        }
        public void Eliminar(int Art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("BEGIN TRANSACTION BEGIN TRY DELETE FROM IMAGENES WHERE IdArticulo = @id DELETE FROM ARTICULOS WHERE id = @id COMMIT END TRY BEGIN CATCH ROLLBACK END CATCH");
                datos.setearParametro("@id", Art);
                datos.ejecutarAccion();
            }

            catch (Exception ex) { throw ex; }

            finally { datos.cerrarConexion(); }
        }

    }
}
