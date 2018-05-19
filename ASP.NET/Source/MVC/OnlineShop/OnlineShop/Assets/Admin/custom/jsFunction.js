function JsonResult() {
    return {
        StatusCode: 0,
        Data: {},
        Message: ''
    };
};

var defaultFunction = {
    Now: function () {
        return new Date();
    },
    ParseDateTime: function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return new Date(+a[1]);
            }
        }
        return value;
    },
    SaveData: async function (url, type, data) {
        var jsonResult = new JsonResult();
        await $.ajax({
            url: url,
            data: data,
            type: type,
            dataType: defaultValue.Json_Type,
            contentType: defaultValue.Json_ContentType,
            success: function () {
                jsonResult.StatusCode = defaultValue.OK;
            },
            error: function () {
                jsonResult.StatusCode = defaultValue.BadRequest;
            },
            complete: function (result) {
                jsonResult.Data = result.responseJSON;
            }
        });
        console.log('jsonResult');
        console.log(jsonResult);
        return jsonResult;
    },
    GetData: async function (url, type, data) {
        var jsonResult = new JsonResult();
        await $.ajax({
            url: url,
            data: data,
            type: type,
            dataType: defaultValue.Json_Type,
            contentType: defaultValue.Json_ContentType,
            success: function () {
                jsonResult.StatusCode = defaultValue.OK;
            },
            error: function () {
                jsonResult.StatusCode = defaultValue.BadRequest;
            },
            complete: function (result) {
                jsonResult.Data = result.responseJSON;
            }
        });
        console.log('jsonResult');
        console.log(jsonResult);
        return jsonResult;
    }
};