<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campusAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Common.Pager<Hashtable>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	专业信息管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var currentPage = <%=Model.getCurrentPage() %>
        var totalPage = <%=Model.getTotalPage() %>
    </script>
    <script src="../../Scripts/bootstrap-paginator.js" type="text/javascript"></script>
    <script src="../../Scripts/adminMajor.js" type="text/javascript"></script>

    <div class="row col-md-12">
        <ul id="mytab" class="nav nav-tabs">
			<li class="active">
				<a href="#list" data-toggle="tab">专业列表</a>
			</li>
			<li>
				<a href="#add" data-toggle="tab">添加专业</a>
			</li>
		</ul>
    </div>
    <div class="tab-content">
        <div class="tab-pane col-md-12 active" id="list">
            <table class="table table-bordered table-hover table-striped text-center">
			    <tr>
				    <td>专业代号</td>
				    <td>专业名称</td>
                    <td>所属院系</td>
                    <td>操作</td>
			    </tr>
				    <% foreach (var item in Model.getList()){ %>
					    <tr>
						    <td><%=Html.Encode(item["majorNo"])%></td>
                            <td><%=Html.Encode(item["majorName"])%></td>
                            <td><%=Html.Encode(item["deptName"])%></td>
						    <td>
                                <input type="hidden" value="<%=Html.Encode(item["id"]) %>" />
							    <a class="delmajor" href='javascript:void(0);'>删除</a>
						    </td>
					    </tr>
				    <%} %>
		    </table>
            <div class="text-center">
			    <ul id="pager"></ul>
		    </div>
        </div>
        <div class="tab-pane col-md-12" id="add">
            <form role="form" action="/Admin/InsertMajor" method="post">
                <div class="row col-md-12 form-group text-right">
                    <label class="col-md-2" for="majorNo">专业代号</label>
                    <div class="col-md-9">
                        <input class="form-control" name="majorNo" id="majorNo" />
                    </div>
                </div>
                <div class="row col-md-12 form-group text-right">
                    <label class="col-md-2" for="majorNm">专业名称</label>
                    <div class="col-md-9">
                        <input class="form-control" name="majorNm" id="majorNm" />
                    </div>
                </div>
                <div class="row col-md-12 form-group text-right">
                    <label class="col-md-2" for="desc">所属院系</label>
                    <div class="col-md-9">
                        <%=Html.DropDownList("dept", ViewData["dept"] as SelectList, "--请选择--", new { @class = "form-control", id = "dept" })%>
                    </div>
                </div>
                <div class="row col-md-12 form-group">
                    <button type="submit" class="btn btn-primary col-md-offset-3">添 加</button>
                </div>
            </form>
        </div>
    </div>

</asp:Content>
