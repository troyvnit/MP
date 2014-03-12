
$(document).ready(function () {
    $("#departureDateDatePicker").kendoDatePicker({
        format: "dd/MM/yyyy",
        change: function (e) {
            changeDepartureInfo();
        }
    });
    var datepicker = $("#departureDateDatePicker").data("kendoDatePicker");
    $('#tripContentModal').on('show.bs.modal', function (e) {
        $('#SeatNumber').val(e.relatedTarget.dataset.seatNumber);
    });
    $("#departureTimeButtonGroup > label").on('click', function (e) {
        $("#departureTimeButtonGroup > label").removeClass("active");
        $(this).addClass("active");
        changeDepartureInfo();
        getPassenger();
        demoFromHTML();
        return false;
    });
    $("#Name").not("[type=submit]").jqBootstrapValidation();
    $("#submitPassenger").on('click', function () {
        $('form input').removeAttr("disabled");
        $.post('/Home/AddOrUpdatePassenger',  $('form').serialize()).done(function () {
            getPassenger();
        });
    });
    var changeDepartureInfo = function () {
        $("#departureInfoLabel").html('<h3>Ngày ' + kendo.toString(datepicker.value(), "dd/MM/yyyy") + ', Chuyến <span class="label label-success">' + $("label.active > input:radio[name='departuretimes']").val() + '</span></h3>');
        $("#DepartureTime").val($("label.active > input:radio[name='departuretimes']").val());
        $("#DepartureDate").val(kendo.toString(datepicker.value(), "dd/MM/yyyy"));
    };
    var getPassenger = function () {
        $("span[class^='seat-number-']").html('');
        $.get('/Home/GetPassenger', { DepartureDate: $("#DepartureDate").val(), DepartureTime: $("#DepartureTime").val() }).done(function (result) {
            $.each(result, function () {
                $(".seat-number-" + this.SeatNumber).html('<strong>' + this.Name + '</strong><br/>' + this.Phone + '<br/>' + this.Address);
            });
        });
    };
    getPassenger();
});
function demoFromHTML() {
    html2canvas(document.body, {
        onrendered: function (canvas) {
            document.body.appendChild(canvas);
        }
    });
}