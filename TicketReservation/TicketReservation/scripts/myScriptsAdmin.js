jQuery.validator.methods["date"] = function (value, element) { return true; }
$('.DateOfEvent').datetimepicker({ format: 'DD-MM-YYYY hh:mm', locale: 'pl' });


$('#bandOrArtist').bootstrapSwitch({
    onText: 'Artysta',
    offText: 'Zespół',
    size: 'normal',
});

$('#showAll').click(function() {
    $('#allEventsAdmin').slideToggle("medium");
});

$(document).ready(function () {
    $('#OtherDetails').summernote({
        height: 300,
        toolbar:
            [
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul','ol','paragraph']],
            ],
        lang: "pl-PL"
    });
    $('#Description').summernote({
        height: 300,
        toolbar:
            [
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
            ],
        lang: "pl-PL"
    });  
});


$('body').on('click', '#deleteEvent', function () {
    var eventId = $(this).data('eventid');
    console.log(eventId);
    swal({
        title: "Jesteś pewny?",
        text: "Nie będziesz w stanie odzyskać tego wydarzenia!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Tak, usuń!",
        cancelButtonText: "Anuluj",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                method: "POST",
                url: "/Admin/Delete",
                data: {
                    eventId: eventId
                }
            }).done(function() {
                swal({
                    title: "Usunięto!",
                    text: "Wydarzenie zostało usnięte",
                    type: "success",
                    confirmButtonText: "Ok",
                    confirmButtonColor: "#337ab7",
                });
                $('.sa-confirm-button-container').on('click', '.confirm', function() {
                    var id = $("tr[data-rowid='" + eventId + "']");
                    if (id.length > 0) {
                        id.remove();
                    }
                });
            });
        }
    });
})