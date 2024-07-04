var table;
var table2;
var table3;
$(function () {


    $(".rptfilters").change(function () {
        $("#divReportSection").hide();
    });
    $("#ID_Report").change(function () {
        $("#divReportSection").hide();
    })
   

  
    $.noConflict(); var Profit = ""; var loss = ""; var prftflag = false; var lossflag = false; var prftamnt = false; var lossamnt = false;
    var groupNames = {}; groupBy = []; var ProfitPercentage = ""; var prftperflag = false; var prftperamnt = false;
    var losspercentage = "";
    if ($("#customers").html().trim() != "") {
        groupBy = [];// ["Criteria"];
        var columns = []; var Criteria = ""; var Criteria2 = ""; var Criteria2Val = 0;
        var rptMode = $("#ID_Report").val();
        Criteria = $("#Criteria  option:selected").text();
        var CriteriaVal = $("#Criteria").val();
        if ($("#Criteria2") != null && $("#Criteria2").val() != "0") {
            Criteria2 = $("#Criteria2 option:selected").text();

            Criteria2Val = $("#Criteria2").val();
        }
        else if ($("#Criteria1") != null && $("#Criteria1").val() != "0") {
            Criteria2 = $("#Criteria1 option:selected").text();

            Criteria2Val = $("#Criteria1").val();
        }
        var col = []; var j = 0; var visible = true;
        var newcolumns = [];
        var cmmnRpt_Type = $("#cmmnRpt_Type").val().trim();
        var cmmnRpt_grp = $("#cmmnRpt_grp").val().split(',');

        var hddncolmn = ["cmmnRpt_Type"];
        var exists = false;
        //alert(cmmnRpt_Type);
        if (cmmnRpt_Type == "2INSL") {
            if (rptMode == 1) {


                if (Criteria2Val != "0") {
                    hddncolmn = [Criteria2, "Bill No", "Bill Date", "Customer", "Mobile", "Category", "Bill Total", "Discount", "HeaderColor", "hdn_cmmnRpt_Type"];

                    groupBy = ["Branch", "Department"];
                    if (Criteria2Val != "0") {
                        groupBy.push(Criteria2);
                    } else {
                        groupBy.push("Bill Type");
                    }
                } else {
                    hddncolmn = ["Bill No", "Bill Date", "Customer", "Mobile", "Category", "Bill Total", "Discount", "HeaderColor", "hdn_cmmnRpt_Type"];

                    groupBy = ["Branch", "Department"];



                }

            }
            else if (rptMode == 3) {

                hddncolmn = [Criteria2, "ID_Sales", "Department", "Bill Type", "Mobile", "Category", "Product", "Qty", "MRP", "Disc", "Total Amt", "Other charges", "Sales Price", "Taxable Amnt", "Tax Amt", "Roundoff", "HeaderColor", "hdn_cmmnRpt_Type"];
                if (Criteria2Val != "0") {
                    groupBy.replace("Criteria", Criteria2);
                }
            }
            else {
                hddncolmn = [Criteria, "ID_Sales", "Department", "Bill Type", "Mobile", "Category", "Bill Total", "Bill Discount", "Other charges", "Advance Amount", "Roundoff", "Net Amount", "Criteria", "HeaderColor", "hdn_cmmnRpt_Type"];
                groupBy = ["Branch"];

                if (CriteriaVal != "0") {
                    groupBy.push(Criteria);
                }
            }
        }
        else if (cmmnRpt_Type == "14INSL") {
            if (rptMode == 14) {
                hddncolmn = [Criteria2, "Branch", "Department", "Bill Type", "Mobile", "Category", "Product", "Qty", "MRP", "Disc", "Total Amt", "Other charges", "Sales Price", "Taxable Amnt", "Tax Amt", "Roundoff", "HeaderColor", "hdn_cmmnRpt_Type"];

            } if (CriteriaVal != "0") {

                groupBy.push(Criteria);
            } else if (Criteria2Val != "0") {

                groupBy.push(Criteria2);
            }
        }
        else if (cmmnRpt_Type == "3INSLHO")
        {
         
            hddncolmn.push("HeaderColor");
            hddncolmn.push("ComCategory"); 
            hddncolmn.push(Criteria2);
        }
        else if (cmmnRpt_Type == "1PRJC") {
            groupBy = [];
        }
        else if (cmmnRpt_Type == "2PRJC") {
            groupBy = ["Project", "Team"];
        }
        else if ($('.DivCriteriaProdLead').is(":visible")) {

            if ($("#ProdRptCriteria") != null && $("#ProdRptCriteria").val() != "") {
                groupBy = $("#ProdRptCriteria option:selected").text();
                groupBy = groupBy.replace(" Type", "").trim();
            }
        }
        else {


            if (CriteriaVal != "0" && CriteriaVal != null && CriteriaVal != "") {
                if (groupBy != null) {

                    groupBy = [Criteria];
                }
            } else if (Criteria2Val != "0" && Criteria2Val != null && Criteria2Val != "") {
                if (groupBy != null) {
                    groupBy = [Criteria2];
                }
            }
        }
        if (cmmnRpt_grp.length > 0) {
            if (cmmnRpt_grp[0].trim() != "") {
                groupBy = cmmnRpt_grp;
            }

        }
        hddncolmn.push("Criteria");
        var keys = [];
        var tabledata = [];


        $("#zero_config thead tr th").each(function () {


            col = $(this).text().trim().replace('.', '#').split('_');
            visible = true;


            
        

            if (col[0].trim().slice(0, 2) == "FK") {
                visible = false;
            }
            else if (col[0].trim().slice(0, 2) == "ID") {
                visible = false;
            }
            else if (col[0].trim().slice(0, 3) == "hdn") {
                
                var hdncol = $(this).text().trim().split('_');
                col[0] = hdncol[1];
                visible = false;

            }
           
           
            if (hddncolmn.indexOf(col[0].trim()) != -1) {
                visible = false;
            }

            if (groupBy != null) {

                if (groupBy.indexOf(col[0].trim()) != -1) {
                    visible = false;
                }

            }
            if (cmmnRpt_Type == "1PRJC") {
                groupBy = [];


            }

            if (col.length > 1) {
debugger

                $(this).text(col[0].trim());
                if (col[1].toUpperCase().trim() == "R") {
                    columns.push({
                        title: col[0].trim(),
                        field: col[0].trim(),
                        autoResize: true,
                        align: "right",
                        accessorDownload: colMoneyFormatter,
                        formatter: "money",
                        titleFormatter: customFormatter,
                        bottomCalc: "sum",
                        bottomCalcParams: {
                            precision: 2
                        },
                        bottomCalcFormatter: "money",
                        bottomCalcFormatterParams: {
                            decimal: ".",
                            thousand: ","
                        },
                        formatterParams: {
                            decimal: ".",
                            thousand: ","
                        },
                        cssClass: "rightAlgn",
                        visible: visible
                      
                    }); //put elements into array
                }
                else {
                    columns.push({
                        title: col[0].trim(),
                        field: col[0].trim(),
                        formatter: "column",
                        titleFormatter: customFormatter,
                        align: "left",
                        visible: visible,
                        autoResize: true                       
                    });
                }
            }
            else {
                $(this).text(col[0].trim());
                if (col[0].trim() == "Sl No") {
                    columns.push({
                        title: col[0].trim(),
                        field: col[0].trim(),
                        autoResize: true,
                        formatter: "column",
                        //formatter: "rownum",
                        titleFormatter: customFormatter,
                        align: "left",
                        visible: visible,
                        sorter: "number"
                        
                    });
                } else {
                    columns.push({
                        title: col[0].trim(),
                        field: col[0].trim(),
                        autoResize: true,
                        formatter: "column",
                        titleFormatter: customFormatter,
                        align: "left",
                        visible: visible
                        
                    });
                }
              

            }




            var k = $(this).text().trim();

            keys.push(k);

        });
        function customFormatter(cell, formatterParams, onRendered) {
            var currentColumnName = cell.getColumn().getField();
            var output = currentColumnName.replace("#", ".");
            return output;
        }

       
        $("#zero_config tbody tr").each(function (i, el) {
            var row = {}

            $.each(keys, function (k, v) {

                row[v] = $("td:eq(" + k + ")", el).text().trim();

            });

            tabledata.push(row);
        });
        if (cmmnRpt_Type == "13PRJC") {
            var Profit, ProfitPercentage;
            $("#zero_config tfoot tr ").each(function (i, el) {

                $(el).find("td").each(function (j, cell) {
                    
                    var cellText = $(cell).text().trim(); 
                    var cellId = $(cell).attr('id'); 
                    console.log("Row " + i + ", Cell ID: " + cellId + ", Cell Text: " + cellText);
                    if (parseFloat($(this).text().trim() == "" ? "0" : $(this).text().trim()) > 0) {
                        if (i == 0) {
                            Profit += "<div>Profit:<span> " + colMoneyFormatter(parseFloat(cellText)) + "</span></div>";
                        } else {
                            ProfitPercentage += "<div>Profit%:<span> " + colMoneyFormatter(parseFloat(cellText).toFixed(2)) + "</span></div>";
                        }
                    }
                    else {
                        Profit = "<div style='color: #e6e6e6;'>Profit:<span> " + colMoneyFormatter(parseFloat(cellText).toFixed(2)) + "</span></div>";
                    }
                });

            });
          
            $("#zero_config2 tfoot tr ").each(function (i, el) {

                $(el).find("td").each(function (j, cell) {
                    var cellText = $(cell).text().trim();
                    var cellId = $(cell).attr('id'); 
                    console.log("Row " + i + ", Cell ID: " + cellId + ", Cell Text: " + cellText);
                if (parseFloat($(this).text().trim() == "" ? "0" : $(this).text().trim()) > 0) {
                    if (i == 0) {
                        loss += "<div>Loss:<span> " + colMoneyFormatter(parseFloat(cellText)) + "</span></div>";
                    } else {
                        losspercentage += "<div>Loss%:<span> " + colMoneyFormatter(parseFloat(cellText).toFixed(2)) + "</span></div>";
                    }
                   // loss = "<div>Loss:<span> " + colMoneyFormatter(parseFloat($(this).text()).toFixed(2)) + "</span></div>"; lossflag = true; lossamnt = $(this).text();
                }
                else {
                    loss = "<div style='color: #e6e6e6;'>Loss:<span> " + colMoneyFormatter(parseFloat($(this).text()).toFixed(2)) + "</span></div>"; lossflag = false; lossamnt = "0";
                }
                });

            });
           
        }
     
        var hasGroupSummary = {};

        table = new Tabulator("#zero_config", {
            //width: "100%",            
            data: tabledata,
            //renderHorizontal: "virtual",
            layout: "fitColumns",
            columns: columns,
            initialSort: [
                { column: "Sl No", dir: "asc" } // set the default sort order by Serial Number in ascending order
            ],
            footerElement: losspercentage+loss,

            //autoColumns: false,
            //columnCalcs: "table",
            footerVisible: true,
            groupBy: groupBy,
            groupClosedShowCalcs: [true, true],
            columnCalcs: "both",
            groupStartOpen: [true, true],
            groupHeader: (value, count, data, group) => {
                //value - the value all members of this group share
                //count - the number of rows in this group
                //data - an array of all the row data objects in this group
                //group - the group component for the group               
                var field = group.getField();              

                if (cmmnRpt_Type == "2INSL") {

                    if (rptMode == 1) {

                        switch (field) {
                            case "Branch":
                                return `<span style='color:#111; margin-left:10px; '>Branch : ${value}</span> <span style='color:#4970cc; margin-left:10px;'> </span>`;
                            case "Department":
                                return `<span style='color:#111; margin-left:10px;'>Department : ${value}</span> <span style='color:#4970cc; margin-left:10px;'></span>`;
                            case "": "All <span style='color:#4970cc; margin-left:10px;'></span>";
                            case Criteria2:

                                return "<span style='color:#111; margin-left:10px;'>" + Criteria2 + " : " + data[0][Criteria2] + ", Bill Date :" + data[0]["Bill Date"] + ", Customer: " + data[0]["Customer"] + ", BillNo: " + data[0]["Bill No"] + ", Mobile: " + data[0]["Mobile"] + " </span> <span style='color:#4970cc; margin-left:10px;'></span>";
                        }
                    }
                    else if (rptMode == 2) {

                        switch (field) {
                            case "Branch":
                                return `<span style='color:#111; margin-left:10px; '>Branch : ${value}</span> <span style='color:#4970cc; margin-left:10px;'> </span>`
                            case "": "All <span style='color:#4970cc; margin-left:10px;'></span>";
                            case Criteria:
                                return `<span style='color:#111; margin-left:10px;'>${Criteria}: ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`

                        }

                    }
                    else if (rptMode == 3) {
                        return `<span style='color:#111; margin-left:10px;'>  ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`



                    }
                    else {
                        return `<span style='color:#111; margin-left:10px;'>  ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`



                    }
                }
                else if (cmmnRpt_Type == "2PRJC") {

                    return `<span style='color:#111; margin-left:10px;'> ${field} : ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`


                }
                else if (cmmnRpt_Type == "13PRJC") {

                    //var groupData = group.getTable().getData().filter((item) => item[field] == value);
                    //var groupSum = groupData.reduce((total, row) => total + row.IncomeAmount, 0)
                    ////var groupSum = data.reduce((total, row) => total + row.IncomeAmount, 0)


                    if (value != null && value != "undefined") {
                        switch (value) {
                            case "Other Expense":
                                return `<span style='color:#111; margin-left:10px;' >  ${value} </span>`
                            case "Extra Work":
                                return `<span style='color:#111; margin-left:10px;'>  ${value} </span>`

                        }

                        return `<span style='color:#111; margin-left:10px;' >  ${value} </span>`
                    } else {
                        return "";
                    }
                }
                else {

                      if (value != null && field != "" && field != null)
                      {
                        if (field != "Criteria")
                        {
                            return `<span style='color:#111; margin-left:10px;'> ${field}: ${value} </span > <span style='color:#4970cc; margin-left:10px;'></span>`
                        }
                        else {
                            return `<span style='color:#111; margin-left:10px;'> ${value} </span > <span style='color:#4970cc; margin-left:10px;'></span>`

                        }

                      }
                       else
                       {
                        return `<span style='color:#111; margin-left:10px;'>   </span> <span style='color:#4970cc; margin-left:10px;'>  </span>`
                       }


                    //return value + "(" + count + ") ";


                }
               },
            groupHeaderDownload: function (value, count, data, group) {
                var field = group.getField();

                if (cmmnRpt_Type == "2INSL") {

                    if (rptMode == 1) {

                        switch (field) {
                            case "Branch":
                                return ` Branch : ${value}`;
                            case "Department":
                                return ` Department : ${value}`;
                            case "": "All  ";
                            case Criteria2:
                                return Criteria2 + " : " + data[0][Criteria2] + ", Bill Date :" + data[0]["Bill Date"] + ", Customer: " + data[0]["Customer"] + ", BillNo: " + data[0]["Bill No"] + ", Mobile: " + data[0]["Mobile"] ;
                        }
                    }
                    else if (rptMode == 2) {

                        switch (field) {
                            case "Branch":
                                return ` Branch : ${value}`
                            case "Department":
                                return ` Department : ${value} `;
                            case "": "All   ";
                            case Criteria:

                                return Criteria + ` : ${value}  `

                        }

                    }
                }
                else {

                    if (value != null && field != "" && field != null) {
                        if (cmmnRpt_Type == "13PRJC") {
                            switch (value) {
                                case "Other Expense":
                                    return `<span style='color:#111; margin-left:10px;' class='rightgrp'>  ${value} </span>`
                                case "Extra Work":
                                    return `<span style='color:#111; margin-left:10px;' class='rightgrp'>  ${value} </span>`

                            }
                        } else {
                            if (field != "Criteria") {
                                return `${field} : ${value} `
                            }
                            else {
                                return ` ${value}`
                            }

                        }
                    }
                    else {
                        if (value != null) {

                            return value ;
                        }
                    }

                }

            },

            downloadConfig: {

                columnHeaders: true, //do not include column headers in downloaded table
                columnGroups: true, //do not include column groups in column headers for downloaded table
                rowGroups: true, //do not include row groups in downloaded table
                columnCalcs: true, //do not include column calcs in downloaded table
                dataTree: false, //do not include data tree in downloaded table
             
            },
            groupToggleElement: ["arrow", "arrow"],
          
            columnFormatter: function (row) {

                row.getElement().style.color = "red";
            }
        });
    
        if (groupBy == null || groupBy.length <= 0) {
            table.setGroupBy(false);
        }
        else if (groupBy.length > 0) {
            var groupby_ = groupBy[0];
            if (groupby_ == "None") {
                table.setGroupBy(false);
            }
        }
        if (cmmnRpt_Type == "13PRJC") {
            console.log(cmmnRpt_grp, "Colo");
            tabledata2 = []; var columns2 = []; var col2 = []; var keys2 = [];
            $("#zero_config2 thead tr th").each(function () {

                col2 = $(this).text().split('_');
                visible = true;


                if (col2[0].slice(0, 2) == "FK") {
                    visible = false;
                }
                else if (col2[0].slice(0, 2) == "ID") {
                    visible = false;
                }
                else if (col2[0].slice(0, 3).trim() == "hdn") {
                    var hdncol = $(this).text().split('_');
                    col2[0] = hdncol[1];
                    visible = false;

                }
                if (hddncolmn.indexOf(col2[0].trim()) != -1) {
                    visible = false;
                }

                if (groupBy != null) {

                    if (groupBy.indexOf(col2[0]) != -1) {
                        visible = false;
                    }

                }
                if (cmmnRpt_Type == "1PRJC") {
                    groupBy = [];


                }

                if (col2.length > 1) {

                    $(this).text(col2[0].trim());
                    if (col2[1].toUpperCase().trim() == "R") {

                        columns2.push({
                            title: col2[0].trim(),
                            field: col2[0].trim(),
                            autoResize: true,
                            align: "right",
                            accessorDownload: colMoneyFormatter,
                            formatter: "money",
                            bottomCalc: "sum",
                            bottomCalcParams: {
                                precision: 2
                            },
                            bottomCalcFormatter: "money",
                            bottomCalcFormatterParams: {
                                decimal: ".",
                                thousand: ","
                                //,symbol: ""
                            },
                            formatterParams: {
                                decimal: ".",
                                thousand: ","
                                //,symbol: ""
                            },
                            cssClass: "rightAlgn",
                            visible: visible,

                            //minWidth: 50
                        }); //put elements into array
                    }
                    else {
                        columns2.push({
                            title: col2[0].trim(),
                            field: col2[0].trim(),
                            formatter: "plaintext",
                            align: "left",
                            visible: visible,
                            autoResize: true,


                        });
                    }
                }
                else {

                    $(this).text(col2[0].trim());
                    columns2.push({
                        title: col2[0].trim(),
                        field: col2[0].trim(), autoResize: true,
                        formatter: "plaintext",
                        align: "left",
                        visible: visible,


                    });

                }




                var k2 = $(this).text().trim();

                keys2.push(k2);

            });




            $("#zero_config2 tbody tr").each(function (i, el) {
               
                var row = {}

                $.each(keys2, function (k2, v) {

                    row[v] = $("td:eq(" + k2 + ")", el).text().trim();

                });

                tabledata2.push(row);
                console.log("tabledata2", tabledata2);
            });






           // var footerElementString = "<div>Profit: " + tabledata2.profit + "</div><div>Profit Percentage: " + tabledata2.profitPercentage + "</div>";
            table2 = new Tabulator("#zero_config2", {
                data: tabledata2,
                //renderHorizontal: "virtual",
                layout: "fitColumns",

                columns: columns2,
                groupBy: groupBy,//[groupBy],// ["Department", "Promo Name"],
                groupClosedShowCalcs: [true, true],
                columnCalcs: "both",
                footerElement: ProfitPercentage + Profit,
              
                footerVisible: true,
                groupStartOpen: [true, true],
                groupHeader: (value, count, data, group) => {
                    //value - the value all members of this group share
                    //count - the number of rows in this group
                    //data - an array of all the row data objects in this group
                    //group - the group component for the group
                    var field = group.getField();
                    if (cmmnRpt_Type == "2INSL") {

                        if (rptMode == 1) {

                            switch (field) {
                                case "Branch":
                                    return `<span style='color:#111; margin-left:10px; '>Branch : ${value}</span> <span style='color:#4970cc; margin-left:10px;'></span>`;
                                case "Department":
                                    return `<span style='color:#111; margin-left:10px;'>Department : ${value}</span> <span style='color:#4970cc; margin-left:10px;'></span>`;
                                case "": "All <span style='color:#4970cc; margin-left:10px;'></span>";
                                case Criteria2:

                                    return "<span style='color:#111; margin-left:10px;'>" + Criteria2 + " : " + data[0][Criteria2] + ", Bill Date :" + data[0]["Bill Date"] + ", Customer: " + data[0]["Customer"] + ", BillNo: " + data[0]["Bill No"] + ", Mobile: " + data[0]["Mobile"] + " </span> <span style='color:#4970cc; margin-left:10px;'></span>";
                            }
                        }
                        else if (rptMode == 2) {

                            switch (field) {
                                case "Branch":
                                    return `<span style='color:#111; margin-left:10px; '>Branch : ${value}</span> <span style='color:#4970cc; margin-left:10px;'></span>`
                                case "": "All <span style='color:#4970cc; margin-left:10px;'></span>";
                                case Criteria:
                                    return `<span style='color:#111; margin-left:10px;'>${Criteria}: ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`

                            }

                        }
                        else if (rptMode == 3) {
                            return `<span style='color:#111; margin-left:10px;'>  ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`



                        }
                        else {
                            return `<span style='color:#111; margin-left:10px;'>  ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`



                        }
                    }
                    else if (cmmnRpt_Type == "2PRJC") {

                        return `<span style='color:#111; margin-left:10px;'> ${field} : ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`


                    }
                    else if (cmmnRpt_Type == "13PRJC") {

                        //var groupData = group.getTable().getData().filter((item) => item[field] == value);
                        //var groupSum = groupData.reduce((total, row) => total + row.IncomeAmount, 0)
                        ////var groupSum = data.reduce((total, row) => total + row.IncomeAmount, 0)

                        if (value != null && value != "undefined") {
                            switch (value) {
                                case "Other Expense":
                                    return `<span style='color:#111; margin-left:10px;' >  ${value} </span>`
                                case "Extra Work":
                                    return `<span style='color:#111; margin-left:10px;'>  ${value} </span>`

                            }

                            return `<span style='color:#111; margin-left:10px;' >  ${value} </span>`
                        } else {
                            return "";
                        }
                    }
                    else {

                        if (value != null && field != "" && field != null) {

                            return `<span style='color:#111; margin-left:10px;'> ${field} : ${value} </span> <span style='color:#4970cc; margin-left:10px;'></span>`
                        } else {
                            return `<span style='color:#111; margin-left:10px;'>   </span> <span style='color:#4970cc; margin-left:10px;'></span>`
                        }


                        //return value + "(" + count + ") ";



                    }
                },
                groupHeaderDownload: function (value, count, data, group) {
                    var field = group.getField();

                    if (cmmnRpt_Type == "2INSL") {

                        if (rptMode == 1) {

                            switch (field) {
                                case "Branch":
                                    return ` Branch : ${value} `;
                                case "Department":
                                    return ` Department : ${value} `;
                                case "": "All  ";
                                case Criteria2:
                                    return Criteria2 + " : " + data[0][Criteria2] + ", Bill Date :" + data[0]["Bill Date"] + ", Customer: " + data[0]["Customer"] + ", BillNo: " + data[0]["Bill No"] + ", Mobile: " + data[0]["Mobile"];
                            }
                        }
                        else if (rptMode == 2) {

                            switch (field) {
                                case "Branch":
                                    return ` Branch : ${value}`
                                case "Department":
                                    return ` Department : ${value} `;
                                case "": "All  ";
                                case Criteria:

                                    return Criteria + ` : ${value}  `

                            }

                        }
                    }
                    else {

                        if (value != null && field != "" && field != null) {
                            if (cmmnRpt_Type == "13PRJC") {
                                switch (value) {
                                    case "Other Expense":
                                        return `<span style='color:#111; margin-left:10px;' class='rightgrp'>  ${value} </span>`
                                    case "Extra Work":
                                        return `<span style='color:#111; margin-left:10px;' class='rightgrp'>  ${value} </span>`

                                }
                            } else {
                                return `${field} : ${value} `

                            }
                        }
                        else {
                            if (value != null) {

                                return value + "  (" + count + ") ";
                            }
                        }

                    }

                },

                downloadConfig: {

                    columnHeaders: true, //do not include column headers in downloaded table
                    columnGroups: true, //do not include column groups in column headers for downloaded table
                    rowGroups: true, //do not include row groups in downloaded table
                    columnCalcs: true, //do not include column calcs in downloaded table
                    dataTree: false, //do not include data tree in downloaded table
                   
                },

                groupToggleElement: ["arrow", "arrow"],
               
                columnFormatter: function (row) {
                    row.getElement().style.color = "red";
                }
            });

            if (groupBy == null || groupBy.length <= 0) {
                table2.setGroupBy(false);//To remove group by section
            }


        }

    }
    $("#customers").show();
    $("#divReportSection").show();
    function customizePDF(doc) {
        // Set the font size
        doc.setFontSize(8); // Change this value to your desired font size

        // You can customize other styles or content here if needed

        // Example: Set font style to bold
        // doc.setFontStyle("bold");
    }

    $("#download-pdf").click(function () {
        debugger
        var Reportdiff = $("#ID_Report").val();        
        var title = $("#ID_Report option:selected").text();
        var cmmnRpt_Type = $("#cmmnRpt_Type").val().trim();
        console.log("Rpt_Type>>>>>>>>>", cmmnRpt_Type);
        var newHeaderNames = [];
        var columns = table.getColumnDefinitions();

        console.log(columns, "columns");
        // Update the column names with the new ones
        for (var i = 0; i < columns.length; i++) {
            columns[i].title = columns[i].title.replace(/#/g, ".");
        }
        //table.setColumns(columns);
        if (cmmnRpt_Type == "13PRJC") {

            var HTML_Width = $(".tables-container").width();
            var HTML_Height = $(".tables-container").height();
            var top_left_margin = 15;
            var PDF_Width = HTML_Width + (top_left_margin * 2);
            var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
            var canvas_image_width = HTML_Width;
            var canvas_image_height = HTML_Height;

            var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;

            html2canvas($(".tables-container")[0]).then(function (canvas) {
                var imgData = canvas.toDataURL("image/jpeg", 1.0);
                var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);

                var heads = CompanyName;// title;
                var u_line = heads.replace(/./g, '_');
                pdf.setLineWidth(2);
                pdf.setTextColor(9, 149, 204);
                pdf.setFontSize(22);
                pdf.text(heads, HTML_Width / 2, 25, 'center');
                pdf.text(u_line, HTML_Width / 2, 27, 'center');
                pdf.setFontSize(19);

                var Projectname = "Project        : " + $("#ProjectName").val();
                var ProjectAsonDate = "As On Date : " + changeDateFormat($("#AsonDate").val());
                pdf.setTextColor("black");
                pdf.text(title, 40, 45);
                pdf.setFontSize(16);
                pdf.setTextColor("black");
                //doc.setFontSize(13);
                pdf.text(Projectname, 40, 70);
                pdf.text(ProjectAsonDate, 40, 95);

                pdf.addImage(imgData, 'JPG', top_left_margin, 125, canvas_image_width, canvas_image_height);
                for (var i = 1; i <= totalPDFPages; i++) {
                    pdf.addPage(PDF_Width, HTML_Height + 600);
                    pdf.addImage(imgData, 'JPG', top_left_margin, 40, canvas_image_width, canvas_image_height);
                }


                const pageHeight = pdf.internal.pageSize.height;

                const pageCount = pdf.internal.getNumberOfPages();
                for (let i = 1; i <= pageCount; i++) {
                    pdf.setPage(i);
                    pdf.setFontSize(13);
                    pdf.setTextColor(150);
                    var date = moment();
                    var currentDate = date.format('D/MM/YYYY hh:mm a');
                    pdf.text(pdf.internal.pageSize.getWidth() - 100, pdf.internal.pageSize.getHeight() - 30, `Page ${i} of ${pageCount}`);
                    pdf.text(RPTusername + ' Printed on ' + currentDate, 40, pdf.internal.pageSize.getHeight() - 30);

                }




                pdf.save(title + ".pdf");

            });

        }
      
        else {
            debugger
            var imgWidth =0;// parseInt('@ViewBag.iWidth'); 
            var imgHeight = 0; //parseInt('@ViewBag.iHeight');
         
         
               

            var firstpage = true;
          
          

            var _fontSize = 8;
          

            var pdfdn = new jsPDF({
                format: $("#rptPdfPageSetup").val(), // Set the paper size to A3
                fontSize: _fontSize,
            });

            var columnStyles = []; var clmcount = parseFloat($('.tabulator-col').length);  
            if (cmmnRpt_Type == "1LFLMRPT") {

                columnStyles = {
                    0: { cellWidth: 30 },
                    1: { cellWidth: 40 },
                    2: { cellWidth: 50 },
                    3: { cellWidth: 50 },
                    4: { cellWidth: 50 },
                    5: { cellWidth: 40 },
                    6: { cellWidth: 50 },
                    7: { cellWidth: 40 },
                    8: { cellWidth: 50 },
                    9: { cellWidth: 40 },
                   10: { cellWidth: 30 },
                   11: { cellWidth: 40 },
                   12: { cellWidth: 45 },
                   13: { cellWidth: 30 },
                   14: { cellWidth: 45 },
                   15: { cellWidth: 45 },
                   16: { cellWidth: 40 },
                   17: { cellWidth: 40 },
                   18: { cellWidth: 40 }
                }
            }
            else if (cmmnRpt_Type == "2LFLMRPT") {

                columnStyles = {
                    0: { cellWidth: 30 },
                    1: { cellWidth: 50 },
                    2: { cellWidth: 50 },
                    3: { cellWidth: 50 },
                    4: { cellWidth: 50 },
                    5: { cellWidth: 37 },
                    6: { cellWidth: 50 },
                    7: { cellWidth: 40 },
                    8: { cellWidth: 45 },
                    9: { cellWidth: 51 },
                   10: { cellWidth: 30 },
                   11: { cellWidth: 38 },
                   12: { cellWidth: 37 },
                   13: { cellWidth: 37 },
                   14: { cellWidth: 45 },
                   15: { cellWidth: 45 },
                   16: { cellWidth: 44 },
                   17: { cellWidth: 30 },
                   18: { cellWidth: 40 }
                }
            }
            else if (cmmnRpt_Type == "4LFLMRPT") {
                columnStyles = {
                    0: { cellWidth: 30 },
                    1: { cellWidth: 50 },
                    2: { cellWidth: 50 },
                    3: { cellWidth: 50 },
                    4: { cellWidth: 50 },
                    5: { cellWidth: 45 },
                    6: { cellWidth: 50 },
                    7: { cellWidth: 40 },
                    8: { cellWidth: 45 },
                    9: { cellWidth: 33 },
                   10: { cellWidth: 52 },
                   11: { cellWidth: 35 },
                   12: { cellWidth: 40 },
                   13: { cellWidth: 37 },
                   14: { cellWidth: 45 },
                   15: { cellWidth: 45 },
                   16: { cellWidth: 48 },
                   17: { cellWidth: 45 },

                }

            } else if (cmmnRpt_Type == "5LFLMRPT") {
                columnStyles = {
                    0: { cellWidth: 30 },
                    1: { cellWidth: 65 },
                    2: { cellWidth: 70 },
                    3: { cellWidth: 70 },
                    4: { cellWidth: 80 },
                    5: { cellWidth: 100 },
                    6: { cellWidth: 70 },
                    7: { cellWidth: 90 },
                    8: { cellWidth: 50 },
                    9: { cellWidth: 70 },
                   10: { cellWidth: 50 },
                   11: { cellWidth: 50 },
                   //12: { cellWidth: 40 },
                   //13: { cellWidth: 45 },
                   //14: { cellWidth: 45 },
                   //15: { cellWidth: 45 },
                   //16: { cellWidth: 40 },
                   //17: { cellWidth: 45 },

                }

            } else if (cmmnRpt_Type == "8LFLMRPT") {
                columnStyles = {
                    0: { cellWidth: 100 },
                    1: { cellWidth: 50  },
                    2: { cellWidth: 50  },
                    3: { cellWidth: 100 },
                    4: { cellWidth: 100 },
                    5: { cellWidth: 45  },
                    6: { cellWidth: 50  },
                    7: { cellWidth: 50  },
                    8: { cellWidth: 100 },
                    9: { cellWidth: 50  },
                   10: { cellWidth: 50  },
                   11: { cellWidth: 50  },


                }

            }
            else if (cmmnRpt_Type == "2INSL" && Reportdiff == 1) {
                columnStyles = {
                    0: { cellWidth: 30 },
                    1: { cellWidth: 60 },
                    2: { cellWidth: 40 },
                    3: { cellWidth: 45 },
                    4: { cellWidth: 45 },
                    5: { cellWidth: 55 },
                    6: { cellWidth: 45 },
                    7: { cellWidth: 50 },
                    8: { cellWidth: 50 },
                    9: { cellWidth: 60 },
                   10: { cellWidth: 50 },
                   11: { cellWidth: 50 },
                   12: { cellWidth: 50 },
                   13: { cellWidth: 60 },
                   14: { cellWidth: 60 },
                }
            }
            else if (cmmnRpt_Type == "2INSL" && Reportdiff == 2) {
                columnStyles = {
                    0: { cellWidth: 30 },
                    1: { cellWidth: 70 },
                    2: { cellWidth: 55 },
                    3: { cellWidth: 70 },
                    4: { cellWidth: 60 },
                    5: { cellWidth: 80 },
                    6: { cellWidth: 75 },
                    7: { cellWidth: 70 },
                    8: { cellWidth: 70 },
                    9: { cellWidth: 80 },
                   10: { cellWidth: 65 },
                   11: { cellWidth: 85 },

                }
            }
            else if (cmmnRpt_Type == "2INSL" && Reportdiff == 3) {
                columnStyles = {
                    0: { cellWidth: 40  },
                    1: { cellWidth: 90  },
                    2: { cellWidth: 90  },
                    3: { cellWidth: 120 },
                    4: { cellWidth: 110 },
                    5: { cellWidth: 90  },
                    6: { cellWidth: 90  },
                    7: { cellWidth: 80  },
                    
                }
            }
                           
            else if (cmmnRpt_Type == "17INSL") {
                columnStyles = {
                    0: { cellWidth: 30  },
                    1: { cellWidth: 45  },
                    2: { cellWidth: 100 },
                    3: { cellWidth: 50  },
                    4: { cellWidth: 60  },
                    5: { cellWidth: 70  },
                    6: { cellWidth: 75  },
                    7: { cellWidth: 50  },
                    8: { cellWidth: 60  },
                    9: { cellWidth: 75  },
                   10: { cellWidth: 50  },
                   11: { cellWidth: 50  },
                   12: { cellWidth: 50  }
                }
            }
            else if (cmmnRpt_Type == "18INSL") {
                columnStyles = {
                    0: { cellWidth: 50  },
                    1: { cellWidth: 55  },
                    2: { cellWidth: 55  },
                    3: { cellWidth: 60  },
                    4: { cellWidth: 60  },
                    5: { cellWidth: 60  },
                    6: { cellWidth: 50  },
                    7: { cellWidth: 35  },
                    8: { cellWidth: 35  },
                    9: { cellWidth: 35  },
                   10: { cellWidth: 37  },
                   11: { cellWidth: 35  },
                   12: { cellWidth: 35  },
                   13: { cellWidth: 35  },
                   14: { cellWidth: 36  },
                   15: { cellWidth: 37  },
                   16: { cellWidth: 35  }
                }
            }
            else if (cmmnRpt_Type == "3INSLHO") {
                columnStyles = {
                    0: { cellWidth: 50  },
                    1: { cellWidth: 60  },
                    2: { cellWidth: 60  },
                    3: { cellWidth: 55  },
                    4: { cellWidth: 60  },
                    5: { cellWidth: 60  },
                    6: { cellWidth: 60  },
                    7: { cellWidth: 60  },
                    8: { cellWidth: 65  },
                    9: { cellWidth: 65  },
                   10: { cellWidth: 45  },
                   11: { cellWidth: 45  },
                   12: { cellWidth: 45  },
                   13: { cellWidth: 65  },
                   
            
               
                }
            }
            else if (cmmnRpt_Type == "11PRJC") {
                columnStyles = {
                    0: { cellWidth: 35  },
                    1: { cellWidth: 70  },
                    2: { cellWidth: 60  },
                    3: { cellWidth: 70  },
                    4: { cellWidth: 70  },
                    5: { cellWidth: 70  },
                    6: { cellWidth: 70  },
                    7: { cellWidth: 70  },
                    8: { cellWidth: 120 },
                    9: { cellWidth: 100 },
                   10: { cellWidth: 70  },
                   

                }
            }
           
            if ($("#rptPdfPageSetup").val() == "a3") {


                table.download("pdf", title + ".pdf", {
                    document: pdfdn,

                    autoTable: {
                        startY: 120,
                        margin: { left: 20, right: 20 },
                        styles: {
                            fontSize: _fontSize,  // Set your desired default font size for the table content
                            // You can add other styles here, such as font, textColor, etc.

                        },

                        columnStyles: columnStyles, // Adjust column widths if needed
                        // ... other autoTable configurations
                        didDrawCell: function (data) {
                            // Set font size for individual cells if needed
                            data.doc.setFontSize(8);  // Adjust the font size as necessary
                        },

                        didDrawPage: function (data) {
                            // Add any additional styling for the entire page if needed
                            data.doc.setFontSize(_fontSize);  // Adjust the font size for the entire page                        

                            // You can add other styles here, such as margins, background color, etc.
                            if (firstpage) {
                                var doc = data.doc;
                                var pageWidth = doc.internal.pageSize.width || doc.internal.pageSize.getWidth();
                                var heads = CompanyName;// title;
                                var u_line = heads.replace(/./g, '_');
                                doc.setLineWidth(2);                               
                            
                                doc.autoTable({
                                    startY: imgHeight + 100,
                                   
                                    columnStyles: { 0: { cellWidth: 30 } }, // Adjust column widths if needed

                                }),

                                    doc.setTextColor(9, 149, 204);
                                //doc.setFontSize(15);
                                if (heads.length > 93)
                                    doc.setFontSize(9);
                                else
                                    doc.setFontSize(15);
                                doc.text(heads, pageWidth / 2, imgHeight + 35, 'center');
                                doc.text(u_line, pageWidth / 2, imgHeight + 45, 'center');
                                doc.setFontSize(13);
                                doc.text(title, 40, imgHeight + 70);
                                var filters = ""; var selected = ""; var fieldname = ""; var type = ""; var selectedval = "";
                                var r = 0;
                                $(".rptfilters").each(function () {

                                    var showorhide = $(this).closest('div').is(':visible');

                                    if (showorhide) {
                                        type = $(this).attr('perfect-ctype');
                                        fieldname = $(this).parent().siblings(".rptfilterfields").find('span').text().replace('*', '');

                                        if (fieldname == "") {
                                            fieldname = $(this).parent().parent().siblings(".rptfilterfields").find('span').text().replace('*', '');
                                        }
                                        if (fieldname != "") {
                                            debugger;
                                            if (type != null) {

                                                if (type == "select") {

                                                    selected = $(this).find('option:selected').text();
                                                    selectedval = $(this).val();

                                                    if ((selected == "Detailed" || selected == "Non Detailed") && $('.DivMode').is(':visible')) {

                                                    }
                                                    if (selectedval != "" && selectedval != "0" && selectedval != " " && selectedval != null) {
                                                        if (selected.toLowerCase().trim() != "please select") {
                                                            if (filters == "") {

                                                                filters = fieldname + ": " + selected + "\t";
                                                            }
                                                            else {
                                                                filters = filters + "" + fieldname + ": " + selected + "\t";
                                                            }
                                                        }

                                                        if ($("#rptPdfPageSetup").val() != 'a3') {
                                                            if (r == 3) {

                                                                filters = filters + "\n";
                                                                r = 0;
                                                            }
                                                        } else {
                                                            if (r == 5) {

                                                                filters = filters + "\n";
                                                                r = 0;
                                                            }
                                                        }
                                                        r++;
                                                    }
                                                }
                                                else if (type == "date" || type == "text" || type == "input") {
                                                    if (type == "date") {
                                                        selected = changeDateFormat($(this).val());
                                                    }
                                                    else {
                                                        selected = $(this).val();
                                                    }

                                                    if (selected.trim() != "") {
                                                        if (filters == "") {

                                                            filters = fieldname + ": " + selected + "\t";
                                                        }
                                                        else {
                                                            filters = filters + "" + fieldname + ": " + selected + "\t";
                                                        }
                                                    }

                                                    if ($("#rptPdfPageSetup").val() != 'a3') {
                                                        if (r == 3) {
                                                            filters = filters + "\n";
                                                            r = 0;
                                                        }
                                                    } else {

                                                        if (r == 5) {
                                                            filters = filters + "\n";
                                                            r = 0;
                                                        }
                                                    } r++;
                                                }



                                            }
                                        }
                                    }


                                });
                                doc.setTextColor("black");
                                doc.setFontSize(_fontSize);
                                doc.text(filters, 40, imgHeight + 90);
                                firstpage = false;
                            }


                        },

                    },
                    documentProcessing: function (doc) {
                        var totalPages = doc.getNumberOfPages();
                        for (i = 1; i <= totalPages; i++) {
                            doc.setPage(i);
                            doc.setFontSize(6);
                            doc.setTextColor(150);
                            doc.text('Page ' + i + ' of ' + totalPages, doc.internal.pageSize.getWidth() - 100, doc.internal.pageSize.getHeight() - 30);
                            var date = moment();
                            var currentDate = date.format('D/MM/YYYY hh:mm a');
                            doc.text(RPTusername + ' Printed on ' + currentDate, 40, doc.internal.pageSize.getHeight() - 30);

                        }

                    },
                    // ... other downloadToPDF options
                });


            }
            else {
                table.download("pdf", title + ".pdf", {
                    orientation: $("#rptPdfPageSetup").val(),

                    autoTable: {
                        startY: 120,
                        margin: { left: 20, right: 20 },
                        styles: {
                            fontSize: _fontSize,  // Set your desired default font size for the table content
                            // You can add other styles here, such as font, textColor, etc.

                        },

                        columnStyles: columnStyles, // Adjust column widths if needed
                        // ... other autoTable configurations
                        didDrawCell: function (data) {
                            // Set font size for individual cells if needed
                            data.doc.setFontSize(8);  // Adjust the font size as necessary

                        },
                        didDrawPage: function (data) {
                            // Add any additional styling for the entire page if needed
                            data.doc.setFontSize(_fontSize);  // Adjust the font size for the entire page


                           
                            // You can add other styles here, such as margins, background color, etc.
                            if (firstpage) {
                                var doc = data.doc;
                                var pageWidth = doc.internal.pageSize.width || doc.internal.pageSize.getWidth();
                                var heads = CompanyName;// title;
                                var u_line = heads.replace(/./g, '_');
                                doc.setLineWidth(2);
                               
                                doc.autoTable({
                                    startY: imgHeight + 100,
                                    //styles: styles,
                                    // Set the font size here
                                    columnStyles: { 0: { cellWidth: 30 } }, // Adjust column widths if needed

                                }),

                                    doc.setTextColor(9, 149, 204);
                                if(heads.length> 93)
                                    doc.setFontSize(9);
                                else
                                    doc.setFontSize(15);

                                doc.text(heads, pageWidth / 2, imgHeight + 35, 'center');
                                doc.text(u_line, pageWidth / 2, imgHeight + 45, 'center');
                                doc.setFontSize(13);
                                doc.text(title, 40, imgHeight + 70);
                                var filters = ""; var selected = ""; var fieldname = ""; var type = ""; var selectedval = "";
                                var r = 0;
                                $(".rptfilters").each(function () {
                                    var showorhide = $(this).closest('div').is(':visible');
                                  
                                    if (showorhide) {
                                       
                                        type = $(this).attr('perfect-ctype');
                                        fieldname = $(this).parent().siblings(".rptfilterfields").find('span').text().replace('*', '');

                                        if (fieldname == "") {
                                            fieldname = $(this).parent().parent().siblings(".rptfilterfields").find('span').text().replace('*', '');
                                        }
                                        if (fieldname != "") {
                                            debugger;
                                            if (type != null) {

                                                if (type == "select") {

                                                    selected = $(this).find('option:selected').text();
                                                    selectedval = $(this).val();

                                                    if ((selected == "Detailed" || selected == "Non Detailed") && $('.DivMode').is(':visible')) {

                                                    }
                                                    if (selectedval != "" && selectedval != "0" && selectedval != " " && selectedval != null) {
                                                        if (selected.toLowerCase().trim() != "please select") {
                                                            if (filters == "") {

                                                                filters = fieldname + ": " + selected + "\t";
                                                            }
                                                            else {
                                                                filters = filters + "" + fieldname + ": " + selected + "\t";
                                                            }
                                                        }

                                                        if ($("#rptPdfPageSetup").val() != 'a3') {
                                                            if (r == 3) {

                                                                filters = filters + "\n";
                                                                r = 0;
                                                            }
                                                        } else {
                                                            if (r == 5) {

                                                                filters = filters + "\n";
                                                                r = 0;
                                                            }
                                                        }
                                                        r++;
                                                    }
                                                }
                                                else if (type == "date" || type == "text" || type == "input") {
                                                    if (type == "date") {
                                                        selected = changeDateFormat($(this).val());
                                                    }
                                                    else {
                                                        selected = $(this).val();
                                                    }

                                                    if (selected.trim() != "") {
                                                        if (filters == "") {

                                                            filters = fieldname + ": " + selected + "\t";
                                                        }
                                                        else {
                                                            filters = filters + "" + fieldname + ": " + selected + "\t";
                                                        }
                                                    }

                                                    if ($("#rptPdfPageSetup").val() != 'a3') {
                                                        if (r == 3) {
                                                            filters = filters + "\n";
                                                            r = 0;
                                                        }
                                                    } else {

                                                        if (r == 5) {
                                                            filters = filters + "\n";
                                                            r = 0;
                                                        }
                                                    } r++;
                                                }



                                            }
                                        }

                                    }

                                });
                                doc.setTextColor("black");
                                doc.setFontSize(_fontSize);
                                doc.text(filters, 40, imgHeight + 90);
                                firstpage = false;
                            }


                        },

                    },
                    documentProcessing: function (doc) {

                        var totalPages = doc.getNumberOfPages();
                        for (i = 1; i <= totalPages; i++) {
                            doc.setPage(i);
                            doc.setFontSize(6);
                            doc.setTextColor(150);
                            doc.text('Page ' + i + ' of ' + totalPages, doc.internal.pageSize.getWidth() - 100, doc.internal.pageSize.getHeight() - 30);
                            var date = moment();
                            var currentDate = date.format('D/MM/YYYY hh:mm a');
                            doc.text(RPTusername + ' Printed on ' + currentDate, 40, doc.internal.pageSize.getHeight() - 30);

                        }

                    },
                    // ... other downloadToPDF options
                });
            }





        



        }
    });

    

});
//$(document).on("click", "#download-xlsx", function () {
    $("#download-xlsx").click(function () {
  
    var title = $("#ID_Report option:selected").text();
    var cmmnRpt_Type = $("#cmmnRpt_Type").val().trim();
    if (cmmnRpt_Type == "13PRJC") {

        var table1Data = table.getData();
        var table2Data = table2.getData();

        var table1Columns = table.getColumns();
        var table2Columns = table2.getColumns();

        // Extract visible columns and headers from both tables
        var visibleTable1Columns = table1Columns.filter(col => col.getDefinition().visible !== false);
        var visibleTable2Columns = table2Columns.filter(col => col.getDefinition().visible !== false);

        var headers = [
            visibleTable1Columns.map(col => col.getDefinition().title),
            visibleTable2Columns.map(col => col.getDefinition().title),
        ];

        // Combine headers side by side
        var combinedHeaders = headers[0].concat(headers[1]);

        // Extract data arrays for visible columns from both tables
        var table1Array = table1Data.map(row => visibleTable1Columns.map(col => row[col.getField()]));
        var table2Array = table2Data.map(row => visibleTable2Columns.map(col => row[col.getField()]));

        var tbl1length = parseFloat(table1Array.length);
        var tbl2length = parseFloat(table2Array.length);
        var newlngth = tbl2length - tbl1length;


        if (newlngth > 0) {
            for (var m = 0; m < newlngth; m++) {
                table1Array.push("");
            }
        } else {
            newlngth = tbl1length - tbl2length;

            for (var m = 0; m < newlngth; m++) {
                table2Array.push("");
            }

        }

        if (prftflag) {
            table1Array.push("          ");
            table2Array.push("Profit : " + prftamnt);
        }

        if (lossflag) {
            table1Array.push("Loss : " + lossamnt);
            table2Array.push("          ");

        }
        // Combine the data arrays side by side
        var combinedData = [];
        var maxRows = Math.max(table1Array.length, table2Array.length);
        for (let rowIndex = 0; rowIndex < maxRows; rowIndex++) {
            var combinedRow = [];
            combinedRow = combinedRow.concat(table1Array[rowIndex] || []);
            combinedRow = combinedRow.concat(table2Array[rowIndex] || []);
            combinedData.push(combinedRow);
        }


        // Combine data and footer
        //combinedData.push(footerContent);
        // Create an Excel workbook and worksheet
        var wb = XLSX.utils.book_new();
        var ws = XLSX.utils.aoa_to_sheet([combinedHeaders].concat(combinedData));

        // Add the worksheet to the workbook
        XLSX.utils.book_append_sheet(wb, ws, title);

        // Export the workbook to Excel
        XLSX.writeFile(wb, title + '.xlsx');




    }
    else {

        //var title = $("#ID_Report option:selected").text().replace(' ', '').trim();
        var title = $("#ID_Report option:selected").text();

        var sheetname = title;
        if (title.length > 31) {
            sheetname = title.substring(0, 28) + "..";
        }
        table.download("xlsx", title + ".xlsx", {
            sheetName: sheetname,
        });

    }
});
$("#expxlsx").click(function () {


    var title = $("#ID_Report option:selected").text();
    var cmmnRpt_Type = $("#cmmnRpt_Type").val().trim();
    if (cmmnRpt_Type == "13PRJC") {

        var table1Data = table.getData();
        var table2Data = table2.getData();

        var table1Columns = table.getColumns();
        var table2Columns = table2.getColumns();

        // Extract visible columns and headers from both tables
        var visibleTable1Columns = table1Columns.filter(col => col.getDefinition().visible !== false);
        var visibleTable2Columns = table2Columns.filter(col => col.getDefinition().visible !== false);

        var headers = [
            visibleTable1Columns.map(col => col.getDefinition().title),
            visibleTable2Columns.map(col => col.getDefinition().title),
        ];

        // Combine headers side by side
        var combinedHeaders = headers[0].concat(headers[1]);

        // Extract data arrays for visible columns from both tables
        var table1Array = table1Data.map(row => visibleTable1Columns.map(col => row[col.getField()]));
        var table2Array = table2Data.map(row => visibleTable2Columns.map(col => row[col.getField()]));

        var tbl1length = parseFloat(table1Array.length);
        var tbl2length = parseFloat(table2Array.length);
        var newlngth = tbl2length - tbl1length;


        if (newlngth > 0) {
            for (var m = 0; m < newlngth; m++) {
                table1Array.push("");
            }
        } else {
            newlngth = tbl1length - tbl2length;

            for (var m = 0; m < newlngth; m++) {
                table2Array.push("");
            }

        }

        if (prftflag) {
            table1Array.push("          ");
            table2Array.push("Profit : " + prftamnt);
        }

        if (lossflag) {
            table1Array.push("Loss : " + lossamnt);
            table2Array.push("          ");

        }
        // Combine the data arrays side by side
        var combinedData = [];
        var maxRows = Math.max(table1Array.length, table2Array.length);
        for (let rowIndex = 0; rowIndex < maxRows; rowIndex++) {
            var combinedRow = [];
            combinedRow = combinedRow.concat(table1Array[rowIndex] || []);
            combinedRow = combinedRow.concat(table2Array[rowIndex] || []);
            combinedData.push(combinedRow);
        }


        // Combine data and footer
        //combinedData.push(footerContent);
        // Create an Excel workbook and worksheet
        var wb = XLSX.utils.book_new();
        var ws = XLSX.utils.aoa_to_sheet([combinedHeaders].concat(combinedData));

        // Add the worksheet to the workbook
        XLSX.utils.book_append_sheet(wb, ws, title);

        // Export the workbook to Excel
        XLSX.writeFile(wb, title + '.xlsx');




    }
    else {

        var title = $("#ID_Report option:selected").text().replace(' ', '').trim();
        var sheetname = title;
        if (title.length > 31) {
            sheetname = title.substring(0, 28) + "..";
        }
        table.download("xlsx", title + ".xlsx", {
            sheetName: sheetname,
        });

    }
});
function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

var formatter = new Intl.NumberFormat('en-US', {
    minimumFractionDigits: 2,
    align: "rigth",
    textAlign: "rigth",
    cssClass: "rightAlgn"
})
var colMoneyFormatter = function (value, data, type, params, column) {

    return formatter.format(value);
}
function changeDateFormat(date) {
    /*var date = '2023-06-05';*/

    // Split the date into year, month, and day components
    var dateComponents = date.split('-');

    // Rearrange the components to form the desired format
    var formattedDate = dateComponents[2] + '/' + dateComponents[1] + '/' + dateComponents[0];

    console.log(formattedDate); // Output: 05/06/23
    return formattedDate;
}
