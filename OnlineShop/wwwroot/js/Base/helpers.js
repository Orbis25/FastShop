


/**
 * Print by specific id of tag
 * @param {string} id
 */
var toPrint = (id) => {
    $(id).printThis();
};

var LoadPartialView = async (contentId, url) => {
    const content = $(`#${contentId}`);
    content.empty();
    content.append("cargando...");
    try {
        const result = await fetch(url, { method: "GET" });
        content.empty();
        if (result.url.includes("Identity/Account/Login")) {
            content.html("<p>Lo sentimos pero no tiene permiso para acceder a este recurso, por favor inicie sesión</p>")
        }
        else if (result.status === 200) {
            content.html(await result.text());
        } else if (result.status === 400) {
            content.append(`<p>${await result.text()}</p>`);
        } else { console.log(result) }
    } catch (e) {
        content.empty();
        content.append(`Error al cargar la información, intente de nuevo mas tarde`);
    }
};


/**
 * Convert form to json
 * @param {Event} event evento del formulario
 */
var FormToJson = (event) => {
    event.preventDefault();
    const data = new FormData(event.target)
    const values = Object.fromEntries(data.entries());
    return values;
}


var showImageInViewver = (id) => {
    new Viewer(document.getElementById(`${id}`), { navbar: false });
}

/**
 * obtine el tipo del campo y lo convierte al tipo de input
 * @param {any} type
 */
const getInputType = (type) => {
    switch (String(type)) {
        case "0": return "text";
        case "1": return "number";
        case "2": return "checkbox";
        case "3": return "text"; //use date pluggin 20/01/2021
        default: return "text";
    }
}


/**
 * define los tipos para el input */
const TYPE_DEFINITION_INPUT = {
    TEXT: 0,
    NUMBER: 1,
    CHECKBOX: 2,
    DATE:3
}


/**
 * @param value // "true" | "false"
 */
const fromBooleanString = (value) => String(value).toLowerCase() === "true";