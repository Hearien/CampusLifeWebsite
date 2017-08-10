<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
二师校园-首页
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .recentnews{overflow:hidden;}
        .recentnews p
        {
        	text-align:right;
        	border-radius: 10px 0 0 10px;
            display: flex;
            background-color: #CCCCCC;
            margin: 20px 17px 15px;
        	}
        .recentnews p span
        {
        	background-color: #007354;
            color: #fff;
            line-height: 26px;
            text-indent: 15px;
            padding: 2px 20px;
            margin-left:0;
        	}
        .recentnews p .more
        {
        	margin-right:0;
        	line-height: 27px;
            padding: 0 10px;
            font-size: 12px;
            font-weight: bold;
            text-align: right;
        	}
        	
        /*********************榜样滚动*****************************/
        #slide{overflow:hidden;width:600px;margin:0 23px;}
        ul{list-style:none;}
        #slide ul li ul li{float:left;padding:0 2px;}
        .slideul1{width:3999px;}
    </style>

    <div class="row col-md-9">
        
        <div class="col-md-12 recentnews">
            <p>
                <span>最近新闻</span>
                <a class="pull-right more">MORE+</a>
            </p>
            <div class="col-md-12">
                <%=Html.Partial("/Views/common/NewsItem.ascx", ViewData["news"])%>
            </div>
        </div>
        <div class="col-md-12">
            <div class="col-md-6">
                <img src="../../img/adindex.png" />
            </div>
            <div class="col-md-6">
                <img src="../../img/adindex2.png" />
            </div>
        </div>
        <div class="col-md-12">
            <%=Html.Partial("/Views/common/TopNews.ascx", ViewData["topnews"])%>
        </div>
        <div class="col-md-12 recentnews">
            <p>
                <span>榜样的力量</span>
                <a class="pull-right more">MORE+</a>
            </p>
            <script type="text/javascript">
                $(function () {
                var _speed = 30;
                var _slide = $("#slide");
                var _slideli1 = $(".slideli1");
                var _slideli2 = $(".slideli2");
                _slideli2.html(_slideli1.html());
                function Marquee() {
                    if (_slide.scrollLeft() >= _slideli1.width())
                        _slide.scrollLeft(0);
                    else {
                        _slide.scrollLeft(_slide.scrollLeft() + 1);
                    }
                }
                
                    //两秒后调用
                    var sliding = setInterval(Marquee, _speed)
                    _slide.hover(function () {
                        //鼠标移动DIV上停止
                        clearInterval(sliding);
                    }, function () {
                        //离开继续调用
                        sliding = setInterval(Marquee, _speed);
                    });
                });
            </script>
            <div id="slide" class="scroller_roll col-md-12">
                <ul class="slideul1"> 
		            <li class="slideli1"> 
      		            <ul class="slideul2"> 
				            <li><img src="../../img/student/s1.png" height="120"/></li> 
				            <li><img src="../../img/student/s2.png" height="120"/></li>
				            <li><img src="../../img/student/s3.png" height="120"/></li>
                            <li><img src="../../img/student/s4.png" height="120"/></li>
                            <li><img src="../../img/student/s5.png" height="120"/></li>
                            <li><img src="../../img/student/s6.png" height="120"/></li>
                            <li><img src="../../img/student/s7.png" height="120"/></li>
                            <li><img src="../../img/student/s8.png" height="120"/></li>
			            </ul> 
                    </li>
		            <li class="slideli2"></li> 
	            </ul> 
            </div>
        </div>
        
    </div>
    <div class="col-md-3">
        <%=Html.Partial("/Views/common/RightSide.ascx", ViewData["goods"])%>
    </div>
</asp:Content>
