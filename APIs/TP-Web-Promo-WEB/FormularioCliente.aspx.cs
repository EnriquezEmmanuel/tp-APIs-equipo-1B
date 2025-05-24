using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TP_Web_Promo_WEB
{
    public partial class FormularioCliente : System.Web.UI.Page
    {
        private Vaucher vaucherLocal = new Vaucher();
        //private int articuloSesion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        protected void btnParticipar_Click(object sender, EventArgs e)
        {
            ////////Hay que meter un try
            try
            {
                if (Page.IsValid)
                {

                    ClienteNegocio negocio = new ClienteNegocio();
                    var existente = negocio.buscarPorDNI(txtDocumento.Text);


                    if (existente == null)
                    {
                        cliente c = new cliente();

                        c.Documento = txtDocumento.Text;
                        c.Nombre = txtNombre.Text;
                        c.Apellido = txtApellido.Text;
                        c.Email = txtEmail.Text;
                        c.Direccion = txtDireccion.Text;
                        c.Ciudad = txtCiudad.Text;
                        c.CodPostal = int.Parse(txtCodPostal.Text);

                        negocio.agregar(c);
                    }


                    //-------------- carga del Vaucher -------------------

                    vaucherLocal.FechaCanje = DateTime.Now;

                    ClienteNegocio clienteAgregado = new ClienteNegocio();

                    cliente clientePrueba = new cliente();
                    clientePrueba = clienteAgregado.buscarPorDNI(txtDocumento.Text);        //la busqueda es por si se agregó un nuevo cliente

                    VaucherNegocio VchrNegocio = new VaucherNegocio();

                    vaucherLocal = (Vaucher)Session["Vaucher"];

                    vaucherLocal.IdCliente = clientePrueba.Id;                              //obtenemos el Id del posible nuevo cliente

                    //string fechaCje = vaucherLocal.FechaCanje.ToString("yyyy/MM/dd"); // esto es para la BD, se hace en VaucherNegocio
                    //verificador.Text = vaucherLocal.Codigo + ", " + vaucherLocal.IdCliente + ", " + fechaCje + ", " + vaucherLocal.IdArticulo;

                    VchrNegocio.registrarVoucher(vaucherLocal);


                    Response.Redirect("RegistroExitoso.aspx?mensaje=¡Registo exitoso!", false);
                }
            }
            catch (Exception)
            {
                lblMensaje.Text = "Hubo un error inesperado. Vuelva a intentar más tarde.";
            }




        }

        protected void txtDocumento_TextChanged(object sender, EventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            string dni = txtDocumento.Text.Trim();

            if (!string.IsNullOrEmpty(dni))
            {
                var lista = negocio.lista();
                cliente clienteExistente = lista.FirstOrDefault(c => c.Documento == dni);

                if (clienteExistente != null)
                {
                    txtNombre.Text = clienteExistente.Nombre;
                    txtApellido.Text = clienteExistente.Apellido;
                    txtEmail.Text = clienteExistente.Email;
                    txtDireccion.Text = clienteExistente.Direccion;
                    txtCiudad.Text = clienteExistente.Ciudad;
                    txtCodPostal.Text = clienteExistente.CodPostal.ToString();
                }
                else
                {
                    limpiarCampos();
                }
            }
        }
        private void limpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtCodPostal.Text = "";
        }
    }
}