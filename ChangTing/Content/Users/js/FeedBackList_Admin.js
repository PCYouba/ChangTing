
$('.table-sort').dataTable({
    "aaSorting": [[1, "desc"]],//默认第几个排序
    "bStateSave": true,//状态保存
    "aoColumnDefs": [
        { "orderable": false, "aTargets": [0,3,4, 6] }// 指定列不参与排序
    ]
});

/*反馈-删除*/
function product_del(obj, id) {
    layer.confirm('确认要删除本条反馈吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/DeleteFeedBack',
            data: { id: id },
            dataType: 'html',
            success: function (data) {
                if (data) {
                    layer.msg('已删除!', { icon: 1, time: 1000 });
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
            url: '/Users/DeleteFeedBackMulti',
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

//反馈-未解决
function feedBack_IsSolveOut(obj, id) {
    layer.confirm('确认要标记为未解决吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/UpdateAvailableOutUsers',
            data: { id: id },
            dataType: 'html',
            success: function (data) {
                if (data>0) {
                    layer.msg("已成功标记ID为:" + data + "的反馈为未解决！");

                    $(obj).closest('.va-m').children('#zhuangtai').html("未解决");
                    $(obj).closest('span').html("<a style='text-decoration:none' class='ml-5' " +
                        "href='javascript:void(0)' onclick=feedBack_IsSolveOut(this," + id + ") " +
                        "title='标记为未解决'><i class='Hui-iconfont;>&#xe6e0;</i></a>");
                }
                else {
                    layer.msg("标记反馈ID为‘" + data + "’的反馈为未解决失败！请联系管理员");
                }
            },
            error: function () {
                layer.msg('Ajex调用出错!请联系管理员');
            }
        });
    });
}

//反馈-解决
function feedBack_IsSolveUp(obj, id) {
    layer.confirm('确认要标记为已解决吗？', function (index) {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Users/UpdateAvailableUpUsers',
            data: { id: id },
            dataType: 'html',
            success: function (data) {
                if (data>0) {
                    layer.msg("已成功标记ID为:" + data + "的反馈为已解决！");

                    $(obj).closest('tr').children('#zhuangtai').html("已解决");
                    $(obj).closest('span').html("<a style='text-decoration:none;' class='ml-5'"+
                        " href='javascript:void(0)' onclick=feedBack_IsSolveUp(this,"+id+") "+
                        "title='标记为已解决'><i class='Hui-iconfont'>&#xe6e1;</i></a>");
                }
                else {
                    layer.msg("标记用户‘"+ data + + "’为解决失败！请联系管理员" );
                }
            },
            error: function () {
                layer.msg('Ajex调用出错!请联系管理员');
            }
        });
    });
}
