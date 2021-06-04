


const getFieldTypesString = (type) => {
    switch (type) {
        case "0": return "Texto";
        case "1": return "Numerico";
        case "2": return "Boleano (si o no)";
        case "3": return "Fecha";
        default: return "";
    }
};

let fields = [];
/**
 * Add to list the field
 */
const addField = () => {
    const result = FormToJson(event);
    const data = {
        name: result.Name,
        type: result.Type,
        description: result.Description,
        isRequired: !!result.IsRequired,
        selectionable: !!result.Selectionable
    }
    const filter = fields.filter(x => x.name === result.Name);
    if (filter.length) {
        Swal.fire("Ya existe este campo", "", "warning");
    } else {

        fields.push(data);
        $(".modal").modal('hide');
        $("#table-container").show();
        $('#fields-body')
            .append(`<tr id="${data.name}">
                        <td>${data.name}</td>
                        <td>${data.description}</td>
                        <td>${data.isRequired ? 'Si' : 'No'}</td>
                        <td>${data.selectionable ? 'Si' : 'No'}</td>
                        <td>${getFieldTypesString(data.type)}</td>
                        <td><button type="button" class='btn btn-sm btn-danger' onClick='removeField("${data.name}")'><i class="fa fa-trash"></i></button></td>
                    </tr>`);
    }
};


const removeField = (name) => {
    fields = fields.filter(x => x.name !== name);
    $(`#${name}`).empty();
};

const submitForm = async () => {
    event.preventDefault();
    let form_data = FormToJson(event);
    if (fields.length) {
        form_data.aditionalFields = fields;
    }
    console.log(form_data)
    if (!form_data.Name) {
        $("#validation").text("Campo requerido");
        return;
    }
    try {
        const response = await axios.post("/category/create", form_data);
        if (response.status === 200) {
            const result = await Swal.fire("Creada exitosamente");
            if (result) {
                window.location.href = "/admin/categories"
            }
        }
    } catch (e) {
        console.log(e.response)
        Swal.fire(e.response.data, "", "error");
    }
}