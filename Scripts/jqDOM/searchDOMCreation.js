



function createSearchModalDOM() {
    let $modaltitle = $('<h5/>', { class: "modal-title" });
    let $modalHead = $('<div/>', { class: "modal-header border-0" }).append($modaltitle, $('<button type="button" class="close" data-dismiss="modal"><span>&times;</span></button>'));
    let $modalBody = $('<div/>', { class: "modal-body" });
    let $modalFooter = $('<div/>', { class: "modal-footer border-0" });
    let $modalContent = $('<div/>', { class: "modal-content rounded-0" });
    let $modalDialog = $('<div/>', { class: "modal-dialog modal-dialog-centered", role: "document" });
    let $modal = $('<div/>', { class: "modal fade", tabindex: "-1", role: "dialog", "aria-labelledby": "", "aria-hidden": "true" })

    //remove the model when modal is closed
    // $modal.modal();
    $modal.on('hidden.bs.modal', function () {
        $(this).remove();
    });

    return {
        modal: $modal.html($modalDialog.html($modalContent.append($modalHead, $modalBody, $modalFooter))),
        show: $modal.modal('show'),
        close: $modal.modal('hide')

    };
}



function createSearchModalDOM2() {
    let $modaltitle = $('<h5/>', { class: "modal-title" });
    let $modalHead = $('<div/>', { class: "modal-header border-0" });
    let $modalBody = $('<div/>', { class: "modal-body" });
    let $modalFooter = $('<div/>', { class: "modal-footer border-0" });
    let $modalContent = $('<div/>', { class: "modal-content " });
    let $modalDialog = $('<div/>', { class: "modal-dialog modal-dialog-centered" });
    let $modal = $('<div/>', { class: "modal fade", tabindex: "-1", role: "dialog", "aria-labelledby": "", "aria-hidden": "true", "style":"z-index: 999999;" })

    //remove the model when modal is closed
    // $modal.modal();
    $modal.on('hidden.bs.modal', function () {
        $(this).show();
    });

    return {
        modal: $modal.html($modalDialog.html($modalContent.append($modalHead, $modalBody, $modalFooter))),
        disable: $modal.modal({ backdrop: 'static', keyboard: false }),
        show: $modal.modal('show'),
        close: $modal.modal('show')

    };
}

//--------------------------------------------------------
function createFilter(inputData) {
    $row = $('<div/>', { class: "row jq_allFilter" });

    $.each(inputData, function (returnkey, returnValue) {

        console.log('returnkey : ' + returnkey + '| returnValue : ' + returnValue);

        if (returnValue.FilterType == "dropdown") {
            $row.append(createDropdown(returnValue.FilterData))
        }
        else if (returnValue.FilterType == "input") {
            $row.append(createInput(returnValue.FilterData))
        }
        else if (returnValue.FilterType == "date") {
            $row.append(createDate(returnValue.FilterData))
        }
        else if (returnValue.FilterType == "checkbox") {
            $row.append(createCheckbox(returnValue.FilterData));
        }
        else if (returnValue.FilterType == "radio") {
            $row.append(createRadio(returnValue.FilterData));
        }
        else if (returnValue.FilterType == "check") {
            $row.append(createRadio(returnValue.FilterData));
        }
    });
    return $row;
}



function createDropdown(inputData) {
    //, multiple=((inputData.MultipleSelect) ? "true":"false")
    let $select = $('<select/>', { class: "form-control form-control-sm jq_valueNode", name: inputData.FilterName })
        .prop('multiple', ((inputData.MultipleSelect == true) ? "multiple" : ""))
        .data({ 'searchKeyType': 'dropdown' });
    //let $select = $('<select class= "form-control jq_valueNode" name=' + inputData.FilterName +' multiple/>').data({ 'searchKeyType': 'dropdown' });
    $.each(inputData.SelectOptions, function (returnkey, returnValue) {
        $select.append(
            $('<option/>', { value: returnValue.FilterJSONKey })
                .text(returnValue.FilterJSONvalue)
                .data({ "name1": returnkey, "value1": returnValue })
        );
    });


    return $('<div/>', { class: "col-md-4 col-sm-12" }).append(
        $('<div/>', { class: "form-group" })
            .append($('<label/>', { class: "mb-0" }).text(inputData.FilterLabel))
            .append($select)
    );

}

function createInput(inputData) {

    $input = $('<input/>', { type: "text", class: "form-control form-control-sm jq_valueNode", placeholder: inputData.Placeholder, name: inputData.FilterName }).val(inputData.FilterValue).data({ 'searchKeyType': 'input' })



    return $('<div/>', { class: "col-md-4 col-sm-12" }).append(
        $('<div/>', { class: "form-group" })
            .append($('<label/>', { class: "mb-0" }).text(inputData.FilterLabel))
            .append($input)
    );
}

function createRadio(inputData) {
    let $radio = $('<div/>', { class: "col-md-12" })


    $.each(inputData.SelectOptions, function (returnkey, returnValue) {
        $radio.append(

            $('<label/>', { class: "radio-inline mr-3" })
                .append($('<input/>', { type: "radio", class: "jq_valueNode mr-2", name: inputData.FilterName }).val(returnValue.FilterJSONKey).data({ 'searchKeyType': 'radio' }), $('<span/>', { class: "font-weight-normal" }).text(returnValue.FilterJSONvalue))
        );
    });


    return $('<div/>', { class: "col-md-12 col-sm-12  pt-2" }).append(
        $('<div/>', { class: "form-group" })
            .append($('<label/>', { class: "mb-0" }).text(inputData.FilterLabel))
            .append($radio)
    );
}
function createCheckbox(inputData) {

    let $checkbox = $('<div/>', { class: "col-md-12" })


    $.each(inputData.SelectOptions, function (returnkey, returnValue) {
        $checkbox.append(
            $('<div/>', { class: "form-check form-check-inline" }).html(
                $('<label/>', { class: "form-check-label" })
                    .append($('<input/>', { type: "checkbox", class: "form-check-input jq_valueNode", name: inputData.FilterName }).val(returnValue.FilterJSONKey).data({ 'searchKeyType': 'checkbox' }), $('<span/>', { class: "font-weight-normal" }).text(returnValue.FilterJSONvalue)))
        );
    });


    return $('<div/>', { class: "col-md-12 col-sm-12  pt-2 " }).append(
        $('<div/>', { class: "form-group" })
            .append($('<label/>', { class: "mb-0" }).text(inputData.FilterLabel))
            .append($checkbox)
    );
}

function createDate(inputData) {

    let $input = $('<input/>', { class: "form-control  jq_valueNode", name: inputData.FilterName })

    if (inputData.DateRangePicker) {

        $input.addClass('input-daterange-datepicker').attr('type', "text").data({ 'searchKeyType': 'dateRange' }).daterangepicker();
    }
    else {
        $input.attr('type', "date").data({ 'searchKeyType': 'date' })
    }

    //let $input = $('<input/>', { class: "form-control input-daterange-datepicker jq_valueNode", type: "text", name: inputData.FilterName }).data({ 'searchKeyType': 'dateRange' }).daterangepicker();

    //let $input = $('<input/>', { type: "date", class: "form-control jq_valueNode", placeholder: inputData.Placeholder, name: inputData.FilterName }).val(inputData.FilterValue).data({ 'searchKeyType': 'date' })



    return $('<div/>', { class: "col-md-4 col-sm-12" }).append(
        $('<div/>', { class: "form-group" })
            .append($('<label/>', { class: "mb-0" }).text(inputData.FilterLabel))
            .append($input)
    );
}


function createcheck(inputData) {
    //<label class="css-control css-control-primary css-checkbox" for="val-terms">
    //    <input type="checkbox" class="css-control-input mr-2" id="val-terms" name="val-terms" value="1">
    //        <span class="css-control-indicator"></span> I agree to the
    //        terms
    //                                    </label>

    let $checkbox = $('<label/>', { class: 'css-control css-control-primary css-checkbox' }).append(
        $('<input/>', { type: 'checkbox', class: 'css-control-input mr-2 jq_valueNode', name: inputData.FilterName }),
        $('<span/>', { class: 'css-control-indicator' }).text(inputData.FilterLabel)

    );

    return $('<div/>', { class: "col-md-4 col-sm-12" }).append(
        $('<div/>', { class: "form-group" })
            .append($('<label/>', { class: "mb-0" }).text())
            .append($checkbox)
    );
}


function createSearchBody(inputData) {
   
    $row = $('<div/>', { class: "row jq_allSearchBody" });
    let options = {
        rowClickAction: function () { console.log($(this).closest('tr').data()) }
    };
    $row.append(pft_table_createtable(inputData, options))
    return $row;
}






function ConfirmDialog(input) {
    var p = new Promise(function (resolve, reject) {

        let hasReason = false;
        let reasonName = "ReasonID";
        let headingText = (input && input.heading) ? input.heading : "Confirm Delete?";
        let bodyText = (input && input.body) ? input.body : "Are you Sure you want to do this?";
        let cancelText = (input && input.cancel) ? input.cancel : "No";
        let confirmText = (input && input.confirm) ? input.confirm : "Yes";
        let headingIcon = (input && input.headingIcon) ? input.headingIcon : "fa fa-warning";
        let headingColor = (input && input.headingColor) ? input.headingColor : "text-primary";


        let $modaltitle = $('<h5/>', { class: "modal-title " + headingColor }).append($('<i>', { class: headingIcon })).append($('<span/>', { class: "ml-2" }).text(headingText));
     //   let $modalHead = $('<div/>', { class: "modal-header border-0" }).append($modaltitle, $('<button type="button" class="close" data-dismiss="modal"><span>&times;</span></button>'));
        let $modalHead = $('<div/>', { class: "modal-header border-0" }).append($modaltitle);
        let $modalBody = $('<div/>', { class: "modal-body pt-1" })
        let $modalFooter = $('<div/>', { class: "modal-footer border-0" });
        let $modalContent = $('<div/>', { class: "modal-content rounded-0" });
        let $modalDialog = $('<div/>', { class: "modal-dialog modal-dialog-centered", role: "document" });
        let $modal = $('<div/>', { class: "modal fade", tabindex: "-1", role: "dialog", "aria-labelledby": "", "aria-hidden": "true", "perfect-class": 'confirmDialogBox' });


        if (input && input.reason) {
            // console.log('SD : reason', input.reason);
            hasReason = true;
            let $select = $('<select/>', { class: "form-control", name: reasonName });
            $select.append($('<option/>', { value: "" }).text("Select a Reason"));
            $.each(input.reason, function (key, value) {
                $select.append($('<option/>', { value: value.ReasonID }).text(value.Reason));
            });

            $modalBody.append($('<div/>', { class: "col-md-12" }).append(
                $('<div/>', { class: "form-group", 'perfect-class': "formGroup" })
                    .append($('<label/>', { class: "mb-0" }).text(""))
                    .append($select)
                    .append($('<div/>', { class: "invalid-feedback animated fadeInUp jq_reason_error d-block" }))
            ));
        }
  


        $modalBody.append($('<div/>', { class: "px-3" }).html(bodyText));

        //remove the model when modal is closed
        // $modal.modal();
        $modal.on('hidden.bs.modal', function () {
            $(this).remove();
            resolve(false);
        });
        let dialog = $('<div/>')
            .append($('<button/>', { class: "btn btn-primary mr-3" }).text(confirmText).click(function () {

                if (hasReason) {

                    let $reasonDropdown = $(this).closest('[perfect-class="confirmDialogBox"]').find('[name="' + reasonName + '"]');
                    if ($reasonDropdown.val()) {
                        console.log('SD: reason dropdown if val() ', $reasonDropdown.val())
                        $reasonDropdown.closest('[perfect-class="formGroup"]').find('.jq_reason_error').text('');
                        resolve($reasonDropdown.val());
                        $modal.modal('hide');
                    }
                    else {
                        console.log('SD: reason dropdown else val() ', $reasonDropdown.val())
                        $reasonDropdown.closest('[perfect-class="formGroup"]').find('.jq_reason_error').text('Please select a reason');

                        //$modal.modal('hide');
                    }



                } else {
                    resolve(true);
                    $modal.modal('hide');
                }



            }))
            .append($('<button/>', { class: "btn btn-light" }).text(cancelText).click(function () {

                resolve(false);
                $modal.modal('hide');
            }))

        $modalBody.find('select').selectpicker('refresh')
        $modalFooter.append(dialog)
        $modal.html($modalDialog.html($modalContent.append($modalHead, $modalBody, $modalFooter))).modal({ backdrop: 'static', keyboard: false, show: true })


    });
    return p
}
function ConfirmDialogservice(input) {
    var p = new Promise(function (resolve, reject) {

        let hasReason = false;
        let reasonName = "ReasonID";
        let headingText = (input && input.heading) ? input.heading : "Confirm Delete?";
        let bodyText = (input && input.body) ? input.body : "Are you Sure you want to do this?";
        let cancelText = (input && input.cancel) ? input.cancel : "No";
        let confirmText = (input && input.confirm) ? input.confirm : "Yes";
        let headingIcon = (input && input.headingIcon) ? input.headingIcon : "fa fa-warning";
        let headingColor = (input && input.headingColor) ? input.headingColor : "text-primary";


        let $modaltitle = $('<h5/>', { class: "modal-title " + headingColor }).append($('<i>', { class: headingIcon })).append($('<span/>', { class: "ml-2" }).text(headingText));
        //   let $modalHead = $('<div/>', { class: "modal-header border-0" }).append($modaltitle, $('<button type="button" class="close" data-dismiss="modal"><span>&times;</span></button>'));
        let $modalHead = $('<div/>', { class: "modal-header border-0" }).append($modaltitle);
        let $modalBody = $('<div/>', { class: "modal-body pt-1" })
        let $modalFooter = $('<div/>', { class: "modal-footer border-0" });
        let $modalContent = $('<div/>', { class: "modal-content rounded-0" });
        let $modalDialog = $('<div/>', { class: "modal-dialog modal-dialog-centered", role: "document" });
        let $modal = $('<div/>', { class: "modal fade", tabindex: "-1", role: "dialog", "aria-labelledby": "", "aria-hidden": "true", "perfect-class": 'confirmDialogBox' });


        if (input && input.reason) {
            // console.log('SD : reason', input.reason);
            hasReason = true;
            let $select = $('<select/>', { class: "form-control", name: reasonName });
            $select.append($('<option/>', { value: "" }).text("Select a Reason"));
            $.each(input.reason, function (key, value) {
                $select.append($('<option/>', { value: value.ReasonID }).text(value.Reason));
            });

            $modalBody.append($('<div/>', { class: "col-md-12" }).append(
                $('<div/>', { class: "form-group", 'perfect-class': "formGroup" })
                    .append($('<label/>', { class: "mb-0" }).text(""))
                    .append($select)
                    .append($('<div/>', { class: "invalid-feedback animated fadeInUp jq_reason_error d-block" }))
            ));
        }



        $modalBody.append($('<div/>', { class: "px-3" }).html(bodyText));

        //remove the model when modal is closed
        // $modal.modal();
        $modal.on('hidden.bs.modal', function () {
            $(this).remove();
            resolve(false);
        });
        let dialog = $('<div/>')
          
            .append($('<button/>', { class: "btn btn-light" }).text(cancelText).click(function () {

                resolve(false);
                $modal.modal('hide');
            }))

        $modalBody.find('select').selectpicker('refresh')
        $modalFooter.append(dialog)
        $modal.html($modalDialog.html($modalContent.append($modalHead, $modalBody, $modalFooter))).modal({ backdrop: 'static', keyboard: false, show: true })


    });
    return p
}

function createSelectList(options) {

    console.log('> inside  [CreateSelectList] Function');

    let $table = $('<table/>', { class: "table mb-0  text-black table-hover  table-striped" });
    let $thead = $('<thead/>');
    let $tbody = $('<tbody/>');
    let $rowhead = $('<tr/>', { class: "btn-reveal-trigger" });

    var p = new Promise(function (resolve, reject) {
        let a = createSearchModalDOM().modal;
        if (options['settings'] && options['settings']['size']) {

            a.find('.modal-dialog').addClass(options['settings']['size'])
        }

        if (options['settings'] && options['settings']['tHeadClass']) {

            $thead.addClass(options['settings']['tHeadClass']);
        }
        else {
            $thead.addClass('thead-primary');
        }
        if (options["headingText"]) {

            a.find('.modal-title')
                .addClass('text-primary')
                .text(options.headingText);

        }

        // 

        // :: create table head ::
        if (options.data) {

            if (options["onlyShowColumn"] && options["onlyShowColumn"].length > 0) {

                // console.log(options["onlyShowColumn"]);
                // console.log(options["onlyShowColumn"].length);

                for (var k in options.data[0]) {
                    if (options["onlyShowColumn"].includes(k)) {

                        let header = (options["renameHeader"] && options["renameHeader"][k]) ? options["renameHeader"][k] : pft_camel_case_space(k);

                        $rowhead.append($('<th/>', { class: "py-2" }).text(header));

                    }

                }
            }
            else {
                for (var k in options.data[0]) {
                    if (options && options["hideColumn"] && options["hideColumn"].includes(k)) {
                        //dont appent any thing
                    }
                    else {
                        let header = (options["renameHeader"] && options["renameHeader"][k]) ? options["renameHeader"][k] : pft_camel_case_space(k);
                        $rowhead.append($('<th/>', { class: "py-2" }).text(header));
                    }

                }
            }
            $thead.append($rowhead);




        }
        // :: create table body ::
        if (options.data) {

            console.log('> inside  [CreateSelectList] Function > create table body if');
            //create rows
            $.each(options.data, function (i, valOne) {
                let $rowBody = $('<tr/>', { class: "btn-reveal-trigger", style: "cursor: pointer;" });
                $rowBody.data({ "pData": valOne });

                $rowBody.click(function () {
                    resolve($(this).closest('tr').data("pData"));
                    a.modal("hide");
                });

                if (options["onlyShowColumn"] && options["onlyShowColumn"].length > 0) {
                    $.each(valOne, function (keyOnef, valOnef) {
                        if (options["onlyShowColumn"].includes(keyOnef)) {
                            let $td = $('<td/>', { class: "py-2" }).text(valOnef);
                            $rowBody.append($td);
                        }
                    });
                }
                else {
                    $.each(valOne, function (keyOne, valOne) {
                        console.log(keyOne + "|" + valOne);

                        if (options && options["hideColumn"] && options["hideColumn"].includes(keyOne)) {
                            //since hidden dont appent anything
                            // $row.append(pft_table_createRowData(valOne));
                        }
                        else {
                            let $td = $('<td/>', { class: "py-2" }).text(valOne);
                            $rowBody.append($td);
                        }
                    });
                }
                $tbody.append($rowBody);
            });
        }

        a.find('.modal-body').append($table.append($thead).append($tbody));

        a.show;
        a.find('table').dataTable({ paging: true });

    });
    return p
}

function NotificationMessage(options) {
    
    if (options) {
        let tostOption = {
            hideMethod: "fadeOut",
            closeButton: !0
        };
        if (options['settings']) {
            if (options['settings']['timeOut']) {
                tostOption['timeOut'] = options['settings']['timeOut'];
            }
           
        }


        tostOption['positionClass'] = (options['position']) ? options['position'] : "toast-top-full-width";
      

        let message = (options['message']) ? options['message'] : "";
     
        heading = message;
       message=''

        if (options.type == 'success') {
            toastr.success(message, heading, tostOption);
        }
        else if (options.type == 'error') {
            toastr.error(message, heading, tostOption);
        }       
        else {
            toastr.info(message, heading, tostOption);
        }

    }
}


function fullSearchOption(option) {
    let $thisModel = createSearchModalDOM().modal;

    $thisModel.find('.model-body').append($('<button/>').click(option.fun(alee)))

    function alee(abc) {
        console.log(abc)
    }
    return $thisModel
}
/*
var options = {
    data: successData.Data,
    element: $model,
    onlyShowColumn: ["State", "ShortName"]
}
*/
function createSelectListWithSearch(options) {

    console.log('> inside  [CreateSelectList] Function');

    let $table = $('<table/>', { class: "table mb-0  text-black table-hover table-striped" });
    let $thead = $('<thead/>');
    let $tbody = $('<tbody/>');
    let $rowhead = $('<tr/>', { class: "btn-reveal-trigger" });

    if (options['settings'] && options['settings']['tHeadClass']) {

        $thead.addClass(options['settings']['tHeadClass']);
    }
    else {
        $thead.addClass('thead-primary');
    }

    var p = new Promise(function (resolve, reject) {
        let a = options.element;


        // :: create table head ::
        if (options.data) {
/*
            if (options["onlyShowColumn"] && options["onlyShowColumn"].length > 0) {

                 console.log(options["onlyShowColumn"]);
                // console.log(options["onlyShowColumn"].length);
 console.log(options["renameHeader"]);
                for (var k in options.data[0]) {
 console.log('loop',k);
                    if (options["onlyShowColumn"].includes(k)) {
                        let header = (options["renameHeader"] && options["renameHeader"][k]) ? options["renameHeader"][k] : pft_camel_case_space(k);
                        $rowhead.append($('<th/>', { class: "py-2" }).text(pft_camel_case_space(k)));

                    }

                }
            }
            else {

                for (var k in options.data[0]) {
                    if (options && options["hideColumn"] && options["hideColumn"].includes(k)) {
                        //dont appent any thing
                    }
                    else {
                        let header = (options["renameHeader"] && options["renameHeader"][k]) ? options["renameHeader"][k] : pft_camel_case_space(k);
                        $rowhead.append($('<th/>', { class: "py-2" }).text(k));
                    }
                }
            }*/
  if (options["onlyShowColumn"] && options["onlyShowColumn"].length > 0) {

                // console.log(options["onlyShowColumn"]);
                // console.log(options["onlyShowColumn"].length);

                for (var k in options.data[0]) {
                    if (options["onlyShowColumn"].includes(k)) {

                        let header = (options["renameHeader"] && options["renameHeader"][k]) ? options["renameHeader"][k] : pft_camel_case_space(k);

                        $rowhead.append($('<th/>', { class: "py-2" }).text(header));

                    }

                }
            }
            else {
                for (var k in options.data[0]) {
                    if (options && options["hideColumn"] && options["hideColumn"].includes(k)) {
                        //dont appent any thing
                    }
                    else {
                        let header = (options["renameHeader"] && options["renameHeader"][k]) ? options["renameHeader"][k] : pft_camel_case_space(k);
                        $rowhead.append($('<th/>', { class: "py-2" }).text(header));
                    }

                }
            }


            $thead.append($rowhead);
        }
        // :: create table body ::
        if (options.data) {

            console.log('> inside  [CreateSelectList] Function > create table body if');
            //create rows
            $.each(options.data, function (i, valOne) {
                let $rowBody = $('<tr/>', { class: "btn-reveal-trigger", style: "cursor: pointer;" });
                $rowBody.data({ "pData": valOne });

                $rowBody.click(function () {
                    resolve($(this).closest('tr').data("pData"));
                    a.modal("hide");
                });


                if (options["onlyShowColumn"] && options["onlyShowColumn"].length > 0) {
                    $.each(valOne, function (keyOnef, valOnef) {
                        if (options["onlyShowColumn"].includes(keyOnef)) {
                            let $td = $('<td/>', { class: "py-2" }).text(valOnef);
                            $rowBody.append($td);
                        }
                    });
                }
                else {
                    $.each(valOne, function (keyOned, valOned) {
                        // console.log(keyOne + "|" + valOne);

                        if (options && options["hideColumn"] && options["hideColumn"].includes(keyOned)) {
                            //since hidden dont appent anything
                            // $row.append(pft_table_createRowData(valOne));
                        }
                        else {
                            let $td = $('<td/>', { class: "py-2" }).text(valOned);
                            $rowBody.append($td);
                        }
                    });
                }



                $tbody.append($rowBody);
            });
        }
        a.on('hidden.bs.modal', function () {
            $(this).find('.modal-body').find('[perfect-class="formGroupModalTable"]').empty();
        });
       // a.find('.modal-body').find('[perfect-class="formGroupModalTable"]').css('background-color','red')
        a.find('.modal-body').find('[perfect-class="formGroupModalTable"]').html($table.append($thead).append($tbody));

        a.show;
        a.find('table').dataTable({ paging: true });

    });
    return p
}
