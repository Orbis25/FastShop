

onload = async () => {
    await loadSaleChart();
    await LoadPartialView("top-products", "/Product/GetTopProducts");
}

/**
 * LOAD THE CHARS LINE OF SALES 
 * */
const loadSaleChart = async () => {
    const loading = $('.loading-sales');
    const graphic = $('#sales-graphic');
    try {
        const response = await axios.get("/sale/GetSalesByMothSummary");
        if (response.status === 200) {
            graphic.show();
            const labels = response.data.map(x => x.label);
            const data = response.data.map(x => x.data);
            new Chart(
                graphic,
                {
                    type: 'line',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: "VENTAS " + new Date().getFullYear(),
                            backgroundColor: 'rgb(64, 75, 105)',
                            borderColor: 'rgb(64, 75, 105)',
                            data: data,
                        }]
                    }
                }
            );
        }
    } catch (e) {
        graphic.hide();
        console.log(e);
        alert("Error loading the graphics " + e);
    } finally {
        loading.hide();
    }


}
