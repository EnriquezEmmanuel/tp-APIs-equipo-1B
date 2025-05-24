<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioCliente.aspx.cs" Inherits="TP_Web_Promo_WEB.FormularioCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-3 needs-validation" novalidate runat="server">
        <h2>Ingresá tus datos</h2>
        <hr />

        <div class="col-12">
            <label for="txtDocumento" class="form-label">DNI</label>
            <asp:TextBox runat="server" ID="txtDocumento" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDocumento_TextChanged" />
            <div class="invalid-feedback">Ingrese un DNI válido.</div>
            <div class="valid-feedback">DNI correcto.</div>
        </div>

        <div class="col-md-4">
            <label for="txtNombre" class="form-label">Nombre</label>
            <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            <div class="invalid-feedback">Ingrese un nombre.</div>
        </div>

        <div class="col-md-4">
            <label for="txtApellido" class="form-label">Apellido</label>
            <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
            <div class="invalid-feedback">Ingrese un apellido.</div>
        </div>

        <div class="col-md-4">
            <label for="txtEmail" class="form-label">Email</label>
            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
            <div class="invalid-feedback">Ingrese un email válido.</div>
        </div>

        <div class="col-md-4">
            <label for="txtDireccion" class="form-label">Dirección</label>
            <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" />
            <div class="invalid-feedback">Ingrese una dirección válida.</div>
        </div>

        <div class="col-md-4">
            <label for="txtCiudad" class="form-label">Ciudad</label>
            <asp:TextBox runat="server" ID="txtCiudad" CssClass="form-control" />
            <div class="invalid-feedback">Ingrese una ciudad válida.</div>
        </div>

        <div class="col-md-4">
            <label for="txtCodPostal" class="form-label">Código Postal</label>
            <asp:TextBox runat="server" ID="txtCodPostal" CssClass="form-control" MaxLength="4" />
            <div class="invalid-feedback">Ingrese un código postal válido (4 números).</div>
        </div>

        <div class="col-12">
            <div class="form-check">
                <input type="checkbox" class="form-check-input" id="chkAceptoHtml" required />
                <label class="form-check-label" for="chkAceptoHtml">Acepto los términos y condiciones</label>
                <div class="invalid-feedback">Debe aceptar los términos para continuar.</div>
            </div>
            <asp:CheckBox runat="server" ID="chkAcepto" CssClass="d-none" />
        </div>

        <div class="col-12 mt-3">
            <!-- Botón visible HTML con JS -->
            <button type="button" class="btn btn-primary" onclick="enviarFormulario()">Participar</button>
            <!-- Botón ASP.NET oculto para el postback -->
            <asp:Button runat="server" ID="btnParticipar" CssClass="d-none" OnClick="btnParticipar_Click" />
        </div>
    </div>



      <asp:Label Text="" ID="lblMensaje" runat="server" /> 

    <!-- Script con validación Bootstrap + ClientID -->
    <script>
        function enviarFormulario() {
            let isValid = true;

            const dni = document.getElementById('<%= txtDocumento.ClientID %>');
        const codPostal = document.getElementById('<%= txtCodPostal.ClientID %>');
        const nombre = document.getElementById('<%= txtNombre.ClientID %>');
        const apellido = document.getElementById('<%= txtApellido.ClientID %>');
        const email = document.getElementById('<%= txtEmail.ClientID %>');
        const direccion = document.getElementById('<%= txtDireccion.ClientID %>');
        const ciudad = document.getElementById('<%= txtCiudad.ClientID %>');
        const chk = document.getElementById("chkAceptoHtml");

        // Expresiones regulares
        const soloLetrasRegex = /^[a-zA-ZÁÉÍÓÚáéíóúñÑ\s]+$/;
        const soloNumerosRegex = /^\d+$/;
        const codigoPostalRegex = /^\d{4}$/;
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        // Validar DNI: solo números
        if (!soloNumerosRegex.test(dni.value.trim())) {
            dni.classList.add("is-invalid");
            dni.classList.remove("is-valid");
            isValid = false;
        } else {
            dni.classList.remove("is-invalid");
            dni.classList.add("is-valid");
        }

        // Validar Código Postal: solo 4 dígitos
        if (!codigoPostalRegex.test(codPostal.value.trim())) {
            codPostal.classList.add("is-invalid");
            codPostal.classList.remove("is-valid");
            isValid = false;
        } else {
            codPostal.classList.remove("is-invalid");
            codPostal.classList.add("is-valid");
        }

        // Validar Nombre, Apellido y Ciudad: solo letras
        const textoCampos = [nombre, apellido, ciudad];
        textoCampos.forEach(input => {
            if (!soloLetrasRegex.test(input.value.trim())) {
                input.classList.add("is-invalid");
                input.classList.remove("is-valid");
                isValid = false;
            } else {
                input.classList.remove("is-invalid");
                input.classList.add("is-valid");
            }
        });

        // Validar Dirección: no vacía
        if (direccion.value.trim() === "") {
            direccion.classList.add("is-invalid");
            direccion.classList.remove("is-valid");
            isValid = false;
        } else {
            direccion.classList.remove("is-invalid");
            direccion.classList.add("is-valid");
        }

        // Validar Email
        if (!emailRegex.test(email.value.trim())) {
            email.classList.add("is-invalid");
            email.classList.remove("is-valid");
            isValid = false;
        } else {
            email.classList.remove("is-invalid");
            email.classList.add("is-valid");
        }

        // Validar checkbox
        if (!chk.checked) {
            chk.classList.add("is-invalid");
            isValid = false;
        } else {
            chk.classList.remove("is-invalid");
        }

        // Enviar formulario si todo es válido
        if (isValid) {
            __doPostBack('<%= btnParticipar.UniqueID %>', '');
            }
        }
    </script>
</asp:Content>