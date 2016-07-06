var getCityRatings = function (parameters) {
	var result = [];
	for (var i = 0; i < cityData.length; i++) {
		var currentCity = cityData[i];
		var cityRating = getCityRating(currentCity, parameters);

		var cityResult = {
			name: currentCity.name,
			lat: currentCity.lat,
			lng: currentCity.lng,
			rating: cityRating.rating,
			separateRatings: cityRating.separateRatings,
			infoText: currentCity.infoText
		}

		result.push(cityResult);
	}
	result.sort(function (a,b) {
		return b.rating - a.rating;
	});
	return result;
}