// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function scrollToElement() {
    var $form = $('#compose-form'); // The form to scroll to

    if ($form.length) {
        // Scroll the page to the form's position
        $('html, body').animate({
            scrollTop: $form.offset().top
        }, 500); // Adjust the duration as needed (500ms in this case)
    } else {
        console.error('#compose-form not found');
    }
}

$(document).ready(function () {
    scrollToElement();
});

document.getElementById('imageInput').addEventListener('change', function (event) {
    const file = event.target.files[0];
    const preview = document.getElementById('imagePreview');

    if (file) {
        const reader = new FileReader();

        reader.onload = function (e) {
            preview.src = e.target.result;
            preview.style.display = 'block'; // Show the preview
        };

        reader.readAsDataURL(file); // Read the image file as data URL
    } else {
        preview.src = '#';
        preview.style.display = 'none';
    }
});