
document.addEventListener("DOMContentLoaded", function () {
    window.enviarFormulario = function () {
        alert("Función enviarFormulario ejecutada");
        let isValid = true;

        const dni = document.getElementById("txtDocumento");
        const codPostal = document.getElementById("txtCodPostal");
        const nombre = document.getElementById("txtNombre");
        const apellido = document.getElementById("txtApellido");
        const email = document.getElementById("txtEmail");
        const direccion = document.getElementById("txtDireccion");
        const ciudad = document.getElementById("txtCiudad");
        const chk = document.getElementById("chkAceptoHtml");

        const soloLetrasRegex = /^[a-zA-ZÁÉÍÓÚáéíóúñÑ\s]+$/;
        const soloNumerosRegex = /^\d+$/;
        const codigoPostalRegex = /^\d{4}$/;
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!soloNumerosRegex.test(dni.value.trim())) {
            dni.classList.add("is-invalid");
            dni.classList.remove("is-valid");
            isValid = false;
        } else {
            dni.classList.remove("is-invalid");
            dni.classList.add("is-valid");
        }

        if (!codigoPostalRegex.test(codPostal.value.trim())) {
            codPostal.classList.add("is-invalid");
            codPostal.classList.remove("is-valid");
            isValid = false;
        } else {
            codPostal.classList.remove("is-invalid");
            codPostal.classList.add("is-valid");
        }

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

        if (direccion.value.trim() === "") {
            direccion.classList.add("is-invalid");
            direccion.classList.remove("is-valid");
            isValid = false;
        } else {
            direccion.classList.remove("is-invalid");
            direccion.classList.add("is-valid");
        }

        if (!emailRegex.test(email.value.trim())) {
            email.classList.add("is-invalid");
            email.classList.remove("is-valid");
            isValid = false;
        } else {
            email.classList.remove("is-invalid");
            email.classList.add("is-valid");
        }

        if (!chk.checked) {
            chk.classList.add("is-invalid");
            isValid = false;
        } else {
            chk.classList.remove("is-invalid");
        }

        if (isValid) {
            document.getElementById("chkAcepto").checked = chk.checked;
            __doPostBack("btnParticipar", "");
        }
    };
});