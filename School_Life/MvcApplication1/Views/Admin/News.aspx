<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campusAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Common.Pager<Hashtable>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	News
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        var currentPage = <%=Model.getCurrentPage() %>
        var totalPage = <%=Model.getTotalPage() %>
    </script>
    <script src="../../Scripts/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap-paginator.js" type="text/javascript"></script>
    <script src="../../Scripts/adminNews.js" type="text/javascript"></script>

    <div class="row col-md-12">
           <div class="panel panel-default">
		    <div class="panel-heading text-left">搜索</div>
		    <div class="panel-body">
				<div class="row">
					<div class="col-md-4">
						<label for="title">新闻标题</label>
						<input type="text" id="title" class="form-control" style="margin-left: 10px;width:63%;display:inline-block;" name="title" placeholder="请输入标题内容" />
					</div>
                    <div class="col-md-6">
						<label>发布时间</label>
						<input type="text" readonly="readonly" id="fdate" class="form-control" style="margin-left: 10px;width:30%;display:inline-block;" name="f_date" />&nbsp;至&nbsp;
						<input type="text" readonly="readonly" id="tdate" class="form-control" style="width:30%;display:inline-block;" name="t_date" />
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
				<td>标题</td>
				<td>类别</td>
				<td>作者</td>
				<td>时间</td>
                <td>操作</td>
			</tr>
				<% foreach (var item in Model.getList()){ %>
					<tr>
						<td><%=Html.Encode(item["newstitle"])%></td>
                        <td><%=Html.Encode(item["newscatnm"])%></td>
                        <td><%=Html.Encode(item["source"])%></td>
                        <td><%=Html.Encode(item["createtime"])%></td>
						<td>
                            <input type="hidden" value="<%=Html.Encode(item["newsid"])%>" />
							
							<a class="delnews" href='javascript:void(0);'>删除</a>
						</td>
					</tr>
				<%} %>
		</table>
        <div class="text-center">
			<ul id="pager"></ul>
		</div>
    </div>

</asp:Content>
