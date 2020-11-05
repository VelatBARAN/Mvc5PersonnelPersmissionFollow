$(function () {
    $('.date-picker').datepicker({
        dateFormat: "dd.MM.yyyy",
        changeMonth: true,
        changeYear: true,
        showOn: "both",
        autoclose:true
    }).val();
});