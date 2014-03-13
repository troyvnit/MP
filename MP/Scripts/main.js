
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
        $('#Id').val(e.relatedTarget.dataset.id);
        $('#Name').val(e.relatedTarget.dataset.name);
        $('#Address').val(e.relatedTarget.dataset.address);
        $('#Phone').val(e.relatedTarget.dataset.phone);
        $('#TicketQuantity').val(e.relatedTarget.dataset.ticketQuantity);
        $('#Town option').removeAttr("selected");
        $('#Town option[value=' + e.relatedTarget.dataset.town + ']').attr("selected","");
        $('#Note').val(e.relatedTarget.dataset.note);
        $('#DepartureDate').attr("disabled", "");
        $('#DepartureTime').attr("disabled", "");
        $('#SeatNumber').attr("disabled", "");
    });
    $("#departureTimeButtonGroup > label").on('click', function (e) {
        $("#departureTimeButtonGroup > label").removeClass("active");
        $(this).addClass("active");
        changeDepartureInfo();
        getPassenger();
        return false;
    });
    $("#Name").not("[type=submit]").jqBootstrapValidation();
    $("#submitPassenger").on('click', function () {
        $('form input').removeAttr("disabled");
        $.post('/Home/AddOrUpdatePassenger', $('form').serialize()).done(function (result) {
            if (result) {
                getPassenger();
            }
            $('#tripContentModal').modal("hide");
        });
    });
    var changeDepartureInfo = function () {
        $("#departureInfoLabel").html('<h3>Tuyến ' + $("#tripName").val() + ', Ngày ' + kendo.toString(datepicker.value(), "dd/MM/yyyy") + ', Chuyến <span class="label label-success">' + $("label.active").text() + '</span></h3>');
        $("#DepartureTime").val($("label.active > input:radio[name='departuretimes']").val());
        $("#DepartureDate").val(kendo.toString(datepicker.value(), "dd/MM/yyyy"));
    };
    var getPassenger = function () {
        $("span[class^='seat-number-']").html('');
        $.get('/Home/GetPassenger', { DepartureDate: kendo.toString(datepicker.value(), "yyyy/MM/dd"), DepartureTime: $("#DepartureTime").val() }).done(function (result) {
            $.each(result, function () {
                $(".seat-number-" + this.SeatNumber).html('<strong>' + this.Name + '</strong><br/>' + this.Phone + '<br/>' + this.Address);
                $(".seat-number-" + this.SeatNumber).parent().attr("data-id", this.Id);
                $(".seat-number-" + this.SeatNumber).parent().attr("data-name", this.Name);
                $(".seat-number-" + this.SeatNumber).parent().attr("data-phone", this.Phone);
                $(".seat-number-" + this.SeatNumber).parent().attr("data-address", this.Address);
                $(".seat-number-" + this.SeatNumber).parent().attr("data-ticket-quantity", this.TicketQuantity);
                $(".seat-number-" + this.SeatNumber).parent().attr("data-town", this.Town);
                $(".seat-number-" + this.SeatNumber).parent().attr("data-note", this.Note);
            });
        });
    };
    $("#departureTimeButtonGroup > label.active").trigger("click");
});
function printScreen() {
    html2canvas($("#printScreen"), {
        onrendered: function (canvas) {
            var extraCanvasWidth = 794;
            var extraCanvasHeight = canvas.height * 794 / canvas.width;
            var extraCanvas = document.createElement("canvas");
            extraCanvas.setAttribute('width', extraCanvasWidth);
            extraCanvas.setAttribute('height', extraCanvasHeight);
            var ctx = extraCanvas.getContext('2d');
            ctx.fillStyle = 'white';
            ctx.fillRect(0, 0, canvas.width, canvas.height);
            ctx.drawImage(canvas, 0, 0, extraCanvasWidth, extraCanvasHeight);
            var dataUrl = extraCanvas.toDataURL("image/jpeg", 1);
            $('body').append(canvas);
            var doc = new jsPDF();
            doc.addImage(dataUrl, 'JPEG', 0, 0, null, null);
            doc.save('Test.pdf');
        }
    });
}