$('#btnSave').off(defaultValue.Click).on(defaultValue.Click, function (e) {
    e.preventDefault();

    defaultFunction.SaveData('Product/CreateItem', defaultValue.Post,
        {
            keyid: 0,
            colorhex: 0,
            idunit: 0,
            createddate: defaultValue.Now(),
            createdby: 0,
            status: 1,
            isdefault: 1
        });
});

//function SaveData(url, data) {
//    $.ajax({
//        url: url,
//        data: { data },
//        type: defaultValue.Json_Type,
//        contentType: defaultValue.Json_ContentType,
//        success: function (result) {
//            console.log(result);
//        }
//    });
//}

function ClearData() {

}

function ValidData() {

}