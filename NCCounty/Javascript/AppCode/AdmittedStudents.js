function admittedStudents(admitStudentsJson) {

	var admittedStudentsJson = JSON.parse(admitStudentsJson);

	var joinMap = {
		geoKey: "properties.Name",
		dataKey: "CountyName", //Must match TO property
		propertyMap: [
			{
				geoProperty: "Admitted",
				dataProperty: "Admitted" //Must match TO property
			},
			{
				geoProperty: "NotAdmitted",
				dataProperty: "NotAdmitted" //Must match TO property
			},
			{
				geoProperty: "Waitlist",
				dataProperty: "Waitlist" //Must match TO property
			},
			{
				geoProperty: "NoAction",
				dataProperty: "NoAction" //Must match TO property
			},
			{
				geoProperty: "Rejected",
				dataProperty: "Rejected" //Must match TO property
			}
		]
	};
	extendGeoJSON(county, admittedStudentsJson, joinMap);
	createAdmitStatusMap(county);
}

function choroplethAdmittedStudents(admitStudentsJson)
{
	var admittedStudentsJson = JSON.parse(admitStudentsJson);

	var joinMap = {
		geoKey: "properties.name",
		dataKey: "CountyName", //Must match TO property
		propertyMap: [
			{
				geoProperty: "Admitted",
				dataProperty: "Admitted" //Must match TO property
			},
			{
				geoProperty: "NotAdmitted",
				dataProperty: "NotAdmitted" //Must match TO property
			},
			{
				geoProperty: "Waitlist",
				dataProperty: "Waitlist" //Must match TO property
			},
			{
				geoProperty: "NoAction",
				dataProperty: "NoAction" //Must match TO property
			},
			{
				geoProperty: "Rejected",
				dataProperty: "Rejected" //Must match TO property
			}
		]
	};
	extendGeoJSON(county, admittedStudentsJson, joinMap);
	createChoroplethAdmitStatus(county);
}