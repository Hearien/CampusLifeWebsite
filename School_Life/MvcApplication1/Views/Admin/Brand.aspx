<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campusAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Common.Pager<Hashtable>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	商品品牌管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var currentPage = <%=Model.getCurrentPage() %>
        var totalPage = <%=Model.getTotalPage() %>
    </script>
    <script src="../../Scripts/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap-paginator.js" type="text/javascript"></script>
    <script src="../../Scripts/adminBrand.js" type="text/javascript"></script>

    <div class="row col-md-12">
        <ul id="mytab" class="nav nav-tabs">
			<li class="active">
				<a href="#list" data-toggle="tab">品牌列表</a>
			</li>
			<li>
				<a href="#add" data-toggle="tab">添加品牌</a>
			</li>
		</ul>
    </div>
    <div class="tab-content">
        <div class="tab-pane col-md-12 active" id="list">
            <table class="table table-bordered table-hover table-striped text-center">
			    <tr>
				    <td>品牌名称</td>
				    <td>所属分类</td>
				    <td>状态</td>
                    <td>操作</td>
			    </tr>
				    <% foreach (var item in Model.getList()){ %>
					    <tr>
						    <td><%=Html.Encode(item["brandname"])%></td>
                            <td><%=Html.Encode(item["goods_catNam"])%></td>
                        
                                <%  var state = "";
                                    if (item["isenable"].ToString() == "1")
                                    {
                                        state = "可用";
                                    }else if(item["isenable"].ToString() == "0"){
                                        state="已禁用";
                                    }
                                  %>
                            <td><%=state %></td>
						    <td>
                                <input type="hidden" value="<%=Html.Encode(item["grandid"])%>" />
							    <a class="delbrand" href='javascript:void(0);'>删除</a>
						    </td>
					    </tr>
				    <%} %>
		    </table>
            <div class="text-center">
			    <ul id="pager"></ul>
		    </div>
        </div>
        <div class="tab-pane col-md-12" id="add">
            <form role="form" action="/Admin/InsertBrand" method="post">
                <div class="row col-md-12 form-group text-right">
                    <label class="col-md-2" for="brandNm">名称</label>
                    <div class="col-md-9">
                        <input class="form-control" type="text" name="brandNm" id="brandNm" />
                    </div>
                </div>
                <div class="row col-md-12 form-group text-right">
                    <label class="col-md-2" for="catdes">所属分类</label>
                    <div class="col-md-9">
                        <%=Html.DropDownList("goodcat", ViewData["goodcat"] as SelectList, "--请选择--", new { @class = "form-control", rename = "goodscat", id = "goodscat" })%>
                    </div>
                </div>
                <div class="row col-md-12 form-group text-right">
                    <label class="col-md-2" for="catdes">状态</label>
                    <div class="col-md-9">
                        <select class="form-control" name="state">
                            <option value="0">禁用</option>
                            <option value="1">启用</option>
                        </select>
                    </div>
                </div>
                <div class="row col-md-12 form-group">
                    <button type="submit" class="btn btn-primary col-md-offset-3">添 加</button>
                </div>
            </form>
        </div>
    </div>

</asp:Content>
