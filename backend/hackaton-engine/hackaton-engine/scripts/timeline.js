angular.module('groupTripApp', [])
  .controller('TimelineController', function () {
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

	var selectedDates = [['Who', 'From', 'To'],
	['Bob',  new Date(2016, 4, 29), new Date(2016, 5, 4)],
	['Mike',  new Date(2016, 4, 27),  new Date(2016, 5, 14)],
	['Frank',  new Date(2016, 5, 1),  new Date(2016, 5, 10)]];

	$('#addTime').click( function() {
		selectedDates.push([
			'Rob',
			new Date(moment($('#fromDate').val(), 'DD/MM/YYYY').format('YYYY/MM/DD')),
			new Date(moment($('#toDate').val(), 'DD/MM/YYYY').format('YYYY/MM/DD'))
		]);
		drawChart();
	});


	function drawChart() {
		var container = document.getElementById('timeline');
		var chart = new google.visualization.Timeline(container);
		var dataTable = new google.visualization.DataTable();
		
		var numRows = selectedDates.length;
		var numCols = selectedDates[0].length;

		dataTable.addColumn('string', selectedDates[0][0]);

		for (var i = 1; i < numCols; i++)
		dataTable.addColumn('date', selectedDates[0][i]);           

		for (var i = 1; i < numRows; i++)
		dataTable.addRow(selectedDates[i]);            

		chart.draw(dataTable, { height: (numRows+1)*38 });
	}

	function checkIfAllDatesOverlaps() {
		var numRows = selectedDates.length;
		var numCols = selectedDates[0].length;
		
		for (var i = 1; i < numRows; i++) {
			if(selectedDates[i+1]) {
				var start1 = moment(selectedDates[i][1]);
				var end1 = moment(selectedDates[i][2])
				var start2 = moment(selectedDates[i+1][1]);
				var end2 = moment(selectedDates[i+1][2])
				console.log(start1, end2)
				console.log(start1.diff(end1, 'days', true))
				var duration1 = moment.duration(start1.diff(end1, 'days', true));
				var days1 = duration1.asDays();
				console.log(days1)
				var duration2 = moment.duration(start2.diff(end2, 'days', true));
				var days2 = duration2.asDays();
				if(days1 && days2) {
					console.log(true)
				}
			}
			
			//var duration = moment.duration(now.diff(end));
			//var days = duration.asDays();
			
		
		}
	}

	checkIfAllDatesOverlaps();
});