

const addField = async () => {
    const productId = $("#product-id").val();
    let result = FormToJson(event);
    

    const data = {
        name: result.Name,
        type: result.Type,
        description: result.Description,
        value: result.Value,
        productId
    }

    if (event.target[2].type === "checkbox") {
        data.Value = String(event.target[2].checked);
    }

    const response = await axios.post("/productDetail/Create", data);
    if (response.status === 200)
        window.location.reload();

}

