function getTaxIndex(taxPerPerson, maximumTax){
	var maximumToAverageDifference = maximumTax * 2 - taxPerPerson;
	  
	if (maximumToAverageDifference < 0 ) return 0;
	
	// the further maximumTax is from taxPerPerson the better
	var result = maximumToAverageDifference / taxPerPerson * 50;
	
  // value cannot be higher than 100
	return Math.min(100, result);
};

/*
var cracovData= {"taxPerPerson": "60"};
var parisData= {"taxPerPerson": "70"};

console.log(getTaxesIndex(cracovData.taxPerPerson, 80));
console.log(getTaxesIndex(parisData.taxPerPerson, 80));
console.log(getTaxesIndex(cracovData.taxPerPerson, 40));
console.log(getTaxesIndex(parisData.taxPerPerson, 40));
*/