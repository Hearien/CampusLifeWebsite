<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	页面错误
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <style>
.w950{width:950px;margin:0px auto;}
.c_404{background:#fff; height:400px;margin-top:25px;}
.c_404 .baocu{ background:url('../../img/404_1.gif') no-repeat; width:500px;padding-left:130px;height:200px; 
float:left;margin-top:120px;margin-left:260px; display:inline;}
.c_404 .baocu span{ color:#ff6633;font-size:30px;  font-weight:bold; float:left;margin:20px 0px 0px 20px; 
display:inline;}
.c_404 .baocu  p{  font-size:18px; font-weight:bold; float:left;margin:20px 0px 0px 20px; display:inline;}
.c_404 .baocu  p a{ font-size:18px; font-weight:bold;color:#666; text-decoration:none;}
.c_404 .baocu  p a:hover{font-size:18px; font-weight:bold;color:#f00; text-decoration:underline;}
</style>
<body>
<div class="w950 c_404">
  <div class="baocu"><span>糟糕！网页无法访问</span><div class="clear"></div><p><a 
href="/Campus/Index">返回首页</a></p></div>

</div>

</asp:Content>
