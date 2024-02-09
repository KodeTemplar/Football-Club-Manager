var requestImageType = {
    Logo: 1,
    TeamMemberImage: 2
};

function ToDate(dateString) {
    var dt = dateString.split('-');
    var dt1 = dateString.split('T');

    var date = dt1[0];

    return date
}

function GetImage(id, requestType, selector) {
    $.ajax({
        url: `/Api/GetImage/${requestType}/${id}`,
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        headers: {
            "Access-Control-Allow-Origin": "*"
        },
        success: function (data) {
            if (data.succeeded) {
                let response = data.data;

                if (response != null) {
                    if (requestType == 1) {
                        if (data.logo != null || data.logo != "") {
                            $(selector).attr('src', "" + data.data.logo + "");
                        }
                    }
                    else {
                        if (data.Image != null || data.Image != "") {
                            $(selector).attr('src', "" + data.data.image + "");
                        }
                    }
                }
                else {
                    if (requestType == requestImageType.Logo) {
                        $(selector).attr('src', "img/Team-Logo-Default.jpg");
                    }
                    else {
                        $(selector).attr('src', "img/default-img.png");

                    }
                }
            }
        },
        error: function (errors) {
            if (requestImageType == requestImageType.Logo) {
                $(selector).attr('src', "~/img/Team-Logo-Default.jpg");
            }
            else {
                $(selector).attr('src', "~/img/default-img.png");

            }
        }
    })
}

//READ IMAGE URL
function readURL(input) {
    if (input.files && input.files[0]) {

        var file_size = input.files[0].size;
        if (file_size > 2000000) {
            ALert("Image cannot be more than 2MB", "error");
            return false;
        }

        var reader = new FileReader();

        reader.onload = function (e) {
            $('#photo')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);

        return true;
    }
}

const validateForm = {
    required: (id) => {
        if ($(id).is("select")) {
            if ($(id).prop('multiple')) {
                if ($(id).val().length < 1) return false;

            } else if ($(`${id} option:selected`).val() <= 0 || $(`${id} option:selected`).val() == null || $(`${id} option:selected`).val() == "" || $(`${id} option:selected`).val() == '') {
                return false;
            }
            return true;
        }
        return $(id).val() == "" ? false : $(id).val() == null ? false : $(id).val() <= 0 ? false : true;
    },
    isEmail: (id) => {
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        return mailformat.test($(id).val()) == false ? false : true;
    },
    isNumber: (id) => {
        var numbers = /^[0-9]+$/;
        return numbers.test($(id).val()) == false ? false : true;
    },
    isSamePassword: (passwordId, confPasswordId) => {
        return $(passwordId).val() === $(confPasswordId).val() ? true : false;
    },
    isValidDate: (dateFieldId) => {
        return moment($(dateFieldId), 'yyyy-mm-d').isValid();
    },
    isRadioChecked: (id) => {
        return id.length == 0 ? false : true;
    },
    isValidPhoneNumber: (number) => {
        //if (typeof number == "undefined")
        //    return false;

        //if (number == null)
        //    return false;

        //return number.length == 11 ? true : false;
        return true;
    }
};