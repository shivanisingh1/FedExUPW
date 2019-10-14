
 function myFunction() {
    var table = document.getElementById("myTable");
    var row = table.insertRow(-1);
    var cell1 = row.insertCell(0);
    
    cell1.innerHTML = "<tr><td style='text-align:left'><input type='checkbox' name='box1' ></td><td><input type='text' name='input1' ></td> <input type='image' src='/images/cross.png' value='remove' onclick='deleteRow(this)' width='20' height='20'/></tr>";
   cell1.scrollIntoView(false);
}
function deleteRow(src)
{
  
    var oRow = src.parentElement.parentElement;   
    document.all("myTable").deleteRow(oRow.rowIndex);  
}
 var count = 4;

 
 function myFunction1() {
	
	
    var table = document.getElementById("myTable1");
    var row = table.insertRow(-1);
   
  
	
    row.innerHTML = '<tr><td style="text-align: center;" ><input  type="checkbox" name="box1" value="">&nbsp;&nbsp;Attempt' + " "+ count +'</td><td style="text-align: center;" ><input type="text" id="datepicker4"></span></td><td style="text-align: center;" ><input type="text" name="timepicker" class="timepicker2"/></td><td style="text-align: center;" ><button class=" btn btn-secondary rounded-0">Set All Time High Priority</button></td><input type="image" src="/images/cross.png" style="padding-left:5px" value="remove" onclick="deleteRow1(this)" width="25" height="20"/></tr>';
   row.scrollIntoView(true);
   count++;
  
  
}
function deleteRow1(src)
{
  
    var oRow1 = src.parentElement.parentElement;   
    document.all("myTable1").deleteRow(-1);  
}

