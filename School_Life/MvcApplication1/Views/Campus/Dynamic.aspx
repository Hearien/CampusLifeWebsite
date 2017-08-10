<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage<List<Hashtable>>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dynamic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/main.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/sinaFaceAndEffec.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/sinaFaceAndEffec.js" type="text/javascript"></script>
    <script src="../../Scripts/comment.js" type="text/javascript"></script>
    <script src="../../Scripts/swfupload.js" type="text/javascript"></script>
    <style>
        .dynamicdetail{color: #403e3b;
                       background-color: #e6e1d3;
                       font-size:15px;
                       font-family: punctuation,"PingFangSC-Regular","Microsoft Yahei","sans-serif";
                       padding:0 5px;
                       margin:10px 40px;}
        .dynamicdetail .creatm{color: #99958d;}
        .dynamicdetail .sname{font-size:25px;}
        .dynamictext .illuspic{max-width:400px;display:block;margin:0;}
        .dynamicdetail .browinfo{margin:8px 0;}
        .dynamicdetail .browinfo div:last-of-type{text-align:right}
        .dynamicdetail .browinfo div:last-of-type a
        {
        	color: #8c8262;
        	font-size: 20px;
        	display: inline-block;
            margin-left: 55px;
            cursor: pointer;
            opacity:0.7;
            text-decoration:none;
        	}
        .dynamicdetail .browinfo div:last-of-type a:hover{opacity:1;}
    </style>
    <div class="row col-md-9">
        <div class="dynamicBox">
           <% using (Html.BeginForm("Anounce", "Campus", FormMethod.Post, new { id = "uploadForm", enctype = "multipart/form-data" }))
              {  %>
                <input name="context" type="hidden" value="" />
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
				<div class="img_div">
				</div>
				<div class="tools-box">
					<div class="operator-box-btn"><span class="face-icon"  >☺</span>
					<a href="javascript:;" class="a-upload">
							<span class="glyphicon glyphicon-picture img-ico">
								<input type="file" name="myFile" id="myFile" />
							</span>
                            <span ></span>
						</a>
					</div>
					<div class="submit-btn pull-right">
                        <%if (Session["sno"] == null)
                          {%>
					        <input id="Submit1" type="submit" value="发表" disabled />
                        <%} %>
                        <%else
                          { %>
                          <input id="btnSub" type="submit" value="发表" />

                        <%} %>
                        
                    </div>
				</div>
            <%} %>
		</div>
        <div class="row col-md-12">
            <% foreach (var item in Model) {%>

              <div class="row col-md-11 dynamicdetail">

                <div class="col-md-2">
                    <img class="img-responsive img-circle" width="70" src='<%=Html.Encode(item["head"])%>' />
                </div>
                <div class="col-md-8">
                    <div class="col-md-12 sname">
                        <%=Html.Encode(item["sname"])%>
                    </div>
                    <div class="col-md-12 creatm">
                        <%=Html.Encode(item["createtime"])%>
                    </div>
                </div>
                <div class="col-md-12 text-left dynamictext">
                    <%=MvcHtmlString.Create(item["context"].ToString())%>
                        
                </div>
                <div class="row col-md-12 browinfo">
                    <div class="col-md-6 text-left">
                        <% Random rd = new Random(); %>
                        浏览<span class="badge"><%=rd.Next(100) %></span>次
                    </div>
                    <div class="col-md-6">
                        <a class="glyphicon glyphicon-thumbs-up"></a>
                        <a class="glyphicon glyphicon-comment"></a>
                        <a class="glyphicon glyphicon-share-alt"></a>
                    </div>
                </div>
              </div>

            <%} %>


        </div>
    </div>
    <div class="col-md-3">
        <%=Html.Partial("/Views/common/RightSide.ascx", ViewData["goods"])%>
    </div>
 
 <script type="text/javascript">

     $(function () {
         $("#uploadForm").submit(function () {
             var text = AnalyticEmotion($(".text").val());
             console.log(text);
             $("input[type='hidden']").attr("value", text);
             
             return true;
         });
     });
      
     var html;
     function reply(content) {
         html = '<div class="reply-cont">';
         html += '<p class="comment-body">' + content + '</p>';
         html += '<p class="comment-footer">2016年10月5日　回复　点赞54　转发12</p>';
         html += '</div>';
         return html;
     }
     $(function () {
         var objUrl;
         var img_html;

         // 绑定表情
         $('.face-icon').SinaEmotion($('.text'));
         // 测试本地解析
         function out() {
             var inputText = $('.text').val();
             $('#info-show ul').append(reply(AnalyticEmotion(inputText)));
         }

         //打开图片
         $("#myFile").change(function () {
             var img_div = $(".img_div");
             var filepath = $("input[name='myFile']").val();
             for (var i = 0; i < this.files.length; i++) {
                 objUrl = getObjectURL(this.files[i]);
                 var extStart = filepath.lastIndexOf(".");
                 var ext = filepath.substring(extStart, filepath.length).toUpperCase();
                 /*
                 作者：z@qq.com
                 时间：2016-12-10
                 描述：鉴定每个图片上传尾椎限制
                 * */
                 if (ext != ".BMP" && ext != ".PNG" && ext != ".GIF" && ext != ".JPG" && ext != ".JPEG") {
                     $(".shade").fadeIn(500);
                     $(".text_span").text("图片限于bmp,png,gif,jpeg,jpg格式");
                     this.value = "";
                     $(".img_div").html("");
                     return false;
                 } else {
                     /*
                     若规则全部通过则在此提交url到后台数据库
                     * */
                     img_html = "<div class='isImg'><img src='" + objUrl + "' onclick='javascript:lookBigImg(this)' style='height: 100%; width: 100%;' /><button class='removeBtn' onclick='javascript:removeImg(this)'>x</button></div>";
                     img_div.append(img_html);
                 }
             }
             /*
             作者：z@qq.com
             时间：2016-12-10
             描述：鉴定每个图片大小总和
             * */
             var file_size = 0;
             var all_size = 0;
             for (j = 0; j < this.files.length; j++) {
                 file_size = this.files[j].size;
                 all_size = all_size + this.files[j].size;
                 var size = all_size / 1024;
                 if (size > 500000) {
                     $(".shade").fadeIn(500);
                     $(".text_span").text("上传的图片大小不能超过100k！");
                     this.value = "";
                     $(".img_div").html("");
                     return false;
                 }
             }
             return true;
         });
         /*
         作者：z@qq.com
         时间：2016-12-10
         描述：鉴定每个浏览器上传图片url 目前没有合并到Ie
         * */
         function getObjectURL(file) {
             var url = null;
             if (window.createObjectURL != undefined) { // basic
                 url = window.createObjectURL(file);
             } else if (window.URL != undefined) { // mozilla(firefox)
                 url = window.URL.createObjectURL(file);
             } else if (window.webkitURL != undefined) { // webkit or chrome
                 url = window.webkitURL.createObjectURL(file);
             }
             //console.log(url);
             return url;
         }
     });
	/*
	 作者：z@qq.com
	 时间：2016-12-10
	  描述：上传图片附带删除 再次地方可以加上一个ajax进行提交到后台进行删除
	 * */
	function removeImg(r){
		$(r).parent().remove();
    }

    
 </script>
</asp:Content>
