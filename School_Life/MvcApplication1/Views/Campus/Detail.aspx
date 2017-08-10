<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage<Model.Goods>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	商品详情
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        
        /****************************/
        .goodsdetail{background-color:#fff;}
        .goodsdetail .thumbnail img{max-width:200px;}
        .goodsdetail .goodstitle{font: 700 16px Arial,"microsoft yahei";
                            color: #666;
                            padding-top: 10px;
                            line-height: 28px;
                            margin-bottom: 5px;}
        .goodsdetail .price
        {
        	color: #E4393C;
        	font-size: 22px;
            font-family: "microsoft yahei";
            margin-right: 5px;
        	}
        .goodsdetail .des img{max-width:910px;}
        .goodsdetail .des{overflow:hidden;}
        .goodsdetail .des ul
        {
        	border-bottom: 1px solid #e4393c;
        	}
        .goodsdetail .des ul.nav-tabs li a
        {
        	background-color: #e4393c;
            color: #fff;
            border: 1px solid #e4393c;
            cursor: default;
        	}
        .goodsdetail .note{color:#ff0000;}
    </style>
    <div id="top"></div>
    <div class="row col-md-12 goodsdetail">
        <div class="row col-md-12 media">
            <div class="media-left">
                <span class="thumbnail">
                    <img src="<%=Session["root"] %><%= Html.Encode(Model.thumb) %>.jpg" alt="缩略图" />
                </span>
            </div>
            <div class="media-body">
                <strong class="media-heading goodstitle"><%= Html.Encode(Model.title) %></strong>
				<p class="note"><%= Html.Encode(Model.note) %></p>
                <p>品牌：<%= Html.Encode(Model.brandid.brandname) %></p>
                <p>价格：<span class="price"><%= Html.Encode(String.Format("{0:F}", Model.price)) %></span></p>
                <p><a href="tencent://Message/?Uin=<%= Html.Encode(Model.Student.QQ) %>&amp;websiteName=www.js-css.cn=&amp;Menu=yes" class="btn btn-lg btn-primary">
                    联系货主 QQ:<%= Html.Encode(Model.Student.QQ) %>
                </a></p>
            </div>
        </div>
            <div class="row col-md-12 des">
                <ul class="nav nav-tabs">
                  <li class="active"><a>商品介绍</a></li>
                </ul>
				<p><%= MvcHtmlString.Create(Model.detail) %></p>
            </div>
    </div>

</asp:Content>

