//页面初始化方法
function init() {
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
                window.location.href = "http://localhost:1188/Admin/DynamicPager?pageCode=" + page;
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

$(function () {

    init();


    //绑定删除动态事件
    $(".comment").on("click", ".close", function () {
        var dynamicId = $(this).parent().find("input[type='hidden']").val();
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
            window.location.href = "http://localhost:1188/Admin/DelDynamic?dynamicId=" + dynamicId;
           
        });
    });
});