//------------------------组织管理-->组织信息-----------------------------------------//
//刷新数据
function initable() {
    $("#dgOrg").treegrid({
      //  url: "/Orgs/List",
        url: abp.appPath + "api/services/app/org/GetAllOrgs",
       method:"GET",
        title: "组织管理",
        pagination: false,
       // pageSize: 10,
       // pageList: [10, 20, 30],
        toolbar: "#dg-button",
        fit: true,
        fitColumns: false,
        loadMsg: "正在加载组织信息...",
        nowarp: false,
        border: false,
        idField: "id",
        sortName: "id",
        sortOrder: "asc",
        treeField: "name",
        onLoadSuccess: function (row,data) {
            for (var i = 0; i < data.rows.length; i++) { var r = data.rows[i];}
                },
        frozenColumns: [[//冻结列
            {
                field: "chk", checkbox: true, align: "left", width: 50
            }           
        ]],
        columns: [[
            { title: "编号", field: "id", width: 50, sortable: true },
            { title: "组织名称", field: "name", width: 200, sortable: true },
            { title: "代码", field: "bizCode", width: 100, sortable: true },
            { title: "海关代码", field: "customCode", width: 100, sortable: true },
            { title: "状态", field: "status", width: 80, sortable: false },

            { title: "类型", field: "type", width: 80, sortable: false },
            { title: "父节点", field: "parentName", width: 120, sortable: false },
            { title: '创建时间', field: 'creationTime', width: 130, align: 'center' }
        ]]
    });
}
function Search() {
    var _$form = $('form[name=searchform]');
    var params = _$form.serializeFormToObject();
    $('#dgOrg').treegrid({ queryParams: params });
}


function reloaded() {   //reload
    $("#reload").click(function () {
        //
        $('#dgOrg').treegrid('reload');

    });
}

//修改点击按钮事件
function updOrgInfo() {    
    $("#edit").click(function () {
		BindTree();
        //判断选择的中
        var row = $("#dgOrg").treegrid('getSelected');        
        if (row) {
           
            $.messager.confirm('编辑', '您想要编辑吗？', function (r) {
                if (r) {
                  
                    //先绑定                    
                    showOrg(row);
                  
                     //打开对话框编辑
                    $("#divAddUpdOrg").dialog({
                        closed: false,
                        title: "修改组织信息",
                        modal: true,
                        width: 600,
                        height: 450,
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        resizable: true,
                    });                       
                }
                
            });

        } else {
            $.messager.alert('提示', ' 请选择要编辑的行！', 'warning');
        }

    });
   
}
//删除模块
function deleteOrg() {
    $("#del").click(function () {
        var rows = $("#dgOrg").datagrid("getSelections");
        if (rows.length > 0) {
            $.messager.confirm("提示", "确定要删除吗?", function (res) {
                if (res) {
                    var codes = []; //重要不是{}
                    for (var i = 0; i < rows.length; i++) {
                        codes.push(rows[i].Id);
                        _orgService.delete({
                            id: rows[i].Id
                        }).done(function () {
                            $.messager.alert("提示", "删除成功！");
                            $("#dgOrg").datagrid("clearChecked");
                            $("#dgOrg").datagrid("clearSelections");
                            $('#dgOrg').treegrid('reload');
                        });
                    }
                   
                    /*
                    $.post("/Orgs/Delete", { "ids": codes.join(',') }, function (data) {
                        if (data == "OK") {
                            $.messager.alert("提示", "删除成功！");
                            $("#dgOrg").datagrid("clearChecked");
                            $("#dgOrg").datagrid("clearSelections");
                            $("#dgOrg").datagrid("load", {});
                        }                       
                        else if (data == "NO") {
                            $.messager.alert("提示", "删除失败！");
                            return;
                        }
                    });*/
                }
            });
        }
    })
}
//清空文本框
function clearAll() {
    $("#IDUpdate").val("");
    $("#UpdBizCode").val("");
    //$("#CreateTimeUpdate").val(getNowFormatDate());    
    $("#NameUpdate").val("");
    $("#UpdCustomCode").val("");
    $(':input[name]', this).each(function () {
            $(this).val("");
        });
}
var _orgService = abp.services.app.org;
var _$modal = $("#divAddUpdOrg").parent();
var _$form = _$modal.find('form');
//弹出 添加货物信息的对话框
function showOrgDialog() {
    
    $("#add").click(function () {
        clearAll();    
       BindTree();
        $("#divAddUpdOrg").dialog({
            closed: false,
            title: "添加组织信息",
            modal: true,
            width: 600,
            height: 450,
            collapsible: true,
            minimizable: true,
            maximizable: true,
            resizable: true
        });
    });

    $("#btnSave").click(function () {
        //  alert('1');
        //启用        
        //保存
        if (!_$form.valid()) {
            return;
        }
        
        var id = $("#IDUpdate").val();
        if (id == "" || id == undefined || id=="0") {
            //验证
            $.messager.confirm('确认', '您确认要保存吗？', function (r) {
                if (r) {
                    $("#IDUpdate").val("0");
                  //  var postData = GetOrg();
                    var postData = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
                    if (postData == null || postData == undefined || postData.Name == "" || postData.BizCode == "") {
                        $.messager.alert('提示', ' 请填写相关必填项！', 'warning');
                        return;
                    }
                    abp.ui.setBusy(_$modal);
                    _orgService.create(postData).done(function () {                                                                
                        $.messager.alert("提示", "保存成功！");
                        $("#IDUpdate").val("");
                        _$modal.modal('hide');
                        $("#divAddUpdOrg").dialog("close");
                        initable(); //reload page to see new user!
                    }).always(function () {
                      //  $.messager.alert("提示", "保存失败！");
                        abp.ui.clearBusy(_$modal);
                        });
                    /*
                    $.post("/Orgs/Add", postData, function (data) {
                       // alert(data);
                        var obj = JSON.parse(data);
                        if (obj.result == "OK") {
                            $("#IDUpdate").val(obj.Id)
                            $("#divAddUpdOrg").dialog("close");
                            $.messager.alert("提示", "保存成功！");
                            initable();
                        }
                        else if (obj.result == "NO") {
                            $.messager.alert("提示", "保存失败！");
                            return;
                        }
                    });
                    */
                }
            })
        }
        else {
            saveDetail();
        }

    });
}

function saveDetail() {
    if (!_$form.valid()) {
        return;
    }

    $.messager.confirm('确认', '您确认要修改吗？', function (r) {
               
            var postData =  _$form.serializeFormToObject();
            if (postData==null || postData==undefined || postData.BizCode == "" || postData.Name == "") {
                $.messager.alert('提示', ' 请填写相关必填项！', 'warning');
                return;
        }

        abp.ui.setBusy(_$modal);
        _orgService.update(postData).done(function () {
            $.messager.alert("提示", "修改成功！");            
            _$modal.modal('hide');
            $("#divAddUpdOrg").dialog("close");
            initable(); //reload page to see new user!
        }).always(function () {        
            abp.ui.clearBusy(_$modal);
        });
        
           /*
            $.post("/Orgs/Update", postData, function (data) {
               // alert(data);
                if (data=="OK") {
                    $.messager.alert("提示", "修改成功！");                   
                    $("#divAddUpdOrg").dialog("close");                    
                    initable();
                }
                else {
                    $.messager.alert("提示", data.msg);
                    return;
                }              
            });        */
        })    
}
function GetOrg() {    
    var postData = {
        "Id": $("#IDUpdate").val(),
        "Name": $("#CargoNameUpdate").val(),
        "BizCode": $("#UpdCargoCode").val(),
        //"CreationTime": $("#CreateTimeUpdate").val(),
        "CustomCode": $("#SupplierIdUpdate").val(),
        "Type": $("#BrandUpdate").val(),
        "Status": $("#CountryUpdate").val(),
        "IsLeaf": $("#CurrUpdate").val(),
        "IsAutoExpand": $("#GrossWtUpdate").val(),
       
        //"UpdateTime": $("#CreateTimeUpdate").val(),
        "ParentName":  $("#AddTree").combotree("getText"), //取所有选中的文本，是一个String
        "ParentId": $("#AddTree").combotree("getValue")//取得选中的编码，单个的
           
    };
    return postData;
}

function showOrg(row) {
    
    $("#IDUpdate").val(row.Id);
    $("#NameUpdate").val(row.Name);
    $("#UpdBizCode").val(row.BizCode);
    
    $("#UpdType").val(row.Type);
    $("#UpdCustomCode").val(row.CustomCode);
    $("#UpdIsAutoExpand").val(row.IsAutoExpand);
    $("#UpdIsLeaf").val(row.IsLeaf);
    $("#UpdStatus").val(row.Status);
    $("#UpdHotKey").val(row.HotKey);
    $("#UpdIconName").val(row.IconName);
    $("#RemarkUpdate").val(row.Remark);
    //$("#UpdParentId").val(row.ParentId);
    $("#AddTree").combotree('setValue', row.ParentId);
    $("#AddTree").combotree('setText', row.ParentName);
        //    $('#AddTree').combotree('select',t[0].id);
    $('#UpdParentName').val(row.ParentName);   
}

function BindTree() {
   
    $('#AddTree').combotree({
        url: '/Orgs/GetJsonTree',
        valueField: 'Id',
        textField: 'Name',
        multiple: false,
        editable: false,
        method: 'get',
        panelHeight: 'auto',
        checkbox: false,
        //required: true,
        //全部折叠
        onLoadSuccess: function (node, data) {
            $('#AddTree').combotree('tree').tree("expandAll"); //collapseAll
        },
        onSelect: function (node) {
            //$('#UpdParentId').val(node.id);
            $('#UpdParentName').val(node.text);            
           // alert(node.id);
        }
    });
}
	
//------------------------系统管理-->组织信息结束-----------------------------------------//
