
$('.table-sort').dataTable({
    "aaSorting": [[1, "desc"]],//默认第几个排序
    "bStateSave": true,//状态保存
    "aoColumnDefs": [
        { "orderable": false, "aTargets": [0, 2, 3, 5, 7,9] }// 指定列不参与排序
    ]
});

/*用户-删除*/
function product_del(obj, id) {
    layer.confirm('确认要删除ID为'+id+'的用户吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/DeleteUsers',
            data: { id: id },
            dataType: 'html',
            success: function (data1, data2, data3) {
                if (data1) {
                    layer.msg('已删除ID为' + id + '的用户,同时删除了' + data2 + '条评论和' + data3 + '条收藏数据!', { icon: 1, time: 1000 });
                    $(obj).parents("tr").remove();
                }
                else {
                    layer.msg('删除失败!');
                }
            },
            error: function () {
                layer.msg('Ajex调用出错!请联系管理员');
            }
        });
    });
}


function datadel() {

    var fuxuankuangs = new Array();

    fuxuankuangs = document.getElementsByName("piliang");
    var fxkint = new Array();
    for (var i = 0; i < fuxuankuangs.length; i++) {
        if (fuxuankuangs[i].checked) { fxkint.push(fuxuankuangs[i].value); }
    }
    if (fxkint.length <= 0) {
        layer.msg('请选择删除项!');
        return;
    }

    layer.confirm('确认要删除吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/DeleteUsersMulti',
            data: { fxkint: fxkint },
            dataType: 'html',
            success: function (data) {
                if (data) {
                    layer.msg('删除' + fxkint.length + "条数据成功", { icon: 1, time: 1000 });
                    for (var i = 0; i < fuxuankuangs.length; i++) {
                        if (fuxuankuangs[i].checked) { $(fuxuankuangs[i]).parents("tr").remove(); }
                    }
                }
                else {
                    layer.msg('删除失败!');
                }
            },
            error: function () {
                layer.msg('Ajex调用出错!请联系管理员');
            }
    
        });
    })
}

//用户-停用
function user_AvailableOut(obj, id) {
    layer.confirm('确认要停用吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/UpdateAvailableOutUsers',
            data: { id: id },
            dataType: 'html',
            success: function (data) {
                if (data>0) {
                    layer.msg("已成功停用ID为:" + data + "的用户！");

                    $(obj).closest('.va-m').children('#zhuangtai').html("停用");
                    $(obj).closest('span').html("<a style='text-decoration:none;' class='ml-5' href='javascript:void(0)'" +
                        "onclick=user_AvailableUp(this," + id + ") title='启用'><i class='Hui-iconfont'>&#xe601;</i></a>");
                }
                else {
                    layer.msg("停用用户‘" + data + "’失败！请联系管理员");
                }
            },
            error: function () {
                layer.msg('Ajex调用出错!请联系管理员');
            }
        });
    });
}

//用户-启用
function user_AvailableUp(obj, id) {
    layer.confirm('确认要启用吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/UpdateAvailableUpUsers',
            data: { id: id },
            dataType: 'html',
            success: function (data) {
                if (data>0) {
                    layer.msg("已成功启用ID为:" + data + "的用户！");

                    $(obj).closest('tr').children('#zhuangtai').html("正常");
                    $(obj).closest('span').html("<a style='text-decoration:none;' class='ml-5' href='javascript:void(0)'" +
                        "onclick=user_AvailableOut(this," + id + ") title='停用'><i class='Hui-iconfont'>&#xe6dd;</i></a>");
                }
                else {
                    layer.msg("启用用户‘"+ data + + "’失败！请联系管理员" );
                }
            },
            error: function () {
                layer.msg('Ajex调用出错!请联系管理员');
            }
        });
    });
}