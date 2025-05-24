<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistroExitoso.aspx.cs" Inherits="TP_Web_Promo_WEB.RegistroExitoso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/RegistroExitoso.css" rel="stylesheet" />
    <div class="contenedor">
        <h2> <%= Request.QueryString["mensaje"].ToString() %> </h2>
        <h3>Redirigiendo hacia la página principal en <span id="segundos"></span></h3>
        <a href="Default.aspx">
            <p class="badge text-bg-secondary">Redirigir</p>
        </a>
    </div>

    <script>
        addEventListener('load', inicio, false);
        var reloj;
        let miliseg = 1000;
        let seg = 7;
        function inicio() {
            reloj = setInterval(retornar, miliseg);
        }
        function retornar() {

            if (seg <= 0) {
                window.location = 'Default.aspx';
            }
            else {
                document.getElementById('segundos').innerHTML = seg;
                seg--;
            }
        }
    </script>
</asp:Content>
