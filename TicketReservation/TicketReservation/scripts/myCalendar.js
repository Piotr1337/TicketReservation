$(function () {
    $('.datetimepicker1').datetimepicker({ format: 'DD-MM-YYYY', locale: 'pl' });
    $('.datetimepicker2').datetimepicker({ format: 'DD-MM-YYYY', locale: 'pl' });
});

function convertToDash(start, end) {
    
}

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf())
    dat.setDate(dat.getDate() + days);
    return dat;
}
var list = [];

var date = $('#EventStartDateTime').val().substring(0, 10);
var date2 = $('#EventEndDateTime').val().substring(0, 10);
var dataParts;
var dataParts2;
if (date.includes("-") && date2.includes("-")) {
    var start = date.replace(/-/g, ".");
    var end = date2.replace(/-/g, ".");
    date = start;
    date2 = end;
}
dataParts = date.split(".");
dataParts2 = date2.split(".");

var dateObjectForStart = new Date(dataParts[2], dataParts[1] - 1, dataParts[0]);
var dateObjectForEnd = new Date(dataParts2[2], dataParts2[1] - 1, dataParts2[0]);

while (dateObjectForStart <= dateObjectForEnd) {

    dateObjectForStart = dateObjectForStart.addDays(1);
    var myNewDate = (new Date(dateObjectForStart)).toISOString().slice(0, 10)
    list.push(myNewDate)
    console.log("M:" + " " + dateObjectForStart)
}

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

var eventId = getUrlParameter('eventId');

$(document).ready(function () {
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
            editable: false,
            slotMinutes: 15,
            locale: 'pl',
            timeFormat: 'H:mm',
            axisFormat: 'H:mm',
            eventBorderColor: '#51dfd4',
            ignoreTimezone: false,
            eventLimit: true,
            events: 'GetTickets?eventID='+ eventId,
            views: {
                agenda: {
                    eventLimit: 4
                }
            },
            dayRender: function (date, cell) {

                var getNormalDate = new Date(date);
                var myNewDate = (new Date(getNormalDate)).toISOString().slice(0, 10)

                list.forEach(function (item) {
                    //console.log("itemek" + item)
                   if (myNewDate === item) {
                       cell.css("background-color", "#eafde4");
                   }
               })
            },
            eventRender: function (event, element, view) {
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
                    placement: 'left',

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
                            var url = "/Admin/TicketEdit?ticketId=" + event.id;
                            window.location.href = url;                                                     
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
 
        if (date <= dateObjectForEnd) {
            var url = "/Admin/AddTicket?date=" + date.format() + "&eventId=" + eventId;
            window.location.href = url;
        } else {
            alert("Można dodać bilet tylko na zielonych polach")
        }
    }

    $("#calendar")
        .dblclick(function () {
            if (slotDate) {
                dblClickFunction(slotDate); //do something with the date
            }
        });
});
