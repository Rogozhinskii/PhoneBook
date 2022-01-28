// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function openSuccessModal(message) {
    var div = document.getElementById("ModalSuccesAlertBody");
    div.innerHTML = message;
    $('#modalSuccess').modal('show');
}


