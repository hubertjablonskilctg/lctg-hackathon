$(document).ready(function () {
    $.ajax({
        url: "http://takeoff2016-krkteam.azurewebsites.net/api/Test/1"
    }).then(function (data) {
        console.log(data.Id + " " + data.Name);
    });

    $('#addMember').on('click', function () {
        alert("Add member " + $('#memberEmail').val());
    });

});
