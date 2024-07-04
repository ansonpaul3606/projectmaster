function setPssBarChart(_data, _labels, _Color, _chartName, _XAxis, _YAxis) {
 
    new Chart(_chartName, {
        type: 'bar',
        data: {
            datasets: [{
                data: _data,
                borderWidth: 0,
                backgroundColor: _Color
            }],
            labels: _labels
        },
        options: {
            legend: false,
            title: {
                display: true,
                text: `X: ${_XAxis} & Y: ${_YAxis}`
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            // when the floored value is the same as the value we have a whole number
                            if (isNaN(label)) {
                                return label;
                            } else if (Math.floor(label) === label) {
                                return label;
                            }
                            return label;
                        },
                    }
                }],
                xAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            if (isNaN(label)) {
                                return label;
                            }
                            // when the floored value is the same as the value we have a whole number
                            else if (Math.floor(label) === label) {
                                return label;
                            }
                            return label;
                        },
                    }
                }],
            },
        }
    });
}
function setPssHorizontalBarChart(_data, _labels, _Color, _chartName,_XAxis, _YAxis) {

    new Chart(_chartName, {
        type: 'horizontalBar',
        data: {
            datasets: [{
                data: _data,
                borderWidth: 0,
                backgroundColor: _Color
            }],
            labels: _labels
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: `X: ${_XAxis} & Y: ${_YAxis}`
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            // when the floored value is the same as the value we have a whole number
                            if (isNaN(label)) {
                                return label;
                            } else if (Math.floor(label) === label) {
                                return label;
                            }
                            return label;
                        },
                    }
                }],
                xAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            if (isNaN(label)) {
                                return label;
                            }
                            // when the floored value is the same as the value we have a whole number
                            else if (Math.floor(label) === label) {
                                return label;
                            }
                            return label;
                        },
                    }
                }],
            }
        }
    });
}
function setPssCompareBarChart(_data, _labels, _data2, _chartName, _labelData, _colorData, _XAxis, _YAxis ) {
 
    var chartData = {
        labels: _labels,
        datasets: [{
            label: _labelData[0],
            backgroundColor: _colorData[0],
            data: _data
        }, {
            label: _labelData[1],
            backgroundColor: _colorData[1],
            data: _data2
        }]
    };
    new Chart(_chartName, {
        type: "bar",
        data: chartData,
        options: {
            title: {
                display: true,
                text: `X: ${_XAxis} & Y: ${_YAxis}`
            },
            //scales: {
            //    yAxes: [{
            //        display: true
            //    }]
            //} 
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            // when the floored value is the same as the value we have a whole number
                            if (isNaN(label)) {
                                return label;
                            } else if (Math.floor(label) === label) {
                                return label;
                            }
                            return label;
                        },
                    }
                }],
                xAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            if (isNaN(label)) {
                                return label;
                            }
                            // when the floored value is the same as the value we have a whole number
                            else if (Math.floor(label) === label) {
                                return label;
                            }
                            return label;
                        },
                    }
                }],
            },
             legend: {
                position: "right",

            }
        }
    });
}
function setPssPieChart(_data, _labels, _Color, _chartName, _extraLabels, legend) {

    //var optionsUsingPluginPie = Chart.plugins.register({
    //    afterDatasetsDraw: function (chartInstance, easing) {
    //        if (chartInstance.canvas.id == _chartName) {
    //            var ctx = chartInstance.chart.ctx;

    //            chartInstance.data.datasets.forEach(function (dataset, i) {
    //                var meta = chartInstance.getDatasetMeta(i);                    
    //                if (!meta.hidden) {
    //                    meta.data.forEach(function (element, index) {
    //                        ctx.fillStyle = 'black';
    //                        var fontSize = 16;
    //                        var fontStyle = 'normal';
    //                        var fontFamily = 'Helvetica Neue';
    //                        ctx.font = Chart.helpers.fontString(fontSize, fontStyle, fontFamily);
    //                        var dataString = dataset.data[index].toString();

    //                        ctx.textAlign = 'center';
    //                        ctx.textBaseline = 'middle';
    //                        var padding = 5;
    //                        var position = element.tooltipPosition();    

    //                        if (element.hidden) {
    //                            ctx.fillText("", position.x, position.y - (fontSize / 2) - padding);
    //                        }
    //                        else {
    //                            ctx.fillText("", position.x, position.y - (fontSize / 2) - padding);
    //                            //ctx.fillText(dataString + " " + _extraLabels, position.x, position.y - (fontSize / 2) - padding);
    //                        } 
    //                    });
    //                }

    //            });
    //        }
    //    },

    //});
    new Chart(_chartName, {
        type: "pie",
        data: {
            labels: legend,
            datasets: [{
                backgroundColor: _Color,
                data: _data
            }],

        },

        options: {
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, data) {
                        var datasetLabel = '';
                        var label = data.labels[tooltipItem.index];
                        console.log(data.labels[tooltipItem.index]);
                        //return data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index]+_extraLabels;
                        return label;
                    }

                }
            }, legend: {
                position: "right",

            }
        }
    });
}
function setPssFunnelChart(_data, _Color, _chartName) {
    AmCharts.makeChart(`${_chartName}`,
        {
            "type": "funnel",
            "angle": 45,
            "balloonText": "[[title]]:<b>[[value]]</b>",
            "depth3D": 45,
            "labelPosition": "right",
            "neckHeight": "30%",
            "neckWidth": "40%",
            "startY": 50,
            "addClassNames": "true",
            "marginLeft": 15,
            "marginRight": 160,
            "startDuration": 3,
            "titleField": "title",
            "valueField": "value",
            "accessibleTitle": "",
            "allLabels": [],
            "balloon": {},
            "colors": _Color,
            "legend": {
                "enabled": false,
                "position": "top",
                "equalWidths": false,
            },
            "titles": [],
            "dataProvider": _data
        });
}
function setPssBarChart(_data, _labels, _Color, _chartName, _XAxis, _YAxis) {
    new Chart(_chartName, {
        type: 'bar',
        data: {
            datasets: [{
                data: _data,
                borderWidth: 0,
                backgroundColor: _Color
            }],
            labels: _labels
        },
        options: {
            legend: false,
            title: {
                display: true,
                text: `X: ${_XAxis} & Y: ${_YAxis}`
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            // when the floored value is the same as the value we have a whole number
                            if (isNaN(label)) {
                                return label;
                            } else if (Math.floor(label) === label) {
                                return label;
                            }
                            return label;
                        },
                    }
                }],
                xAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            if (isNaN(label)) {
                                return label;
                            }
                            // when the floored value is the same as the value we have a whole number
                            else if (Math.floor(label) === label) {
                                return label;
                            }
                            return label;
                        },
                    }
                }],
            },
        }
    });
}
function setPssLineChart(_data, _labels, _Color, _chartName, _XAxis, _YAxis) {
    new Chart(_chartName, {
        type: 'line', // changed type to 'line'
        data: {
            datasets: [{
                data: _data,
                borderWidth: 2, // added a border width for line chart
                borderColor: _Color,
                borderColor: 'rgb(75, 192, 192)',
                fill: false,
                // added border color for line chart
                backgroundColor: 'transparent' // set background color to transparent for line chart
            }],
            labels: _labels
        },
        options: {
            legend: false,
            title: {
                display: true,
                text: `X: ${_XAxis} & Y: ${_YAxis}`
            }
           
        },

    });
}
function setPssLineChart1(_data, _labels, _Color, _chartName, _XAxis, _YAxis) {
    new Chart(_chartName, {
        type: 'line',
        data: {
            datasets: [{
                data: _data,
                borderWidth: 2,
                borderColor: _Color,
                backgroundColor: 'black',
                borderColor: 'rgb(75, 192, 192)',
                fill: false,
            }],
            labels: _labels
        },
        options: {
            legend: {
                display: false
            },
            title: {
                display: true,
                text: `X: ${_XAxis} & Y: ${_YAxis}`,
                fontSize: 16,
                fontColor: 'white', // Change the font color to white for better visibility
                fontStyle: 'bold'
            },
            scales: {
                xAxes: [{
                    ticks: {
                        fontColor: 'green'
                    },
                    scaleLabel: {
                        display: true,
                        labelString: _XAxis,
                        fontColor: 'white', // Change the font color to white for better visibility
                        fontSize: 14
                    }
                }],
                yAxes: [{
                    ticks: {
                        fontColor: 'orange'
                    },
                    scaleLabel: {
                        display: true,
                        labelString: _YAxis,
                        fontColor: 'white', // Change the font color to white for better visibility
                        fontSize: 14
                    }
                }]
            },
            // Set background color for the entire chart
            backgroundColor: 'black'
        }
    });
}

