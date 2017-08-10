<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage<Model.Student>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MyInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/bootstrap-filestyle.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function getObjectURL(file) {
            var url = null;
            if (window.createObjectURL != undefined) { // basic
                url = window.createObjectURL(file);
            } else if (window.URL != undefined) { // mozilla(firefox)
                url = window.URL.createObjectURL(file);
            } else if (window.webkitURL != undefined) { // webkit or chrome
                url = window.webkitURL.createObjectURL(file);
            }
            return url;
        }
        $(function () {
            $("#up").change(function () {
                var objUrl = getObjectURL(this.files[0]);
                console.log("objUrl = " + objUrl);
                if (objUrl) {
                    $("#shead").attr("src", objUrl);
                }
            });

        });
    </script>
    <fieldset class="row col-md-12">
        <legend>个人信息</legend>
        <form class="form-inline" action="/Campus/UpdateInfo" role="form" method ="post" enctype ="multipart/form-data">
            <div class="col-md-6">
                <div class="form-group col-md-12" style="margin:10px 0">
                    学号
                    <input type="text" name="stuno" class="form-control" value="<%= Html.Encode(Model.sno) %>" readonly/>
                </div>
                <div class="form-group col-md-12" style="margin:10px 0">
                    姓名
                    <input type="text" name="stuname" class="form-control" value="<%= Html.Encode(Model.sname) %>" />
                </div>
                <div class="form-group col-md-12" style="margin:10px 0">
                    QQ
                    <input type="text" name="stuqq" class="form-control" value="<%= Html.Encode(Model.QQ) %>" />
                </div>
                <div class="form-group col-md-12" style="margin:10px 0">
                    地址
                    <input type="text" name="stuaddr" class="form-control" value="<%= Html.Encode(Model.address) %>" />
                </div>
             </div>
             <div class="col-md-6">
                <div class="col-md-12">
                    <img id="shead" class="img-responsive img-circle pull-left" src="<%=Html.Encode(Model.head) %>" width="100" height="100" />
                </div>
                <div class="col-md-12">
                    <input type="file" name="stuhead" multiple="multiple" id="up" class="filestyle" data-buttonName="btn-success" data-buttonText="选择图片" />
                </div>
             </div>
            <div class="form-group col-md-12" style="margin:10px 0">
                <center>
                <input type="hidden" name="stuid" value="<%= Html.Encode(Model.id) %>" />
                <button type="submit" class="btn btn-primary"> 修 改 </button>
                </center>
            </div>
        </form>
    </fieldset>

</asp:Content>

