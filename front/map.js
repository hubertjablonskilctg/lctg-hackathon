var map;
var markerColors = ['ff0033', 'ff3333', 'ff6633', 'ff9933', 'ffcc33', 'ffff33', 'ccff33', '66ff33', '33ff33', '00ff33']
var markers = [];

var places = [
	{
		name: 'London',
		lat: 51.5085300,
		lng: -0.1257400,
		infoText: 'something<br />something else',
		rating: 83
	},
	{
		name: 'Berlin',
		lat: 52.5243700,
		lng: 13.4105300,
		infoText: 'another info<br />to test',
		rating: 23
	},
	{
		name: 'Paris',
		lat: 48.8534100,
		lng: 2.3488000,
		infoText: 'paris description',
		rating: 58
	},
	{
		name: 'Rome',
		lat: 41.8919300,
		lng: 12.5113300,
		infoText: 'rome description',
		rating: 62
	},
	{
		name: 'Madrid',
		lat: 40.4165000,
		lng: -3.7025600,
		infoText: 'campeones! ;)',
		rating: 18
	},
	{
		name: 'Vienna',
		lat: 48.2084900,
		lng: 16.3720800,
		infoText: 'vienna description',
		rating: 3
	},
	{
		name: 'Warsaw',
		lat: 52.2297700,
		lng: 21.0117800,
		infoText: 'warsaw description',
		rating: 48
	},
	{
		name: 'Zurych',
		lat: 47.3666700,
		lng: 8.5500000,
		infoText: 'zurych description',
		rating: 91
	},
	{
		name: 'Budapest',
		lat: 47.4980100,
		lng: 19.0399100,
		infoText: 'budapest description',
		rating: 77
	},
	{
		name: 'Amsterdam',
		lat: 52.3740300,
		lng: 4.8896900,
		infoText: 'amsterdam description',
		rating: 34
	}
]

function render() {
	map = new google.maps.Map(document.getElementById('map'), {});
	drawMarkers(null);
}

function drawMarkers(data) {
	var bounds = new google.maps.LatLngBounds();
	if (data) {
		for (var i = 0; i < data.length; i++) {
			var position = new google.maps.LatLng(data[i].lat, data[i].lng);

			bounds.extend(position);
			marker = addMarker(data[i]);
			var infoWindow = new google.maps.InfoWindow(), marker, i;
			google.maps.event.addListener(marker, 'click', (function (marker, i) {
				var infoData = '<h3>'+data[i].name+'</h3><p>City rating: <strong>'+data[i].rating+'</strong></p><ul>';
				for (var key in data[i].separateRatings) {
					if (data[i].separateRatings.hasOwnProperty(key)) {
						infoData += '<li>'+key+': '+data[i].separateRatings[key]+'</li>';
					}
				}
				infoData += '</ul>';
				return function () {
					infoWindow.setContent(infoData);
					infoWindow.open(map, marker);
				}
			})(marker, i));

			// Automatically center the map fitting all markers on the screen
		}
	}
	map.fitBounds(bounds);
	var boundsListener = google.maps.event.addListener((map), 'bounds_changed', function (event) {
		this.setZoom(5);
		google.maps.event.removeListener(boundsListener);
	});
}

function refreshMarkers(data) {
	deleteMarkers();
	drawMarkers(data);
}

function addMarker(place) {
	var rating = parseInt(place.rating/10);
	var marker = new google.maps.Marker({
		position: { lat: place.lat, lng: place.lng },
		map: map,
		title: place.name,
		icon: 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|' + markerColors[rating]
	});
	markers.push(marker);
	return marker;
}

function setMapOnAll(map) {
	for (var i = 0; i < markers.length; i++) {
		markers[i].setMap(map);
	}
}

function deleteMarkers() {
	setMapOnAll(null)
	markers = [];
}