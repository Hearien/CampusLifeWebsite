<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage<Common.Pager<Hashtable>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Goods
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/bootstrap-paginator.js" type="text/javascript"></script>
        <script type="text/javascript">
            var currentPage = <%=Model.getCurrentPage() %>;
            var totalPage = <%=Model.getTotalPage() %>;
        $(function(){
            var params = {
                bootstrapMajorVersion: 3, //版本
                currentPage: currentPage, //当前页码
                totalPages: totalPage, 	//总共多少页
                numberOfPages: 5, 	//提供5个页码供选择
                tooltipTitles: function (type, page, current) {
                    switch (type) {
                        case "first":
                            return "首页";
                        case "prev":
                            return "上一页";
                        case "next":
                            return "下一页";
                        case "last":
                            return "末页";
                        case "page":
                            return "第" + page + "页";
                    }
                },
                onPageClicked: function (event, originalEvent, type, page) {
                    if (page != currentPage) {
                        window.location.href = "http://localhost:1188/Campus/GoodsPager?pageCode=" + page;
                    }
                }
            };
            $('#pager').bootstrapPaginator(params);
        });
    </script>
    <style type="text/css">
        .right p{
	        width:100%;
	        height:30px;
	        background-color: #ccc;
	        box-sizing: border-box;
	        border-radius: 5px;
	        display: flex;
	        padding-left:0;
        }
        .right p span{
	        text-indent: 0;
	        text-align: center;
	        display: inline-block;
	        box-sizing: border-box;
	        border-radius: 5px;
	        background-color:#007354;
	        color:#fff;
	        padding:2px 20px;
	        line-height:26px;
	        flex:1;
        }
        .right p a{	
	        display: inline-block;
	        text-align: right;
	        padding: 6px 16px 0 0;
	        font-size: 12px;
	        font-weight: bold;
	        flex: 9;
	        color: #5d5e5e;
            text-decoration: none;
            font-family: "微软雅黑"
        }
        .good ul
        {
        	border: 1px solid #DBDCDC;
            box-shadow: 5px 5px 4px #999999;
        	}
        .good ul li a img{height:200px;width:200px;}
    </style>
    <div class="row col-md-12 right">
        <p class="col-md-12"><span>RECENT</span><a href="#">MORE+</a></p>
        <% foreach (var item in Model.getList()) { %>
        <div class="col-md-3 good">
            <ul>
                <li>
                    <a href="Detail?goodid=<%= Html.Encode(item["goodsid"]) %>">
                        <img class="img img-responsive" src="<%=Session["root"] %><%= Html.Encode(item["thumb"]) %>.jpg" alt="缩略图" />
                    </a>
                </li>
                <li><%= Html.Encode(item["title"].ToString().Length > 13 ? item["title"].ToString().Substring(0, 13) : item["title"])%></li>
                <li><%= Html.Encode(String.Format("{0:F}", item["price"]))%></li>
                <li><%= Html.Encode(String.Format("{0:g}", item["upTime"]))%></li>
            
            </ul>
        </div>
    <% } %>
        
    </div>
   <div class="text-center">
		<ul id="pager"></ul>
	</div>
</asp:Content>

