
/*new*/

const updateItem = async (id) => {
    const maxvalue = $(`#max-value-product`).val();

    const result = await Swal.fire({
        title: 'Nueva cantidad',
        input: 'number',
        inputAttributes: {
            autocapitalize: 'off',
            min: 1,
            max: Number(maxvalue)
        },
        showCancelButton: true,
        confirmButtonText: 'Actualizar',
        cancelButtonText: "Cancelar",
        showLoaderOnConfirm: true,
        preConfirm: (login) => login
    })

    if (result.value) {
        const data = {
            Id: id,
            Quantity: result.value
        };
        const fetch_result = await fetch("/cartItem/update/", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=UTF-8',
                'Accept': 'application/json; charset=utf-8'
            },
            body: JSON.stringify(data)
        });
        if (fetch_result.status === 200) {
            window.location.reload();
        }
    }
};


const newShop = () => {
    $('#alert-await').show();
    $("#proccess-sale").hide();
};