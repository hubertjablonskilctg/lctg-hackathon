angular.module('groupTripApp', [])
  .controller('LoginController', function ($window) {
      var ctrl = this;

      ctrl.email = "";

      ctrl.loginUser = function () {
          $.ajax({
              url: 'http://localhost:62200/api/user/get',
              type: 'POST',
              data: JSON.stringify(ctrl.email),
              contentType: 'application/json; charset=utf-8',
              success: function (data) {
                  if (data.Groups && data.Groups.length > 0)
                      localStorage.setItem("groupId", data.Groups[0].Id);
                  if (data.User) {
                      localStorage.setItem("userId", data.User.Id);
                      localStorage.setItem("userEmail", data.User.Email);
                      $window.location.href = '/preferences.html';
                  } else {
                      allert("Wrong email. Dont try to cheat me!!");
                  }
              },
              error: function (data) {
                  console.log(data);
              }
          })
      }
  });
