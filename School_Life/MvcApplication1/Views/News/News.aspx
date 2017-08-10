<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage<Common.Pager<Hashtable>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	News
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
                    window.location.href = "http://localhost:1188/News/NewsPager?pageCode=" + page;
                }
            }
        };
        $('#pager').bootstrapPaginator(params);
    });
    </script>
    <style type="text/css">
        .learnsite{background-color: #DBDCDC;padding-left:0;}
        .learnsite span
        {
        	display:inline-block;
        	background-color: #007354;
            color: #fff;
            line-height: 26px;
            text-indent: 15px;
            padding: 2px 20px;
        }
        .webright img{height:200px;}
        .webleft img{width:220px;}
        th
        {
        	background-color:#018C6A;
        	text-align:center;
        	color:#fff;
        	border:1px solid #fff;
        	line-height:28px;
        	font-size:18px;
        	}  
        td
        {
        	line-height:25px;
        	font-size:15px;
        	}
        	
        .th-right
        {
        	border-bottom-right-radius:10px;
        	border-top-right-radius:10px;
        	}
        .th-left
        {
            border-bottom-left-radius:10px;
        	border-top-left-radius:10px;	
        	}
        
        .cutspace{background-color: #018C6A;
                display: flex;
                border-radius: 5px;
                height: 30px;}
    </style>
    <div class="row col-md-12">
        <p class="col-md-12 learnsite"><span>学习网址推荐</span></p>
        <div class="col-md-8 webleft">
            <div class="row col-md-12 col-md-offset-1">
            <div class="col-md-6">
                <a class="thumbnail" href="http://www.imooc.com/" target="_blank"><img class="img img-responsive" src="../../img/news/IT.png" /></a>
            </div>
            <div class="col-md-6">
                 <a class="thumbnail" href="http://www.musiceol.com/" target="_blank"><img class="img img-responsive" src="../../img/news/music.png" /></a>
            </div>
            </div>
            <div class="row col-md-12 col-md-offset-1">
            <div class="col-md-6">
                 <a class="thumbnail" href="http://wenxue.yjbys.com/" target="_blank"><img class="img img-responsive" src="../../img/news/read.png" /></a>
            </div>
            <div class="col-md-6">
                 <a class="thumbnail" href="https://www.khanacademy.org" target="_blank"><img class="img img-responsive" src="../../img/news/English.png" /></a>
            </div>
            </div>
        </div>
        <div class="col-md-4 webright">
            <img class="img img-responsive" src="../../img/thought.png" />
        </div>
    </div>
    <!--新闻开始-->
    <table class="col-md-12 text-center">
        <tr>
            <th class="col-md-5 th-left">
                新闻标题
            </th>
            <th class="col-md-2 ">
                新闻类别
            </th>
            <th class="col-md-3 ">
                创建时间
            </th>
            <th class="col-md-2 th-right">
                作者
            </th>
            
        </tr>

    <% foreach (var item in Model.getList()) { %>
    
        <tr>
            <td class="text-left">
                <a href="Details?newsid=<%=Html.Encode(item["newsid"]) %>"><%= Html.Encode(item["newstitle"])%></a>
            </td>
            <td>
               <%= Html.Encode(item["newscatnm"])%>
            </td>
            <td>
                <%= Html.Encode(item["createtime"])%>
            </td>
            <td>
                <%= Html.Encode(item["source"])%>
            </td>
        </tr>
    
    <% } %>
    </table>
    <div class="col-md-12 cutspace"></div>
    <div class="text-center">
			<ul id="pager"></ul>
		</div>
</asp:Content>

