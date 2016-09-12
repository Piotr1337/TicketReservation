$('#Email').blur(function () {
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if (!testEmail.test(this.value)) {
        $('.validate').remove();
        $('#Email').after('<p style="color: red;" class="validate">Wpisz poprawny adres Email.</p>')
    } else {
        $('.validate').remove();
    }
});