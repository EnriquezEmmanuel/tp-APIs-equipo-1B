using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TP_Web_Promo_WEB
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {

            Image1.Style["opacity"] = "1";
            try
            {
                VaucherNegocio ListaVauchers = new VaucherNegocio();
                bool validacion = false;
                string mensaje = "";

                foreach (Vaucher item in ListaVauchers.lista())
                {
                    if (item.Codigo == txtBoxVoucher.Text)
                    {
                        if (item.IdCliente == 0)
                        {
                            validacion = true;

                            Session.Add("Vaucher", item);

                            Response.Redirect("ListadoProducto.aspx", false);
                        }
                        else
                        {
                            validacion = true;
                            mensaje = "El Vaucher ya ha sido utilizado.";
                        }
                    }
                }

                Image1.Style["opacity"] = "0";
                if (validacion == true) lblMensaje.Text = mensaje; else lblMensaje.Text = "Vaucher inexistente";
            }
            catch (Exception)
            {
                Image1.Style["opacity"] = "0";
                lblMensaje.Text = "Hubo un error inesperado. Vuelva a intentar más tarde.";
            }


        }

    }
}