function drawChart() {
    var dataChart = google.visualization.arrayToDataTable([]);
    dataChart.addColumn('string', 'ManagerName');
    dataChart.addColumn('number', 'SumAmount');

    var options = {
        title: 'Продажи менеджеров',
        is3D: true,
        'width': 500,
        'height': 400
    };

    $.ajax({
        url: '/Home/GetManagersSales',
        type: 'GET',
        success: function (result)
        {
            if (result.length) {
                $.each(result, function (index, value) {
                    dataChart.addRow([value.ManagerName, value.SumAmount]);
                });
                var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
                chart.draw(dataChart, options);
            }
        },
        contentType: "application/json",
        dataType: 'json'
    });

    //var data = google.visualization.arrayToDataTable([
    //    ['Task', 'Hours per Day'],
    //    ['Work', 11],
    //    ['Eat', 2],
    //    ['Commute', 2],
    //    ['Watch TV', 2],
    //    ['Sleep', 7]
    //]);
}