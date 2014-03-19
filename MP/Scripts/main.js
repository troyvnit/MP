﻿
$(document).ready(function () {
    $("#departureDateDatePicker").kendoDatePicker({
        format: "dd/MM/yyyy",
        change: function (e) {
            changeDepartureInfo();
        }
    });
    var datepicker = $("#departureDateDatePicker").data("kendoDatePicker");
    $("#today").on('click', function() {
        datepicker.value(new Date());
        datepicker.trigger('change');
    });
    $("#nextDay").on('click', function () {
        datepicker.value(new Date(datepicker.value().setDate(datepicker.value().getDate() + 1)));
        datepicker.trigger('change');
    });
    $("#lastDay").on('click', function () {
        datepicker.value(new Date(datepicker.value().setDate(datepicker.value().getDate() - 1)));
        datepicker.trigger('change');
    });
    $("#printScreen").on('click', function () {
        printScreen();
    });
    $("#printScreenPassengerList").on('click', function () {
        printScreenPassengerList();
    });
    $('#tripContentModal').on('show.bs.modal', function (e) {
        $('#SeatNumber').val(e.relatedTarget.dataset.seatNumber != undefined ? e.relatedTarget.dataset.seatNumber : e.relatedTarget.dataset.defaultSeatNumber);
        $('#Id').val(e.relatedTarget.dataset.id);
        $('#Name').val(e.relatedTarget.dataset.name);
        $('#Address').val(e.relatedTarget.dataset.address);
        $('#Phone').val(e.relatedTarget.dataset.phone);
        $('#TicketQuantity').val(e.relatedTarget.dataset.ticketQuantity != undefined ? e.relatedTarget.dataset.ticketQuantity : 1);
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
        return false;
    });
    $("#submitPassenger").on('click', function () {
        if (!$("#tripContentForm").valid()) {
            return false;
        }
        $('form input').removeAttr("disabled");
        var ticketQuantity = parseInt($('#TicketQuantity').val());
        var seatNumbers = [];
        $('#SeatNumber').val($('#SeatNumber').val().split(',')[0]);
        for (var i = 0; i < ticketQuantity; i++) {
            seatNumbers.push(parseInt($('#SeatNumber').val()) + i);
        }
        $('#SeatNumber').val(seatNumbers.join(','));
        $.post('/Home/AddOrUpdatePassenger', $('form').serialize()).done(function (result) {
            if (result) {
                getPassenger();
                $('#tripContentModal').modal("hide");
            } else {
                $('#tripContentModal').modal("show");
            }
        });
    });
    var changeDepartureInfo = function () {
        $("#departureInfoLabel").html('<h3>Tuyến ' + $("#TripName").attr('text') + ', Ngày ' + kendo.toString(datepicker.value(), "dd/MM/yyyy") + ', Chuyến <span class="label label-success">' + $("label.active").text() + '</span></h3>');
        $("#DepartureTime").val($("label.active > input:radio[name='departuretimes']").val());
        $("#DepartureDate").val(kendo.toString(datepicker.value(), "dd/MM/yyyy"));
        getPassenger();
    };
    var getPassenger = function () {
        $("span[class^='seat-number-']").html('');
        $("span[class^='seat-number-']").parent().removeAttr("data-id");
        $("span[class^='seat-number-']").parent().removeAttr("data-name");
        $("span[class^='seat-number-']").parent().removeAttr("data-phone");
        $("span[class^='seat-number-']").parent().removeAttr("data-address");
        $("span[class^='seat-number-']").parent().removeAttr("data-ticket-quantity");
        $("span[class^='seat-number-']").parent().removeAttr("data-town");
        $("span[class^='seat-number-']").parent().removeAttr("data-note");
        $("span[class^='seat-number-']").parent().removeAttr("data-seat-number");
        $.get('/Home/GetPassenger', { DepartureDate: kendo.toString(datepicker.value(), "yyyy/MM/dd"), DepartureTime: $("#DepartureTime").val(), TripName: $("#TripName").val() }).done(function (result) {
            $.each(result, function () {
                var seatNumbers = this.SeatNumber.split(',');
                var textColor = 'rgb('
                                + (Math.floor(Math.random() * 256)) + ','
                                + (Math.floor(Math.random() * 256)) + ','
                                + (Math.floor(Math.random() * 256)) + ')';
                for (var i = 0; i < seatNumbers.length; i++) {
                    $(".seat-number-" + seatNumbers[i]).css('color', textColor);
                    var seatInfo = '<strong>' + this.Phone + ' (' + this.TicketQuantity + ' vé)</strong><br/>';
                    seatInfo += this.Name != null ? 'Tên: ' + this.Name + '<br/>' : '';
                    seatInfo += this.Address != null ? 'Đón tại: ' + this.Address + ' (' + $('#Town option[value=' + this.Town + ']').text() + ')<br/>' : '';
                    seatInfo += this.Note != null ? 'Ghi chú: ' + this.Note : '';
                    $(".seat-number-" + seatNumbers[i]).html(seatInfo);
                    $(".seat-number-" + seatNumbers[i]).parent().attr("data-id", this.Id);
                    $(".seat-number-" + seatNumbers[i]).parent().attr("data-name", this.Name);
                    $(".seat-number-" + seatNumbers[i]).parent().attr("data-phone", this.Phone);
                    $(".seat-number-" + seatNumbers[i]).parent().attr("data-address", this.Address);
                    $(".seat-number-" + seatNumbers[i]).parent().attr("data-ticket-quantity", this.TicketQuantity);
                    $(".seat-number-" + seatNumbers[i]).parent().attr("data-town", this.Town);
                    $(".seat-number-" + seatNumbers[i]).parent().attr("data-note", this.Note);
                    $(".seat-number-" + seatNumbers[i]).parent().attr("data-seat-number", this.SeatNumber);
                }
            });
        });
    };
    $("#departureTimeButtonGroup > label.active").trigger("click");
    $('#passengerListModal').on('show.bs.modal', function (e) {
        $("#passengerGrid").kendoGrid({
            dataSource: {
                transport: {
                    read: "/Home/GetPassenger",
                    type: "POST",
                    parameterMap: function (options, operation) {
                            return {
                                DepartureDate: kendo.toString(datepicker.value(), "yyyy/MM/dd"),
                                DepartureTime: $("#DepartureTime").val(),
                                TripName: $("#TripName").val()
                            };
                    }
                }
            },
            height: 600,
            sortable: true,
            filterable: true,
            columns: [{
                field: "Phone",
                title: "Điện thoại",
                width: 100
            }, {
                field: "Address",
                title: "Địa điểm đón",
                width: 200
            }, {
                field: "TicketQuantity",
                title: "SL Vé",
                width: 70
            }, {
                field: "Name",
                title: "Tên hành khách",
                width: 120
            }, {
                field: "Note",
                title: "Ghi chú",
                width: 120
            }
            ]
        });
    });
    
    $("#selectTown").multiselect({
        includeSelectAllOption: true
    });
    $("#filterTown").on('click', function() {
        var dataSource = $("#passengerGrid").data("kendoGrid").dataSource;
        dataSource.filter({
            "field": "Town",
            "operator": function(item) {
                var items = $("#selectTown").val();
                console.log($.inArray(item, items));
                return $.inArray(item, items) != -1;
            }
    });
    });
    function startChange() {
        var startDate = from.value(),
        endDate = to.value();

        if (startDate) {
            startDate = new Date(startDate);
            startDate.setDate(startDate.getDate());
            to.min(startDate);
        } else if (endDate) {
            from.max(new Date(endDate));
        } else {
            endDate = new Date();
            from.max(endDate);
            to.min(endDate);
        }
        $("#itemGrid").data("kendoGrid").dataSource.read();
    }

    function endChange() {
        var toDate = to.value(),
        fromDate = from.value();

        if (toDate) {
            toDate = new Date(toDate);
            toDate.setDate(toDate.getDate());
            from.max(toDate);
        } else if (fromDate) {
            to.min(new Date(fromDate));
        } else {
            toDate = new Date();
            from.max(toDate);
            to.min(toDate);
        }
        $("#itemGrid").data("kendoGrid").dataSource.read();
    }

    var from = $("#fromDateDatePicker").kendoDatePicker({
        format: "dd/MM/yyyy",
        change: startChange
    }).data("kendoDatePicker");

    var to = $("#toDateDatePicker").kendoDatePicker({
        format: "dd/MM/yyyy",
        change: endChange
    }).data("kendoDatePicker");

    from.max(to.value());
    to.min(from.value());
    var itemDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Home/GetItem",
                type: "post"
            },
            update: {
                url: "/Home/AddOrUpdateItem",
                type: "post"
            },
            destroy: {
                url: "/Home/DeleteItem",
                type: "post",
            },
            create: {
                url: "/Home/AddOrUpdateItem",
                type: "post"
            },
            parameterMap: function (options, operation) {
                if (operation !== "read" && options.models) {
                    return {
                        models: kendo.stringify(options.models),
                        DepartureDate: options.models[0].TripDepartureDate == "" ? kendo.toString(datepicker.value(), "yyyy/MM/dd") : options.models[0].TripDepartureDate,
                        DepartureTime: options.models[0].TripDepartureTime == "" ? $("#DepartureTime").val() : options.models[0].TripDepartureTime,
                        TripName: $("#TripName").val()
                    };
                } else {
                    return {
                        fromDate: kendo.toString(from.value(), "yyyy/MM/dd"),
                        toDate: kendo.toString(to.value(), "yyyy/MM/dd"),
                        fromTime: $("#fromTime").val(),
                        toTime: $("#toTime").val(),
                        TripName: $("#TripName").val(),
                        skip: options.skip,
                        take: options.take
                    };
                }
            }
        },
        batch: true,
        pageSize: 10,
        serverPaging: true,
        schema: {
            model: {
                id: "Id",
                fields: {
                    Id: { editable: false, nullable: false, defaultValue: 0 },
                    ItemCode: { type: "string" },
                    Description: { type: "string" },
                    SenderName: { type: "string" },
                    SenderPhone: { type: "string" },
                    ReceiverName: { type: "string" },
                    ReceiverPhone: { type: "string" },
                    DeliveryAddress: { type: "string" },
                    Note: { type: "string" },
                    TripDepartureDate: { type: "string" },
                    TripDepartureTime: { type: "string" }
                }
            },
            total: "total",
            data: "data"
        }
    });
    
    $("#itemGrid").kendoGrid({
        dataSource: itemDataSource,
        navigatable: true,
        pageable: true,
        groupable: {
            messages: {
                empty: "Kéo thả tên cột vào đây để xem theo nhóm."
            }
        },
        height: 500,
        toolbar: [{ name: "create", text: "Thêm" }, { name: "save", text: "Lưu" }, { name: "cancel", text: "Hủy" }],
        edit: function(e) {
            if (e.model.isNew()) {
                e.model.set("TripDepartureDate", kendo.toString(datepicker.value(), "dd/MM/yyyy"));
                e.model.set("TripDepartureTime", $("#DepartureTime").val());
            }
        },
        columns: [
            { field: "ItemCode", title: "Mã", width: 50 },
            { field: "Description", title: "Mô tả", width: 200 },
            { field: "ReceiverName", title: "Người nhận" },
            { field: "ReceiverPhone", title: "SĐT nhận", width: 100 },
            { field: "SenderName", title: "Người gửi", hidden: true },
            { field: "SenderPhone", title: "SĐT gửi", width: 100, hidden: true },
            { field: "DeliveryAddress", title: "Địa chỉ giao hàng", width: 200 },
            { field: "Note", title: "Ghi chú" },
            {
                field: "TripDepartureDate",
                title: "Ngày", width: 100
            },
            {
                field: "TripDepartureTime",
                title: "Chuyến", width: 70
            },
            { command: [{ name: "destroy", title: "&nbsp;", width: 70, text: "Xóa" }] }
        ],
        editable: true
    });
    $("#fromTime").on('change', function () {
        $("#itemGrid").data("kendoGrid").dataSource.read();
    });
    $("#toTime").on('change', function () {
        $("#itemGrid").data("kendoGrid").dataSource.read();
    });
    $("#showAllItems").on('click', function () {
        from.value(0);
        to.value(0);
        $("#fromTime").val('');
        $("#toTime").val('');
        $("#itemGrid").data("kendoGrid").dataSource.read();
    });
    $("#showAllItems").trigger('click');
    $("#tripContentForm").validate({
        rules: {
            Phone: "required",
            Address: "required",
            TicketQuantity: {
                required: true,
                number: true,
                min: 1
            }
        },
        messages: {
            Phone: {
                required: "Vui lòng nhập số điện thoại"
            },
            Address: {
                required: "Vui lòng nhập địa điểm đón"
            },
            TicketQuantity: {
                required: "Vui lòng nhập số lượng vé",
                number: "Vui lòng nhập số",
                min: "Nhỏ nhất là 1"
            }
        }
    });
});
function printScreen() {
    html2canvas($("#printScreenContent"), {
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
            var doc = new jsPDF();
            doc.addImage(dataUrl, 'JPEG', 0, 0, null, null);
            doc.save('SM_' + kendo.toString($("#departureDateDatePicker").data("kendoDatePicker").value(), "dd_MM_yyyy") + '_' + $("#DepartureTime").val().replace(':', '_') + '.pdf');
        }
    });
} 
function printScreenPassengerList() {
    html2canvas($("#passengerGrid"), {
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
            var doc = new jsPDF();
            doc.addImage(dataUrl, 'JPEG', 0, 0, null, null);
            var selectTowns = $("#selectTown").val() != null ? $("#selectTown").val().join('_') : "All";
            doc.save('PLFT' + kendo.toString($("#departureDateDatePicker").data("kendoDatePicker").value(), "dd_MM_yyyy") + '_' + $("#DepartureTime").val().replace(':', '_') + '_' + selectTowns + '.pdf');
        }
    });
}