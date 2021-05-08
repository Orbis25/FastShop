

/**
 * Show alert to remove any object from db 
 * @param {any} url url preformated for delete with params exaple : "/products/remove/1"
*/
const sweetRemove = async (url = "", contentId = null, partialUrl = null) => {
    const { value } = await Swal.fire(
        {
            title: "¿Seguro que desea eliminarlo?",
            icon: "question",
            showCancelButton: true
        }
    )
    //evaluate the result
    if (value) {
        try {
            const response = await fetch(url, { method: "post" })
            if (response.status === 200) {
                await Swal.fire("Eliminado con exito", "", "success");
                if (partialUrl && contentId) {
                    LoadPartialView(contentId,partialUrl);
                } else {
                    window.location.reload();
                }
            }

            if (response.status === 400) {
                await Swal.fire(await response.text(), "", "error");
            }
        } catch (e) {
            await Swal.fire(e);
        }
    }
} 
