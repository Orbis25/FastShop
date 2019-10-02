
function removeOffert (id) {
    Swal.fire(
        'Seguro que desea eliminarlo?',
        '',
        'warning'
    ).then((result) => {
        if (result.value) {
            fetch(`/Offert/Delete/${id}`, {
                method: 'POST'
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

function setIdModal(id) {
    let imp = document.getElementById('idimg');
    imp.value = id;
}