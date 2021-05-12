


var getAllCountries = async () => {
    try {
        const result = await axios.get("/Country/GetAvaibleCountriesJson");
        const data = (result.data.map((x) => {
            return { value: x.iso3, text: x.name }
        }));

        const select = $("#select-search-country");
        data.forEach(country => {
            select.append(`<option value='${country.value}'>${country.text + " - " + country.value}</option>`);
        });

    } catch (e) {
        alert(e);
    } finally {
        $(".select-search").select2();

    }
};

getAllCountries();


const getAllCity = async (code) => {
    try {
        const result = await axios.get("/Country/GetAvaiblesCities?name=" + code);
        const select = $("#select-search-city");
        select.empty();
        select.append(`<option selected value="">Seleccione una opción </option>`)
        result.data.forEach(city => {
            select.append(`<option value='${city.name}'>${city.name}</option>`);
        });

    } catch (e) {
        alert(e);
    } finally {
        $(".select-search").select2();

    }
}