// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Disable Button when no search text
$(document).ready(function () {
    $('Button[type="submit"]').attr('disabled', true);
    $('input[type="text"],textarea').on('keyup', function () {
        var text_value = $('input[name="searchString"]').val();
        if ( text_value != '') {
            $('Button[type="submit"]').attr('disabled', false);
        } else {
            $('Button[type="submit"]').attr('disabled', true);
        }
    });
});

//Carousel
