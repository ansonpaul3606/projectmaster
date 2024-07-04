

//jquery validation fail if the element dont have a 'name' attribute , so when every creating a jquery validation event give a name attribute to the element

var perfectCRMvalidation = (function () {

    
    //const _alphanumericREGX = /^([a-zA-Z0-9_,.-\s\+]+)$/;

    const _alphanumericREGX = /^([\()\a-zA-Z0-9,.\s\\\/+-]+)$/
    const _alphanumericspace = /^[^\s]+(\s+[^\s]+)*$/;
    //const _decimalNumber = /^[0-9]*\.[0-9]{2}$/;
    const _decimalNumber = /^\d+(\.\d{1,2})?$/;
    //const _quantityNumber = /^[0-9]*\.[0-9]{3}$/;
    const _quantityNumber = /^\d+(\.\d{1,3})?$/;
    const _mobileAndLand =/^[\+]?[0-9]*$/;
    //-----this may change
    const _numberOnlyREGX = /^[0-9]*$/;

    const _textareaStringREGX = /^([a-zA-Z0-9_,.-\s\+]+)$/;

    const _faxValidation = /^\+?[0-9-\s]+$/;

    const _latitude = /^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}?[eENn]{1}$/;

    const _speciaCharREGX = /^([\\/\()\[\]a-zA-Z0-9_,.\-\s\+]+)$/;

    const _specialcode = /^[0-9A-Za-z\s\-\\//]+$/
;

    //const _speciaCharNamecode = /^([\()\a-zA-Z0-9,.\s]+)$/;

    const _emailRegex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    const _speciaCharUserpwd= /[A-Za-z0-9_~\-!@#\$%\^&\*\(\)]+$/;

    const _alphanumericOnlyREGX = /^([a-zA-Z0-9]+)$/;
    const _folderlocationRegx = /[a-zA-Z]:\\./;
    const _NospaceNameregexp = /^\S*$/;
    const _alphaOnlyREGX = /^([a-zA-Z]+)$/;
    //--------------|   D E F I N E   M E T H O D S  H E R E   |--------------

    //---   Method to validate number(digit only)
    //---   Method for number with 2 decimal point
    jQuery.validator.addMethod('decimal', function (value, element, param) {
        //if (_decimalNumber.test(value)) {
        //    return true;
        //} else {
        //    return false;
        //}
        return this.optional(element) || _decimalNumber.test(value);
    }, "Number with 2 decimal point only");
    //---   Method for number with 3 decimal point
    jQuery.validator.addMethod('quantity', function (value, element, param) {
        //if (_quantityNumber.test(value)) {
        //    return true;
        //} else {
        //    return false;
        //}
        return this.optional(element) || _quantityNumber.test(value);
    }, "Number with 3 decimal point only");
    //---   Method to validate alpha-numberic string
    jQuery.validator.addMethod('stringonly', function (value, element, param) {
        //if (_alphanumericREGX.test(value)) {
        //    return true;
        //} else {
        //    return false;
        //}
        return this.optional(element) || _alphanumericREGX.test(value);
    }, "Only allow ( a-zA-Z0-9( ),.\/\\ +-)");
    jQuery.validator.addMethod('space', function (value, element, param) {
        //if (_alphanumericREGX.test(value)) {
        //    return true;
        //} else {
        //    return false;
        //}
        return this.optional(element) || _alphanumericspace.test(value);
    }, "Not allowed space in beginning and end");

    jQuery.validator.addMethod("alphanumeric", function (value, element) {
        return this.optional(element) || /^\w+$/i.test(value);
    }, "Letters, numbers, and underscores only please");
    jQuery.validator.addMethod('anyphonenumber', function (value, element, param) {
        if (_mobileAndLand.test(value)) {
            return true;
        } else {
            return false;
        }
    }, "Only digits allowed");
    jQuery.validator.addMethod('alphastringonly', function (value, element, param) {
        //if (_alphanumericREGX.test(value)) {
        //    return true;
        //} else {
        //    return false;
        //}
        return this.optional(element) || _alphaOnlyREGX.test(value);
    }, "Only allow (a-zA-Z)");


    //---------this may change

    jQuery.validator.addMethod('numberonly', function (value, element, param) {
        if (_numberOnlyREGX.test(value)) {
            return true;
        } else {
            return false;
        }
    }, "Only number allowed");
    
    $.validator.addMethod("time", function (value, element) {
        return this.optional(element) || /^([01]\d|2[0-3]|[0-9])(:[0-5]\d){1,2}$/.test(value);
    }, "Please enter a valid time, between 00:00 and 23:59");
    $.validator.addMethod("time12h", function (value, element) {
        return this.optional(element) || /^((0?[1-9]|1[012])(:[0-5]\d){1,2}(\ ?[AP]M))$/i.test(value);
    }, "Please enter a valid time in 12-hour am/pm format");

    jQuery.validator.addMethod('textareaString', function (value, element, param) {
        return this.optional(element) || _textareaStringREGX.test(value);
    }, "invalid input [ a-zA-Z0-9_-,.+]");

    jQuery.validator.addMethod('fax', function (value, element, param) {
        return this.optional(element) || _faxValidation.test(value);
    }, "Invalid fax");

    jQuery.validator.addMethod('latitude', function (value, element, param) {
        return this.optional(element) || _latitude.test(value);
    }, "Invalid latitude");
    jQuery.validator.addMethod('specialChar', function (value, element, param) {
        return this.optional(element) || _speciaCharREGX.test(value);
    }, "Special characters are not allowed");
    jQuery.validator.addMethod('specialCharUserpwd', function (value, element, param) {
        return this.optional(element) || _speciaCharUserpwd.test(value);
    }, "Invalid Input");

    //jQuery.validator.addMethod('specialCharName', function (value, element, param) {
    //    return this.optional(element) || _speciaCharName.test(value);
    //}, "Only allow ( a-zA-Z0-9( ),.\/\\ )");

    jQuery.validator.addMethod('emailRegex', function (value, element, param) {
        return this.optional(element) || _emailRegex.test(value);
    }, "Invalid Mail Id");
    jQuery.validator.addMethod('specialcode', function (value, element, param) {
        return this.optional(element) || _specialcode.test(value);
    }, "Invalid Input");

    jQuery.validator.addMethod('usercode', function (value, element, param) {
        return this.optional(element) || _alphanumericOnlyREGX.test(value);
    }, "Invalid ,Only Alphabet and number");

    jQuery.validator.addMethod('folderlocationRegx', function (value, element, param) {
        return this.optional(element) || _folderlocationRegx.test(value);
    }, "Invalid Path");
    jQuery.validator.addMethod('NospaceNameregexp', function (value, element, param) {
        return this.optional(element) || _NospaceNameregexp.test(value);
    }, "Invalid,Space not Allowed");
    //--------------|   D E F A U L T   V A L I D A T I O N   H E R E   |--------------


    jQuery.validator.setDefaults({
        ignore: [],
        errorClass: "invalid-feedback animated fadeInUp",
        errorElement: "div",
        errorPlacement: function (e, a) {
            jQuery(a).parents(".form-group > div").append(e)
            
        },
        highlight: function (e) {
            jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
       
        },
        success: function (e) {
            jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-valid")
        },
        invalidHandler: function (form, validator) {
            var errors = validator.numberOfInvalids();
            if (errors) {

                validator.errorList[0].element.focus();
            }
        } 
    });

    //--------------|   C U S T O M E   C L A S S   H E R E   |--------------

    //--- C O N F I R M E D  C L A S S E S

    jQuery.validator.addClassRules("perfectValidate_name", {
        required: true,
        maxlength: 150,
        'stringonly': true,
        'space': true
    });
    jQuery.validator.addClassRules("perfectValidate_name_nm", {
        maxlength: 150,
        'stringonly': true
    });
    jQuery.validator.addClassRules("perfectValidate_shortName", {
        required: true,
        maxlength: 10,
        'alphanumeric': true,
        'space': true
    });
   
    jQuery.validator.addClassRules("perfectValidate_shortName_nm", {
        maxlength: 10,
        'alphanumeric': true
    });
    jQuery.validator.addClassRules("perfectValidate_empname", {
        required: true,
        maxlength: 20,
        'alphastringonly': true,
        'space': true
    });
    jQuery.validator.addClassRules("perfectValidate_empname_nm", {
        maxlength: 20,
        'alphastringonly': true
    });
    jQuery.validator.addClassRules("perfectValidate_empLastName", {
        required: true,
        maxlength: 10,
        'alphastringonly': true,
        'space': true
    });

    jQuery.validator.addClassRules("perfectValidate_empLastName_nm", {
        maxlength: 10,
        'alphastringonly': true
    });
    jQuery.validator.addClassRules("perfectValidate_numeric", {
        required: true,
        digits: !0
    });
    jQuery.validator.addClassRules("perfectValidate_numeric_nm", {
        digits: !0
    });
    jQuery.validator.addClassRules("perfectValidate_digit", {
        required: true,
        digits: !0
    });
    jQuery.validator.addClassRules("perfectValidate_decimal", {
        required: true,
        'decimal': true,

    });
    jQuery.validator.addClassRules("perfectValidate_decimal_nm", {
        
        'decimal': true,

    });
    jQuery.validator.addClassRules("perfectValidate_quantity", {
        required: true,
        'quantity': true,
    });
    jQuery.validator.addClassRules("perfectValidate_quantity_nm", {
        'quantity': true,
    });
    jQuery.validator.addClassRules("perfectValidate_email", {
        required: true,
        'emailRegex': true
     
       //email: !0
    });
    jQuery.validator.addClassRules("perfectValidate_email_nm", {
        'emailRegex': true,
    });

    jQuery.validator.addClassRules("perfectValidate_specialcode_nm", {
        'specialcode': true,
    });

    jQuery.validator.addClassRules("perfectValidate_mobile", {
        required: true,
        'anyphonenumber': true,
         minlength: 9,
        maxlength:15

    });
    jQuery.validator.addClassRules("perfectValidate_mobile_nm", {
        'anyphonenumber': true,
        minlength: 9,
        maxlength: 15

    });
    jQuery.validator.addClassRules("perfectValidate_date", {
        required: true, 
        max: "2050-01-01",
        date: true
       
    });
    jQuery.validator.addClassRules("perfectValidate_date_nm", {
        max: "2050-01-01",
        date: true
    });
    jQuery.validator.addClassRules("perfectValidate_string", {
        required: true,
        'stringonly': true,
        'space':true
    });
    jQuery.validator.addClassRules("perfectValidate_string_SPC", {
        required: true,
        'stringonly': false,
        'space': true
    });
    jQuery.validator.addClassRules("perfectValidate_string_nm", {
        'stringonly': true,
        'space': true
    });
    jQuery.validator.addClassRules("perfectValidate_url", {
        required: true,
        url: !0
    });
    jQuery.validator.addClassRules("perfectValidate_url_nm", {
        url: !0
    });
    jQuery.validator.addClassRules("perfectValidate_gstinno", {
        required: true,
        'usercode': true,
        minlength: 15,
        maxlength: 15
    });
    jQuery.validator.addClassRules("perfectValidate_gstinno_nm", {
        
        'usercode': true,
        minlength: 15,
        maxlength: 15
    });
    jQuery.validator.addClassRules("perfectValidate_Adhaar_nm", {

        'usercode': true,
        minlength: 12,
        maxlength: 12
    });
    jQuery.validator.addClassRules("perfectValidate_Adhaar", {

        'usercode': true,
        minlength: 12,
        maxlength: 12
    });
    jQuery.validator.addClassRules("perfectValidate_required", {
        required: true,

    });

    jQuery.validator.addClassRules("perfectValidate_time", {
        required: true,
        'time': true,
    });
    jQuery.validator.addClassRules("perfectValidate_time_nm", {
        'time': true,
    });
    jQuery.validator.addClassRules("perfectValidate_time12h", {
        required: true,
        'time12h': true,
    });
    jQuery.validator.addClassRules("perfectValidate_time12h_nm", {
        'time12h': true,
    });
    jQuery.validator.addClassRules("perfectValidate_textarea", {
        required: true,
        'textareaString': true,
    });
    jQuery.validator.addClassRules("perfectValidate_textarea_nm", {
        'textareaString': true,
    });
    jQuery.validator.addClassRules("perfectValidate_hsncode_nm", {
        'textareaString': true,
       
        maxlength: 15
    });
    jQuery.validator.addClassRules("perfectValidate_fax", {
        required: true,
        'fax': true,
        minlength: 10,
        maxlength: 15
    });
    jQuery.validator.addClassRules("perfectValidate_fax_nm", {
        'fax': true,
        minlength: 10,
        maxlength: 15
    });

    jQuery.validator.addClassRules("perfectValidate_latitude", {
        required: true,
        'latitude': true,
    });
    jQuery.validator.addClassRules("perfectValidate_latitude_nm", {
        'latitude': true,
    });
    jQuery.validator.addClassRules("perfectValidate_specialChar", {
        required: true,
       'specialChar': true,
        'stringonly': true,
        'space': true
    });
    jQuery.validator.addClassRules("perfectValidate_specialChar_nm", {
       'specialChar': true,
        'stringonly': true,
        
    });

    jQuery.validator.addClassRules("perfectValidate_specialCharName", {
        required: true,
        maxlength: 150,
      //  'specialCharName': true,
        'stringonly': true,
        'space': true
    });
    jQuery.validator.addClassRules("perfectValidate_specialCharName_nm", {
        maxlength: 150,
      //  'specialCharName': true,
        'stringonly': true,
        'space': true
    });
    jQuery.validator.addClassRules("perfectValidate_Remarks_nm", {
        maxlength: 250,
        'specialChar': true,
        'stringonly': true,

    });
    jQuery.validator.addClassRules("perfectValidate_Remarks", {
        required: true,
        maxlength: 250,

        'specialChar': true,
        'stringonly': true,

    });
    jQuery.validator.addClassRules("perfectValidate_userCodepwd", {
        required: true,
        'specialCharUserpwd': true,
        minlength: 6,
        maxlength: 10
    });
    jQuery.validator.addClassRules("perfectValidate_userCode_nm", {
        'specialCharUserpwd': true,
        minlength: 6,
        maxlength: 10
    });
    jQuery.validator.addClassRules("perfectValidate_userCode", {
        required:true,
        'specialCharUserpwd': true,
        minlength: 6,
        maxlength: 10
    });
    jQuery.validator.addClassRules("perfectValidate_folderlocationRegx", {
        required: true,
        'folderlocationRegx': true,
    }); 
    jQuery.validator.addClassRules("perfectValidate_backupname", {
        required: true,
        maxlength: 150,
        'stringonly': true,
        'NospaceNameregexp':true
        
    });
    //---  C L A S S E S   M A Y   C H A N G E
    jQuery.validator.addClassRules("perfectValidate_number", {
        required: true,
        'numberonly': true,
    });

   

    jQuery.validator.addClassRules("perfectValidate_checkbox", {
        required: true,

    });
   
    jQuery.validator.addClassRules("perfectValidate_string_PR", {
        required: true,
        'stringonly': true,
        'space': true,
        maxlength: 20,
    });
    jQuery.validator.addClassRules("perfectValidate_specialCharCusName", {
        required: true,
        maxlength: 20,
        //  'specialCharName': true,
        'stringonly': true,
        'space': true
    });
    jQuery.validator.addClassRules("perfectValidate_specialCharCusName_nm", {
        maxlength: 20,
        //  'specialCharName': true,
        'stringonly': true,
        'space': true
    });
    //---- This is commented because default settings is set via jQuery.validator.setDefaults({}) above
    //var ValidateForm = function (formSelector) {

    //    jQuery(formSelector).validate({
    //        //rules: {
    //        //    'aaa':{
    //        //        validateAlphaNumericOnly: true,

    //        //    },
    //        //    'aab': {
    //        //        validateNumberOnly: true,
    //        //    }
    //        //},
    //        ignore: [],
    //        errorClass: "invalid-feedback animated fadeInUp",
    //        errorElement: "div",
    //        errorPlacement: function (e, a) {
    //            jQuery(a).parents(".form-group > div").append(e)
    //        },
    //        highlight: function (e) {
    //            jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
    //        },
    //        success: function (e) {
    //            jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-valid")
    //        }
    //    });
    //}

    //---- This section is commented because add class section can do the same thing but if some reason its dont work(ie validation only work for the first class only or you need custom message for a specific method which is not relevent) use this code
    //var validateCustomClass = function () {
    //    $('.perfectValidate_number').each(function () {
    //        $(this).rules('add', {
    //            digits: true,
    //            'numberonly': true,
    //            required: true,
                
    //            messages: {
    //                 digits:"Enter 0-9",
    //                 required: "validation message field empty number",
    //                 'numberonly':"Message in class function mumber"//<---- This field overright the message in the addMethod validaton section
    //            }
    //        });
    //    });

    //    $('.perfectValidate_string').each(function () {
    //        $(this).rules('add', {
    //            'stringonly': true,
    //            required: true,
    //            // messages: {
    //            //     required: "validation message field empty number",
    //            //     'validateNumberOnly':"Message in class function mumber"//<---- This field overright the message in the addMethod validaton section
    //            //}
    //        });
    //    });
    //};


    return {
        init: function (formSelector) {

            console.log("> Validation init")
           // ValidateForm('form');
          jQuery(formSelector).validate();
            //jQuery(formSelector).find('[perfect-css="select"]').selectpicker()
            //jQuery(formSelector).find('[perfect-css="input"]').val('ddd')
           // validateCustomClass()
        }
    }

})();


var initFunctions = function (ele) {

    //$(ele).validate();

    $(ele).find('[perfect-css="select"]').selectpicker()
    $(ele).find('select[perfect-selectButton]').each(function () {

        let $this = $(this);
        let optgrp = $('<optgroup/>', { "perfect-class":"selectButtongroup"});
       // let optgrp = ($this.find('[perfect-class=selectButtongroup]').length > 0) ? $this.find('[perfect-class=selectButtongroup]') : $('<optgroup/>', { "perfect-class": "selectButtongroup" });
      //  alert($this.find('[perfect-class=selectButtongroup]').length);
        if ($this.find('[perfect-class=selectButtongroup]').length > 0) {
            $this.find('[perfect-class=selectButtongroup]').remove();

        }
      
        optgrp.html($('<option/>', { 'data-icon': "fa fa-plus" })
            .text($this.attr('perfect-selectButton'))
            .data({ pftActionButton: { name: $this.attr('perfect-selectButton'), action: $this.attr('perfect-selectButtonAction') } })
        );
       
        $this.append(optgrp);
        
        $this.selectpicker('refresh');

    });
    $(ele).find('select[perfect-selectButton]').off('changed.bs.select');
    $(ele).find('select[perfect-selectButton]').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
       
   
        // do something...
        let $this = $(this);
        //console.log($(this).eq(clickedIndex).text())
        let actionvalue = $this.find('option').eq(clickedIndex).data('pftActionButton');

        $this.closest("form").valid();
        if (actionvalue) {
            $this.val('');
            $this.selectpicker('refresh');

            window[actionvalue.action]($this);

        }
    });


    //$(ele).find('button[perfect-SearchButton]').each(function () {
    //    let $this = $(this);
    //    $this.on('click', window[$this.attr('perfect-SearchButton')]);

    //});

};