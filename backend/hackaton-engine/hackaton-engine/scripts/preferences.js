angular.module('groupTripApp', [])
  .controller('PreferencesController', function ($scope) {
		var ctrl = this;
		var groupId = localStorage.getItem("groupId");
		var userId = localStorage.getItem("userId");

		// TODO
		// assume they're there
		groupId = 3;
		userId = 3;
		
		//get initial data
		$.ajax({
          url: "http://takeoff2016-krkteam.azurewebsites.net/api/group/" + groupId,
		  type: 'GET',
		  contentType: 'application/json; charset=utf-8',
          success: function (data) {
				var userPreferences = data.UserPreferences[userId];
				console.log(data.UserPreferences)
				var localizations = userPreferences.Localizations;
				var tags = userPreferences.Tags;
				var mustHaves = userPreferences.MustHaves;
				
				ctrl.selectedDates = [];
				angular.forEach(data.Users, function (user) {
				  if (userPreferences && userPreferences.DateRange) {
					  ctrl.selectedDates.push([moment(userPreferences.DateRange.m_Item1).format('YYYY/MM/DD'), moment(userPreferences.DateRange.m_Item2).format('YYYY/MM/DD'),user.Email, 'user']);
					  console.log(user.Id, userId)
					  if(user.Id == userId) {
						ctrl.datesSelectedValues = moment(userPreferences.DateRange.m_Item1).format('YYYY/MM/DD') + ' - ' + moment(userPreferences.DateRange.m_Item2).format('YYYY/MM/DD');
						ctrl.dateFrom = moment(userPreferences.DateRange.m_Item1).format('YYYY/MM/DD');
						ctrl.dateTo = moment(userPreferences.DateRange.m_Item2).format('YYYY/MM/DD');
						ctrl.datesSelected = true;
					  }
				  }

				});
				ctrl.timelinesOverlapping = checkIfAllDatesOverlaps();
				drawChart();				

				for (var i = 0; localizations != null && i < localizations.length; i++) {
					$('p#' + localizations[i]).addClass('active');
					// console.log('p#' + localizations[i]);
				}

				for (var i = 0; tags != null && i < tags.length; i++) {
					$('p#' + tags[i]).addClass('active');
				}

				for (var i = 0; mustHaves != null && i < mustHaves.length; i++) {
					$('p#' + mustHaves[i]).addClass('active');
					console.log('p#' + mustHaves[i]);
				}
				
				$scope.$apply(() => {
				  ctrl = ctrl;
				});
          },
          error: function (data) {
              console.log(data);
          }
		});
		
		ctrl.editSelectedDates = function() {
			ctrl.selectedDates.pop();
			  ctrl.selectedDates.pop();
			  ctrl.datesSelected = false;
			  drawChart();
		}
		
		ctrl.savePreferences = function() {
			$.get('http://takeoff2016-krkteam.azurewebsites.net/api/group/' + groupId, function(data) {
				console.log(data);

				// TODO userid zahardkodowane
				var preferences = data.UserPreferences[userId];
				preferences.DateRange.m_Item1 = ctrl.dateFrom;
				preferences.DateRange.m_Item2 = ctrl.dateTo;
				console.log('pref',preferences)
				$.ajax({
					url: 'http://localhost/takeoff2016/api/group/changePreferences/' + 4 + '/' + groupId + '/',
					type: 'POST',
					data: JSON.stringify(preferences),
					contentType: 'application/json; charset=utf-8',
					// url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changePreferences/' + userId + '/' + groupId + '/',
					success: function(data) {
						console.log(data);
						console.log('success');
					},
					error: function(data) {
						console.log(data);
						console.log('error');
					}
				});
			});
		}
		
		//has to wait for DOM
		  $(document).ready( function() {
			  $('#date-from').datetimepicker({
				  format: 'YYYY/MM/DD',
			  });
			  $('#date-to').datetimepicker({
				  useCurrent: false,
				  format: 'YYYY/MM/DD',
			  });
			  $("#date-from").on("dp.change", function (e) {
				  $('#date-to').data("DateTimePicker").minDate(e.date);
			  });
			  $("#date-to").on("dp.change", function (e) {
				  $('#date-from').data("DateTimePicker").maxDate(e.date);
			  });
		  });

		$('#next-page').click(function () {
			console.log('next page');
			var savePreferences = function() {
				// what's to be sent in preferences?
				// tags are already sent, so
				// * budget
				// * date range

				var budgetFrom = $('#budget-from').val();
				var budgetTo = $('#budget-to').val();

				var dateFrom = $('#date-from').val();
				var dateTo = $('#date-to').val();

				var postPreferences = function (userId, groupId) {
					console.log('posting');
					
				}

				postPreferences(userId, groupId);
			}

			savePreferences();
		});

		$('.badgeSelect').click(function () {
			// highlight badge
			if ($(this).hasClass('active')) {
				$(this).removeClass('active');
			} else {
				$(this).addClass('active');
			}
			// /highglight badge

			// save selection
			var selectedName = $(this).attr('id');

			var postSelection = function (userId, groupId, selectedName) {
				$.ajax({
					url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changePreferences/' + userId + '/' + groupId + '/' + selectedName + '/',
					// url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changePreferences/' + userId + '/' + groupId + '/',
					success: function (data) {
						// console.log(data);
						// console.log('success');
					},
					error: function (data) {
						// console.log(data);
						// console.log('error');
					}
				});
			}

			postSelection(userId, groupId, selectedName);
			// /save selection
		});
		
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
					  console.log(days1, days2)
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
  });