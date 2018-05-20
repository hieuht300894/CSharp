var controls = {
    btnSave: document.getElementById('btnSave'),
    btnSaveAndNew: document.getElementById('btnSaveAndNew'),
    btnAdd: document.getElementById('btnAdd'),
    btnDetail: document.querySelectorAll('a[data-id=btnDetail]'),
    btnEdit: document.querySelectorAll('a[data-id=btnEdit]'),
    btnDelete: document.querySelectorAll('a[data-id=btnDelete]'),
    txtCode: document.getElementById('txtCode'),
    txtName: document.getElementById('txtName'),
    txtSize: document.getElementById('txtSize'),
    lokUnit: document.getElementById('lokUnit'),
    txtNote: document.getElementById('txtNote'),
    lbTitle: document.getElementById('lbTitle'),
    lbCode: document.getElementById('lbCode'),
    lbName: document.getElementById('lbName'),
    lbUnit: document.getElementById('lbUnit'),
    lbSize: document.getElementById('lbSize'),
    lbNote: document.getElementById('lbNote'),
    lbMessage: document.getElementById('lbMessage'),
};
//var btnSave = document.getElementById('btnSave');
//var btnSaveAndNew = document.getElementById('btnSaveAndNew');
//var btnAdd = document.querySelector('a[data-id=btnAdd]');
//var btnDetail = document.querySelectorAll('a[data-id=btnDetail]');
//var btnEdit = document.querySelectorAll('a[data-id=btnEdit]');
//var btnDelete = document.querySelectorAll('a[data-id=btnDelete]');
//var txtCode = document.querySelector('input[id=Code]');
//var txtName = document.querySelector('input[id=Name]');
//var txtSize = document.querySelector('input[id=Size]');
//var lokUnit = document.querySelector('select[id=IDUnit]');
//var txtNote = document.querySelector('textarea[id=Note]');
//var lbTitle = document.querySelector('h4[class=modal-title]');
//var groupCode = document.getElementById('group_code');
//var groupName = document.getElementById('group_name');
//var groupUnit = document.getElementById('group_unit');
var _iEntry = {};
var _aEntry = {};

$(controls.btnSave).off(defaultValue.Click).on(defaultValue.Click, async function (e) {
    e.preventDefault();
    if (ValidData()) {
        var jsonResult;
        _aEntry = _iEntry;
        _aEntry.Name = $(controls.txtName).val();
        _aEntry.IDUnit = $(controls.lokUnit).val();
        _aEntry.Note = $(controls.txtNote).val();
        _aEntry.Size = $(controls.txtSize).val();
        if (_aEntry.KeyID > 0) {
            _aEntry.CreatedDate = defaultFunction.ParseDateTime(_aEntry.CreatedDate);
            _aEntry.ModifiedDate = defaultFunction.Now();
            _aEntry.ModifiedBy = 0;
            _aEntry.Status = defaultValue.Update;
            jsonResult = await defaultFunction.SaveData('Product/EditItem', defaultValue.Put, JSON.stringify({ OldData: _iEntry, NewData: _aEntry, Status: defaultValue.Update }));
        }
        else {
            _aEntry.Code = $(controls.txtCode).val();
            _aEntry.CreatedDate = defaultFunction.Now();
            _aEntry.CreatedBy = 0;
            _aEntry.Status = defaultValue.Insert;
            jsonResult = await defaultFunction.SaveData('Product/CreateItem', defaultValue.Post, JSON.stringify({ OldData: _iEntry, NewData: _aEntry, Status: defaultValue.Insert }));
        }

        if (jsonResult.StatusCode == defaultValue.OK && jsonResult.Data) {
            _iEntry = jsonResult.Data;
            _aEntry = jsonResult.Data;
            $(controls.lbTitle).text('Cập nhật sản phẩm');
            $(controls.lbMessage.parentNode).attr('class', 'form-group has-success');
            $(controls.lbMessage).text(defaultValue.MessageSuccess);
        }
        else {
            //console.log(jsonResult.Message);
            $(controls.lbMessage.parentNode).attr('class', 'form-group has-error');
            $(controls.lbMessage).text(defaultValue.MessageFail);
        }
    }
});

$(controls.btnSaveAndNew).off(defaultValue.Click).on(defaultValue.Click, async function (e) {
    e.preventDefault();
    if (ValidData()) {
        _aEntry = _iEntry;
        _aEntry.Name = $(controls.txtName).val();
        _aEntry.IDUnit = $(controls.lokUnit).val();
        _aEntry.Note = $(controls.txtNote).val();
        _aEntry.Size = $(controls.txtSize).val();
        if (_aEntry.KeyID > 0) {
            _aEntry.CreatedDate = defaultFunction.ParseDateTime(_aEntry.CreatedDate);
            _aEntry.ModifiedDate = defaultFunction.Now();
            _aEntry.ModifiedBy = 0;
            _aEntry.Status = defaultValue.Update;
            jsonResult = await defaultFunction.SaveData('Product/EditItem', defaultValue.Put, JSON.stringify({ OldData: _iEntry, NewData: _aEntry, Status: defaultValue.Update }));
        }
        else {
            _aEntry.Code = $(controls.txtCode).val();
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
            $(controls.lbMessage.parentNode).attr('class', 'form-group has-error');
            $(controls.lbMessage).text(defaultValue.MessageFail);
        }
    }
});

$(controls.btnAdd).off(defaultValue.Click).on(defaultValue.Click, function (e) {
    e.preventDefault();
    InsertItem();
});

for (var i = 0; i < controls.btnEdit.length; i++) {
    $(controls.btnEdit[i]).off(defaultValue.Click).on(defaultValue.Click, function (e) {
        e.preventDefault();
        UpdateItem(this);
    });
}

async function InsertItem() {
    ClearFormat();
    $(controls.lbTitle).text('Thêm mới sản phẩm');
    var jsonResult = await defaultFunction.GetData('Product/InitItem', defaultValue.Get, {});
    if (jsonResult.StatusCode == defaultValue.OK && jsonResult.Data) {
        _iEntry = jsonResult.Data;
        $(controls.txtCode).removeAttr('disabled');
        $(controls.txtCode).val(_iEntry.Code);
        $(controls.txtName).val(_iEntry.Name);
        $(controls.lokUnit).val(_iEntry.IDUnit);
        $(controls.txtNote).val(_iEntry.Note);
        $(controls.txtSize).val(_iEntry.Size);
    }
    else {
        //console.log(jsonResult.Message);
    }
}

async function UpdateItem(_btnEdit) {
    ClearFormat();
    $(controls.lbTitle).text('Cập nhật sản phẩm');
    var rowid = $(_btnEdit).attr('data-row');
    var row = $('tr[data-row=' + rowid + ']')[0];
    var column = $(row).find('td[data-id]');
    var id = $(column).attr('data-id');
    var jsonResult = await defaultFunction.GetData('Product/GetItem', defaultValue.Get, { id: id });
    if (jsonResult.StatusCode == defaultValue.OK && jsonResult.Data) {
        _iEntry = jsonResult.Data;
        $(controls.txtCode).attr('disabled', '');
        $(controls.txtCode).val(_iEntry.Code);
        $(controls.txtName).val(_iEntry.Name);
        $(controls.lokUnit).val(_iEntry.IDUnit);
        $(controls.txtNote).val(_iEntry.Note);
        $(controls.txtSize).val(_iEntry.Size);
    }
    else {
        //console.log(jsonResult.Message);
    }
}

function ClearFormat() {
    $(controls.lbCode.parentNode).attr('class', 'form-group');
    $(controls.lbName.parentNode).attr('class', 'form-group');
    $(controls.lbUnit.parentNode).attr('class', 'form-group');
    $(controls.lbMessage.parentNode).attr('class', 'form-group');

    $(controls.txtCode.parentNode).attr('class', 'form-group');
    $(controls.txtName.parentNode).attr('class', 'form-group');
    $(controls.lokUnit.parentNode).attr('class', 'form-group');

    $(controls.txtCode).attr('data-original-title', '');
    $(controls.txtName).attr('data-original-title', '');
    $(controls.lokUnit).attr('data-original-title', '');

    $(controls.lbMessage).text('');
}

function ValidData() {
    var chk = true;
    ClearFormat();
    if (!$(controls.txtCode).val()) {
        $(controls.lbCode.parentNode).attr('class', 'form-group has-error');
        $(controls.txtCode.parentNode).attr('class', 'form-group has-error');
        $(controls.txtCode).attr('data-original-title', 'Vui lòng nhập mã.');
        chk = false;
    }
    if (!$(txtName).val()) {
        $(controls.lbName.parentNode).attr('class', 'form-group has-error');
        $(controls.txtName.parentNode).attr('class', 'form-group has-error');
        $(controls.txtName).attr('data-original-title', 'Vui lòng nhập tên.');
        chk = false;
    }
    if (!$(lokUnit).val() || $(lokUnit).val() == 0) {
        $(controls.lbUnit.parentNode).attr('class', 'form-group has-error');
        $(controls.lokUnit.parentNode).attr('class', 'form-group has-error');
        $(controls.lokUnit).attr('data-original-title', 'Vui lòng chọn đơn vị tính.');
        chk = false;
    }
    return chk;
}