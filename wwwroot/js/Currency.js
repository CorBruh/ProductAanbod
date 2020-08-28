$('#NoDot').keydown(function (e) { // Remove the access of inputting a dot by a price field
    if (e.keyCode === 190 || e.keyCode === 110) {
        e.preventDefault();
    }
});

$.validator.methods.number = function (value, element) { // Allow user to input price with a comma
        return this.optional(element) || /-?(?:\d+|\d{1, 3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}
