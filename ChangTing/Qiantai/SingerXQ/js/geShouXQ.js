
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
var num = 2;

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


	
	