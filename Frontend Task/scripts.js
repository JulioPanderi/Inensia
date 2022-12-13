document.addEventListener('readystatechange', function(event) {
    if (document.readyState === "complete") {
        let xmlData = getXmlData();
		//createTable(xmlData);
    }
});

async function getXmlData()
{
	//console.log(xml);
	const myRequest = new Request('https://test.ce2s.net/Study.xml');
	fetch(myRequest)
    .then((response) => response.text())
    .then((xmlData) => {
		createTable(xmlData)
    });
};

function createTable(xmlData)
{
	let pathSimulationResult = "/Result/Item[@alias='sys_System'][position()='rowIndexValue']/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[@alias='ar_SimulationResult']/ar_resultname/text()" + ";" +
							   "/Result/Item[@alias='sys_System'][position()='rowIndexValue']/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[@alias='ar_SimulationResult']/ar_resultvalue/text()" + ";" +
							   "/Result/Item[@alias='sys_System'][position()='rowIndexValue']/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[@alias='ar_SimulationResult']/ar_resultunit/text()" +  ";" +
							   "/Result/Item[@alias='sys_System'][position()='rowIndexValue']/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[@alias='ar_SimulationResult']/ar_target_met/text()";
	/*
		sys_System -- name
		sm_Study -- name, state
		sm_Task -- keyed_name
		ar_SimulationResult -- ar_resultname, ar_resultvalue, ar_resultunit, ar_target_met
		re_Requirement -- ar_conditionexp
	*/
	let columns = [
		  ["Name",  "//Result/Item[@alias='sys_System'][position()='rowIndexValue']/name[1]/text()"],
		  ["Study", "//Result/Item[@alias='sys_System'][position()='rowIndexValue']/Relationships[1]/Item[1]/Relationships[1]/Item[1]/state[1]/text()"],
		  ["Task",  "//Result/Item[@alias='sys_System'][position()='rowIndexValue']/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[@alias='sm_Task']/keyed_name/text()"],
		  ["Simulation Result", pathSimulationResult],
		  ["Requirement", "/Result/Item[@alias='sys_System'][position()='rowIndexValue']/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1]/Relationships[1]/Item[1][@alias='re_Requirement']/ar_conditionexp[1]/text()"]
		];

	//Create main table
	let table = document.createElement('table');
	document.body.appendChild(table);
	
	//Add headers
	createTableHeader(table, columns);
	
	//Add body data
	createTableBody(table, columns, xmlData);
}

function createTableBody(table, columns, xmlData)
{
	let iniPath = "//Result[position()=1]/Item[@alias='sys_System']";
	let rows = getXmlElementByXpath(xmlData, iniPath);

	if (rows != null)
	{
		let rowsCount = countXmlElementByXpath(rows, iniPath);
		let tb = table.createTBody();
		
		for (let i = 0; i < rowsCount; i++)
		{
			let tr = tb.insertRow();
			
			for (let ii = 0; ii < columns.length; ii++)
			{
				//Get the xpath for the column value
				let xPaths = columns[ii][1].split(";");
				let colValue = "";
				
				if (xPaths.length == 1)
				{
					//Assing column value
					colValue = getXmlElementByXpath(xmlData, xPaths[0].replace("'rowIndexValue'", (i + 1).toString())).nodeValue;
				}
				else
				{
					//Assing column multiple value
					for (let iii = 0; iii < xPaths.length; iii++)
					{
						if (colValue != "")
							colValue = colValue + " ";
						colValue = colValue + getXmlElementByXpath(xmlData, xPaths[iii].replace("'rowIndexValue'", (i + 1).toString())).nodeValue;
					}
				}
				let td = tr.insertCell();
				td.innerText = colValue;
			}
		}
	}
}

function createTableHeader(table, columns)
{
	var th = table.createTHead();
	let tr = th.insertRow();
	
	for (let i = 0; i < columns.length; i++)
	{
		let td = tr.insertCell();
		td.innerText = columns[i][0];
	}
}

function getXmlElementByXpath(xmlText, path) {
	let parser = new DOMParser();
	let xmlDoc = parser.parseFromString(xmlText, "text/xml");
	let retValue = xmlDoc.evaluate(path, xmlDoc, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
	return retValue;
}

function countXmlElementByXpath(xmlText, path) {
	let parser = new DOMParser();
	let xmlDoc = parser.parseFromString(xmlText, "text/xml");
	parser = parser.parseFromString(xmlText, "text/xml");
	return xmlDoc.documentElement.childElementCount;
}