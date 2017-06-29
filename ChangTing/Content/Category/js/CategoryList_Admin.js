
$('.table-sort').dataTable({
    "aaSorting": [1, "asc"],//默认第几个排序
    "bStateSave": true,//状态保存
    "aoColumnDefs": [
        { "orderable": false, "aTargets": [0, 2, 6] }// 指定列不参与排序
    ]
});

/*图片-删除*/
function product_del(obj, id) {
layer.confirm('确认要删除id为' + id + '的类别吗？', function () {
    layer.confirm('这样做同样会删除该类别的所有歌曲,您确定这样做吗？', function () {
        $.ajax({
            cache: false,
            type: 'Post',
            url: '/Music/DeleteCategory',
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
            url: '/Music/DeleteCategoryMulti',
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
