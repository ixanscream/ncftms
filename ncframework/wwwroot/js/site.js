// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var baseUrl = window.location.origin;

var userLang = navigator.language || navigator.userLanguage;
$(function () {

    $('select').select2();

    //$('input[type="submit"],#displayOverlay').on("click", function () {
    //    document.getElementById("overlay").style.display = "block";
    //});


    var container = "<div class='table-responsive'></div>";
    $("table").wrap(container);
    $("table").addClass('table-hover table-bordered table-sm');
    

    //$('input[type=datetime],input[type=date]').datepicker({
    //    format: 'mm/dd/yyyy'
    //});

});

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}
