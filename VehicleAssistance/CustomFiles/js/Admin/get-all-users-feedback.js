$(() => {
    getUserData()

})

let getUserData = () => {
    $.ajax({
        url: '/Admin/ShowUsersFeedback',
        type: 'Get',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            loadData(response.result.feedback)
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(errorThrown);
        }

    });
}
let loadData = (arg_data) => {
    arg_data.map((row,index) => {
        renderRow(row,index)
    })
}

let renderRow = (row,index) => {
    const {UserRoleId,FullName,Feedbackdata} = row;
    if(UserRoleId==2){renderCustomerRow(FullName,Feedbackdata)}
    else if(UserRoleId==3){renderMechanicRow(FullName,Feedbackdata)}
}

let renderCustomerRow =(FullName,Feedbackdata)=>{
    let dataRow = $("#dataRow").clone();
    dataRow.removeAttr('hidden')
    dataRow.removeAttr('id')
    dataRow.find('#fullname').text(FullName)    
    dataRow.find('#feedback').text(Feedbackdata)

    $("#tabs-icons-text-1").append(dataRow)
}
let renderMechanicRow =(FullName,Feedbackdata)=>{
    let dataRow = $("#dataRow1").clone();
    dataRow.removeAttr('hidden')
    dataRow.removeAttr('id')
    dataRow.find('#fullnameM').text(FullName)    
    dataRow.find('#feedbackM').text(Feedbackdata)

    $("#tabs-icons-text-2").append(dataRow)
    
}