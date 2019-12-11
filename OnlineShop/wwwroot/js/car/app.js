

/**
 * This function add the product to car
 * @param {String} id id
 * @param {Number} quantity quantity
 * @param {String} name name
 * @param {Number} price price
 * @param {String} pic pic
 * @param {Number} totalQuantity totalQuantity
 * 
 */
function addToCar(id, quantity, name, price, pic, totalQuantity) {
    let location = '/Product/Car';
    if (localStorage.getItem('products') === null) {
        localStorage.setItem('products', JSON.stringify([{ id, quantity, name, price, pic, total: quantity * price }]));
    } else {
        let products = JSON.parse(localStorage.getItem('products'));
        let productFiltered = products.filter((values) => {
            return values.id === id;
        });

        if (productFiltered.length > 0) {
            productFiltered[0].quantity = Number(quantity) + Number(productFiltered[0].quantity);
            productFiltered[0].total += Number(quantity) * Number(price);
            if (productFiltered[0].quantity <= totalQuantity) {
                let productNotFiltered = products.filter((values) => {
                    return values.id !== id;
                });

                products = productNotFiltered;
                products.push(productFiltered[0]);
            } else {
                Swal.fire({
                    title: "Lo sentimos",
                    text: "No tenemos mas productos en el almacen",
                    type:"error"
                });
                location = '#';
            }

        } else {
            products.push({ id, quantity, name, price, pic, total: quantity * price });
        }
        let newFilter = products.filter((values) => {
            return values.id === id;
        });
        if (newFilter[0].quantity <= totalQuantity) {
            localStorage.setItem('products', JSON.stringify(products));
        }
    }
    window.location = location;
}

let subTotal = 0;
function GetAllProducts() {
    $(document).ready(function () {
        let products = JSON.parse(localStorage.getItem('products'));
        if (products !== null) {
            products.forEach((values, index) => {
                $('#tbl-products').append(`<tr key=${index}>
                            <td>
                                <div class="media">
                                    <div class="d-flex">
                                        <img height="100px" src="/files/${values.pic}" alt="">
                                    </div>
                                    <div class="media-body">
                                    <a href="/Product/GetById?id=${values.id}">${values.name}</a>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <h5>$${values.price}</h5>
                            </td>
                            <td>
                                <div class="product_count">
                                    <input type="text" name="qty" disabled id="sst" value="${values.quantity}" title="cantidad"
                                           class="input-text qty">
                                </div>
                            </td>
                            <td>
                                <h5>$${values.total}</h5>
                            </td>
                                <td>
                                <i class="btn text-warning" onClick="RemoveProduct('${values.id}')">X</i>
                            </td>
                        </tr>`);
                subTotal += values.total;
            });
        } else {
            $('#noProducts').show();
        }

        if (products.length > 0) {
            $('#tbl-products').append(`<tr class="bottom_button">
                            <td></td>
                            <td></td>
                           
                            <td>
                                <div class="cupon_text d-flex align-items-center">
                                    <input type="text" id="cupon-code" placeholder="Codigo de cupon">
                                    <button class="primary-btn" id="btn-cuppon" href="#" onClick="ApplyCuppon()">Aplicar</button>
                                    <a class="button" href="#">Tienes un cupón?</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <h5>Subtotal</h5>
                            </td>
                            <td>
                                <h5 id="subTotal">$${subTotal}</h5>
                            </td>
                        </tr>
                        <tr class="out_button_area">
                            <td class="d-none-l"></td>
                            <td class=""></td>
                            <td></td>
                            <td>
                                <div class="checkout_btn_inner d-flex align-items-center">
                                    <a class="gray_btn" href="/Category">Continuar comprando</a>
                                    <a class="primary-btn ml-2" href="#" onClick="displayPaymentType()">Proceder a pagar</a>
                                </div>
                            </td>
                        </tr>`);
        }
    });
}

const displayPaymentType = () => {
    $('#paymentType').show();
};

function ApplyCuppon() {
    if (subTotal > 0) {
        $(document).ready(function () {
            fetch('/Cuppon/GetByCupponCode/?code=' + $('#cupon-code').val()).then((response) => {
                return response.json();
            }).then(data => {
                subTotal -= data.amount;
                $('#subTotal').text(`$${subTotal}`);
                $('#btn-cuppon').attr("disabled", "disabled");
                $('#btn-cuppon').addClass("btn btn-default");
                $('#cupon-code').attr("disabled", "disabled");
                $('#alert').show();
            }).catch((err) => console.log('error => ', err));
        });
    }
}

function RemoveProduct(id) {
    let products = JSON.parse(localStorage.getItem('products'));
    products = products.filter((v) => {
        return v.id  !== id;
    });
    localStorage.setItem('products', JSON.stringify(products));
    location.reload();
}

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

async function AddSale(type =  "MyLocation") {

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

const sweetProcces = () => {
    Swal.fire({
        title: "información",
        text: "Espere un momento estamos procesando su venta",
        showCancelButton: false,
        showConfirmButton: false,
        allowOutsideClick: false
    });
};