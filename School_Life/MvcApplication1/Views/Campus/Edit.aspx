<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage<Model.Goods>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	商品修改
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
    <div class="page-header" style="height:auto;">
        <h2>修改商品</h2>
    </div>
    <form class="form-horizontal" action="/Campus/UpdateGoods" method="post" role="form">
    <div class="col-md-12">
    <div class="col-md-8">
      <div class="form-group">
        <label for="title" class="col-md-2 control-label">标题</label>
        <div class="col-md-7">
            <input type="hidden" name="goodsid" value="<%= Html.Encode(Model.id) %>" />
          <input type="text" name="title" rename="title" class="form-control" id="title" value="<%= Html.Encode(Model.title) %>">
        </div>
        <div class="col-md-3">
            <span class=""></span>
            <span class="text-danger note"></span>
        </div>
      </div>
      <div class="form-group">
        <label for="goodscat" class="col-md-2 control-label">分类</label>
        <div class="col-md-7">
          <%=Html.DropDownList("goodcat", ViewData["goodcat"] as SelectList, "--请选择--", new { @class = "form-control", rename = "goodscat", id = "goodscat" })%>
        </div>
        <div class="col-md-3">
            <span class=""></span>
            <span class="text-danger note"></span>
        </div>
      </div>
      <div class="form-group">
        <label for="goodscat" class="col-md-2 control-label">品牌</label>
        <div class="col-md-7">
          <select name="brand" id="brand" class="form-control">
                    <option value="0">--请选择--</option>
          </select>
        </div>
        <div class="col-md-3">
            <span class=""></span>
            <span class="text-danger note"></span>
        </div>
      </div>
      <div class="form-group">
        <label for="note" class="col-md-2 control-label">提示信息</label>
        <div class="col-md-7">
          <input type="text" name="note" class="form-control" id="note" value="<%=Html.Encode(Model.note) %>">
        </div>
        <div class="col-md-3">
            <span class=""></span>
            <span class="text-danger note"></span>
        </div>
      </div>
      <div class="form-group">
        <label for="price" class="col-md-2 control-label">商品价格</label>
        <div class="col-md-7">
          <input type="text" class="form-control" rename="price" name="price" id="price" value="<%=Html.Encode(Model.price) %>">
        </div>
        <div class="col-md-3">
             <span class=""></span>
             <span class="text-danger note"></span>
        </div>
      </div>
      <div class="form-group">
        <label for="thum" class="col-md-2 control-label">上传缩略图</label>
        <div class="col-md-10">
            <input name="photo" id="photo" type="file" />
            <input type="hidden" name="gdthumb" value="<%=Model.thumb %>" />
        </div>
      </div>
    </div>
    <div class="col-md-4">
        <img src="<%=Session["root"] %><%= Html.Encode(Model.thumb) %>.jpg" id="preImg" width="200" class="img-thumbnail img-responsive" />
    </div>
    </div>
      <div class="form-group">
        <label for="detail" class="col-md-1 control-label">详情</label>
        <div class="col-md-11"> 
            <textarea id="Editor1" cols = "100", rows = "50" name="Editor1"><%=Model.detail %></textarea>
        </div>
        <div>
             <span class=""></span>
             <span class="text-danger note"></span>
        </div>
      </div>
      <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
          <div class="checkbox">
            <label>
              <input type="checkbox" checked="true" id="chk">同意并遵守<a href="#">商品规范</a>
            </label>
          </div>
        </div>
      </div>
      <div class="form-group">
        <div class="col-md-3 text-right">
          <button type="submit" class="btn btn-info">确认修改</button>
        </div>
      </div>
    </form>
    <style type="text/css">
        DIV.ProgressBar { width: 100px; padding: 0; border: 1px solid black; margin-right: 1em; height:.75em; margin-left:1em; display:-moz-inline-stack; display:inline-block; zoom:1; display:inline; }
        DIV.ProgressBar DIV { background-color: Green; font-size: 1pt; height:100%; float:left; }
        SPAN.asyncUploader OBJECT { position: relative; top: 5px; left: 10px; }
    </style>
    <script src="../../Scripts/validate.js" type="text/javascript"></script>
    <script src="../../Scripts/msg.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#photo").makeAsyncUploader({
                upload_url: "/Campus/AsyncUpload",
                flash_url: "/Scripts/swfupload.swf",
                button_image_url: "/img/blankButton.png",
                disableDuringUpload: "INPUT[type='submit']"
            });

            $("#goodscat").change(function () {
                var catNo = $("#goodscat").val();
                $.ajax({
                    type: "post",
                    async: true,
                    url: "/Campus/GetBrandList",
                    data: { cateory: catNo },
                    success: function (data) {
                        $("#brand").find("option").remove();
                        $("#brand").html(data);
                    }
                });

            });

            var flag = false;
            $("#title").blur(function () {
                flag = RegistVal(this);
            });
            $("#goodscat").blur(function () {
                flag = RegistVal(this);
            });
            $("#price").blur(function () {
                flag = RegistVal(this);
            });
            $("#chk").click(function () {
                flag = isChecked(this);
            });

            $("form").submit(function () {
                if (!flag) {
                    return false;
                } else if (!$("#title") || !$("#goodscat") || !$("#price") || !$("#chk")) {
                    return false;
                } else if ($("#title").val() == "" || $("#goodscat").val() == "" || $("#price").val() == "" || $("#chk").getAttribute("checked") == "false") {
                    return false;
                }
                return true;
            });

        });

    </script>

    <div>
        <%= Html.ActionLink("返回列表", "Mygoods") %>
    </div>

</asp:Content>

