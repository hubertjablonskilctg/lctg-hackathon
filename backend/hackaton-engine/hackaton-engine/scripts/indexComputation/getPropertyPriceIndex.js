function getPropertyPriceIndex(propertPrice, maximumPrice){
	var maximumToAverageDifference = 4 * maximumPrice - propertPrice;
	  
	if (maximumToAverageDifference < 0 ) return 0;
	
	// the further maximumPrice is from averagePropertyPrice the better
	var result = maximumToAverageDifference / propertPrice * 22;
	
  // value cannot be higher than 100
	return Math.min(100, result);
};

/*
var cracovData= {"propertPrice": "260000"};
var parisData= {"propertPrice": "3332110"};

console.log(getPropertyPriceIndex(cracovData.propertPrice, 240000));
console.log(getPropertyPriceIndex(parisData.propertPrice, 2000000));
*/