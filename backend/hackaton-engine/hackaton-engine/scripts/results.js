angular.module('groupTripApp', [])
  .controller('ResultsController', function ($scope) {
      ctrl = this;
      var userId = localStorage.getItem("userId");
      var groupId = localStorage.getItem("groupId");
      ctrl.userEmail = localStorage.getItem("userEmail");

      var hotelsOrder = {};
      ctrl.init = function () {
          $.ajax({
              url: "http://takeoff2016-krkteam.azurewebsites.net/api/group/" + groupId + "/hotelvotes",
              type: 'GET',
              contentType: 'application/json; charset=utf-8'
          }).then(function (data) {
              $scope.$apply(() => {
                  ctrl.hotels = data;
              });
          }).fail(function (data) {
              console.log(data);
          });

          $.ajax({
              url: "http://takeoff2016-krkteam.azurewebsites.net/api/group/" + groupId,
              type: 'GET',
              contentType: 'application/json; charset=utf-8'
          })
              .then(handleGroup)
              .fail(function (data) {
                  console.log(data);
              });
      }

      ctrl.approveHotel = function (hotel) {
          var hotelId = hotel.Id;
          //ctrl.groups.UserPreferences[userId]
          if (!ctrl.group.UserHotelUpVotes)
              ctrl.group.UserHotelUpVotes = {};

          var index = $.inArray(hotelId, ctrl.group.UserHotelUpVotes[userId]);
          upVoteHotel(hotelId, index === -1);
      }

      ctrl.isSelected = function (hotel) {
          var index = $.inArray(hotel.Id, ctrl.group.UserHotelUpVotes[userId]);
          return index > -1;
      }

      var addRankToHotel = function (hotelId) {
          if (!hotelsOrder[hotelId])
              hotelsOrder[hotelId] = 0;
          hotelsOrder[hotelId]++;
      }

      var removeRankFromHotel = function (hotelId) {
          hotelsOrder[hotelId]--;
      }

      var handleGroup = function (data) {
          $scope.$apply(() => {
              hotelsOrder = {};
              ctrl.group = data;
              for (var key in ctrl.group.UserHotelUpVotes) {
                  var value = ctrl.group.UserHotelUpVotes[key];
                  for (var i = 0; i < value.length; ++i) {
                      addRankToHotel(value[i]);
                  }
              }
          });
      }

      var upVoteHotel = function (hotelId, upVote) {
          $.ajax({
              url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/upvotehotel/' + userId + '/' + groupId + '/' + hotelId + "/" + upVote,
              type: 'GET',
              contentType: 'application/json; charset=utf-8'
          })
              .then(handleGroup)
              .fail(function (data) {
                  console.log(data);
              });
      }

      ctrl.rankHotels = function (hotel) {
          if (hotelsOrder[hotel.Id])
              return -hotelsOrder[hotel.Id];
          else
              return 0;
      }
  })
