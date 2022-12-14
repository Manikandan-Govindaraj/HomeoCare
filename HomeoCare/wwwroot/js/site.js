
(function ($) {
    MakePayment = function (options) {

        var opts = $.extend({
            "currency": "INR",
            "name": "Kovai Cart",
            "description": "Test Transaction",
            "image": "https://www.reckonspace.com/Assets/images/ReckonSpace_Google_Logo.png",
            "handler": function (response) {
                PaymentSuccess(response);
            },
            "notes": {
                "address": "Razorpay Corporate Office"
            },
            "theme": {
                "color": "#F37254"
            },
            "retry": {
                "enabled": false
            },
            "modal": {
                "confirm_close": true,
                ondismiss: function () { alert("Model closed."); }
            }
        }, options);
        return opts;
    };

    customNotify = function (options) {
        PNotify.defaults.styling = "material";
        PNotify.defaults.icons = "material";
        if (typeof window.stackTopCenter === 'undefined') {
            window.stackTopCenter = new PNotify.Stack({
                dir1: 'down',
                firstpos1: 25,
                modal: false,
                maxOpen: 1,
                maxStrategy: 'close',
                maxClosureCausesWait: false,
            });
        }

        var opts = $.extend({
            text: "Check me out. I'm in a different stack.",
            stack: window.stackTopCenter,
            sticker: false,
            closerHover: false,
            type: 'success'
        }, options);

        PNotify.alert(opts);
    };
}(jQuery));