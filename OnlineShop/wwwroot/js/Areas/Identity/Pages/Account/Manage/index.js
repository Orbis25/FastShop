


const showOrHideUploadImageButton = () => {
    const form = $("#form-upload");
    const btnShow = $("#btnUpload");
    if (form.is(":visible")) {
        form.hide();
        btnShow.show();
    } else {
        form.show();
        btnShow.hide();
    }

}

const removeDiacritics = (value) => {
    return value.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}

var getAllCountries = async (selected) => {
    try {
        const result = await axios.get("/Country/GetAvaibleCountriesJson");
        const data = (result.data.map((x) => {
            return { value: x.iso3, text: x.name }
        }));

        const select = $("#select-country");
        data.forEach(country => {
            select.append(`<option value='${removeDiacritics(country.value)}'>${country.text + " - " + country.value}</option>`);
        });


    } catch (e) {
        alert(e);
    } finally {
        $("#select-country").val(selected);
        $("#select-country").select2();
    }
};

const getAllCity = async (code, selected) => {
    try {
        const result = await axios.get("/Country/GetAvaiblesCities?name=" + code);
        const select = $("#select-city");
        select.empty();
        result.data.forEach(city => {
            select.append(`<option value='${removeDiacritics(city.name)}'>${city.name}</option>`);
        });

    } catch (e) {
        alert(e);
    } finally {
        $("#select-city").val(selected.normalize("NFD").replace(/[\u0300-\u036f]/g, ""));
        $("#select-city").select2();

    }
}