var btnSave = document.getElementById('btnSave');
var btnSaveAndNew = document.getElementById('btnSaveAndNew');
var btnAdd = document.querySelector('a[data-id=btnAdd]');
var btnDetail = document.querySelectorAll('a[data-id=btnDetail]');
var btnEdit = document.querySelectorAll('a[data-id=btnEdit]');
var btnDelete = document.querySelectorAll('a[data-id=btnDelete]');
var txtCode = document.querySelector('input[id=Code]');
var txtName = document.querySelector('input[id=Name]');
var txtSize = document.querySelector('input[id=Size]');
var lokUnit = document.querySelector('select[id=IDUnit]');
var txtNote = document.querySelector('textarea[id=Note]');
var lbTitle = document.querySelector('h4[class=modal-title]');
var groupCode = document.getElementById('group_code');
var groupName = document.getElementById('group_name');
var groupUnit = document.getElementById('group_unit');
var _iEntry = {};
var _aEntry = {};

if (btnSave) {
    $(btnSave).off(defaultValue.Click).on(defaultValue.Click, async function (e) {
        e.preventDefault();
        if (ValidData()) {
            var jsonResult;
            _aEntry = _iEntry;
            _aEntry.Name = $(txtName).val();
            _aEntry.IDUnit = $(lokUnit).val();
            _aEntry.Note = $(txtNote).val();
            _aEntry.Size = $(txtSize).val();
            if (_aEntry.KeyID > 0) {
                _aEntry.CreatedDate = defaultFunction.ParseDateTime(_aEntry.CreatedDate);
                _aEntry.ModifiedDate = defaultFunction.Now();
                _aEntry.ModifiedBy = 0;
                _aEntry.Status = defaultValue.Update;
                jsonResult = await defaultFunction.SaveData('Product/EditItem', defaultValue.Put, JSON.stringify({ OldData: _iEntry, NewData: _aEntry, Status: defaultValue.Update }));
            }
            else {
                _aEntry.Code = $(txtCode).val();
                _aEntry.CreatedDate = defaultFunction.Now();
                _aEntry.CreatedBy = 0;
                _aEntry.Status = defaultValue.Insert;
                jsonResult = await defaultFunction.SaveData('Product/CreateItem', defaultValue.Post, JSON.stringify({ OldData: _iEntry, NewData: _aEntry, Status: defaultValue.Insert }));
            }

            if (jsonResult.StatusCode == defaultValue.OK && jsonResult.Data) {
                _iEntry = jsonResult.Data;
                _aEntry = jsonResult.Data;
                $(lbTitle).text('Cập nhật sản phẩm');
            }
            else {
                //console.log(jsonResult.Message);
            }
        }
    });
}

if (btnSaveAndNew) {
    $(btnSaveAndNew).off(defaultValue.Click).on(defaultValue.Click, async function (e) {
        e.preventDefault();
        if (ValidData()) {
            _aEntry = _iEntry;
            _aEntry.Name = $(txtName).val();
            _aEntry.IDUnit = $(lokUnit).val();
            _aEntry.Note = $(txtNote).val();
            _aEntry.Size = $(txtSize).val();
            if (_aEntry.KeyID > 0) {
                _aEntry.CreatedDate = defaultFunction.ParseDateTime(_aEntry.CreatedDate);
                _aEntry.ModifiedDate = defaultFunction.Now();
                _aEntry.ModifiedBy = 0;
                _aEntry.Status = defaultValue.Update;
                jsonResult = await defaultFunction.SaveData('Product/EditItem', defaultValue.Put, JSON.stringify({ OldData: _iEntry, NewData: _aEntry, Status: defaultValue.Update }));
            }
            else {
                _aEntry.Code = $(txtCode).val();
                _aEntry.CreatedDate = defaultFunction.Now();
                _aEntry.CreatedBy = 0;
                _aEntry.Status = defaultValue.Insert;
                jsonResult = await defaultFunction.SaveData('Product/CreateItem', defaultValue.Post, JSON.stringify({ OldData: _iEntry, NewData: _aEntry, Status: defaultValue.Insert }));
            }

            if (jsonResult.StatusCode == defaultValue.OK && jsonResult.Data) {
                InsertItem();
            }
            else {
                //console.log(jsonResult.Message);
            }
        }
    });
}

if (btnAdd) {
    $(btnAdd).off(defaultValue.Click).on(defaultValue.Click, function (e) {
        e.preventDefault();
        InsertItem();
    });
}

if (btnEdit.length > 0) {
    for (var i = 0; i < btnEdit.length; i++) {
        $(btnEdit[i]).off(defaultValue.Click).on(defaultValue.Click, function (e) {
            e.preventDefault();
            UpdateItem(this);
        });
    }
}

async function InsertItem() {
    ClearFormat();
    $(lbTitle).text('Thêm mới sản phẩm');
    var jsonResult = await defaultFunction.GetData('Product/InitItem', defaultValue.Get, {});
    if (jsonResult.StatusCode == defaultValue.OK && jsonResult.Data) {
        _iEntry = jsonResult.Data;
        $(txtCode).removeAttr('disabled');
        $(txtCode).val(_iEntry.Code);
        $(txtName).val(_iEntry.Name);
        $(lokUnit).val(_iEntry.IDUnit);
        $(txtNote).val(_iEntry.Note);
        $(txtSize).val(_iEntry.Size);
    }
    else {
        //console.log(jsonResult.Message);
    }
}

async function UpdateItem(_btnEdit) {
    ClearFormat();
    $(lbTitle).text('Cập nhật sản phẩm');
    var rowid = $(_btnEdit).attr('data-row');
    var row = $('tr[data-row=' + rowid + ']')[0];
    var column = $(row).find('td[data-id]');
    var id = $(column).attr('data-id');
    var jsonResult = await defaultFunction.GetData('Product/GetItem', defaultValue.Get, { id: id });
    if (jsonResult.StatusCode == defaultValue.OK && jsonResult.Data) {
        _iEntry = jsonResult.Data;
        $(txtCode).attr('disabled', '');
        $(txtCode).val(_iEntry.Code);
        $(txtName).val(_iEntry.Name);
        $(lokUnit).val(_iEntry.IDUnit);
        $(txtNote).val(_iEntry.Note);
        $(txtSize).val(_iEntry.Size);
    }
    else {
        //console.log(jsonResult.Message);
    }
}

function ClearFormat() {
    $(groupCode).attr('class', 'form-group');
    $(groupName).attr('class', 'form-group');
    $(groupUnit).attr('class', 'form-group');
    $(txtCode).attr('data-original-title', '');
    $(txtName).attr('data-original-title', '');
    $(lokUnit).attr('data-original-title', '');
}

function ValidData() {
    var chk = true;
    ClearFormat();
    if (!$(txtCode).val()) {
        $(groupCode).attr('class', 'form-group has-error');
        $(txtCode).attr('data-original-title', 'Vui lòng nhập mã.');
        chk = false;
    }
    if (!$(txtName).val()) {
        $(groupName).attr('class', 'form-group has-error');
        $(txtName).attr('data-original-title', 'Vui lòng nhập tên.');
        chk = false;
    }
    if (!$(lokUnit).val() || $(lokUnit).val() == 0) {
        $(groupUnit).attr('class', 'form-group has-error');
        $(lokUnit).attr('data-original-title', 'Vui lòng chọn đơn vị tính.');
        chk = false;
    }
    return chk;
}