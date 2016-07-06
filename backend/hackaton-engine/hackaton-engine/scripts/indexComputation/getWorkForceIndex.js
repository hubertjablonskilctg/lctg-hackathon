function getWorkForceIndex(workForce, employeeNeeds){
	var ratioCurrent = (workForce.current /  employeeNeeds) * 10;	
	var ratioChange = workForce.yearChange / 0.05 * employeeNeeds * 10;
	
	if(ratioCurrent > 50){
		ratioCurrent = 50;
  }
	else if(ratioCurrent< 0){
		ratioCurrent = 0;
  }
	
	if(ratioChange > 50){
		ratioChange = 50;
  }
	else if(ratioChange<0){
		ratioChange =0;
  }
	
	return (ratioChange + ratioCurrent);
};


/* sample usage
var cracovData= '{"current": "1000", "yearChange": "200"}';
var parisData= '{"current": "3310", "yearChange": "450"}';

var cracovData = {
  current: 1000,
  yearChange: 200
}

var parisData = {
  current: 3310,
  yearChange: 450
}


console.log(getWorkForceIndex(cracovData, 400));
console.log(getWorkForceIndex(parisData, 400));
*/