/**
 * Remove
 * @param {*} id this param is the id to remove
 * */
function Deleted(id) {

    Swal.fire({
        title: 'Esta seguro?',
        text: "Si la elimina no podra deshacer esta accion",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar!'
    }).then((result) => {
        if (result.value) {
            fetch(`/Cuppon/Remove/${id}`, {
                method: 'Post'
            })
                .then(() => {
                    Swal.fire(
                        'Eliminado!',
                        'Ha sido eliminado',
                        'success'
                    ).then(() => location.reload()).catch(() => location.reload());
                }).catch(() => {
                    Swal.fire(
                        'Error!',
                        'Intente de nuevo!',
                        'error');
                });
        }
    });
}