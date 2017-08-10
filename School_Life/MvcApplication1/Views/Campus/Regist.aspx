<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	校园生活-注册
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/msg.js" type="text/javascript"></script>
    <script src="../../Scripts/validate.js" type="text/javascript"></script>
 
    <form class="form-horizontal" role="form" action="/Campus/Regist" method="post">
	    <div class="form-group">
			<label for="stuid" class="col-sm-2 control-label">学号</label>
		    <div class="col-sm-6">
                <%=Html.TextBox("stuid", "", new { PlaceHolder = "请输入学号", @class = "form-control", id="stuNo", rename="stuNo" })%>
			    <!--<input type="text" class="form-control" id="stuid" />-->
		    </div>
            <div class="col-sm-3">
                <span id="sno-note-img" class=""></span>
                <span id="sno-note-info" class="text-danger note"></span>
            </div>
	    </div>
	    <div class="form-group">
			<label for="stuname" class="col-sm-2 control-label">昵称</label>
		    <div class="col-sm-6">
                <%=Html.TextBox("stuname", "", new { PlaceHolder = "请输入昵称", @class = "form-control", id = "stuname", rename = "sname" })%>
		    </div>
            <div class="col-sm-3">
                <span class=""></span>
                <span class="text-danger note"></span>
            </div>
	    </div>
        <div class="form-group">
			    <label for="stupwd" class="col-sm-2 control-label">密码</label>
		    <div class="col-sm-6">
                <%=Html.TextBox("stupwd", "", new { type = "password", PlaceHolder = "请输入密码", @class = "form-control", id="pwd1", rename = "pwd" })%>
		    </div>
            <div class="col-sm-3">
                <span class=""></span>
                <span class="text-danger note"></span>
            </div>
	    </div>
        <div class="form-group">
			    <label for="stupwd2" class="col-sm-2 control-label">重输密码</label>
		    <div class="col-sm-6">
                <%=Html.TextBox("stupwd2", "", new { type = "password", PlaceHolder = "请输入密码", @class = "form-control", id="pwd2" })%>
			    <!--<input type="password" class="form-control" id="stupwd2" />-->
		    </div>
            <div class="col-sm-3">
                <span class=""></span>
                <span class="text-danger note"></span>
            </div>
	    </div>
        <div class="form-group">
			    <label for="gender" class="col-sm-2 control-label">性别</label>
		    <div class="col-sm-2">
                <%=Html.TextBox("gender", "0", new { type = "radio",id="man", @class = "radio-inline" })%>
			    <!--<input type="radio" name="gender" value="0" class="radio-inline" id="man" />-->
                <label for="man">男</label>
		    </div>
            <div class="col-sm-2">
                <%=Html.TextBox("gender", "1", new { type = "radio",id="woman", @class = "radio-inline" })%>
			    <!--<input type="radio" name="gender" value="1" class="radio-inline" id="woman" />-->
                <label for="woman">女</label>
		    </div>
	    </div>
        <div class="form-group">
			<label for="stugrade" class="col-sm-2 control-label">年级</label>
		    <div class="col-sm-2">
                <%=Html.DropDownList("grade", ViewData["grade"] as SelectList, "--请选择--", new { @class = "form-control", id = "stugrade" })%>
		    </div>
            <label for="studept" class="col-sm-1 control-label">系别</label>
		    <div class="col-sm-2">
                <%=Html.DropDownList("dept", ViewData["dept"] as SelectList, "--请选择--", new { @class = "form-control", id = "studept" })%>
		    </div>
            <label for="stumajor" class="col-sm-1 control-label">专业</label>
		    <div class="col-sm-2">
			    <select name="major" id="stumajor" class="form-control">
                    <option value="0">--请选择--</option>
                </select>
		    </div>
	    </div>
        <div class="form-group">
			    <label for="QQ" class="col-sm-2 control-label">QQ</label>
		    <div class="col-sm-6">
			    <input type="text" name="QQ" class="form-control" id="QQ" placeholder="请输入QQ号码" rename="QQ" />
		    </div>
            <div class="col-sm-3">
                <span class=""></span>
                <span class="text-danger note"></span>
            </div>
	    </div>
        <div class="form-group">
			    <label for="address" class="col-sm-2 control-label">住址</label>
		    <div class="col-sm-6">
			    <input type="text" name="address" class="form-control" id="address" placeholder="请输入家庭住址" />
		    </div>
	    </div>
	    <div class="form-group">
		    <div class="col-sm-offset-2 col-sm-10">
			    <div class="checkbox">
					    <label><input type="checkbox" id="chk"/>同意并遵守<a href="#">校园生活规范</a></label>
			    </div>
		    </div>
	    </div>
	    <div class="form-group">
		    <div class="col-sm-offset-2 col-sm-10">
				    <button type="submit" class="btn btn-default" id="btnSign">Sign in</button>
		    </div>
	    </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $("#studept").change(function () {
                var Request = new Object();
                var deptNo = $("#studept").val();
                $.ajax({
                    type: "post",
                    async: true,
                    url: "/Campus/GetMajor",
                    data: { dept: deptNo },
                    success: function (msg) {
                        $("#stumajor").find("option").remove();
                        $("#stumajor").html(msg);
                    }
                });

            });

            var flag = false;
            $("#stuNo").blur(function () {
                flag = RegistVal(this);
                if (flag) {
                    $.ajax({
                        type: "post",
                        url: "/Campus/CheckSno",
                        data: "sno=" + $("#stuNo").val(),
                        success: function (data) {
                            data = eval("(" + data + ")");
                            if (data.msg == true) {
                                $("#sno-note-img").addClass("glyphicon glyphicon-exclamation-sign text-danger");
                                $("#sno-note-info").html("学号已存在，请重新填写");
                                $("#stuNo").focus();
                                flag = false;
                            } else {
                                $("#sno-note-info").html("");
                            }
                            console.log(data);
                            
                        }
                    });
                }
            });

            $("#stuname").blur(function () {
                flag = RegistVal(this);
            });

            $("#pwd1").blur(function () {
                flag = RegistVal(this);
            });

            $("#QQ").blur(function () {
                flag = RegistVal(this);
            });

            $("#pwd2").blur(function () {
                flag = isMatchPwd(this);
            });

            $("#chk").click(function () {
                flag = isChecked(this);
            });

            $("form").submit(function () {
                if (!flag) {
                    return false;
                }
                return true;
            });

        });
    </script>
</asp:Content>
