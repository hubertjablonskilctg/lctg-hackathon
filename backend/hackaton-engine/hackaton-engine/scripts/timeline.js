angular.module('groupTripApp', [])
  .controller('TimelineController', function () {
      var ctrl = this;

      var groupId = localStorage.getItem("groupId");

      $.ajax({
          url: "http://takeoff2016-krkteam.azurewebsites.net/api/group/" + groupId,
		  type: 'GET',
		  contentType: 'application/json; charset=utf-8',
          success: function (data) {
              ctrl.selectedDates = [];
              angular.forEach(data.Users, function (user) {
                  if (data.UserPreferences[user.Id] && data.UserPreferences[user.Id].DateRange) {
                      ctrl.selectedDates.push([moment(data.UserPreferences[user.Id].DateRange.m_Item1).format('YYYY/MM/DD'), moment(data.UserPreferences[user.Id].DateRange.m_Item2).format('YYYY/MM/DD'),user.Email, 'user']);
                  }
              });
			  ctrl.timelinesOverlapping = checkIfAllDatesOverlaps();
              drawChart();
          },
          error: function (data) {
              console.log(data);
          }
		});

      ctrl.addPreferences = function () {
          var groupId = localStorage.getItem("groupId");
          var userId = localStorage.getItem("userId");

          var preferences = {
              "Tags": [
                "Cities",
                "Beaches"
              ],
              "Localizations": [
                "Spain",
                "Portugal"
              ],
              "MustHaves": [
                "WheelchairAccessible",
                "ChildFriendly",
				"Vegetarian"
              ],
              "PriceRange": {
                  "m_Item1": 50,
                  "m_Item2": 100
              },
              "DateRange": {
                  "m_Item1": new Date($('#fromDate').val()),
                  "m_Item2": new Date($('#toDate').val())
              }
          }
          var url = 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changepreferences/' + userId + '/' + groupId;
          $.ajax({
              url: url,
              type: 'POST',
              data: JSON.stringify(preferences),
              contentType: 'application/json; charset=utf-8',
              success: function (data) {
                  var userPreferences = data.UserPreferences;
                  ctrl.addNewDate();

              },
              error: function (data) {
                  console.log(data);
              }
          }
         )
      }

	  //has to wait for DOM
	  $(document).ready( function() {
		  $('#fromDate').datetimepicker({
			  format: 'YYYY/MM/DD',
		  });
		  $('#toDate').datetimepicker({
			  useCurrent: false,
			  format: 'YYYY/MM/DD',
		  });
		  $("#fromDate").on("dp.change", function (e) {
			  $('#toDate').data("DateTimePicker").minDate(e.date);
		  });
		  $("#toDate").on("dp.change", function (e) {
			  $('#fromDate').data("DateTimePicker").maxDate(e.date);
		  });
	  });

      ctrl.datesSelected = false;

      ctrl.addNewDate = function () {
          ctrl.selectedDates.pop();
          if (!checkIfAllDatesOverlaps()) {
              ctrl.selectedDates.pop();
              ctrl.timelinesOverlapping = false;
          } else {
              drawChart();
              ctrl.datesSelectedValues = $('#fromDate').val() + ' - ' + $('#toDate').val();
              ctrl.datesSelected = true;
          }
      };

      ctrl.editSelectedDate = function () {
          
      };

	  function getMinValue() {
		  var dataTable = ctrl.selectedDates;
		  var min = dataTable[0][0];
		  for(var i=0;i<dataTable.length-1;i++) {
			  if(dataTable[i+1]) {
				  var min1 = moment(min);
                  var min2 = moment(dataTable[i + 1][0]);
				  var duration1 = moment.duration(min1.diff(min2, 'days', true));
                  var days1 = duration1.asDays();
				  if(days1>0)
					  min = dataTable[i][0];
			  }
		  }
		  return moment(min).subtract(2,'days').format('YYYY/MM/DD');
	  }
	  
	  function getMaxValue() {
		  var dataTable = ctrl.selectedDates;
		  var max = dataTable[0][1];
		  for(var i=0;i<dataTable.length-1;i++) {
			  if(dataTable[i+1]) {
				  var max1 = moment(max);
                  var max2 = moment(dataTable[i + 1][1]);
				  var duration1 = moment.duration(max1.diff(max2, 'days', true));
                  var days1 = duration1.asDays();
				  if(days1<0)
					  max = dataTable[i][1];
			  }
		  }
		  return moment(max).add(2,'days').format('YYYY/MM/DD');
	  }

      function drawChart() {
          var numRows = ctrl.selectedDates.length;
          if (numRows > 0) {
              if (numRows > 1) {
                  ctrl.selectedDates.push(findCommonRange());
              }
			new Timesheet('timeline', getMinValue(), getMaxValue(), ctrl.selectedDates);  
          }
      }

      function checkIfAllDatesOverlaps() {
          var numRows = ctrl.selectedDates.length;
          var doesntOverlaps = 0;
		  if(numRows > 1) {
			  for (var i = 0; i < numRows; i++) {
				  if (ctrl.selectedDates[i + 1]) {
					  var start1 = moment(ctrl.selectedDates[i][0]);
					  var end1 = moment(ctrl.selectedDates[i][1])
					  var start2 = moment(ctrl.selectedDates[i + 1][0]);
					  var end2 = moment(ctrl.selectedDates[i + 1][1])
					  var duration1 = moment.duration(start1.diff(end2, 'days', true));
					  var days1 = duration1.asDays();
					  var duration2 = moment.duration(start2.diff(end1, 'days', true));
					  var days2 = duration2.asDays();
					  if (!(days1 < 0 && days2 < 0)) {
						  doesntOverlaps++;
					  }
				  }
			  }
		  } else {
			  doesntOverlaps = 1;
		  }
          return (doesntOverlaps) ? false : true;
      }

      function findCommonRange() {
          var numRows = ctrl.selectedDates.length;
          var startDate = ctrl.selectedDates[0][0];
          var endDate = ctrl.selectedDates[0][1];
          for (var i = 0; i < numRows; i++) {
              if (ctrl.selectedDates[i + 1]) {
                  var start1 = moment(startDate);
                  var start2 = moment(ctrl.selectedDates[i + 1][0]);
                  var duration1 = moment.duration(start1.diff(start2, 'days', true));
                  var days1 = duration1.asDays();
                  if (days1 < 0)
                      startDate = start2.format('YYYY/MM/DD');
                  var end1 = moment(endDate);
                  var end2 = moment(ctrl.selectedDates[i + 1][1]);
                  var duration2 = moment.duration(end1.diff(end2, 'days', true));
                  var days2 = duration2.asDays();
                  if (days2 > 0)
                      endDate = end2.format('YYYY/MM/DD');
              }
          }
          return [startDate, endDate,'Common daterange', 'common'];
      }
  })
