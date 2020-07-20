$(document).ready(function (e) {
     
    //approve event handler.
    $("body").on("click", "#tabledata .Approve", function () {
        $("#coverScreen").show();
        var row = $(this).closest("tr");              
        var Id = row.find(".id").find("span").html();       
        $.ajax({
            async: true,
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset-8',
            data: '{Id: ' + Id + '}',
            url: '/registrationApprove/Approve',
            success: function (response) {
                if (response.success === true) {
                    $("#coverScreen").hide();
                    toastr.success('Yes! your request has been approved !', 'success', { timeOut: 3000, "closeButton": true });
                    location.reload();
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

    //Disapprove event handler.
    $("body").on("click", "#tabledata .Disapprove", function () {
        $("#coverScreen").show();
        var row = $(this).closest("tr");
        var Id = row.find(".id").find("span").html();
        $.ajax({
            async: true,
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset-8',
            data: '{Id: ' + Id + '}',
            url: '/registrationApprove/DisApprove',
            success: function (response) {
                if (response.success === true) {
                    $("#coverScreen").hide();
                    toastr.success('Yes! your request has been DisApproved !', 'success', { timeOut: 3000, "closeButton": true });
                    location.reload();
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
});