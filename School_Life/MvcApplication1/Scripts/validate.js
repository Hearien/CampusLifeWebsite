//验证输入信息
function RegistVal(obj) {
    var rename = obj.getAttribute("rename");
    var reg_StuNo = new RegExp(msg[rename].reg);
    var doc = obj.parentElement.nextElementSibling.children;
    if (!obj || obj.value == "") {
        doc[0].className = "glyphicon glyphicon-exclamation-sign text-danger";
        doc[1].className = "text-danger";
        doc[1].innerHTML = msg[rename].nul;
        //obj.focus();
        return false;
    }
    if (!reg_StuNo.test(obj.value)) {
        doc[0].className = "glyphicon glyphicon-exclamation-sign text-danger";
        doc[1].className = "text-danger";
        doc[1].innerHTML = msg[rename].val;
        //obj.focus();
        return false;
    }
    doc[0].className = "glyphicon glyphicon-ok text-success";
    doc[1].innerHTML = "";
    return true;
}

//密码是否匹配
function isMatchPwd(obj) {
    var pwd1 = document.getElementById("pwd1");
    var pwd2 = document.getElementById("pwd2");
    var doc = obj.parentElement.nextElementSibling.children;
    if (!pwd2 || pwd2.value == "") {
        doc[0].className = "glyphicon glyphicon-exclamation-sign text-danger";
        doc[1].className = "text-danger";
        doc[1].innerHTML = "请重新输入密码";
        //obj.focus();
        return false;
    }
    if (pwd1.value != pwd2.value) {
        doc[0].className = "glyphicon glyphicon-exclamation-sign text-danger";
        doc[1].className = "text-danger";
        doc[1].innerHTML = "密码不匹配，请重新输入";
        //obj.focus();
        return false;
    }
    doc[0].className = "glyphicon glyphicon-ok text-success";
    doc[1].innerHTML = "";
    return true;
}


//是否同意注册
function isChecked(obj) {
    var chk = obj.getAttribute("checked");
    if (!chk || chk == "false") {
        obj.setAttribute("checked", "true");
        return true;
    } else {
        obj.setAttribute("checked", "false");
        return false;
    }
}