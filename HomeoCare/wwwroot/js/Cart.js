﻿
/* Assign actions */
$('.product-quantity input').change(function () {
    updateQuantity(this);
});

$('.product-removal button').click(function () {
    removeItem(this);
});


/* Recalculate cart */
function recalculateCart() {
    var subtotal = 0;

    /* Sum up row totals */
    $('.product').each(function () {
        subtotal += parseFloat($(this).children('.product-line-price').text());
    });

    /* Calculate totals */
    var tax = subtotal * taxRate / 100;
    var shipping = (subtotal > 0 ? shippingRate : 0);
    var total = subtotal + tax + shipping;

    /* Update totals display */
    $('.totals-value').fadeOut(fadeTime, function () {
        $('[Id*="cart-subtotal"]').html(subtotal.toFixed(2));
        $('[Id*="cart-tax"]').html(tax.toFixed(2));
        $('[Id*="cart-shipping"]').html(shipping.toFixed(2));
        $('[Id*="cart-total"]').html(total.toFixed(2));
        if (subtotal == 0) {
            $('.checkout').fadeOut(fadeTime);
            $('.btnProducts').fadeIn(fadeTime);
        } else {
            $('.checkout').fadeIn(fadeTime);
        }
        $('.totals-value').fadeIn(fadeTime);
    });
}


/* Update quantity */
function updateQuantity(quantityInput) {
    /* Calculate line price */
    var productRow = $(quantityInput).parent().parent();
    var price = parseFloat(productRow.children('.product-price').text());
    var quantity = $(quantityInput).val();
    var linePrice = price * quantity;

    /* Update line price display and recalc cart totals */
    productRow.children('.product-line-price').each(function () {
        $(this).fadeOut(fadeTime, function () {
            $(this).text(linePrice.toFixed(2));
            recalculateCart();
            $(this).fadeIn(fadeTime);
        });
    });
}


/* Remove item from cart */
function removeItem(removeButton) {
    /* Remove row from DOM and recalc cart total */
    var productRow = $(removeButton).parent().parent();
    productRow.slideUp(fadeTime, function () {
        productRow.remove();
        recalculateCart();
    });

    $.post(RemoveCartItemURI, {
        ProductID: $(removeButton).data("productid")
    }, function (data) {
        $(".cartitemcount").html(data.cartitemcount);
    });
}

function Calc(input) {

    $.post(UpdateCartURI, {
        ProductID: $(input).data("productid"),
        Quantity: $(input).val()
    }, function (data) {

    });
}