var getSalaryIndex = function(cityData, maximumSalaryRange){
	var debug = false;
	
	var maximumToAverageDifference = maximumSalaryRange - cityData.averageSalary;
	
	if (maximumToAverageDifference < 0 ) {
		if(debug){
			console.log('maximumToAverageDifference');
		}
		
		return 0;
	}
	
	// the further maximumSalaryRange is from averageSalaryRange the better
	var result = maximumToAverageDifference / cityData.averageSalary * 100;
	
	if (debug){
		console.log('maximumSalaryRange = ' + maximumSalaryRange);
		console.log('cityData.averageSalary = ' + cityData.averageSalary);
		console.log('maximumToAverageDifference = ' + maximumToAverageDifference);
		console.log('cityData.averageSalary = ' + cityData.averageSalary);
		console.log('result = ' + result);
	}
	
  // value cannot be higher than 100
	return Math.min(100, result);
}


// sample usage
// var result = getSalaryIndex(cracovData, 1230);
// console.log(result);
// console.log(getSalaryIndex(parisData, 1230));
