


var pft_camel_case_space = (text) => text.replace(/([A-Z])/g, ' $1').replace(/^./, function (str) { return str.toUpperCase(); })

var pft_table_createRow = () => $('<tr/>', { class: "btn-reveal-trigger" });
var pft_table_createRowData = (val, head) => $('<td/>', { class: "py-2 text-left " + head }).text(val);
var pft_table_createRowDataright = (val, head) => $('<td/>', { class: "py-2 text-right " + head }).text(val);
var pft_table_createRowDataIcon = (icon) => $('<td/>', { class: "py-2 text-left" }).html($('<i/>', { class: icon }));
var pft_table_createRowDataSVG = (icon) => $('<td/>', { class: "py-2 text-left" }).html($("<i>" + icon + "<i/>"));
var pft_table_createRowDataImage = (image) => $('<td/>', { class: "py-2 text-left" }).html($('<img/>', { src: image, width: '50px' }));

var pft_table_createRowDatahyperlinkparam = (hyper, urls) => $('<td/>', { class: "py-2 text-left" }).html($('<a href ="' + urls + hyper + '" class="atag" target="_blank" >' + hyper + '</a>'));

var pft_table_createRowDataTrackInfo = (track, title, val) => $('<td/>', { class: "py-2 text-left" }).html($(`<a href ="#" class="atag" title="${title}" onClick="fn_ShowTrackInfo(${val["ID_Master"]},'${val["TransMode"]}','${track}')">${track}</a>`));
var pft_table_createRowDatahyperlink = (hyper, urls) => $('<td/>', { class: "py-2 text-left" }).html($('<a href ="' + urls + '" class="atag" target="_blank" >' + hyper + '</a>'));
var pft_table_createRowDatanohyperlink = (hyper) => $('<td/>', { class: "py-2 text-left" }).html($('<a href ="#" class="atag">' + hyper + '</a>'));
var pft_table_createRowDataSVGbutton = (icon) => $('<td/>', { class: "py-2 text-left" }).html($("<button>" + icon + "</button>"));
var pft_table_createRowDatalink = (link) => $('<td/>', { class: "py-2 text-left" }).html($("<a>" + link + "</a>"));

//var pft_table_createRowDataFeedbackInfo = (track, title, val) => $('<td/>', { class: "py-2 text-left" }).html($(`<a href ="#" class="atag" title="${title}" onClick="fn_ShowFeedbackInfo(${val["ID_Master"]},'${val["TransMode"]}','${track}')">${track}</a>`));

var pft_table_createRowWithData = (val, options) => {
    // this function return a full table row

    let $row = pft_table_createRow();

    $row.data({ "pData": val });

  
    if (options && options["onlyShowColumn"] && options["onlyShowColumn"].length > 0) {
        $.each(val, function (keyOne, valOne) {
     
        // console.log( keyOne + "|" + valOne);

            if (options["onlyShowColumn"].includes(keyOne)) {
                //since hidden dont appent anything
                let $td = "";
                if (options["renameHeader"] && options["renameHeader"][keyOne]) {
                    if (options["renameHeader"][keyOne].includes('1R')) {
                        $td = pft_table_createRowDataright(valOne == "" || valOne == null ? null : (((options && options["isDateType"] && options["isDateType"].includes(keyOne)) ? moment(valOne).format('DD/MM/YYYY') : valOne)), keyOne);

                    }
                    else {
                       
                        $td = pft_table_createRowData(valOne == "" || valOne == null? null : (((options && options["isDateType"] && options["isDateType"].includes(keyOne)) ? moment(valOne).format('DD/MM/YYYY') : valOne)), keyOne);

                    }

                }
                else {
                    if (keyOne.includes('1R')) {
                        $td = pft_table_createRowDataright(valOne == "" || valOne == null ? null : (((options && options["isDateType"] && options["isDateType"].includes(keyOne)) ? moment(valOne).format('DD/MM/YYYY') : valOne)), keyOne);

                    }
                    else {

                        $td = pft_table_createRowData(valOne == "" || valOne == null ? null : (((options && options["isDateType"] && options["isDateType"].includes(keyOne)) ? moment(valOne).format('DD/MM/YYYY') : valOne)), keyOne);

                    }

                }
               
                if (options && options["isCheckType"] && options["isCheckType"].includes(keyOne)) {
                    $td = pft_table_createRowDataIcon((valOne == "true" || valOne == true) ? 'text-success fa fa-check' : 'text-danger fa fa-times');
                }
                if (options && options["customIcon"] && options["customIcon"].includes(keyOne)) {
                  
                   
                    if (valOne =="Cold") {
                        $td = pft_table_createRowDataSVG((valOne == "Cold") ? '<svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 24 27.319"><path id = "cold-temperature" d = "M25.711,19.788,23.227,18.36l1.105-.3a1.242,1.242,0,1,0-.646-2.4l-3.5.944-3.365-1.95,3.365-1.95,3.5.944h.323a1.252,1.252,0,1,0,.323-2.484l-1.105-.236,2.484-1.428a1.246,1.246,0,0,0-1.242-2.161L21.787,8.91l.373-1.378a1.242,1.242,0,1,0-2.4-.646l-1.018,3.725-3.167,1.9V8.624l2.57-2.57A1.242,1.242,0,1,0,16.385,4.3l-.807.807V2.242a1.242,1.242,0,1,0-2.484,0V5.309L12.089,4.3a1.242,1.242,0,0,0-1.763,1.751l2.769,2.769v3.725L9.9,10.661,8.885,6.936a1.242,1.242,0,1,0-2.4.646l.4,1.329L4.2,7.37A1.246,1.246,0,1,0,2.962,9.531l2.484,1.4L4.4,11.257a1.252,1.252,0,1,0,.323,2.484h.323l3.5-.944,3.3,1.863-3.365,1.95-3.5-.944a1.242,1.242,0,1,0-.584,2.4l1.105.3L3.024,19.788a1.246,1.246,0,0,0,1.242,2.161l2.62-1.54-.373,1.378a1.242,1.242,0,0,0,.857,1.565,1.428,1.428,0,0,0,.323,0,1.242,1.242,0,0,0,1.242-.919l1.018-3.725,3.142-1.9v3.887l-2.57,2.57a1.242,1.242,0,1,0,1.763,1.751l.807-.807v2.868a1.242,1.242,0,1,0,2.484,0V24.01l1.006,1.006a1.242,1.242,0,1,0,1.763-1.751L15.578,20.5V16.77l3.191,1.85,1.018,3.725a1.242,1.242,0,0,0,1.242.919,1.428,1.428,0,0,0,.323,0,1.242,1.242,0,0,0,.882-1.527l-.447-1.329,2.657,1.54a1.246,1.246,0,0,0,1.242-2.161Z" transform = "translate(-2.336 -1)" fill="#1ca4fc"/></svg >' : '<svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 24 27.319"><path id = "cold-temperature" d = "M25.711,19.788,23.227,18.36l1.105-.3a1.242,1.242,0,1,0-.646-2.4l-3.5.944-3.365-1.95,3.365-1.95,3.5.944h.323a1.252,1.252,0,1,0,.323-2.484l-1.105-.236,2.484-1.428a1.246,1.246,0,0,0-1.242-2.161L21.787,8.91l.373-1.378a1.242,1.242,0,1,0-2.4-.646l-1.018,3.725-3.167,1.9V8.624l2.57-2.57A1.242,1.242,0,1,0,16.385,4.3l-.807.807V2.242a1.242,1.242,0,1,0-2.484,0V5.309L12.089,4.3a1.242,1.242,0,0,0-1.763,1.751l2.769,2.769v3.725L9.9,10.661,8.885,6.936a1.242,1.242,0,1,0-2.4.646l.4,1.329L4.2,7.37A1.246,1.246,0,1,0,2.962,9.531l2.484,1.4L4.4,11.257a1.252,1.252,0,1,0,.323,2.484h.323l3.5-.944,3.3,1.863-3.365,1.95-3.5-.944a1.242,1.242,0,1,0-.584,2.4l1.105.3L3.024,19.788a1.246,1.246,0,0,0,1.242,2.161l2.62-1.54-.373,1.378a1.242,1.242,0,0,0,.857,1.565,1.428,1.428,0,0,0,.323,0,1.242,1.242,0,0,0,1.242-.919l1.018-3.725,3.142-1.9v3.887l-2.57,2.57a1.242,1.242,0,1,0,1.763,1.751l.807-.807v2.868a1.242,1.242,0,1,0,2.484,0V24.01l1.006,1.006a1.242,1.242,0,1,0,1.763-1.751L15.578,20.5V16.77l3.191,1.85,1.018,3.725a1.242,1.242,0,0,0,1.242.919,1.428,1.428,0,0,0,.323,0,1.242,1.242,0,0,0,.882-1.527l-.447-1.329,2.657,1.54a1.246,1.246,0,0,0,1.242-2.161Z" transform = "translate(-2.336 -1)" fill="#1ca4fc"/></svg >');
                    }
                    else if (valOne =="Hot") {

                        $td = pft_table_createRowDataSVG((valOne == "Hot") ? '<svg xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" width="15" height="15" viewBox="0 0 30 30"><defs><clipPath id="clip-path"><path id="Path_5" data-name="Path 5" d="M0-47.5H30v30H0Z" fill="#FF0000"/></clipPath></defs ><g id="hot" transform="translate(0 47.5)" clip-path="url(#clip-path)"><path id="Path_1" data-name="Path 1" d="M-14.064,0a4.071,4.071,0,0,1,1.154,2.706c0,3.488-5.3,6.316-11.842,6.316S-36.594,6.194-36.594,2.706A4.075,4.075,0,0,1-35.439,0c-1.708,1.192-2.733,2.674-2.733,4.285,0,3.924,6.009,7.105,13.421,7.105S-11.33,8.209-11.33,4.285c0-1.611-1.026-3.093-2.733-4.285" transform="translate(39.751 -29.68)" fill="#FF0000" /><path id="Path_2" data-name="Path 2" d="M-1.152-12.434a1.185,1.185,0,0,1-1.06-.654,1.185,1.185,0,0,1,.53-1.589,5.071,5.071,0,0,0,2.5-4.467c0-1.679-.919-2.869-1.892-4.128-1.011-1.308-2.056-2.661-2.056-4.556A8.109,8.109,0,0,1-1.2-33.4a1.186,1.186,0,0,1,1.675,0,1.184,1.184,0,0,1,.008,1.667,5.821,5.821,0,0,0-1.24,3.908c0,1.086.724,2.023,1.562,3.108A8.886,8.886,0,0,1,3.19-19.145,7.438,7.438,0,0,1-.623-12.559a1.185,1.185,0,0,1-.529.125" transform="translate(9.047 -12.566)" fill="#FF0000" /><path id="Path_3" data-name="Path 3" d="M-1.152-12.434a1.185,1.185,0,0,1-1.06-.654,1.185,1.185,0,0,1,.53-1.589,5.071,5.071,0,0,0,2.5-4.467c0-1.679-.919-2.869-1.892-4.128-1.011-1.308-2.056-2.661-2.056-4.556A8.109,8.109,0,0,1-1.2-33.4a1.186,1.186,0,0,1,1.675,0,1.184,1.184,0,0,1,.008,1.667,5.821,5.821,0,0,0-1.24,3.908c0,1.086.724,2.023,1.562,3.108A8.89,8.89,0,0,1,3.19-19.145,7.438,7.438,0,0,1-.623-12.559a1.185,1.185,0,0,1-.529.125" transform="translate(14.574 -12.566)" fill="#FF0000" /><path id="Path_4" data-name="Path 4" d="M-1.152-12.434a1.185,1.185,0,0,1-1.06-.654,1.185,1.185,0,0,1,.53-1.589,5.071,5.071,0,0,0,2.5-4.467c0-1.679-.919-2.869-1.891-4.128-1.011-1.308-2.057-2.661-2.057-4.556A8.109,8.109,0,0,1-1.2-33.4a1.186,1.186,0,0,1,1.675,0,1.184,1.184,0,0,1,.008,1.667,5.821,5.821,0,0,0-1.24,3.908c0,1.086.724,2.023,1.562,3.108A8.894,8.894,0,0,1,3.19-19.145,7.438,7.438,0,0,1-.623-12.559a1.187,1.187,0,0,1-.529.125" transform="translate(20.1 -12.566)" fill="#FF0000" /></g></svg >' : '<svg xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" width="15" height="15" viewBox="0 0 30 30"><defs><clipPath id="clip-path"><path id="Path_5" data-name="Path 5" d="M0-47.5H30v30H0Z" fill="#FF0000" /></clipPath></defs ><g id="hot" transform="translate(0 47.5)" clip-path="url(#clip-path)"><path id="Path_1" data-name="Path 1" d="M-14.064,0a4.071,4.071,0,0,1,1.154,2.706c0,3.488-5.3,6.316-11.842,6.316S-36.594,6.194-36.594,2.706A4.075,4.075,0,0,1-35.439,0c-1.708,1.192-2.733,2.674-2.733,4.285,0,3.924,6.009,7.105,13.421,7.105S-11.33,8.209-11.33,4.285c0-1.611-1.026-3.093-2.733-4.285" transform="translate(39.751 -29.68)" fill="#FF0000" /><path id="Path_2" data-name="Path 2" d="M-1.152-12.434a1.185,1.185,0,0,1-1.06-.654,1.185,1.185,0,0,1,.53-1.589,5.071,5.071,0,0,0,2.5-4.467c0-1.679-.919-2.869-1.892-4.128-1.011-1.308-2.056-2.661-2.056-4.556A8.109,8.109,0,0,1-1.2-33.4a1.186,1.186,0,0,1,1.675,0,1.184,1.184,0,0,1,.008,1.667,5.821,5.821,0,0,0-1.24,3.908c0,1.086.724,2.023,1.562,3.108A8.886,8.886,0,0,1,3.19-19.145,7.438,7.438,0,0,1-.623-12.559a1.185,1.185,0,0,1-.529.125" transform="translate(9.047 -12.566)" fill="#FF0000" /><path id="Path_3" data-name="Path 3" d="M-1.152-12.434a1.185,1.185,0,0,1-1.06-.654,1.185,1.185,0,0,1,.53-1.589,5.071,5.071,0,0,0,2.5-4.467c0-1.679-.919-2.869-1.892-4.128-1.011-1.308-2.056-2.661-2.056-4.556A8.109,8.109,0,0,1-1.2-33.4a1.186,1.186,0,0,1,1.675,0,1.184,1.184,0,0,1,.008,1.667,5.821,5.821,0,0,0-1.24,3.908c0,1.086.724,2.023,1.562,3.108A8.89,8.89,0,0,1,3.19-19.145,7.438,7.438,0,0,1-.623-12.559a1.185,1.185,0,0,1-.529.125" transform="translate(14.574 -12.566)" fill="#FF0000" /><path id="Path_4" data-name="Path 4" d="M-1.152-12.434a1.185,1.185,0,0,1-1.06-.654,1.185,1.185,0,0,1,.53-1.589,5.071,5.071,0,0,0,2.5-4.467c0-1.679-.919-2.869-1.891-4.128-1.011-1.308-2.057-2.661-2.057-4.556A8.109,8.109,0,0,1-1.2-33.4a1.186,1.186,0,0,1,1.675,0,1.184,1.184,0,0,1,.008,1.667,5.821,5.821,0,0,0-1.24,3.908c0,1.086.724,2.023,1.562,3.108A8.894,8.894,0,0,1,3.19-19.145,7.438,7.438,0,0,1-.623-12.559a1.187,1.187,0,0,1-.529.125" transform="translate(20.1 -12.566)" fill="#FF0000" /></g></svg >');
                    }
                    else if (valOne =="Warm") {
                        $td = pft_table_createRowDataSVG((valOne == "Warm") ? '<svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 24 23.968"><path id = "Layer_2" data - name="Layer 2" d = "M9.1,21.922c-.6.83-2.2-.176-1.716-1.078C7.954,19.8,9.774,20.948,9.1,21.922ZM12.751,7.377a.8.8,0,0,0,0-1.6,7.187,7.187,0,1,0,5.8,2.634.8.8,0,0,0-1.229,1.014,5.588,5.588,0,0,1-4.359,9.124C5.718,18.345,5.511,7.848,12.751,7.377Zm2.81.639a.8.8,0,0,0,1.078-.335c.407-.8-.487-1.221-1.11-1.429a.8.8,0,0,0-1.03.463C14.212,7.5,15,7.768,15.561,8.016Zm9.116,7.344c-1.836-1.644-1.724-3.057,0-4.79a.774.774,0,0,0-.311-1.357c-2.395-.6-3.145-1.82-2.395-4.175a.8.8,0,0,0-1.022-1.022c-2.395.71-3.608,0-4.183-2.395a.8.8,0,0,0-1.4-.375c-1.668,1.78-3.153,1.78-4.79,0a.8.8,0,0,0-1.357.343c-.631,2.395-1.82,3.193-4.167,2.435a.8.8,0,0,0-1.03.958c.647,2.395,0,3.672-2.395,4.191a.823.823,0,0,0-.407,1.4C3,12.262,3,13.7,1.216,15.36a.8.8,0,0,0,.351,1.357A5.588,5.588,0,0,1,3,17.252c.806.455,1.756,1.3,1.03,3.7a.8.8,0,0,0,.16.8c.511.527,1.309,0,1.876,0a.808.808,0,0,0-.255-1.6A3.976,3.976,0,0,0,3.18,15.631a4.319,4.319,0,0,0,1.006-2.666,4.279,4.279,0,0,0-.958-2.594A4.215,4.215,0,0,0,5.3,8.694a4.47,4.47,0,0,0,.487-2.85,4.2,4.2,0,0,0,2.642-.415A4.4,4.4,0,0,0,10.308,3.2a3.991,3.991,0,0,0,5.237,0A3.991,3.991,0,0,0,20.063,5.82a3.991,3.991,0,0,0,2.65,4.526,3.991,3.991,0,0,0-.064,5.213,3.991,3.991,0,0,0-2.5,4.526,3.991,3.991,0,0,0-4.526,2.65,3.991,3.991,0,0,0-5.3,0,.8.8,0,0,0-.87-.192c-.8.319-.431,1.134-.255,1.764a.8.8,0,0,0,1.357.359c1.668-1.78,3.153-1.78,4.79,0a.8.8,0,0,0,1.357-.343,3.688,3.688,0,0,1,1.541-2.459,3.289,3.289,0,0,1,2.618,0,.8.8,0,0,0,1.054-.918c-.631-2.395,0-3.7,2.395-4.2a.8.8,0,0,0,.367-1.389Z" transform = "translate(-0.963 -0.954)" fill = "#0BDA51" /></svg >' :'<svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 24 23.968"><path id = "Layer_2" data - name="Layer 2" d = "M9.1,21.922c-.6.83-2.2-.176-1.716-1.078C7.954,19.8,9.774,20.948,9.1,21.922ZM12.751,7.377a.8.8,0,0,0,0-1.6,7.187,7.187,0,1,0,5.8,2.634.8.8,0,0,0-1.229,1.014,5.588,5.588,0,0,1-4.359,9.124C5.718,18.345,5.511,7.848,12.751,7.377Zm2.81.639a.8.8,0,0,0,1.078-.335c.407-.8-.487-1.221-1.11-1.429a.8.8,0,0,0-1.03.463C14.212,7.5,15,7.768,15.561,8.016Zm9.116,7.344c-1.836-1.644-1.724-3.057,0-4.79a.774.774,0,0,0-.311-1.357c-2.395-.6-3.145-1.82-2.395-4.175a.8.8,0,0,0-1.022-1.022c-2.395.71-3.608,0-4.183-2.395a.8.8,0,0,0-1.4-.375c-1.668,1.78-3.153,1.78-4.79,0a.8.8,0,0,0-1.357.343c-.631,2.395-1.82,3.193-4.167,2.435a.8.8,0,0,0-1.03.958c.647,2.395,0,3.672-2.395,4.191a.823.823,0,0,0-.407,1.4C3,12.262,3,13.7,1.216,15.36a.8.8,0,0,0,.351,1.357A5.588,5.588,0,0,1,3,17.252c.806.455,1.756,1.3,1.03,3.7a.8.8,0,0,0,.16.8c.511.527,1.309,0,1.876,0a.808.808,0,0,0-.255-1.6A3.976,3.976,0,0,0,3.18,15.631a4.319,4.319,0,0,0,1.006-2.666,4.279,4.279,0,0,0-.958-2.594A4.215,4.215,0,0,0,5.3,8.694a4.47,4.47,0,0,0,.487-2.85,4.2,4.2,0,0,0,2.642-.415A4.4,4.4,0,0,0,10.308,3.2a3.991,3.991,0,0,0,5.237,0A3.991,3.991,0,0,0,20.063,5.82a3.991,3.991,0,0,0,2.65,4.526,3.991,3.991,0,0,0-.064,5.213,3.991,3.991,0,0,0-2.5,4.526,3.991,3.991,0,0,0-4.526,2.65,3.991,3.991,0,0,0-5.3,0,.8.8,0,0,0-.87-.192c-.8.319-.431,1.134-.255,1.764a.8.8,0,0,0,1.357.359c1.668-1.78,3.153-1.78,4.79,0a.8.8,0,0,0,1.357-.343,3.688,3.688,0,0,1,1.541-2.459,3.289,3.289,0,0,1,2.618,0,.8.8,0,0,0,1.054-.918c-.631-2.395,0-3.7,2.395-4.2a.8.8,0,0,0,.367-1.389Z" transform = "translate(-0.963 -0.954)" fill = "#0BDA51" /></svg >');
                    }
                    
                }
                if (options && options["buttons"] && options["buttons"].includes(keyOne)) {


                    if (valOne ==1) {
                        $td = pft_table_createRowDataSVG((valOne == "Assign") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"  width="30" height="30" viewBox="0 0 24 24" style="enable-background:new 0 0 24 24;" xml:space="preserve"> <style type="text/css">.st0{fill:#146EBE;}</style><path class="st0" d="M6,6h4V4H4v6h2V6z"/><path class="st0" d="M10,18H6v-4H4v6h6V18z"/><path class="st0" d="M14,6h4v4h2V4h-6V6z"/><path class="st0" d="M14,18h4v-4h2v6h-6V18z"/><path class="st0" d="M12,8.5c-1.9,0-3.5,1.6-3.5,3.5s1.6,3.5,3.5,3.5s3.5-1.6,3.5-3.5S13.9,8.5,12,8.5z"/></svg>' :'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"width="30" height="30" viewBox="0 0 24 24" style="enable-background:new 0 0 24 24;" xml:space="preserve"> <style type="text/css">.st0{fill:#146EBE;}</style><path class="st0" d="M6,6h4V4H4v6h2V6z"/><path class="st0" d="M10,18H6v-4H4v6h6V18z"/><path class="st0" d="M14,6h4v4h2V4h-6V6z"/><path class="st0" d="M14,18h4v-4h2v6h-6V18z"/><path class="st0" d="M12,8.5c-1.9,0-3.5,1.6-3.5,3.5s1.6,3.5,3.5,3.5s3.5-1.6,3.5-3.5S13.9,8.5,12,8.5z"/></svg>');
                    }
                    else if (valOne ==2) {

                        $td = pft_table_createRowDataSVG((valOne == "View") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="30" height="30"viewBox="0 0 576 512" style="enable-background:new 0 0 576 512;" xml:space="preserve"><style type="text/css">.st1{fill:#0d64b5;}</style><path class="st1" d="M279.6,160.4c2.8-0.3,5.6-0.4,8.4-0.4c53,0,96,42.1,96,96c0,53-43,96-96,96c-53.9,0-96-43-96-96c0-2.8,0.1-5.6,0.4-8.4c9.3,4.5,20.1,8.4,31.6,8.4c35.3,0,64-28.7,64-64C288,180.5,284.1,169.7,279.6,160.4z M480.6,112.6c46.8,43.4,78.1,94.5,92.9,131.1c3.3,7.9,3.3,16.7,0,24.6c-14.8,35.7-46.1,86.8-92.9,131.1C433.5,443.2,368.8,480,288,480s-145.5-36.8-192.6-80.6c-46.8-44.3-78.1-95.4-93-131.1c-3.3-7.9-3.3-16.7,0-24.6c14.9-36.6,46.2-87.7,93-131.1C142.5,68.8,207.2,32,288,32S433.5,68.8,480.6,112.6L480.6,112.6z M288,112c-79.5,0-144,64.5-144,144s64.5,144,144,144s144-64.5,144-144S367.5,112,288,112z"/></svg>' :'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"width="30" height="30"viewBox="0 0 576 512" style="enable-background:new 0 0 576 512;" xml:space="preserve"><style type="text/css">.st1{fill:#0d64b5;}</style><path class="st1" d="M279.6,160.4c2.8-0.3,5.6-0.4,8.4-0.4c53,0,96,42.1,96,96c0,53-43,96-96,96c-53.9,0-96-43-96-96c0-2.8,0.1-5.6,0.4-8.4c9.3,4.5,20.1,8.4,31.6,8.4c35.3,0,64-28.7,64-64C288,180.5,284.1,169.7,279.6,160.4z M480.6,112.6c46.8,43.4,78.1,94.5,92.9,131.1c3.3,7.9,3.3,16.7,0,24.6c-14.8,35.7-46.1,86.8-92.9,131.1C433.5,443.2,368.8,480,288,480s-145.5-36.8-192.6-80.6c-46.8-44.3-78.1-95.4-93-131.1c-3.3-7.9-3.3-16.7,0-24.6c14.9-36.6,46.2-87.7,93-131.1C142.5,68.8,207.2,32,288,32S433.5,68.8,480.6,112.6L480.6,112.6z M288,112c-79.5,0-144,64.5-144,144s64.5,144,144,144s144-64.5,144-144S367.5,112,288,112z"/></svg>');
                    }
                    else if (valOne ==0) {
                        $td = pft_table_createRowDataSVG((valOne == "Reassign") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="30" height="30"viewBox="0 0 52 52" style="enable-background:new 0 0 52 52;" xml:space="preserve"><style type="text/css">.st2{fill:#5E6833;}</style><path class="st2" d="M27.3,37.6c-3-1.2-3.5-2.3-3.5-3.5s0.8-2.3,1.8-3.2c1.8-1.5,2.6-3.9,2.6-6.4c0-4.7-2.9-8.5-8.3-8.5s-8.3,3.8-8.3,8.5c0,2.5,0.8,4.9,2.6,6.4c1,0.9,1.8,2,1.8,3.2s-0.5,2.3-3.5,3.5c-4.4,1.8-8.6,3.8-8.7,7.6C4,47.8,6,50,8.5,50h23c2.5,0,4.5-2.2,4.5-4.7C35.9,41.5,31.7,39.4,27.3,37.6z M44.5,19c0-7.4-6.1-13.5-13.5-13.5V2l-6.8,5.5c-0.3,0.3-0.2,0.8,0.1,1.1L31,14v-3.5c4.7,0,8.5,3.8,8.5,8.5H36l5.5,6.8c0.3,0.3,0.8,0.3,1.1,0L48,19C48,19,44.5,19,44.5,19z"/></svg>' : '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="30" height="30"viewBox="0 0 52 52" style="enable-background:new 0 0 52 52;" xml:space="preserve"><style type="text/css">.st2{fill:#5E6833;}</style><path class="st2" d="M27.3,37.6c-3-1.2-3.5-2.3-3.5-3.5s0.8-2.3,1.8-3.2c1.8-1.5,2.6-3.9,2.6-6.4c0-4.7-2.9-8.5-8.3-8.5s-8.3,3.8-8.3,8.5c0,2.5,0.8,4.9,2.6,6.4c1,0.9,1.8,2,1.8,3.2s-0.5,2.3-3.5,3.5c-4.4,1.8-8.6,3.8-8.7,7.6C4,47.8,6,50,8.5,50h23c2.5,0,4.5-2.2,4.5-4.7C35.9,41.5,31.7,39.4,27.3,37.6z M44.5,19c0-7.4-6.1-13.5-13.5-13.5V2l-6.8,5.5c-0.3,0.3-0.2,0.8,0.1,1.1L31,14v-3.5c4.7,0,8.5,3.8,8.5,8.5H36l5.5,6.8c0.3,0.3,0.8,0.3,1.1,0L48,19C48,19,44.5,19,44.5,19z"/></svg>');
                    }

                }

                if (options && options["status"] && options["status"].includes(keyOne)) {


                    if (valOne == "New") {
                        $td = pft_table_createRowDataSVG((valOne == "New") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="20" height="20"viewBox="0 0 576 512" style="enable-background:new 0 0 576 512;" xml:space="preserve"><title id="new">New</title><style type="text/css">.st3{fill:#0389E5;}</style><path class="st3" d="M0,64C0,28.6,28.6,0,64,0h160v128c0,17.7,14.3,32,32,32h128v38.6c-73.9,20.9-128,88.8-128,169.4c0,59.1,29.1,111.3,73.7,143.3c-3.1,0.4-6.4,0.7-9.7,0.7H64c-35.3,0-64-28.7-64-64V64z M256,128V0l128,128H256z M288,368c0-79.5,64.5-144,144-144s144,64.5,144,144s-64.5,144-144,144S288,447.5,288,368z M448,303.1c0-7.9-7.2-16-16-16s-16,8.1-16,16v48h-48c-8.8,0-16,8.1-16,16c0,9.7,7.2,16,16,16h48v48c0,9.7,7.2,16,16,16s16-6.3,16-16v-48h48c8.8,0,16-6.3,16-16c0-7.9-7.2-16-16-16h-48V303.1z"/></svg>' : '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" "width="20" height="20"viewBox="0 0 576 512" style="enable-background:new 0 0 576 512;" xml:space="preserve"><title id="new">New</title><style type="text/css">.st3{fill:#0389E5;}</style><path class="st3" d="M0,64C0,28.6,28.6,0,64,0h160v128c0,17.7,14.3,32,32,32h128v38.6c-73.9,20.9-128,88.8-128,169.4c0,59.1,29.1,111.3,73.7,143.3c-3.1,0.4-6.4,0.7-9.7,0.7H64c-35.3,0-64-28.7-64-64V64z M256,128V0l128,128H256z M288,368c0-79.5,64.5-144,144-144s144,64.5,144,144s-64.5,144-144,144S288,447.5,288,368z M448,303.1c0-7.9-7.2-16-16-16s-16,8.1-16,16v48h-48c-8.8,0-16,8.1-16,16c0,9.7,7.2,16,16,16h48v48c0,9.7,7.2,16,16,16s16-6.3,16-16v-48h48c8.8,0,16-6.3,16-16c0-7.9-7.2-16-16-16h-48V303.1z"/></svg>');
                    }
                    else if (valOne == "On-Hold") {

                        $td = pft_table_createRowDataSVG((valOne == "On-Hold") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="20" height="20" viewBox="0 0 122.88 122.88" style="enable-background:new 0 0 122.88 122.88" xml:space="preserve"><g><title id="onhold">On Hold</title><style type="text/css">.st4{fill:#706c6c;}</style><path class="st4" d="M61.44,0c16.97,0,32.33,6.88,43.44,18c11.12,11.12,18,26.48,18,43.44c0,16.97-6.88,32.33-18,43.44 c-11.12,11.12-26.48,18-43.44,18c-16.97,0-32.33-6.88-43.44-18C6.88,93.77,0,78.41,0,61.44C0,44.47,6.88,29.11,18,18 C29.11,6.88,44.47,0,61.44,0L61.44,0z M42.3,39.47h13.59v43.95l-13.59,0V39.47L42.3,39.47L42.3,39.47z M66.99,39.47h13.59v43.95 l-13.59,0V39.47L66.99,39.47L66.99,39.47z M97.42,25.46c-9.21-9.21-21.93-14.9-35.98-14.9c-14.05,0-26.78,5.7-35.98,14.9 c-9.21,9.21-14.9,21.93-14.9,35.98s5.7,26.78,14.9,35.98c9.21,9.21,21.93,14.9,35.98,14.9c14.05,0,26.78-5.7,35.98-14.9 c9.21-9.21,14.9-21.93,14.9-35.98S106.63,34.66,97.42,25.46L97.42,25.46z"/></g></svg>' : '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"  width="20" height="20" viewBox="0 0 122.88 122.88" style="enable-background:new 0 0 122.88 122.88" xml:space="preserve"><g><title id="onhold">On Hold</title><style type="text/css">.st6{fill:#706c6c;}</style><path class="st6" d="M61.44,0c16.97,0,32.33,6.88,43.44,18c11.12,11.12,18,26.48,18,43.44c0,16.97-6.88,32.33-18,43.44 c-11.12,11.12-26.48,18-43.44,18c-16.97,0-32.33-6.88-43.44-18C6.88,93.77,0,78.41,0,61.44C0,44.47,6.88,29.11,18,18 C29.11,6.88,44.47,0,61.44,0L61.44,0z M42.3,39.47h13.59v43.95l-13.59,0V39.47L42.3,39.47L42.3,39.47z M66.99,39.47h13.59v43.95 l-13.59,0V39.47L66.99,39.47L66.99,39.47z M97.42,25.46c-9.21-9.21-21.93-14.9-35.98-14.9c-14.05,0-26.78,5.7-35.98,14.9 c-9.21,9.21-14.9,21.93-14.9,35.98s5.7,26.78,14.9,35.98c9.21,9.21,21.93,14.9,35.98,14.9c14.05,0,26.78-5.7,35.98-14.9 c9.21-9.21,14.9-21.93,14.9-35.98S106.63,34.66,97.42,25.46L97.42,25.46z"/></g></svg>');
                    }
                    else if (valOne == "Completed") {
                        $td = pft_table_createRowDataSVG((valOne == "Completed") ? '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 512 512"><title id="completed">Completed</title><path fill="#3AAF3C" d="M256 0c141.39 0 256 114.61 256 256S397.39 512 256 512 0 397.39 0 256 114.61 0 256 0z"/><path fill="#0DA10D" fill-rule="nonzero" d="M391.27 143.23h19.23c-81.87 90.92-145.34 165.89-202.18 275.52-29.59-63.26-55.96-106.93-114.96-147.42l22.03-4.98c44.09 36.07 67.31 76.16 92.93 130.95 52.31-100.9 110.24-172.44 182.95-254.07z"/><path fill="#fff" fill-rule="nonzero" d="M158.04 235.26c19.67 11.33 32.46 20.75 47.71 37.55 39.53-63.63 82.44-98.89 138.24-148.93l5.45-2.11h61.06c-81.87 90.93-145.34 165.9-202.18 275.53-29.59-63.26-55.96-106.93-114.96-147.43l64.68-14.61z"/></svg>' : '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 512 512"><title id="completed">Completed</title><path fill="#3AAF3C" d="M256 0c141.39 0 256 114.61 256 256S397.39 512 256 512 0 397.39 0 256 114.61 0 256 0z"/><path fill="#0DA10D" fill-rule="nonzero" d="M391.27 143.23h19.23c-81.87 90.92-145.34 165.89-202.18 275.52-29.59-63.26-55.96-106.93-114.96-147.42l22.03-4.98c44.09 36.07 67.31 76.16 92.93 130.95 52.31-100.9 110.24-172.44 182.95-254.07z"/><path fill="#fff" fill-rule="nonzero" d="M158.04 235.26c19.67 11.33 32.46 20.75 47.71 37.55 39.53-63.63 82.44-98.89 138.24-148.93l5.45-2.11h61.06c-81.87 90.93-145.34 165.9-202.18 275.53-29.59-63.26-55.96-106.93-114.96-147.43l64.68-14.61z"/></svg>');
                    }
                    else if (valOne == "Pending") {
                        $td = pft_table_createRowDataSVG((valOne == "Pending") ? '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 502 511.82"><title id="open">Open</title><style type="text/css">.st6{fill:#FF0000;}</style><path class="st6" fill-rule="nonzero" d="M279.75 471.21c14.34-1.9 25.67 12.12 20.81 25.75-2.54 6.91-8.44 11.76-15.76 12.73a260.727 260.727 0 0 1-50.81 1.54c-62.52-4.21-118.77-31.3-160.44-72.97C28.11 392.82 0 330.04 0 260.71 0 191.37 28.11 128.6 73.55 83.16S181.76 9.61 251.1 9.61c24.04 0 47.47 3.46 69.8 9.91a249.124 249.124 0 0 1 52.61 21.97l-4.95-12.96c-4.13-10.86 1.32-23.01 12.17-27.15 10.86-4.13 23.01 1.32 27.15 12.18L428.8 68.3a21.39 21.39 0 0 1 1.36 6.5c1.64 10.2-4.47 20.31-14.63 23.39l-56.03 17.14c-11.09 3.36-22.8-2.9-26.16-13.98-3.36-11.08 2.9-22.8 13.98-26.16l4.61-1.41a210.71 210.71 0 0 0-41.8-17.12c-18.57-5.36-38.37-8.24-59.03-8.24-58.62 0-111.7 23.76-150.11 62.18-38.42 38.41-62.18 91.48-62.18 150.11 0 58.62 23.76 111.69 62.18 150.11 34.81 34.81 81.66 57.59 133.77 61.55 14.9 1.13 30.23.76 44.99-1.16zm-67.09-312.63c0-10.71 8.69-19.4 19.41-19.4 10.71 0 19.4 8.69 19.4 19.4V276.7l80.85 35.54c9.8 4.31 14.24 15.75 9.93 25.55-4.31 9.79-15.75 14.24-25.55 9.93l-91.46-40.2c-7.35-2.77-12.58-9.86-12.58-18.17V158.58zm134.7 291.89c-15.62 7.99-13.54 30.9 3.29 35.93 4.87 1.38 9.72.96 14.26-1.31 12.52-6.29 24.54-13.7 35.81-22.02 5.5-4.1 8.36-10.56 7.77-17.39-1.5-15.09-18.68-22.74-30.89-13.78a208.144 208.144 0 0 1-30.24 18.57zm79.16-69.55c-8.84 13.18 1.09 30.9 16.97 30.2 6.21-.33 11.77-3.37 15.25-8.57 7.86-11.66 14.65-23.87 20.47-36.67 5.61-12.64-3.13-26.8-16.96-27.39-7.93-.26-15.11 4.17-18.41 11.4-4.93 10.85-10.66 21.15-17.32 31.03zm35.66-99.52c-.7 7.62 3 14.76 9.59 18.63 12.36 7.02 27.6-.84 29.05-14.97 1.33-14.02 1.54-27.9.58-41.95-.48-6.75-4.38-12.7-10.38-15.85-13.46-6.98-29.41 3.46-28.34 18.57.82 11.92.63 23.67-.5 35.57zM446.1 177.02c4.35 10.03 16.02 14.54 25.95 9.96 9.57-4.4 13.86-15.61 9.71-25.29-5.5-12.89-12.12-25.28-19.69-37.08-9.51-14.62-31.89-10.36-35.35 6.75-.95 5.03-.05 9.94 2.72 14.27 6.42 10.02 12 20.44 16.66 31.39z"/></svg>' : '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 502 511.82"><title id="open">Open</title><style type="text/css">.st6{fill:#FF0000;}</style><path class="st6" fill-rule="nonzero" d="M279.75 471.21c14.34-1.9 25.67 12.12 20.81 25.75-2.54 6.91-8.44 11.76-15.76 12.73a260.727 260.727 0 0 1-50.81 1.54c-62.52-4.21-118.77-31.3-160.44-72.97C28.11 392.82 0 330.04 0 260.71 0 191.37 28.11 128.6 73.55 83.16S181.76 9.61 251.1 9.61c24.04 0 47.47 3.46 69.8 9.91a249.124 249.124 0 0 1 52.61 21.97l-4.95-12.96c-4.13-10.86 1.32-23.01 12.17-27.15 10.86-4.13 23.01 1.32 27.15 12.18L428.8 68.3a21.39 21.39 0 0 1 1.36 6.5c1.64 10.2-4.47 20.31-14.63 23.39l-56.03 17.14c-11.09 3.36-22.8-2.9-26.16-13.98-3.36-11.08 2.9-22.8 13.98-26.16l4.61-1.41a210.71 210.71 0 0 0-41.8-17.12c-18.57-5.36-38.37-8.24-59.03-8.24-58.62 0-111.7 23.76-150.11 62.18-38.42 38.41-62.18 91.48-62.18 150.11 0 58.62 23.76 111.69 62.18 150.11 34.81 34.81 81.66 57.59 133.77 61.55 14.9 1.13 30.23.76 44.99-1.16zm-67.09-312.63c0-10.71 8.69-19.4 19.41-19.4 10.71 0 19.4 8.69 19.4 19.4V276.7l80.85 35.54c9.8 4.31 14.24 15.75 9.93 25.55-4.31 9.79-15.75 14.24-25.55 9.93l-91.46-40.2c-7.35-2.77-12.58-9.86-12.58-18.17V158.58zm134.7 291.89c-15.62 7.99-13.54 30.9 3.29 35.93 4.87 1.38 9.72.96 14.26-1.31 12.52-6.29 24.54-13.7 35.81-22.02 5.5-4.1 8.36-10.56 7.77-17.39-1.5-15.09-18.68-22.74-30.89-13.78a208.144 208.144 0 0 1-30.24 18.57zm79.16-69.55c-8.84 13.18 1.09 30.9 16.97 30.2 6.21-.33 11.77-3.37 15.25-8.57 7.86-11.66 14.65-23.87 20.47-36.67 5.61-12.64-3.13-26.8-16.96-27.39-7.93-.26-15.11 4.17-18.41 11.4-4.93 10.85-10.66 21.15-17.32 31.03zm35.66-99.52c-.7 7.62 3 14.76 9.59 18.63 12.36 7.02 27.6-.84 29.05-14.97 1.33-14.02 1.54-27.9.58-41.95-.48-6.75-4.38-12.7-10.38-15.85-13.46-6.98-29.41 3.46-28.34 18.57.82 11.92.63 23.67-.5 35.57zM446.1 177.02c4.35 10.03 16.02 14.54 25.95 9.96 9.57-4.4 13.86-15.61 9.71-25.29-5.5-12.89-12.12-25.28-19.69-37.08-9.51-14.62-31.89-10.36-35.35 6.75-.95 5.03-.05 9.94 2.72 14.27 6.42 10.02 12 20.44 16.66 31.39z"/></svg>');
                    }

                    else if (valOne == "Pick-up Request") {
                        $td = pft_table_createRowDataSVG((valOne == "Pick-up Request") ? '<svg style="color: red" xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-truck" viewBox="0 0 16 16"> <title id="pickup">Pick Up</title><path d="M0 3.5A1.5 1.5 0 0 1 1.5 2h9A1.5 1.5 0 0 1 12 3.5V5h1.02a1.5 1.5 0 0 1 1.17.563l1.481 1.85a1.5 1.5 0 0 1 .329.938V10.5a1.5 1.5 0 0 1-1.5 1.5H14a2 2 0 1 1-4 0H5a2 2 0 1 1-3.998-.085A1.5 1.5 0 0 1 0 10.5v-7zm1.294 7.456A1.999 1.999 0 0 1 4.732 11h5.536a2.01 2.01 0 0 1 .732-.732V3.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5v7a.5.5 0 0 0 .294.456zM12 10a2 2 0 0 1 1.732 1h.768a.5.5 0 0 0 .5-.5V8.35a.5.5 0 0 0-.11-.312l-1.48-1.85A.5.5 0 0 0 13.02 6H12v4zm-9 1a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm9 0a1 1 0 1 0 0 2 1 1 0 0 0 0-2z" fill="red"></path> </svg>' : '< svg style = "color: red" xmlns = "http://www.w3.org/2000/svg" width = "20" height = "20" fill = "currentColor" class= "bi bi-truck" viewBox = "0 0 16 16" > <title id="pickup">Pick Up</title><path d="M0 3.5A1.5 1.5 0 0 1 1.5 2h9A1.5 1.5 0 0 1 12 3.5V5h1.02a1.5 1.5 0 0 1 1.17.563l1.481 1.85a1.5 1.5 0 0 1 .329.938V10.5a1.5 1.5 0 0 1-1.5 1.5H14a2 2 0 1 1-4 0H5a2 2 0 1 1-3.998-.085A1.5 1.5 0 0 1 0 10.5v-7zm1.294 7.456A1.999 1.999 0 0 1 4.732 11h5.536a2.01 2.01 0 0 1 .732-.732V3.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5v7a.5.5 0 0 0 .294.456zM12 10a2 2 0 0 1 1.732 1h.768a.5.5 0 0 0 .5-.5V8.35a.5.5 0 0 0-.11-.312l-1.48-1.85A.5.5 0 0 0 13.02 6H12v4zm-9 1a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm9 0a1 1 0 1 0 0 2 1 1 0 0 0 0-2z" fill="red"></path> </svg >');
                    }

                    else if (valOne == "Standby") {
                        $td = pft_table_createRowDataSVG((valOne == "Standby") ? '<svg style="color: red" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 640 512"><title id="standby">Standby</title><path d="M640 .0003V400C640 461.9 589.9 512 528 512C467 512 417.5 463.3 416 402.7L48.41 502.9C31.36 507.5 13.77 497.5 9.126 480.4C4.48 463.4 14.54 445.8 31.59 441.1L352 353.8V64C352 28.65 380.7 0 416 0L640 .0003zM528 352C501.5 352 480 373.5 480 400C480 426.5 501.5 448 528 448C554.5 448 576 426.5 576 400C576 373.5 554.5 352 528 352zM23.11 207.7C18.54 190.6 28.67 173.1 45.74 168.5L92.1 156.1L112.8 233.4C115.1 241.9 123.9 246.1 132.4 244.7L163.3 236.4C171.8 234.1 176.9 225.3 174.6 216.8L153.9 139.5L200.3 127.1C217.4 122.5 234.9 132.7 239.5 149.7L280.9 304.3C285.5 321.4 275.3 338.9 258.3 343.5L103.7 384.9C86.64 389.5 69.1 379.3 64.52 362.3L23.11 207.7z" fill="red"></path></svg>' : '<svg style="color: red" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512"> <title id="standby">Standby</title> <path d="M640 .0003V400C640 461.9 589.9 512 528 512C467 512 417.5 463.3 416 402.7L48.41 502.9C31.36 507.5 13.77 497.5 9.126 480.4C4.48 463.4 14.54 445.8 31.59 441.1L352 353.8V64C352 28.65 380.7 0 416 0L640 .0003zM528 352C501.5 352 480 373.5 480 400C480 426.5 501.5 448 528 448C554.5 448 576 426.5 576 400C576 373.5 554.5 352 528 352zM23.11 207.7C18.54 190.6 28.67 173.1 45.74 168.5L92.1 156.1L112.8 233.4C115.1 241.9 123.9 246.1 132.4 244.7L163.3 236.4C171.8 234.1 176.9 225.3 174.6 216.8L153.9 139.5L200.3 127.1C217.4 122.5 234.9 132.7 239.5 149.7L280.9 304.3C285.5 321.4 275.3 338.9 258.3 343.5L103.7 384.9C86.64 389.5 69.1 379.3 64.52 362.3L23.11 207.7z" fill="red"></path></svg>');

                    }

                    else if (valOne == "Delivery Request") {
                        $td = pft_table_createRowDataSVG((valOne == "Delivery Request") ? '<svg width="24" height="24" stroke-width="1.5" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><title id="deliveryrequest">Delivery Request</title><path d="M8 19C9.10457 19 10 18.1046 10 17C10 15.8954 9.10457 15 8 15C6.89543 15 6 15.8954 6 17C6 18.1046 6.89543 19 8 19Z" stroke="currentColor" stroke-miterlimit="1.5" stroke-linecap="round" stroke-linejoin="round"/> <path d="M18 19C19.1046 19 20 18.1046 20 17C20 15.8954 19.1046 15 18 15C16.8954 15 16 15.8954 16 17C16 18.1046 16.8954 19 18 19Z" stroke="currentColor" stroke-miterlimit="1.5" stroke-linecap="round" stroke-linejoin="round"/> <path d="M10.05 17H15V6.6C15 6.26863 14.7314 6 14.4 6H1" stroke="currentColor" stroke-linecap="round"/> <path d="M5.65 17H3.6C3.26863 17 3 16.7314 3 16.4V11.5" stroke="currentColor" stroke-linecap="round"/> <path d="M2 9L6 9" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round"/> <path d="M15 9H20.6101C20.8472 9 21.0621 9.13964 21.1584 9.35632L22.9483 13.3836C22.9824 13.4604 23 13.5434 23 13.6273V16.4C23 16.7314 22.7314 17 22.4 17H20.5" stroke="currentColor" stroke-linecap="round"/> <path d="M15 17H16" stroke="currentColor" stroke-linecap="round"/> </svg>' : '<svg width="24" height="24" stroke-width="1.5" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><title id="deliveryrequest">Delivery Request</title><path d="M8 19C9.10457 19 10 18.1046 10 17C10 15.8954 9.10457 15 8 15C6.89543 15 6 15.8954 6 17C6 18.1046 6.89543 19 8 19Z" stroke="currentColor" stroke-miterlimit="1.5" stroke-linecap="round" stroke-linejoin="round"/> <path d="M18 19C19.1046 19 20 18.1046 20 17C20 15.8954 19.1046 15 18 15C16.8954 15 16 15.8954 16 17C16 18.1046 16.8954 19 18 19Z" stroke="currentColor" stroke-miterlimit="1.5" stroke-linecap="round" stroke-linejoin="round"/> <path d="M10.05 17H15V6.6C15 6.26863 14.7314 6 14.4 6H1" stroke="currentColor" stroke-linecap="round"/> <path d="M5.65 17H3.6C3.26863 17 3 16.7314 3 16.4V11.5" stroke="currentColor" stroke-linecap="round"/> <path d="M2 9L6 9" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round"/> <path d="M15 9H20.6101C20.8472 9 21.0621 9.13964 21.1584 9.35632L22.9483 13.3836C22.9824 13.4604 23 13.5434 23 13.6273V16.4C23 16.7314 22.7314 17 22.4 17H20.5" stroke="currentColor" stroke-linecap="round"/> <path d="M15 17H16" stroke="currentColor" stroke-linecap="round"/> </svg>');
                    }


                }

                if (options && options["priority"] && options["priority"].includes(keyOne)) {


                    if (valOne == "Normal") {
                        //$td = pft_table_createRowDataSVG((valOne == "Normal") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="low">Normal</title><style type="text/css">.st7{fill:#e94560;}</style><g><path class="st7" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0C95.37,0,122.88,27.51,122.88,61.441S95.37,122.88,61.438,122.88 C27.509,122.88,0,95.373,0,61.441S27.509,0,61.438,0L61.438,0z M61.438,18.382c23.781,0,43.06,19.278,43.06,43.06 s-19.278,43.057-43.06,43.057c-23.779,0-43.057-19.275-43.057-43.057S37.66,18.382,61.438,18.382L61.438,18.382z"/></g></svg>' :'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="low">Low</title><style type="text/css">.st7{fill:#e94560;}</style><g><path class="st7" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0');
                        $td = pft_table_createRowDataSVG((valOne == "Normal") ? '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 512 345.5"><title id="low">Normal</title><style type="text/css">.st7{fill:#1d56d1;}</style><g><path class="st7" fill-rule="nonzero" d="M240.74 337.81 3.95 30.57C-2.44 22.3-.92 10.4 7.36 4.01A18.9 18.9 0 0 1 18.93.06V0H493c10.49 0 19 8.51 19 19 0 4.91-1.86 9.38-4.92 12.75L270.97 338.14c-6.39 8.28-18.29 9.8-26.56 3.41-1.43-1.1-2.65-2.36-3.67-3.74zM57.49 38l198.49 257.54L454.46 38H57.49z"/></svg>' :'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="low">Low</title><style type="text/css">.st7{fill:#1d56d1;}</style><g><path class="st7" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0');
                    }
                    else if (valOne == "High") {

                        //$td = pft_table_createRowDataSVG((valOne == "High") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"  width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="high">High</title><style type="text/css">.st8{fill:#e94560;}</style><g><path class="st8" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0c33.93,0,61.441,27.512,61.441,61.441 c0,33.929-27.512,61.438-61.441,61.438C27.512,122.88,0,95.37,0,61.441C0,27.512,27.512,0,61.438,0L61.438,0z"/></g></svg>' :'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"  width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="hide">High</title><style type="text/css">.st8{fill:#e94560;}</style><g><path class="st8" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0c33.93,0,61.441,27.512,61.441,61.441 c0,33.929-27.512,61.438-61.441,61.438C27.512,122.88,0,95.37,0,61.441C0,27.512,27.512,0,61.438,0L61.438,0z"/></g></svg>');
                        $td = pft_table_createRowDataSVG((valOne == "High") ? '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 512 345.5"><title id="high">High</title><style type="text/css">.st8{fill:#eb1a1a;}</style><g><path class="st8" fill-rule="nonzero" d="M240.74 7.7 3.95 314.93c-6.39 8.27-4.87 20.17 3.41 26.56 3.45 2.67 7.53 3.95 11.57 3.95v.06H493c10.49 0 19-8.51 19-18.99 0-4.91-1.86-9.39-4.92-12.76L270.97 7.36c-6.39-8.27-18.29-9.8-26.56-3.41-1.43 1.1-2.65 2.36-3.67 3.75zM57.49 307.51 255.98 49.96l198.48 257.55H57.49z"/></g></svg>' :'<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 512 345.5"><title id="high">High</title><style type="text/css">.st8{fill:#eb1a1a;}</style><g><path class="st8" fill-rule="nonzero" d="M240.74 7.7 3.95 314.93c-6.39 8.27-4.87 20.17 3.41 26.56 3.45 2.67 7.53 3.95 11.57 3.95v.06H493c10.49 0 19-8.51 19-18.99 0-4.91-1.86-9.39-4.92-12.76L270.97 7.36c-6.39-8.27-18.29-9.8-26.56-3.41-1.43 1.1-2.65 2.36-3.67 3.75zM57.49 307.51 255.98 49.96l198.48 257.55H57.49z"/></g></svg>');
                    }
                    else if (valOne == "Medium") {
                        //$td = pft_table_createRowDataSVG((valOne == "Medium") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="medium">Medium</title><style type="text/css">.st9{fill:#e94560;}</style><g><path class="st9" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0c33.938,0,61.442,27.509,61.442,61.442S95.375,122.88,61.438,122.88 C27.509,122.88,0,95.376,0,61.442S27.509,0,61.438,0L61.438,0z M61.442,43.027c10.17,0,18.413,8.245,18.413,18.416 c0,10.17-8.243,18.413-18.413,18.413c-10.171,0-18.416-8.243-18.416-18.413C43.026,51.272,51.271,43.027,61.442,43.027 L61.442,43.027z M61.438,18.389c23.778,0,43.054,19.279,43.054,43.054s-19.275,43.049-43.054,43.049 c-23.77,0-43.049-19.274-43.049-43.049S37.668,18.389,61.438,18.389L61.438,18.389z"/></g></svg>' :'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="medium">Medium</title><style type="text/css">.st9{fill:#e94560;}</style><g><path class="st9" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0c33.938,0,61.442,27.509,61.442,61.442S95.375,122.88,61.438,122.88 C27.509,122.88,0,95.376,0,61.442S27.509,0,61.438,0L61.438,0z M61.442,43.027c10.17,0,18.413,8.245,18.413,18.416 c0,10.17-8.243,18.413-18.413,18.413c-10.171,0-18.416-8.243-18.416-18.413C43.026,51.272,51.271,43.027,61.442,43.027 L61.442,43.027z M61.438,18.389c23.778,0,43.054,19.279,43.054,43.054s-19.275,43.049-43.054,43.049 c-23.77,0-43.049-19.274-43.049-43.049S37.668,18.389,61.438,18.389L61.438,18.389z"/></g></svg>');
                        $td = pft_table_createRowDataSVG((valOne == "Medium") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="medium">Medium</title><style type="text/css">.st9{fill:#29d646;}</style><g><path class="st9" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0C95.37,0,122.88,27.51,122.88,61.441S95.37,122.88,61.438,122.88 C27.509,122.88,0,95.373,0,61.441S27.509,0,61.438,0L61.438,0z M61.438,18.382c23.781,0,43.06,19.278,43.06,43.06 s-19.278,43.057-43.06,43.057c-23.779,0-43.057-19.275-43.057-43.057S37.66,18.382,61.438,18.382L61.438,18.382z"/></g></svg>' :'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="20" height="20" viewBox="0 0 122.88 122.88" enable-background="new 0 0 122.88 122.88" xml:space="preserve"><title id="medium">Medium</title><style type="text/css">.st9{fill:#29d646;}</style><g><path class="st9" fill-rule="evenodd" clip-rule="evenodd" d="M61.438,0C95.37,0,122.88,27.51,122.88,61.441S95.37,122.88,61.438,122.88 C27.509,122.88,0,95.373,0,61.441S27.509,0,61.438,0L61.438,0z M61.438,18.382c23.781,0,43.06,19.278,43.06,43.06 s-19.278,43.057-43.06,43.057c-23.779,0-43.057-19.275-43.057-43.057S37.66,18.382,61.438,18.382L61.438,18.382z"/></g></svg>');
                    }

                }

                if (options && options["Channel"] && options["Channel"].includes(keyOne)) {


                    if (valOne == "Portal") {
                        $td = pft_table_createRowDataSVG((valOne == "Low") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="20" height="20" viewBox="0 0 122.88 105.37" style="enable-background:new 0 0 122.88 105.37" xml:space="preserve"><title id="Portal">Portal</title><style type="text/css">.st10{fill:#0b4199;}</style><g><path class="st10" d="M72.81,86.58c-3.83,4.63-8.47,9.22-13.89,13.77c1.17-0.16,2.34-0.35,3.48-0.57c3.13-0.63,6.13-1.55,9.04-2.81 c1.48-0.64,2.92-1.33,4.3-2.08l0.11,0.32c0.53,1.51,1.23,2.76,2.06,3.76c-1.52,0.82-3.08,1.58-4.68,2.26 c-3.22,1.39-6.54,2.4-9.96,3.1c-3.41,0.7-6.95,1.04-10.59,1.04s-7.14-0.35-10.59-1.04c-3.38-0.66-6.64-1.67-9.83-3.03 c-0.03,0-0.09-0.03-0.13-0.06c-3.16-1.36-6.16-3-8.98-4.87c-2.81-1.86-5.4-4.01-7.78-6.38c-2.4-2.37-4.52-4.96-6.38-7.78 c-1.9-2.81-3.51-5.82-4.87-8.98c-1.39-3.22-2.4-6.54-3.1-9.96C0.35,59.86,0,56.32,0,52.69c0-3.63,0.35-7.14,1.04-10.59 c0.66-3.38,1.68-6.64,3.03-9.83c0-0.03,0.03-0.09,0.06-0.13c1.36-3.19,3-6.16,4.87-8.98c1.86-2.81,4.01-5.4,6.38-7.78 c2.37-2.4,4.96-4.52,7.78-6.38c2.81-1.9,5.82-3.51,8.98-4.87c3.22-1.39,6.54-2.4,9.96-3.1C45.51,0.35,49.05,0,52.69,0 s7.14,0.35,10.59,1.04c3.38,0.66,6.64,1.67,9.83,3.03c0.03,0,0.09,0.03,0.13,0.06c3.16,1.36,6.16,3,8.98,4.87 c2.81,1.86,5.4,4.01,7.78,6.38c2.4,2.37,4.52,4.96,6.38,7.78c1.9,2.81,3.51,5.82,4.87,8.98c1.39,3.22,2.4,6.54,3.1,9.96 c0.04,0.2,0.08,0.4,0.12,0.6l-5.11-1.8c-0.59-2.37-1.36-4.67-2.34-6.92c-0.79-1.86-1.68-3.63-2.65-5.34l0,0H78.92 c1.21,2.07,2.27,4.13,3.18,6.19l-6.2-2.18c-0.73-1.33-1.52-2.67-2.38-4.01h-9.03c-1.06-0.33-2.2-0.41-3.31-0.24 c-0.35,0.06-0.7,0.14-1.05,0.24h-5.14v21.74h5.07l1.62,4.61h-6.69v21.74h14.35l1.62,4.61H54.99V97.6 C61.5,92.26,66.84,86.86,71,81.46L72.81,86.58L72.81,86.58z M112.3,92.8c-0.77,0.69-1.96,0.7-2.66,0L95.66,78.9l-6.63,13.19 c-2,3.98-5.56,5.78-7.24,1.02L61.68,36c-0.41-1.17,0.08-1.64,1.24-1.23l57.1,20.1c4.76,1.68,2.95,5.24-1.02,7.24l-13.19,6.63 l13.9,13.97c0.69,0.7,0.69,1.9,0,2.66L112.3,92.8L112.3,92.8L112.3,92.8z M46.49,100.35c-7.46-6.26-13.43-12.58-17.86-18.99H14.06 c1.39,1.9,2.94,3.67,4.61,5.34c2.18,2.18,4.52,4.11,7.08,5.82c2.53,1.71,5.28,3.19,8.25,4.46C34.04,97,34.07,97,34.1,97.03 c2.88,1.2,5.85,2.15,8.94,2.75c1.14,0.22,2.31,0.41,3.48,0.57H46.49L46.49,100.35z M11.03,76.74H25.7 c-4.08-7.14-6.29-14.41-6.54-21.74H4.68c0.13,2.53,0.41,4.96,0.89,7.36c0.63,3.13,1.55,6.13,2.81,9.04 C9.17,73.26,10.05,75.03,11.03,76.74L11.03,76.74z M4.68,50.38h14.57c0.54-7.21,2.94-14.44,7.21-21.74H11.03 c-0.98,1.71-1.86,3.48-2.65,5.34c-0.03,0.03-0.03,0.06-0.06,0.09c-1.2,2.88-2.15,5.85-2.75,8.94C5.09,45.42,4.77,47.85,4.68,50.38 L4.68,50.38z M14.03,24.02h15.39C33.82,17.7,39.6,11.35,46.81,4.96c-1.3,0.16-2.56,0.35-3.79,0.6c-3.13,0.63-6.13,1.55-9.04,2.81 c-2.94,1.26-5.69,2.75-8.25,4.46c-2.56,1.71-4.9,3.63-7.08,5.82c-1.67,1.68-3.22,3.44-4.61,5.34V24.02L14.03,24.02z M58.57,4.96 c7.24,6.38,13.02,12.74,17.38,19.06h15.39c-1.39-1.9-2.94-3.67-4.61-5.34c-2.18-2.18-4.52-4.11-7.08-5.82 c-2.53-1.71-5.28-3.19-8.25-4.46c-0.03-0.03-0.06-0.03-0.09-0.06c-2.88-1.2-5.85-2.15-8.94-2.75c-1.26-0.25-2.53-0.44-3.79-0.6 V4.96L58.57,4.96z M54.99,7.96v16.06h15.3C66.28,18.71,61.19,13.37,54.99,7.96L54.99,7.96z M50.38,97.6V81.35H34.29 C38.43,86.79,43.81,92.23,50.38,97.6L50.38,97.6z M50.38,76.74V54.99H23.77c0.28,7.3,2.75,14.54,7.3,21.74H50.38L50.38,76.74z M50.38,50.38V28.64H31.86c-4.74,7.36-7.4,14.6-8,21.74H50.38L50.38,50.38z M50.38,24.02V7.96c-6.19,5.4-11.28,10.75-15.3,16.06 H50.38L50.38,24.02z"/></g></svg>' : '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="20" height="20" viewBox="0 0 122.88 105.37" style="enable-background:new 0 0 122.88 105.37" xml:space="preserve"><title id="Portal">Portal</title><style type="text/css">.st10{fill:#0b4199;}</style><g><path class="st10" d="M72.81,86.58c-3.83,4.63-8.47,9.22-13.89,13.77c1.17-0.16,2.34-0.35,3.48-0.57c3.13-0.63,6.13-1.55,9.04-2.81 c1.48-0.64,2.92-1.33,4.3-2.08l0.11,0.32c0.53,1.51,1.23,2.76,2.06,3.76c-1.52,0.82-3.08,1.58-4.68,2.26 c-3.22,1.39-6.54,2.4-9.96,3.1c-3.41,0.7-6.95,1.04-10.59,1.04s-7.14-0.35-10.59-1.04c-3.38-0.66-6.64-1.67-9.83-3.03 c-0.03,0-0.09-0.03-0.13-0.06c-3.16-1.36-6.16-3-8.98-4.87c-2.81-1.86-5.4-4.01-7.78-6.38c-2.4-2.37-4.52-4.96-6.38-7.78 c-1.9-2.81-3.51-5.82-4.87-8.98c-1.39-3.22-2.4-6.54-3.1-9.96C0.35,59.86,0,56.32,0,52.69c0-3.63,0.35-7.14,1.04-10.59 c0.66-3.38,1.68-6.64,3.03-9.83c0-0.03,0.03-0.09,0.06-0.13c1.36-3.19,3-6.16,4.87-8.98c1.86-2.81,4.01-5.4,6.38-7.78 c2.37-2.4,4.96-4.52,7.78-6.38c2.81-1.9,5.82-3.51,8.98-4.87c3.22-1.39,6.54-2.4,9.96-3.1C45.51,0.35,49.05,0,52.69,0 s7.14,0.35,10.59,1.04c3.38,0.66,6.64,1.67,9.83,3.03c0.03,0,0.09,0.03,0.13,0.06c3.16,1.36,6.16,3,8.98,4.87 c2.81,1.86,5.4,4.01,7.78,6.38c2.4,2.37,4.52,4.96,6.38,7.78c1.9,2.81,3.51,5.82,4.87,8.98c1.39,3.22,2.4,6.54,3.1,9.96 c0.04,0.2,0.08,0.4,0.12,0.6l-5.11-1.8c-0.59-2.37-1.36-4.67-2.34-6.92c-0.79-1.86-1.68-3.63-2.65-5.34l0,0H78.92 c1.21,2.07,2.27,4.13,3.18,6.19l-6.2-2.18c-0.73-1.33-1.52-2.67-2.38-4.01h-9.03c-1.06-0.33-2.2-0.41-3.31-0.24 c-0.35,0.06-0.7,0.14-1.05,0.24h-5.14v21.74h5.07l1.62,4.61h-6.69v21.74h14.35l1.62,4.61H54.99V97.6 C61.5,92.26,66.84,86.86,71,81.46L72.81,86.58L72.81,86.58z M112.3,92.8c-0.77,0.69-1.96,0.7-2.66,0L95.66,78.9l-6.63,13.19 c-2,3.98-5.56,5.78-7.24,1.02L61.68,36c-0.41-1.17,0.08-1.64,1.24-1.23l57.1,20.1c4.76,1.68,2.95,5.24-1.02,7.24l-13.19,6.63 l13.9,13.97c0.69,0.7,0.69,1.9,0,2.66L112.3,92.8L112.3,92.8L112.3,92.8z M46.49,100.35c-7.46-6.26-13.43-12.58-17.86-18.99H14.06 c1.39,1.9,2.94,3.67,4.61,5.34c2.18,2.18,4.52,4.11,7.08,5.82c2.53,1.71,5.28,3.19,8.25,4.46C34.04,97,34.07,97,34.1,97.03 c2.88,1.2,5.85,2.15,8.94,2.75c1.14,0.22,2.31,0.41,3.48,0.57H46.49L46.49,100.35z M11.03,76.74H25.7 c-4.08-7.14-6.29-14.41-6.54-21.74H4.68c0.13,2.53,0.41,4.96,0.89,7.36c0.63,3.13,1.55,6.13,2.81,9.04 C9.17,73.26,10.05,75.03,11.03,76.74L11.03,76.74z M4.68,50.38h14.57c0.54-7.21,2.94-14.44,7.21-21.74H11.03 c-0.98,1.71-1.86,3.48-2.65,5.34c-0.03,0.03-0.03,0.06-0.06,0.09c-1.2,2.88-2.15,5.85-2.75,8.94C5.09,45.42,4.77,47.85,4.68,50.38 L4.68,50.38z M14.03,24.02h15.39C33.82,17.7,39.6,11.35,46.81,4.96c-1.3,0.16-2.56,0.35-3.79,0.6c-3.13,0.63-6.13,1.55-9.04,2.81 c-2.94,1.26-5.69,2.75-8.25,4.46c-2.56,1.71-4.9,3.63-7.08,5.82c-1.67,1.68-3.22,3.44-4.61,5.34V24.02L14.03,24.02z M58.57,4.96 c7.24,6.38,13.02,12.74,17.38,19.06h15.39c-1.39-1.9-2.94-3.67-4.61-5.34c-2.18-2.18-4.52-4.11-7.08-5.82 c-2.53-1.71-5.28-3.19-8.25-4.46c-0.03-0.03-0.06-0.03-0.09-0.06c-2.88-1.2-5.85-2.15-8.94-2.75c-1.26-0.25-2.53-0.44-3.79-0.6 V4.96L58.57,4.96z M54.99,7.96v16.06h15.3C66.28,18.71,61.19,13.37,54.99,7.96L54.99,7.96z M50.38,97.6V81.35H34.29 C38.43,86.79,43.81,92.23,50.38,97.6L50.38,97.6z M50.38,76.74V54.99H23.77c0.28,7.3,2.75,14.54,7.3,21.74H50.38L50.38,76.74z M50.38,50.38V28.64H31.86c-4.74,7.36-7.4,14.6-8,21.74H50.38L50.38,50.38z M50.38,24.02V7.96c-6.19,5.4-11.28,10.75-15.3,16.06 H50.38L50.38,24.02z"/></g></svg>');
                    }
                    else if (valOne == "Direct") {

                        $td = pft_table_createRowDataSVG((valOne == "Direct") ? '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 491 512.35"><title id="Direct">Direct</title><style type="text/css">.st11{fill:#e94560;}</style><path class="st11" d="M160.37 285.48c31.28 0 59.63 12.71 80.16 33.22 20.57 20.52 33.27 48.89 33.27 80.22 0 16.13-3.37 31.48-9.44 45.38 29.87-3.3 58.67-13.92 83.47-30.72 35.28-23.9 62.52-60.39 73.31-106.13.81-3.44 1.52-6.91 2.15-10.4l-25.54.54c-3.11-.08-5.36-1.09-6.69-3-3.65-5.23 1.11-10.56 4.34-14.33 9.23-10.62 33.1-43.39 37.62-49a7.795 7.795 0 0 1 6.02-3.08c2.36-.04 4.63 1 6.14 2.82 4.96 5.56 31.41 38.39 40.65 48.33 3.2 3.44 7.17 8.19 4.01 13.14-1.23 1.98-3.43 3.08-6.55 3.29l-24.45.53c-.95 6.59-2.15 12.96-3.6 19.11-12.91 54.75-45.54 98.43-87.82 127.08-37.44 25.35-82.49 38.88-128.1 37.84-20.43 19.81-48.27 32.03-78.95 32.03-31.29 0-59.63-12.71-80.16-33.22-20.57-20.52-33.28-48.9-33.28-80.21 0-31.33 12.71-59.7 33.22-80.22 20.52-20.51 48.89-33.22 80.22-33.22zm76.99 154.26c-14.63 27.54-43.61 46.3-76.98 46.3-33.39 0-62.38-18.78-77.01-46.36 1.83-1.2 3.91-2.3 5.63-3.24 11.65-6.49 38.87-6.01 48.41-14.6l11.55 34.63 1.35 2.52 5.16-14.7-3.48-3.81c-2.63-3.84-1.72-8.18 3.13-8.97 1.64-.26 3.48-.1 5.27-.1 1.88 0 3.99-.18 5.77.22 4.52 1 4.98 5.37 2.74 8.85l-3.49 3.81 4.94 14.07.99-1.89 13.72-34.63c8.6 7.75 38.92 9.3 48.4 14.6l3.9 3.3zm-95.94-23.87c-3.83-3.39-6.84-5.86-7.49-12.91l-.41.01c-.95-.01-1.86-.22-2.71-.71-1.37-.78-2.34-2.12-2.99-3.63-1.37-3.16-2.46-11.48 1-13.84l-.72-1.35c-.13-1.68-.17-3.7-.19-5.84-.14-7.8-.3-17.28-6.58-19.18l-2.71-.82 1.8-2.19c5.06-6.27 10.37-11.77 15.74-15.97 6.04-4.78 12.19-7.95 18.21-8.87 6.19-.94 12.2.49 17.73 4.96 1.63 1.33 3.23 2.92 4.75 4.78 5.92.57 10.74 3.75 14.19 8.3 2.06 2.7 3.62 5.91 4.62 9.32 1 3.39 1.43 7.01 1.24 10.57-.33 6.37-2.66 12.59-7.36 17.13.83.03 1.6.22 2.29.58 2.62 1.41 2.71 4.45 2.01 7.02-.68 2.13-1.53 4.61-2.35 6.68-1 2.81-2.44 3.34-5.25 3.03-.14 6.95-3.34 8.86-7.66 12.93-3.01 8.86-33.62 8.65-37.16 0zm280.31-253.78c-16.12 22.91-42.75 37.88-72.88 37.88-30.58 0-57.55-15.42-73.59-38.9 5.34-19.37 26.78-15.39 46.38-27.93 7.59 20.42 45.47 20.46 53.99 0 19.91 12.73 35.6 10.42 46.1 28.95zM31.11 239.53l.12-1.25c7.46-61.67 38.98-111.29 82.34-144.24 40.16-30.54 90.54-46.65 141.32-44.7 3.97-5.78 8.45-11.18 13.39-16.12C288.8 12.71 317.17 0 348.5 0c31.28 0 59.63 12.71 80.16 33.22 20.57 20.52 33.27 48.89 33.27 80.22 0 31.31-12.7 59.69-33.22 80.21l-.79.73c-20.47 20.08-48.51 32.49-79.42 32.49-31.29 0-59.63-12.71-80.17-33.22-20.56-20.52-33.27-48.9-33.27-80.21 0-10.09 1.32-19.86 3.79-29.17-37.65 1.6-74.3 14.84-104.2 37.57-35.53 27-61.53 67.55-68.3 118l23.61.22c2.99.17 5.11 1.2 6.34 3.09 3.36 5.13-1.37 10.12-4.58 13.64-9.21 9.95-33.13 40.78-37.64 46.04a7.453 7.453 0 0 1-5.87 2.78 7.405 7.405 0 0 1-5.83-2.89c-4.61-5.49-29.08-37.85-37.68-47.68-2.98-3.4-6.65-8.08-3.47-12.76 1.24-1.86 3.39-2.86 6.39-2.97l23.49.22zM348.5 13.13c55.4 0 100.31 44.91 100.31 100.31 0 55.39-44.91 100.3-100.31 100.3-55.4 0-100.32-44.91-100.32-100.3 0-55.4 44.92-100.31 100.32-100.31zm-19.61 118.59c-.3-.54.97-4 1.29-4.55-3.72-3.31-6.66-6.64-7.29-13.52l-.4.01c-.91-.01-1.81-.22-2.63-.7-1.34-.75-2.27-2.06-2.91-3.52-1.34-3.08-2.4-11.18.98-13.5l-.64-.41-.07-.9c-.13-1.63-.16-3.6-.19-5.67-.12-7.61-.28-14.32-6.4-16.17L308 72l1.73-2.14c4.95-6.12 10.12-11.46 15.33-15.56 5.89-4.65 11.89-7.73 17.75-8.63 6.02-.91 11.87.47 17.26 4.83 1.59 1.29 3.14 2.83 4.63 4.65 5.76.55 10.46 3.66 13.82 8.08 2.01 2.64 3.53 5.76 4.5 9.08.97 3.31 1.38 6.83 1.21 10.3-.33 6.2-2.6 9.74-7.17 14.16.8.03 1.56.22 2.23.57 2.55 1.37 2.63 4.34 1.96 6.83-.66 2.07-1.5 4.48-2.3 6.51-.96 2.74-2.38 3.25-5.1 2.95-.14 6.76-3.26 10.08-7.47 14.05l.95 3.33c-2.65 12.87-30.83 14.27-38.44.71zM160.37 298.61c55.4 0 100.31 44.91 100.31 100.31 0 55.39-44.91 100.3-100.31 100.3-55.4 0-100.31-44.91-100.31-100.3 0-55.4 44.91-100.31 100.31-100.31z"/></svg>' : '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 491 512.35"><title id="Direct">Direct</title><style type="text/css">.st11{fill:#e94560;}</style><path class="st11" d="M160.37 285.48c31.28 0 59.63 12.71 80.16 33.22 20.57 20.52 33.27 48.89 33.27 80.22 0 16.13-3.37 31.48-9.44 45.38 29.87-3.3 58.67-13.92 83.47-30.72 35.28-23.9 62.52-60.39 73.31-106.13.81-3.44 1.52-6.91 2.15-10.4l-25.54.54c-3.11-.08-5.36-1.09-6.69-3-3.65-5.23 1.11-10.56 4.34-14.33 9.23-10.62 33.1-43.39 37.62-49a7.795 7.795 0 0 1 6.02-3.08c2.36-.04 4.63 1 6.14 2.82 4.96 5.56 31.41 38.39 40.65 48.33 3.2 3.44 7.17 8.19 4.01 13.14-1.23 1.98-3.43 3.08-6.55 3.29l-24.45.53c-.95 6.59-2.15 12.96-3.6 19.11-12.91 54.75-45.54 98.43-87.82 127.08-37.44 25.35-82.49 38.88-128.1 37.84-20.43 19.81-48.27 32.03-78.95 32.03-31.29 0-59.63-12.71-80.16-33.22-20.57-20.52-33.28-48.9-33.28-80.21 0-31.33 12.71-59.7 33.22-80.22 20.52-20.51 48.89-33.22 80.22-33.22zm76.99 154.26c-14.63 27.54-43.61 46.3-76.98 46.3-33.39 0-62.38-18.78-77.01-46.36 1.83-1.2 3.91-2.3 5.63-3.24 11.65-6.49 38.87-6.01 48.41-14.6l11.55 34.63 1.35 2.52 5.16-14.7-3.48-3.81c-2.63-3.84-1.72-8.18 3.13-8.97 1.64-.26 3.48-.1 5.27-.1 1.88 0 3.99-.18 5.77.22 4.52 1 4.98 5.37 2.74 8.85l-3.49 3.81 4.94 14.07.99-1.89 13.72-34.63c8.6 7.75 38.92 9.3 48.4 14.6l3.9 3.3zm-95.94-23.87c-3.83-3.39-6.84-5.86-7.49-12.91l-.41.01c-.95-.01-1.86-.22-2.71-.71-1.37-.78-2.34-2.12-2.99-3.63-1.37-3.16-2.46-11.48 1-13.84l-.72-1.35c-.13-1.68-.17-3.7-.19-5.84-.14-7.8-.3-17.28-6.58-19.18l-2.71-.82 1.8-2.19c5.06-6.27 10.37-11.77 15.74-15.97 6.04-4.78 12.19-7.95 18.21-8.87 6.19-.94 12.2.49 17.73 4.96 1.63 1.33 3.23 2.92 4.75 4.78 5.92.57 10.74 3.75 14.19 8.3 2.06 2.7 3.62 5.91 4.62 9.32 1 3.39 1.43 7.01 1.24 10.57-.33 6.37-2.66 12.59-7.36 17.13.83.03 1.6.22 2.29.58 2.62 1.41 2.71 4.45 2.01 7.02-.68 2.13-1.53 4.61-2.35 6.68-1 2.81-2.44 3.34-5.25 3.03-.14 6.95-3.34 8.86-7.66 12.93-3.01 8.86-33.62 8.65-37.16 0zm280.31-253.78c-16.12 22.91-42.75 37.88-72.88 37.88-30.58 0-57.55-15.42-73.59-38.9 5.34-19.37 26.78-15.39 46.38-27.93 7.59 20.42 45.47 20.46 53.99 0 19.91 12.73 35.6 10.42 46.1 28.95zM31.11 239.53l.12-1.25c7.46-61.67 38.98-111.29 82.34-144.24 40.16-30.54 90.54-46.65 141.32-44.7 3.97-5.78 8.45-11.18 13.39-16.12C288.8 12.71 317.17 0 348.5 0c31.28 0 59.63 12.71 80.16 33.22 20.57 20.52 33.27 48.89 33.27 80.22 0 31.31-12.7 59.69-33.22 80.21l-.79.73c-20.47 20.08-48.51 32.49-79.42 32.49-31.29 0-59.63-12.71-80.17-33.22-20.56-20.52-33.27-48.9-33.27-80.21 0-10.09 1.32-19.86 3.79-29.17-37.65 1.6-74.3 14.84-104.2 37.57-35.53 27-61.53 67.55-68.3 118l23.61.22c2.99.17 5.11 1.2 6.34 3.09 3.36 5.13-1.37 10.12-4.58 13.64-9.21 9.95-33.13 40.78-37.64 46.04a7.453 7.453 0 0 1-5.87 2.78 7.405 7.405 0 0 1-5.83-2.89c-4.61-5.49-29.08-37.85-37.68-47.68-2.98-3.4-6.65-8.08-3.47-12.76 1.24-1.86 3.39-2.86 6.39-2.97l23.49.22zM348.5 13.13c55.4 0 100.31 44.91 100.31 100.31 0 55.39-44.91 100.3-100.31 100.3-55.4 0-100.32-44.91-100.32-100.3 0-55.4 44.92-100.31 100.32-100.31zm-19.61 118.59c-.3-.54.97-4 1.29-4.55-3.72-3.31-6.66-6.64-7.29-13.52l-.4.01c-.91-.01-1.81-.22-2.63-.7-1.34-.75-2.27-2.06-2.91-3.52-1.34-3.08-2.4-11.18.98-13.5l-.64-.41-.07-.9c-.13-1.63-.16-3.6-.19-5.67-.12-7.61-.28-14.32-6.4-16.17L308 72l1.73-2.14c4.95-6.12 10.12-11.46 15.33-15.56 5.89-4.65 11.89-7.73 17.75-8.63 6.02-.91 11.87.47 17.26 4.83 1.59 1.29 3.14 2.83 4.63 4.65 5.76.55 10.46 3.66 13.82 8.08 2.01 2.64 3.53 5.76 4.5 9.08.97 3.31 1.38 6.83 1.21 10.3-.33 6.2-2.6 9.74-7.17 14.16.8.03 1.56.22 2.23.57 2.55 1.37 2.63 4.34 1.96 6.83-.66 2.07-1.5 4.48-2.3 6.51-.96 2.74-2.38 3.25-5.1 2.95-.14 6.76-3.26 10.08-7.47 14.05l.95 3.33c-2.65 12.87-30.83 14.27-38.44.71zM160.37 298.61c55.4 0 100.31 44.91 100.31 100.31 0 55.39-44.91 100.3-100.31 100.3-55.4 0-100.31-44.91-100.31-100.3 0-55.4 44.91-100.31 100.31-100.31z"/></svg>');
                    }
                    else if (valOne == "Call") {
                        $td = pft_table_createRowDataSVG((valOne == "Call") ? '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="20" height="20" viewBox="0 0 122.88 122.27" style="enable-background:new 0 0 122.88 122.27" xml:space="preserve"><title id="Call">Call</title><style type="text/css">.st12{fill:#080808;}</style><g><path class="st12" d="M33.84,50.25c4.13,7.45,8.89,14.6,15.07,21.12c6.2,6.56,13.91,12.53,23.89,17.63c0.74,0.36,1.44,0.36,2.07,0.11 c0.95-0.36,1.92-1.15,2.87-2.1c0.74-0.74,1.66-1.92,2.62-3.21c3.84-5.05,8.59-11.32,15.3-8.18c0.15,0.07,0.26,0.15,0.41,0.21 l22.38,12.87c0.07,0.04,0.15,0.11,0.21,0.15c2.95,2.03,4.17,5.16,4.2,8.71c0,3.61-1.33,7.67-3.28,11.1 c-2.58,4.53-6.38,7.53-10.76,9.51c-4.17,1.92-8.81,2.95-13.27,3.61c-7,1.03-13.56,0.37-20.27-1.69 c-6.56-2.03-13.17-5.38-20.39-9.84l-0.53-0.34c-3.31-2.07-6.89-4.28-10.4-6.89C31.12,93.32,18.03,79.31,9.5,63.89 C2.35,50.95-1.55,36.98,0.58,23.67c1.18-7.3,4.31-13.94,9.77-18.32c4.76-3.84,11.17-5.94,19.47-5.2c0.95,0.07,1.8,0.62,2.25,1.44 l14.35,24.26c2.1,2.72,2.36,5.42,1.21,8.12c-0.95,2.21-2.87,4.25-5.49,6.15c-0.77,0.66-1.69,1.33-2.66,2.03 c-3.21,2.33-6.86,5.02-5.61,8.18L33.84,50.25L33.84,50.25L33.84,50.25z"/></g></svg>' : '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="20" height="20" viewBox="0 0 122.88 122.27" style="enable-background:new 0 0 122.88 122.27" xml:space="preserve"><title id="Call">Call</title><style type="text/css">.st12{fill:#080808;}</style><g><path class="st12" d="M33.84,50.25c4.13,7.45,8.89,14.6,15.07,21.12c6.2,6.56,13.91,12.53,23.89,17.63c0.74,0.36,1.44,0.36,2.07,0.11 c0.95-0.36,1.92-1.15,2.87-2.1c0.74-0.74,1.66-1.92,2.62-3.21c3.84-5.05,8.59-11.32,15.3-8.18c0.15,0.07,0.26,0.15,0.41,0.21 l22.38,12.87c0.07,0.04,0.15,0.11,0.21,0.15c2.95,2.03,4.17,5.16,4.2,8.71c0,3.61-1.33,7.67-3.28,11.1 c-2.58,4.53-6.38,7.53-10.76,9.51c-4.17,1.92-8.81,2.95-13.27,3.61c-7,1.03-13.56,0.37-20.27-1.69 c-6.56-2.03-13.17-5.38-20.39-9.84l-0.53-0.34c-3.31-2.07-6.89-4.28-10.4-6.89C31.12,93.32,18.03,79.31,9.5,63.89 C2.35,50.95-1.55,36.98,0.58,23.67c1.18-7.3,4.31-13.94,9.77-18.32c4.76-3.84,11.17-5.94,19.47-5.2c0.95,0.07,1.8,0.62,2.25,1.44 l14.35,24.26c2.1,2.72,2.36,5.42,1.21,8.12c-0.95,2.21-2.87,4.25-5.49,6.15c-0.77,0.66-1.69,1.33-2.66,2.03 c-3.21,2.33-6.86,5.02-5.61,8.18L33.84,50.25L33.84,50.25L33.84,50.25z"/></g></svg>');
                    }
                    else if (valOne == "Email") {
                        $td = pft_table_createRowDataSVG((valOne == "Email") ? '<svg id="Layer_1" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 122.88 88.86"><title id="email">email</title><style type="text/css">.st13{fill:#076b07;}</style><path class="st13" d="M7.05,0H115.83a7.07,7.07,0,0,1,7,7.05V81.81a7,7,0,0,1-1.22,4,2.78,2.78,0,0,1-.66,1,2.62,2.62,0,0,1-.66.46,7,7,0,0,1-4.51,1.65H7.05a7.07,7.07,0,0,1-7-7V7.05A7.07,7.07,0,0,1,7.05,0Zm-.3,78.84L43.53,40.62,6.75,9.54v69.3ZM49.07,45.39,9.77,83.45h103L75.22,45.39l-11,9.21h0a2.7,2.7,0,0,1-3.45,0L49.07,45.39Zm31.6-4.84,35.46,38.6V9.2L80.67,40.55ZM10.21,5.41,62.39,47.7,112.27,5.41Z"/></svg>' : '<svg id="Layer_1" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 122.88 88.86"><title id="email">email</title><style type="text/css">.st13{fill:#076b07;}</style><path class="" d="M7.05,0H115.83a7.07,7.07,0,0,1,7,7.05V81.81a7,7,0,0,1-1.22,4,2.78,2.78,0,0,1-.66,1,2.62,2.62,0,0,1-.66.46,7,7,0,0,1-4.51,1.65H7.05a7.07,7.07,0,0,1-7-7V7.05A7.07,7.07,0,0,1,7.05,0Zm-.3,78.84L43.53,40.62,6.75,9.54v69.3ZM49.07,45.39,9.77,83.45h103L75.22,45.39l-11,9.21h0a2.7,2.7,0,0,1-3.45,0L49.07,45.39Zm31.6-4.84,35.46,38.6V9.2L80.67,40.55ZM10.21,5.41,62.39,47.7,112.27,5.41Z"/></svg>');
                    }
                    else if (valOne == "Employee") {
                        $td = pft_table_createRowDataSVG((valOne == "Employee") ? '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 512 469.76"><title id="employee">Employee</title><style type="text/css">.st14{fill:#e94560;}</style><path class="st14" fill-rule="nonzero" d="M139.64 191.28c3.34-9.28 8.44-12.29 15.16-12.1l-4.43-2.94c-2.4-30.05 4.64-82.18-27.99-92.05 61.74-76.3 132.9-117.79 186.33-49.92 64.37 3.38 90.19 105.67 38.85 148.73l-.3 2.54c2.01-.61 4.02-1 5.95-1.15 3.79-.29 7.42.33 10.51 1.97 3.39 1.81 5.98 4.74 7.31 8.89 1.28 3.97 1.33 9.04-.35 15.27l-8.03 22.78c-1.3 3.7-2.49 6.3-5.08 8.36-2.67 2.11-5.9 2.9-10.9 2.63-.83-.04-1.66-.13-2.47-.28a55.67 55.67 0 0 1-3.68 16.32c-2.77 7.12-6.63 12.93-10.43 18.29-4.3 6.05-8.85 11.53-13.6 16.45 3.7 12.99 8.47 22.56 14.19 29.84 29.04 20.88 107.25 25.79 134.48 40.97 39.31 22 38.23 64.52 46.84 103.88H0c8.53-39.01 7.65-82.23 46.84-103.88 34.52-19.22 107.77-17.99 136.96-42.9 4.2-6.58 7.75-14.46 10.47-23.86-6.23-6.48-12.07-14.18-17.39-23.12l-.36-.57c-4.8-8.06-10.6-17.79-12.42-31.3l-1.56.03c-3.27-.04-6V7.42 1.92-10.97 4.61-1.3.98-2.99 1.45-4.72 1.15-3.33-.57-5.58-3.74-5.01-7.07 5.77-33.67 3.13-55.61-4.04-70.57-6.28-13.11-16.3-21.05-27.17-26.98-24.12 18.47-41.1 20.58-58.04 22.67-14.01 1.73-27.99 3.46-46.51 16.27-8.75 6.05-14.58 13.37-17.59 21.87-3.09 8.73-3.33 18.89-.83 30.4.32 1.16.29 2.43-.16 3.65-1.14 3.17-4.66 4.81-7.83 3.67l-5.61-2.03c-7.24-2.53-12.37-3.71-14.35.84-.16 1.47-.27 3.72-.33 6.04v.05c-.09 3.35-.09 6.85-.01 8.61.21 4.15 1.08 9.55 2.97 14.13l.22.45c1.27 2.97 2.96 5.49 5.06 6.68 1.06.6 2.24.88 3.48.89 1.52.02 3.21-.3 5.01-.83.57-.2 1.18-.31 1.82-.32a6.111 6.111 0 0 1 6.26 5.96c.33 14.1 6.38 24.24 11.25 32.41l.34.57c5.44 9.14 11.44 16.81 17.83 23.04z"/></svg>' : '<svg xmlns="http://www.w3.org/2000/svg" shape-rendering="geometricPrecision" text-rendering="geometricPrecision" image-rendering="optimizeQuality" fill-rule="evenodd" clip-rule="evenodd" width="20" height="20" viewBox="0 0 512 469.76"><title id="employee">Employee</title><style type="text/css">.st14{fill:#e94560;}</style><path class="st14" fill-rule="nonzero" d="M139.64 191.28c3.34-9.28 8.44-12.29 15.16-12.1l-4.43-2.94c-2.4-30.05 4.64-82.18-27.99-92.05 61.74-76.3 132.9-117.79 186.33-49.92 64.37 3.38 90.19 105.67 38.85 148.73l-.3 2.54c2.01-.61 4.02-1 5.95-1.15 3.79-.29 7.42.33 10.51 1.97 3.39 1.81 5.98 4.74 7.31 8.89 1.28 3.97 1.33 9.04-.35 15.27l-8.03 22.78c-1.3 3.7-2.49 6.3-5.08 8.36-2.67 2.11-5.9 2.9-10.9 2.63-.83-.04-1.66-.13-2.47-.28a55.67 55.67 0 0 1-3.68 16.32c-2.77 7.12-6.63 12.93-10.43 18.29-4.3 6.05-8.85 11.53-13.6 16.45 3.7 12.99 8.47 22.56 14.19 29.84 29.04 20.88 107.25 25.79 134.48 40.97 39.31 22 38.23 64.52 46.84 103.88H0c8.53-39.01 7.65-82.23 46.84-103.88 34.52-19.22 107.77-17.99 136.96-42.9 4.2-6.58 7.75-14.46 10.47-23.86-6.23-6.48-12.07-14.18-17.39-23.12l-.36-.57c-4.8-8.06-10.6-17.79-12.42-31.3l-1.56.03c-3.27-.04-6V7.42 1.92-10.97 4.61-1.3.98-2.99 1.45-4.72 1.15-3.33-.57-5.58-3.74-5.01-7.07 5.77-33.67 3.13-55.61-4.04-70.57-6.28-13.11-16.3-21.05-27.17-26.98-24.12 18.47-41.1 20.58-58.04 22.67-14.01 1.73-27.99 3.46-46.51 16.27-8.75 6.05-14.58 13.37-17.59 21.87-3.09 8.73-3.33 18.89-.83 30.4.32 1.16.29 2.43-.16 3.65-1.14 3.17-4.66 4.81-7.83 3.67l-5.61-2.03c-7.24-2.53-12.37-3.71-14.35.84-.16 1.47-.27 3.72-.33 6.04v.05c-.09 3.35-.09 6.85-.01 8.61.21 4.15 1.08 9.55 2.97 14.13l.22.45c1.27 2.97 2.96 5.49 5.06 6.68 1.06.6 2.24.88 3.48.89 1.52.02 3.21-.3 5.01-.83.57-.2 1.18-.31 1.82-.32a6.111 6.111 0 0 1 6.26 5.96c.33 14.1 6.38 24.24 11.25 32.41l.34.57c5.44 9.14 11.44 16.81 17.83 23.04z"/></svg>' );
                    }
                    else if (valOne == "Media") {
                        $td = pft_table_createRowDataSVG((valOne == "Media") ? '<svg id="Layer_1" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 109.77 122.88"><defs><style type="text/css">.st15{fill:#0f9173;}</style></defs><title>Media</title><path class="st15" d="M36.3,0H80.44A13.28,13.28,0,0,1,93.71,13.28v28.4H89.12V13.15H27.6v4.5H23V13.26A13.25,13.25,0,0,1,36.3,0ZM14.75,35.76a3.41,3.41,0,1,1-3.41,3.4,3.4,3.4,0,0,1,3.41-3.4ZM29.08,50.37l6.8-11.77,4.64,11.71,0,6.59H9.35V54.63l2.84-.15L15,47.53l1.42,5H20.7L24.39,43l4.69,7.38ZM5.63,25.08H44.88a5.67,5.67,0,0,1,5.63,5.63V58.77a5.67,5.67,0,0,1-5.63,5.62H5.63A5.65,5.65,0,0,1,0,58.77V30.71a5.65,5.65,0,0,1,5.63-5.63Zm39.25,3.46H5.63a2.17,2.17,0,0,0-2.17,2.17V58.77a2.15,2.15,0,0,0,2.17,2.16H44.88A2.15,2.15,0,0,0,47,58.77V30.71a2.17,2.17,0,0,0-2.16-2.17ZM73.94,76.23a1.52,1.52,0,1,1,0-3h21a1.52,1.52,0,1,1,0,3Zm0-9.3a1.52,1.52,0,1,1,0-3H99.65a1.52,1.52,0,0,1,0,3Zm.91,24.78,7.56-7.25a1.5,1.5,0,0,1,1-.42h22.44a.85.85,0,0,0,.85-.85V57.33a.87.87,0,0,0-.85-.86H66.75a.85.85,0,0,0-.6.26l-.05,0a.86.86,0,0,0-.21.56V83.18a.86.86,0,0,0,.25.6.89.89,0,0,0,.61.25h6.58a1.52,1.52,0,0,1,1.52,1.52v6.16Zm9.21-4.65-9.61,9.22a1.53,1.53,0,0,1-1.12.49,1.52,1.52,0,0,1-1.51-1.52V87.06H66.75a3.89,3.89,0,0,1-3.89-3.88V57.33a3.87,3.87,0,0,1,1.06-2.65l.09-.09a3.84,3.84,0,0,1,2.74-1.15h39.14a3.89,3.89,0,0,1,3.88,3.89V83.18a3.89,3.89,0,0,1-3.88,3.88Zm9.65,10.51v12a13.28,13.28,0,0,1-13.27,13.28H36.3A13.28,13.28,0,0,1,23,109.6V71.83H27.6v32.61H89.12V97.57Zm-35.36,11a5.46,5.46,0,1,1-5.46,5.45,5.45,5.45,0,0,1,5.46-5.45Z"/></svg>' : '<svg id="Layer_1" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 109.77 122.88"><defs><style type="text/css">.st15{fill:#0f9173;}</style></defs><title>Media</title><path class="st15" d="M36.3,0H80.44A13.28,13.28,0,0,1,93.71,13.28v28.4H89.12V13.15H27.6v4.5H23V13.26A13.25,13.25,0,0,1,36.3,0ZM14.75,35.76a3.41,3.41,0,1,1-3.41,3.4,3.4,3.4,0,0,1,3.41-3.4ZM29.08,50.37l6.8-11.77,4.64,11.71,0,6.59H9.35V54.63l2.84-.15L15,47.53l1.42,5H20.7L24.39,43l4.69,7.38ZM5.63,25.08H44.88a5.67,5.67,0,0,1,5.63,5.63V58.77a5.67,5.67,0,0,1-5.63,5.62H5.63A5.65,5.65,0,0,1,0,58.77V30.71a5.65,5.65,0,0,1,5.63-5.63Zm39.25,3.46H5.63a2.17,2.17,0,0,0-2.17,2.17V58.77a2.15,2.15,0,0,0,2.17,2.16H44.88A2.15,2.15,0,0,0,47,58.77V30.71a2.17,2.17,0,0,0-2.16-2.17ZM73.94,76.23a1.52,1.52,0,1,1,0-3h21a1.52,1.52,0,1,1,0,3Zm0-9.3a1.52,1.52,0,1,1,0-3H99.65a1.52,1.52,0,0,1,0,3Zm.91,24.78,7.56-7.25a1.5,1.5,0,0,1,1-.42h22.44a.85.85,0,0,0,.85-.85V57.33a.87.87,0,0,0-.85-.86H66.75a.85.85,0,0,0-.6.26l-.05,0a.86.86,0,0,0-.21.56V83.18a.86.86,0,0,0,.25.6.89.89,0,0,0,.61.25h6.58a1.52,1.52,0,0,1,1.52,1.52v6.16Zm9.21-4.65-9.61,9.22a1.53,1.53,0,0,1-1.12.49,1.52,1.52,0,0,1-1.51-1.52V87.06H66.75a3.89,3.89,0,0,1-3.89-3.88V57.33a3.87,3.87,0,0,1,1.06-2.65l.09-.09a3.84,3.84,0,0,1,2.74-1.15h39.14a3.89,3.89,0,0,1,3.88,3.89V83.18a3.89,3.89,0,0,1-3.88,3.88Zm9.65,10.51v12a13.28,13.28,0,0,1-13.27,13.28H36.3A13.28,13.28,0,0,1,23,109.6V71.83H27.6v32.61H89.12V97.57Zm-35.36,11a5.46,5.46,0,1,1-5.46,5.45,5.45,5.45,0,0,1,5.46-5.45Z"/></svg>');
                    }

                }
                if (options && options["image"] && options["image"].includes(keyOne)) {
                    //console.log('image', valOne)
                    $td = pft_table_createRowDataImage(valOne);
                }
              
                if (options && options["hyperlink"] && Object.keys(options["hyperlink"]).some(key => key === keyOne)) {  
                   
                    if (options["hyperlink"][keyOne].includes("?")) {
                       
                        $td = pft_table_createRowDatahyperlinkparam(valOne, options["hyperlink"][keyOne]);

                    }
                    else if (options["hyperlink"][keyOne]=="")
                    {                       
                        $td = pft_table_createRowDatanohyperlink(valOne);
                    }
                    else {
                      
                        $td = pft_table_createRowDatahyperlink(valOne, options["hyperlink"][keyOne]);
                    }
                }
                if (options && options["trackInfo"] && Object.keys(options["trackInfo"]).some(key => key === keyOne)) {
                    $td = pft_table_createRowDataTrackInfo(valOne, options["trackInfo"][keyOne], val)
                }
                if (options && options["rowClickAction"]) {
                    if (options && options["trackInfo"] && Object.keys(options["trackInfo"]).some(key => key === keyOne)) {
                    }
                    else {
                        $td.css('cursor', 'pointer');
                        $td.click(options.rowClickAction)
                    }      
                }

                //if (options && options["FeedBackInfo"] && Object.keys(options["FeedBackInfo"]).some(key => key === keyOne)) {
                //    $td = pft_table_createRowDataFeedBackInfo(valOne, options["trackInfo"][keyOne], val)
                //}
                //if (options && options["rowClickAction"]) {
                //    if (options && options["FeedBackInfo"] && Object.keys(options["FeedBackInfo"]).some(key => key === keyOne)) {
                //    }
                //    else {
                //        $td.css('cursor', 'pointer');
                //        $td.click(options.rowClickAction)
                //    }
                //}
                $row.append($td);
            }

        });
    }
    else {
        $.each(val, function (keyOne, valOne) {
           // console.log( keyOne + "|" + valOne);

            if (options && options["hideColumn"] && options["hideColumn"].includes(keyOne)) {
                //since hidden dont appent anything
                // $row.append(pft_table_createRowData(valOne));
            }
            else {
                let $td = "";
                if (options["renameHeader"] && options["renameHeader"][keyOne]) {
                  
                    if (options["renameHeader"][keyOne].includes('1R')) {
                 
                        $td = pft_table_createRowDataright(valOne == "" || valOne == null? null : (((options && options["isDateType"] && options["isDateType"].includes(keyOne)) ? moment(valOne).format('DD/MM/YYYY') : valOne)), keyOne);

                    }
                    else {
                        
                        $td = pft_table_createRowData(valOne == "" || valOne == null ? null : (((options && options["isDateType"] && options["isDateType"].includes(keyOne)) ? moment(valOne).format('DD/MM/YYYY') : valOne)), keyOne);

                    }

                }
                else {
                   
                    if (keyOne.includes('1R')) {
                        $td = pft_table_createRowDataright(valOne == "" || valOne == null ? null : (((options && options["isDateType"] && options["isDateType"].includes(keyOne)) ? moment(valOne).format('DD/MM/YYYY') : valOne)), keyOne);

                    }
                    else {

                        $td = pft_table_createRowData(valOne == "" || valOne == null ? null : (((options && options["isDateType"] && options["isDateType"].includes(keyOne)) ? moment(valOne).format('DD/MM/YYYY') : valOne)), keyOne);

                    }

                }
                if (options && options["isCheckType"] && options["isCheckType"].includes(keyOne)) {
                    $td = pft_table_createRowDataIcon((valOne == "true" || valOne== true) ? 'text-success fa fa-check' : 'text-danger fa fa-times');
                }
                if (options && options["image"] && options["image"].includes(keyOne)) {                  
                    $td = pft_table_createRowDataImage(valOne);
                }

              
                if (options && options["hyperlink"] && Object.keys(options["hyperlink"]).some(key => key === keyOne)) {
                   
                    if (options["hyperlink"][keyOne].includes("?")) {
                      
                        $td = pft_table_createRowDatahyperlinkparam(valOne, options["hyperlink"][keyOne]);
                       
                    }
                    else if (options["hyperlink"][keyOne] == "") {                       
                        $td = pft_table_createRowDatanohyperlink(valOne);
                    }
                    else {
                       
                        $td = pft_table_createRowDatahyperlink(valOne, options["hyperlink"][keyOne]);
                    }
                }
                if (options && options["trackInfo"] && Object.keys(options["trackInfo"]).some(key => key === keyOne)) {
                    $td = pft_table_createRowDataTrackInfo(valOne, options["trackInfo"][keyOne], val)
                }
                if (options && options["rowClickAction"]) {
                    if (Object.keys(val).find(key => val["ID_FIELD"] === -1)) { }
                    else {
                        if (options && options["trackInfo"] && Object.keys(options["trackInfo"]).some(key => key === keyOne)) {
                        }
                        else {
                            $td.css('cursor', 'pointer');
                            $td.click(options.rowClickAction)
                        }
                    }
                }
                //if (options && options["FeedBackInfo"] && Object.keys(options["FeedBackInfo"]).some(key => key === keyOne)) {
                //    $td = pft_table_createRowDataFeedBackInfo(valOne, options["FeedBackInfo"][keyOne], val)
                //}
                //if (options && options["rowClickAction"]) {
                //    if (Object.keys(val).find(key => val["ID_FIELD"] === -1)) { }
                //    else {
                //        if (options && options["FeedBackInfo"] && Object.keys(options["FeedBackInfo"]).some(key => key === keyOne)) {
                //        }
                //        else {
                //            $td.css('cursor', 'pointer');
                //            $td.click(options.rowClickAction)
                //        }
                //    }
                //} 
                $row.append($td);
            }
        });
    }
    if (options) {

        if (options["iconButton"]) {

            $row.append(pft_table_createRowDataButton(options["iconButton"]))

        }
        if (options["textButton"]) {

            $row.append(pft_table_createRowDataTextButton(options["textButton"]))

        }
        if (options["dropdown"]) {

            $row.append(pft_table_createRowDataDropDown(options["dropdown"]))

        }
        

    }

    return $row;
}
var pft_table_createRowDataDropDown = (options) => {
    let $dropDownButton = $('<button/>', { class: "btn btn-primary tp-btn-light sharp pft_dynamic_table_dropbtn py-0", type: "button", "data-toggle": "dropdown", "data-boundary": "viewport", "aria-haspopup": "true", "aria-expanded": "false" }).html('<span><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="18px" height="18px" viewBox="0 0 24 24" version="1.1"><g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><rect x="0" y="0" width="24" height="24"></rect><circle fill="#000000" cx="5" cy="12" r="2"></circle><circle fill="#000000" cx="12" cy="12" r="2"></circle><circle fill="#000000" cx="19" cy="12" r="2"></circle></g></svg></span>');

    if (options["class"]) {
        $dropDownButton.addClass(options["class"]);
    }

    let $dropDownList = $('<div/>', { class: "py-2" });

    $.each(options["buttons"], function (i, valOne) {
        if (valOne.type == "divider") {
            $dropDownList.append('<div class="dropdown-divider"></div>');
        }
        else {
            $dropDownList.append($('<a/>', { class: "dropdown-item", href: "#!" }).text(valOne.displayName).click(valOne.action));
        }

    });

    return $('<td/>', { class: "py-2 text-right" }).append($('<div/>', { class: "dropdown text-sans-serif" }).append($dropDownButton).append($('<div/>', { class: "dropdown-menu dropdown-menu-right border py-0" }).append($dropDownList).selectpicker()));
}
var pft_table_createRowDataButton = (options) => {

    let $buttonList = $('<div/>', { class: "d-flex" });
    if (options["class"]) {
        $buttonList.addClass(options["class"]);
    }



    $.each(options["buttons"], function (i, valOne) {

        $buttonList.append($('<button/>', { class: "btn btnPerfectGrid shadow btn-xs sharp mr-1", "type": "button", "title": valOne.title }).html($('<i/>', { class: valOne.icon })).click(valOne.action));

    });

    return $('<td/>', { class: "py-2 text-right" }).append($buttonList);
};
var pft_table_createRowDataTextButton = (options) => {

    let $buttonList = $('<div/>', { class: "" });
    if (options["class"]) {
        $buttonList.addClass(options["class"]);
    }



    $.each(options["buttons"], function (i, valOne) {

        let $button = $('<button/>', { class: "btn  mr-1 btn-xs", "type": "button" }).text(valOne.text).click(valOne.action);

        if (valOne["class"]) {
            $button.addClass(valOne["class"]);
        }

        $buttonList.append($button);

       

    });

    return $('<td/>', { class: "py-2 text-right" }).append($buttonList);
};


var pft_table_createtable = (val, options) => {
    // This function retuen a complete table

   // console.log("> fn create table");
    let $table = $('<table/>', { class: "table mb-0 table-hover  table-striped  text-black" });
    let $thead = $('<thead/>');
    let $tbody = $('<tbody/>');
    let $tfoot = $('<tfoot/>');

    //add class to the tabe head
    if (options && options["tHeadClass"]) {
        $thead.addClass(options["tHeadClass"]);
    }
    else {
        $thead.addClass('thead-primary');
    }

    

    let $rowhead = pft_table_createRow();

    // :: create table head ::
    if (val) {
     //   console.log("> in If statament");

        if (options && options["serialNo"]) {
            $rowhead.append($('<th/>', { class: "py-2" }).text('Sl No.'));
        }

        if (options && options["onlyShowColumn"] && options["onlyShowColumn"].length > 0) {

          
            for (var k in val[0]) {
           
                if (options["onlyShowColumn"].includes(k)) {
                    let rename = "";


                    if (options["renameHeader"] && options["renameHeader"][k]) {
                        if (options["renameHeader"][k].includes('1R')) {
                            rename = options["renameHeader"][k].replace('1R', '');
                            let header = rename;
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW text-right" }).text(header));
                        }
                        else {
// style: "width:" + value.Width + "%;"
                            let header = options["renameHeader"][k];
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW" }).text(header));
                        }

                    }
                    else {
                        if (k.includes('1R')) {
                            rename = k.replace('1R', '');
                            let header = pft_camel_case_space(rename); 
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW text-right" }).text(header));
                        }
                        else {

                            let header = pft_camel_case_space(k);
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW" }).text(header));
                        }

                    }
                }

            }
        }
        else {
            for (var k in val[0]) {
               
                if (options && options["hideColumn"] && options["hideColumn"].includes(k)) {
                    //dont appent any thing
                }
                else {
                    let rename = "";
                    if (options["renameHeader"] && options["renameHeader"][k]) {
                        if (options["renameHeader"][k].includes('1R')) {
                            rename = options["renameHeader"][k].replace('1R', '');
                            let header = rename;
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW text-right" }).text(header));
                        }
                        else {
                          
                            let header = options["renameHeader"][k];
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW" }).text(header));
                        }
                       
                    }
                    else {
                        if (k.includes('1R')) {
                            rename = k.replace('1R', '');
                            let header = pft_camel_case_space(rename); 
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW text-right" }).text(header));
                        }
                        else {

                            let header = pft_camel_case_space(k);
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW" }).text(header));
                        }
                       
                    }
                }

            }
        }
        if (options) {
            if (options["textButton"]) {

                // if dropdown dont appent any table head
                $rowhead.append($('<th/>', { class: "no-sort" }));
            }
            if (options["iconButton"]) {

                // if button dont appent any table head
                $rowhead.append($('<th/>', { class: "no-sort" }));
            }
            if (options["dropdown"]) {

                // if dropdown dont appent any table head
                $rowhead.append($('<th/>', { class: "no-sort" }));
            }


        }

    }
    else {
        $rowhead.append($('<th/>', { class: "no-sort" }));
    }
    // :: create table body ::
    if (val) {
       // console.log("> in If statament");

        $.each(val, function (i, valOne) {
            let $trow = pft_table_createRowWithData(valOne, options);

            //add row attribute
            if (options && options["rowAttribute"]) {
                $.each(options["rowAttribute"], function (ratKey, ratVal) {
                    if (valOne[ratVal]) {
                        $trow.attr(ratVal, valOne[ratVal]);
                    }
                });
            }
            //add serial number
            if (options && options["serialNo"]) {
                let $sl = pft_table_createRowData($tbody.find('tr').length + 1,'');
                if (options && options["rowClickAction"]) {
                  
                        $sl.css('cursor', 'pointer');
                        $sl.click(options.rowClickAction);
                    
                 
                }
                $sl.addClass('pft_tbl_slno')
                $trow.prepend($sl);
            }
           
            $tbody.append($trow);
        });

    }

    $table.append($thead.append($rowhead));
    $table.append($tbody);
    $table.append($tfoot);
    return $table//.dataTable();
}
var pft_table_addTableRow = (tableid, val, options) => {

    let $tbody = $(tableid).find('tbody');
    let $trow = pft_table_createRowWithData(val, options);

    //add row attribute
    if (options && options["rowAttribute"]) {
        $.each(options["rowAttribute"],function (ratKey,ratVal) {
            if (val[ratVal]) {
                $trow.attr(ratVal, val[ratVal]);
            }
        });
    }
    //add serial number
    if (options && options["serialNo"]) {
        let $sl = pft_table_createRowData($tbody.find('tr').length + 1,'');
        if (options && options["rowClickAction"]) {
            $sl.css('cursor', 'pointer');
            $sl.click(options.rowClickAction);
        }
        $sl.addClass('pft_tbl_slno');
        $trow.prepend($sl);
    }
    $tbody.append($trow);

    //$(tableid).find('tbody').append(pft_table_createRowWithData(val, options));
}

//---Functon to load bulk data to existiing datatable ::  S T A R T  ::
var pft_table_addMultipleTableRows = (tableid, val, options) => {

    let $tbody = $(tableid).find('tbody');

    if (val) {
       

        $.each(val, function (i, valOne) {
            let $trow = pft_table_createRowWithData(valOne, options);

            //add row attribute
            if (options && options["rowAttribute"]) {
                $.each(options["rowAttribute"], function (ratKey, ratVal) {
                    if (valOne[ratVal]) {
                        $trow.attr(ratVal, valOne[ratVal]);
                    }
                });
            }
            //add serial number
            if (options && options["serialNo"]) {
                let $sl = pft_table_createRowData($tbody.find('tr').length + 1,'');
                if (options && options["rowClickAction"]) {
                    $sl.css('cursor', 'pointer');
                    $sl.click(options.rowClickAction);
                }
                $sl.addClass('pft_tbl_slno');
                $trow.prepend($sl);
            }
            $tbody.append($trow);
        });
    }
    //$(tableid).find('tbody').append(pft_table_createRowWithData(val, options));
}
//---Functon to load bulk data to existiing datatable ::  E N D  ::
//---Functon to load bulk data to existiing datatable ::  S T A R T  ::
var pft_table_newTableBody = (tableid, val, options) => {

    let $tbody = $(tableid).find('tbody');
    $tbody.empty();
    if (val) {


        $.each(val, function (i, valOne) {
            let $trow = pft_table_createRowWithData(valOne, options);

            //add row attribute
            if (options && options["rowAttribute"]) {
                $.each(options["rowAttribute"], function (ratKey, ratVal) {
                    if (valOne[ratVal]) {
                        $trow.attr(ratVal, valOne[ratVal]);
                    }
                });
            }
            //add serial number
            if (options && options["serialNo"]) {
                //alert($tbody.find('tr').length)
                //let slno = ($tbody.find('tr')) ? ($tbody.find('tr').length+ 1):1

                let $sl = pft_table_createRowData($tbody.find('tr').length + 1,'');
                if (options && options["rowClickAction"]) {
                    $sl.css('cursor', 'pointer');
                    $sl.click(options.rowClickAction);
                }
                $sl.addClass('pft_tbl_slno');
                $trow.prepend($sl);
            }
            $tbody.append($trow);
        });
    }
    //$(tableid).find('tbody').append(pft_table_createRowWithData(val, options));
}
//---Functon to load bulk data to existiing datatable ::  E N D  ::


var pft_table_replaceTableRow = (tableid,rcond, val, options) => {

    let $tbody = $(tableid).find('tbody').find(rcond);
    let $trow = pft_table_createRowWithData(val, options);

    //add row attribute
    if (options && options["rowAttribute"]) {
        $.each(options["rowAttribute"], function (ratKey, ratVal) {
            if (val[ratVal]) {
                $trow.attr(ratVal, val[ratVal]);
            }
        });
    }
    //add serial number
    if (options && options["serialNo"]) {
        let $sl = pft_table_createRowData($tbody.find('.pft_tbl_slno').text(),'');
        if (options && options["rowClickAction"]) {
            $sl.css('cursor', 'pointer');
            $sl.click(options.rowClickAction);
        }
        $sl.addClass('pft_tbl_slno');
        $trow.prepend($sl);
    }
    $tbody.replaceWith($trow);

    //$(tableid).find('tbody').append(pft_table_createRowWithData(val, options));
}

var pft_table_slreset = (tableid) => {
    let $tbody = $(tableid).find('tbody');
    $tbody.find('tr').each(function (i) {
        $(this).find('.pft_tbl_slno').text(i+1);
    });

}

//---------------ANOTHER METHOD TO ADD TABLE
var pft_table_createtable_LAZY = (val, options) => {
    // This function retuen a complete table

    //console.log("> fn create table");
    let $table = $('<table/>', { class: "table mb-0  table-hover table-striped  text-black" });
    let $thead = $('<thead/>');
    let $tbody = $('<tbody/>');

    let $rowhead = pft_table_createRow();

    // :: create table head ::
    if (val) {
        console.log("> in If statament");

        for (var k in val[0]) {
            if (options && options["hideColumn"] && options["hideColumn"].includes(k)) {
                //dont appent any thing
            }
            else {
                $rowhead.append($('<th/>', { class: "py-2" }).text(k));
            }

        }

        if (options) {
            if (options["iconButton"]) {

                // if button dont appent any table head
                $rowhead.append($('<th/>', { class: "no-sort" }));
            }
            if (options["dropdown"]) {

                // if dropdown dont appent any table head
                $rowhead.append($('<th/>', { class: "no-sort" }));
            }

        }

    }
    // :: create table body ::
    //if (val) {
    //    console.log("> in If statament");

    //    $.each(val, function (i, valOne) {
    //        $tbody.append(pft_table_createRowWithData(valOne, options));
    //    });

    //}

    $table.append($thead.append($rowhead));
    $table.append($tbody);
    return $table//.dataTable();
}

var CreateTableCommonGrid = (val, options,_id) => {
    // This function retuen a complete table

    // console.log("> fn create table");
    let $table = $('<table/>', { id: _id,class: "table mb-0 table-hover  table-striped  text-black" });
    let $thead = $('<thead/>');
    let $tbody = $('<tbody/>');
    let $tfoot = $('<tfoot/>');

    //add class to the tabe head
    if (options && options["tHeadClass"]) {
        $thead.addClass(options["tHeadClass"]);
    }
    else {
        $thead.addClass('thead-primary');
    }



    let $rowhead = pft_table_createRow();

    // :: create table head ::
    if (val) {
        
        if (options && options["serialNo"]) {
            $rowhead.append($('<th/>', { class: "py-2" }).text('Sl No.'));
        }

        if (options && options["onlyShowColumn"] && options["onlyShowColumn"].length > 0) {


            for (var k in val[0]) {

                if (options["onlyShowColumn"].includes(k)) {
                    let rename = "";


                    if (options["renameHeader"] && options["renameHeader"][k]) {
                        if (options["renameHeader"][k].includes('1R')) {
                            rename = options["renameHeader"][k].replace('1R', '');
                            let header = rename;
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW text-right" }).text(header));
                        }
                        else {
                            // style: "width:" + value.Width + "%;"
                            let header = options["renameHeader"][k];
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW" }).text(header));
                        }

                    }
                    else {
                        if (k.includes('1R')) {
                            rename = k.replace('1R', '');
                            let header = pft_camel_case_space(rename);
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW text-right" }).text(header));
                        }
                        else {

                            let header = pft_camel_case_space(k);
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW" }).text(header));
                        }

                    }
                }

            }
        }
        else {
            for (var k in val[0]) {

                if (options && options["hideColumn"] && options["hideColumn"].includes(k)) {
                    //dont appent any thing
                }
                else {
                    let rename = "";
                    if (options["renameHeader"] && options["renameHeader"][k]) {
                        if (options["renameHeader"][k].includes('1R')) {
                            rename = options["renameHeader"][k].replace('1R', '');
                            let header = rename;
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW text-right" }).text(header));
                        }
                        else {

                            let header = options["renameHeader"][k];
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW" }).text(header));
                        }

                    }
                    else {
                        if (k.includes('1R')) {
                            rename = k.replace('1R', '');
                            let header = pft_camel_case_space(rename);
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW text-right" }).text(header));
                        }
                        else {

                            let header = pft_camel_case_space(k);
                            $rowhead.append($('<th/>', { class: "py-2 pf_tableHeadMaxW" }).text(header));
                        }

                    }
                }

            }
        }
        if (options) {
            if (options["textButton"]) {

                // if dropdown dont appent any table head
                $rowhead.append($('<th/>', { class: "no-sort" }));
            }
            if (options["iconButton"]) {

                // if button dont appent any table head
                $rowhead.append($('<th/>', { class: "no-sort" }));
            }
            if (options["dropdown"]) {

                // if dropdown dont appent any table head
                $rowhead.append($('<th/>', { class: "no-sort" }));
            }


        }

    }
    else {
        $rowhead.append($('<th/>', { class: "no-sort" }));
    }
    // :: create table body ::
    if (val) {
        // console.log("> in If statament");

        $.each(val, function (i, valOne) {
            let $trow = pft_table_createRowWithData(valOne, options);

            //add row attribute
            if (options && options["rowAttribute"]) {
                $.each(options["rowAttribute"], function (ratKey, ratVal) {
                    if (valOne[ratVal]) {
                        $trow.attr(ratVal, valOne[ratVal]);
                    }
                });
            }
            //add serial number
            if (options && options["serialNo"]) {
                let $sl = pft_table_createRowData($tbody.find('tr').length + 1, '');
                if (options && options["rowClickAction"]) {

                    $sl.css('cursor', 'pointer');
                    $sl.click(options.rowClickAction);


                }
                $sl.addClass('pft_tbl_slno')
                $trow.prepend($sl);
            }

            $tbody.append($trow);
        });

    }

    $table.append($thead.append($rowhead));
    $table.append($tbody);
    $table.append($tfoot);
    return $table//.dataTable();
}



