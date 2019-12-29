let getMechanicsData = () => {
    $.ajax({
        url: '/Customer/GetRequestedMechanics',
        type: 'Get',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            loadData(response.result.Customer)
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

    const {FirstName, LastName,PhoneNumber,Location,RequestId} = row;
    let dataRow = $("#dataRow").clone();
    dataRow.removeAttr('hidden')
    dataRow.removeAttr('id')
    dataRow.find('#No').text(index+1)
    dataRow.find('#firstName').text(FirstName)    
    dataRow.find('#lastName').text(LastName)
    dataRow.find('#mobile').text(PhoneNumber)  
    dataRow.find('#Location').text(Location)
    dataRow.find('#feedback').attr('onclick',`sendFeedback(${RequestId})`)

    $("#tbody").append(dataRow)
}

let sendFeedback = (RequestId) =>{
    $("#saveFeedBackButton").attr('onclick',`saveFeedbackDetail(${RequestId})`)
    $("#myModal").modal('show')
}

let saveFeedbackDetail =(RequestId)=>{
    if(isFormValid() == true)
    {
        let feeback = $("#feedback-detail").val()
        $.ajax({
            type: "POST",
            url: "/Customer/CustomerFeedback",
            data: { 'RequestId': RequestId , 'feeback':feeback},
            success: function (response) {
                if (response.result) {
                    $("#feedback-detail").val("")
                    $("#myModal").modal('hide')
                    $.notify("Thanks for your valuable feedback!", 'success')
                    
                }
            }
        });
    }
}
let isFormValid = () =>{
    let feeback = $("#feedback-detail").val()
    if(feeback == "")
    {
        $("#showError").removeAttr("hidden")
        setTimeout( function(){ 
            $('#showError').attr('hidden',"hidden"); 
        }, 1000 ); 
        return false;
    }
    return true;
}

$(() => {
    getMechanicsData()
   
})