$(document).ready(function (e) {
    $('#submit').click(function (e) {

        if ($('#addition').val() == "") {
            $('#errorMsg').text('Enter the value').css("color", "red");
            $('#addition').focus();
            e.preventDefault();
            return false;
        }
        else {
            $('#errorMsg').text('');
        }
        SaveData();
        e.preventDefault();
        return false;
    });

    function SaveData() {
        $("#coverScreen").show();
        var additionData = {
            additionValue: $('#addition').val()
        };

        $.ajax({
            async: true,
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset-8',
            data: JSON.stringify(additionData),
            url: '/addition/SaveAddition',
            success: function (response) {
                if (response.success === true) {
                    $("#coverScreen").hide();
                    toastr.success('Yes! You have successfully submitted data !', 'success', { timeOut: 3000, "closeButton": true });
                    clear();
                    location.reload();
                }
                else {
                    toastr.error('Some error occured', 'Error Alert', { timeOut: 3000, "closeButton": true });
                }
            },
            error: function () {
                toastr.error('There is some problem to process your request', 'Error Alert', { timeOut: 3000, "closeButton": true });
            }
        });
    }
    $('#cancelbtn').click(function (e) {
        clear();
    });
    function clear() {
        $('#addition').val('');
    }

    //Edit event handler.
    $("body").on("click", "#tabledata .Edit", function () {
        var row = $(this).closest("tr");
        $("td", row).each(function () {
            if ($(this).find("input").length > 0) {
                $(this).find("input").show();
                $(this).find("span").hide();
            }
        });

        row.find(".Update").show();
        row.find(".Cancel").show();
        row.find(".Delete").hide();
        $(this).hide();
    });

    //Cancel event handler.
    $("body").on("click", "#tabledata .Cancel", function () {
        var row = $(this).closest("tr");
        $("td", row).each(function () {
            if ($(this).find("input").length > 0) {
                var span = $(this).find("span");
                var input = $(this).find("input");
                input.val(span.html());
                span.show();
                input.hide();
            }
        });
        row.find(".Edit").show();
        row.find(".Delete").show();
        row.find(".Update").hide();
        $(this).hide();
    });

    //Update event handler.
    $("body").on("click", "#tabledata .Update", function () {
        var row = $(this).closest("tr");
        $("td", row).each(function () {
            if ($(this).find("input").length > 0) {
                var span = $(this).find("span");
                var input = $(this).find("input");
                span.html(input.val());
                span.show();
                input.hide();
            }
        });
        row.find(".Edit").show();
        row.find(".Delete").show();
        row.find(".Cancel").hide();
        $(this).hide();

        var Data = {};
        Data.Id = row.find(".id").find("span").html();
        Data.additionValue = row.find(".addition").find("span").html();
        $.ajax({
            async: true,
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset-8',
            data: JSON.stringify(Data),
            url: '/addition/updateAdditionData',
            success: function (response) {
                if (response.success === true) {
                    //$("#coverScreen").hide();
                    //toastr.success('Yes! You have successfully submitted data !', 'success', { timeOut: 3000, "closeButton": true });
                    //location.reload();
                    //clear();
                }
                else {
                    toastr.error('Some error occured', 'Error Alert', { timeOut: 3000, "closeButton": true });
                }
            },
            error: function () {
                toastr.error('There is some problem to process your request', 'Error Alert', { timeOut: 3000, "closeButton": true });
            }
        });
    });

    //Delete event handler.
    $("body").on("click", "#tabledata .Delete", function () {
        if (confirm("Do you want to delete this row?")) {
            var row = $(this).closest("tr");
            var Id = row.find("span").html();

            $.ajax({
                async: true,
                type: 'POST',
                dataType: 'JSON',
                contentType: 'application/json; charset-8',
                data: '{Id: ' + Id + '}',
                url: '/addition/deleteData',
                success: function (response) {
                    if (response.success === true) {
                        if ($("#tabledata tr").length > 2) {
                            row.remove();
                        } else {
                            row.find(".Edit").hide();
                            row.find(".Delete").hide();
                            row.find("span").html('&nbsp;');
                        }
                    }
                    else {
                        toastr.error('Some error occured', 'Error Alert', { timeOut: 3000, "closeButton": true });
                    }
                },
                error: function () {
                    toastr.error('There is some problem to process your request', 'Error Alert', { timeOut: 3000, "closeButton": true });
                }
            });
        }
    });
});