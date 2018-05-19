var btnSave = document.getElementById('btnSave');
var btnAdd = document.querySelector('a[data-id=btnAdd]');
var btnDetail = document.querySelector('a[data-id=btnDetail]');
var btnEdit = document.querySelector('a[data-id=btnEdit]');
var btnDelete = document.querySelector('a[data-id=btnDelete]');
var txtCode = document.querySelector('input[id=Code]');
var txtName = document.querySelector('input[id=Name]');
var lokUnit = document.querySelector('select[id=IDUnit]');
var txtNote = document.querySelector('textarea[id=Note]');
var lbTitle = document.querySelector('h4[class=modal-title]');
var _iEntry = {};

if (btnSave) {
    $(btnSave).off(defaultValue.Click).on(defaultValue.Click, function (e) {
        e.preventDefault();
        if (_iEntry.KeyID > 0) {
            _iEntry.ModifiedDate = defaultValue.Now();
            _iEntry.ModifiedBy = 0;
            _iEntry.Status = defaultValue.Update;
        }
        else {
            _iEntry.Code = $(txtCode).val();
            _iEntry.CreatedDate = defaultValue.Now();
            _iEntry.CreatedBy = 0;
            _iEntry.Status = defaultValue.Insert;
        }
        _iEntry.Name = $(txtName).val();
        _iEntry.IDUnit = $(lokUnit).val();
        _iEntry.Note = $(txtNote).val();
        defaultFunction.SaveData('Product/CreateItem', defaultValue.Post, JSON.stringify(_iEntry));
    });
}

if (btnAdd) {
    $(btnAdd).off(defaultValue.Click).on(defaultValue.Click, function (e) {
        e.preventDefault();
        $(lbTitle).text('Thêm mới sản phẩm');

        _iEntry = defaultFunction.GetData('Product/InitItem', defaultValue.Get, {});
        if (_iEntry) {
            $(txtCode).val(_iEntry.Code);
            $(txtName).val(_iEntry.Name);
            $(lokUnit).val(_iEntry.IDUnit);
            $(txtNote).val(_iEntry.Note);
        }
    });
}

if (btnEdit) {
    $(btnEdit).off(defaultValue.Click).on(defaultValue.Click, function (e) {
        e.preventDefault();
        $(lbTitle).text('Cập nhật sản phẩm');

        var rowid = $(this).attr('data-row');
        var row = $('tr[data-row=' + rowid + ']')[0];
        var column = $(row).find('td[data-id]');
        var id = $(column).attr('data-id');
        _iEntry = defaultFunction.GetData('Product/GetItem', defaultValue.Get, { id: id });
        if (_iEntry) {
            $(txtCode).val(_iEntry.Code);
            $(txtName).val(_iEntry.Name);
            $(lokUnit).val(_iEntry.IDUnit);
            $(txtNote).val(_iEntry.Note);
        }
    });
}

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