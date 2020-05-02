// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function findMypass() {
    Swal.fire({
        title: "Ingresa tu correo",
        type:"info",
        html: "<input type='text' id='email' />"
    }).then((val) => {
        let email = document.getElementById('email').value;
        if (val) {
            if (email.length > 0) {
                fetch('/Home/RecoveryPassword/?email=' + email).then((va) => {
                    return va.json();
                }).then((res) => {
                    Swal.fire({
                        title: "Correo enviado",
                        type: "success",
                        text: "Porfavor revisa tu email"
                    });
                }).catch((e) => console.log("erro" + e));
            }
        }
    }).catch((e) => console.log("erro => " + e ));
}