var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};


$(document).ready(function () {
    jQuery.validator.methods["date"] = function (value, element) { return true; }
    var eventId = getUrlParameter('EventID');
    $('#calendar')
        .fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay,listWeek'
            },
            buttonText: {
                today: 'dziś',
                month: 'miesiąc',
                week: 'tydzień',
                day: 'dzień',
                list: 'lista'
            },
            allDayText: 'cały dzień',
            defaultView: 'month',
            allDaySlot: true,
            selectable: true,
            editable: true,
            slotMinutes: 15,
            locale: 'pl',
            timeFormat: 'H:mm',
            axisFormat: 'H:mm',
            ignoreTimezone: false,
            eventLimit: true,
            events: 'GetTickets?eventID='+ eventId,
            views: {
                agenda: {
                    eventLimit: 4
                }
            },

            eventRender: function (event, element, view) {
                Date.prototype.addDays = function (days) {
                    var dat = new Date(this.valueOf())
                    dat.setDate(dat.getDate() + days);
                    return dat;
                }
                var list = [],
                    dateStart = event.beginDate,
                    dateEnd = event.finishDate
                    var dataParts = dateStart.split(".");
                    var dataParts2 = dateEnd.split(".");
                var dateObjectForStart = new Date(dataParts[2], dataParts[1] - 1, dataParts[0]);
                var dateObjectForEnd = new Date(dataParts2[2], dataParts2[1] - 1, dataParts2[0]);

                while (dateObjectForStart <= dateObjectForEnd) {

                    list.push(dateObjectForStart);
                    dateObjectForStart = dateObjectForStart.addDays(1);
                    var myNewDate = (new Date(dateObjectForStart)).toISOString().slice(0, 10)
                    $(view.el[0]).find(".fc-day[data-date='" + myNewDate + "']").css('background-color', '#effbff')
                }
                

                var param = event.creatorID;
                var url = '@Url.Action("GetImage", "Home", new { contactId = "myParam" })';
                if (view.name === "agendaWeek" || view.name === "agendaDay") {
                    if (event.allDay === true) {
                        element.find('.fc-title')
                            .append("<br/>" +
                                "<span class='glyphicon glyphicon-home'></span>" +
                                " " +
                                event.roomName +
                                "<br/>" +
                                "<span class='glyphicon glyphicon-user'></span>" +
                                " " +
                                event.creatorName)
                    } else {
                        element.find('.fc-title')
                            .append("<br/>" +
                                "<span class='glyphicon glyphicon-home'></span>" +
                                event.roomName +
                                " " +
                                "<span class='glyphicon glyphicon-user'></span>" +
                                event.creatorName)
                    }
                }
                element.popover({
                    title: event.title,
                    html: true,
                    animation: true,
                    placement: 'right',

                    content:
                        "<h6>" +
                            event.roomName +
                            "</h6>" +
                            "<br/>" +
                            "<img src=" +
                            url.replace('myParam', encodeURIComponent(param)) +
                            ">" +
                            " " +
                            event.creatorName +
                            "<br/><br/>" +
                            "<p class='bg-info' id='forAppend' style='padding: 5px;'>" +
                            "<span class='glyphicon glyphicon-calendar' style='color: #3b8dff'></span>" +
                            " " +
                            event.normalStartDate +
                            "</p>" +
                            ('<div>' + (event.creatorID === event.loggedUserID ? "<a><span class='glyphicon glyphicon-pencil' style='color: blue'></span>Edytuj</a>" + " " + "<a><span class='glyphicon glyphicon-remove' style='color: red'></span>Anuluj</a>" : "<a><span class='glyphicon glyphicon-ok' style='color: green'></span>Akceptuj</a>" + "&nbsp" + "<a><span class='glyphicon glyphicon-remove' style='color: red'></span> Odrzuć</a>") + '</div>'),

                    container: 'body',

                });

                $('body').on('click', function (e) {
                    if (!element.is(e.target) && element.has(e.target).length === 0 && $('.popover').has(e.target).length === 0)
                        element.popover('hide');
                });

                element.bind('dblclick',
                    function () {

                        if (event.creatorID === event.loggedUserID) {
                            var url = "/Create/EditAppointment?appointmentId=" + event.id;
                            window.location.href = url;                                                     
                        } else {
                            alert("Nie jesteś organizatorem tego spotkania")
                        }

                    })

            },
            dayClick: dayClickCallback,
        });

    var slotDate;

    function dayClickCallback(date) {
        slotDate = date;
        $("#calendar").on("mousemove", forgetSlot);
    }

    function eventRenderCallback(event, element) {
        element.on("dblclick",
            function () {
                dblClickFunction(event.start)
            });
    }

    function forgetSlot() {
        slotDate = null;
        $("#calendar").off("mousemove", forgetSlot);
    }

    function dblClickFunction(date) {
        var url = "/Create/CreateAppointment?date=" + date.format();
        window.location.href = url;
    }

    $("#calendar")
        .dblclick(function () {
            if (slotDate) {
                dblClickFunction(slotDate); //do something with the date
            }
        });
});
