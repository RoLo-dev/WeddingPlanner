// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const open = document.getElementById('open');
const close = document.getElementById('close');
const nav = document.getElementById('mobileNav');

open.addEventListener('click', reveal);
close.addEventListener('click', exit);

function reveal(){
    nav.classList.add('show');
    console.log('clicked');
}
function exit(){
    nav.classList.remove('show');
}

$(document).ready(function(){
    // Add smooth scrolling to all links
    $(".show").on('click', function(event) {

        if (this.hash !== "b") {
        // Prevent default anchor click behavior
        event.preventDefault();

        // Store hash
        var hash = this.hash;

        $('html, body').animate({
            scrollTop: $(hash).offset().top
        }, 600, function(){

            window.location.hash = hash;
        });
        } // End if
    });
});