angular.module('groupTripApp', [])
  .controller('PreferencesController', function () {
		var ctrl = this;
		var groupId = localStorage.getItem("groupId");
		var userId = localStorage.getItem("userId");

		// TODO
		// assume they're there
		groupId = 1;
		userId = 1;

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
					$.get('http://takeoff2016-krkteam.azurewebsites.net/api/group/' + groupId, function(data) {
						//console.log(data);

						// TODO userid zahardkodowane
						var preferences = data.UserPreferences[2];
						console.log(preferences);
						preferences.DateRange.m_Item1 = dateFrom;
						preferences.DateRange.m_Item2 = dateTo;

						preferences.PriceRange.m_Item1 = budgetFrom;
						preferences.PriceRange.m_Item2 = budgetTo;
						console.log(preferences);

						$.ajax({
							url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changePreferences/' + userId + '/' + groupId + '/',
							type: 'POST',
							data: preferences,
							contentType: 'application/json; charset=utf-8',
							// url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changePreferences/' + userId + '/' + groupId + '/',
							success: function(data) {
								// console.log(data);
								// console.log('success');
							},
							error: function(data) {
								// console.log(data);
								// console.log('error');
							}
						});
					});
				}

				postPreferences(userId, groupId);
			}

			savePreferences();
		});

		var highlightAssignedInDatabase = function (userId, groupId) {
			$.get('http://takeoff2016-krkteam.azurewebsites.net/api/group/' + groupId,
				// url: 'http://takeoff2016-krkteam.azurewebsites.net/api/group/changePreferences/' + userId + '/' + groupId + '/',
				function (data) {
					// console.log(data);

					var userPreferences = data.UserPreferences[userId];
					var localizations = userPreferences.Localizations;
					var tags = userPreferences.Tags;
					var mustHaves = userPreferences.MustHaves;

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
				}
			);
		}

		highlightAssignedInDatabase(userId, groupId);

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
      
  });