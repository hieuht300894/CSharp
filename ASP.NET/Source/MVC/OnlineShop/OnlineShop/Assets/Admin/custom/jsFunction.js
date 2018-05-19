var defaultFunction = {
    SaveData: function (url, type, data) {
        $.ajax({
            url: url,
            data: data,
            type: type,
            dataType: defaultValue.Json_Type,
            contentType: defaultValue.Json_ContentType,
            success: function (result) {
                console.log(result);
            }
        });
    },
    GetData: function (url, type, data) {
        var item = {};
        $.ajax({
            url: url,
            data: data,
            type: type,
            dataType: defaultValue.Json_Type,
            contentType: defaultValue.Json_ContentType,
            async: false,
            success: function (result) {
                item = result;
            }
        });
        return item;
    }
};