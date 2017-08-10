<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campusAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Common.Pager<Hashtable>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var currentPage = <%=Model.getCurrentPage() %>
        var totalPage = <%=Model.getTotalPage() %>
    </script>
    <script src="../../Scripts/bootstrap-paginator.js" type="text/javascript"></script>
    <script src="../../Scripts/adminUser.js" type="text/javascript"></script>
    <div class="row col-md-12">
           <div class="panel panel-default">
		    <div class="panel-heading text-left">搜索</div>
		    <div class="panel-body">
				<div class="row">
					<div class="col-md-3">
						<label for="sno">学号</label>
						<input type="text" id="sno" class="form-control" style="margin-left: 10px;width:70%;display:inline-block;" name="sno" placeholder="请输入学号" />
					</div>
                    <div class="col-md-3">
						<label for="grade">年级</label>
                        <%=Html.DropDownList("grade", ViewData["grade"] as SelectList, "--请选择--", new { @class = "form-control", id = "grade", style="margin-left: 10px;width:70%;display:inline-block;" })%>
					</div>
                    <div class="col-md-4">
						<label for="major">专业</label>
                        <%=Html.DropDownList("major", ViewData["major"] as SelectList, "--请选择--", new { @class = "form-control", id = "major", style = "margin-left: 10px;width:70%;display:inline-block;" })%>
					</div>
					<div class="col-md-1">
						<button id="search" type="button" class="btn btn-primary">
						  <span class="glyphicon glyphicon-search"></span> 查询
						</button>
					</div>
				</div>
			</div>
		</div>
    </div>

    <div class="col-md-12">
        <table class="table table-bordered table-hover table-striped text-center">
			<tr>
				<td>学号</td>
				<td>姓名</td>
				<td>性别</td>
				<td>年级</td>
				<td>系别</td>
                <td>专业</td>
                <td>QQ</td>
                <td>家庭地址</td>
                <td>操作</td>
			</tr>
				<% foreach (var item in Model.getList()){ %>
					<tr>
						<td><%=Html.Encode(item["sno"])%></td>
                        <td><%=Html.Encode(item["sname"])%></td>
                        <td><%=Html.Encode(item["genderVal"])%></td>
                        <td><%=Html.Encode(item["grade"])%>级</td>
                        <td><%=Html.Encode(item["deptName"])%></td>
                        <td><%=Html.Encode(item["majorName"])%></td>
                        <td><%=Html.Encode(item["QQ"])%></td>
                        <td><%=Html.Encode(item["address"])%></td>
						<td>
                            <input type="hidden" value="<%=Html.Encode(item["sno"])%>" />
							<a class="uptuser" href='javascript:void(0);'>密码重置</a>
							<a class="deluser" href='javascript:void(0);'>删除</a>
						</td>
					</tr>
				<%} %>
		</table>
        <div class="text-center">
			<ul id="pager"></ul>
		</div>
    </div>

</asp:Content>
