var Functions = {
    GenerateGuid: function () {
        // https://jsfiddle.net/briguy37/2MVFd/
        var d = new Date().getTime();
        var Guid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
        return Guid;
    },
    IsJsonString: function (str) {
        try {
            JSON.parse(str);
        } catch (e) {
            return false;
        }
        return true;
    },
    IsGuid: function (strGuid) {
        // Check if the value is a string
        if (typeof strGuid !== "string") {
            return false;
        }

        // Check if the value has the correct format
        var regex = /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/i;
        return regex.test(strGuid);

        // Check if the value is not an empty guid
        if (result == true && strGuid != "00000000-0000-0000-0000-000000000000") {
            return true;
        }
        return false;
    },
    IsGuidEmpty: function (strGuid) {
        debugger
        // Check if the value is a guid
        if (this.IsGuid(strGuid) == true) {

            // Check if the value is an empty guid
            if (strGuid == "00000000-0000-0000-0000-000000000000") {
                return true;
            }
        }
        return false;
    },
    HandleAjaxResponse: function (data) {
        debugger;
        console.log(data);
        var IsSuccess = false;
        var result = null;
        try {
            result = JSON.parse(data);
        } catch (e) {
            console.log(e);
            toastr.error('Something went wrong, please contact administrator.');
        }

        if (result != null) {
            if (result.IsSuccess == true) {
                IsSuccess = true;
                if (result.Message != null && result.Message != "") {
                    toastr.success(result.Message);
                }
                if (result.IsRedirectionRequired == true) {
                    setTimeout(function () { location.href = result.RedirectUrl; }, 4000);
                }
            }
            else {
                debugger
                if (this.IsJsonString(result.ValidationErrors)) {
                    var errorResult = JSON.parse(result.ValidationErrors);
                    var errorText = "<b>" + result.Message + errorResult.message + "</b>";
                    jQuery.each(errorResult.errors, function (i, val) {
                        errorText = errorText + "<br/><br/>" + "* " + val;
                    });
                    toastr.error(errorText);
                }
                else {
                    var errorText = "";
                    if (result.Message != null && result.Message != "") {
                        errorText = "<b>" + result.Message + "</b><br/><br/>";
                    }
                    errorText = errorText + result.ValidationErrors;
                    toastr.error(errorText);
                }

            }
        }

        return IsSuccess;
    }

}

$.fn.showModal = function () {
    $(this).modal({ backdrop: 'static', keyboard: false, show: true });
};

$.fn.hideModal = function () {
    $(this).modal('hide');
};