angular.module('groupTripApp', [])
  .controller('MainController', function () {
      var ctrl = this;
      
      ctrl.tripMembers = [];

      ctrl.newMemberName;

      ctrl.addMember = function () {
          ctrl.tripMembers.push(ctrl.newMemberName);
          ctrl.newMemberName = "";
      };

      ctrl.removeMember = function (member) {
          var index = ctrl.tripMembers.indexOf(member);
          ctrl.tripMembers.splice(index, 1);
      };

      ctrl.createTrip = function () {
          alert("Create trip " + ctrl.tripMembers[0]);
      };
      
  });
