﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var myIndex = 0;
carousel();

function carousel() {
    var i;
    var x = document.getElementsByClassName("mySlides");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    myIndex++;
    if (myIndex > x.length) { myIndex = 1 }
    x[myIndex - 1].style.display = "block";
    setTimeout(carousel, 4000); // Change image every 2 seconds
}


$(document).ready(function () {
    $('.helloWorld').on('click', function () {
        $.ajax({
            url: '@(Url.Action("Index", "Paintings"))',
            type: 'post',
            data: {
                Value1: 'Value1'
               
            },
            success: function (result) {
                //for redirect
                window.location = "url";
         
            }
        });
    });
});