

$(document).ready(function () {
    $.getScript("/Scripts/jspdf.min.js", function () {
        console.log("jspdf.min.js loaded")
    });
    $.getScript("/Scripts/jspdf.plugin.autotable.min.js", function () {
        console.log("jspdf.plugin.autotable.min.js is loaded")
    });
    $.getScript("/Scripts/xlsx.min.js", function () {
        console.log("xlsx.min.js is loaded")
    });
});


function exportPdf(ptitle, phead, cdata, upperhead, footerdata, ptableData, grpvariable, filename, pageSize, tableOptions, columnStyles, PageOrientation) {
    //-----------------------------------------------------------------------
     //PARAMETERS
    //--------------------------------------------------------------------------------------------------------------
    //ptitle:string => tittle of Report
    //phead:string[] => head of table as string of array ,first element should be Sl.no
    //cdata:object => Object which contains Company,Branch,EntrBy
    //upperhead:Object => {
    //    columnStyles: { 0: { halign: 'left', textColor: "#black", fontSize: 11, fillColor: false, } },
    //    margin: { top: 10 },
    //    body: [["From Date  :   " + vfromdate + "           To Date :   " + vtodate + "         Branch  :   " + vBranch]]
    //}
    //footerdata: object => {
    //    columnStyles: { 0: { halign: 'left', textColor: "#black", fontSize: 11, fillColor: false, } },
    //    margin: { top: 10 },
    //    body: [["From Date  :   " + vfromdate + "           To Date :   " + vtodate + "         Branch  :   " + vBranch]]
    //}
    //ptableData: object => { columns: [{ title: "Voucher", dataKey: "Voucher" }], rows:[]}
    //grpvariable: string => Column name which is used for the grouping
    //filename: string => downloaded file name
    //pageSize:string => page size like 'a4','a3'
    //tableOptions :Object => Object containing hook functions
    
    ////--------------------------------------------------------------------------------------------------------------
    
    
    if ( ptitle == "undefined" || ptitle ==null) {
        ptitle = "";
    }
    if (phead == "undefined" || phead == null) {
        phead = [];
    }
    if (cdata == "undefined" || cdata == null) {
        cdata = {
            Company: "",
            Branch: "",
            EntrBy:""
        };
    }
    if (upperhead == "undefined" || upperhead == null) {
        upperhead = "";
    }
    if (footerdata == "undefined" || footerdata == null) {
        footerdata = "";
    }
    if (ptableData == "undefined" || ptableData == null) {
        ptableData = {
            columns: [],
            rows: []
        }
    }
    if (grpvariable == "undefined" || grpvariable == null) {
        grpvariable = "";
    }
    if (filename == "undefined" || filename == null) {
        filename = "";
    }
    if (columnStyles == "" || columnStyles == null || columnStyles == undefined) {
        columnStyles == {}
    }
    if (PageOrientation == "" || PageOrientation == null || columnStyles == undefined) {
        PageOrientation = "p";
    }


    var headerStyles = {
        fillColor: '#2aa2ad', // Set the fill color of the header
        textColor: '#FFFFFF' // Set the text color of the header
    };


    var body = [];
    if (grpvariable != "") {
        ptableData.rows.sort(function (x, y) {
            if (x[grpvariable] < y[grpvariable]) {
                return -1;
            } if (x[grpvariable] > y[grpvariable]) {
                return 1;
            }
            return 0;
        });
  
        var num = 1;
        var grpValue;

        var colspan;
        if (phead.length == 1) {
             colspan = phead[0].length;
        } else if (phead.length == 2) {
            colspan = phead[1].length;
        }
        

        $.each(ptableData.rows, function (key, value) {
            //console.log(value[grpvariable],'value[grpvariable]');
            if (num == 1) {
                grpValue = value[grpvariable];

                body.push([{
                    content:( value[grpvariable]==null? '' : value[grpvariable]),
                    colSpan: colspan, rowSpan: 0, styles: { halign: 'left', fillColor: [204, 209, 209] }
                }]);
            }
            if (grpValue == value[grpvariable]) {
                var array = [num]
                $.each(ptableData.columns, function (key, value2) {
                    //array.push(value[value2.dataKey]);
                    if (typeof (value[value2.dataKey]) == 'number') {

                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    } else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);
                    }
                });
                body.push(array);

            } else {

                body.push([{
                    content: (value[grpvariable] == null ? '' : value[grpvariable]),
                    colSpan: colspan, rowSpan: 0, styles: { halign: 'left', fillColor: [204, 209, 209] }
                }]);
                grpValue = value[grpvariable];
               
                var array = [num]
                $.each(ptableData.columns, function (key, value2) {
                    // array.push(value[value2.dataKey]);
                
                    if (typeof (value[value2.dataKey]) == 'number') {

                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    }
                    else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);
                    }
                });


                body.push(array);
            }
            num++;

        });
    } else {
        //var body = [];
        var num = 1;
        var grpValue;
     
        var colspan = phead.length;



        $.each(ptableData.rows, function (key, value) {
           // console.log(value);
                var array = [num]
            $.each(ptableData.columns, function (key, value2) {
                //console.log(ptableData.columns);
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 
                
                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                    //array.push(value[value2.dataKey]);    //number ,string number
               // }
                    

                });
                body.push(array);

           
            num = num + 1;

        });
    }
    //console.log(ptableData.rows);
    var doc = new jsPDF(PageOrientation, 'pt', pageSize );

    doc.autoTable({
        columnStyles: {
            0: { halign: 'center', textColor: 'black', fontSize: 14, fillColor: false }
        },
        rowStyles: {},
        body: [
            [
                cdata.Company + '\n' +
                cdata.Branch + '\n' +
                '\n'+
                ptitle
            ]
        ],
        lineHeightFactor: 10,
    });

    // Calculate the height of the first table
    var firstTableHeight1 = doc.previousAutoTable.finalY;

    // Reduce the space between tables by adjusting the startY of the second table
    var secondTableStartY1 = firstTableHeight1 + 5; // Adjust the spacing value as needed


    upperhead["startY"] = secondTableStartY1;

    if (upperhead != "") {
        doc.autoTable(upperhead);
    }


    // Calculate the height of the first table
    var firstTableHeight = doc.previousAutoTable.finalY;

    // Reduce the space between tables by adjusting the startY of the second table
    var secondTableStartY = firstTableHeight + 5; // Adjust the spacing value as needed

    

    // Set the initial y-position for the table
    var startY = 30;

    doc.autoTable({
        // Configuration for the second table
        startY: secondTableStartY,
        head: phead,
        body: body,
        ...tableOptions,
        //...autotableOptions,
        theme: 'striped',
        styles: {
            lineWidth: 0.2,
            lineColor: 0,
            fontSize: 8
        },
        //headerStyles: {
        //    fillColor: 230,
        //    textColor: 0,
        //    fontStyle: 'normal',
        //    halign: 'center'

        //},
        headStyles: headerStyles,
        //columnStyles: {
        //    //0: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
        //    //1: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
        //    //2: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
        //},

        columnStyles: columnStyles,
        showHead: 'everyPage',
        rowPageBreak: 'avoid',
        pageBreak: 'auto',
        
 
        margin: { top: 30, bottom: 40 },   
        //margin: { top: 20 },
       // bodyStyles: { valign: 'top' },
        //columnStyles: { 0: { cellWidth: 60 } },
       // headStyles: { fillColor: [41, 128, 185], textColor: [255, 255, 255] },
        didDrawPage: function (data) {
            // Check if there is enough space on the current page for the table
            if (startY + doc.autoTable.previous.finalY > doc.internal.pageSize.height - 10) {
                // Add a new page
                doc.addPage();

                // Reset the y-position for the new page
                startY = 30;

                // Call the table creation function recursively on the new page
                tableOptions.startY = startY;
                doc.autoTable(tableOptions);
            }
        },
    })
    if (footerdata != null) {
        doc.autoTable(footerdata);
    };

     //addFooters(doc);
     //addline(doc);

   



   
    doc.autoTable({
        didDrawPage: function (data) {

            if (
                doc.internal.getCurrentPageInfo().pageNumber ===
                doc.internal.getNumberOfPages()
            ) {

                var date = new Date();
                var formattedDate =
                    date.getDate() +
                    '-' +
                    (date.getMonth() + 1) +
                    '-' +
                    date.getFullYear() +" " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
                doc.setFontSize(9);
                doc.text(
                    cdata.EntrBy + '    Printed on ' + formattedDate,
                    data.settings.margin.left,
                    doc.internal.pageSize.height - 10
                );
            }
        }
    });

    doc.autoTable({
        didDrawPage: function (data) {

            if (
                doc.internal.getCurrentPageInfo().pageNumber ===
                doc.internal.getNumberOfPages()
            ) {

                var height = doc.internal.pageSize.height - 24;
                var pageWidth = doc.internal.pageSize.getWidth();
                doc.setLineWidth(0.1);
                doc.line(15, height, pageWidth - 15, height);
            }
        }
    });

    doc.autoTable({
        didDrawPage: function (data) {

            const pageCount = doc.internal.getNumberOfPages()

            doc.setFont('helvetica', 'italic')
            doc.setFontSize(8)
            const pageWidth = doc.internal.pageSize.width;
            const pageHeight = doc.internal.pageSize.height;
            for (var i = 1; i <= pageCount; i++) {
                let horizontalPos = pageWidth / 2;
                let verticalPos = pageHeight - 10;

                doc.setPage(i)
                doc.text('Page ' + String(i) + ' of ' + String(pageCount), pageWidth - 55, verticalPos, {
                    align: 'center'
                })
            }
        }
    });

    
    doc.save(filename +'.pdf')

}


var addFooters = (doc) => {
    const pageCount = doc.internal.getNumberOfPages()

    doc.setFont('helvetica', 'italic')
    doc.setFontSize(8)
    const pageWidth = doc.internal.pageSize.width;
    const pageHeight = doc.internal.pageSize.height;
    for (var i = 1; i <= pageCount; i++) {
        let horizontalPos = pageWidth / 2;
        let verticalPos = pageHeight - 10;

        doc.setPage(i)
        doc.text('Page ' + String(i) + ' of ' + String(pageCount), pageWidth - 55, verticalPos, {
            align: 'center'
        })
    }
}

var addline = (doc) => {
    doc.autoTable({
        didDrawPage: function (data) {

            if (
                doc.internal.getCurrentPageInfo().pageNumber ===
                doc.internal.getNumberOfPages()
            ) {
                var height = doc.internal.pageSize.height - 24;
                var pageWidth = doc.internal.pageSize.getWidth();
                doc.setLineWidth(0.1);
                doc.line(15, height, pageWidth - 15, height);
            }
        }
    });

}




function generateExell(ptitle, phead, ptableData, mergeRangesdata, grpvariable, filename) {
    //-----------------------------------------------------------------------
    //PARAMETERS
    //--------------------------------------------------------------------------------------------------------------
    //ptitle:string => tittle of Report
    //phead:string[] => head of table as string of array ,first element should be Sl.no

    //ptableData: object => { columns: [{ title: "Voucher", dataKey: "Voucher" }], rows:[]}
    //mergeRangesdata: array of object =>
    //grpvariable: string => Column name which is used for the grouping
    //filename: string => downloaded file name


    ////--------------------------------------------------------------------------------------------------------------

    //debugger;
   // console.log(filter,"----filter----")
    console.log(ptableData, "ptableData  start")
    let pheadlen = phead.length;
    var arr = [ptitle.toUpperCase()];
    //if (filter) {
    //    alert("")
    //    arr.push({ filter });
    //}
    var mainArr = [];
    var repclmNo;
    var body = [];
    var mergeRanges = [];

    if (mergeRangesdata == "" || mergeRangesdata == null) {
        mergeRangesdata = {};
    };

    if (grpvariable != "" && grpvariable !=null) {
        ptableData.rows.sort(function (x, y) {
            if (x[grpvariable] < y[grpvariable]) {
                return -1;
            } if (x[grpvariable] > y[grpvariable]) {
                return 1;
            }
            return 0;
        });

        var num = 1;
        var grpValue;

        var colspan;
        if (phead.length == 1) {
            colspan = phead[0].length;
        } else if (phead.length == 2) {
            colspan = phead[1].length;
        }

        ///header pushing

        if (pheadlen == 1) {
            repclmNo = phead[0].length - 1;

            for (let i = 1; i < phead[0].length; i++) {
                arr.push("");
            }
            console.log(arr, 'arr1-exportFile');
            body.push(arr, phead[0])
        } else if (pheadlen == 2) {
            repclmNo = phead[1].length - 1;

            for (let i = 1; i < phead[1].length; i++) {
                arr.push("");
            }
            console.log(arr, 'arr2-exportFile');
            body.push(arr, phead[0], phead[1])
        };


        

         mergeRanges = [{ s: { r: 0, c: 0 }, e: { r: 0, c: repclmNo } }];

        var rowNo = 0;
        $.each(ptableData.rows, function (key, value) {
            //console.log(key,"key");
            if (num == 1) {
                grpValue = value[grpvariable];

               // console.log(key, "key -n1");
                //body.push(["", value[grpvariable]]);
                body.push(["", (value[grpvariable] == null ? '' : value[grpvariable])]);
                if (pheadlen == 1) {
                    rowNo = key + 2;
                } else if (pheadlen == 2) {
                    rowNo = key + 3;
                }
                
                mergeRanges.push({ s: { r: rowNo, c: 1 }, e: { r: rowNo, c: repclmNo } });
               // console.log({ s: { r: rowNo, c: 1 }, e: { r: rowNo, c: repclmNo } }, "first conditions");
                rowNo++;
            }
            if (grpValue == value[grpvariable]){
                var array = [num]
                $.each(ptableData.columns, function (key, value2) {
                   // array.push(value[value2.dataKey]);
                    if (typeof (value[value2.dataKey]) == 'number') {

                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    } else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);   ///new line of code
                    }

                });
                body.push(array);
                //console.log(key, "key");
                rowNo++;

            } else {

                
               // debugger;
                //body.push(["", value[grpvariable]]);
                body.push(["", (value[grpvariable] == null ? '' : value[grpvariable]) ]);
                
                mergeRanges.push({ s: { r: rowNo, c: 1 }, e: { r: rowNo, c: repclmNo } });
               // console.log({ s: { r: rowNo, c: 1 }, e: { r: rowNo, c: repclmNo } },"else conditions");
                grpValue = value[grpvariable];
                rowNo++;

                var array = [num]
                $.each(ptableData.columns, function (key, value2) {
                   // array.push(value[value2.dataKey]);
                    if (typeof (value[value2.dataKey]) == 'number') {
                        //alert(value[value2.dataKey])
                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                        //alert((value[value2.dataKey]).toLocaleString('en-IN', {
                        //    useGrouping: true,
                        //    minimumFractionDigits: 2,
                        //    maximumFractionDigits: 2,
                        //}))
                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    } else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);
                    }

                });
                body.push(array);
                rowNo++;
            }
            num++;
           
        });
      //  console.log(body, "body")
    } else {

        ///header pushing

        if (pheadlen == 1) {
            repclmNo = phead[0].length - 1;

            for (let i = 1; i < phead[0].length; i++) {
                arr.push("");
            }
          //  console.log(arr, 'arr1-exportFile');
            body.push(arr, phead[0])
        } else if (pheadlen == 2) {
            repclmNo = phead[1].length - 1;

            for (let i = 1; i < phead[1].length; i++) {
                arr.push("");
            }
         //   console.log(arr, 'arr2-exportFile');
            body.push(arr, phead[0], phead[1])
        };

        

        var mergeRanges = [{ s: { r: 0, c: 0 }, e: { r: 0, c: repclmNo } }];
        ///data of table

        $.each(ptableData.rows, function (key, value) {
            let array = [key += 1];
            // console.log(key, value, "key","ptableData.rows loop");
            $.each(ptableData.columns, function (key, value2) {
                // console.log(value2, "ptableData.colum loop");
               // array.push(value[value2.dataKey]);
                //debugger;
                if (typeof (value[value2.dataKey]) == 'number') {
                   // alert("number enterd");
                    
                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey]==null) {
                    array.push("");
                }

            });
            body.push(array);
        });
   


    };





  //  console.log(mainArr, "mainArr---last");

    var workbook = XLSX.utils.book_new();

    var worksheet = XLSX.utils.aoa_to_sheet(body);

   // // Merge multiple cells
   
  
    $.each(mergeRangesdata, function (key, value) {
        mergeRanges.push(value);
    });

  //  console.log(mergeRanges,"mergeRanges","ln497")

   // console.log(ptableData.rows.length, "ptableData.rows.length");
  
    if (!worksheet['!merges']) worksheet['!merges'] = [];
    worksheet['!merges'] = worksheet['!merges'].concat(mergeRanges);

    //// Apply style to the report title cell
    //var reportTitleCell = 'A1';
    //worksheet[reportTitleCell].s = {
    //    font: { bold: true, color: { rgb: 'FF0000' } },
    //    alignment: { horizontal: 'center', vertical: 'center' }
    //};

    // Add the worksheet to the workbook
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');

    //var reportName = filename.toLowerCase() + '.xlsx';
    var reportName = filename.trim()  + '.xlsx';

    // Save the workbook as an Excel file
    XLSX.writeFile(workbook, reportName);
}
function exportPdf2(inputdata) {


    //inputdata = {
    //    PageOrientation:,
    //    pageSize:,
    //    pageSize:,
    //    cdata: {},
    //    phead: [],
    //    upperhead:,
    //    footerdata:,
    //    tableOptions: {},
    //    columnStyles: {},
    //    grpvariable: "",
    //    ptableData: {},
    //    isSort :boolan 


    //}





    if (!inputdata.ptableData) {
        inputdata.ptableData = {
            columns: [],
            rows: []
        }
    }

    if (!inputdata["columnStyles"]) inputdata["columnStyles"] = {}




    var headerStyles = {
        fillColor: '#2aa2ad', // Set the fill color of the header
        textColor: '#FFFFFF' // Set the text color of the header
    };


    // debugger
    var body = [];
    if (inputdata.isSort == null || inputdata.isSort == undefined || inputdata.isSort === "") {
        inputdata.isSort = true;
    }

    if (inputdata.grpvariable) {

        if (inputdata.isSort) {
            inputdata["ptableData"].rows.sort(function (x, y) {
                if (x[inputdata.grpvariable] < y[inputdata.grpvariable]) {
                    return -1;
                } if (x[inputdata.grpvariable] > y[inputdata.grpvariable]) {
                    return 1;
                }
                return 0;
            });
        }


        var num = 1;
        var grpValue;

        let colspan;
        if (Array.isArray(inputdata.phead)) {
            if (inputdata.phead.length == 1) {
                colspan = inputdata.phead[0].length;
            } else if (inputdata.phead.length == 2) {
                colspan = inputdata.phead[1].length;
            }
        }



        $.each(inputdata["ptableData"].rows, function (key, value) {
            //console.log(value[grpvariable],'value[grpvariable]');
            if (num == 1) {
                grpValue = value[inputdata.grpvariable];

                body.push([{
                    content: (value[inputdata.grpvariable] == null ? '' : value[inputdata.grpvariable]),
                    colSpan: colspan, rowSpan: 0, styles: { halign: 'left', fillColor: [204, 209, 209] }
                }]);
            }
            if (grpValue == value[inputdata.grpvariable]) {
                var array = [num]
                $.each(inputdata["ptableData"].columns, function (key, value2) {
                    //array.push(value[value2.dataKey]);
                    if (typeof (value[value2.dataKey]) == 'number') {

                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    } else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);
                    }
                });
                body.push(array);

            } else {

                body.push([{
                    content: (value[inputdata.grpvariable] == null ? '' : value[inputdata.grpvariable]),
                    colSpan: colspan, rowSpan: 0, styles: { halign: 'left', fillColor: [204, 209, 209] }
                }]);
                grpValue = value[inputdata.grpvariable];

                var array = [num]
                $.each(inputdata["ptableData"].columns, function (key, value2) {
                    // array.push(value[value2.dataKey]);

                    if (typeof (value[value2.dataKey]) == 'number') {

                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    }
                    else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);
                    }
                });


                body.push(array);
            }
            num++;

        });
    } else {
        //var body = [];
        var num = 1;
        var grpValue;

        var colspan = inputdata.phead.length;



        $.each(inputdata["ptableData"].rows, function (key, value) {
            // console.log(value);
            var array = [num]
            $.each(inputdata["ptableData"].columns, function (key, value2) {
                //console.log(ptableData.columns);
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body.push(array);


            num = num + 1;

        });
    }

    if (inputdata.pushdata) {
        //console.log(body)
        inputdata.pushdata.forEach((i) => {
            body.push(i)
        })
    }

    if (!inputdata.pageSize) inputdata.pageSize = 'a4';
    if (!inputdata.PageOrientation) inputdata.pageSize = 'p';
    if (!inputdata["ptitle"]) inputdata["ptitle"] = '';

    var doc = new jsPDF(inputdata.PageOrientation, 'pt', inputdata.pageSize);

    if (inputdata.cdata) {
        doc.autoTable({
            columnStyles: {
                0: { halign: 'center', textColor: 'black', fontSize: 14, fillColor: false }
            },
            rowStyles: {},
            body: [
                [
                    inputdata.cdata.Company + '\n' +
                    inputdata.cdata.Branch + '\n' +
                    '\n' +
                    inputdata["ptitle"]
                ]
            ],
            lineHeightFactor: 10,
        });
    }


    if (inputdata.upperhead && typeof inputdata.upperhead === "object") {
        inputdata.upperhead["startY"] = secondTableStartY1;
        doc.autoTable(inputdata.upperhead);
    }


    // Set the initial y-position for the table
    var startY = 30;

    //Uppertable 

    // Calculate the height of the first table
    var firstTableHeight1 = doc.previousAutoTable.finalY;

    // Reduce the space between tables by adjusting the startY of the second table
    var secondTableStartY1 = firstTableHeight1 + 10; // Adjust the spacing value as needed
    if (inputdata.uppertable) {
        inputdata.uppertable.startY = secondTableStartY1;
    }


    //  debugger
    if (inputdata.uppertable) { doc.autoTable(inputdata.uppertable) }

    // Calculate the height of the first table
    var firstTableHeight = doc.previousAutoTable.finalY;

    // Reduce the space between tables by adjusting the startY of the second table
    var secondTableStartY = firstTableHeight + 10; // Adjust the spacing value as needed

    //mian table 
    doc.autoTable({
        // Configuration for the second table
        startY: secondTableStartY,
        head: inputdata.phead,
        body: body,
        ...inputdata["tableOptions"],
        //...autotableOptions,
        theme: 'striped',
        styles: {
            lineWidth: 0.2,
            lineColor: 0,
            fontSize: 8
        },
        //headerStyles: {
        //    fillColor: 230,
        //    textColor: 0,
        //    fontStyle: 'normal',
        //    halign: 'center'

        //},
        headStyles: headerStyles,
        //columnStyles: {
        //    //0: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
        //    //1: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
        //    //2: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
        //},

        columnStyles: inputdata["columnStyles"],
        showHead: 'everyPage',
        rowPageBreak: 'avoid',
        pageBreak: 'auto',


        margin: { top: 30, bottom: 40 },
        //margin: { top: 20 },
        // bodyStyles: { valign: 'top' },
        //columnStyles: { 0: { cellWidth: 60 } },
        // headStyles: { fillColor: [41, 128, 185], textColor: [255, 255, 255] },
        didDrawPage: function (data) {
            // Check if there is enough space on the current page for the table
            if (startY + doc.autoTable.previous.finalY > doc.internal.pageSize.height - 10) {
                // Add a new page
                doc.addPage();

                // Reset the y-position for the new page
                startY = 30;

                // Call the table creation function recursively on the new page
                tableOptions.startY = startY;
                doc.autoTable(tableOptions);
            }
        },
    })


    var firstTableHeight3 = doc.previousAutoTable.finalY;
    var secondTableStartY3 = firstTableHeight3 + 10;
    if (inputdata.footerdata && typeof inputdata.footerdata === "object") {
        inputdata.footerdata["startY"] = secondTableStartY3;
        doc.autoTable(inputdata.footerdata);
    }

    doc.autoTable({
        didDrawPage: function (data) {

            if (
                doc.internal.getCurrentPageInfo().pageNumber ===
                doc.internal.getNumberOfPages()
            ) {

                var date = new Date();
                var formattedDate =
                    date.getDate() +
                    '-' +
                    (date.getMonth() + 1) +
                    '-' +
                    date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
                doc.setFontSize(9);
                doc.text(
                    inputdata.cdata.EntrBy + '    Printed on ' + formattedDate,
                    data.settings.margin.left,
                    doc.internal.pageSize.height - 10
                );
            }
        }
    });

    doc.autoTable({
        didDrawPage: function (data) {

            if (
                doc.internal.getCurrentPageInfo().pageNumber ===
                doc.internal.getNumberOfPages()
            ) {

                var height = doc.internal.pageSize.height - 24;
                var pageWidth = doc.internal.pageSize.getWidth();
                doc.setLineWidth(0.1);
                doc.line(15, height, pageWidth - 15, height);
            }
        }
    });

    doc.autoTable({
        didDrawPage: function (data) {

            const pageCount = doc.internal.getNumberOfPages()

            doc.setFont('helvetica', 'italic')
            doc.setFontSize(8)
            const pageWidth = doc.internal.pageSize.width;
            const pageHeight = doc.internal.pageSize.height;
            for (var i = 1; i <= pageCount; i++) {
                let horizontalPos = pageWidth / 2;
                let verticalPos = pageHeight - 10;

                doc.setPage(i)
                doc.text('Page ' + String(i) + ' of ' + String(pageCount), pageWidth - 55, verticalPos, {
                    align: 'center'
                })
            }
        }
    });

    if (!inputdata.filename) inputdata.filename = '';
    doc.save(inputdata.filename + '.pdf')

};
function exportPdfMultiple(inputdata) {
   

    //inputdata = {
    //    PageOrientation:,
    //    pageSize:,
    //    pageSize:,
    //    cdata: {},
    //    phead: [],
    //    upperhead:,
    //    footerdata:,
    //    tableOptions: {},
    //    columnStyles: {},
    //    grpvariable: "",
    //    ptableData: {},
    //    isSort :boolan 


    //}


    
   
 
    if (!inputdata.ptableData) {
        inputdata.ptableData = {
            columns: [],
            rows: []
        }
    }
   
    if (!inputdata["columnStyles"]) inputdata["columnStyles"] ={}
  
  


    var headerStyles = {
        fillColor: '#2aa2ad', // Set the fill color of the header
        textColor: '#FFFFFF', // Set the text color of the header
        
    };


   // debugger
    var body = [];
    var body1 = [];
    var body2 = [];
    var body3 = [];
    var body4 = [];
    var body5 = [];
    var body6 = [];
    var body7 = [];
    var body8 = [];
    var body9 = [];
    if (inputdata.isSort == null || inputdata.isSort == undefined || inputdata.isSort === "") {
        inputdata.isSort = true;
    }

    if (inputdata.grpvariable) {

        if (inputdata.isSort) {
            inputdata["ptableData"].rows.sort(function (x, y) {
                if (x[inputdata.grpvariable] < y[inputdata.grpvariable]) {
                    return -1;
                } if (x[inputdata.grpvariable] > y[inputdata.grpvariable]) {
                    return 1;
                }
                return 0;
            });
        }
       
       
        var num = 1;
        var grpValue;

        let colspan;
        if (Array.isArray(inputdata.phead)) {
            if (inputdata.phead.length == 1) {
                colspan = inputdata.phead[0].length;
            } else if (inputdata.phead.length == 2) {
                colspan = inputdata.phead[1].length;
            }
        }
        


        $.each(inputdata["ptableData"].rows, function (key, value) {
            //console.log(value[grpvariable],'value[grpvariable]');
            if (num == 1) {
                grpValue = value[inputdata.grpvariable];

                body.push([{
                    content: (value[inputdata.grpvariable] == null ? '' : value[inputdata.grpvariable]),
                    colSpan: colspan, rowSpan: 0, styles: { halign: 'left', fillColor: [204, 209, 209] }
                }]);
            }
            if (grpValue == value[inputdata.grpvariable]) {
                var array = [num]
                $.each(inputdata["ptableData"].columns, function (key, value2) {
                    //array.push(value[value2.dataKey]);
                    if (typeof (value[value2.dataKey]) == 'number') {

                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    } else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);
                    }
                });
                body.push(array);

            } else {

                body.push([{
                    content: (value[inputdata.grpvariable] == null ? '' : value[inputdata.grpvariable]),
                    colSpan: colspan, rowSpan: 0, styles: { halign: 'left', fillColor: [204, 209, 209] }
                }]);
                grpValue = value[inputdata.grpvariable];

                var array = [num]
                $.each(inputdata["ptableData"].columns, function (key, value2) {
                    // array.push(value[value2.dataKey]);

                    if (typeof (value[value2.dataKey]) == 'number') {

                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    }
                    else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);
                    }
                });


                body.push(array);
            }
            num++;

        });
    }
    else {
        //var body = [];
        var num = 1;
        var grpValue;

        var colspan = inputdata.phead.length;



        $.each(inputdata["ptableData"].rows, function (key, value) {
          
            var array = [num]
            $.each(inputdata["ptableData"].columns, function (key, value2) {
                console.log(value2,'value2454');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData1"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData1"].columns, function (key, value2) {
                console.log(value2,'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body1.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData2"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData2"].columns, function (key, value2) {
                console.log(value2, 'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body2.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData3"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData3"].columns, function (key, value2) {
                console.log(value2, 'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body3.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData4"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData4"].columns, function (key, value2) {
                console.log(value2, 'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body4.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData5"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData5"].columns, function (key, value2) {
                console.log(value2, 'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body5.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData6"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData6"].columns, function (key, value2) {
                console.log(value2, 'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body6.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData7"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData7"].columns, function (key, value2) {
                console.log(value2, 'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body7.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData8"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData8"].columns, function (key, value2) {
                console.log(value2, 'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body8.push(array);


            num = num + 1;

        });
        $.each(inputdata["ptableData9"].rows, function (key, value) {

            var array = [num]
            $.each(inputdata["ptableData9"].columns, function (key, value2) {
                console.log(value2, 'value356');
                //if (Object.is(parseFloat(value[value2.dataKey]) ,NaN)) {  //string with character
                //    array.push(value[value2.dataKey]);
                //} else {   //Number string 

                if (typeof (value[value2.dataKey]) == 'number') {

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

                //array.push(value[value2.dataKey]);    //number ,string number
                // }


            });
            body9.push(array);


            num = num + 1;

        });
    }
   
    if (inputdata.pushdata) {
        //console.log(body)
        inputdata.pushdata.forEach((i) => {
            body.push(i)
        })
    }

    if (!inputdata.pageSize) inputdata.pageSize = 'a4';
    if (!inputdata.PageOrientation) inputdata.pageSize = 'p';
    if (!inputdata["ptitle"]) inputdata["ptitle"] = '';

    const  doc = new jsPDF(inputdata.PageOrientation, 'pt', inputdata.pageSize);

    if (inputdata.cdata) {
        doc.autoTable({
            columnStyles: {
                0: { halign: 'center', textColor: 'black', fontSize: 14, fillColor: false }
            },
            rowStyles: {},
            body: [
                [
                    inputdata.cdata.Company + '\n' +
                    inputdata.cdata.Branch + '\n' +
                    '\n' +
                    inputdata["ptitle"]
                ]
            ],
            lineHeightFactor: 10,
        });
    }

    

    if (inputdata.upperhead && typeof inputdata.upperhead === "object") {
        inputdata.upperhead["startY"] = secondTableStartY1;
        doc.autoTable(inputdata.upperhead);
    }
    

    // Set the initial y-position for the table
    var startY = 30;

    //Uppertable 

    // Calculate the height of the first table
    var firstTableHeight1 = doc.previousAutoTable.finalY;

    // Reduce the space between tables by adjusting the startY of the second table
    var secondTableStartY1 = firstTableHeight1 + 10; // Adjust the spacing value as needed
    if (inputdata.uppertable) {
        inputdata.uppertable.startY = secondTableStartY1;
    }
   

  //  debugger
    if (inputdata.uppertable) {doc.autoTable(inputdata.uppertable)}

    // Calculate the height of the first table
    var firstTableHeight = doc.previousAutoTable.finalY;

    // Reduce the space between tables by adjusting the startY of the second table
    var secondTableStartY = firstTableHeight + 10; // Adjust the spacing value as needed

    //mian table 
    
    

    $.each(inputdata.ptableDataArray, function (i, valuear) {
        console.log(i, 'i')
        var BodyType = [];
        var Heading;
        var HeaderName;
        var TableHead;
        if (i == 0) {
            BodyType = body
            Heading = inputdata.phead
            HeaderName='Team Details'
        }
        else if (i == 1) {
            BodyType = body1
            Heading = inputdata.phead1
            HeaderName = 'Stage Details'
        }
        else if (i == 2) {
            BodyType = body2
            Heading = inputdata.phead2
            HeaderName = 'Material Allocation'
        }
        else if (i == 3) {
            BodyType = body3
            Heading = inputdata.phead3
            HeaderName = 'Material Usage'
        }
        else if (i == 4) {
            BodyType = body4
            Heading = inputdata.phead4
            HeaderName = 'Material Wastage'
        }
        else if (i == 5) {
            BodyType = body5
            Heading = inputdata.phead5
            HeaderName = 'Material Damage'
        }
        else if (i == 6) {
            BodyType = body6
            Heading = inputdata.phead6
            HeaderName = 'Extra Work'
        }
        else if (i == 7) {
            BodyType = body7
            Heading = inputdata.phead7
            HeaderName = 'Project Transaction'
        }
        else if (i == 8) {
            BodyType = body8
            Heading = inputdata.phead8
            HeaderName = 'Resource Details'
        }
        else if (i == 9) {
            BodyType = body9
            Heading = inputdata.phead9
            HeaderName = 'Project Billing'
        }
       
        console.log(valuear, 'valuear');
        var firstTableHeight2 = doc.previousAutoTable.finalY;
        var secondTableStartY2 = firstTableHeight2 + 31;
        doc.setFontSize(10);
        doc.text(HeaderName, 40, secondTableStartY2 - 10,);
        doc.autoTable({
            // Configuration for the second table
            startY: secondTableStartY2+10,
            head: Heading,
            body: BodyType,
            ...inputdata["tableOptions"],
            //...autotableOptions,
            theme: 'striped',
            styles: {
                lineWidth: 0.2,
                lineColor: 0,
                fontSize: 8
            },
            //headerStyles: {
            //    fillColor: 230,
            //    textColor: 0,
            //    fontStyle: 'normal',
            //    halign: 'center'

            //},
            headStyles: headerStyles,
            //columnStyles: {
            //    //0: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
            //    //1: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
            //    //2: { cellWidth: "auto", cellStyles: { overflow: 'linebreak' } },
            //},

            columnStyles: inputdata["columnStyles"],
            showHead: 'everyPage',
            rowPageBreak: 'avoid',
            pageBreak: 'auto',
            

            margin: { top: 30, bottom: 40 },
            //margin: { top: 20 },
            // bodyStyles: { valign: 'top' },
            //columnStyles: { 0: { cellWidth: 60 } },
            // headStyles: { fillColor: [41, 128, 185], textColor: [255, 255, 255] },
            didDrawPage: function (data) {
                // Check if there is enough space on the current page for the table
                if (startY + doc.autoTable.previous.finalY > doc.internal.pageSize.height - 10) {
                    // Add a new page
                    doc.addPage();

                    // Reset the y-position for the new page
                    startY = 30;
                   
                    // Call the table creation function recursively on the new page
                    tableOptions.startY = startY;
                    doc.autoTable(tableOptions);
                }
            },
        })

    });

    
   

    var firstTableHeight3 = doc.previousAutoTable.finalY;
    var secondTableStartY3 = firstTableHeight3 + 10; 
    if (inputdata.footerdata && typeof inputdata.footerdata === "object") {
        inputdata.footerdata["startY"] = secondTableStartY3;
        doc.autoTable(inputdata.footerdata);
    }

    doc.autoTable({
        didDrawPage: function (data) {

            if (
                doc.internal.getCurrentPageInfo().pageNumber ===
                doc.internal.getNumberOfPages()
            ) {

                var date = new Date();
                var formattedDate =
                    date.getDate() +
                    '-' +
                    (date.getMonth() + 1) +
                    '-' +
                    date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
                doc.setFontSize(9);
                doc.text(
                    inputdata.cdata.EntrBy + '    Printed on ' + formattedDate,
                    data.settings.margin.left,
                    doc.internal.pageSize.height - 10
                );
            }
        }
    });

    doc.autoTable({
        didDrawPage: function (data) {

            if (
                doc.internal.getCurrentPageInfo().pageNumber ===
                doc.internal.getNumberOfPages()
            ) {

                var height = doc.internal.pageSize.height - 24;
                var pageWidth = doc.internal.pageSize.getWidth();
                doc.setLineWidth(0.1);
                doc.line(15, height, pageWidth - 15, height);
            }
        }
    });

    doc.autoTable({
        didDrawPage: function (data) {

            const pageCount = doc.internal.getNumberOfPages()

            doc.setFont('helvetica', 'italic')
            doc.setFontSize(8)
            const pageWidth = doc.internal.pageSize.width;
            const pageHeight = doc.internal.pageSize.height;
            for (var i = 1; i <= pageCount; i++) {
                let horizontalPos = pageWidth / 2;
                let verticalPos = pageHeight - 10;

                doc.setPage(i)
                doc.text('Page ' + String(i) + ' of ' + String(pageCount), pageWidth - 55, verticalPos, {
                    align: 'center'
                })
            }
        }
    });

    if (!inputdata.filename) inputdata.filename = '';
    doc.save(inputdata.filename + '.pdf')

};

function generateExell2(inputobj) {

    //debugger
    console.log(inputobj.ptableData, "ptableData  start")
    let pheadlen = inputobj.phead.length;
    var arr = [inputobj.ptitle.toUpperCase()];
    //if (filter) {
    //    alert("")
    //    arr.push({ filter });
    //}
    var mainArr = [];
    var repclmNo;
    var body = [];
    var mergeRanges = [];

    if (!inputobj.mergeRangesdata)  inputobj.mergeRangesdata = {};
   

    if (inputobj.grpvariable != "" && inputobj.grpvariable != null) {



        if (inputobj.isSort == null || inputobj.isSort == undefined || inputobj.isSort === "") {
            inputobj.isSort = true;
        }

        if (inputobj.isSort) {
            inputobj.ptableData.rows.sort(function (x, y) {
                if (x[inputobj.grpvariable] < y[inputobj.grpvariable]) {
                    return -1;
                } if (x[inputobj.grpvariable] > y[inputobj.grpvariable]) {
                    return 1;
                }
                return 0;
            });
        }
        inputobj.ptableData.rows.sort(function (x, y) {
            if (x[inputobj.grpvariable] < y[inputobj.grpvariable]) {
                return -1;
            } if (x[inputobj.grpvariable] > y[inputobj.grpvariable]) {
                return 1;
            }
            return 0;
        });

        var num = 1;
        var grpValue;

        var colspan;
        if (inputobj.phead.length == 1) {
            colspan = inputobj.phead[0].length;
        } else if (inputobj.phead.length == 2) {
            colspan = inputobj.phead[1].length;
        }

        ///header pushing

        if (pheadlen == 1) {
            repclmNo = inputobj.phead[0].length - 1;

            for (let i = 1; i < inputobj.phead[0].length; i++) {
                arr.push("");
            }
            console.log(arr, 'arr1-exportFile');
            body.push(arr, inputobj.phead[0])
        } else if (pheadlen == 2) {
            repclmNo = inputobj.phead[1].length - 1;

            for (let i = 1; i < inputobj.phead[1].length; i++) {
                arr.push("");
            }
            console.log(arr, 'arr2-exportFile');
            body.push(arr, inputobj.phead[0], inputobj.phead[1])
        };




        mergeRanges = [{ s: { r: 0, c: 0 }, e: { r: 0, c: repclmNo } }];

        var rowNo = 0;
        $.each(inputobj.ptableData.rows, function (key, value) {
            //console.log(key,"key");
            if (num == 1) {
                grpValue = value[inputobj.grpvariable];

                // console.log(key, "key -n1");
                //body.push(["", value[grpvariable]]);
                body.push(["", (value[inputobj.grpvariable] == null ? '' : value[inputobj.grpvariable])]);

                if (pheadlen == 1) {
                    rowNo = key + 2;
                } else if (pheadlen == 2) {
                    rowNo = key + 3;
                }

                mergeRanges.push({ s: { r: rowNo, c: 1 }, e: { r: rowNo, c: repclmNo } });
                // console.log({ s: { r: rowNo, c: 1 }, e: { r: rowNo, c: repclmNo } }, "first conditions");
                rowNo++;
            }
            if (grpValue == value[inputobj.grpvariable]) {
                var array = [num]
                $.each(inputobj.ptableData.columns, function (key, value2) {
                    // array.push(value[value2.dataKey]);
                    if (typeof (value[value2.dataKey]) == 'number') {

                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    } else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);   ///new line of code
                    }

                });
                body.push(array);
                //console.log(key, "key");
                rowNo++;

            } else {


                // debugger;
                //body.push(["", value[grpvariable]]);
                body.push(["", (value[inputobj.grpvariable] == null ? '' : value[inputobj.grpvariable])]);

                mergeRanges.push({ s: { r: rowNo, c: 1 }, e: { r: rowNo, c: repclmNo } });
                // console.log({ s: { r: rowNo, c: 1 }, e: { r: rowNo, c: repclmNo } },"else conditions");
                grpValue = value[inputobj.grpvariable];
                rowNo++;

                var array = [num]
                $.each(inputobj.ptableData.columns, function (key, value2) {
                    // array.push(value[value2.dataKey]);
                    if (typeof (value[value2.dataKey]) == 'number') {
                        //alert(value[value2.dataKey])
                        array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                            useGrouping: true,
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                        }));

                        //alert((value[value2.dataKey]).toLocaleString('en-IN', {
                        //    useGrouping: true,
                        //    minimumFractionDigits: 2,
                        //    maximumFractionDigits: 2,
                        //}))
                    } else if (typeof (value[value2.dataKey]) == 'string') {
                        //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                        array.push(value[value2.dataKey]);
                    } else if (value[value2.dataKey] == null) {
                        array.push("");
                    } else {
                        array.push(value[value2.dataKey]);
                    }

                });
                body.push(array);
                rowNo++;
            }
            num++;

        });
        //  console.log(body, "body")
    } else {

        ///header pushing
        if (pheadlen == 1) {
            repclmNo = inputobj.phead[0].length - 1;

            for (let i = 1; i < inputobj.phead[0].length; i++) {
                arr.push("");
            }
            //  console.log(arr, 'arr1-exportFile');
            body.push(arr)
           // body.push(arr, inputobj.phead[0])
        } else if (pheadlen == 2) {
            repclmNo = inputobj.phead[1].length - 1;

            for (let i = 1; i < inputobj.phead[1].length; i++) {
                arr.push("");
            }
            //   console.log(arr, 'arr2-exportFile');
            //body.push(arr, inputobj.phead[0], inputobj.phead[1]);
            body.push(arr);
        };
        //debugger

        //Merge Header cells
        var mergeRanges = [
            { s: { r: 0, c: 0 }, e: { r: 0, c: repclmNo } },
           
        ];

        //ifheadertable present
        if (inputobj.headertable) {
            //const bhead = ['Name', 'Email', 'Country'];
            //const bbody = [
            //    ['David', 'david@example.com', 'Sweden'],
            //    ['Castille', 'castille@example.com', 'Spain'],

            //];
          //  debugger
            const item = [...inputobj.headertable.bhead, ...inputobj.headertable.bbody];
            

            item.forEach((i) => {
                let arr = [];
                i.forEach((j) => {
                    arr.push(j);
                });
                body.push(arr);
            });
           
        };


        //tbaleheader push
        if (pheadlen == 1) {
            body.push( inputobj.phead[0])
        } else if (pheadlen == 2) {
            body.push(inputobj.phead[0], inputobj.phead[1]);
        };


        ///data of table
        $.each(inputobj.ptableData.rows, function (key, value) {
            let array = [key += 1];
            // console.log(key, value, "key","ptableData.rows loop");
            $.each(inputobj.ptableData.columns, function (key, value2) {
                // console.log(value2, "ptableData.colum loop");
                // array.push(value[value2.dataKey]);
                //debugger;
                if (typeof (value[value2.dataKey]) == 'number') {
                    // alert("number enterd");

                    array.push((value[value2.dataKey]).toLocaleString('en-IN', {
                        useGrouping: true,
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2,
                    }));

                } else if (typeof (value[value2.dataKey]) == 'string') {
                    //array.push((parseFloat(value[value2.dataKey])).toLocaleString());
                    array.push(value[value2.dataKey]);
                } else if (value[value2.dataKey] == null) {
                    array.push("");
                }

            });
            body.push(array);
        });



    };





    //  console.log(mainArr, "mainArr---last");

    var workbook = XLSX.utils.book_new();

    var worksheet = XLSX.utils.aoa_to_sheet(body);

    // // Merge multiple cells


    $.each(inputobj.mergeRangesdata, function (key, value) {
        mergeRanges.push(value);
    });

    //  console.log(mergeRanges,"mergeRanges","ln497")

    // console.log(ptableData.rows.length, "ptableData.rows.length");

    if (!worksheet['!merges']) worksheet['!merges'] = [];
    worksheet['!merges'] = worksheet['!merges'].concat(mergeRanges);

    //// Apply style to the report title cell
    //var reportTitleCell = 'A1';
    //worksheet[reportTitleCell].s = {
    //    font: { bold: true, color: { rgb: 'FF0000' } },
    //    alignment: { horizontal: 'center', vertical: 'center' }
    //};

    // Add the worksheet to the workbook
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');

    //var reportName = filename.toLowerCase() + '.xlsx';
    if (!inputobj.filename) inputobj.filename = '';
    var reportName = inputobj.filename.trim() + '.xlsx';

    // Save the workbook as an Excel file
    XLSX.writeFile(workbook, reportName);
};



//function InvoiceTemplate(props) {
//    alert('jsPDFInvoiceTemplate');
//    debugger
//    const param = {
//        outputType: props.outputType || "save",
//        returnJsPDFDocObject: props.returnJsPDFDocObject || false,
//        fileName: props.fileName || "",
//        orientationLandscape: props.orientationLandscape || false,
//        compress: props.compress || false,
//        logo: {
//            src: props.logo?.src || "",
//            type: props.logo?.type || "",
//            width: props.logo?.width || "",
//            height: props.logo?.height || "",
//            margin: {
//                top: props.logo?.margin?.top || 0,
//                left: props.logo?.margin?.left || 0,
//            },
//        },
//        stamp: {
//            inAllPages: props.stamp?.inAllPages || false,
//            src: props.stamp?.src || "",
//            width: props.stamp?.width || "",
//            height: props.stamp?.height || "",
//            margin: {
//                top: props.stamp?.margin?.top || 0,
//                left: props.stamp?.margin?.left || 0,
//            },
//        },
//        business: {
//            name: props.business?.name || "",
//            address: props.business?.address || "",
//            phone: props.business?.phone || "",
//            email: props.business?.email || "",
//            email_1: props.business?.email_1 || "",
//            website: props.business?.website || "",
//        },
//        contact: {
//            label: props.contact?.label || "",
//            name: props.contact?.name || "",
//            address: props.contact?.address || "",
//            phone: props.contact?.phone || "",
//            email: props.contact?.email || "",
//            otherInfo: props.contact?.otherInfo || "",
//        },
//        invoice: {
//            label: props.invoice?.label || "",
//            num: props.invoice?.num || "",
//            invDate: props.invoice?.invDate || "",
//            invGenDate: props.invoice?.invGenDate || "",
//            headerBorder: props.invoice?.headerBorder || false,
//            tableBodyBorder: props.invoice?.tableBodyBorder || false,
//            header: props.invoice?.header || [],
//            table: props.invoice?.table || [],
//            invDescLabel: props.invoice?.invDescLabel || "",
//            invDesc: props.invoice?.invDesc || "",
//            additionalRows: props.invoice?.additionalRows?.map(x => {
//                return {
//                    col1: x?.col1 || "",
//                    col2: x?.col2 || "",
//                    col3: x?.col3 || "",
//                    style: {
//                        fontSize: x?.style?.fontSize || 12,
//                    }
//                }
//            })
//        },
//        footer: {
//            text: props.footer?.text || "",
//        },
//        pageEnable: props.pageEnable || false,
//        pageLabel: props.pageLabel || "Page",
//    };

//    const splitTextAndGetHeight = (text, size) => {
//        var lines = doc.splitTextToSize(text, size);
//        return {
//            text: lines,
//            height: doc.getTextDimensions(lines).h,
//        };
//    };
//    if (param.invoice.table && param.invoice.table.length) {
//        if (param.invoice.table[0].length != param.invoice.header.length)
//            throw Error("Length of header and table column must be equal.");
//    }

//    const options = {
//        orientation: param.orientationLandscape ? "landscape" : "",
//        compress: param.compress
//    };

//    var doc = new jsPDF(options);

//    var docWidth = doc.internal.pageSize.width;
//    var docHeight = doc.internal.pageSize.height;

//    var colorBlack = "#000000";
//    var colorGray = "#4d4e53";
//    //starting at 15mm
//    var currentHeight = 15;
//    //var startPointRectPanel1 = currentHeight + 6;

//    var pdfConfig = {
//        headerTextSize: 20,
//        labelTextSize: 12,
//        fieldTextSize: 10,
//        lineHeight: 6,
//        subLineHeight: 4,
//    };

//    doc.setFontSize(pdfConfig.headerTextSize);
//    doc.setTextColor(colorBlack);
//    doc.text(docWidth - 10, currentHeight, param.business.name, "right");
//    doc.setFontSize(pdfConfig.fieldTextSize);

//    if (param.logo.src) {
//        var imageHeader = '';
//        if (typeof window === "undefined") {
//            imageHeader = param.logo.src;
//        } else {
//            imageHeader = new Image();
//            imageHeader.src = param.logo.src;
//        }
//        //
//        var htmlDoc = {
//            sessionDateText: ""
//        };
//        ////
//        doc.text(htmlDoc.sessionDateText, docWidth - (doc.getTextWidth(htmlDoc.sessionDateText) + 10), currentHeight);
//           if (param.logo.type)
//             doc.addImage(
//               imageHeader,
//               param.logo.type,
//               10 + param.logo.margin.left,
//               currentHeight - 5 + param.logo.margin.top,
//               param.logo.width,
//               param.logo.height
//             );
//           else
//             doc.addImage(
//               imageHeader,
//               10 + param.logo.margin.left,
//               currentHeight - 5 + param.logo.margin.top,
//               param.logo.width,
//               param.logo.height
//             );
//    }

//    doc.setTextColor(colorGray);

//    currentHeight += pdfConfig.subLineHeight;
//    currentHeight += pdfConfig.subLineHeight;
//    doc.text(docWidth - 10, currentHeight, param.business.address, "right");
//    currentHeight += pdfConfig.subLineHeight;
//    doc.text(docWidth - 10, currentHeight, param.business.phone, "right");
//    doc.setFontSize(pdfConfig.fieldTextSize);
//    // doc.setTextColor(colorGray);
//    currentHeight += pdfConfig.subLineHeight;
//    doc.text(docWidth - 10, currentHeight, param.business.email, "right");

//    currentHeight += pdfConfig.subLineHeight;
//    doc.text(docWidth - 10, currentHeight, param.business.email_1, "right");

//    currentHeight += pdfConfig.subLineHeight;
//    doc.text(docWidth - 10, currentHeight, param.business.website, "right");

//    //line breaker after logo & business info
//    if (param.invoice.header.length) {
//        currentHeight += pdfConfig.subLineHeight;
//        doc.line(10, currentHeight, docWidth - 10, currentHeight);
//    }

//    //Contact part
//    doc.setTextColor(colorGray);
//    doc.setFontSize(pdfConfig.fieldTextSize);
//    currentHeight += pdfConfig.lineHeight;
//    if (param.contact.label) {
//        doc.text(10, currentHeight, param.contact.label);
//        currentHeight += pdfConfig.lineHeight;
//    }

//    doc.setTextColor(colorBlack);
//    doc.setFontSize(pdfConfig.headerTextSize - 5);
//    if (param.contact.name) doc.text(10, currentHeight, param.contact.name);

//    if (param.invoice.label && param.invoice.num) {
//        doc.text(
//            docWidth - 10,
//            currentHeight,
//            param.invoice.label + param.invoice.num,
//            "right"
//        );
//    }

//    if (param.contact.name || (param.invoice.label && param.invoice.num))
//        currentHeight += pdfConfig.subLineHeight;

//    doc.setTextColor(colorGray);
//    doc.setFontSize(pdfConfig.fieldTextSize - 2);

//    if (param.contact.address || param.invoice.invDate) {
//        doc.text(10, currentHeight, param.contact.address);
//        doc.text(docWidth - 10, currentHeight, param.invoice.invDate, "right");
//        currentHeight += pdfConfig.subLineHeight;
//    }

//    if (param.contact.phone || param.invoice.invGenDate) {
//        doc.text(10, currentHeight, param.contact.phone);
//        doc.text(docWidth - 10, currentHeight, param.invoice.invGenDate, "right");
//        currentHeight += pdfConfig.subLineHeight;
//    }

//    if (param.contact.email) {
//        doc.text(10, currentHeight, param.contact.email);
//        currentHeight += pdfConfig.subLineHeight;
//    }

//    if (param.contact.otherInfo)
//        doc.text(10, currentHeight, param.contact.otherInfo);
//    else currentHeight -= pdfConfig.subLineHeight;
//    //end contact part

//    //TABLE PART
//    //var tdWidth = 31.66;
//    //10 margin left - 10 margin right
//    //var tdWidth = (140 - 20) / param.invoice.header.length;
//    // Get the page size
    

//    // Extract the width from the page size
//    var pageWidth = doc.internal.pageSize.width;
//    var tdWidth = (pageWidth - 20) / param.invoice.header.length;

//    //#region TD WIDTH
//    if (param.invoice.header.length > 2) { //add style for 2 or more columns
//        const customColumnNo = param.invoice.header.map(x => x?.style?.width || 0).filter(x => x > 0);
//        let customWidthOfAllColumns = customColumnNo.reduce((a, b) => a + b, 0);
//        //tdWidth = (140 - 20 - customWidthOfAllColumns) / (param.invoice.header.length - customColumnNo.length);
//        tdWidth = (pageWidth - 20 - customWidthOfAllColumns) / (param.invoice.header.length - customColumnNo.length);
//    }
//    //#endregion

//    //#region TABLE HEADER BORDER
//    var addTableHeaderBorder = () => {
//        currentHeight += 2;
//        const lineHeight = 7;
//        let startWidth = 0;
//        for (let i = 0; i < param.invoice.header.length; i++) {
//            const currentTdWidth = param.invoice.header[i]?.style?.width || tdWidth;
//            if (i === 0) doc.rect(10, currentHeight, currentTdWidth, lineHeight);
//            else {
//                const previousTdWidth = param.invoice.header[i - 1]?.style?.width || tdWidth;
//                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
//                startWidth += widthToUse;
//                doc.rect(startWidth + 10, currentHeight, currentTdWidth, lineHeight);
//            }
//        }
//        currentHeight -= 2;
//    };
//    //#endregion

//    //#region TABLE BODY BORDER
//    var addTableBodyBorder = (lineHeight) => {
//        let startWidth = 0;
//        for (let i = 0; i < param.invoice.header.length; i++) {
//            const currentTdWidth = param.invoice.header[i]?.style?.width || tdWidth;
//            if (i === 0) doc.rect(10, currentHeight, currentTdWidth, lineHeight);
//            else {
//                const previousTdWidth = param.invoice.header[i - 1]?.style?.width || tdWidth;
//                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
//                startWidth += widthToUse;
//                doc.rect(startWidth + 10, currentHeight, currentTdWidth, lineHeight);
//            }
//        }
//    };
//    //#endregion

//    //#region TABLE HEADER
//    var addTableHeader = () => {
//        if (param.invoice.headerBorder) addTableHeaderBorder();

//        currentHeight += pdfConfig.subLineHeight;
//        doc.setTextColor(colorBlack);
//        doc.setFontSize(pdfConfig.fieldTextSize);
//        //border color
//        doc.setDrawColor(colorGray);
//        currentHeight += 2;

//        let startWidth = 0;
//        param.invoice.header.forEach(function (row, index) {
//            if (index == 0) doc.text(row.title, 11, currentHeight);
//            else {
//                const currentTdWidth = row?.style?.width || tdWidth;
//                const previousTdWidth = param.invoice.header[index - 1]?.style?.width || tdWidth;
//                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
//                startWidth += widthToUse;
//                doc.text(row.title, startWidth + 11, currentHeight);
//            }
//        });

//        currentHeight += pdfConfig.subLineHeight - 1;
//        doc.setTextColor(colorGray);
//    };
//    //#endregion

//    addTableHeader();

//    //#region TABLE BODY
//    var tableBodyLength = param.invoice.table.length;
//    param.invoice.table.forEach(function (row, index) {
//        doc.line(10, currentHeight, docWidth - 10, currentHeight);

//        //get nax height for the current row
//        var getRowsHeight = function () {
//            let rowsHeight = [];
//            row.forEach(function (rr, index) {
//                const widthToUse = param.invoice.header[index]?.style?.width || tdWidth;

//                let item = splitTextAndGetHeight(rr.toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders
//                rowsHeight.push(item.height);
//            });

//            return rowsHeight;
//        };

//        var maxHeight = Math.max(...getRowsHeight());

//        //body borders
//        if (param.invoice.tableBodyBorder) addTableBodyBorder(maxHeight + 1);

//        let startWidth = 0;
//        row.forEach(function (rr, index) {
//            const widthToUse = param.invoice.header[index]?.style?.width || tdWidth;
//            let item = splitTextAndGetHeight(rr.toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders

//            if (index == 0) doc.text(item.text, 11, currentHeight + 4);
//            else {
//                const currentTdWidth = rr?.style?.width || tdWidth;
//                const previousTdWidth = param.invoice.header[index - 1]?.style?.width || tdWidth;
//                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
//                startWidth += widthToUse;
//                doc.text(item.text, 11 + startWidth, currentHeight + 4);
//            }
//        });

//        currentHeight += maxHeight - 4;

//        //td border height
//        currentHeight += 5;

//        //pre-increase currentHeight to check the height based on next row
//        if (index + 1 < tableBodyLength) currentHeight += maxHeight;

//        if (
//            param.orientationLandscape &&
//            (currentHeight > 185 ||
//                (currentHeight > 178 && doc.getNumberOfPages() > 1))
//        ) {
//            doc.addPage();
//            currentHeight = 10;
//            if (index + 1 < tableBodyLength) addTableHeader();
//        }

//        if (
//            !param.orientationLandscape &&
//            (currentHeight > 265 ||
//                (currentHeight > 255 && doc.getNumberOfPages() > 1))
//        ) {
//            doc.addPage();
//            currentHeight = 10;
//            if (index + 1 < tableBodyLength) addTableHeader();
//            //else
//            //currentHeight += pdfConfig.subLineHeight + 2 + pdfConfig.subLineHeight - 1; //same as in addtableHeader
//        }

//        //reset the height that was increased to check the next row
//        if (index + 1 < tableBodyLength && currentHeight > 30)
//            // check if new page
//            currentHeight -= maxHeight;
//    });
//    //doc.line(10, currentHeight, docWidth - 10, currentHeight); //if we want to show the last table line 
//    //#endregion

//    var invDescSize = splitTextAndGetHeight(
//        param.invoice.invDesc,
//        docWidth / 2
//    ).height;

//    //#region PAGE BREAKER
//    var checkAndAddPageLandscape = function () {
//        if (!param.orientationLandscape && currentHeight + invDescSize > 270) {
//            doc.addPage();
//            currentHeight = 10;
//        }
//    }

//    var checkAndAddPageNotLandscape = function (heightLimit = 173) {
//        if (param.orientationLandscape && currentHeight + invDescSize > heightLimit) {
//            doc.addPage();
//            currentHeight = 10;
//        }
//    }
//    var checkAndAddPage = function () {
//        checkAndAddPageNotLandscape();
//        checkAndAddPageLandscape();
//    }
//    //#endregion

//    //#region Stamp
//    var addStamp = () => {
//        let _addStampBase = () => {
//            var stampImage = '';
//            if (typeof window === "undefined") {
//                stampImage = param.stamp.src;
//            } else {
//                stampImage = new Image();
//                stampImage.src = param.stamp.src;
//            }

//            if (param.stamp.type)
//                doc.addImage(
//                    stampImage,
//                    param.stamp.type,
//                    10 + param.stamp.margin.left,
//                    docHeight - 22 + param.stamp.margin.top,
//                    param.stamp.width,
//                    param.stamp.height
//                );
//            else
//                doc.addImage(
//                    stampImage,
//                    10 + param.stamp.margin.left,
//                    docHeight - 22 + param.stamp.margin.top,
//                    param.stamp.width,
//                    param.stamp.height
//                );
//        };

//        if (param.stamp.src) {
//            if (param.stamp.inAllPages)
//                _addStampBase();
//            else if (!param.stamp.inAllPages && doc.getCurrentPageInfo().pageNumber == doc.getNumberOfPages())
//                _addStampBase();
//        }
//    }
//    //#endregion

//    checkAndAddPage();

//    doc.setTextColor(colorBlack);
//    doc.setFontSize(pdfConfig.labelTextSize);
//    currentHeight += pdfConfig.lineHeight;

//    //#region additionalRows
//    if (param.invoice.additionalRows?.length > 0) {
//        //#region Line breaker before invoce total
//        doc.line(docWidth / 2, currentHeight, docWidth - 10, currentHeight);
//        currentHeight += pdfConfig.lineHeight;
//        //#endregion

//        for (let i = 0; i < param.invoice.additionalRows.length; i++) {
//            currentHeight += pdfConfig.lineHeight;
//            doc.setFontSize(param.invoice.additionalRows[i].style.fontSize);

//            doc.text(docWidth / 1.5, currentHeight, param.invoice.additionalRows[i].col1, "right");
//            doc.text(docWidth - 25, currentHeight, param.invoice.additionalRows[i].col2, "right");
//            doc.text(docWidth - 10, currentHeight, param.invoice.additionalRows[i].col3, "right");
//            checkAndAddPage();
//        }
//    }
//    //#endregion

//    checkAndAddPage();

//    doc.setTextColor(colorBlack);
//    currentHeight += pdfConfig.subLineHeight;
//    currentHeight += pdfConfig.subLineHeight;
//    //   currentHeight += pdfConfig.subLineHeight;
//    doc.setFontSize(pdfConfig.labelTextSize);

//    //#region Add num of pages at the bottom
//    if (doc.getNumberOfPages() > 1) {
//        for (let i = 1; i <= doc.getNumberOfPages(); i++) {
//            doc.setFontSize(pdfConfig.fieldTextSize - 2);
//            doc.setTextColor(colorGray);

//            if (param.pageEnable) {
//                doc.text(docWidth / 2, docHeight - 10, param.footer.text, "center");
//                doc.setPage(i);
//                doc.text(
//                    param.pageLabel + " " + i + " / " + doc.getNumberOfPages(),
//                    docWidth - 20,
//                    doc.internal.pageSize.height - 6
//                );
//            }

//            checkAndAddPageNotLandscape(183);
//            checkAndAddPageLandscape();
//            addStamp();
//        }
//    }
//    //#endregion

   

//    //#region INVOICE DESCRIPTION
//    var addInvoiceDesc = () => {
//        debugger
//        doc.setFontSize(pdfConfig.labelTextSize);
//        doc.setTextColor(colorBlack);

//        doc.text(param.invoice.invDescLabel, 10, currentHeight);
//        currentHeight += pdfConfig.subLineHeight;
//        doc.setTextColor(colorGray);
//        doc.setFontSize(pdfConfig.fieldTextSize - 1);

//        var lines = doc.splitTextToSize(param.invoice.invDesc, docWidth / 2);
//        //text in left half
//        doc.text(lines, 10, currentHeight);
//        currentHeight +=
//            doc.getTextDimensions(lines).h > 5
//                ? doc.getTextDimensions(lines).h + 6
//                : pdfConfig.lineHeight;

//        return currentHeight;
//    };
//    addInvoiceDesc();
//    //#endregion

//    addStamp();

//    //#region Add num of first page at the bottom
//    if (doc.getNumberOfPages() === 1 && param.pageEnable) {
//        doc.setFontSize(pdfConfig.fieldTextSize - 2);
//        doc.setTextColor(colorGray);
//        doc.text(docWidth / 2, docHeight - 10, param.footer.text, "center");
//        doc.text(
//            param.pageLabel + "1 / 1",
//            docWidth - 20,
//            doc.internal.pageSize.height - 6
//        );
//    }
//    //#endregion

//    let returnObj = {
//        pagesNumber: doc.getNumberOfPages(),
//    };

//    if (param.returnJsPDFDocObject) {
//        returnObj = {
//            ...returnObj,
//            jsPDFDocObject: doc,
//        };
//    }

//    if (param.outputType === "save") doc.save(param.fileName);
//    else if (param.outputType === "blob") {
//        const blobOutput = doc.output("blob");
//        returnObj = {
//            ...returnObj,
//            blob: blobOutput,
//        };
//    } else if (param.outputType === "datauristring") {
//        returnObj = {
//            ...returnObj,
//            dataUriString: doc.output("datauristring", {
//                filename: param.fileName,
//            }),
//        };
//    } else if (param.outputType === "arraybuffer") {
//        returnObj = {
//            ...returnObj,
//            arrayBuffer: doc.output("arraybuffer"),
//        };
//    } else
//        doc.output(param.outputType, {
//            filename: param.fileName,
//        });

//    return returnObj;
//}

function InvoiceTemplate2(props) {
  //  debugger
    const param = {
        outputType: props.outputType || "save",
        returnJsPDFDocObject: props.returnJsPDFDocObject || false,
        fileName: props.fileName || "",
        orientationLandscape: props.orientationLandscape || false,
        compress: props.compress || false,
        logo: {
            src: props.logo?.src || "",
            type: props.logo?.type || "",
            width: props.logo?.width || "",
            height: props.logo?.height || "",
            margin: {
                top: props.logo?.margin?.top || 0,
                left: props.logo?.margin?.left || 0,
            },
        },
        stamp: {
            inAllPages: props.stamp?.inAllPages || false,
            src: props.stamp?.src || "",
            width: props.stamp?.width || "",
            height: props.stamp?.height || "",
            margin: {
                top: props.stamp?.margin?.top || 0,
                left: props.stamp?.margin?.left || 0,
            },
        },
        header: {
            firstRigthLn: props.header?.firstRigthLn || "",
            deliveryNote: props.header?.deliveryNote || "",
            date: props.header?.date || "",
            shipping_date: props.header?.shipping_date || "",
            showinAllpage: props.header?.showinAllpage || false,
            //website: props.business?.website || "",
        },
        business: {
            companyname: props.business?.companyname || "",
            address: props.business?.address || "",
            phone: props.business?.phone || "",
            email: props.business?.email || "",
            website: props.business?.website || "",
            //email_1: props.business?.email_1 || "",
            //website: props.business?.website || "",
        },
        contact: {
            label: props.contact?.label || "",
            name: props.contact?.name || "",
            address: props.contact?.address || "",
            phone: props.contact?.phone || "",
            email: props.contact?.email || "",
            website: props.contact?.website || "",
        },
        bill_to: {
            header: props.bill_to?.header || "",
            name: props.bill_to?.name || "",
            address: props.bill_to?.address || "",
            phone: props.bill_to?.phone || "",
            email: props.bill_to?.email || "",
            otherInfo: props.bill_to?.otherInfo || "",
        },
        ship_to: {
            header: props.ship_to?.header || "",
            name: props.ship_to?.name || "",
            address: props.ship_to?.address || "",
            phone: props.ship_to?.phone || "",
            email: props.ship_to?.email || "",
            otherInfo: props.ship_to?.otherInfo || "",
        }, 
        invoice: {
            label: props.invoice?.label || "",
            num: props.invoice?.num || "",
            invDate: props.invoice?.invDate || "",
            invGenDate: props.invoice?.invGenDate || "",
            headerBorder: props.invoice?.headerBorder || false,
            tableBodyBorder: props.invoice?.tableBodyBorder || false,
            header: props.invoice?.header || [],
            table: props.invoice?.table || [],
            tableDatakey: props.invoice?.tableDatakey || [],
            secounflineInTable: props.invoice?.secounflineInTable || [],
            invDescLabel: props.invoice?.invDescLabel || "",
            invDesc: props.invoice?.invDesc || "",
            fullLength: props.invoice?.fullLength || "",
            additionalRows: props.invoice?.additionalRows?.map(x => {
                return {
                    col1: x?.col1 || "",
                    col2: x?.col2 || "",
                    col3: x?.col3 || "",
                    style: {
                        fontSize: x?.style?.fontSize || 12,
                    }
                }
            })
        },
        subfooter: {
            title: props.subfooter?.title || "",
            line1: props.subfooter?.line1 || "",
            line2: props.subfooter?.line2 || "",
            line3: props.subfooter?.line3 || "",
            line4: props.subfooter?.line4 || "",
            line5: props.subfooter?.line5 || "",
            line6: props.subfooter?.line6 || "",
        },
        footer: {
            text: props.footer?.text || "",
        },
        pageEnable: props.pageEnable || false,
        pageLabel: props.pageLabel || "Page",
        subheaderinAllpage: props.subheaderinAllpage ||false,
    };

    const splitTextAndGetHeight = (text, size) => {
        var lines = doc.splitTextToSize(text, size);
        return {
            text: lines,
            height: doc.getTextDimensions(lines).h,
        };
    };
    //removed becoz of double line
    ////if (param.invoice.table && param.invoice.table.length) {
    //if (param.invoice.tableDatakey && param.invoice.tableDatakey.length) {
    //   // if (param.invoice.table[0].length != param.invoice.header.length)
    //    if (param.invoice.tableDatakey.length != param.invoice.header.length)
    //        throw Error("Length of header and table column must be equal.");
    //}

    const options = {
        orientation: param.orientationLandscape ? "landscape" : "",
        compress: param.compress
    };

    var doc = new jsPDF(options);

    var docWidth = doc.internal.pageSize.width;
    var docHeight = doc.internal.pageSize.height;

    var colorBlack = "#000000";
    var colorGray = "#4d4e53";
    var blue_theme = "#061e59";
    //starting at 15mm
    var currentHeight = 15;
    //var startPointRectPanel1 = currentHeight + 6;

    var pdfConfig = {
        headerTextSize: 20,
        labelTextSize: 12,
        fieldTextSize: 10,
        lineHeight: 6,
        subLineHeight: 4,
        f1: 20,
        f2: 15,
        f3: 10,
        f4: 12,
        XSeed:10
    };

    ///header Part start
    var addheader = () => {
        doc.setFontSize(pdfConfig.f4);
        doc.setTextColor(colorBlack);
        doc.text((docWidth - pdfConfig.XSeed), currentHeight, param.header.firstRigthLn, "right");
        doc.setFontSize(pdfConfig.fieldTextSize);

        if (param.logo.src) {
            var imageHeader = '';
            if (typeof window === "undefined") {
                imageHeader = param.logo.src;
            } else {
                imageHeader = new Image();
                imageHeader.src = param.logo.src;
            }
            //
            var htmlDoc = {
                sessionDateText: ""
            };
            ////
            doc.text(htmlDoc.sessionDateText, docWidth - (doc.getTextWidth(htmlDoc.sessionDateText) + pdfConfig.XSeed), currentHeight);
            if (param.logo.type)
                doc.addImage(
                    imageHeader,
                    param.logo.type,
                    pdfConfig.XSeed + param.logo.margin.left,
                    currentHeight - 5 + param.logo.margin.top,
                    param.logo.width,
                    param.logo.height
                );
            else
                doc.addImage(
                    imageHeader,
                    pdfConfig.XSeed + param.logo.margin.left,
                    currentHeight - 5 + param.logo.margin.top,
                    param.logo.width,
                    param.logo.height
                );
        }

      //  debugger;
        //doc.setTextColor(colorGray);
        currentHeight += pdfConfig.subLineHeight;
        currentHeight += 2;
        //currentHeight += 12;
        doc.setFontSize(pdfConfig.f2);
        doc.setTextColor(blue_theme);
        doc.text(docWidth - pdfConfig.XSeed, currentHeight, param.header.deliveryNote, "right");
        currentHeight += 2;
        doc.setFontSize(pdfConfig.f4);
        doc.setTextColor(colorGray);
        currentHeight += pdfConfig.subLineHeight;
        doc.text(docWidth - pdfConfig.XSeed, currentHeight, param.header.date, "right");
        doc.setFontSize(pdfConfig.f4);

        // doc.setTextColor(colorGray);
        currentHeight += pdfConfig.lineHeight;
        doc.text(docWidth - pdfConfig.XSeed, currentHeight, param.header.shipping_date, "right");

        //line breaker after logo & business info
        if (param.invoice.header.length) {
            currentHeight += pdfConfig.subLineHeight;
            doc.setDrawColor(colorBlack);
            doc.line(10, currentHeight, docWidth - pdfConfig.XSeed, currentHeight);
            currentHeight += 0.3;
            doc.line(10, currentHeight, docWidth - pdfConfig.XSeed, currentHeight);
        }
    }
    /////header Part End
    addheader();

    //business part
   

    ////Sub header Section Start
    var subHeader = () => {
        doc.setTextColor(colorBlack);
        doc.setFontSize(pdfConfig.headerTextSize - 5);
        currentHeight += pdfConfig.lineHeight;
        var lineHeight = currentHeight;
        var currentHeigthArry = [];
        //line 1
        if (param.business.companyname) {
            ///test  -start
            var lines = breakTextIntoLines(param.business.companyname, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);


            // Now, you can use the 'lines' array to add the text to your PDF
            for (var i = 0; i < lines.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.lineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines[i]);

            }
            currentHeigthArry.push(currentHeight);
            //lineHeight += pdfConfig.lineHeight; //reset current line for write on same line 
            ///test  --end
            // doc.text(10, currentHeight, param.business.companyname);
            //currentHeight += pdfConfig.lineHeight;
        }

        //doc.setTextColor(colorBlack);
        //doc.setFontSize(pdfConfig.headerTextSize - 5);
        //if (param.business.address) doc.text(10, currentHeight, param.business.address);



        if (param.bill_to.header) {

            //doc.text(
            //    (pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed*2)) / 3)),
            //    currentHeight,
            //    param.bill_to.header,

            //);
            currentHeight = lineHeight;
            var lines = breakTextIntoLines(param.bill_to.header, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);


            // Now, you can use the 'lines' array to add the text to your PDF
            for (var i = 0; i < lines.length; i++) {
                //doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.lineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed * 2)) / 3)), currentHeight, lines[i]);

            };
            currentHeigthArry.push(currentHeight);
        }


        if (param.ship_to.header) {
            //doc.text(
            //    (pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed*2)) / 3)*2 )),
            //    currentHeight,
            //    param.ship_to.header,

            //);
            //debugger
            currentHeight = lineHeight;


            var lines = breakTextIntoLines(param.ship_to.header, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);


            //// Now, you can use the 'lines' array to add the text to your PDF
            for (var i = 0; i < lines.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.lineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines[i]);

            };
            currentHeigthArry.push(currentHeight);

        }
        var maxVal = Math.max(...currentHeigthArry);
        currentHeight = maxVal;

        if (param.business.companyname || param.bill_to.header || param.ship_to.header) {
            currentHeight += pdfConfig.subLineHeight;  //seed
        }


        doc.setTextColor(colorGray);
        doc.setFontSize(pdfConfig.fieldTextSize - 2);



        /////
        lineHeight = currentHeight;
        if (param.bill_to.name || param.ship_to.name) {


           
            var currentHeigthArr = [];

            var lines1 = "";
            var lines2 = breakTextIntoLines(param.bill_to.name, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.name, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
            // doc.text(pdfConfig.XSeed, currentHeight, 'Test Position2;');
        }









        /////
       // debugger
        // line 2

        lineHeight = currentHeight;
        if (param.business.address || param.bill_to.address || param.ship_to.address) {


            //doc.text(pdfConfig.XSeed, currentHeight, param.business.address);
            //doc.text((pdfConfig.XSeed + ( (docWidth - (pdfConfig.XSeed * 2) ) / 3)), currentHeight, param.bill_to.address);
            //doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, param.ship_to.address);
            //currentHeight += pdfConfig.subLineHeight;
            var currentHeigthArr = [];

            var lines1 = breakTextIntoLines(param.business.address, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines2 = breakTextIntoLines(param.bill_to.address, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.address, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
            // doc.text(pdfConfig.XSeed, currentHeight, 'Test Position2;');
        }

        ////line 3
        //currentHeight += pdfConfig.subLineHeight; //seed
        //doc.text(pdfConfig.XSeed, currentHeight, 'Test Position3;');
        lineHeight = currentHeight;
        if (param.business.phone || param.bill_to.phone || param.ship_to.phone) {
            //doc.text(pdfConfig.XSeed, currentHeight, param.business.phone);
            //doc.text((pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed * 2)) / 3)), currentHeight, param.bill_to.phone);
            //doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, param.ship_to.phone);
            //currentHeight += pdfConfig.subLineHeight;

            var currentHeigthArr = [];

            var lines1 = breakTextIntoLines(param.business.phone, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines2 = breakTextIntoLines(param.bill_to.phone, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.phone, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
        }

        ////line 4
        //currentHeight += pdfConfig.subLineHeight; //seed
        lineHeight = currentHeight;
        if (param.business.email || param.bill_to.email || param.ship_to.email) {
            //doc.text(pdfConfig.XSeed, currentHeight, param.business.email);
            //doc.text((pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed * 2)) / 3)), currentHeight, param.bill_to.email);
            //doc.text((pdfConfig.XSeed + ( ( (docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, param.ship_to.email);
            //currentHeight += pdfConfig.subLineHeight;
            var currentHeigthArr = [];

            var lines1 = breakTextIntoLines(param.business.email, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines2 = breakTextIntoLines(param.bill_to.email, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.email, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
        }

        ////line 5
        //currentHeight += pdfConfig.subLineHeight;
        lineHeight = currentHeight;
        if (param.business.website || param.bill_to.otherInfo || param.ship_to.otherInfo) {
            //doc.text(pdfConfig.XSeed, currentHeight, param.business.website);
            //doc.text((pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed * 2)) / 3)), currentHeight, param.bill_to.otherInfo);
            //doc.text((pdfConfig.XSeed + ( ( (docWidth - (pdfConfig.XSeed * 2) ) / 3) * 2)), currentHeight, param.ship_to.otherInfo);
            //currentHeight += pdfConfig.subLineHeight;
            var currentHeigthArr = [];

            var lines1 = breakTextIntoLines(param.business.website, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines2 = breakTextIntoLines(param.bill_to.otherInfo, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.otherInfo, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
        }
        //end contact part


        //line breaker after logo & business info
        if (param.invoice.header.length) {
            currentHeight += pdfConfig.subLineHeight;
            doc.setDrawColor(colorBlack);
            doc.line(10, currentHeight, docWidth - pdfConfig.XSeed, currentHeight);
            currentHeight += 0.3;
            doc.line(10, currentHeight, docWidth - pdfConfig.XSeed, currentHeight);
        }
    }
    subHeader();
    ////Sub header Section end





    //TABLE PART
    //var tdWidth = 31.66;
    //10 margin left - 10 margin right
    //var tdWidth = (140 - 20) / param.invoice.header.length;
    // Get the page size


    // Extract the width from the page size
    var pageWidth = docWidth;
    var tdWidth = (pageWidth - 20) / param.invoice.header.length;

    //#region TD WIDTH
    if (param.invoice.header.length > 2) { //add style for 2 or more columns
        const customColumnNo = param.invoice.header.map(x => x?.style?.width || 0).filter(x => x > 0);
        let customWidthOfAllColumns = customColumnNo.reduce((a, b) => a + b, 0);
        //tdWidth = (140 - 20 - customWidthOfAllColumns) / (param.invoice.header.length - customColumnNo.length);
        tdWidth = (pageWidth - 20 - customWidthOfAllColumns) / (param.invoice.header.length - customColumnNo.length);
    }
    //#endregion

    //#region TABLE HEADER BORDER
    var addTableHeaderBorder = () => {
        currentHeight += 2;
        const lineHeight = 7;
        let startWidth = 0;
        for (let i = 0; i < param.invoice.header.length; i++) {
            const currentTdWidth = param.invoice.header[i]?.style?.width || tdWidth;
            if (i === 0) doc.rect(10, currentHeight, currentTdWidth, lineHeight);
            else {
                const previousTdWidth = param.invoice.header[i - 1]?.style?.width || tdWidth;
                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
                startWidth += widthToUse;
                doc.rect(startWidth + 10, currentHeight, currentTdWidth, lineHeight);
            }
        }
        currentHeight -= 2;
    };
    //#endregion

    //#region TABLE BODY BORDER
    var addTableBodyBorder = (lineHeight) => {
        let startWidth = 0;
        for (let i = 0; i < param.invoice.header.length; i++) {
            const currentTdWidth = param.invoice.header[i]?.style?.width || tdWidth;
            if (i === 0) doc.rect(10, currentHeight, currentTdWidth, lineHeight);
            else {
                const previousTdWidth = param.invoice.header[i - 1]?.style?.width || tdWidth;
                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
                startWidth += widthToUse;
                doc.rect(startWidth + 10, currentHeight, currentTdWidth, lineHeight);
            }
        }
    };
    //#endregion

    //#region TABLE HEADER
    var addTableHeader = () => {
        if (param.invoice.headerBorder) addTableHeaderBorder();

        currentHeight += pdfConfig.subLineHeight;


        doc.setTextColor(colorBlack);
        doc.setFontSize(pdfConfig.fieldTextSize);
        //border color
        doc.setDrawColor(colorGray);
        currentHeight += 2;

        let startWidth = 0;
        param.invoice.header.forEach(function (row, index) {
            if (index == 0) doc.text(row.title, 11, currentHeight);
            else {
                const currentTdWidth = row?.style?.width || tdWidth;
                const previousTdWidth = param.invoice.header[index - 1]?.style?.width || tdWidth;
                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
                startWidth += widthToUse;
                doc.text(row.title, startWidth + 11, currentHeight);
            }
        });

        currentHeight += pdfConfig.subLineHeight - 1;
        doc.setTextColor(colorGray);

    };
    //#endregion

    addTableHeader();

    //#region TABLE BODY
    var tableBodyLength = param.invoice.table.length;
    param.invoice.table.forEach(function (row, index) {
        doc.line(10, currentHeight, docWidth - 10, currentHeight);

        //get nax height for the current row
        var getRowsHeight = function () {
            let rowsHeight = [];
            //row.forEach(function (rr, index) {
            //    const widthToUse = param.invoice.header[index]?.style?.width || tdWidth;

            //    let item = splitTextAndGetHeight(rr.toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders
            //    rowsHeight.push(item.height);
            //});
            debugger
            console.log(row,'row')
            param.invoice.tableDatakey.forEach((d, i) => {
                console.log(d,'d')
                const widthToUse = param.invoice.header[i]?.style?.width || tdWidth;
                if (row[d]) {
                    let item = splitTextAndGetHeight((row[d]).toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders
                    rowsHeight.push(item.height);
                }
               
            });

            return rowsHeight;
        };

        var maxHeight = Math.max(...getRowsHeight());

        //body borders
        if (param.invoice.tableBodyBorder) addTableBodyBorder(maxHeight + 1);



        ////let currenthightreset = currentHeight;    ///test line *****************
        let startWidth = 0;
        //let secoundlineHeight = currentHeight;     ///test line *****************
        //row.forEach(function (rr, index) {

        //     ///test line *****************
        //    const widthToUse = param.invoice.header[index]?.style?.width || tdWidth;
        //    let item = splitTextAndGetHeight(rr.toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders

        //    if (index == 0) doc.text(item.text, 11, currentHeight + 4);
        //    else {
        //        const currentTdWidth = rr?.style?.width || tdWidth;
        //        const previousTdWidth = param.invoice.header[index - 1]?.style?.width || tdWidth;
        //        const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
        //        startWidth += widthToUse;
        //        doc.text(item.text, 11 + startWidth, currentHeight + 4);

        //        secoundlineHeight=currentHeight +pdfConfig.subLineHeight; ////////////////////******************
        //        doc.text('Test Line', 11 + startWidth, secoundlineHeight + 4 );   ///test line *****************
        //    };
            
        //});

        ///test
        let secoundlineHeight = currentHeight;     ///test line *****************
        param.invoice.tableDatakey.forEach((data, index2) => {
          //  debugger

            console.log(row[data]);
            if (!param.invoice.secounflineInTable.includes(data)) {
                const widthToUse = param.invoice.header[index2]?.style?.width || tdWidth;
                let item = splitTextAndGetHeight((row[data] ? row[data] : "").toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders
               
                // if (index == 0) doc.text(item.text, 11, currentHeight + 4);
                if (index2 == 0) doc.text((index + 1).toString(), 11, currentHeight + 4);
                else {
                    const currentTdWidth = (row[data])?.style?.width || tdWidth;
                    const previousTdWidth = param.invoice.header[index2 - 1]?.style?.width || tdWidth;
                    const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
                    startWidth += widthToUse;
                    doc.text(item.text, 11 + startWidth, currentHeight + 4);
                    if (item.text.length)
                        for (let i = 1; i < item.text.length; i++) {
                            currentHeight += pdfConfig.subLineHeight;
                        };
                    secoundlineHeight = currentHeight + pdfConfig.subLineHeight;   ////////////////////******************
                    if (data == "Product") {
                        doc.setFontSize(8);
                        doc.text(row["Service"], 11 + startWidth, secoundlineHeight+4); /////////test line *****************
                    } else if (data == "Qty") {
                        doc.setFontSize(8);
                        doc.text(row["Uom"], 11 + startWidth, secoundlineHeight+4 ); /////////test line *****************
                    }
                    
                };
            };
           
        });
        //for (let person of ab) {
        //    console.log(`Name: ${person.name}, Age: ${person.age}, Place: ${person.place}`);
        //}



        ///test


        currentHeight = secoundlineHeight;  ///test line *****************

        //currentHeight += maxHeight - 4;

        //td border height
        currentHeight += 5;
        
        //pre-increase currentHeight to check the height based on next row
        if (index + 1 < tableBodyLength) currentHeight += maxHeight;

        if (
            param.orientationLandscape &&(currentHeight > 185 || (currentHeight > 178 && doc.getNumberOfPages() > 1))
        ) {
            doc.addPage();
            currentHeight = 10;
            if (param.header.showinAllpage) addheader()//new line of code *********
            if (param.subheaderinAllpage) subHeader(); //new line of code *********
            if (index + 1 < tableBodyLength) { addTableHeader(); currentHeight += pdfConfig.subLineHeight }
            else currentHeight += pdfConfig.subLineHeight;
        }

        if (
            !param.orientationLandscape &&
            (currentHeight > 265 ||
                (currentHeight > 255 && doc.getNumberOfPages() > 1))

        ) {
            doc.addPage();
            currentHeight = 10;
            if (param.header.showinAllpage) addheader()//new line of code *********
            if (param.subheaderinAllpage) subHeader(); //new line of code *********
            if (index + 1 < tableBodyLength) { addTableHeader(); currentHeight += pdfConfig.subLineHeight}
          else
            currentHeight += pdfConfig.subLineHeight + 2 + pdfConfig.subLineHeight - 1; //same as in addtableHeader
            

        }

        //reset the height that was increased to check the next row
        if (index + 1 < tableBodyLength && currentHeight > 30)
            // check if new page
            currentHeight -= maxHeight;

      //  doc.text("Test Line125", 11 + startWidth, currentHeight);
    });
    doc.line(10, currentHeight, docWidth - 10, currentHeight); //if we want to show the last table line 
    //#endregion

    var invDescSize = splitTextAndGetHeight(
        param.invoice.invDesc,
        docWidth / 2
    ).height;

    //#region PAGE BREAKER
    var checkAndAddPageLandscape = function () {
        if (!param.orientationLandscape && currentHeight + invDescSize > 270) {
            doc.addPage();
            currentHeight = 10;
            if (param.header.showinAllpage) addheader()//new line of code *********
            if (param.subheaderinAllpage) subHeader(); //new line of code *********
        }
    };

    var checkAndAddPageNotLandscape = function (heightLimit = 173) {
        if (param.orientationLandscape && currentHeight + invDescSize > heightLimit) {
            doc.addPage();
            currentHeight = 10;
            if (param.header.showinAllpage) addheader()//new line of code *********
            if (param.subheaderinAllpage) subHeader(); //new line of code *********
        }
    };

    var checkAndAddPage = function () {
        checkAndAddPageNotLandscape();
        checkAndAddPageLandscape();
    };
    //#endregion

    //#region Stamp
    var addStamp = () => {
        let _addStampBase = () => {
            var stampImage = '';
            if (typeof window === "undefined") {
                stampImage = param.stamp.src;
            } else {
                stampImage = new Image();
                stampImage.src = param.stamp.src;
            }

            if (param.stamp.type)
                doc.addImage(
                    stampImage,
                    param.stamp.type,
                    10 + param.stamp.margin.left,
                    docHeight - 22 + param.stamp.margin.top,
                    param.stamp.width,
                    param.stamp.height
                );
            else
                doc.addImage(
                    stampImage,
                    10 + param.stamp.margin.left,
                    docHeight - 22 + param.stamp.margin.top,
                    param.stamp.width,
                    param.stamp.height
                );
        };

        if (param.stamp.src) {
            if (param.stamp.inAllPages)
                _addStampBase();
            else if (!param.stamp.inAllPages && doc.getCurrentPageInfo().pageNumber == doc.getNumberOfPages())
                _addStampBase();
        }
    }
    //#endregion

    checkAndAddPage();

    doc.setTextColor(colorBlack);
    doc.setFontSize(pdfConfig.labelTextSize);
    currentHeight += pdfConfig.lineHeight;

    //#region additionalRows
    if (param.invoice.additionalRows?.length > 0) {
        //#region Line breaker before invoce total
        doc.line(docWidth / 2, currentHeight, docWidth - 10, currentHeight);
        currentHeight += pdfConfig.lineHeight;
        //#endregion

        for (let i = 0; i < param.invoice.additionalRows.length; i++) {
            currentHeight += pdfConfig.lineHeight;
            doc.setFontSize(param.invoice.additionalRows[i].style.fontSize);

            doc.text(docWidth / 1.5, currentHeight, param.invoice.additionalRows[i].col1, "right");
            doc.text(docWidth - 25, currentHeight, param.invoice.additionalRows[i].col2, "right");
            doc.text(docWidth - 10, currentHeight, param.invoice.additionalRows[i].col3, "right");
            checkAndAddPage();
        }
    }
    //#endregion

    checkAndAddPage();

    doc.setTextColor(colorBlack);
    currentHeight += pdfConfig.subLineHeight;
    currentHeight += pdfConfig.subLineHeight;
    //   currentHeight += pdfConfig.subLineHeight;
    doc.setFontSize(pdfConfig.labelTextSize);

    

    //#region INVOICE DESCRIPTION
    var addInvoiceDesc = () => {
       // debugger;
        doc.setFontSize(pdfConfig.labelTextSize);
        doc.setTextColor(colorBlack);

        doc.text(param.invoice.invDescLabel, 10, currentHeight);
        currentHeight += pdfConfig.subLineHeight;
        doc.setTextColor(colorGray);
        doc.setFontSize(pdfConfig.fieldTextSize - 1);

        if (param.invoice.fullLength) {
            var lines = doc.splitTextToSize(param.invoice.invDesc, (docWidth - (pdfConfig.XSeed * 2)));
        } else {
            lines = doc.splitTextToSize(param.invoice.invDesc, docWidth / 2);
        }
            
        //text in left half
        doc.text(lines, 10, currentHeight);
        currentHeight +=doc.getTextDimensions(lines).h > 5? doc.getTextDimensions(lines).h + 6: pdfConfig.lineHeight;

        return currentHeight;
    };

     //#region for subfooter

    var addSubfooter = () => {
        //if (doc.getCurrentPageInfo().pageNumber == doc.getNumberOfPages()) {
        //last page of report/invoice
       // debugger;
       

        checkAndAddPage();
        //var footerlineHeight = currentHeight;

        if (!param.orientationLandscape && currentHeight <= 250) {
            currentHeight = 250;
        } else if (!param.orientationLandscape && currentHeight > 250) {
            doc.addPage();
            currentHeight = 250;
        } else if (param.orientationLandscape && currentHeight < 173) {
            currentHeight = 173;
        } else if (param.orientationLandscape && currentHeight > 173){
            doc.addPage();
            currentHeight = 173;
        }
        //doc.setTextColor("#de0917");
        doc.setDrawColor(blue_theme);
        doc.line(10, currentHeight, docWidth - 10, currentHeight); //if we want to show the last table line 
        currentHeight += 0.3;
        doc.line(10, currentHeight, docWidth - 10, currentHeight);
        

        currentHeight += pdfConfig.subLineHeight;

            doc.setFontSize(pdfConfig.fieldTextSize);
            doc.setTextColor(colorBlack);
            if (param.subfooter) {
                doc.text(param.subfooter.title, 10, currentHeight);
                currentHeight += pdfConfig.subLineHeight;
                doc.setTextColor(colorGray);
                doc.setFontSize(pdfConfig.fieldTextSize - 1);

               // var lines = doc.splitTextToSize(param.invoice.invDesc, docWidth / 2);
                for (let i =1; i <= 6; i++) {
                    doc.text(param.subfooter['line' + i], 10, currentHeight);
                    currentHeight += pdfConfig.subLineHeight;
                }
                
            }
        //}
    }
    


    //#end region subfooter

    //#region Add num of pages at the bottom
    if (doc.getNumberOfPages() > 1) {
        for (let i = 1; i <= doc.getNumberOfPages(); i++) {
            doc.setFontSize(pdfConfig.fieldTextSize - 2);
            doc.setTextColor(colorGray);

            if (param.pageEnable) {
                doc.text(docWidth / 2, docHeight - 10, param.footer.text, "center");
                doc.setPage(i);
                doc.text(
                    param.pageLabel + " " + i + " / " + doc.getNumberOfPages(),
                    docWidth - 20,
                    doc.internal.pageSize.height - 6
                );
            }

            checkAndAddPageNotLandscape(183);
            checkAndAddPageLandscape();
            addStamp();
        }
    }
    //#endregion

    



   // addInvoiceDesc();
    addSubfooter();
    //#endregion

    addStamp();

    //#region Add num of first page at the bottom
    if (doc.getNumberOfPages() === 1 && param.pageEnable) {
        doc.setFontSize(pdfConfig.fieldTextSize - 2);
        doc.setTextColor(colorGray);
        doc.text(docWidth / 2, docHeight - 10, param.footer.text, "center");
        doc.text(
            param.pageLabel + "1 / 1",
            docWidth - 20,
            doc.internal.pageSize.height - 6
        );
    }
    //#endregion

    let returnObj = {
        pagesNumber: doc.getNumberOfPages(),
    };

    if (param.returnJsPDFDocObject) {
        returnObj = {
            ...returnObj,
            jsPDFDocObject: doc,
        };
    }

    if (param.outputType === "save") doc.save(param.fileName);
    else if (param.outputType === "blob") {
        const blobOutput = doc.output("blob");
        returnObj = {
            ...returnObj,
            blob: blobOutput,
        };
    } else if (param.outputType === "datauristring") {
        returnObj = {
            ...returnObj,
            dataUriString: doc.output("datauristring", {
                filename: param.fileName,
            }),
        };
    } else if (param.outputType === "arraybuffer") {
        returnObj = {
            ...returnObj,
            arrayBuffer: doc.output("arraybuffer"),
        };
    } else
        doc.output(param.outputType, {
            filename: param.fileName,
        });

    return returnObj;
}


function breakTextIntoLines(text, maxWidth,doc) {
    var words = text.split(' ');
    var lines = [];
    var currentLine = words[0];

    for (var i = 1; i < words.length; i++) {
        var word = words[i];
        var testLine = currentLine + ' ' + word;

        var lineWidth = doc.getTextWidth(testLine);

        if (lineWidth < maxWidth) {
            currentLine = testLine;
        } else {
            lines.push(currentLine);
            currentLine = word;
        }
    }

    lines.push(currentLine);
    return lines;
}


function ProjectBilling(props) {
  //  debugger
    const param = {
        outputType: props.outputType || "save",
        returnJsPDFDocObject: props.returnJsPDFDocObject || false,
        fileName: props.fileName || "",
        orientationLandscape: props.orientationLandscape || false,
        compress: props.compress || false,
        logo: {
            src: props.logo?.src || "",
            type: props.logo?.type || "",
            width: props.logo?.width || "",
            height: props.logo?.height || "",
            margin: {
                top: props.logo?.margin?.top || 0,
                left: props.logo?.margin?.left || 0,
            },
        },
        stamp: {
            inAllPages: props.stamp?.inAllPages || false,
            src: props.stamp?.src || "",
            width: props.stamp?.width || "",
            height: props.stamp?.height || "",
            margin: {
                top: props.stamp?.margin?.top || 0,
                left: props.stamp?.margin?.left || 0,
            },
        },
        header: {
            firstRigthLn: props.header?.firstRigthLn || "",
            deliveryNote: props.header?.deliveryNote || "",
            date: props.header?.date || "",
            shipping_date: props.header?.shipping_date || "",
            showinAllpage: props.header?.showinAllpage || false,
            //website: props.business?.website || "",
        },
        business: {
            companyname: props.business?.companyname || "",
            address: props.business?.address || "",
            phone: props.business?.phone || "",
            email: props.business?.email || "",
            website: props.business?.website || "",
            //email_1: props.business?.email_1 || "",
            //website: props.business?.website || "",
        },
        contact: {
            label: props.contact?.label || "",
            name: props.contact?.name || "",
            address: props.contact?.address || "",
            phone: props.contact?.phone || "",
            email: props.contact?.email || "",
            website: props.contact?.website || "",
        },
        bill_to: {
            header: props.bill_to?.header || "",
            name: props.bill_to?.name || "",
            address: props.bill_to?.address || "",
            phone: props.bill_to?.phone || "",
            email: props.bill_to?.email || "",
            otherInfo: props.bill_to?.otherInfo || "",
        },
        ship_to: {
            header: props.ship_to?.header || "",
            name: props.ship_to?.name || "",
            address: props.ship_to?.address || "",
            phone: props.ship_to?.phone || "",
            email: props.ship_to?.email || "",
            otherInfo: props.ship_to?.otherInfo || "",
        },
        invoice: {
            label: props.invoice?.label || "",
            num: props.invoice?.num || "",
            invDate: props.invoice?.invDate || "",
            invGenDate: props.invoice?.invGenDate || "",
            headerBorder: props.invoice?.headerBorder || false,
            tableBodyBorder: props.invoice?.tableBodyBorder || false,
            header: props.invoice?.header || [],
            table: props.invoice?.table || [],
            tableDatakey: props.invoice?.tableDatakey || [],
            secounflineInTable: props.invoice?.secounflineInTable || [],
            invDescLabel: props.invoice?.invDescLabel || "",
            invDesc: props.invoice?.invDesc || "",
            fullLength: props.invoice?.fullLength || "",
            additionalRows: props.invoice?.additionalRows?.map(x => {
                return {
                    col1: x?.col1 || "",
                    col2: x?.col2 || "",
                    col3: x?.col3 || "",
                    style: {
                        fontSize: x?.style?.fontSize || 12,
                    }
                }
            })
        },
        subfooter: {
            title: props.subfooter?.title || "",
            line1: props.subfooter?.line1 || "",
            line2: props.subfooter?.line2 || "",
            line3: props.subfooter?.line3 || "",
            line4: props.subfooter?.line4 || "",
            line5: props.subfooter?.line5 || "",
            line6: props.subfooter?.line6 || "",
        },
        footer: {
            text: props.footer?.text || "",
        },
        pageEnable: props.pageEnable || false,
        pageLabel: props.pageLabel || "Page",
        subheaderinAllpage: props.subheaderinAllpage || false,
    };

    const splitTextAndGetHeight = (text, size) => {
        var lines = doc.splitTextToSize(text, size);
        return {
            text: lines,
            height: doc.getTextDimensions(lines).h,
        };
    };
    //removed becoz of double line
    ////if (param.invoice.table && param.invoice.table.length) {
    //if (param.invoice.tableDatakey && param.invoice.tableDatakey.length) {
    //   // if (param.invoice.table[0].length != param.invoice.header.length)
    //    if (param.invoice.tableDatakey.length != param.invoice.header.length)
    //        throw Error("Length of header and table column must be equal.");
    //}

    const options = {
        orientation: param.orientationLandscape ? "landscape" : "",
        compress: param.compress
    };

    var doc = new jsPDF(options);

    var docWidth = doc.internal.pageSize.width;
    var docHeight = doc.internal.pageSize.height;

    var colorBlack = "#000000";
    var colorGray = "#4d4e53";
    var blue_theme = "#4d4e53";
    //starting at 15mm
    var currentHeight = 15;
    //var startPointRectPanel1 = currentHeight + 6;

    var pdfConfig = {
        headerTextSize: 20,
        labelTextSize: 12,
        fieldTextSize: 10,
        lineHeight: 6,
        subLineHeight: 4,
        f1: 20,
        f2: 15,
        f3: 10,
        f4: 12,
        XSeed: 10
    };

    //get center start width
    var center_startingpoint = (text,font) => {

        var textWidth = doc.getStringUnitWidth(text) * font;
        var linestartpoint = (docWidth - (XSeed * 2)) - textWidth / 2;
        return linestartpoint;

    }



    ///header Part start
    var addheader = () => {
        doc.setFontSize(pdfConfig.f4);
        doc.setTextColor(colorBlack);

        //var textWidth = doc.getStringUnitWidth(param.header.firstRigthLn) * pdfConfig.f4;
        //var linestartpoint = (docWidth - (XSeed * 2)) - textWidth / 2;
        

        doc.text(center_startingpoint(param.header.firstRigthLn , pdfConfig.f4), currentHeight, param.header.firstRigthLn, "left");
        doc.setFontSize(pdfConfig.fieldTextSize);

        if (param.logo.src) {
            var imageHeader = '';
            if (typeof window === "undefined") {
                imageHeader = param.logo.src;
            } else {
                imageHeader = new Image();
                imageHeader.src = param.logo.src;
            }
            //
            var htmlDoc = {
                sessionDateText: ""
            };
            ////
            doc.text(htmlDoc.sessionDateText, docWidth - (doc.getTextWidth(htmlDoc.sessionDateText) + pdfConfig.XSeed), currentHeight);
            if (param.logo.type)
                doc.addImage(
                    imageHeader,
                    param.logo.type,
                    pdfConfig.XSeed + param.logo.margin.left,
                    currentHeight - 5 + param.logo.margin.top,
                    param.logo.width,
                    param.logo.height
                );
            else
                doc.addImage(
                    imageHeader,
                    pdfConfig.XSeed + param.logo.margin.left,
                    currentHeight - 5 + param.logo.margin.top,
                    param.logo.width,
                    param.logo.height
                );
        }

        debugger;
        //doc.setTextColor(colorGray);
        currentHeight += pdfConfig.subLineHeight;
        currentHeight += 2;
        //currentHeight += 12;
        doc.setFontSize(pdfConfig.f2);
        doc.setTextColor(blue_theme);
        doc.text(docWidth - pdfConfig.XSeed, currentHeight, param.header.deliveryNote, "right");
        currentHeight += 2;
        doc.setFontSize(pdfConfig.f4);
        doc.setTextColor(colorGray);
        currentHeight += pdfConfig.subLineHeight;
        doc.text(docWidth - pdfConfig.XSeed, currentHeight,  param.header.date, "right");
        doc.setFontSize(pdfConfig.f4);

        // doc.setTextColor(colorGray);
        currentHeight += pdfConfig.lineHeight;
        doc.text(docWidth - pdfConfig.XSeed, currentHeight, param.header.shipping_date, "right");

        //line breaker after logo & business info
        if (param.invoice.header.length) {
            currentHeight += pdfConfig.subLineHeight;
            doc.setDrawColor(blue_theme);
            doc.line(10, currentHeight, docWidth - pdfConfig.XSeed, currentHeight);
            currentHeight += 0.3;
            doc.line(10, currentHeight, docWidth - pdfConfig.XSeed, currentHeight);
        }
    }
    /////header Part End
    addheader();

    //business part


    ////Sub header Section Start
    var subHeader = () => {
        doc.setTextColor(colorBlack);
        doc.setFontSize(pdfConfig.headerTextSize - 5);
        currentHeight += pdfConfig.lineHeight;
        var lineHeight = currentHeight;
        var currentHeigthArry = [];
        //line 1
        if (param.business.companyname) {
            ///test  -start
            var lines = breakTextIntoLines(param.business.companyname, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);


            // Now, you can use the 'lines' array to add the text to your PDF
            for (var i = 0; i < lines.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.lineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines[i]);

            }
            currentHeigthArry.push(currentHeight);
            //lineHeight += pdfConfig.lineHeight; //reset current line for write on same line 
            ///test  --end
            // doc.text(10, currentHeight, param.business.companyname);
            //currentHeight += pdfConfig.lineHeight;
        }

        //doc.setTextColor(colorBlack);
        //doc.setFontSize(pdfConfig.headerTextSize - 5);
        //if (param.business.address) doc.text(10, currentHeight, param.business.address);



        if (param.bill_to.header) {

            //doc.text(
            //    (pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed*2)) / 3)),
            //    currentHeight,
            //    param.bill_to.header,

            //);
            currentHeight = lineHeight;
            var lines = breakTextIntoLines(param.bill_to.header, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);


            // Now, you can use the 'lines' array to add the text to your PDF
            for (var i = 0; i < lines.length; i++) {
                //doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.lineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed * 2)) / 3)), currentHeight, lines[i]);

            };
            currentHeigthArry.push(currentHeight);
        }


        if (param.ship_to.header) {
            //doc.text(
            //    (pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed*2)) / 3)*2 )),
            //    currentHeight,
            //    param.ship_to.header,

            //);
            //debugger
            currentHeight = lineHeight;


            var lines = breakTextIntoLines(param.ship_to.header, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);


            //// Now, you can use the 'lines' array to add the text to your PDF
            for (var i = 0; i < lines.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.lineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines[i]);

            };
            currentHeigthArry.push(currentHeight);

        }
        var maxVal = Math.max(...currentHeigthArry);
        currentHeight = maxVal;

        if (param.business.companyname || param.bill_to.header || param.ship_to.header) {
            currentHeight += pdfConfig.subLineHeight;  //seed
        }


        doc.setTextColor(colorGray);
        doc.setFontSize(pdfConfig.fieldTextSize - 2);



        //

        //debugger
        // line 2

        lineHeight = currentHeight;
        if (param.business.address || param.bill_to.address || param.ship_to.address) {


            //doc.text(pdfConfig.XSeed, currentHeight, param.business.address);
            //doc.text((pdfConfig.XSeed + ( (docWidth - (pdfConfig.XSeed * 2) ) / 3)), currentHeight, param.bill_to.address);
            //doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, param.ship_to.address);
            //currentHeight += pdfConfig.subLineHeight;
            var currentHeigthArr = [];

            var lines1 = breakTextIntoLines(param.business.address, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines2 = breakTextIntoLines(param.bill_to.address, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.address, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
            // doc.text(pdfConfig.XSeed, currentHeight, 'Test Position2;');
        }

        ////line 3
        //currentHeight += pdfConfig.subLineHeight; //seed
        //doc.text(pdfConfig.XSeed, currentHeight, 'Test Position3;');
        lineHeight = currentHeight;
        if (param.business.phone || param.bill_to.phone || param.ship_to.phone) {
            //doc.text(pdfConfig.XSeed, currentHeight, param.business.phone);
            //doc.text((pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed * 2)) / 3)), currentHeight, param.bill_to.phone);
            //doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, param.ship_to.phone);
            //currentHeight += pdfConfig.subLineHeight;

            var currentHeigthArr = [];

            var lines1 = breakTextIntoLines(param.business.phone, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines2 = breakTextIntoLines(param.bill_to.phone, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.phone, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
        }

        ////line 4
        //currentHeight += pdfConfig.subLineHeight; //seed
        lineHeight = currentHeight;
        if (param.business.email || param.bill_to.email || param.ship_to.email) {
            //doc.text(pdfConfig.XSeed, currentHeight, param.business.email);
            //doc.text((pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed * 2)) / 3)), currentHeight, param.bill_to.email);
            //doc.text((pdfConfig.XSeed + ( ( (docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, param.ship_to.email);
            //currentHeight += pdfConfig.subLineHeight;
            var currentHeigthArr = [];

            var lines1 = breakTextIntoLines(param.business.email, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines2 = breakTextIntoLines(param.bill_to.email, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.email, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
        }

        ////line 5
        //currentHeight += pdfConfig.subLineHeight;
        lineHeight = currentHeight;
        if (param.business.website || param.bill_to.otherInfo || param.ship_to.otherInfo) {
            //doc.text(pdfConfig.XSeed, currentHeight, param.business.website);
            //doc.text((pdfConfig.XSeed + ((docWidth - (pdfConfig.XSeed * 2)) / 3)), currentHeight, param.bill_to.otherInfo);
            //doc.text((pdfConfig.XSeed + ( ( (docWidth - (pdfConfig.XSeed * 2) ) / 3) * 2)), currentHeight, param.ship_to.otherInfo);
            //currentHeight += pdfConfig.subLineHeight;
            var currentHeigthArr = [];

            var lines1 = breakTextIntoLines(param.business.website, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines2 = breakTextIntoLines(param.bill_to.otherInfo, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            var lines3 = breakTextIntoLines(param.ship_to.otherInfo, ((docWidth - (pdfConfig.XSeed * 2)) / 3) - 2, doc);
            for (let i = 0; i < lines1.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text(pdfConfig.XSeed, currentHeight, lines1[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines2.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3))), currentHeight, lines2[i]);

            };
            currentHeigthArr.push(currentHeight);
            currentHeight = lineHeight;
            for (let i = 0; i < lines3.length; i++) {
                // doc.text(lines[i], xCoordinate, currentHeight);
                if (i > 0) currentHeight += pdfConfig.subLineHeight; // Update the vertical position if needed
                doc.text((pdfConfig.XSeed + (((docWidth - (pdfConfig.XSeed * 2)) / 3) * 2)), currentHeight, lines3[i]);

            };
            currentHeigthArr.push(currentHeight);
            var maxValue = Math.max(...currentHeigthArr);
            //if (maxValue == (lineHeight)) maxValue += pdfConfig.subLineHeight;
            maxValue += pdfConfig.subLineHeight;
            currentHeight = maxValue;
        }
        //end contact part


        //line breaker after logo & business info
        if (param.invoice.header.length) {
            currentHeight += pdfConfig.subLineHeight;
            doc.setDrawColor(blue_theme);
            doc.line(10, currentHeight, docWidth - pdfConfig.XSeed, currentHeight);
            currentHeight += 0.3;
            doc.line(10, currentHeight, docWidth - pdfConfig.XSeed, currentHeight);
        }
    }
    subHeader();
    ////Sub header Section end





    //TABLE PART
    //var tdWidth = 31.66;
    //10 margin left - 10 margin right
    //var tdWidth = (140 - 20) / param.invoice.header.length;
    // Get the page size


    // Extract the width from the page size
    var pageWidth = docWidth;
    var tdWidth = (pageWidth - 20) / param.invoice.header.length;

    //#region TD WIDTH
    if (param.invoice.header.length > 2) { //add style for 2 or more columns
        const customColumnNo = param.invoice.header.map(x => x?.style?.width || 0).filter(x => x > 0);
        let customWidthOfAllColumns = customColumnNo.reduce((a, b) => a + b, 0);
        //tdWidth = (140 - 20 - customWidthOfAllColumns) / (param.invoice.header.length - customColumnNo.length);
        tdWidth = (pageWidth - 20 - customWidthOfAllColumns) / (param.invoice.header.length - customColumnNo.length);
    }
    //#endregion

    //#region TABLE HEADER BORDER
    var addTableHeaderBorder = () => {
        currentHeight += 2;
        const lineHeight = 7;
        let startWidth = 0;
        for (let i = 0; i < param.invoice.header.length; i++) {
            const currentTdWidth = param.invoice.header[i]?.style?.width || tdWidth;
            if (i === 0) doc.rect(10, currentHeight, currentTdWidth, lineHeight);
            else {
                const previousTdWidth = param.invoice.header[i - 1]?.style?.width || tdWidth;
                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
                startWidth += widthToUse;
                doc.rect(startWidth + 10, currentHeight, currentTdWidth, lineHeight);
            }
        }
        currentHeight -= 2;
    };
    //#endregion

    //#region TABLE BODY BORDER
    var addTableBodyBorder = (lineHeight) => {
        let startWidth = 0;
        for (let i = 0; i < param.invoice.header.length; i++) {
            const currentTdWidth = param.invoice.header[i]?.style?.width || tdWidth;
            if (i === 0) doc.rect(10, currentHeight, currentTdWidth, lineHeight);
            else {
                const previousTdWidth = param.invoice.header[i - 1]?.style?.width || tdWidth;
                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
                startWidth += widthToUse;
                doc.rect(startWidth + 10, currentHeight, currentTdWidth, lineHeight);
            }
        }
    };
    //#endregion

    //#region TABLE HEADER
    var addTableHeader = () => {
        if (param.invoice.headerBorder) addTableHeaderBorder();

        currentHeight += pdfConfig.subLineHeight;


        doc.setTextColor(colorBlack);
        doc.setFontSize(pdfConfig.fieldTextSize);
        //border color
        doc.setDrawColor(colorGray);
        currentHeight += 2;

        let startWidth = 0;
        param.invoice.header.forEach(function (row, index) {
            if (index == 0) doc.text(row.title, 11, currentHeight);
            else {
                const currentTdWidth = row?.style?.width || tdWidth;
                const previousTdWidth = param.invoice.header[index - 1]?.style?.width || tdWidth;
                const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
                startWidth += widthToUse;
                doc.text(row.title, startWidth + 11, currentHeight);
            }
        });

        currentHeight += pdfConfig.subLineHeight - 1;
        doc.setTextColor(colorGray);

    };
    //#endregion

    addTableHeader();

    //#region TABLE BODY
    var tableBodyLength = param.invoice.table.length;
    param.invoice.table.forEach(function (row, index) {
        doc.line(10, currentHeight, docWidth - 10, currentHeight);

        //get nax height for the current row
        var getRowsHeight = function () {
            let rowsHeight = [];
            //row.forEach(function (rr, index) {
            //    const widthToUse = param.invoice.header[index]?.style?.width || tdWidth;

            //    let item = splitTextAndGetHeight(rr.toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders
            //    rowsHeight.push(item.height);
            //});
            debugger
          //  console.log(row, 'row')
            param.invoice.tableDatakey.forEach((d, i) => {
              //  console.log(d, 'd')
                const widthToUse = param.invoice.header[index]?.style?.width || tdWidth;
                if (row[d]) {
                    let item = splitTextAndGetHeight((row[d]).toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders
                    rowsHeight.push(item.height);
                }

            });

            return rowsHeight;
        };

        var maxHeight = Math.max(...getRowsHeight());

        //body borders
        if (param.invoice.tableBodyBorder) addTableBodyBorder(maxHeight + 1);



        ////let currenthightreset = currentHeight;    ///test line *****************
        let startWidth = 0;
        //let secoundlineHeight = currentHeight;     ///test line *****************
        //row.forEach(function (rr, index) {

        //     ///test line *****************
        //    const widthToUse = param.invoice.header[index]?.style?.width || tdWidth;
        //    let item = splitTextAndGetHeight(rr.toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders

        //    if (index == 0) doc.text(item.text, 11, currentHeight + 4);
        //    else {
        //        const currentTdWidth = rr?.style?.width || tdWidth;
        //        const previousTdWidth = param.invoice.header[index - 1]?.style?.width || tdWidth;
        //        const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
        //        startWidth += widthToUse;
        //        doc.text(item.text, 11 + startWidth, currentHeight + 4);

        //        secoundlineHeight=currentHeight +pdfConfig.subLineHeight; ////////////////////******************
        //        doc.text('Test Line', 11 + startWidth, secoundlineHeight + 4 );   ///test line *****************
        //    };

        //});

        ///test
        let secoundlineHeight = currentHeight;     ///test line *****************
        param.invoice.tableDatakey.forEach((data, index2) => {
            //debugger

           // console.log(row[data]);
            if (!param.invoice.secounflineInTable.includes(data)) {
                const widthToUse = param.invoice.header[index2]?.style?.width || tdWidth;
                let item = splitTextAndGetHeight((row[data] ? row[data] : "").toString(), widthToUse - 1); //minus 1, to fix the padding issue between borders

                // if (index == 0) doc.text(item.text, 11, currentHeight + 4);
                if (index2 == 0) doc.text((index + 1).toString(), 11, currentHeight + 4);
                else {
                    const currentTdWidth = (row[data])?.style?.width || tdWidth;
                    const previousTdWidth = param.invoice.header[index2 - 1]?.style?.width || tdWidth;
                    const widthToUse = currentTdWidth == previousTdWidth ? currentTdWidth : previousTdWidth;
                    startWidth += widthToUse;
                   



                    doc.text(item.text, 11 + startWidth, currentHeight + 4);
                    if (item.text.length)
                        for (let i = 1; i <= item.text.length; i++){
                    currentHeight += pdfConfig.subLineHeight;
                      };
                    secoundlineHeight = currentHeight + pdfConfig.subLineHeight;   ////////////////////******************
                    if (data == "Product") {
                        doc.setFontSize(8);
                        doc.text(row["Service"], 11 + startWidth, secoundlineHeight + 4); /////////test line *****************
                    } else if (data == "Qty") {
                        doc.setFontSize(8);
                        doc.text(row["Uom"], 11 + startWidth, secoundlineHeight + 4); /////////test line *****************
                    }

                };
            };

        });
        //for (let person of ab) {
        //    console.log(`Name: ${person.name}, Age: ${person.age}, Place: ${person.place}`);
        //}



        ///test


        currentHeight = secoundlineHeight;  ///test line *****************

        //currentHeight += maxHeight - 4;

        //td border height
        currentHeight += 5;

        //pre-increase currentHeight to check the height based on next row
        if (index + 1 < tableBodyLength) currentHeight += maxHeight;

        if (
            param.orientationLandscape && (currentHeight > 185 || (currentHeight > 178 && doc.getNumberOfPages() > 1))
        ) {
            doc.addPage();
            currentHeight = 10;
            if (param.header.showinAllpage) addheader()//new line of code *********
            if (param.subheaderinAllpage) subHeader(); //new line of code *********
            if (index + 1 < tableBodyLength) { addTableHeader(); currentHeight += pdfConfig.subLineHeight }
            else currentHeight += pdfConfig.subLineHeight;
        }

        if (
            !param.orientationLandscape &&
            (currentHeight > 265 ||
                (currentHeight > 255 && doc.getNumberOfPages() > 1))

        ) {
            doc.addPage();
            currentHeight = 10;
            if (param.header.showinAllpage) addheader()//new line of code *********
            if (param.subheaderinAllpage) subHeader(); //new line of code *********
            if (index + 1 < tableBodyLength) { addTableHeader(); currentHeight += pdfConfig.subLineHeight }
            else
                currentHeight += pdfConfig.subLineHeight + 2 + pdfConfig.subLineHeight - 1; //same as in addtableHeader


        }

        //reset the height that was increased to check the next row
        if (index + 1 < tableBodyLength && currentHeight > 30)
            // check if new page
            currentHeight -= maxHeight;

          doc.text("Test Line125", 11 + startWidth, currentHeight);
    });
    doc.line(10, currentHeight, docWidth - 10, currentHeight); //if we want to show the last table line 
    //#endregion

    var invDescSize = splitTextAndGetHeight(
        param.invoice.invDesc,
        docWidth / 2
    ).height;

    //#region PAGE BREAKER
    var checkAndAddPageLandscape = function () {
        if (!param.orientationLandscape && currentHeight + invDescSize > 270) {
            doc.addPage();
            currentHeight = 10;
            if (param.header.showinAllpage) addheader()//new line of code *********
            if (param.subheaderinAllpage) subHeader(); //new line of code *********
        }
    };

    var checkAndAddPageNotLandscape = function (heightLimit = 173) {
        if (param.orientationLandscape && currentHeight + invDescSize > heightLimit) {
            doc.addPage();
            currentHeight = 10;
            if (param.header.showinAllpage) addheader()//new line of code *********
            if (param.subheaderinAllpage) subHeader(); //new line of code *********
        }
    };

    var checkAndAddPage = function () {
        checkAndAddPageNotLandscape();
        checkAndAddPageLandscape();
    };
    //#endregion

    //#region Stamp
    var addStamp = () => {
        let _addStampBase = () => {
            var stampImage = '';
            if (typeof window === "undefined") {
                stampImage = param.stamp.src;
            } else {
                stampImage = new Image();
                stampImage.src = param.stamp.src;
            }

            if (param.stamp.type)
                doc.addImage(
                    stampImage,
                    param.stamp.type,
                    10 + param.stamp.margin.left,
                    docHeight - 22 + param.stamp.margin.top,
                    param.stamp.width,
                    param.stamp.height
                );
            else
                doc.addImage(
                    stampImage,
                    10 + param.stamp.margin.left,
                    docHeight - 22 + param.stamp.margin.top,
                    param.stamp.width,
                    param.stamp.height
                );
        };

        if (param.stamp.src) {
            if (param.stamp.inAllPages)
                _addStampBase();
            else if (!param.stamp.inAllPages && doc.getCurrentPageInfo().pageNumber == doc.getNumberOfPages())
                _addStampBase();
        }
    }
    //#endregion

    checkAndAddPage();

    doc.setTextColor(colorBlack);
    doc.setFontSize(pdfConfig.labelTextSize);
    currentHeight += pdfConfig.lineHeight;

    //#region additionalRows
    if (param.invoice.additionalRows?.length > 0) {
        //#region Line breaker before invoce total
        doc.line(docWidth / 2, currentHeight, docWidth - 10, currentHeight);
        currentHeight += pdfConfig.lineHeight;
        //#endregion

        for (let i = 0; i < param.invoice.additionalRows.length; i++) {
            currentHeight += pdfConfig.lineHeight;
            doc.setFontSize(param.invoice.additionalRows[i].style.fontSize);

            doc.text(docWidth / 1.5, currentHeight, param.invoice.additionalRows[i].col1, "right");
            doc.text(docWidth - 25, currentHeight, param.invoice.additionalRows[i].col2, "right");
            doc.text(docWidth - 10, currentHeight, param.invoice.additionalRows[i].col3, "right");
            checkAndAddPage();
        }
    }
    //#endregion

    checkAndAddPage();

    doc.setTextColor(colorBlack);
    currentHeight += pdfConfig.subLineHeight;
    currentHeight += pdfConfig.subLineHeight;
    //   currentHeight += pdfConfig.subLineHeight;
    doc.setFontSize(pdfConfig.labelTextSize);



    //#region INVOICE DESCRIPTION
    var addInvoiceDesc = () => {
        debugger;
        doc.setFontSize(pdfConfig.labelTextSize);
        doc.setTextColor(colorBlack);

        doc.text(param.invoice.invDescLabel, 10, currentHeight);
        currentHeight += pdfConfig.subLineHeight;
        doc.setTextColor(colorGray);
        doc.setFontSize(pdfConfig.fieldTextSize - 1);

        if (param.invoice.fullLength) {
            var lines = doc.splitTextToSize(param.invoice.invDesc, (docWidth - (pdfConfig.XSeed * 2)));
        } else {
            lines = doc.splitTextToSize(param.invoice.invDesc, docWidth / 2);
        }

        //text in left half
        doc.text(lines, 10, currentHeight);
        currentHeight += doc.getTextDimensions(lines).h > 5 ? doc.getTextDimensions(lines).h + 6 : pdfConfig.lineHeight;

        return currentHeight;
    };

    //#region for subfooter

    var addSubfooter = () => {
        //if (doc.getCurrentPageInfo().pageNumber == doc.getNumberOfPages()) {
        //last page of report/invoice
       // debugger;


        checkAndAddPage();
        //var footerlineHeight = currentHeight;

        if (!param.orientationLandscape && currentHeight <= 250) {
            currentHeight = 250;
        } else if (!param.orientationLandscape && currentHeight > 250) {
            doc.addPage();
            currentHeight = 250;
        } else if (param.orientationLandscape && currentHeight < 173) {
            currentHeight = 173;
        } else if (param.orientationLandscape && currentHeight > 173) {
            doc.addPage();
            currentHeight = 173;
        }
        //doc.setTextColor("#de0917");
        doc.setDrawColor(colorBlack );
        doc.line(10, currentHeight, docWidth - 10, currentHeight); //if we want to show the last table line 
        currentHeight += 0.3;
        doc.line(10, currentHeight, docWidth - 10, currentHeight);


        currentHeight += pdfConfig.subLineHeight;

        doc.setFontSize(pdfConfig.fieldTextSize);
        doc.setTextColor(colorBlack);
        if (param.subfooter) {
            doc.text(param.subfooter.title, 10, currentHeight);
            currentHeight += pdfConfig.subLineHeight;
            doc.setTextColor(colorGray);
            doc.setFontSize(pdfConfig.fieldTextSize - 1);

            // var lines = doc.splitTextToSize(param.invoice.invDesc, docWidth / 2);
            for (let i = 1; i <= 6; i++) {
                doc.text(param.subfooter['line' + i], 10, currentHeight);
                currentHeight += pdfConfig.subLineHeight;
            }

        }
        //}
    }



    //#end region subfooter

    //#region Add num of pages at the bottom
    if (doc.getNumberOfPages() > 1) {
        for (let i = 1; i <= doc.getNumberOfPages(); i++) {
            doc.setFontSize(pdfConfig.fieldTextSize - 2);
            doc.setTextColor(colorGray);

            if (param.pageEnable) {
                doc.text(docWidth / 2, docHeight - 10, param.footer.text, "center");
                doc.setPage(i);
                doc.text(
                    param.pageLabel + " " + i + " / " + doc.getNumberOfPages(),
                    docWidth - 20,
                    doc.internal.pageSize.height - 6
                );
            }

            checkAndAddPageNotLandscape(183);
            checkAndAddPageLandscape();
            addStamp();
        }
    }
    //#endregion





    // addInvoiceDesc();
    addSubfooter();
    //#endregion

    addStamp();

    //#region Add num of first page at the bottom
    if (doc.getNumberOfPages() === 1 && param.pageEnable) {
        doc.setFontSize(pdfConfig.fieldTextSize - 2);
        doc.setTextColor(colorGray);
        doc.text(docWidth / 2, docHeight - 10, param.footer.text, "center");
        doc.text(
            param.pageLabel + "1 / 1",
            docWidth - 20,
            doc.internal.pageSize.height - 6
        );
    }
    //#endregion

    let returnObj = {
        pagesNumber: doc.getNumberOfPages(),
    };

    if (param.returnJsPDFDocObject) {
        returnObj = {
            ...returnObj,
            jsPDFDocObject: doc,
        };
    }

    if (param.outputType === "save") doc.save(param.fileName);
    else if (param.outputType === "blob") {
        const blobOutput = doc.output("blob");
        returnObj = {
            ...returnObj,
            blob: blobOutput,
        };
    } else if (param.outputType === "datauristring") {
        returnObj = {
            ...returnObj,
            dataUriString: doc.output("datauristring", {
                filename: param.fileName,
            }),
        };
    } else if (param.outputType === "arraybuffer") {
        returnObj = {
            ...returnObj,
            arrayBuffer: doc.output("arraybuffer"),
        };
    } else
        doc.output(param.outputType, {
            filename: param.fileName,
        });

    return returnObj;
}
