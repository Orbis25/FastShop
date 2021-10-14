
const getFieldTypesString = (type) => {
    switch (type) {
        case "0": return "Texto";
        case "1": return "Numerico";
        case "2": return "Boleano (si o no)";
        case "3": return "Fecha";
        default: return "";
    }
};

/**
 * fields of products to create 
 * */
let fields = [];


const addField = () => {
    let result = FormToJson(event);
    const container = $("#fields-body");
    if (event.target[2].type === "checkbox") {
        data.Value = String(event.target[2].checked);
    }


    const data = {
        name: result.Name,
        type: result.Type,
        description: result.Description,
        value: result.Value
    }
    const filter = fields.filter(x => x.name === result.Name);
    if (filter.length) {
        Swal.fire("Ya existe este campo", "", "warning");
    } else {
        fields.push(data);
        $(".modal").modal('hide');
        container.append(`<tr id="${data.name}">
                        <td>${data.name}</td>
                        <td>${data.description}</td>
                        <td>${fromBooleanString(data.value) ? 'Si' : 'No'}</td>
                        <td>${getFieldTypesString(data.type)}</td>
                        <td><button type="button" class='btn btn-sm btn-danger' onClick='removeField("${data.name}")'><i class="fa fa-trash"></i></button></td>
                    </tr>`);
    }
}


const removeField = (name) => {
    fields = fields.filter(x => x.name !== name);
    $(`#${name}`).empty();
};


const submitForm = async (e) => {
    e.preventDefault();
    console.log();
    const data = new FormData(e.target);
    const values = Object.fromEntries(data.entries());

    if (!$("form").valid())
        return;

    try {
        values.ProductDetails = fields;

        const response = await axios.post("/product/create", values);
        if (response.status === 200)
            history.back();

    } catch (e) {
        Swal.fire("Lo sentimos", e.response.data, "error");
        console.log(e)
    }
}