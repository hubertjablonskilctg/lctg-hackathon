angular.module('groupTripApp', [])
  .controller('MainController', function ($window) {
      var ctrl = this;

      ctrl.createTrip = function () {
          var dataObject = {
              newUserIds: "[1,2]"
          };

          $.ajax({
              url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/addUsers',
              type: 'POST',
              data: JSON.stringify([1]),
              contentType: 'application/json; charset=utf-8',
              success: function (data) {
                  localStorage.setItem("groupId", data.Id);
                  localStorage.setItem("userId", 1);
                  $window.location.href = '/timelines.html';
              },
              error: function (data) {
                  console.log(data);
              }
          }
         )
      };

  });
