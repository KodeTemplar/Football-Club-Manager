const token = $("#jwToken").val();

$(document).ready(function () {
    $("#updateMember").hide();
    $(document).on("click", ".viewMemberBtn", function (ev) {
        const id = $(this).attr("id");
        viewTeamMember(id, "view");
    });

    $(document).on("click", ".editMemberButton", function (ev) {
        const id = $(this).attr("id");
        localStorage.setItem("teamMemberIdForEdit", id);
        viewTeamMember(id, "edit");
    });

    $(document).on("click", ".deleteMemberButton", function (ev) {
        const id = $(this).attr("id");
        deleteTeamMember(id);
    });

    $("#createMember").on("click", createTeamMember);
    $("#updateMember").on("click", updateTeamMemberFunction);

    $("#closeModal").on("click", function () {
        $('form :input').val('').removeAttr("disabled");
        $("#updateTeam").hide();
        $("#players").hide();
        $("#createTeam").show();
        $("#photo").attr('src', "/img/Team-Logo-Default.jpg.png");
    });
});

function createTeamMember() {
    if (!validateForm.required("#firstname")) return (alert("Please add First Name, it is required", "error"));
    if (!validateForm.required("#lastname")) return (alert("Please add Last Name, it is required", "error"));
    if (!validateForm.required("#category")) return (alert("Please add Category, it is required", "error"));
    if (!validateForm.required("#nationality")) return (alert("Please add Nationality, it is required", "error"));
    if (!validateForm.required("#position")) return (alert("Please add Position, it is required", "error"));

    $("#createMember").html('Please wait...');
    const param = getTeamMemberDetail();
    const url = `/api/AddTeamMember`;
    debugger
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
                $("#staticBackdrop").modal("hide");
                alert("Team Member was created successfully");
                window.location.reload();
            }
            window.location.reload();
        },
        error: function (err) {
            $("#createMember").html('Create');
            alert(err.responseJSON.Message);
        }
    })
}

function updateTeamMemberFunction(id) {
    $("#updateMember").html('Please wait...');
    const param = getTeamMemberDetail();
    param.teamMemberId = param.TeamId = localStorage.getItem("teamMemberIdForEdit", id);;

    $.ajax({
        url: `/api/UpdateTeamMember`,
        type: "PUT",
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
                $("#staticBackdrop").modal("hide");
                alert(param.FirstName+ "'s record was updated successfully");
                window.location.reload();
            }
            window.location.reload();
        },
        error: function (err) {
            $("#updateMember").html('Update');
            alert(err.responseJSON.Message);
        }
    })
}

function deleteTeamMember(id) {
    $(".deleteMemberButton").html('Deleting...');
    $.ajax({
        url: `/api/DeleteTeamMember/${id}`,
        type: "DELETE",
        dataType: "json",
        contentType: "application/json",
        headers: {
            "Access-Control-Allow-Origin": "*"
        },
        success: function (data) {
            if (data.succeeded) {
                window.location.reload();
            }
            window.location.reload();
        },
        error: function (errors) {
            alert(errors.responseJSON.Message);
            window.location.reload();
        }
    })
}

function viewTeamMember(id, type) {
    $.ajax({
        url: `/Api/ViewTeamMember/${id}`,
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        headers: {
            "Access-Control-Allow-Origin": "*"
        },
        success: function (data) {
            let response = data.data;
            if (data.succeeded) {
                if (type == "view") {
                    $("#firstname").val(response.firstName).attr("disabled", "disabled");
                    $("#lastname").val(response.lastName).attr("disabled", "disabled");
                    $("#height").val(response.height).attr("disabled", "disabled");
                    $("#weight").val(response.weight).attr("disabled", "disabled");
                    $("#nationality").val(response.nationality).attr("disabled", "disabled");
                    $("#position").val(response.position).attr("disabled", "disabled");
                    $("#number").val(response.playerNumber).attr("disabled", "disabled");
                    $("#gender, #category").prop('disabled', 'disabled');
                    $(`#gender option[value=${response.genderId}]`).attr('selected', 'selected');
                    $(`#category option[value=${response.memberCategoryId}]`).attr('selected', 'selected');
                    $("#dateofsigning").val(ToDate(response.dateOfSigning)).attr("disabled", "disabled");
                    $("#dateofbirth").val(ToDate(response.dateOfBirth)).attr("disabled", "disabled");

                    $("#staticBackdropLabel").text("Team Member Details");
                    $("#createMember").hide();
                    $("#updateMember").hide();
                    $("#file-input").hide();
                }
                else {
                    $("#firstname").val(response.firstName).removeAttr("disabled");
                    $("#lastname").val(response.lastName).removeAttr("disabled");
                    $("#height").val(response.height).removeAttr("disabled");
                    $("#weight").val(response.weight).removeAttr("disabled");
                    $("#nationality").val(response.nationality).removeAttr("disabled");
                    $("#position").val(response.position).removeAttr("disabled");
                    $("#number").val(response.playerNumber).removeAttr("disabled");
                    $("#gender, #category").prop('disabled', false);
                    $(`#gender option[value=${response.genderId}]`).attr('selected', 'selected');
                    $(`#category option[value=${response.memberCategoryId}]`).attr('selected', 'selected');
                    $("#dateofsigning").val(ToDate(response.dateOfSigning)).removeAttr("disabled");
                    $("#dateofbirth").val(ToDate(response.dateOfBirth)).removeAttr("disabled");

                    $("#staticBackdropLabel").text("Edit Team Member Details");
                    $("#updateMember").show();
                    $("#createMember").hide();
                }
            }
            $("#staticBackdrop").modal("show");
            GetImage(response.teamMemberId, requestImageType.TeamMemberImage, `#photo`)
        },
        error: function (errors) {
            alert(errors.responseJSON.Message);
        }
    })
}

//GET TEAM MODEL
function getTeamMemberDetail() {
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
        Image: $('#photo').prop('src') == location.origin+"/img/default_img.png" ? null : $('#photo').prop('src')
    };
}