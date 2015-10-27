/*!
 * Start Bootstrap - Grayscale Bootstrap Theme (http://startbootstrap.com)
 * Code licensed under the Apache License v2.0.
 * For details, see http://www.apache.org/licenses/LICENSE-2.0.
 */

// jQuery to collapse the navbar on scroll
$(window).scroll(function () {
    var $navbar = $(".navbar");

    //var $detailslist = $(".row-details-list");
    //var $detailsafter = $("#details-after");
    //var topBreakPoint = $(this).scrollTop() - $detailslist.offset().top;
    //var bottomBreakPoint = $(this).scrollTop() - $(this).height();//$detailsafter.offset().top;// + $detailsafter.height();

    //if (topBreakPoint >= 0) {
    //    $("#details-slider").css("position", "fixed");
    //}
    //else {
    //    $("#details-slider").css("position", "relative");
    //}

    //console.log(topBreakPoint + " " + bottomBreakPoint);

    if (null != $navbar.offset()) {
        if ($navbar.offset().top > 50) {
            $(".navbar-fixed-top").addClass("top-nav-collapse");
            $(".brand").addClass("colored");
        } else {
            $(".navbar-fixed-top").removeClass("top-nav-collapse");
            $(".brand").removeClass("colored");
        }
    }
});

// jQuery for page scrolling feature - requires jQuery Easing plugin
$(function () {
    $('a.page-scroll').bind('click', function (event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });
});

// Closes the Responsive Menu on Menu Item Click
$('.navbar-collapse ul li a :not(.dropdown)').click(function () {
    $('.navbar-toggle:visible').click();
});
