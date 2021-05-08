

var getAllCountries = async (code) => {
    try {
        const result = await axios.get("/country/GetAllCities?countryCode=" + code);
        const data = (result.data.map((x) => {
            return { lat: x.lat, lng: x.lng, text: x.name, country: x.country }
        }));
        const select = $("#select-search");
        data.forEach(city => {
            select.append(`<option value='${city.text + "," + city.country + "," + city.lat + "," + city.lng}'>${city.text}</option>`);
        });
        $(".select-search").selectpicker();
    } catch (e) {
        alert(e);
    }
}



var addCity = async () => {
    const values = $("#select-search").val();
    const list = values.split(",");
    const data = {
        name: list[0],
        countryCode: list[1],
        lat: list[2],
        long:list[3]
    };

    try {
        const result = await axios.post("/country/AddCity", data);
        if (result.status === 200) {
            await LoadPartialView('tab-container', '/Country/GetAvaibleCity?code='+list[1]);
        }
    } catch (e) {
        Swal.fire(e.response.data, "", "warning");
    }
};



