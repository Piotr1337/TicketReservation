﻿
var options = {
    url: "/Event/AutoCompleteSearch",



    getValue: function (element) {
        return element.Name;
    },

    template: {
        type: "custom",
        method: function(value, item) {
            return "<i id='searchInputIcons' class='" + item.Icon + "'></i>&nbsp;" + "<p id='searchInputValue' data-item-id='" + item.Id + "' >" + value + "</p>";
        },
    },

    placeholder: "artysta, zespół, wydarzenie...",
    ajaxSettings: {
        dataType: "json",
        method: "POST",
        data: {
            dataType: "json"
        }
    },

    preparePostData: function (data) {
        data.phrase = $("#searchInput").val();
        return data;
    },
    list: {
        match: {
            enabled: true
        },
        maxNumberOfElements: 8,

        onClickEvent: function () {


            $('li').on('click', function() {
                var id = $(this).find("#searchInputValue").data("item-id");
                window.location = '/Event/ShowEvent?eventId=' + id;
            });

            //$('li.selected').click(function (event) {
            //    var id = $(event.target).data("item-id");
            //    alert(id);
            //    window.location = '/Event/ShowEvent?eventId=' + id;
            //});

            //var selectedItemId = $("li.selected").on("click",function () {
            //    var id = $("#searchInputValue").data("item-id");
            //    alert(id);
            //});

        }

    },

    theme: "square"

};
$("#searchInput").easyAutocomplete(options);

$(document).ready(function() {
    $('article').readmore({
        speed: 500,
        collapsedHeight: 380,
        heightMargin: 16,
        moreLink: '<a href="#">czytaj więcej...</a>',
        lessLink: '<a href="#">zwiń</a>'
    });

    //$('#searchInput').autocomplete({
    //    source: function(request, response) {
    //        $.ajax({
    //            url: "/Event/AutoCompleteSearch",
    //            type: "POST",
    //            dataType: "json",
    //            data: { prefix: request.term },
    //            success: function (data) {
    //                response($.map(data, function (item) {
    //                    console.log(item.name)
    //                    return { label: item.name, value: item.name }
    //                }));
    //            }
    //        });
    //    },
    //    messages: {
    //        noResults: "", results: ""
    //    }
    //});
});

$('#Email').on('keyup blur', function () {
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if (!testEmail.test(this.value) && this.value !== '') {
        $('.validate').remove();
        $('#Email').after('<p style="color: red;" class="validate">Wpisz poprawny adres Email.</p>')
    } else {
        $('.validate').remove();
    }
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

