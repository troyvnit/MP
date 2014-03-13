
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
    html2canvas($("#printScreen"), {
        onrendered: function (canvas) {
            var extraCanvasWidth = canvas.width / 3 * 2;
            var extraCanvasHeight = canvas.height / 3 * 2;
            var extraCanvas = document.createElement("canvas");
            extraCanvas.setAttribute('width', extraCanvasWidth);
            extraCanvas.setAttribute('height', extraCanvasHeight);
            var ctx = extraCanvas.getContext('2d');
            ctx.fillStyle = 'white';
            ctx.fillRect(0, 0, canvas.width, canvas.height);
            ctx.drawImage(canvas, 0, 0, canvas.width, canvas.height, 0, 0, extraCanvasWidth, extraCanvasHeight);
            var dataUrl = extraCanvas.toDataURL("image/jpeg", 2);
            $('body').append(canvas);
            var doc = new jsPDF();
            doc.addImage(dataUrl, 'JPEG', 0, 0, null, null);
            doc.save('Test.pdf');
        }
    });
}