

const sendOrderEmail = async (id) => {
    Swal.fire("Espere un momento...");
    try {
        const result = await axios.get("/sale/SendOrderEmail/" + id);
        if (result.status === 200) {
            Swal.fire(result.data, "", "success");
        }
    } catch (e) {
        Swal.fire("Intente de nuevo mas tarde", "", "error");

    }   
}