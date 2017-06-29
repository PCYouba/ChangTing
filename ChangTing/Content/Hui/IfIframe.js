if ((parent.window.location.host != window.location.host) && (top.window.location.href != window.location.href)) {
    top.window.location.href = '../Admin/index'
}else if(top==self){
    top.window.location.href = '../Admin/index';
}