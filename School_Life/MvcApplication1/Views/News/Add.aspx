<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campusAdmin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	新闻添加
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../../Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
    <script src="../../Scripts/swfupload.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-asyncUpload-0.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            // 创建编辑器
            var editor = CKEDITOR.replace('Editor1');
            // 为编辑器绑定"上传控件"
            CKFinder.setupCKEditor(editor, '/Scripts/ckfinder/');
        };

        //上传成功以后执行该方法
        function Show(file, serverData) {
            var s = serverData.split(':'); //接收从服务端返回的数据，按照分号分隔

            if (s[0] == "ok") {
                $("#preImg").attr("src", s[1]);
            }
        }
    </script>
    <div class="page-header">
        <h4>新闻添加</h4>
    </div>
    <form class="form-horizontal" action="/News/Add" method="post" role="form">
        <div class="row col-md-12">
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">新闻标题</label>
                <div class="col-md-7">
                  <input type="text" name="title" rename="title" class="form-control" id="title" placeholder="请输入商品标题" />
                </div>
                <div class="col-md-3">
                    <span class=""></span>
                    <span class="text-danger note"></span>
                </div>
              </div>
              <div class="form-group">
                <label for="newscat" class="col-md-2 control-label">分类</label>
                <div class="col-md-7">
                  <%=Html.DropDownList("newscat", ViewData["newscat"] as SelectList, "--请选择--", new { @class = "form-control", rename = "newscat", id = "newscat" })%>
                </div>
                <div class="col-md-3">
                    <span class=""></span>
                    <span class="text-danger note"></span>
                </div>
              </div>
              <div class="form-group">
                <label for="source" class="col-md-2 control-label">来源</label>
                <div class="col-md-7">
                  <input type="text" name="source" rename="source" class="form-control" id="source" placeholder="请输入新闻来源" />
                </div>
                <div class="col-md-3">
                    <span class=""></span>
                    <span class="text-danger note"></span>
                </div>
              </div>
              <div class="form-group">
                <input type="checkbox" value="0" name="head" id="head" class="checkbox col-md-1 col-md-push-2"  /><label class="col-md-2" for="head">头条新闻</label>
                <input type="checkbox"  value="0" name="hot" id="hot" class="checkbox col-md-1 col-md-push-2"  /><label class="col-md-5" for="hot">热点新闻</label>
              </div>
              <div class="form-group">
                <div class="col-md-8">
                    <%=Html.TextArea("Editor1", new {cols = "30", rows = "50" })%>
                </div>
                <div>
                     <span class=""></span>
                     <span class="text-danger note"></span>
                </div>
              </div>
              <div class="form-group">
              <button type="submit" class="btn btn-primary col-md-offset-4">添加</button>
              </div>
        </div>
    </form>
    <script src="../../Scripts/validate.js" type="text/javascript"></script>
    <script src="../../Scripts/msg.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var flag = false;
            $("#title").blur(function () {
                flag = RegistVal(this);
                if (!flag) {
                    $(this).focus();
                }
                
            });
            $("#source").blur(function () {
                flag = RegistVal(this);
                if (!flag) {
                    $(this).focus();
                }
            });
            $("#head").click(function () {
                if ($(this).attr("value") == "0") {
                    $(this).attr("value", "1");
                } else {
                    $(this).attr("value", "0");
                }
            });
            $("#hot").click(function () {
                if ($(this).attr("value") == "0") {
                    $(this).attr("value", "1");
                } else {
                    $(this).attr("value", "0");
                }
            });
        });
    </script>
</asp:Content>
