
var audio = document.getElementById('bfq');//播放器
bofangqi(); //初始化播放器位置
yinliang(); //初始化音量
function yinliang() {
	var yinliangs = $('.btns-yinliang'); //音量控制显示框
	yinliangs.css('height', $(window).height() * 0.15); //设置音量控制框初始高度
	yinliangs.css('top',-( parseInt($('.btns-yinliang').height()) + parseInt($('#play-btns-right').position().top-1))); //设置音量控制框的初始位置
	var default_volume_percentage = Math.ceil($('.btns-yinliang').height() * 0.8 * audio.volume); //获取当前音量应该在的位置 
	$('#yinliang-duan').css('height', default_volume_percentage); //当前音量高度
	$('#yinliang-btn').css('bottom', default_volume_percentage - $('#yinliang-btn').height() / 2); //设置当前音量按钮应该在的位置
}

var ply = document.getElementById('btns-ply'); //获取开始按钮

ply.onclick = function ()
{
    play();
}
function play() { //开始按钮点击事件
	if(audio.paused) {
		$('#btns-ply').addClass('btns-ply-No');
		$('#btns-ply').removeClass('btns-ply-Yes');
		audio.play(); //播放音乐
		dangqianbofang(); //时间计时器开始移动
	} else {
	    guan();
	}
}
function guan() {
    $('#btns-ply').addClass('btns-ply-Yes');
    $('#btns-ply').removeClass('btns-ply-No');
    audio.pause(); // 这个就是暂停
    clearInterval(dangqianbofang.moveTimer);
}

audio.oncanplay = function() { //缓冲完成后
	shijian(); //初始化音乐总时间
}

$('.btns-ylbtn').click( //大音量按钮点击事件
	function() {
		var yl = $('.btns-yinliang');
		if(yl.css('display') == 'none') {
			yl.css('display', 'block');
		} else {
			yl.css('display', 'none');
		}
	}
)

function shijian() {
	var miao = Math.floor(audio.duration); //获取总时间 （秒）
	var fen = Math.floor(miao / 60); //计算分
	if(fen.toString().length < 2) {
		fen = '0' + fen;
	} //初始化分的格式
	miao = miao % 60; //计算秒
	if(miao.toString().length < 2) {
		miao = '0' + miao;
	} //初始化分的格式

	$('#shijian-zong').text(fen + ':' + miao); //显示时间
}

function shijianxianshi(dangqian) {//更改当前时间的
	var miao = 0;
	var fen = Math.floor(dangqian / 60); //计算分
	if(fen.toString().length < 2) {
		fen = '0' + fen;
	} //初始化分的格式
	miao = parseInt(dangqian % 60); //计算秒
	if(miao.toString().length < 2) {
		miao = '0' + miao;
	} //初始化分的格式
	$('#shijian-dangqian').text(fen + ':' + miao); //更新时间
}

function dangqianbofang() {
	this.moveTimer = setInterval(
		function() {
			var dangqianshijian = audio.currentTime;

			shijianxianshi(dangqianshijian);
			var tiaokuang = (dangqianshijian / audio.duration) * parseInt($('#kuang-tiao-chang').width());
			$('#kuang-tiao-duan').width(tiaokuang);
			var btn = document.getElementById('btn-style');

			btn.style.left = (btn.getAttribute('left') + tiaokuang) + 'px';
			if (audio.ended) {//判断音乐是否播放完成
			    guan();
			}
		}, 1000
	);
}

function bofangqi() {
	var g = $('#main-music-play');
	var h = g.width() / 2;
	g.css('margin-left', -h);
	audio.volume = $('#bfq').attr('_volume');
}


var divduan = document.getElementById("kuang-tiao-duan");
var chushi = document.getElementById("btn-style").offsetLeft;
var div = document.getElementById("btn-style");
div.onmousedown = function(ev) { //鼠标
	var e = window.event || ev;
	//var Mydiv=document.getElementById("div");
	//获取到鼠标点击的位置距离div左侧和顶部边框的距离；
	var oX = e.clientX;
	//var oY = e.clientY - div.offsetTop;

	var c = parseInt($('#btn-style').css('left'));
	//当鼠标移动，把鼠标的偏移量付给div
	document.onmousemove = function(ev) {
			//计算出鼠标在XY方向上移动的偏移量，把这个偏移量加给DIV的左边距和上边距，div就会跟着移动
			var e = window.event || ev;
			var pianti = e.clientX - oX; //计算偏移量
			if(((pianti + c) <= parseInt($('#kuang-tiao-chang').css('width'))) && (pianti + c) >= chushi) {
				musicTime(pianti + c); //调整时间
				div.style.left = pianti + c + "px"; //设置按钮的left
				divduan.style.width = pianti + c + "px"; //设置当前播放的宽度
			}
		}
		//当鼠标按键抬起，清除移动事件
	document.onmouseup = function() {
		document.onmousemove = null;
		document.onmouseup = null;
	}
}

function musicTime(timeWidth) { //设置当前播放时间
	timeWidth = parseInt(timeWidth);
	var tim = timeWidth / parseInt($('#kuang-tiao-chang').css('width')) * audio.duration;
	shijianxianshi(tim);
	audio.currentTime = tim;
}

var yinliangbtn = document.getElementById('yinliang-btn');
var dd = parseInt($('#yinliang-btn').css('bottom')); // style.bottom;
var btnkuan = parseInt($('#yinliang-btn').css('width')) / 2;
yinliangbtn.onmousedown = function(ev) { //鼠标
	var e = window.event || ev;

	var oY = e.clientY;
	//当鼠标移动，把鼠标的偏移量付给div
	var c = parseInt($('#yinliang-btn').css('bottom'));
	document.onmousemove = function(ev) {
			//计算出鼠标在XY方向上移动的偏移量，把这个偏移量加给DIV的左边距和上边距，div就会跟着移动
			var e = window.event || ev;
			var pianti = oY - e.clientY; //计算偏移量

			if(((pianti + c) <= $('#yinliang-chang').height() - btnkuan) && (pianti + c >= 0 - btnkuan)) {

			    musicYinLiang(pianti + c + btnkuan); //调整音量
				yinliangbtn.style.bottom = pianti + c + "px"; //设置按钮的bottm
				document.getElementById('yinliang-duan').style.height = pianti + c + btnkuan + "px"; //设置当前播放的长度
			}
		}
		//当鼠标按键抬起，清除移动事件
	document.onmouseup = function() {
		document.onmousemove = null;
		document.onmouseup = null;
	}
}

function musicYinLiang(TimeYin) {
	TimeYin = parseInt(TimeYin);
	var tim = TimeYin / parseInt($('#yinliang-chang').css('height'));
	if(tim <= 0) {
	    tim = 0;
	    $(".btns-ylbtn").css("background-position-x", "-106px");
	    $(".btns-ylbtn").css("background-position-y", "-65px");
	    
	} else if(tim > 1) {
		tim = 1;
	}
	else if(tim >0){
	    $(".btns-ylbtn").css("background-position", "0px -244px");
	}
	audio.volume = tim;
}


$('.btns-ylbtn').mouseover(
    function () {
        if (parseInt($(this).css('background-position-y'))<-200) {
            $(this).css('background-position-x', parseInt($(this).css('background-position-x')) - 29 + 'px');
        }
        else {
            $(this).css('background-position-x', parseInt($(this).css('background-position-x')) - 21 + 'px');
        }
    }
);
$('.btns-ylbtn').mouseout(
    function () {
        if (parseInt($(this).css('background-position-y')) < -200) {
            $(this).css('background-position-x', parseInt($(this).css('background-position-x')) + 29 + 'px');
        }
        else {
            $(this).css('background-position-x', parseInt($(this).css('background-position-x')) + 21 + 'px');
        }
    }
);