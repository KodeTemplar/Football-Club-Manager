const token = $("#jwToken").val();

$(document).ready(function () {
    $("#registerUser").on("click", createTeamMember);
});

function createTeamMember() {
    if (!validateForm.required("#firstname")) return (alert("Please add First Name, it is required", "error"));
    if (!validateForm.required("#lastname")) return (alert("Please add Last Name, it is required", "error"));
    if (!validateForm.required("#category")) return (alert("Please select Category, it is required", "error"));
    if (!validateForm.required("#nationality")) return (alert("Please add Nationality, it is required", "error"));
    if (!validateForm.required("#position")) return (alert("Please add Position, it is required", "error"));
    if (!validateForm.required("#emailAddress")) return (alert("Please add Email, it is required", "error"));
    if (!validateForm.required("#password")) return (alert("Please add Password, it is required", "error"));
    if (!validateForm.required("#teamId")) return (alert("Please select a Team, it is required", "error"));

    $("#registerUser").html('Please wait...');
    const param = getUserDetail();
    const url = `/api/Register`;
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(param),
        contentType: "application/json",
        headers: {
            'Authorization': `Bearer ${token}`,
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS'
        },
        success: function (data) {
            if (data.succeeded) {
                alert("Team Member was created successfully");
                return location.href = "Login"
            }
            window.location.reload();
        },
        error: function (err) {
            $("#registerUser").html('Register');
            alert(err.responseJSON.Message);
        }
    })
}

//GET TEAM MODEL
function getUserDetail() {
    return {
        FirstName: $("#firstname").val(),
        LastName: $("#lastname").val(),
        Height: parseFloat($("#height").val()),
        Weight: parseFloat($("#weight").val()),
        Nationality: $("#nationality").val(),
        Position: $("#position").val(),
        TeamId: $("#teamId").val(),
        GenderId: parseInt($("#gender").val()),
        PlayerNumber: parseInt($("#number").val()),
        DateOfBirth: $("#dateofbirth").val(),
        MemberCategoryId: parseInt($("#category").val()),
        DateOfSigning: $("#dateofsigning").val(),
        CreatedBy: $("#admin").val(),
        Email: $("#emailAddress").val(),
        Password: $("#password").val(),
        TeamId: $("#teamId").val(),
        Image: $('#photo').prop('src') == location.origin + "/img/default_img.png" ? null : $('#photo').prop('src')
    };
}