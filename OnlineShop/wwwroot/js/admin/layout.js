


(function ($) {
    "use strict"; // Start of use strict

    // Toggle the side navigation
    $("#sidebarToggle").on('click', function (e) {
        e.preventDefault();
        $("body").toggleClass("sidebar-toggled");
        $(".sidebar").toggleClass("toggled");
    });

    // Prevent the content wrapper from scrolling when the fixed side navigation hovered over
    $('body.fixed-nav .sidebar').on('mousewheel DOMMouseScroll wheel', function (e) {
        if ($(window).width() > 768) {
            var e0 = e.originalEvent,
                delta = e0.wheelDelta || -e0.detail;
            this.scrollTop += (delta < 0 ? 1 : -1) * 30;
            e.preventDefault();
        }
    });

    // Scroll to top button appear
    $(document).on('scroll', function () {
        var scrollDistance = $(this).scrollTop();
        if (scrollDistance > 100) {
            $('.scroll-to-top').fadeIn();
        } else {
            $('.scroll-to-top').fadeOut();
        }
    });

    // Smooth scrolling using jQuery easing
    $(document).on('click', 'a.scroll-to-top', function (event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top)
        }, 1000, 'easeInOutExpo');
        event.preventDefault();
    });

})(jQuery);

$(document).ready(async () => {
    $(".input-date").datepicker({
        languaje: "es",
        format: "dd/mm/yyyy",
        autoclose: true,
        endDate: new Date()
    });
    $('.input-date-now').datepicker({
        format: "dd/mm/yyyy",
        endDate: new Date(),
        languaje: "es",
        autoclose: true
    });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })

    $(".select-search").selectpicker();

    await getImageProfile();
});

/**
 * get profile image in admin
 * */
const getImageProfile = async () => {
    const img = $("#profile-img");
    const result = await axios.get("/account/getImageProfile");
    if (result.data !== null) {
        img[0].src = `/uploads/${result.data}`;
    }
};

