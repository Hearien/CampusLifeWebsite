<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campusAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Common.Pager<Hashtable>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	商品分类管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var currentPage = <%=Model.getCurrentPage() %>
        var totalPage = <%=Model.getTotalPage() %>
    </script>
    <script src="../../Scripts/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap-paginator.js" type="text/javascript"></script>
    <script src="../../Scripts/adminGoodsCat.js" type="text/javascript"></script>

    <div class="row col-md-12">
        <ul id="mytab" class="nav nav-tabs">
			<li class="active">
				<a href="#list" data-toggle="tab">分类列表</a>
			</li>
			<li>
				<a href="#add" data-toggle="tab">添加分类</a>
			</li>
		</ul>
    </div>
    <div class="tab-content">
        <div class="tab-pane col-md-12 active" id="list">
            <table class="table table-bordered table-hover table-striped text-center">
			    <tr>
				    <td>分类名称</td>
				    <td>简介</td>
				    <td>状态</td>
                    <td>操作</td>
			    </tr>
				    <% foreach (var item in Model.getList()){ %>
					    <tr>
						    <td><%=Html.Encode(item["goods_catNam"])%></td>
                            <td><%=Html.Encode(item["catdesc"])%></td>
                        
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
                                <input type="hidden" value="<%=Html.Encode(item["goods_catId"])%>" />
							   
							    <a class="delgoodscat" href='javascript:void(0);'>删除</a>
						    </td>
					    </tr>
				    <%} %>
		    </table>
            <div class="text-center">
			    <ul id="pager"></ul>
		    </div>
        </div>
        <div class="tab-pane col-md-12" id="add">
            <form role="form" action="/Admin/InsertGoodCat" method="post">
                <div class="row col-md-12 form-group text-right">
                    <label class="col-md-2" for="catnm">名称</label>
                    <div class="col-md-9">
                        <input class="form-control" type="text" name="catnm" id="catnm" />
                    </div>
                </div>
                <div class="row col-md-12 form-group text-right">
                    <label class="col-md-2" for="catdes">描述</label>
                    <div class="col-md-9">
                        <textarea class="form-control" name="catdes" id="catdes" cols="8" rows="8"></textarea>
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
