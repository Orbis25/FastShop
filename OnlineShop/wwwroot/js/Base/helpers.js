


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
        if (result.status === 200) {
            content.append(await result.text());
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
var FormToJson = async (event, url) => {
    event.preventDefault();
    const data = new FormData(event.target)
    const values = Object.fromEntries(data.entries());
    return values;
}


var showImageInViewver = (id) => {
    new Viewer(document.getElementById(`${id}`), { navbar: false });
}