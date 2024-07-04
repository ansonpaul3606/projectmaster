var perfect = function () {
    var e = $(window).width(),
        t = function () { $("#preloader").fadeOut(500), $("#main-wrapper").addClass("show") },
        n = function () { jQuery("select").length > 0 && jQuery("select").selectpicker() },
        o = function () {
            var e = $(window).height() - 206;
            //$(".chatbox .msg_card_body").css("height", e)


        },
        selectAddon = function () {


            $('select[perfect-SelectButton]').each(function () {
                let $this = $(this);

                //let optgrp = ($this.find('[perfect-class=selectButtongroup]').length > 0) ? $this.find('[perfect-class=selectButtongroup]') : $('<optgroup/>', { "perfect-class": "selectButtongroup" });
                if ($this.find('[perfect-class=selectButtongroup]').length > 0) {
                    $this.find('[perfect-class=selectButtongroup]').remove();
                }

                 let optgrp = $('<optgroup/>', { "perfect-class":"selectButtongroup"});

                optgrp.html($('<option/>', { 'data-icon': "fa fa-plus" })
                    .text($this.attr('perfect-SelectButton'))
                    .data({ pftActionButton: { name: $this.attr('perfect-SelectButton'), action: $this.attr('perfect-SelectButtonAction') } })
                );
                $this.append(optgrp);
                $this.selectpicker('refresh');

            });



            $('select[perfect-SelectButton]').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
                // do something...
                let $this = $(this);
                //console.log($(this).eq(clickedIndex).text())
                let actionvalue = $this.find('option').eq(clickedIndex).data('pftActionButton');

                if (actionvalue) {
                    $this.val('');
                    $this.selectpicker('refresh');

                    window[actionvalue.action]($this);

                }

                //console.log($(this).val())



            });
        },
        passwordShowIcon = function () {
            jQuery('[perfect-passwordGroup=container]').find('[perfect-passwordGroup=eyeicon]').click(function () {
              //  alert('')
                let $iconBtn = $(this).closest('[perfect-passwordGroup=container]').find('[perfect-passwordGroup=input]');
                if ($iconBtn.attr('type') == "password") {
                    $iconBtn.attr('type', 'text');
                }
                else {
                    $iconBtn.attr('type', 'password');
                }
                   

            });;
        },
        inputSearchAddon = function () {
            $('button[perfect-SearchButton]').each(function () {
                let $this = $(this);
                $this.on('click',window[$this.attr('perfect-SearchButton')]);

            });
        };
    return {
        init: function () {
            var l; n(), t(),
                jQuery("#menu").length > 0 && $("#menu").metisMenu(),
                jQuery(".metismenu > .mm-active ").each(function () {
                    !jQuery(this).children("ul").length > 0 && jQuery(this).addClass("active-no-child")
                }),
                $("#checkAll").on("change", function () {
                    $("td input:checkbox, .email-list .custom-checkbox input:checkbox").prop("checked", $(this).prop("checked"))
                }),
                $(".nav-control").on("click", function () {
                    $("#main-wrapper").toggleClass("menu-toggle"), $(".hamburger").toggleClass("is-active")
                }),
                //function () {
                //    for (var e = window.location, t = $("ul#menu a").filter(function () { return this.href == e }).addClass("mm-active").parent().addClass("mm-active"); t.is("li");)
                //        t = t.parent().addClass("mm-show").parent().addClass("mm-active")
                //}(),
                $(".custom-file-input").on("change", function () {
                    var e = $(this).val().split("\\").pop(); $(this).siblings(".custom-file-label").addClass("selected").html(e)
                }),
                $("ul#menu>li").on("click", function () {
                    "mini" === $("body").attr("data-sidebar-style") && (console.log($(this).find("ul")), $(this).find("ul").stop())
                }),
                l = window.outerHeight,
                //((l = window.outerHeight) > 0 ? l : screen.height) && $(".content-body").css("min-height", l + 60 + "px"),
                ((l = window.outerHeight) > 0 ? l : screen.height) && $(".content-body").css("min-height", "calc( 100vh - 60px)"),
                $('a[data-action="collapse"]').on("click", function (e) {
                    e.preventDefault(), $(this).closest(".card").find('[data-action="collapse"] i').toggleClass("mdi-arrow-down mdi-arrow-up"), $(this).closest(".card").children(".card-body").collapse("toggle")
                }),
                $('a[data-action="expand"]').on("click", function (e) {
                    e.preventDefault(), $(this).closest(".card").find('[data-action="expand"] i').toggleClass("icon-size-actual icon-size-fullscreen"), $(this).closest(".card").toggleClass("card-fullscreen")
                }),
                $('[data-action="close"]').on("click", function () {
                    $(this).closest(".card").removeClass().slideUp("fast")
                }),
                $('[data-action="reload"]').on("click", function () {
                    var e = $(this);
                    e.parents(".card").addClass("card-load"),
                        e.parents(".card").append('<div class="card-loader"><i class=" ti-reload rotate-refresh"></div>'),
                        setTimeout(function () { e.parents(".card").children(".card-loader").remove(), e.parents(".card").removeClass("card-load") }, 2e3)
                }),
                function () {
                    const e = $(".header").innerHeight();
                    $(window).scroll(function () { "horizontal" === $("body").attr("data-layout") && "static" === $("body").attr("data-header-position") && "fixed" === $("body").attr("data-sidebar-position") && ($(this.window).scrollTop() >= e ? $(".deznav").addClass("fixed") : $(".deznav").removeClass("fixed")) })
                }(),
                jQuery(".dz-scroll").each(function () {
                    var e = jQuery(this).attr("id"); new PerfectScrollbar("#" + e, { wheelSpeed: 2, wheelPropagation: !0, minScrollbarLength: 20 }).isRtl = !1
                }), e <= 991 && (
                    jQuery(".menu-tabs .nav-link").on("click", function () {
                        jQuery(this).hasClass("open") ? (jQuery(this).removeClass("open"), jQuery(".fixed-content-box").removeClass("active"), jQuery(".hamburger").show()) : (jQuery(".menu-tabs .nav-link").removeClass("open"), jQuery(this).addClass("open"), jQuery(".fixed-content-box").addClass("active"), jQuery(".hamburger").hide())
                    }),
                    jQuery(".close-fixed-content").on("click", function () {
                        jQuery(".fixed-content-box").removeClass("active"), jQuery(".hamburger").removeClass("is-active"), jQuery("#main-wrapper").removeClass("menu-toggle"), jQuery(".hamburger").show()
                    })),
                //jQuery(".bell-link").on("click", function () {
                //    jQuery(".chatbox").addClass("active")
                //}),
                //jQuery(".chatbox-close").on("click", function () {
                //    jQuery(".chatbox").removeClass("active")
                //}),
                $(".btn-number").on("click", function (e) {
                    e.preventDefault(), fieldName = $(this).attr("data-field"), type = $(this).attr("data-type"); var t = $("input[name='" + fieldName + "']"), n = parseInt(t.val()); isNaN(n) ? t.val(0) : "minus" == type ? t.val(n - 1) : "plus" == type && t.val(n + 1)
                }),
                //jQuery(".dz-chat-user-box .dz-chat-user").on("click", function () {
                //    jQuery(".dz-chat-user-box").addClass("d-none"), jQuery(".dz-chat-history-box").removeClass("d-none")
                //}),
                //jQuery(".dz-chat-history-back").on("click", function () {
                //    jQuery(".dz-chat-user-box").removeClass("d-none"), jQuery(".dz-chat-history-box").addClass("d-none")
                //}),
                //jQuery(".dz-fullscreen").on("click", function () {
                //    jQuery(".dz-fullscreen").toggleClass("active")
                //}),
                //jQuery(".dz-fullscreen").on("click", function (e) {
                //    document.fullscreenElement || document.webkitFullscreenElement || document.mozFullScreenElement || document.msFullscreenElement ? document.exitFullscreen ? document.exitFullscreen() : document.msExitFullscreen ? document.msExitFullscreen() : document.mozCancelFullScreen ? document.mozCancelFullScreen() : document.webkitExitFullscreen && document.webkitExitFullscreen() : document.documentElement.requestFullscreen ? document.documentElement.requestFullscreen() : document.documentElement.webkitRequestFullscreen ? document.documentElement.webkitRequestFullscreen() : document.documentElement.mozRequestFullScreen ? document.documentElement.mozRequestFullScreen() : document.documentElement.msRequestFullscreen && document.documentElement.msRequestFullscreen()
                //}),
                jQuery(".deznav-scroll").length > 0 && (new PerfectScrollbar(".deznav-scroll").isRtl = !1),
                jQuery(".dz-demo-content").length > 0 && (new PerfectScrollbar(".dz-demo-content"),
                    $(".dz-demo-trigger").on("click", function () {
                        $(".dz-demo-panel").toggleClass("show")
                    }),
                    $(".bg-close").on("click", function () {
                        $(".dz-demo-panel").removeClass("show")
                    }),
                    $(".dz-demo-bx").on("click", function () {
                        $(".dz-demo-bx").removeClass("demo-active"), $(this).addClass("demo-active")
                    })),
                o(),
               // selectAddon(),
                passwordShowIcon(),
                inputSearchAddon();
        },
        load: function () {

            n(),
           // selectAddon(),
            t();
        },
        resize: function () {
            o()
        },
        handleMenuPosition: function () {
            e > 1024 && $(".metismenu  li").unbind().each(function (e) {
                if ($("ul", this).length > 0) {
                    var t = (o = $("ul:first", this).css("display", "block")).offset().left,
                        n = o.width(),
                        o = $("ul:first", this).removeAttr("style"),
                        l = ($("body").height(), $("body").width());
                    if (jQuery("html").hasClass("rtl")) var c = t + n <= l;
                    else c = t > 0;
                    c ? $(this).find("ul:first").removeClass("left") : $(this).find("ul:first").addClass("left")
                }
            })
        }
    }
}();
jQuery(document).ready(function () { $('[data-toggle="popover"]').popover(), perfect.init() }),
    jQuery(window).on("load", function () { "use strict"; perfect.load(), setTimeout(function () { perfect.handleMenuPosition() }, 1e3) }),
    jQuery(window).on("resize", function () { "use strict"; perfect.resize(), setTimeout(function () { perfect.handleMenuPosition() }, 1e3) });


var dezSettingsOptions = {};

function getUrlParams(dParam) {
    var dPageURL = window.location.search.substring(1),
        dURLVariables = dPageURL.split('&'),
        dParameterName,
        i;

    for (i = 0; i < dURLVariables.length; i++) {
        dParameterName = dURLVariables[i].split('=');

        if (dParameterName[0] === dParam) {
            return dParameterName[1] === undefined ? true : decodeURIComponent(dParameterName[1]);
        }
    }
}

(function ($) {

    "use strict"

    var direction = getUrlParams('dir');

    dezSettingsOptions = {
        typography: "poppins",
        version: "light",
        layout: "vertical",
        headerBg: "color_1",
        navheaderBg: "color_3",
        sidebarBg: "color_3",
        sidebarStyle: "full",
        sidebarPosition: "fixed",
        headerPosition: "fixed",
        containerLayout: "full",
        direction: direction
    };


    if (direction == 'rtl') {
        direction = 'rtl';
    } else {
        direction = 'ltr';
    }

    new dezSettings(dezSettingsOptions);

    jQuery(window).on('resize', function () {
        /*Check container layout on resize */
        dezSettingsOptions.containerLayout = $('#container_layout').val();
        /*Check container layout on resize END */

        new dezSettings(dezSettingsOptions);
    });

})(jQuery);