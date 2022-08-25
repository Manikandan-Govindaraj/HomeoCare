jQuery(document).ready(function ($) {
    "use strict";
    // Page loading animation

    //$("#preloader").animate({
    //    'opacity': '0'
    //}, 600, function () {
    //    setTimeout(function () {
    //        $("#preloader").css("visibility", "hidden").fadeOut();
    //    }, 300);
    //});


    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        var box = $('.header-text').height();
        var header = $('header').height();

        if (scroll >= box - header) {
            $("header").addClass("background-header");
        } else {
            $("header").removeClass("background-header");
        }
    });

    $('.accordion > li:eq(0) a').addClass('active').next().slideDown();

    $('.aaccordion').click(function (j) {
        if ($('.addressheader').hasClass('active')) {
            $('.addresscontent').slideUp();
            $('.addressheader').removeClass('active');
            $('.cartheader').addClass('active');
            $('.cartcontent').slideDown();
        } else {
            $('.cartcontent').slideUp();
            $('.cartheader').removeClass('active');
            $('.addressheader').addClass('active');
            $('.addresscontent').slideDown();
        }
        j.preventDefault();
    });


    $('.movetosummery').click(function (j) {
        $('.addresscontent').slideUp();
        $('.addressheader').removeClass('active');
        $('.cartheader').addClass('active');
        $('.cartcontent').slideDown();
        $('.cartcontent').focus();
        
        j.preventDefault();
    });
});
