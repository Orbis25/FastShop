


var addCity = async () => {
    const values = $("#select-search").val();
    const list = values.split(",");
    const data = {
        name: list[0],
        countryCode: list[1],
        lat: list[2],
        long: list[3]
    };

    try {
        const result = await axios.post("/country/AddCity", data);
        if (result.status === 200) {
            await LoadPartialView('tab-container', '/Country/GetAvaibleCity?code=' + list[1]);
        }
    } catch (e) {
        Swal.fire(e.response.data, "", "warning");
    }
};


var searchCity = async (code) => {
    const input = $('#name').val();
    await LoadPartialView('tab-container', `/Country/GetAvaibleCity?code=${code}&name=${input}`);
};




