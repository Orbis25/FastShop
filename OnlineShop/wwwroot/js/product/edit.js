




/**
 * Hace el request para crear el valor de ese campo
 * */
const submitForm = async () => {

    const result = FormToJson(event);

    //evaluamos para sacar el valor del input tipo checkbox
    if (event.target[0].type === "checkbox") {
        result.Value = event.target[0].checked.toString();
    }

    try {
        const response = await axios.post("/productDetail/create", result);
        if (response.status === 200) {
            Swal.fire("Actualizado","", "success");
        }
    } catch (e) {
        Swal.fire("Ha ocurrido un error", e.response.data, "error");
        console.log(e);
    }
}


/**
 * 
 * TODO: LOGICA SI HAY UN CAMPO QUE ES REQUERIDO Y NO TIENE VALOR ENTONCES NO SE PUEDE CREAR EL PRODUCTO
 * HASTA QUE EL CAMPO TENGA VALOR ALGUNO SE PONE UN DISABLED AL FORMULARIO DEL BOTON
 * @param {any} categoryId
 */
/**
 * Obtiene los campos adicionales
 * @param {any} categoryId
 */
const getAdditionalsFieldsByCategoryId = async (categoryId, productId) => {

    //boton de actualizar el producto
    const updateBtn = $("#update-btn");

    try {
        const response = await axios.get("/additionalField/GetByCategoryId?categoryId=" + categoryId);
        if (response.status === 200) {
            const data = response.data;
            const container = $("#container-additional-field");
            container.empty();
            const content = ` <div class="col-12 col-md-4">
                                                        <form onsubmit="submitForm()" id="[entityId]">
                                                            <div class="row">
                                                                <div class="col">
                                                                    <div class="form-group">
                                                                            <label>[label]</label> [br]
                                                                            <input value="[value]"  min="1" type="[type]" name="Value" [required] [checked] class=" - [class]" />
                                                                            <input type="hidden" value="${productId}" name="ProductId"/>
                                                                            <input type="hidden" value="[entityId]" name="AdditionalFieldId"/>

                                                                        </div>
                                                                    <div class="form-group">
                                                                        <button type="submit" class="btn btn-primary btn-sm"><i class="fa fa-sync-alt"></i></button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </form> </div>`;


            //crea los campos con sus respectivas propiedades
            data.forEach((value) => {
                let classes = "form-control form-control-sm ";

                let html = content.replace("[label]", value.name)
                    .replace("[type]", getInputType(value.type))
                    .replace("[required]", (value.isRequired && value.type !== TYPE_DEFINITION_INPUT.CHECKBOX) ? "required" : "")
                    .replaceAll("[entityId]", value.id)
                    .replace("[value]", value.productDetail.length > 0 ? value.productDetail[0].value : "")
                    .replace("[br]", value.type === TYPE_DEFINITION_INPUT.CHECKBOX ? "</br>" : "");



                if (value.type === TYPE_DEFINITION_INPUT.DATE) {
                    classes += "input-date-now ";
                }

                if (value.type === TYPE_DEFINITION_INPUT.CHECKBOX) {
                    classes = "";
                    const response = value.productDetail;
                    html = html.replace("[checked]", (response.length > 0 && response[0].value === "true") ? "checked" : "");
                }

                html = html.replace("[class]", classes);

                container.append(html);
            });
        }

        //carga el toggle
        $(function () {
            $('input[type="checkbox"]').bootstrapToggle({
                on: 'Si',
                off: 'No',
                size: "mini"
            });
        })

        //carga el input de tipo date para los de tipo date
        $('.input-date-now').datepicker({
            format: "dd/mm/yyyy",
            endDate: new Date(),
            languaje: "es"
        })

    } catch (e) {
        console.log("error -> " + e);
    }
}