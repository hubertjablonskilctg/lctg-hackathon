angular.module('groupTripApp', [])
  .controller('TimelineController', function () {
      var ctrl = this;

      var groupId = localStorage.getItem("groupId");

     // $.ajax({
     //     url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/' + groupId,
     //     type: 'GET',
     //     data: JSON.stringify([1]),
     //     contentType: 'application/json; charset=utf-8',
     //     success: function (data) {
     //         console.log(data);

     //     },
     //     error: function (data) {
     //         console.log(data);
     //     }
     // }
     //)

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
                "Disability1",
                "ChildFriendly"
              ],
              "PriceRange": {
                  "m_Item1": 50,
                  "m_Item2": 100
              },
              "DateRange": {
                  "m_Item1": "2016-07-07T12:00:00+02:00",
                  "m_Item2": "2016-07-07T12:00:00+02:00"
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

                  console.log(data);
                  ctrl.addNewDate();

              },
              error: function (data) {
                  console.log(data);
              }
          }
         )
      }


      google.charts.load('current', { 'packages': ['timeline'] });
      google.charts.setOnLoadCallback(drawChart);

      $('#fromDate').datetimepicker({
          format: 'DD/MM/YYYY',
      });
      $('#toDate').datetimepicker({
          useCurrent: false,
          format: 'DD/MM/YYYY',
      });
      $("#fromDate").on("dp.change", function (e) {
          $('#toDate').data("DateTimePicker").minDate(e.date);
      });
      $("#toDate").on("dp.change", function (e) {
          $('#fromDate').data("DateTimePicker").maxDate(e.date);
      });

      ctrl.selectedDates = [['Who', 'From', 'To'],
      ['Bob', new Date(2016, 4, 29), new Date(2016, 5, 8)],
      ['Mike', new Date(2016, 4, 27), new Date(2016, 5, 14)],
      ['Frank', new Date(2016, 5, 1), new Date(2016, 5, 10)],
      ['Michelle', new Date(2016, 4, 25), new Date(2016, 5, 7)]];

      ctrl.timelinesOverlapping = checkIfAllDatesOverlaps();
      ctrl.datesSelected = false;

      ctrl.addNewDate = function () {
          ctrl.selectedDates.pop();
          ctrl.selectedDates.push([
              'Rob',
              new Date(moment($('#fromDate').val(), 'DD/MM/YYYY').format('YYYY/MM/DD')),
              new Date(moment($('#toDate').val(), 'DD/MM/YYYY').format('YYYY/MM/DD'))
          ]);
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
          ctrl.selectedDates.pop();
          ctrl.selectedDates.pop();
          ctrl.datesSelected = false;
          drawChart();
      };

      function drawChart() {
          var container = document.getElementById('timeline');
          var chart = new google.visualization.Timeline(container);
          var dataTable = new google.visualization.DataTable();

          ctrl.selectedDates.push(findCommonRange());

          var numRows = ctrl.selectedDates.length;
          var numCols = ctrl.selectedDates[0].length;

          dataTable.addColumn('string', ctrl.selectedDates[0][0]);

          for (var i = 1; i < numCols; i++)
              dataTable.addColumn('date', ctrl.selectedDates[0][i]);

          for (var i = 1; i < numRows; i++)
              dataTable.addRow(ctrl.selectedDates[i]);

          chart.draw(dataTable, { height: (numRows + 1) * 38 });
      }

      function checkIfAllDatesOverlaps() {
          var numRows = ctrl.selectedDates.length;
          var doesntOverlaps = 0;
          for (var i = 1; i < numRows; i++) {
              if (ctrl.selectedDates[i + 1]) {
                  var start1 = moment(ctrl.selectedDates[i][1]);
                  var end1 = moment(ctrl.selectedDates[i][2])
                  var start2 = moment(ctrl.selectedDates[i + 1][1]);
                  var end2 = moment(ctrl.selectedDates[i + 1][2])
                  var duration1 = moment.duration(start1.diff(end2, 'days', true));
                  var days1 = duration1.asDays();
                  var duration2 = moment.duration(start2.diff(end1, 'days', true));
                  var days2 = duration2.asDays();
                  if (!(days1 < 0 && days2 < 0)) {
                      doesntOverlaps++;
                  }
              }
          }
          return (doesntOverlaps) ? false : true;
      }

      function findCommonRange() {
          var numRows = ctrl.selectedDates.length;
          var startDate = ctrl.selectedDates[1][1];
          var endDate = ctrl.selectedDates[1][1];
          for (var i = 1; i < numRows; i++) {
              if (ctrl.selectedDates[i + 1]) {
                  var start1 = moment(ctrl.selectedDates[i][1]);
                  var start2 = moment(ctrl.selectedDates[i + 1][1]);
                  var duration1 = moment.duration(start1.diff(start2, 'days', true));
                  var days1 = duration1.asDays();
                  if (days1 < 0)
                      startDate = start2;
                  var end1 = moment(ctrl.selectedDates[i][2]);
                  var end2 = moment(ctrl.selectedDates[i + 1][2]);
                  var duration2 = moment.duration(end1.diff(end2, 'days', true));
                  var days2 = duration2.asDays();
                  if (days2 > 0)
                      endDate = end2;
              }
          }
          var duration = moment.duration(endDate.diff(startDate)).asDays();
          if (duration < 7) {

          } else {

          }
          return ['Common daterange', new Date(startDate), new Date(endDate)];
      }
  })
