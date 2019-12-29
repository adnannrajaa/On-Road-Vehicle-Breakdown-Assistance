var data = []
var filterData = []

let getMechanicsData = () => {
    $.ajax({
        url: '/Customer/GetAvailableMechanics',
        type: 'Get',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            data = response.result.Customer
            loadData(data)
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(errorThrown);
        }

    });
}

let clearTable = () => {
    $("#tableData tbody").empty()
}

let loadData = (arg_data) => {
    clearTable()
    arg_data.map((row,index) => {
        renderRow(row,index)
    })
}
let enableDisable=(userId)=>{
    $.ajax({
        type: "POST",
        url: "/Admin/EnableDisable",
        data: { 'id': userId },
        success: function (response) {
            if(response){ $.notify("Account blocked successfully", "warn");}
            else{ $.notify("Account Unblocked successfully", "success");}
            getUserData(mechanics)
        }
    })
}

let renderRow = (row,index) => {

    const {FirstName, LastName,PhoneNumber,Location,MechanicId} = row;
    let dataRow = $("#dataRow").clone();
    dataRow.removeAttr('hidden')
    dataRow.removeAttr('id')
    dataRow.find('#No').text(index+1)
    dataRow.find('#firstName').text(FirstName)    
    dataRow.find('#lastName').text(LastName)
    dataRow.find('#mobile').text(PhoneNumber)  
    dataRow.find('#Location').text(Location)
    dataRow.find('#send-request').attr('onclick',`sendRequest(${MechanicId})`)
    $("#tbody").append(dataRow)
}

let sendRequest = (mechanicId) =>{
    $.ajax({
        type: "POST",
        url: "/Customer/SendRequestToMechanic",
        data: { 'mechanicId': mechanicId },
        success: function (response) {
            if(response.result==true){$.notify("Request send successfully","success")}
            else{$.notify("Request not send!","error")}
        }
    })
}
let handleTextBoxChange = () => {
    $("#search-mechanic").on('keyup', (e) => {
        applyFilter($("#search-mechanic").val());
    })
   
}
let applyFilter = (mechanciLocation) => {
    filterData = data.filter(x => (mechanciLocation == '' || x.Location.toLowerCase().includes(mechanciLocation.toLowerCase())))
    loadData(filterData)
}

$(() => {
    getMechanicsData()
    handleTextBoxChange()
    
})