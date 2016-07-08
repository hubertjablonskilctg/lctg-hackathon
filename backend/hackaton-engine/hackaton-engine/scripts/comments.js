angular.module('groupTripApp')
  .controller('CommentsController', function ($scope, $window) {
      var userId = localStorage.getItem("userId");
      var groupId = localStorage.getItem("groupId");

      conmmentsCtrl = this;

      if (userId == null)
          userId = 1;
      if (groupId == null)
          groupId = 1;
      
      conmmentsCtrl.init = function () {
          $.get('http://takeoff2016-krkteam.azurewebsites.net/api/group/' + groupId, reloadComments);
      }
      
      conmmentsCtrl.submitComment = function () {
          postComment(userId, groupId, conmmentsCtrl.newComment, reloadComments);
      }

      var reloadComments = function (group) {
          conmmentsCtrl.comments = group.Comments;
      }

      var postComment = function (groupId, userId, comment, callback) {
          $.post('http://takeoff2016-krkteam.azurewebsites.net/api/group/PostComment?groupId=' + groupId + '&userId=' + userId, '=' + comment, function (s) {
              console.log(s);
              if (callback != null) callback(s);
          });
      }
  });