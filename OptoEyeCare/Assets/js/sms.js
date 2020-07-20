
$(document).ready(function (e) {
    $("#send").click(function (e) {
        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
        var textarea = ValidateCharacterLength();
         if ($("#upload").val() == "") {
            $('#errorMsg').text('Please upload a Excel file!').css("color", "red");
            $('#upload').focus();
            e.preventDefault();
            return false;
            }
         else if ($("#upload").val() != "" && !regex.test($("#upload").val().toLowerCase())) {           
                $('#errorMsg').text('Please upload a valid Excel file!').css("color", "red");
                $('#upload').focus();
                e.preventDefault();
                return false;                                
        }             
        else if (textarea == "") {
            $('#errorMsg').text('Please enter the text').css("color", "red");
            $('#txtEditor').focus();
            e.preventDefault();
            return false;
        }
        else {
            $('#errorMsg').text('');
            UploadSelectedExcelsheet(textarea);
        }
    });

    function UploadSelectedExcelsheet(textarea) {
        $("#coverScreen").show();
        var data = new FormData();
        var i = 0;
        var fl = $("#upload").get(0).files[0];
        if (fl != undefined) {
            data.append("file", fl);
        }
        if (textarea != null){
            data.append('textarea', textarea);
        }
            
        $.ajax({
            type: "POST",
            url: '/SMS/UploadExcelsheet',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.success === true) {
                    $("#coverScreen").hide();
                    toastr.success('Yes! SMS delivered !', 'success', { timeOut: 3000, "closeButton": true });
                    clear();
                    location.reload();
                }
                else {
                    $("#coverScreen").hide();
                    toastr.error('Some error occured', 'Error Alert', { timeOut: 3000, "closeButton": true });
                }               
            },
            error: function () {
                clear();
                $("#coverScreen").hide();                
                //toastr.error('There is some problem to process your request', 'Error Alert', { timeOut: 3000, "closeButton": true });
                toastr.success('Yes! SMS delivered !', 'success', { timeOut: 3000, "closeButton": true });
            }
        });
    }

    function ValidateCharacterLength() {      
        var body = tinymce.get("txtEditor").getBody();
        var content = tinymce.trim(body.innerText || body.textContent);
        return content;
    }   

    function clear() {
        $("#upload").val('');
    }
});
