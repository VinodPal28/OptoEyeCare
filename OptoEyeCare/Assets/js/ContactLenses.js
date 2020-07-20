
$(document).ready(function (e) {
    $('.switch5').on('click', function () {     
        if ($(this).is(":checked")) {
            $('#gst').show();          
        }
         else {          
            $('#gst').hide();
            $('#gst').val('');
            var price = $('#totalPayment').val();
            var gst = $('#gst').val();
            if (price != '') {
                $('#grossAmount').text(price);
            }
            if (price != '' && gst != '') {
                var gstPrice = (parseFloat(price) * parseFloat(gst)) / parseFloat(100);
                var grossPrice = (parseFloat(price) + parseFloat(gstPrice));
                $('#grossAmount').text(grossPrice);
            }
        }
    });
  
    $("#totalPayment").keyup(function () {
        var price = $('#totalPayment').val();
        var gst = $('#gst').val();
        if(price !='')
        {
            $('#grossAmount').text(price);
        }
        if(price !='' && gst != '')
        {
            var gstPrice = (parseFloat(price) * parseFloat(gst)) / parseFloat(100);
            var grossPrice = (parseFloat(price) + parseFloat(gstPrice));
            $('#grossAmount').text(grossPrice);
        }           
    });
  
    $("#gst").keyup(function () {
        var price = $('#totalPayment').val();     
        var gst = $('#gst').val();
        if (price != '') {
            $('#grossAmount').text(price);
        }
        if (price != '' && gst != '') {
            var gstPrice = (parseFloat(price) * parseFloat(gst)) / parseFloat(100);
            var grossPrice = (parseFloat(price) + parseFloat(gstPrice));
            $('#grossAmount').text(grossPrice);
        }
    });      
    $('#contactLense').click(function (e) {
        $("#divHeader").html("Contact Lense information");
        $('#DivLense').show();
        $('#DivBtn').show();
    });
    $('#spect').click(function (e) {
        $("#divHeader").html("Spect(s) Rx");
        $('#DivLense').show();
        $('#DivBtn').show();
    });
       
    customerId();
    function customerId() {
        var customerId = Date.now().toString().substr(6);
        $('#customerId').text(customerId);
    }  
    $(function () {      
        $(".myclass").each(function () {
            $(this).uniqueId();
        });

        $("#OrderDate").datepicker({
            showButtonPanel: true,
            dateFormat: "dd-mm-yy",

        });
        $("#DeliveryDate").datepicker({
            showButtonPanel: true,
            dateFormat: "dd-mm-yy",

        });
        $("#DeliveredOn").datepicker({
            showButtonPanel: true,
            dateFormat: "dd-mm-yy",

        });
    });
    $('.Numeric').on('input', function (e) {
        if (this.value.match(/[^0-9]/g)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    }); 

    $('#submit').click(function (e) {       
        var RegexEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        //toastr.success('Added', 'sdsds');
        if ($('#title').val() == "0") {
           
            $('#errorMsg').text('Select the title').css("color", "red");
            $('#title').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#name').val() == "") {
            $('#errorMsg').text('Enter the name').css("color", "red");
            $('#name').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#mobNo').val() == "") {
            $('#errorMsg').text('Enter the mob. no.').css("color", "red");
            $('#mobNo').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#mobNo').val().length != 10) {
            $('#errorMsg').text('mob no must be 10 digit').css("color", "red");
            $('#mobNo').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#email').val() == "") {
            $('#errorMsg').text('Enter email id').css("color", "red");
            $('#email').focus();
            e.preventDefault();
            return false;
        }
        else if (!RegexEmail.test($('#email').val())) {
            $('#errorMsg').text('Invalid Email').css("color", "red");
            $('#email').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#address').val() == "") {
            $('#errorMsg').text('Enter the address').css("color", "red");
            $('#address').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#OrderDate').val() == "") {
            $('#errorMsg').text('Select the order date').css("color", "red");
            $('#OrderDate').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#DeliveryDate').val() == "") {
            $('#errorMsg').text('Select the delivery date').css("color", "red");
            $('#DeliveryDate').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#DeliveredOn').val() == "") {
            $('#errorMsg').text('Select the Delivered on').css("color", "red");
            $('#DeliveredOn').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#totalPayment').val() == "" || $('#totalPayment').val() == "0") {
            $('#errorMsg').text('enter the price').css("color", "red");
            $('#totalPayment').focus();
            e.preventDefault();
            return false;
        }
        else if ($('.switch5').is(":checked") == true && $('#gst').val() == "") {
            $('#errorMsg').text('enter the gst value in %').css("color", "red");
            $('#gst').focus();
            e.preventDefault();
            return false;
        }
        else {
            $('#errorMsg').text('');
        }       
        SaveContactLensesData();
        e.preventDefault();
        return false;
    });

    function SaveContactLensesData() {       
        $("#coverScreen").show();
        var _contactLenses = {
            customerId: $('#customerId').text(),
            title: $('#title option:selected').text(),
            name: $('#name').val(),
            mobno: $('#mobNo').val(),
            email: $('#email').val(),
            address: $('#address').val(),
            orderDate: $('#OrderDate').val(),
            DeliveryDate: $('#DeliveryDate').val(),
            DeliveredOn: $('#DeliveredOn').val(),
            RefNo: $('#RefNo').val(),
            Examinedy: $('#ExaminedBy').val(),
            rightDistanceSPH: ($('#rightSPH option:selected').text() == 'Select') ? '0' : $('#rightSPH option:selected').text(),
            rightDistanceCYL: ($('#rightCYL option:selected').text() == 'Select') ? '0' : $('#rightCYL option:selected').text(),
            rightDistanceAxis: ($('#rightAxis option:selected').text() == 'Select') ? '0' : $('#rightAxis option:selected').text(),
            rightAddition: ($('#rightAddition option:selected').text() == 'Select') ? '0' : $('#rightAddition option:selected').text(),
            //rightPD: $('#rightPD').val(),
            vision: ($('#rightVision option:selected').text() == 'Select') ? '0' : $('#rightVision option:selected').text(),
            leftDistanceSPH: ($('#leftSPH option:selected').text() == 'Select') ? '0' : $('#leftSPH option:selected').text(),
            leftDistanceCYL: ($('#leftCYL option:selected').text() == 'Select') ? '0' : $('#leftCYL option:selected').text(),
            leftDistanceAxis: ($('#leftAxis option:selected').text() == 'Select') ? '0' : $('#leftAxis option:selected').text(),
            leftAddition: ($('#leftAddition option:selected').text() == 'Select') ? '0' : $('#leftAddition option:selected').text(),
            //leftPD: $('#leftPD').val(),
            leftvision: ($('#leftVision option:selected').text() == 'Select') ? '0' : $('#leftVision option:selected').text(),
            totalPayment: $('#totalPayment').val(),
            advancedPayment: $('#advancedPayment').val(),
            BalancePayment: $('#balancePayment').val(),
            frame: $('#frame').val(),
            lenses: $('#lenses').val(),
            remarks: $('#remarks').val(),
            entryType: $('#divHeader').html(),
            IGST:$('#gst').val(),
            TotalPrice: $('#grossAmount').text()
        };
        
        $.ajax({
            async: true,
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset-8',
            data: JSON.stringify(_contactLenses),
            url: '/ContactLenses/SaveContactLenses',
            success: function (response) {
                if (response.success === true) {
                    $("#coverScreen").hide();
                    toastr.success('Yes! You have successfully submitted data !', 'success', { timeOut: 3000, "closeButton": true });
                    clear();
                    customerId();
                }
                else {
                    $("#coverScreen").hide();
                    toastr.error('Some error occured', 'Error Alert', { timeOut: 3000, "closeButton": true });
                }
            },
            error: function () {
                $("#coverScreen").hide();
                toastr.error('There is some problem to process your request', 'Error Alert', { timeOut: 3000, "closeButton": true });
            }
        });
    }

    function clear() {
        $('#customerId').text(''),
        $('#title').val('0'),
        $('#name').val(''),
        $('#mobNo').val(''),
        $('#email').val(''),
        $('#address').val(''),
        $('#OrderDate').val(''),
        $('#DeliveryDate').val(''),
        $('#DeliveredOn').val(''),
        $('#RefNo').val(''),
        $('#ExaminedBy').val(''),
        $('#rightSPH').val('0'),
        $('#rightCYL').val('0'),
        $('#rightAxis').val('0'),
        $('#rightAddition').val('0'),
        $('#rightPD').val(''),
        $('#leftSPH').val('0'),
        $('#leftCYL').val('0'),
        $('#leftAxis').val('0'),
        $('#leftAddition').val('0'),
        $('#leftPD').val(),
        $('#totalPayment').val(''),
        $('#advancedPayment').val(''),
        $('#balancePayment').val(''),
        $('#frame').val('0'),
        $('#lenses').val('0'),
        $('#remarks').val(''),
        $('#rightVision').val('0'),
        $('#leftVision').val('0'),
        $('#grossAmount').text('0.00'),
        $('#gst').val('')
    }

    $('#cancelbtn').click(function () {       
        clear();
        customerId();
    });
});
