// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function findMypass() {
    Swal.fire({
        title: "Ingresa tu correo",
        type:"info",
        html: "<input type='email' id='email' required />"
    }).then((val) => {
        let email = document.getElementById('email').value;
        if (val) {
            if (email.length > 0) {
                Swal.fire({
                    title: "información",
                    text: "Espere un momento",
                    showCancelButton: false,
                    showConfirmButton: false,
                    allowOutsideClick: false
                });
                fetch('/Home/RecoveryPassword/?email=' + email).then((va) => {
                    if (va) {
                        Swal.close();
                    }
                    return va.json();
                }).then((res) => {
                    Swal.fire({
                        title: "Correo enviado",
                        type: "success",
                        text: "Porfavor revisa tu email"
                    });
                }).catch((e) => {
                    Swal.fire({
                        title: "Lo sentimos este correo no existe",
                        type: "error"
                    });
                    console.log("erro" + e);
                });
            }
        }
    }).catch((e) => console.log("erro => " + e ));
}