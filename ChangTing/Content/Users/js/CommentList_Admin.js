
$('.table-sort').dataTable({
    "aaSorting": [[1, "desc"]],//默认第几个排序
    "bStateSave": true,//状态保存
    "aoColumnDefs": [
        { "orderable": false, "aTargets": [0, 7] }// 指定列不参与排序
    ]
});

/*用户-删除*/
function product_del(obj, id) {
    layer.confirm('确认要删除吗？', function (index) {

        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/DeleteComment',
            data: { id: id },
            dataType: 'html',
            success: function (data1, data2) {
                if (data1,data2) {
                    layer.msg('已删除ID为' + data1 + '的评论和' + data2 + '条子评论!');
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
            url: '/Users/DeleteCommentMulti',
            data: { fxkint: fxkint },
            dataType: 'html',
            success: function (data) {
                if (data) {
                    layer.msg('删除' + data + "条数据成功", { icon: 1, time: 1000 });
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

//评论-停用
function comment_DisplayOut(obj, id) {
    layer.confirm('确认要停用吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/UpdateDisplayOut',
            data: { id: id },
            dataType: 'html',
            success: function (data) {
                if (data>0) {
                    layer.msg("已成功停用ID为:" + data + "的评论！");

                    $(obj).closest('.va-m').children('#zhuangtai').html("停用");
                    $(obj).closest('span').html("<a style='text-decoration:none;' class='ml-5' href='javascript:void(0)'" +
                        "onclick=comment_DisplayOut(this," + id + ") title='停用'><i class='Hui-iconfont'>&#xe601;</i></a>");
                }
                else {
                    layer.msg("停用评论‘" + data + "’失败！请联系管理员");
                }
            },
            error: function () {
                layer.msg('Ajex调用出错!请联系管理员');
            }
        });
    });
}

//评论-启用
function comment_DisplayUp(obj, id) {
    layer.confirm('确认要启用吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/UpdateDisplayUp',
            data: { id: id },
            dataType: 'html',
            success: function (data) {
                if (data>0) {
                    layer.msg("已成功启用ID为:" + data + "的评论！");

                    $(obj).closest('tr').children('#zhuangtai').html("正常");
                    $(obj).closest('span').html("<a style='text-decoration:none;' class='ml-5' href='javascript:void(0)'" +
                        "onclick=comment_DisplayUp(this," + id + ") title='启用'><i class='Hui-iconfont'>&#xe6dd;</i></a>");
                }
                else {
                    layer.msg("启用ID为‘" + data + + "’的评论失败！请联系管理员");
                }
            },
            error: function () {
                layer.msg('Ajex调用出错!请联系管理员');
            }
        });
    });
}
