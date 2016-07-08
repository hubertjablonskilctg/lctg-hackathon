angular.module('groupTripApp', [])
  .controller('PreferencesController', function ($scope, $window) {
		var ctrl = this;
		var groupId = localStorage.getItem("groupId");
		var userId = localStorage.getItem("userId");

		
		//get initial data
		$.ajax({
          url: "http://takeoff2016-krkteam.azurewebsites.net/api/group/" + groupId,
		  type: 'GET',
		  contentType: 'application/json; charset=utf-8',
          success: function (data) {
			  console.log(data)
				refreshData(data)
          },
          error: function (data) {
              console.log(data);
          }
		});
		
		var refreshData = function(data) {
			var userPreferences = data.UserPreferences[userId];
			var localizations = userPreferences.Localizations;
			var tags = userPreferences.Tags;
			var mustHaves = userPreferences.MustHaves;
			ctrl.selectedDates = [];
			angular.forEach(data.Users, function (user) {
			  if (data.UserPreferences && data.UserPreferences[user.Id]) {
				  ctrl.selectedDates.push([moment(data.UserPreferences[user.Id].DateRange.m_Item1).format('YYYY/MM/DD'), moment(data.UserPreferences[user.Id].DateRange.m_Item2).format('YYYY/MM/DD'),user.Email, 'user']);
				  if(user.Id == userId) {
					ctrl.datesSelectedValues = moment(data.UserPreferences[user.Id].DateRange.m_Item1).format('YYYY/MM/DD') + ' - ' + moment(data.UserPreferences[user.Id].DateRange.m_Item2).format('YYYY/MM/DD');
					ctrl.dateFrom = moment(data.UserPreferences[user.Id].DateRange.m_Item1).format('YYYY/MM/DD');
					ctrl.dateTo = moment(data.UserPreferences[user.Id].DateRange.m_Item2).format('YYYY/MM/DD');
					ctrl.datesSelected = true;
				  }
			  }

			});
			ctrl.timelinesOverlapping = false; //checkIfAllDatesOverlaps();
			drawChart();				
			createTagCloud(data);
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
		}
		
		ctrl.editSelectedDates = function() {
			ctrl.selectedDates.pop();
			  ctrl.datesSelected = false;
			  drawChart();
		}
		
		ctrl.savePreferences = function() {
			$.get('http://takeoff2016-krkteam.azurewebsites.net/api/group/' + groupId, function(data) {
				
				var preferences = data.UserPreferences[userId];
				preferences.DateRange.m_Item1 = moment($('#date-from').val()).format()+'Z';
				preferences.DateRange.m_Item2 = moment($('#date-to').val()).format()+'Z';
				
				preferences.PriceRange.m_Item1 = $('#budgetFrom').text();
				preferences.PriceRange.m_Item2 = $('#budgetTo').text();
				
				console.log('pref',preferences)
				$.ajax({
					url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changePreferences/' + userId + '/' + groupId + '/',
					type: 'POST',
					data: JSON.stringify(preferences),
					contentType: 'application/json; charset=utf-8',
					success: function(data) {
						refreshData(data)
						console.log('success');
					},
					error: function(data) {
						console.log(data);
						console.log('error');
					}
				});
			});
		}

		ctrl.showHotels = function () {
			$.get('http://takeoff2016-krkteam.azurewebsites.net/api/group/' + groupId, function(data) {
				
				var preferences = data.UserPreferences[userId];
				preferences.DateRange.m_Item1 = moment($('#date-from').val()).format()+'Z';
				preferences.DateRange.m_Item2 = moment($('#date-to').val()).format()+'Z';
				
				preferences.PriceRange.m_Item1 = $('#budgetFrom').text();
				preferences.PriceRange.m_Item2 = $('#budgetTo').text();
				
				$.ajax({
					url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changePreferences/' + userId + '/' + groupId + '/',
					type: 'POST',
					data: JSON.stringify(preferences),
					contentType: 'application/json; charset=utf-8',
					success: function(data) {
						$window.location.href = '/results.html';
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

			  var slideChange = function (val) {
			      $("#budgetFrom").text(val.value[0]);
			      $("#budgetTo").text(val.value[1]);
			  };

			  $('#sliderinput').slider({
			      formatter: function (value) {
			          return value;
			      },
			  }).on('slideStop', slideChange).data('slider');
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
						refreshData(data)
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
					  max = dataTable[i+1][1];
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
	  
	  function createTagCloud (data) {
			var tagsAlready = $('#tagCloud').html();
		  var allTags = [];
			Object.keys(data.UserPreferences).forEach(function(key,index) {
				var current = data.UserPreferences[key];
				if(current == null) return;
				allTags = allTags.concat(current.Tags);
				allTags = allTags.concat(current.Localizations);
			});

			var results = {};
			for (var i = 0; i < allTags.length; i++){
				if (allTags[i] in results){
					results[allTags[i]]++;
				}
				else{
					results[allTags[i]] = 1;
				}
			}

			var jqCloudResults = [];
			var resultKeys = Object.keys(results);
			for (var i = 0; i < resultKeys.length; i++) {
				// console.log('text', resultKeys[i]);
				// console.log('results', results[resultKeys[i]]);

				var tmp = { text: resultKeys[i], weight: results[resultKeys[i]] };
				jqCloudResults.push(tmp);
			}
			ctrl.tagCloudResults = jqCloudResults.length;

			if(!tagsAlready) {
				$('#tagCloud').jQCloud(jqCloudResults, {
						width: 200,
						height: 200,
						colors: ["#ffffff"]
				});
			} else {
				$('#tagCloud').html('')
				$('#tagCloud').jQCloud('update', jqCloudResults);
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
  });