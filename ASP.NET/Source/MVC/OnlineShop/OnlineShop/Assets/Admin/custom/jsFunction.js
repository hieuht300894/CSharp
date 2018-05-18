var defaultFunction = {
    SaveData: function (url, type, data) {
        console.log(url);
        console.log(type);
        console.log(data);
        console.log(JSON.stringify(data));
        $.ajax({
            url: url,
            data: JSON.stringify(data),
            type: type,
            dataType: defaultValue.Json_Type,
            contentType: defaultValue.Json_ContentType,
            success: function (result) {
                console.log(result);
            }
        });
    }
}