let storedPage = parseInt(localStorage.getItem("storedPage"));
let storedPagesize = parseInt(localStorage.getItem("storedPageSize"));
let storedPageCount = parseInt(localStorage.getItem("storedPageCount"));

$(document).ready(function () {
    let page = 1;
    const pageSize = 10;
    LoadTeams(pageSize, page);

    let $win = $(window);

    $win.scroll(function () {
        if ($win.scrollTop() == 0) {
            page -= 1;
            let storedPageCount = parseInt(localStorage.getItem("storedPageCount"));
            if (page <= storedPageCount) {
                LoadTeams(pageSize, page);
            }
        }
        else if ($win.height() + $win.scrollTop() == $(document).height()) {
            page += 1;
            let storedPageCount = parseInt(localStorage.getItem("storedPageCount"));
            if (page <= storedPageCount) {
                LoadTeams(pageSize, page);
            }
        }
    });
});

function LoadTeams(pageSize, page) {
    localStorage.setItem("storedPage", page);
    localStorage.setItem("storedPageSize", pageSize);
    $.ajax({
        url: `api/ViewTeamList/${page}/${pageSize}`,
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
                let mockup = ""
                for (const element of response) {
                    let currentData = element;
                    let tag = `
                                <div class="col-sm-3 mb-2">
                                    <div class="card">
                                            <img class="card-img-top" src="/img/Team-Logo-Default.jpg" alt="Card image cap"  id="${currentData.teamId}">
                                        <div class="card-body">
                                            <h5 class="card-title" style="font-size: 13px !important">${currentData.name}</h5>
                                            <div class="row">
                                                 <div class="col-6">
                                                    <a class="btn btn-info" style="color: white !important;" onClick="viewTeam('${currentData.teamId}')">Details</a>
                                                 </div>
                                                 <div class="col-6">
                                                    <a class="btn btn-primary" onClick="RedirectToPlayer('${currentData.teamId}', '${currentData.name}')">Players</a>
                                                 </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                              `;
                    GetImage(currentData.teamId, requestImageType.Logo, `#${currentData.teamId}`)
                    mockup += tag;
                }
                $("#teams").empty();
                $("#teams").append(mockup);
            }
            $("#pageLoader").addClass("d-none");
        },
        error: function (errors) {
        }
    })
}

function viewTeam(id) {
    $.ajax({
        url: `/Api/ViewTeam/${id}`,
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        headers: {
            "Access-Control-Allow-Origin": "*"
        },
        success: function (data) {
            let response = data.data;
            if (data.succeeded) {
                    $("#name").val(response.name).attr("disabled", "disabled");
                    $("#country").val(response.country).attr("disabled", "disabled");
                    $("#stadium").val(response.stadium).attr("disabled", "disabled");
                    $("#url").val(response.homePageURL).attr("disabled", "disabled");
            }
            $("#staticBackdropLabel").text(`${response.name}`);
            $("#staticBackdrop").modal("show");
            GetImage(response.teamId, requestImageType.Logo, `#photo`)
        },
        error: function (errors) {
            alert(errors.responseJSON.Message);
        }
    })
}