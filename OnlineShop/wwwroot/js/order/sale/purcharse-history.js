
const loadMore = async (contentId, url) => {
    const content = $(`#${contentId}`);
    try {
        const result = await fetch(url, { method: "GET" });
        if (result.status === 200) {
            content.append(await result.text());
        } else if (result.status === 400) {
            return;
        }
    } catch (e) {
        content.empty();
        content.append(`Error al cargar la información, intente de nuevo mas tarde`);
    }
};
