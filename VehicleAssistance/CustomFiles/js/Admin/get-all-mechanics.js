let getUserData = (roleid) => {
    $.ajax({
        url: '/Admin/GetAllMechanicsUsers',
        type: 'Get',
        data: { 'roleId': roleid },
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
             loadData(response.result.Mechanics)
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
    let Getuser="Doctor"
    const {FullName, UserName,PhoneNumber,Location,IsBlock,UserId} = row;
    let dataRow = $("#dataRow").clone();
    dataRow.removeAttr('hidden')
    dataRow.removeAttr('id')
    dataRow.find('#No').text(index+1)
    dataRow.find('#fullName').text(FullName)    
    dataRow.find('#userName').text(UserName)
    dataRow.find('#mobile').text(PhoneNumber)  
    dataRow.find('#Location').text(Location)
    if(IsBlock==true){
        dataRow.find('#block').attr('hidden',"")
        dataRow.find('#active').attr('onclick',`enableDisable(${UserId})`)
    }
    else{
        dataRow.find('#active').attr('hidden',"")
        dataRow.find('#block').attr('onclick',`enableDisable(${UserId})`)
    }
    
    $("#tbody").append(dataRow)
}

const mechanics = 3

$(() => {
    getUserData(mechanics)
    
})