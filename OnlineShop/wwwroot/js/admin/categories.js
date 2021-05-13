
/**
 * Update Category
 * @param {*} id this id of category
 */
async function edit(id) {
    //show alert for update the category
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

        //update the category
        const response = await fetch('/Category/Update', {
            body: JSON.stringify({ id: id, name: name }),
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }
        });
        //check if status if 200
        if (response.status === 200) {
            //show message and reload
            await Swal.fire(
                'Actualizado!',
                'Haz Actualizado la categoria!',
                'success'
            )
            window.location.reload();
        }

        //show message if 400 and show message error from server
        if (response.status === 400) {
            const responseText = await response.text();
            await Swal.fire("error", responseText, "error")
        }
    }
}
