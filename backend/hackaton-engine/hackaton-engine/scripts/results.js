$(document).ready(function () {
    $.ajax({
        url: "http://takeoff2016-krkteam.azurewebsites.net/api/group/0/hotels"
    }).then(function (data) {
        data.forEach(function (entry) {
            console.log(entry);
        });
    }).fail(function (data) {
        alert('fail');
    });


});
