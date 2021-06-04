
function findMypass() {
    Swal.fire({
        title: "Ingresa tu correo",
        type: "info",
        html: "<input type='email' id='email' required />"
    }).then((val) => {
        let email = document.getElementById('email').value;
        if (val) {
            if (email.length > 0) {
                Swal.fire({
                    title: "información",
                    text: "Espere un momento",
                    showCancelButton: false,
                    showConfirmButton: false,
                    allowOutsideClick: false
                });
                fetch('/Home/RecoveryPassword/?email=' + email).then((va) => {
                    if (va) {
                        Swal.close();
                    }
                    return va.json();
                }).then((res) => {
                    Swal.fire({
                        title: "Correo enviado",
                        type: "success",
                        text: "Porfavor revisa tu email"
                    });
                }).catch((e) => {
                    Swal.fire({
                        title: "Lo sentimos este correo no existe",
                        type: "error"
                    });
                    console.log("erro" + e);
                });
            }
        }
    }).catch((e) => console.log("erro => " + e));
}

$(function () {
    "use strict";

    //------- Parallax -------//
    skrollr.init({
        forceHeight: false
    });



    //------- hero carousel -------//
    $(".hero-carousel").owlCarousel({
        items: 3,
        margin: 10,
        autoplay: false,
        autoplayTimeout: 5000,
        loop: true,
        nav: false,
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            810: {
                items: 3
            }
        }
    });

    //------- Best Seller Carousel -------//
    if ($('.owl-carousel').length > 0) {
        $('#bestSellerCarousel').owlCarousel({
            loop: true,
            margin: 30,
            nav: true,
            navText: ["<i class='ti-arrow-left'></i>", "<i class='ti-arrow-right'></i>"],
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                900: {
                    items: 3
                },
                1130: {
                    items: 4
                }
            }
        });
    }

    //------- single product area carousel -------//
    $(".s_Product_carousel").owlCarousel({
        items: 1,
        autoplay: false,
        autoplayTimeout: 5000,
        loop: true,
        nav: false,
        dots: false
    });


    //------- fixed navbar --------//  
    $(window).scroll(function () {
        var sticky = $('.header_area'),
            scroll = $(window).scrollTop();

        if (scroll >= 100) sticky.addClass('fixed');
        else sticky.removeClass('fixed');
    });

    //------- Price Range slider -------//
    if (document.getElementById("price-range")) {

        var nonLinearSlider = document.getElementById('price-range');

        noUiSlider.create(nonLinearSlider, {
            connect: true,
            behaviour: 'tap',
            start: [500, 4000],
            range: {
                // Starting at 500, step the value by 500,
                // until 4000 is reached. From there, step by 1000.
                'min': [0],
                '10%': [500, 500],
                '50%': [4000, 1000],
                'max': [10000]
            }
        });


        var nodes = [
            document.getElementById('lower-value'), // 0
            document.getElementById('upper-value')  // 1
        ];

        // Display the slider value and how far the handle moved
        // from the left edge of the slider.
        nonLinearSlider.noUiSlider.on('update', function (values, handle, unencoded, isTap, positions) {
            nodes[handle].innerHTML = values[handle];
        });

    }

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })

});


/**
 * get count items of cart
 * */
const getTotalCountCartItems = async () => {
    const fetch_result = await fetch("/cartItem/getTotal");
    const result = await fetch_result.json();
    $('#car-count').text(result);
}

/*Custon**/
$(document).ready(async function () {
    
    /**
     *Mask for phone Numbers
     */
    $(".phone-number").mask("000-000-0000");
    $('.money').mask("#,##0.00", { reverse: true });
    await getTotalCountCartItems();

    /*Date pickers configuration*/
    $(".input-date").datepicker({
        languaje: "es",
        format: "dd/mm/yyyy"
    })

    $('.input-date-now').datepicker({
        format: "dd/mm/yyyy",
        endDate: new Date(),
        languaje:"es"
    })


});


