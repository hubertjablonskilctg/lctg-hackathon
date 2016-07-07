$(document).ready(function () {
    $.ajax({
        url: "http://takeoff2016-krkteam.azurewebsites.net/api/Test/1"
    }).then(function (data) {
        console.log(data.Id + " " + data.Name);
    });

    var tripMembers = [];

    $('#addMember').on('click',
        function () {
            var newMemberName = $('#memberEmail').val();
            if (newMemberName.length) {
                $("<div class='row member'><div class='col-sm-10'>" + newMemberName + "</div><div class='col-sm-2'><i id='" + newMemberName + "' class='fa fa-close deleteMember'></i></div></div>")
                    .insertBefore($("#addMemberRow"));
                $('#memberEmail').val('');
                tripMembers.push(newMemberName);
            }
        });

    $('.members-content').on('click', '.fa-close', function () {
        var memberName = $(this).attr('id');
        var index = tripMembers.indexOf(memberName);
        tripMembers.splice(index, 1);;
        $(this).parent().parent().remove();
    });

    $('#createTrip').on('click',
    function () {
        alert("Create trip " + tripMembers[0]);
    });

});
