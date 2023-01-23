//-----------------------系统管理-->入库单管理-----------------------------------------//
var editIndex = undefined;
var mainIndex = undefined;
//刷新数据
function initable() {
    $("#dgOutSO").datagrid({
        url: "/OutStock/List",
        //url:"api/services/app/instock/GetAllInStockOrders",
        title: "出库单管理",
        pagination: true,
        pageSize: 10,
        pageList: [10, 20, 30],
        fit: true,
        fitColumns: false,
        loadMsg: "正在加载出库单信息...",
        nowarp: false,
        border: false,
        idField: "Id",
        sortName: "Id",
        sortOrder: "asc",
        frozenColumns: [[//冻结列
            { field: "ck", checkbox: true, align: "left", width: 50 }
           
        ]],
        columns: [[
            { title: "编号", field: "Id", width: 50, sortable: true },
            { title: "出库单号", field: "No", width: 100, sortable: true },            
            {title: "状态", field: "Status", width: 50            },
            { title: '出库日期', field: 'ReceiveTime', width: 100, align: 'center' },
            { title: "车牌号", field: "VehicleNo", width: 100, sortable: false },
            { title: '客户', field: 'CustomerName', width: 120, align: 'center' },
            { title: '收货人', field: 'Consignee', width: 120, align: 'center' },
            { title: "净重", field: "Nwt", width: 100, sortable: true },
            { title: "毛重", field: "Gwt", width: 100, sortable: true },
          
            { title: '审核人',field: 'Checker', width: 120, align: 'center' },
            { title: '件数', field: 'PackageQty', width: 100, align: 'center' },
            { title: '创建时间', field: 'CreationTime', width: 100, align: 'center' }
        ]]
    });
 
}

//显示送货单数据
function ShowCargo() {
    abp.log.warn('入库货物信息列表日志...'); 
    $("#dgCargo").datagrid({
        url: "/InStock/List",
        title: "入库货物管理管理",
        pagination: true,
        pageSize: 10,
        pageList: [10, 20, 30],
        fit: true,
        fitColumns: false,
        loadMsg: "正在加载入库货物信息...",
        nowarp: false,
        border: false,
        idField: "Id",
        sortName: "Id",
        sortOrder: "asc",
        frozenColumns: [[//冻结列
            { field: "ck", checkbox: true, align: "left", width: 50 }

        ]],
        columns: [[
            { title: "编号", field: "Id", width: 50, sortable: true },
            { title: "供应商", field: "SupplierId", width: 80, sortable: true },
            { title: "HSCode", field: "HSCode", width: 100, sortable: true },
            { title: "货物代码", field: "CargoCode", width: 100, sortable: true },
            { title: "货物名称", field: "CargoName", width: 80, sortable: false },
            { title: "规格型号", field: "Spcf", width: 100, sortable: false },
            { title: "产销国", field: "Country", width: 80, sortable: false },
            { title: "计量单位", field: "Unit", width: 100, sortable: false },
            { title: "包装", field: "Package", width: 100, sortable: false },
            { title: "单价", field: "Price", width: 100, sortable: false },
            { title: "币制", field: "Curr", width: 80, sortable: false },
            {
                title: "长宽高", field: "Length", width: 100, sortable: false, formatter: function (value, row, index) {
                    return row.Length + '*' + row.Width + '*' + row.Height;
                }
            },
            { title: "体积", field: "Vol", width: 80, sortable: false },
            { title: "备注", field: "Remark", width: 80, sortable: false },
            { title: '创建时间', field: 'CreationTime', width: 100, align: 'center' }

        ]]
        

    });
    abp.log.warn('3货物信息列表日志...'); 
}

function ShowCargoInfo() {
  
    $("#divImportCargo").dialog({
            closed: false,
            title: "选择入库货物信息",
            modal: true,
            width: 820,
            height: 550,
            collapsible: true,
            minimizable: true,
            maximizable: true,
            resizable: true
        });
       ShowCargo();
       $("#dgCargo").datagrid("clearChecked");
       $("#dgCargo").datagrid("clearSelections");

}

function reloaded() {   //reload
    $("#reload").click(function () {
        //
        $('#dgOutSO').datagrid('reload');

    });}

//修改点击按钮事件
function updInSOInfo() {    
    $("#edit").click(function () {
       
        //判断选择的中
        var row = $("#dgOutSO").datagrid('getSelected');        
        if (row) {
           
            $.messager.confirm('编辑', '您想要编辑吗？', function (r) {
                if (r) {
                  
                     //打开对话框编辑
                    $("#divAddUpdINO").dialog({
                        closed: false,
                        title: "修改出库单",
                        modal: true,
                        width: 820,
                        height: 550,
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        resizable: true,
                    });    
                    //先绑定                    
                    showINO(row);
                    defaultTab();
                    ShowDetail(row.No);
                }
                
            });
            SetEnabled(row.Status);
        } else {
            $.messager.alert('提示', ' 请选择要编辑的行！', 'warning');
        }

    });
    
}
//删除模块
function deleteInSO() {
    $("#del").click(function () {
        var rows = $("#dgOutSO").datagrid("getSelections");
        if (rows.length > 0) {
            $.messager.confirm("提示", "确定要删除吗?", function (res) {
                if (res) {
                    var codes = []; //重要不是{}
                    for (var i = 0; i < rows.length; i++) {
                        codes.push(rows[i].Id);
                    }
                    $.post("/OutStock/Delete", { "ids": codes.join(',') }, function (data) {
                        if (data == "OK") {
                            $.messager.alert("提示", "删除成功！");
                            $("#dgINSO").datagrid("clearChecked");
                            $("#dgINSO").datagrid("clearSelections");
                            $("#dgINSO").datagrid("load", {});
                        }                       
                        else if (data == "NO") {
                            $.messager.alert("提示", "删除失败！");
                            return;
                        }
                    });
                }
            });
        }
    })
}
//清空文本框
function clearAll() {

    
    $("#divAddUpdINO input").each(function () {
            $(this).val("");
        }); 
    $("#PreDeliveryTimeUpdate").val(getNowFormatDate());    
    $("#StatusUpdate").val("0");
    $("#NwtUpdate").val("0");

    $("#GwtUpdate").val("0");
    $("#PackageQtyUpdate").val("0");
    
}
function GetNo() {
    // url: abp.appPath + "api/services/app/org/GetAllOrgs",
    $.get(abp.appPath + "api/services/app/OutStockOrder/GetNo", function (data) {
    //     alert(data);
      //  var obj = JSON.parse(data);
        $("#UpdNO").val(data);
        //$("#IDUpdate").val(obj.Id);
    });
}
//获取当前时间，格式YYYY-MM-DD
function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = year + seperator1 + month + seperator1 + strDate;
    return currentdate;
}

//将表单数据转为json
function form2Json(id) {

    var arr = $("#" + id).serializeArray()
    var jsonStr = "";

    jsonStr += '{';
    for (var i = 0; i < arr.length; i++) {
        jsonStr += '"' + arr[i].name + '":"' + arr[i].value + '",'
    }
    jsonStr = jsonStr.substring(0, (jsonStr.length - 1));
    jsonStr += '}'

    var json = JSON.parse(jsonStr)
    return json
}

function searchFunc() {
  //  var obj = $("#searchform").serializeArray();
  //  $("#dgPOD").datagrid({ queryParams: form2Json("searchform") });
    var jsonStr = '{"cargoName":"' + $("#cargoName").val() + '"}';
    var queryParams = JSON.parse(jsonStr);
    $("#dgPOD").datagrid({ queryParams: queryParams });
   // $("#dgPOD").datagrid('reload');
} //扩展方法
//点击清空按钮出发事件
function clearSearch() {
    $("#dgPOD").datagrid("load", {}); //重新加载数据，无填写数据，向后台传递值则为空
    $("#searchForm").find("input").val(""); //找到form表单下的所有input标签并清空
}
function SetEnabled(status) {
    //var status = $("#StatusUpdate").val()
    if (status == "提交") {
        $("#btnSave").prop('disabled', true);
    }
    else {
        $("#btnSave").removeAttr("disabled");
    }
}

function defaultTab() {
    //默认显示第一个tab
    $('#box').tabs('select', "出库单");
   /* var t = $('#box').tabs('getTab', "入库单");
    $('#box').tabs('update', {
        tab:t,
        options: {
            selected: true
        }
    });*/
}
//弹出 导入送货单的的对话框
function showInSODialog() {

    $("#add").click(function () {
       
            $.messager.confirm('编辑', '您想要创建出库单吗？', function (r) {
                if (r) {

                    //打开对话框编辑
                    $("#divAddUpdINO").dialog({
                        closed: false,
                        title: "新增出库单",
                        modal: true,
                        width: 820,
                        height: 550,
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        resizable: true,
                    });                    
                }
                defaultTab();
                GetNo();
                clearAll();
                ShowDetail("");
            });
       
        //    SetEnabled("新建");     

    });

  

    $("#btnSave").click(function () {
      
        //保存
        var id = $("#IDUpdate").val();
        if (id == "" || id == undefined) {
            //验证
            $.messager.confirm('确认', '您确认要保存吗？', function (r) {
                if (r) {
                   
                    var postData = GetINO();
                    
                    if (postData.No == "" || postData.CustomerCode == "" || postData.CustomerName=="" || postData.OwnerName=="") {
                        $.messager.alert('提示', ' 请填写相关必填项！', 'warning');
                        return;
                    }
                   
                    $.post("/OutStock/Add", postData, function (data) {
                        if (data == "OK") {
                           // $("#divAddUpdDO").dialog("close");
                            $.messager.alert("提示", "保存成功！");
                            initable();
                            collapseRows();

                        }
                        else if (data == "NO") {
                            $.messager.alert("提示", "保存失败！");
                            return;
                        }
                    });

                }
            })
        }
        else {
            saveDetail();
            initable();
            collapseRows();

        }

    });



}


//添加明细

function ShowDetail(no) {
    var lastIndex;
    $("#dgINOD").datagrid({
        url: "/OutStock/GetDetail?no=" + no,
        title: "入库单明细",
        pagination: false,      
        fit: true,
        fitColumns: false,
        loadMsg: "正在加载出库单明细信息...",
        toolbar: [
            { text: '添加明细', iconCls: 'icon-add', handler: function () { ShowCargoInfo(); } },
          
            { text: '删除', iconCls: 'icon-remove', handler: function () { deviceInfoDeleteClick(); } },
            '-'
        ],
        nowarp: false,
        border: false,
        idField: "Id",
        sortName: "Id",
        sortOrder: "asc",
        singleSelect: true,
        iconCls: 'icon-edit',
      
        columns: [[
            { title: "编号", field: "SeqNo", width: 50, sortable: true },
            { title: "入库单号", field: "InStockNo", width: 100, sortable: true },
            { title: "HSCode", field: "HSCode", width: 80, sortable: false },
            { title: "货物代码", field: "CargoCode", width: 100, sortable: true },
            { title: "货物名称", field: "CargoName", width: 160, sortable: false },
            { title: "规格型号", field: "Spcf", width: 80, sortable: false },
            {
                title: "数量", field: "Qty", width: 100, align: 'center', editor: {
                    type: 'numberbox', options: {
                        required: true, min: 0, precision: 4
                    }
                }
            },
            {
                title: "长", field: "Length", width: 70, align: 'center', editor: {
                    type: 'numberbox', options: {
                        required: true, min: 0, precision: 2
                    }
                }
            },
            {
                title: "宽", field: "Width", width: 70, align: 'center', editor: {
                    type: 'numberbox', options: {
                        required: true, min: 0, precision: 2
                    }
                }
            },
            {
                title: "高", field: "Height", width: 70, align: 'center', editor: {
                    type: 'numberbox', options: {
                        required: true, min: 0, precision: 2
                    }
                }
            },
            { title: "产销国", field: "Country", width: 70, align: 'center' },
            {
                title: "单价", field: "Price", width: 100, align: 'center', editor: {
                    type: 'numberbox', options: {
                        required: true, min: 0, precision: 2
                    }
                }
            },        
            {
                title: "总价", field: "TotalAmt", width: 100, align: 'center', editor: {
                    type: 'numberbox', options: {
                        required: true, min: 0, precision: 2
                    }
                }
            },
            { title: "包装", field: "Package", width: 70, align: 'center' },
            { title: "计量单位", field: "Unit", width: 70, align: 'center' },
            {
                title: "总体积", field: "Vol", width: 70, align: 'center', editor: {
                    type: 'numberbox', options: {
                        required: true, min: 0, precision: 4
                    }
                }
            },
            { title: "品牌", field: "Brand", width: 70, align: 'center' }

          
        ]],
    
    

        onClickRow: function (index, rowData) {
          
            if (lastIndex != index) {
                $('#dgINOD').datagrid('endEdit', lastIndex);
                editrow(index);
            }
            lastIndex = index;
            mainIndex = index;
        },
        
        onBeginEdit: function (rowIndex, rowData) {  
            setEditing(rowIndex);
        }
    });
}
//计算报价小计
function setEditing(rowIndex) {
    var editors = $('#dgINOD').datagrid('getEditors', rowIndex);
    var priceEditor = editors[4];
    var qtyEditor = editors[0];
    var lengthEditor = editors[1];
    var widthEditor = editors[2];
    var heightEditor = editors[3];
    var totalVolEditor = editors[6];
    var totalAmtEditor = editors[5];
    priceEditor.target.numberbox({
        onChange: function () { calculate();}
    });
    qtyEditor.target.numberbox({
        onChange: function () {
            calculate();
            calculateVol();
        }
    });
    lengthEditor.target.numberbox({
        onChange: function () { calculateVol(); }
    });
    widthEditor.target.numberbox({
        onChange: function () { calculateVol(); }
    });
    heightEditor.target.numberbox({
        onChange: function () { calculateVol(); }
    });
    function calculate() {
        var cost = (priceEditor.target.val()) * (qtyEditor.target.val());
        console.log(cost);
        totalAmtEditor.target.numberbox("setValue", cost);
    }
    function calculateVol() {
        var vol = (lengthEditor.target.val() / 100.0) * (widthEditor.target.val() / 100.0) * (heightEditor.target.val() / 100.0)* (qtyEditor.target.val());
        console.log(vol);
        totalVolEditor.target.numberbox("setValue", vol);
    }
}
function editrow(index) {
    $('#dgINOD').datagrid('selectRow', index)
        .datagrid('beginEdit', index);
}
function endEdit() {
    var rows = $('#dgINOD').datagrid('getRows');
    if (rows==undefined) {
        return;
    }
    for (var i = 0; i < rows.length; i++) {
        $('#dgINOD').datagrid('endEdit', i);
    }
}
//设置出库单明细数据
function setGridDetail(effectRow) {

    if ($('#dgINOD').datagrid('getChanges').length) {

        var inserted = $('#dgINOD').datagrid('getChanges', "inserted");
        var deleted = $('#dgINOD').datagrid('getChanges', "deleted");
        var updated = $('#dgINOD').datagrid('getChanges', "updated");
        
        if (inserted.length) {
            effectRow["inserted"] = JSON.stringify(inserted);
        }
        if (deleted.length) {
            effectRow["deleted"] = JSON.stringify(deleted);
        }
        if (updated.length) {
            effectRow["updated"] = JSON.stringify(updated);
        }
     }
    return effectRow;
}
function endEditSub(ddv) {    
    if (mainIndex != undefined) {
        var rows = ddv.datagrid('getRows');
        if (rows!=undefined) {
            for (var i = 0; i < rows.length; i++) {
                ddv.datagrid('endEdit', i);
            }  
        }
          
    }
}
function saveDetail() {

    endEdit();
    $.messager.confirm('确认', '您确认要修改吗？', function (r) {
        var effectRow = new Object();
        var postData = GetINO();
        if (postData.Id) {
            effectRow["postdata"] = JSON.stringify(postData);
        }
    
        effectRow = setGridDetail(effectRow);
        $.post("/OutStock/Update", effectRow, function (data) {
           //  alert(data);
            if (data.success) {
                $.messager.alert("提示", data.result);
                $('#dgINOD').datagrid('acceptChanges');
                // $("#divAddUpdPO").dialog("close");                    
                // initable();
               
            }
            else {
                $.messager.alert("提示", data.result);
                return;
            }
        }, "JSON")
            ;
       
        })    
}
function init() {
    $("#PreDeliveryTimeUpdate").val(getNowFormatDate());
    $("#CreationTimeUpdate").val(getNowFormatDate());
    $("#btnCancle").click(function () {       
        $("#divAddUpdINO").dialog("close");   
        $('#dgINSO').datagrid('reload');
    });
    $("#btnCancleDO").click(function () {
        $("#divImportDO").dialog("close");
        $('#dgINSO').datagrid('reload');
    });


    $("#btnImportDO").click(function () {
        //保存

        var rows = $('#dgCargo').datagrid('getSelections');
        if (rows.length > 0) {

            //验证
            $.messager.confirm('确认', '您确认要保存所选择的货物信息吗？', function (r) {
                if (r) {
                    var obj_No = $("#UpdNO").val();
                    var ids = [];//重要不是{}
                    for (var i = 0; i < rows.length; i++) {
                        ids.push(rows[i].Id);
                    }
                    var postData = {
                        "Ids": ids.join(','),
                        "No": obj_No
                    };

                    $.post("/OutStock/ImportInStockOrder", postData, function (data) {
                        if (data == "OK") {
                            $.messager.alert("提示", "保存货物信息成功！");
                            ShowDetail(obj_No);
                        }
                        else if (data == "NO") {
                            $.messager.alert("提示", "保存货物信息失败！");
                            return;
                        }
                    });

                }
            })
        }

    });



    $("#btnSubmit").click(function () {
        //保存
        var id = $("#IDUpdate").val();
        if (id == "" || id == undefined) {
            $.messager.alert("提示", "入库单没有保存，请先保存！");
            return;
        }
            //验证
            $.messager.confirm('确认', '您确认要提交入库单吗？', function (r) {
                if (r) {
                   
                    var postData = {
                        "Id": id
                        
                    };

                    $.post("/OutStock/Submit", postData, function (data) {
                        if (data == "OK") {
                            $.messager.alert("提示", "入库单已经提交成功！");                           
                            $("#StatusUpdate").val("提交");
                            SetEnabled("提交");
                        }
                        else if (data == "NO") {
                            $.messager.alert("提示", "入库单提交失败！");
                            return;
                        }
                    });

                }
            })
        

    });
}

function endEditing(ddv) {
    var changes = ddv.datagrid('getChanges');
    if (editIndex == undefined) { return true }
    if (ddv.datagrid('validateRow', editIndex)) {
        //验证前一行
        //返回编辑器，结束编辑 
        ddv.datagrid('endEdit', editIndex);
        editIndex = undefined;
        return true;
    } else { return false; }
}

function collapseRows() {
    var rows = $('#dgINOD').datagrid('getRows');
    $.each(rows, function (i, k) {
        //获取当前所有展开的子网格
        var expander = $('#dgINOD').datagrid('getExpander', i);
        if (expander.length && expander.hasClass('datagrid-row-collapse')) {
            if (k.id != row.id) {
                //折叠上一次展开的子网格
                $('#dgINOD').datagrid('collapseRow', i);
            }
        }
    });
}

function GetINO() {
    var postData = {
        //"Id": $("#IDUpdate").val(),
        "No": $("#UpdNO").val(),
        "DeliveryNo": "",
        "PreDeliveryTime": $("#PreDeliveryTimeUpdate").val(),
        "CustomerCode": $("#CustomerCodeUpdate").val(),
        "OwnerName": $("#OwnerNameUpdate").val(),
        //"ConsignerSccd": $("#ConsignerSccdUpdate").val(),
        "OwnerCode": $("#OwnerCodeUpdate").val(),
        "CustomerName": $("#CustomerNameUpdate").val(),
        "CreationTime": $("#CreationTimeUpdate").val(),
        "CheckTime": $("#CheckTimeUpdate").val(),
        "WarehouseType": $("#WarehouseTypeUpdate").val(),
        "WarehouseNo": $("#WarehouseNoUpdate").val(),
        "Oper": $("#OperUpdate").val(),
        "Receiver": $("#ReceiverUpdate").val(),
        "Nwt": $("#NwtUpdate").val(),
        //"FreightClause": $("#FreightClauseUpdate").combobox('getValue'),
        "Remark": $("#RemarkUpdate").val(),
        //"ForwardingClause": $("#ForwardingClauseUpdate").combobox('getValue'),
        "ReceiveTime": $("#ReceiveTimeUpdate").val(),      
        "Status": $("#StatusUpdate").val(),
        "Gwt": $("#GwtUpdate").val(),
        "Checker": $("#CheckerUpdate").val(),
        "PackageQty": $("#PackageQtyUpdate").val(),
        "LastUpdateTime": "",
        "LastOper":""
    };
    var id = $("#IDUpdate").val();
    if (!(id=="" || id==undefined)) {
        postData.Id = id;
    }
    return postData;
}

function showINO(row) {
     
    $("#IDUpdate").val(row.Id);
    $("#UpdNO").val(row.No);
    $("#PreDeliveryTimeUpdate").val(row.PreDeliveryTime);
    $("#CustomerCodeUpdate").val(row.CustomerCode);
    $("#PackageQtyUpdate").val(row.PackageQty);
    $("#CustomerNameUpdate").val(row.CustomerName);
    //$("#ConsignerSccdUpdate").val(row.ConsignerSccd);
    $("#OwnerCodeUpdate").val(row.OwnerCode);
    $("#OwnerNameUpdate").val(row.OwnerName);
    $("#CreationTimeUpdate").val(row.CreationTime);
    $("#CheckTimeUpdate").val(row.CheckTime);
    $("#WarehouseTypeUpdate").val(row.WarehouseType);
    $("#WarehouseNoUpdate").val(row.WarehouseNo);
    $("#OperUpdate").val(row.Oper);
    $("#ReceiverUpdate").val(row.Receiver);
    $("#NwtUpdate").val(row.Nwt);
    //$("#FreightClauseUpdate").combobox('setValue', row.FreightClause);
    $("#RemarkUpdate").val(row.Remark);
    $("#ReceiveTimeUpdate").val(row.ReceiveTime);
    $("#StatusUpdate").val(row.Status);    
    $("#GwtUpdate").val(row.Gwt);
    $("#CheckerUpdate").val(row.Checker);
    //$("#ForwardingClauseUpdate").combobox('setValue', row.ForwardingClause);

}
//------------------------系统管理-->入库单管理结束-----------------------------------------//
