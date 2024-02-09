const token = $("#jwToken").val();

$(document).ready(function () {
    $("#updateTeam").hide();
    $("#players").hide();
    $(document).on("click", ".viewButton", function (ev) {
        const id = $(this).attr("id");
        viewTeam(id, "view");
    });

    $(document).on("click", ".editButton", function (ev) {
        const id = $(this).attr("id");
        localStorage.setItem("teamIdForEdit", id);
        viewTeam(id, "edit");
    });

    $(document).on("click", ".deleteButton", function (ev) {
        const id = $(this).attr("id");
        deleteTeam(id);
    });

    $("#createTeam").on("click", createTeam);
    $("#updateTeam").on("click", updateTeamFunction);

    $("#closeModal").on("click", function () {
        $('form :input').val('').removeAttr("disabled");
        $("#updateTeam").hide();
        $("#players").hide();
        $("#createTeam").show();
        $("#photo").attr('src', "/img/Team-Logo-Default.jpg.png");
    });
});

function createTeam() {
    if (!validateForm.required("#name")) return (alert("Please add name, it is required", "error"));
    if (!validateForm.required("#country")) return (alert("Please add Country, it is required", "error"));
    if (!validateForm.required("#stadium")) return (alert("Please add Stadium, it is required", "error"));

    $("#createTeam").html('Please wait...');
    const param = getCreateTeamDetail();
    $.ajax({
        url: `/api/CreateTeam`,
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
                alert("Team was created successfully");
                window.location.reload();
            }
            window.location.reload();
        },
        error: function (err) {
            $("#createTeam").html('Create');
            alert(err.responseJSON.Message);
        }
    })
}

function updateTeamFunction(id) {
    $("#updateTeam").html('Please wait...');
    const param = getCreateTeamDetail();
    param.TeamId = localStorage.getItem("teamIdForEdit", id);;
    $.ajax({
        url: `api/UpdateTeam`,
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
                $("#updateTeam").html('Update');
                alert("Team was updated successfully");
                window.location.reload();
            }
            window.location.reload();
        },
        error: function (err) {
            $("#updateTeam").html('Update');
            alert(err.responseJSON.Message);
        }
    })
}

function deleteTeam(id) {
    $(".deleteButton").html('Deleting...');
    $.ajax({
        url: `/api/DeleteTeam/${id}`,
        type: "DELETE",
        dataType: "json",
        contentType: "application/json",
        headers: {
            'Authorization': `Bearer ${token}`,
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS'
        },
        success: function (data) {
            if (data.succeeded) {
                window.location.reload();
            }
            window.location.reload();
        },
        error: function (errors) {
            $(".deleteButton").html('Delete');
            alert(errors.responseJSON.Message);
            window.location.reload();
        }
    })
}

function viewTeam(id, type) {
    $.ajax({
        url: `/Api/ViewTeam/${id}`,
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        headers: {
            'Authorization': `Bearer ${token}`,
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS'
        },
        success: function (data) {
            let response = data.data;
            if (data.succeeded) {
                if (type == "view") {
                    $("#name").val(response.name).attr("disabled", "disabled");
                    $("#country").val(response.country).attr("disabled", "disabled");
                    $("#stadium").val(response.stadium).attr("disabled", "disabled");
                    $("#url").val(response.homePageURL).attr("disabled", "disabled");
                    $("#createTeam").hide();
                    $("#updateTeam").hide();
                    $("#file-input").hide();
                    $("#players").show();
                }
                else {
                    $("#name").val(response.name).removeAttr("disabled");
                    $("#country").val(response.country).removeAttr("disabled");
                    $("#stadium").val(response.stadium).removeAttr("disabled");
                    $("#url").val(response.homePageURL).removeAttr("disabled");
                    $("#updateTeam").show();
                    $("#createTeam").hide();
                    $("#players").hide();
                }
            }
            $("#staticBackdrop").modal("show");
            GetImage(response.teamId, requestImageType.Logo, `#photo`)
        },
        error: function (errors) {
            alert(errors.responseJSON.Message);
        }
    })
}

//GET TEAM MODEL
function getCreateTeamDetail() {
    return {
        Name: $("#name").val(),
        Country: $("#country").val(),
        Stadium: $("#stadium").val(),
        HomePageURL: $("#url").val() == null ? null : $("#url").val(),
        Image: $('#photo').prop('src') == location.origin+"/img/Team-Logo-Default.jpg" ? null : $('#photo').prop('src')
    };
}