
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
};


const newShop = () => {
    $('#alert-await').show();
    $("#proccess-sale").hide();
};