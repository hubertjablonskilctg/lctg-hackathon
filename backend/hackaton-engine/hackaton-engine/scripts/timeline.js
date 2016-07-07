angular.module('groupTripApp', [])
  .controller('TimelineController', function () {
	var ctrl = this;  
	  
	google.charts.load('current', {'packages':['timeline']});
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
	['Bob',  new Date(2016, 4, 29), new Date(2016, 5, 8)],
	['Mike',  new Date(2016, 4, 27),  new Date(2016, 5, 14)],
	['Frank',  new Date(2016, 5, 1),  new Date(2016, 5, 10)],
	['Michelle',  new Date(2016, 4, 25),  new Date(2016, 5, 7)]];

	ctrl.timelinesOverlapping = checkIfAllDatesOverlaps();
	
	ctrl.addNewDate = function() {
		ctrl.selectedDates.push([
			'Rob',
			new Date(moment($('#fromDate').val(), 'DD/MM/YYYY').format('YYYY/MM/DD')),
			new Date(moment($('#toDate').val(), 'DD/MM/YYYY').format('YYYY/MM/DD'))
		]);
		if(!checkIfAllDatesOverlaps()) {
			ctrl.selectedDates.pop();
			ctrl.timelinesOverlapping = false;
		} else {
			findCommonRange();
			ctrl.selectedDates.push(findCommonRange());
			console.log(ctrl.selectedDates)
			drawChart();
		}
	}

	function drawChart() {
		var container = document.getElementById('timeline');
		var chart = new google.visualization.Timeline(container);
		var dataTable = new google.visualization.DataTable();
		
		var numRows = ctrl.selectedDates.length;
		var numCols = ctrl.selectedDates[0].length;

		dataTable.addColumn('string', ctrl.selectedDates[0][0]);

		for (var i = 1; i < numCols; i++)
		dataTable.addColumn('date', ctrl.selectedDates[0][i]);           

		for (var i = 1; i < numRows; i++)
		dataTable.addRow(ctrl.selectedDates[i]);            

		chart.draw(dataTable, { height: (numRows+1)*38 });
	}

	function checkIfAllDatesOverlaps() {
		var numRows = ctrl.selectedDates.length;
		var doesntOverlaps = 0;
		for (var i = 1; i < numRows; i++) {
			if(ctrl.selectedDates[i+1]) {
				var start1 = moment(ctrl.selectedDates[i][1]);
				var end1 = moment(ctrl.selectedDates[i][2])
				var start2 = moment(ctrl.selectedDates[i+1][1]);
				var end2 = moment(ctrl.selectedDates[i+1][2])
				var duration1 = moment.duration(start1.diff(end2, 'days', true));
				var days1 = duration1.asDays();
				var duration2 = moment.duration(start2.diff(end1, 'days', true));
				var days2 = duration2.asDays();
				if(!(days1 < 0 && days2 < 0)) {
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
			if(ctrl.selectedDates[i+1]) {
				var start1 = moment(ctrl.selectedDates[i][1]);
				var start2 = moment(ctrl.selectedDates[i+1][1]);
				var duration1 = moment.duration(start1.diff(start2, 'days', true));
				var days1 = duration1.asDays();
				if(days1<0)
					startDate = start2;
				var end1 = moment(ctrl.selectedDates[i][2]);
				var end2 = moment(ctrl.selectedDates[i+1][2]);
				var duration2 = moment.duration(end1.diff(end2, 'days', true));
				var days2 = duration2.asDays();
				if(days2>0)
					endDate = end2;
			}
		}
		return ['Common daterange', new Date(startDate), new Date(endDate)];
	}
});