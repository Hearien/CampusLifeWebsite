<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage<Model.News>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/sinaFaceAndEffec.js" type="text/javascript"></script>
    <script src="../../Scripts/comment.js" type="text/javascript"></script>
    <link href="../../Content/sinaFaceAndEffec.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/comment.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/attention.js" type="text/javascript"></script>

    <style>
        
        /*回到顶部客服等*/
        .izl-rmenu{position:fixed; right:0; margin-left:532px; bottom:0; padding-bottom:0; background:url(/img/top/r_b.png) 0px bottom no-repeat; z-index:999; }
        .izl-rmenu .btn{width:72px; height:73px; margin-bottom:1px; cursor:pointer; position:relative;}
        .izl-rmenu .btn-qq{background:url(/img/top/r_qq.png) 0px 0px no-repeat; background-color:#6da9de;}
        .izl-rmenu .btn-qq:hover{background-color:#488bc7;}
        .izl-rmenu a.btn-qq,.izl-rmenu a.btn-qq:visited{background:url(/img/top/r_qq.png) 0px 0px no-repeat; background-color:#6da9de; text-decoration:none; display:none;}
        .izl-rmenu .btn-wx{display:none;background:url(/img/top/r_wx.png) 0px 0px no-repeat; background-color:#78c340;}
        .izl-rmenu .btn-wx:hover{background-color:#58a81c;}
        .izl-rmenu .btn-wx .pic{position:absolute; left:-160px; top:0px; display:none;width:160px;height:160px;}
        .izl-rmenu .btn-phone{display:none;background:url(/img/top/r_phone.png) 0px 0px no-repeat; background-color:#fbb01f;}
        .izl-rmenu .btn-phone:hover{background-color:#ff811b;}
        .izl-rmenu .btn-phone .phone{background-color:#ff811b; position:absolute; width:160px; left:-160px; top:0px; line-height:73px; color:#FFF; font-size:18px; text-align:center; display:none;}
        .izl-rmenu .btn-top{background:url(/img/top/r_top.png) 0px 0px no-repeat; background-color:#666666; display:none;}
        .izl-rmenu .btn-top:hover{background-color:#444;}
        
        .newsdetail .newstitle h1
        {
        	padding-bottom: 16px;
            font-family: '微软雅黑';
            font-size: 26px;
        	}
       .newsdetail .otherinfo{border-bottom: 1px dotted #ccc; margin:5px 0;}
        .newsdetail .otherinfo p{margin:5px 38px;}
       .newsdetail .newsdesc{background-color:#fff;}
    </style>
    <div id="top"></div>
    <div class="row col-md-12 newsdetail">
        <div class="row col-md-12 newstitle">
            <h1 class="text-center"><%= Html.Encode(Model.newstitle) %></h1>
        </div>
        <div class="row col-md-12 otherinfo">
            <p class="col-md-10">
                <span><%= Html.Encode(Model.source) %> <%= Html.Encode(Model.createtime) %></span>
                <span class="pull-right">点击量：<span class="badge">78</span></span>
           </p>
        </div>
        <div class="row col-md-12 newsdesc"><%=MvcHtmlString.Create(Model.newsdetsc.ToString().Trim().Replace("&middot;", "·").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rdquo;", "‘").Replace("&rsquo;", "’"))%></div>
        <div class="row col-md-12">
        <p class="text-right pull-right">
                <span class="bdsharebuttonbox">
                <a href="#" class="bds_more" data-cmd="more"></a>
                <a href="#" class="bds_qzone" data-cmd="qzone"></a>
                <a href="#" class="bds_tsina" data-cmd="tsina"></a>
                <a href="#" class="bds_tqq" data-cmd="tqq"></a>
                <a href="#" class="bds_renren" data-cmd="renren"></a>
                <a href="#" class="bds_weixin" data-cmd="weixin"></a>
                </span>
        <script>
            window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "", "bdMini": "2", "bdPic": "", "bdStyle": "0", "bdSize": "16" }, "share": {}, "image": { "viewList": ["qzone", "tsina", "tqq", "renren", "weixin"], "viewText": "分享到：", "viewSize": "16" }, "selectShare": { "bdContainerClass": null, "bdSelectMiniList": ["qzone", "tsina", "tqq", "renren", "weixin"]} }; with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];
        </script>
        </p>
        </div>
        <div class="col-md-12">
                <div class="content" style="margin-left:0;">
				    <div class="cont-box">
					    <%if (Session["sno"] == null)
                  {%>
					<textarea name="sayscom" class="text" placeholder="请登陆后发表" readonly></textarea>
                <%} %>
                <%else
                  { %>
                  <textarea name="sayscom" class="text" placeholder="请输入..."></textarea>

                <%} %>
				    </div>
				    <div class="tools-box">
					    <div class="operator-box-btn"><span class="face-icon"  >☺</span></div>
					    <div class="submit-btn"><input type="button" id="subCom" value="提交评论" /></div>
				    </div>
			    </div>
                <div class="col-md-12" id="info-show">
                    <ul></ul>
                </div>
        </div>
    </div>
    
    <script type="text/javascript">

        var html;
	    function reply(content,headImg,date,username){
		    html  = '<li>';
		    html += '<div class="head-face">';
		    html += '<img src="'+headImg+'" / >';
		    html += '</div>';
		    html += '<div class="reply-cont">';
		    html += '<p class="username">'+username+'</p>';
		    html += '<p class="comment-body">'+content+'</p>';
		    html += '<p class="comment-footer">'+date+'　回复　点赞54　转发12</p>';
		    html += '</div>';
		    html += '</li>';
		    return html;
	    }
        
        $(function () {
            // 绑定表情
            $('.face-icon').SinaEmotion($('.text'));

            $("#subCom").click(function () {
                $.ajax({
                    type: "post",
                    url: "AddCom",
                    data: "newsId="+<%= Html.Encode(Model.newsid) %>+"&comDesc="+$(".text").val()+"&sno="+<%=Session["sno"] %>,
                    success: function (data) {
                        data = eval("(" + data + ")");
                        console.log(data);
                        var inputText = data.com["comdesc"];
                        var headImg = data.com["uerid"].head;
                        var date = data.com["comtime"];
                        var uname = data.com["uerid"].sname;
                        $('#info-show ul').append(reply(AnalyticEmotion(inputText),headImg,date,uname));
                    }
                });
            });
        });
    </script>
</asp:Content>

