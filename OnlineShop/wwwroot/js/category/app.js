
/**
 * Open a modal with sweetAlertjs
 * */
async function openModalToCreateCategory() {
    const { value: name } = await Swal.fire({
        title: 'Nombre de categoria',
        input: 'text',
        showCancelButton: true,
        inputValidator: (value) => {
            if (!value) {
                return 'Porfavor ingresa un nombre de categoria!';
            }
        }
    });
    if (name) {
        await fetch('/Category/Create', {
            body: JSON.stringify({ name: name }),
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(x => {
            Swal.fire(
                'Agregado!',
                'Haz agregado una nueva categoria!',
                'success'
            ).then(() => location.reload()).catch(() => location.reload());
        }).catch(x => {
            Swal.fire(
                'Error!',
                'Intente de nuevo!',
                'error'
            );
        });
    }
}


/**
 * RemoveCategory
 * @param id this param is the id to remove
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
            fetch(`/Category/Remove/${id}`, {
                method: 'GET'
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


/**
 * Update Category
 * @param id this id of category
 */
async function edit(id) {
    const { value: name } = await Swal.fire({
        title: 'Nuevo nombre de categoria',
        input: 'text',
        showCancelButton: true,
        inputValidator: (value) => {
            if (!value) {
                return 'Porfavor ingresa un nombre de categoria!';
            }
        }
    });
    if (name) {
        await fetch('/Category/Update', {
            body: JSON.stringify({id : id,  name: name }),
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(x => {
            Swal.fire(
                'Actualizado!',
                'Haz Actualizado la categoria!',
                'success'
            ).then(() => location.reload()).catch(() => location.reload());
        }).catch(x => {
            Swal.fire(
                'Error!',
                'Intente de nuevo!',
                'error'
            );
        });
    }
}