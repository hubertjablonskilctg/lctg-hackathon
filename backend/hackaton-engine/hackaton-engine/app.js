$(document).ready(function () {
    $.ajax({
        url: "http://takeoff2016-krkteam.azurewebsites.net/api/Test/1"
    }).then(function (data) {
        console.log(data.Id + " " + data.Name);
    });

    var parameters = {
	neededEmployees: 1200,
	maximumSalary: 3742, 
	maximumPropertyPrice: 1200000, 
	maximumTaxPerEmployee: 54
}

    var recalculate = function () {
        var ratings = getCityRatings(parameters);
        $("#ne").text(parameters.neededEmployees);
        $("#ms").text(parameters.maximumSalary);
        $("#mpp").text(parameters.maximumPropertyPrice);
        $("#mtpe").text(parameters.maximumTaxPerEmployee);
        refreshMarkers(ratings);
		renderRankingTable(ratings);
    };

    var setNeededEmployees = function (val) { 
        parameters.neededEmployees = val.value;
        recalculate();
    };
    
    var setMaximumSalary = function (val) { 
        parameters.maximumSalary = val.value;
        recalculate();
    };

    var setMaximumPropertyPrice = function (val) { 
        parameters.maximumPropertyPrice = val.value;
        recalculate();
    };

    var setMaximumTaxPerEmployee = function (val) { 
        parameters.maximumTaxPerEmployee = val.value;
        recalculate();
    };
	
	var renderRankingTable = function(data) {
		$('#ratingTable').html('');
		for(var i=0;i<data.length;i++) {
			$('#ratingTable').append('<p>'+data[i].name+'<span>'+data[i].rating+'</span></p>')
		};
	}
	
	$('#legendAndRanking h2').on('click', function() {
		var tabName = $(this).attr('data-tab');
		$('#legendAndRanking .child').hide();
		$('#legendAndRanking h2').removeClass('active');
		$('#'+tabName).show();
		$(this).addClass('active');
	});

    $('#needed-employees').slider({
        formatter: function (value) {
            return value;
        },
    }).on('slideStop', setNeededEmployees).data('slider');

    $('#maximum-salary').slider({
        formatter: function (value) {
            return value;
        },
    }).on('slideStop', setMaximumSalary).data('slider');

    $('#maximum-property-price').slider({
        formatter: function (value) {
            return value;
        },
    }).on('slideStop', setMaximumPropertyPrice).data('slider');

    $('#maximum-tax-per-employee').slider({
        formatter: function (value) {
            return value;
        },
    }).on('slideStop', setMaximumTaxPerEmployee).data('slider');

    recalculate();
});
