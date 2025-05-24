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
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void AgregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("BEGIN TRANSACTION INSERT ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) OUTPUT INSERTED.ID VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio) DECLARE @ID INT= SCOPE_IDENTITY() INSERT INTO IMAGENES(IdArticulo, ImagenUrl) VALUES (@ID,@ImagenUrl1 ), (@ID,@ImagenUrl2 ), (@ID,@ImagenUrl3 )COMMIT;");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);

                //esto se puede mejorar, solo es para probar

                datos.setearParametro("@ImagenUrl1", nuevo.Imagenes[0].Url);
                datos.setearParametro("@ImagenUrl2", nuevo.Imagenes[1].Url);
                datos.setearParametro("@ImagenUrl3", nuevo.Imagenes[2].Url);

                datos.ejecutarAccion();

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
        public void Modificar(Articulo art)
        {
            AccesoDatos datosModificados = new AccesoDatos();
            try
            {

                datosModificados.setearParametro("@id", art.Id);
                datosModificados.setearParametro("@cod", art.Codigo);
                datosModificados.setearParametro("@nom", art.Nombre);
                datosModificados.setearParametro("@desc", art.Descripcion);
                datosModificados.setearParametro("@Mrca", art.Marca.Id);
                datosModificados.setearParametro("@Ctgria", art.Categoria.Id);
                datosModificados.setearParametro("@Prec", art.Precio);
                //datosModificados.setearParametro("@img", art.UrlImagen);

                datosModificados.setearParametro("@img1", art.Imagenes[0].Url);
                datosModificados.setearParametro("@img2", art.Imagenes[1].Url);
                datosModificados.setearParametro("@img3", art.Imagenes[2].Url);

                datosModificados.setearConsulta("UPDATE IMAGENES SET ImagenUrl=@img1 WHERE Id=( SELECT MIN(Id) FROM IMAGENES WHERE IdArticulo =@id)");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();
                datosModificados.setearConsulta("UPDATE IMAGENES SET ImagenUrl=@img2 WHERE Id=( SELECT MIN(Id)+1 FROM IMAGENES WHERE IdArticulo =@id)");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();
                datosModificados.setearConsulta("UPDATE IMAGENES SET ImagenUrl=@img3 WHERE Id=( SELECT MIN(Id)+2 FROM IMAGENES WHERE IdArticulo =@id)");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();

                datosModificados.setearConsulta("UPDATE ARTICULOS SET IdMarca = @Mrca WHERE Id = @id");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();
                datosModificados.setearConsulta("UPDATE ARTICULOS SET IdCategoria = @Ctgria WHERE Id = @id");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();
                datosModificados.setearConsulta("UPDATE ARTICULOS SET Codigo=@cod, Nombre=@nom, Descripcion=@desc, Precio=@Prec WHERE Id=@id");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();

            }
            catch (Exception ex)
            { throw ex; }
            //finally { datosModificados.cerrarConexion(); }

        }

    }
}
