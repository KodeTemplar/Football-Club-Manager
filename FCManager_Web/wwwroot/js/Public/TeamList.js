let storedPage = parseInt(localStorage.getItem("storedPage"));
let storedPagesize = parseInt(localStorage.getItem("storedPageSize"));
let storedPageCount = parseInt(localStorage.getItem("storedPageCount"));

$(document).ready(function () {
    let teamName = localStorage.getItem("teamName");
    $("#teamName").text(teamName);
    let page = 1;
    const pageSize = 10;
    LoadTeamMembers(pageSize, page);

    let $win = $(window);

    $win.scroll(function () {
        if ($win.scrollTop() == 0) {
            page -= 1;
            let storedPageCount = parseInt(localStorage.getItem("storedPageCount"));
            if (page <= storedPageCount) {
                LoadTeamMembers(pageSize, page);
            }
        }
        else if ($win.height() + $win.scrollTop() == $(document).height()) {
            page += 1;
            let storedPageCount = parseInt(localStorage.getItem("storedPageCount"));
            if (page <= storedPageCount) {
                LoadTeamMembers(pageSize, page);
            }
        }
    });
});

function LoadTeamMembers(pageSize, page) {
    localStorage.setItem("storedPage", page);
    localStorage.setItem("storedPageSize", pageSize);
    let teamId = localStorage.getItem("teamId");
    $.ajax({
        url: `/api/ViewTeamMemberList/${page}/${pageSize}/${teamId}`,
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        headers: {
            "Access-Control-Allow-Origin": "*"
        },
        success: function (data) {
            if (data.succeeded) {
                let response = data.data.responseData;
                localStorage.setItem("storedPageCount", data.data.pages);
                let mockup = "";
                let tag = "";

                if (response.length < 1) {
                    tag = "<h2>No team list found.</h2>"
                    mockup += tag;
                }
                else {
                    for (const element of response) {
                        let currentData = element;
                        tag = `
                                <div class="col-sm-3 mb-2">
                                    <div class="card">
                                            <img class="card-img-top" src="../../img/default_img.png" alt="Card image cap" id="${currentData.teamMemberId}" >
                                        <div class="card-body">
                                            <h1 class="card-title" style="font-size: 20px !important">${currentData.firstName} ${currentData.lastName}</h1>
                                            <a class="btn btn-primary" onClick="viewTeamMemberDetail('${currentData.teamMemberId}');">Details</a>
                                        </div>
                                    </div>
                                </div>
                              `;
                        GetImage(currentData.teamMemberId, requestImageType.TeamMemberImage, `#${currentData.teamMemberId}`)
                        mockup += tag;
                    }
                }
                $("#teamMembers").empty();
                $("#teamMembers").append(mockup);
            }
            $("#pageLoader").addClass("d-none");
        },
        error: function (errors) {
        }
    })
}

function viewTeamMemberDetail(id) {
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
            }
            $("#staticBackdrop").modal("show");
            GetImage(response.teamMemberId, requestImageType.TeamMemberImage, `#photo`)
        },
        error: function (errors) {
            alert(errors.responseJSON.Message);
        }
    })
}