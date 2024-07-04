

var $favo = $('.perfect-quick-menu-list');

$favo.owlCarousel({
    loop: false,
    autoplay: false,
    margin: 10,
    nav: true,
    dots: true,
    left: true,
    items: 15,
    autoWidth: true,
    //rtl: ,
    navText: ['', ''],
    //responsive: {
    //    0: {
    //        items: 1
    //    },
    //    600: {
    //        items: 2
    //    },
    //    991: {
    //        items: 3
    //    },

    //    1200: {
    //        items: 4
    //    },
    //    1600: {
    //        items: 5
    //    }
    //}
});
$favo.on('mousewheel', '.owl-stage', function (e) {
   
    if (e.originalEvent.deltaY> 0) {
        $favo.trigger('next.owl');
    }
    else {
        $favo.trigger('prev.owl');
    }
    e.preventDefault();
});

$('.perfect-quick-menu-list-nextbtn').click(function () {
    $favo.trigger('next.owl')
});
$('.perfect-quick-menu-list-prevbtn').click(function () {
    $favo.trigger('prev.owl')
});



//------------

$('perfect-sidenav-menu').click(function () {


});

