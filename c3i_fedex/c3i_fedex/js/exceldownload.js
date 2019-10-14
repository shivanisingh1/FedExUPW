function convert(dataURI) 
	{
		  // convert base64 to raw binary data held in a string
		  // doesn't handle URLEncoded DataURIs
		  var byteString;
		  if (dataURI.split(',')[0].indexOf('base64') >= 0)
			  byteString = atob(dataURI.split(',')[1]);
		  else
			  byteString = unescape(dataURI.split(',')[1]);
		  // separate out the mime component
		  var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
		  // write the bytes of the string to an ArrayBuffer
		  var ab = new ArrayBuffer(byteString.length);
		  var ia = new Uint8Array(ab);
		  for (var i = 0; i < byteString.length; i++) {
			  ia[i] = byteString.charCodeAt(i);
		  }
		  // write the ArrayBuffer to a blob, and you're done
		  return new Blob([ab],{type: mimeString});
	}
	
	 var tablesToExcel = (function() {
    var uri = 'data:application/vnd.ms-excel;base64,'
    , tmplWorkbookXML = '<?xml version="1.0"?><?mso-application progid="Excel.Sheet"?><Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">'
      + '<DocumentProperties xmlns="urn:schemas-microsoft-com:office:office"><Author>ThaiND</Author><Created>{created}</Created></DocumentProperties>'
	  +'<Styles><Style ss:ID="Default" ss:Name="Normal"><Alignment ss:Horizontal="Center" ss:WrapText="1"/><Borders></Borders><Font ss:Size="8"/><Interior /><NumberFormat /><Protection /></Style><Style ss:ID="s21"><Alignment ss:WrapText="1" ss:Horizontal="Center" ss:Vertical="Top"/><Font ss:Size="8" ss:Bold="1" /><Interior ss:Color="#999999" ss:Pattern="Solid" /><Borders><Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/></Borders></Style> <Style ss:ID="s22"><Alignment ss:WrapText="1" ss:Horizontal="Left" ss:Indent="1" ss:Vertical="Top"/><Font ss:Size="8" ss:Bold="1" /><Interior ss:Color="#ffff99" ss:Pattern="Solid" /><Borders><Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/></Borders></Style><Style ss:ID="s26"><Alignment ss:WrapText="1" ss:Horizontal="Center" ss:Vertical="Top"/><Font ss:Size="8" ss:Bold="1" /><Interior ss:Color="#ffff99" ss:Pattern="Solid" /><Borders><Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/></Borders></Style> <Style ss:ID="s23"><Alignment ss:Horizontal="Center" ss:WrapText="1"/><Borders><Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="0"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="0"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="0"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="0"/></Borders><Font ss:Bold="1"/><Interior /><NumberFormat /><Protection /></Style><Style ss:ID="s24"><Alignment ss:Horizontal="Left" ss:Indent="1" ss:WrapText="1"/><Borders><Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/></Borders><Font/><Interior /><NumberFormat /><Protection /></Style><Style ss:ID="s28"><Alignment ss:WrapText="1" ss:Horizontal="Left" ss:Indent="1" ss:Vertical="Top"/><Font ss:Size="8" ss:Bold="1" /><Interior ss:Color="#cccccc" ss:Pattern="Solid" /><Borders><Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/></Borders></Style><Style ss:ID="s29"><Alignment ss:WrapText="1" ss:Horizontal="Center" ss:Vertical="Top"/><Font ss:Size="8" ss:Bold="1" /><Interior ss:Color="#cccccc" ss:Pattern="Solid" /><Borders><Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/></Borders></Style></Styles>'
      + '{worksheets}</Workbook>'
    , tmplWorksheetXML = '<Worksheet ss:Name="{nameWS}"><Table><Column ss:AutoFitWidth="0" ss:Width="35"/><Column ss:AutoFitWidth="0" ss:Width="130"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/><Column ss:AutoFitWidth="0" ss:Width="50"/>{rows}</Table></Worksheet>'
	, tmplCellXML = '<Cell ss:StyleID="{sid}" ss:MergeAcross="{mrg}"><Data ss:Type="{dtype}">{data}</Data></Cell>'
    , base64 = function(s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) }
	
    return function(tables, wsnames, wbname) {
		var ctx = "";
		var workbookXML = "";
		var worksheetsXML = "";
		var rowsXML = "";
		
		for (var i = 0; i < tables.length; i++) 
		{
		
			if (!tables[i].nodeType) 
			tables[i] = document.getElementById(tables[i]);
		
			for (var j = 0; j < tables[i].rows.length; j++) 
			{
				rowsXML += '<Row>'
				for (var k = 0; k < tables[i].rows[j].cells.length; k++) 
				{
					
					var cols=tables[i].rows[j].cells[k].getAttribute("colspan");
					var rws=tables[i].rows[j].cells[k].getAttribute("rowspan");
					var styl=tables[i].rows[j].cells[k].getAttribute("bgcolor");
					var styl2=tables[i].rows[j].getAttribute("bgcolor");
					var cls=tables[i].rows[j].cells[k].getAttribute("class");
					var algn=tables[i].rows[j].cells[k].getAttribute("align");
					var cls2=tables[i].rows[j].getAttribute("class");
					
						var dataType = 'String';
						var dataValue = tables[i].rows[j].cells[k].innerHTML;
						var typeD="";
						var parsed=parseInt(dataValue);
						
						if (parsed==dataValue)
						{
							typeD="Number";
						}
						else
						{
							typeD="String";
						}
					var n;
						if(cls)
						{
							n=cls.indexOf("value"); 
						}		
						
					if(cls=="all2" || n>=0)
					{
						if(cols)
						{
							ctx = {data: dataValue, mrg: cols-1, sid:"s23",dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
						else
						{
						
							ctx = {data: dataValue, mrg: 0, sid: "s23",dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
					}
					
					else if(cls2=="ylw")
					{
						if(k==1&&j>3)
						var cl="s22";
						else
						var cl="s26";
						
						if(cols)
						{
							ctx = {data: dataValue, mrg: cols-1, sid:cl,dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
						else
						{
						
							ctx = {data: dataValue, mrg: 0, sid:cl,dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
					}
					
					else if(cls2=="gray")
					{
						if(k==1&&j>3)
						var cl="s28";
						else
						var cl="s29";
						
						if(cols)
						{
							ctx = {data: dataValue, mrg: cols-1, sid:cl,dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
						else
						{
						
							ctx = {data: dataValue, mrg: 0, sid:cl,dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
					}
					else if(styl2)
					{
						if(k==1&&j>3)
						var cl="s22";
						else
						var cl="s26";
						
						if(cols)
						{
							ctx = {data: dataValue, mrg: cols-1, sid:cl,dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
						else
						{
						
							ctx = {data: dataValue, mrg: 0, sid:cl,dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
					}
					
					
					else if(styl)
					{
						if(cols)
						{
							ctx = {data: dataValue, mrg: cols-1, sid:"s21",dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
						else
						{
							ctx = {data: dataValue, mrg: 0, sid: "s21",dtype:typeD};
							rowsXML += format(tmplCellXML, ctx);
						}
					}
					
					else
					{
						if(cols)
						{
							if(algn=="left")
							{
								ctx = {data: dataValue, mrg: cols-1, sid:"s24",dtype:typeD};
								rowsXML += format(tmplCellXML, ctx);
							}
							else
							{
								ctx = {data: dataValue, mrg: cols-1, sid:"s24",dtype:typeD};
								rowsXML += format(tmplCellXML, ctx);
							}
						}
						else
						{
							if(algn=="left")
							{
								ctx = {data: dataValue, mrg: 0, sid:"s24",dtype:typeD};
								rowsXML += format(tmplCellXML, ctx);
							}
							else
							{
								ctx = {data: dataValue, mrg: 0, sid: "s24",dtype:typeD};
								rowsXML += format(tmplCellXML, ctx);
							}
						}
					
					}
					
				}
				  rowsXML += '</Row>'
			}
			ctx = {rows: rowsXML, nameWS: wsnames[i] || 'Sheet' + i};
			worksheetsXML += format(tmplWorksheetXML, ctx);
			rowsXML = "";
      }
		ctx = {created: (new Date()).getTime(), worksheets: worksheetsXML};
		workbookXML = format(tmplWorkbookXML, ctx);
		console.log(workbookXML);
		var blobUrl = URL.createObjectURL(convert(uri + base64(workbookXML)));
		var link = document.createElement("A");
		link.href = blobUrl;
		//link.href = uri + base64(workbookXML);
		link.download = wbname || 'Workbook.xls';
		link.target = '_blank';
		document.body.appendChild(link);
		link.click();
		document.body.removeChild(link);
    }
  })();