﻿$(document).ready(function () {

    $('.datepicker').datepicker({
        format: 'dd-mm-yyyy',
        autoclose: true,
        startDate: '0d'
    });

    $('.cell').click(function () {
        $('.cell').removeClass('select');
        $(this).addClass('select');
        $("#selectedTime").text(this.innerHTML);
        $("#AppointmentTime").val(this.innerHTML);
    });

    $("#dp1").on('change', function () {
        $("#selectedDate").text(this.value);
        $("#AppointmentDate").val(this.value);
    });
});