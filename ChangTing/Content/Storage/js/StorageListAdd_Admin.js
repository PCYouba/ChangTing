$(function () {
    $('.skin-minimal input').iCheck({
        checkboxClass: 'icheckbox-blue',
        radioClass: 'iradio-blue',
        increaseArea: '20%'
    });//单选按钮样式等  蓝色
});
function getFileName(o) {
    var pos = o.lastIndexOf("\\");
    return o.substring(pos + 1);
}
$(function () {
    $("#file_music").change(function () {
        var $file = $(this);
        $('#preview').text(getFileName($file.val()));
    });
});


var ts;
$(".list-text-button").click(
    function (g) {
        $('.list-textlist').css('display', 'none');
        ts = this;
        if (
            $(ts).next().css('display') != 'block') {
            $(ts).next().css('display', 'block');
        }
        else {
            $(ts).next().css('display', 'none');
        }
        g = g || window.event;//获取当前事件
        g.stopPropagation();//当前事件后的事件停止
    }
);

$(document).click(function (e) { $('.list-textlist').css('display', 'none'); });

$(".list-textlist").find('a').click(
    function () {
        var txt = this.text;
        var txt2 = $(this).attr('value');
        var thisthis = $(this).parents('div').attr('_val');
        
        $("#"+thisthis+"name").val(txt);
        $("#" + thisthis + "id").attr('value', txt2);

        if ($(this).parents('div').hasClass('SingerName')) {
            $.ajax({
                cache: false,
                type: 'Post',
                url: '/Singer/SelectSingerIdAlbum',
                data: { id: txt2 },
                dataType: 'html',
                success: function (data) {
                    var obj = JSON.parse(data);
                    var x ="";
                    for (var i = 0; i < obj.length; i++) {
                        x += "<a value=" + obj[i].AlbumId + ">" + obj[i].Name + "</a>";
                    }
                    $(".album").html(x);
                    $("#albumname").val('');
                    $("#albumid").attr('value', 0);
                },
                error: function () {
                    layer.msg('Ajex调用出错!请联系管理员');
                }
            });
        }
    });

$("#file_music").change(function () {
    var dataURL;
    var $img = $("#Frequency");


    var $file = $(this);
    var fileObj = $file[0];
    var windowURL = window.URL || window.webkitURL;

    if (fileObj && fileObj.files && fileObj.files[0]) {
        dataURL = windowURL.createObjectURL(fileObj.files[0]);
    }
    else {
        dataURL = $file.val();
        var imgObj = document.getElementById("Frequency");
        // 两个坑:
        // 1、在设置filter属性时，元素必须已经存在在DOM树中，动态创建的Node，也需要在设置属性前加入到DOM中，先设置属性在加入，无效；
        // 2、src属性需要像下面的方式添加，上面的两种方式添加，无效；
        imgObj.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
        imgObj.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = dataURL;
    }
    
    if (changeType(dataURL))
    {
    }
    else {
        alert("请上传MP3格式的音乐");
    }
})


//判断文件类型

function changeType(objFile) {

    var objtype = objFile.substring(objFile.lastIndexOf(".")).toLowerCase();

    var fileType = new Array(".mp3");//,".mp4"

    for (var i = 0; i < fileType.length; i++) {

        if (objtype == fileType[i]) {

            return false;

            break;

        }

    }

    return true;

}
