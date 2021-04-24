


/**
 * Print by specific id of tag
 * @param {string} id
 */
const toPrint = (id) => {
    $(id).printThis();
};

const LoadPartialView = async (contentId, url) => {
    const content = $(`#${contentId}`);
    content.append("cargando...");
    try {
        const result = await fetch(url, { method: "GET" });
        content.empty();
        if (result.status === 200) {
            content.append(await result.text());
        } else if (result.status === 400) {
            content.append(`<p>${await result.text()}</p>`);
        }
    } catch (e) {
        content.empty();
        content.append(`Error al cargar la información, intente de nuevo mas tarde`);
    }
};