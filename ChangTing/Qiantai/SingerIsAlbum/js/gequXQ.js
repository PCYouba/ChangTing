
var oUl = document.querySelector('.content-middle-music-lis');
var aList = document.querySelectorAll('.list-on-lis');
var aDate = document.querySelectorAll('.list-on-date');

for ( var i=0; i<oUl.children.length; i++ ) {
	
	if ( i % 2 == 0 )oUl.children[i].style.backgroundColor = '#f7f7f7'; 
	
	oUl.children[i].index = i;
	
	oUl.children[oUl.children[i].index].onmousemove = function() {
		
		aDate[this.index].style.display = 'none';
		
		aList[this.index].style.display = 'block';
		
	}
	oUl.children[oUl.children[i].index].onmouseout = function() {
		
		aList[this.index].style.display = 'none';
		
		aDate[this.index].style.display= 'block';
		
	}
	
}

var contentFenye = document.getElementById('content-fenye');
var fenyeLi = contentFenye.getElementsByTagName('li'); 
var num = 1;

function fnTab() {
	
	for ( var i=0; i<fenyeLi.length; i++ ) {
		
		if (num == 1) {
			fenyeLi[0].style.disabled = 'disabled';
			fenyeLi[0].style.color = '#cacaca';
		} else {
			fenyeLi[0].style.disabled = '';
			fenyeLi[0].style.color = '';
		}
	
		if( Number(fenyeLi[i].innerHTML) ) {
			
			fenyeLi[i].className = '';
			fenyeLi[i].index = i;
			
			fenyeLi[num].className = 'content-fenye-bg';
			
			fenyeLi[i].onmousemove = function() {
				this.style.background = 'red';
				this.style.color = '#fff';
			}
			
			fenyeLi[i].onmouseout = function() {
				this.style.background = '';
				this.style.color = '';
			}
			
		}
	
	}
	
}
fnTab();
for ( var i=0; i<fenyeLi.length; i++ ) {
	
	if( Number(fenyeLi[i].innerHTML) ) {
	
		fenyeLi[i].index = i;
		
		fenyeLi[i].onclick = function() {
			num = this.index;
			fnTab();
		}
	
	}
}

//字体输入多少 
var textareada = document.querySelector('.textareada');
var pinglun = document.querySelector('.music-numda');
var PLnum = pinglun.getElementsByTagName('span')[0];
var PLbtn = pinglun.getElementsByTagName('input')[0];

var textareaxiao = document.querySelector('.textareaxiao'); 
var pinglunxiao = document.querySelector('.music-numxiao');
var PLnumxiao = pinglunxiao.getElementsByTagName('span')[0];
var PLbtnxiao = pinglunxiao.getElementsByTagName('input')[0];


  
PDnum(textareada,PLnum,PLbtn);
PDnum(textareaxiao,PLnumxiao,PLbtnxiao);

function PDnum( object,plnum,plbtn ) {
	
	var num = 0;
	
	object.onfocus = function() { 
	
		timer = setInterval( function(){
		
			if (object.value != '') {
				num = object.value.length;
			}
			plnum.innerHTML = 140 - num;
			if ( plnum.innerHTML < 0 ) {
				plnum.style.color = 'red';
			} else {
				plnum.style.color = '';
			}
		
		}, 100 );
	
		plbtn.onclick = function() {
			if ( plnum.innerHTML < 0 ) {
				alert('输入不能超过140个字符');
			} 
		}
	}
	object.onblur = function() {
		clearInterval(timer);
	}
	
	
}



var HFparent = document.querySelectorAll('.section-text-footer-right');
var HFdisplay = document.querySelectorAll('.music-jingcaiPL-section-text-display');
var judge = true;
for ( var i=0; i<HFparent.length; i++ ) {
	HFparent[i].index = i;
	HFparent[i].children[2].onclick = function() {
		if ( judge ) {
			HFdisplay[this.parentNode.index].style.display = 'block';
			judge = false;
		} else{
			HFdisplay[this.parentNode.index].style.display = 'none';
			judge = true;
		}
		
	} 
}


 
	
	

	
	
	