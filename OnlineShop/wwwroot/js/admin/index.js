

onload = async () => {
    await loadSaleChart();
    await loadSaleAmountChart();
    await LoadPartialView("top-products", "/Product/GetTopProducts");
    await LoadPartialView("metrics-dashboard", "/Admin/MetricsDashboard");
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


/**
 * load amount by month sales /*
 * 
 * */
const loadSaleAmountChart = async () => {
    const loading = $('.loading-sales-amount');
    const graphic = $('#sales-amount-graphic');
    try {
        const response = await axios.get("/sale/GetStadisticAmount");
        if (response.status === 200) {
            graphic.show();
            const labels = response.data.map(x => x.label);
            const data = response.data.map(x => x.data);
            new Chart(
                graphic,
                {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: "Ganancias " + new Date().getFullYear(),
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