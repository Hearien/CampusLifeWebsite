<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campusAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Common.Pager<Hashtable>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dynamic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var currentPage = <%=Model.getCurrentPage() %>
        var totalPage = <%=Model.getTotalPage() %>
    </script>
    <script src="../../Scripts/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap-paginator.js" type="text/javascript"></script>
    <script src="../../Scripts/adminDynamic.js" type="text/javascript"></script>

    <style>
        .comment{
		    display:inline-block;
		    width:100%;
	    }
	    .comment span{
		    display:inline-block;
		    padding-right:20px;
		    font-weight:bold;
	    }
	    .close:hover{
		    color:red;
	    }
        .illuspic{height:100px;}
    </style>
    <div class="row col-md-12">
    <% foreach (var item in Model.getList()){ %>
        <div class="col-md-offset-3 form-group comments">
			    <div class="col-sm-8">
			    	<div class="panel panel-default">
					    <div class="panel-heading">
					       	<div class="comment">
					       		作者：<span><%=Html.Encode(item["sname"])%></span>
					       		时间：<span><%=Html.Encode(item["createtime"])%></span>
                                <span class="close pull-right">X</span>
                                <input type="hidden" value="<%=Html.Encode(item["dynamicid"])%>" />
					       	</div>
					    </div>
					    <div class="panel-body">
					       	<%=MvcHtmlString.Create(item["context"].ToString())%>
					    </div>
					</div>
			    </div>
			  </div>
    <%} %>
    </div>
        <div class="text-center">
			<ul id="pager"></ul>
		</div>
</asp:Content>
