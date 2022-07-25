// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let showErrorMessage = () => {
    let el = $('#message').first();
    let fieldValidationError = $('span.field-validation-error').first().text();
    let message = el.text();
    let error = el.data('type');
    
    if (fieldValidationError && fieldValidationError.length > 0){
        swal({
            icon: "error",
            text: fieldValidationError,
            button: "Ok"
        });
    }
    else if (error && message.length > 0) {
        swal({
            icon: "error",
            text: message,
            button: "Ok"
        });
    }
    else if(message.length > 0) {
        swal({
            icon: "success",
            text: message,
            button: "Ok"
        });
    }
}

showErrorMessage();

$('form').valid(() => {
    showErrorMessage();
})





