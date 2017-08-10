//页面初始化方法
function init() {
    /*****************日期控件*******************/
    $("#fdate,#tdate").datetimepicker({
        format: "yyyy-mm-dd",
        autoclose: true, //是否自动关闭
        todayBtn: true, //是否包含今天按钮
        clearBtn: true,
        language: 'zh-CN', //语言
        pickerPosition: "bottom-left"
    });

    /*************************分页**************************/
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
                getNews(page);
            }
        }
    };
    $('#pager').bootstrapPaginator(params);

    /*************************toast******************************/
    toastr.options = {
        closeButton: false,
        debug: false,
        progressBar: false,
        positionClass: "toast-bottom-right",
        onclick: null,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "5000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut"
    };
}

function getNews(page) {
    var title = $("#title").val();
    var fDate = $("#fdate").val();
    var tDate = $("#tdate").val();
    var pageCode = page || "";
    $.ajax({
        type: "POST",
        data: "pageCode=" + pageCode + "&title=" + title + "&fDate=" + fDate + "&tDate=" + tDate,
        url: "/Admin/NewsPager",
        async: true,
        success: function (data) {
            $(".table tr:gt(0)").html("");
            data = eval("(" + data + ")");
            console.log(data);
            currentPage = data.pageCode;
            totalPage = data.totalPage;
            $('#pager').bootstrapPaginator("setOptions", { currentPage: currentPage, totalPages: totalPage });
            var news = data.news;
            var tr = "";
            for (var item in news) {
                tr += "<tr>";
                tr += "<td>" + news[item]["newstitle"] + "</td>" + "<td>" + news[item]["newscatnm"] + "</td>";
                tr += "<td>" + news[item]["source"] + "</td>" + "<td>" + news[item]["createtime"] + "</td>";
                tr += "<td><input type='hidden' value=" + news[item]["newsid"] + " /><a class='delnews'  href='javascript:void(0);'>删除</a></td>";
                tr += "</tr>";
            }
            $(".table tr:eq(1)").after(tr);
        }
    });
}

$(function () {

    init();

    //事件冒泡
    $(".table").on("click", "tr", function () {
        if ($(this).index() != 0) {
            $(this).parent().find(".success").removeClass("success");
            $(this).addClass("success");
        }
    });

    //绑定删除新闻事件
    $(".table").on("click", ".delnews", function () {
        var newsid = $(this).parent().find("input[type='hidden']").val();
        swal({
            title: "操作提示",      //弹出框的title
            text: "确定删除吗？",   //弹出框里面的提示文本
            type: "warning",        //弹出框类型
            showCancelButton: true, //是否显示取消按钮
            confirmButtonColor: "#DD6B55", //确定按钮颜色
            cancelButtonText: "取消", //取消按钮文本
            confirmButtonText: "是的，确定删除！", //确定按钮上面的文档
            closeOnConfirm: true
        }, function () {
            $.ajax({
                url: 'DelNews',
                data: 'newsId=' + newsid,
                type: 'post',
                cache: false,
                success: function (data) {
                    data = eval("(" + data + ")");
                    var $toast = toastr['success'](data["msg"]);
                    getNews(); //重新查询首页，从第一页开始
                }
            });
        });
    });

    //修改新闻
    $(".table").on("click", ".uptuser", function () {
        var sno = $(this).parent().find("input[type='hidden']").val();
        $.ajax({
            type: "post",
            url: "/Admin/UptUser",
            data: "sno=" + sno,
            cache: false,
            async: true,
            success: function (data) {
                data = eval("(" + data + ")");
                var $toast = toastr['success'](data["msg"]);
                getStudent(); //重新查询首页，从第一页开始
            }
        });
    });

    $("#search").click(function () {
        getNews();
    });
});