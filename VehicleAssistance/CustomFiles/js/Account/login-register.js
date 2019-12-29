let showRegisterForm =()=>{
    $('.loginBox').fadeOut('fast',function(){
        $('.registerBox').fadeIn('fast');
        $('.login-footer').fadeOut('fast',function(){
            $('.register-footer').fadeIn('fast');
        });
        $('.modal-title').html('Register');
    }); 
    $('.error').removeClass('alert alert-danger').html('');
    resetDatafeilds()
       
}
let showLoginForm=()=>{
    $('#loginModal .registerBox').fadeOut('fast',function(){
        $('.loginBox').fadeIn('fast');
        $('.register-footer').fadeOut('fast',function(){
            $('.login-footer').fadeIn('fast');    
        });
        
        $('.modal-title').html('Login');
    });       
     $('.error').removeClass('alert alert-danger').html(''); 
}

let openLoginModal=()=>{
    showLoginForm();
    setTimeout(function(){
        $('#loginModal').modal('show');    
    }, 230);
    
}
let openRegisterModal=()=>{
    showRegisterForm();
    setTimeout(function(){
        $('#loginModal').modal('show');    
    }, 230);
    
}

let loginAjax = () =>{
    let userName = $("#userName").val();
    let password = $("#password").val();
    if (isFormValid(userName, password) == true)
    {
        $.ajax({
            type: "POST",
            url: "/Account/Login",
            data: { "userName": userName, "password": password },
            success: function (response) {
                performActionAccordingToResponse(response)
            }
        });

    }
   


}
let performActionAccordingToResponse = (response) => {
    switch (response.result) {
        case 1:
            showPageAccordingToRole(response.UserRole)
            break
        case 2:
            shakeModal("Your account is blocked by admin", '.error')
            break
        case 3:
            shakeModal("Invalid username/password combination", '.error')
            break
    }
}
let isFormValid = (userName, password) => {
    if (userName == "")
    { shakeModal("username is required", '.error'); return false }
    else if (password == "") { shakeModal("password is required", '.error'); return false }
   return true
}

let showPageAccordingToRole = (role) => {
    switch(role)
    {
        case 'Admin':
            window.location = "/Admin/Index"
            break
        case 'Customer':
            window.location = "/Customer/Index"
            break
        case 'Mechanic':
            window.location = "/Mechanic/Index"
            break
    }
}
let shakeModal=(msg,selector)=>{
    $('#loginModal .modal-dialog').addClass('shake');
    $(selector).addClass('alert alert-danger').html(msg);
             setTimeout( function(){ 
                $('#loginModal .modal-dialog').removeClass('shake'); 
    }, 1000 ); 
}
let successMessage=(msg,selector)=>{
    $(selector).addClass('alert alert-success').html(msg);
    setTimeout( function(){ 
        $(selector).removeClass('alert alert-success').html("");
    }, 3000 ); 
}

let registerUser = () => {
    if (isFormValidated() == true) {
       
        if (IsUserExist() == false)
        {
            if (phone_validate() == true)
            {
                let param = {
                    User: {
                        RoleId: $("#userRole option:selected").val(),
                        FirstName: $("#firstName").val(),
                        LastName: $("#lastName").val(),
                        UserName: $("#userNameR").val(),
                        PhoneNumber: $("#phoneNo").val(),
                        Password: $("#passwordR").val()
                    },
                    CustomerDetail: {
                        carType: $("#carType").val()
                    },
                    MechanicsDetail: {
                        location: $("#location").val()

                    }
                   
                }
            
                $.ajax({
                    type: "POST",
                    url: "/Account/registerUser",
                    data: { '_model': param },
                    success: function (response) {
                        if (response.result) {
                            resetDatafeilds();
                            successMessage("Your Account Created Successfully!", '.errorRegister')
                        }
                    }
                });
            }
          
        }
        else { shakeModal("User name already exist", '.errorRegister') }
       
    }

}
let resetDatafeilds = () => {
    $("#userRole").val("0")
    $("#firstName").val("")
    $("#lastName").val("")
    $("#userNameR").val("")
    $("#passwordR").val("")
    $("#password_confirmation").val("")
    $(".errorRegister").removeClass('alert alert-danger').html("");
    $("#phoneNo").val("")
    $("#carType").val("")
    $("#location").val("")
    showRelatedFeilds()
}
let isFormValidated = () => {
    let RoleId = $("#userRole option:selected").val()
    let firstName = $("#firstName").val()
    let lastName = $("#lastName").val()
    let userName = $("#userNameR").val()
    let mobile = $("#phoneNo").val()
    let password = $("#passwordR").val()
    let confirmPassword = $("#password_confirmation").val()
    let carType = $("#carType").val()
    let location = $("#location").val()
    if (firstName == "") { shakeModal("First name is required", '.errorRegister'); return false }
    else if (lastName == "") { shakeModal("Last name is required", '.errorRegister'); return false }
    else if (mobile == "") { shakeModal("Mobile no is required", '.errorRegister'); return false }
    else if (userName == "") { shakeModal("User name is required", '.errorRegister'); return false }
    else if (password == "") { shakeModal("Password is required", '.errorRegister'); return false }
    else if (confirmPassword == "") { shakeModal("Confirm password is required", '.errorRegister'); return false }
    else if (confirmPassword != password) { shakeModal("Password not match", '.errorRegister'); return false }
    else if (RoleId == 0) { shakeModal("Please select role", '.errorRegister'); return false }
    else if (RoleId == 2 && carType == "") { shakeModal("Car type is required", '.errorRegister'); return false }
    else if (RoleId == 3 && location == "") { shakeModal("Location is required", '.errorRegister'); return false }
    return true

  
}
let phone_validate = () =>{
    let mobile = $("#phoneNo").val()
    var regexPattern = new RegExp(/^\d{4}[- ]?\d{7}$/);    // regular expression pattern
    let isValid = regexPattern.test(mobile);
    if(!isValid){
        shakeModal("Mobile no is in invalid format", '.errorRegister')
        setTimeout(function () {
            $('.errorRegister').removeClass('alert alert-danger').html("");
        }, 2000);
        return false;
    }
    return true;
}
let IsUserExist = () => {
        $.ajax({
            type: "POST",
            url: "/Account/isUserExist",
            data: { 'UserName': $("#userNameR").val() },
            success: function (response) {
                if (response.result) {
                    shakeModal("User name already exist", '.errorRegister')
                    return true;
                }
            }
        });
        return false;
    }


let showRelatedFeilds = () => {
    let RoleId = $("#userRole option:selected").val()
    if (RoleId == 2) { $("#location").addClass('hide'); $("#carType").removeClass('hide') }
    else if (RoleId == 3) { $("#carType").addClass('hide'); $("#location").removeClass('hide') }
    else {
        $("#carType").addClass('hide')
        $("#location").addClass('hide')
    }
}

$(() => {
    $("#carType").addClass('hide')
    $("#location").addClass('hide')
})