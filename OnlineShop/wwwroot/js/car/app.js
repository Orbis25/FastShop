

const displayPaymentType = () => {
    $('#paymentType').show();
};

const payment = async (model) => {
    await fetch('/Sale/Add', {
        method: "POST",
        body: JSON.stringify(model),
        headers: {
            'Content-Type': 'application/json'
        }
    }).then((response) => response).then((res) => {
        if (res) {
            Swal.close();
        }
        localStorage.clear();
        Swal.fire({
            title: '<strong>Compra realizada</strong>',
            type: 'success',
            text: "En su correo estara la factura con el detalle de su compra, recuerde (el pago se realiza en efectivo al llevarle su compra pero si pago por paypal ignorar el mensaje)"
        }).then(() => {
            location.href = '/';

        }).catch(() => {
            location.href = '/';

        });
    }).catch(e => console.log('err', e));
};

async function AddSale(type = "MyLocation") {

    switch (type) {
        case "MyLocation":
            const user = await fetch('/Account/GetUser').then((response) => response.json());
            sweetProcces();
            if (user !== null) {
                if (localStorage.getItem('products') !== null) {
                    let model = {
                        CuponCode: $('#cupon-code').val(),
                        Total: subTotal,
                        Description: user.address,
                        DetailSales: []
                    };
                    let products = JSON.parse(localStorage.getItem('products'));
                    products.forEach(value => {
                        model.DetailSales.push({ Quantity: value.quantity, ProductId: value.id });
                    });
                    await payment(model);
                }
            }
            break;
        case "AddLocation":
            if ($('#Description').val() !== undefined) {
                if ($('#Description').val().length <= 0) {
                    Swal.fire({
                        title: '<strong>Lo sentimos</strong>',
                        type: 'error',
                        text: "Ingrese una dirrección"
                    });
                } else {
                    sweetProcces();
                    if (localStorage.getItem('products') !== null) {
                        let model = {
                            CuponCode: $('#cupon-code').val(),
                            Total: subTotal,
                            Description: $('#Description').val(),
                            DetailSales: []
                        };
                        let products = JSON.parse(localStorage.getItem('products'));
                        products.forEach(value => {
                            model.DetailSales.push({ Quantity: value.quantity, ProductId: value.id });
                        });
                        await payment(model);
                    }
                }
            }
            break;
    }

}


/*new*/

const handleChangeQyt = (event, value) => {
    $(`#${value}`).show();
}

const updateItem = async (id, productId) => {
    const value = $(`#${productId}`).val();
    const data = {
        Id: id,
        Quantity: value
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