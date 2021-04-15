(function ($) {
    var defaultOptions = {
        errorClass: 'has-error',
        validClass: 'has-success',
        highlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .addClass(errorClass)
                .removeClass(validClass);
            $(element).addClass("is-invalid")
                      .removeClass("is-valid");
            $(element).siblings("span")
                      .addClass("invalid-feedback")
                      .removeClass("valid-feedback");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .removeClass(errorClass)
                .addClass(validClass);
            $(element).addClass("is-valid")
                      .removeClass("is-invalid");
            $(element).siblings("span")
                      .addClass("valid-feedback")
                      .removeClass("invalid-feedback");
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
        validClass: defaultOptions.validClass,
    };
})(jQuery);