
//url countries
var API_URL = {
    GET_ALL: "https://restcountries.eu/rest/v2/all"
};

var getAllCountries = async () => {
    try {
        const result = await axios.get(API_URL.GET_ALL);
        const data = (result.data.map((x) => {
            return { value: x.alpha2Code, text: x.name }
        }));

        const select = $("#select-search");
        data.forEach(country => {
            select.append(`<option value='${country.value + "," + country.text}'>${country.text + " - " + country.value}</option>`);
        });

    } catch (e) {
        alert(e);
    } finally {
        $(".select-search").select2();

    }
}


getAllCountries();


var addCountry = async () => {
    const values = $("#select-search").val();
    const list = values.split(",");
    const data = {
        iso3: list[0],
        name: list[1]
    };

    try {
        const result = await axios.post("/country/create", data);
        if (result.status === 200) {
            await LoadPartialView('tab-container', '/Country/Index');
        }
    } catch (e) {
        Swal.fire(e.response.data, "", "warning");
    }
};


var searchCountry = async () => {
    const input = $('#name').val();
    await LoadPartialView('tab-container', `/Country/index?name=${input}`);
};

