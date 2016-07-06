var getCityRating = function(cityData, parameters){
		// available workforce
		// salary range
		// property prices
		// maximum tax company is willing to pay
		
		var workForceIndex = getWorkForceIndex(cityData.workForce, parameters.neededEmployees);
		var salaryIndex = getSalaryIndex(cityData, parameters.maximumSalary);
		var propertyPriceIndex = getPropertyPriceIndex(cityData.averagePropertyPrice, parameters.maximumPropertyPrice);
		var taxIndex = getTaxIndex(cityData.taxPerPerson, parameters.maximumTaxPerEmployee);
		
		// truncate to integer value
		workForceIndex = parseInt(workForceIndex, 10);
		salaryIndex = parseInt(salaryIndex, 10);
		propertyPriceIndex = parseInt(propertyPriceIndex, 10);
		taxIndex = parseInt(taxIndex, 10);
		
		var indexCount = 4;
		var indexSum = workForceIndex + salaryIndex + propertyPriceIndex + taxIndex;
		
		var separateRatings = {
			workForceIndex: workForceIndex,
			salaryIndex: salaryIndex,
			propertyPriceIndex: propertyPriceIndex,
			taxIndex: taxIndex
		};
		
		var resultAsInt = parseInt(indexSum / indexCount, 10);
		
		return {
			rating: resultAsInt,
			separateRatings: separateRatings
		};
}
