$(document).on('ready', function () {
    $('#sub').click(function () {

            

        $('#registrationForm').validate({
            rules: {
                firstname: {
                    required: true
                },
                lastname: {
                    required: true
                }
            },
            messages: {
                firstname: {
                    required: "Please enter your first name."
                },
                lastname: {
                    required: "Please enter your last name."
                }
            }
        });
    });
});