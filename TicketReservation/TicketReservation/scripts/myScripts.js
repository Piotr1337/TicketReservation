$('#Email').blur(function () {
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if (!testEmail.test(this.value)) {
        $('.validate').remove();
        $('#Email').after('<p style="color: red;" class="validate">Wpisz poprawny adres Email.</p>')
    } else {
        $('.validate').remove();
    }
});

$('#eventDescription').readmore({
    speed: 500,
    collapsedHeight: 300,
    heightMargin: 16,
    moreLink: '<a href="#">czytaj więcej...</a>',
    lessLink: '<a href="#">zwiń</a>'
});

(function () {
    // Initialize
    var bLazy = new Blazy({

        selector: 'img',
        offset: 100,
        success: function (element) {
            setTimeout(function () {
                // We want to remove the loader gif now.
                // First we find the parent container
                // then we remove the "loading" class which holds the loader image
                var parent = element.parentNode;
                parent.className = parent.className.replace(/\bloading\b/, '');
            }, 200);
        }
    });
})();

