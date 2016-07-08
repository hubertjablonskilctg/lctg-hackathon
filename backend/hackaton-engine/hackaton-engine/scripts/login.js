angular.module('groupTripApp', [])
  .controller('LoginController', function ($window) {
      var ctrl = this;

      ctrl.email = "";

      ctrl.loginUser = function () {
              $.ajax({
                  url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/addUsers',
                  type: 'POST',
                  data: JSON.stringify([ctrl.email]),
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
