$(function () {
    $('.skin-minimal input').iCheck({
        checkboxClass: 'icheckbox-blue',
        radioClass: 'iradio-blue',
        increaseArea: '20%'
    });//单选按钮样式等  蓝色
});

$("#list-text-button").click(
    function (g) {
        if ($('#list-textlist').css('display') != 'block') {
            $('#list-textlist').css('display', 'block');
        }
        else {
            $('#list-textlist').css('display', 'none');
        }
        g = g || window.event;//获取当前事件
        g.stopPropagation();//当前事件后的事件停止
    }
);

$(document).click(function (e) { $('#list-textlist').css('display', 'none'); });

$("#list-textlist div a").click(
    function () {
        var txt = this.text;
        var txt2 = $(this).attr('value');

        $("#Number").val(txt);
        $("#singerid").attr('value', txt2);
    });