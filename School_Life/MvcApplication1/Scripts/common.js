/**
* 登录验证
*/
function fun_login() {
    var username = document.getElementById("username");
    var userpwd = document.getElementById("userpwd");
    if (!username || username.value == "") {
        alert("please input username!");
        username.focus();
        return false;
    }
    if (!userpwd.value || userpwd.value == "") {
        alert("please input the password!");
        userpwd.focus();
        return false;
    }
    return true;
}