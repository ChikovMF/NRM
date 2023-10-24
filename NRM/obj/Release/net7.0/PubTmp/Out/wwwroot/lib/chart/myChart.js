const ctx = document.getElementById('chart1');

new Chart(ctx, {
    type: 'bar',
    data: {
        labels: data1.map(row => row.label),
        datasets: [{
            label: 'Количество посылок направляющихся к местам',
            data: data1.map(row => row.count)
        }]
    },
});