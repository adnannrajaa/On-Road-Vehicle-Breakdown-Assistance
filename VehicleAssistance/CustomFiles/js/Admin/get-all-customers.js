let getUserData = (roleid) => {
    $.ajax({
        url: '/Admin/GetAllUsers',
        type: 'Get',
        data: { 'roleId': roleid },
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            loadData(response.result.Users)
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

let renderRow = (row,index) => {
    const {FirstName, LastName, UserName,PhoneNumber,CarType} = row;
    let dataRow = $("#dataRow").clone();
    dataRow.removeAttr('hidden')
    dataRow.removeAttr('id')
    dataRow.find('#No').text(index+1)
    dataRow.find('#firstName').text(FirstName)    
    dataRow.find('#lastName').text(LastName)
    dataRow.find('#userName').text(UserName)   
    
    dataRow.find('#mobile').html(PhoneNumber)
    dataRow.find('#carType').html(CarType)    

    $("#tbody").append(dataRow)
}

const customers = 2

$(() => {
    getUserData(customers)
    
})