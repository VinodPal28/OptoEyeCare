$(document).ready(function (e) {
    function login() {
        $("#submit").attr("disabled", true).css("background-color", "gray");
        var EmailId = $('#txtUserName').val();
        var password = $('#txtPassword').val();
        var objLoginViewModel = { EmailId: EmailId, Password: password };

        $.ajax({
            async: true,
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset-8',
            data: JSON.stringify(objLoginViewModel),
            url: '/Home/Login',
            success: function (response) {
                if (response.success === true) {
                    $("#submit").attr("disabled", false);
                    window.location = response.url;
                }
                else {
                    toastr.error('email id & password is not valid', 'Error Alert', { timeOut: 3000, "closeButton": true });
                }
            },
            error: function () {
                toastr.error('There is some problem to process your request', 'Error Alert', { timeOut: 3000, "closeButton": true });
            }
        });
    }

    $('#submit').click(function (e) {
        alert('1');
        var re = /^(([^<>()[\]\\.,;:\s@@\"]+(\.[^<>()[\]\\.,;:\s@@\"]+)*)|(\".+\"))@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if ($('#txtUserName').val() == "") {
            $('#password').text('');
            $('#email').text('Please enter the email address').css("color", "red");
            $('#txtUserName').focus();
            e.preventDefault();
            return false;
        }
        else if (!re.test($('#txtUserName').val())) {
            $('#password').text('');
            $('#email').text('Invalid Email').css("color", "red");
            $('#txtUserName').focus();
            e.preventDefault();
            return false;
        }
        else if ($('#txtPassword').val() == "") {
            $('#email').text('');
            $('#password').text('Please enter the password').css("color", "red");
            $('#txtPassword').focus();
            e.preventDefault();
            return false;
        }
        else {
            $('#email').text('');
            $('#password').text('');

        }
        login();
        e.preventDefault();
        return false;
    });

    $('#signUp').click(function (e) {
        $('#DivSignUp').show().addClass("active");
        $('#DivSignIn').hide().removeClass("active");
    });
    $('#signIn').click(function (e) {
        $('#DivSignUp').hide().addClass("active");
        $('#DivSignIn').show().removeClass("active");
    });
    $('#DivSignUp').hide();

});