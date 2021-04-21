const changeState = async (event, id, state) => {
    const data = {
        Id: Number(id),
        State: (state !== 'Invalid' ? 1 : 0)
    }

    const { status } = await axios.post("/cuppon/UpdateState", data);
    if (status === 200) {
        window.location.reload();
    } else {
        Swal.fire("intente de nuevo", "", "error");
        $('#toggle').bootstrapToggle('off', true);
    }
}