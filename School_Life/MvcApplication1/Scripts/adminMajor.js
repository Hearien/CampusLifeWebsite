//页面初始化方法
function init() {

    $("#mytab a").click(function (e) {
        e.preventDefault();
        $(this).tab("show");
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
                getMajor(page);
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

function getMajor(page) {
    var pageCode = page || "";
    $.ajax({
        type: "POST",
        data: "pageCode=" + pageCode,
        url: "/Admin/MajorPager",
        async: true,
        success: function (data) {
            $(".table tr:gt(0)").html("");
            data = eval("(" + data + ")");
            console.log(data);
            currentPage = data.pageCode;
            totalPage = data.totalPage;
            $('#pager').bootstrapPaginator("setOptions", { currentPage: currentPage, totalPages: totalPage });
            var major = data.major;
            var tr = "";
            for (var item in major) {
                tr += "<tr>";
                tr += "<td>" + major[item]["majorNo"] + "</td>" + "<td>" + major[item]["majorName"] + "</td>";
                tr += "<td>" + major[item]["deptName"] + "</td>";
                tr += "<td><input type='hidden' value=" + major[item]["id"] + " /><a class='delmajor'  href='javascript:void(0);'>删除</a></td>";
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

    //绑定删除商品分类事件
    $(".table").on("click", ".delmajor", function () {
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
                url: 'DelMajor',
                data: 'majorId=' + newsid,
                type: 'post',
                cache: false,
                success: function (data) {
                    data = eval("(" + data + ")");
                    var $toast = toastr['success'](data["msg"]);
                    getMajor(); //重新查询首页，从第一页开始
                }
            });
        });
    });

});