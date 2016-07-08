angular.module('groupTripApp', [])
  .controller('MainController', function ($window) {
      var ctrl = this;
      ctrl.email = "";

      ctrl.tripMembers = [];

      ctrl.addMember = function () {
          ctrl.tripMembers.push(ctrl.email);
          ctrl.email = "";
      };

      ctrl.removeMember = function (member) {
          var index = ctrl.tripMembers.indexOf(member);
          ctrl.tripMembers.splice(index, 1);
      };

      ctrl.createTrip = function () {
              $.ajax({
                  url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/addUsers',
                  type: 'POST',
                  data: JSON.stringify(ctrl.tripMembers),
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      localStorage.setItem("groupId", data.Id);
                      localStorage.setItem("userId", data.UserIds[0]);
                      localStorage.setItem("userEmail", ctrl.email);
                      $window.location.href = '/timelines.html';
                  },
                  error: function (data) {
                      console.log(data);
                  }
              })
              }
      });
