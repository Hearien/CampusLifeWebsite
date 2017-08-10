//页面初始化方法
function init() {
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

function getStudent(page) {
    var sno = $("#sno").val();
    var grade = $("#grade").val();
    var major = $("#major").val();
    var pageCode = page || "";
    $.ajax({
        type: "POST",
        data: "pageCode=" + pageCode + "&sno=" + sno + "&grade=" + grade + "&major=" + major,
        url: "/Admin/StuPager",
        async: true,
        success: function (data) {
            $(".table tr:gt(0)").html("");
            data = eval("(" + data + ")");
            currentPage = data.pageCode;
            totalPage = data.totalPage;
            $('#pager').bootstrapPaginator("setOptions", { currentPage: currentPage, totalPages: totalPage });
            var students = data.students;
            var tr = "";
            for (var s in students) {
                tr += "<tr>";
                tr += "<td>" + students[s]["sno"] + "</td>" + "<td>" + students[s]["sname"] + "</td>";
                tr += "<td>" + students[s]["genderVal"] + "</td>" + "<td>" + students[s]["grade"] + "</td>";
                tr += "<td>" + students[s]["deptName"] + "</td>" + "<td>" + students[s]["majorName"] + "</td>";
                tr += "<td>" + students[s]["QQ"] + "</td>" + "<td>" + students[s]["address"] + "</td>";
                tr += "<td><input type='hidden' value=" + students[s]["sno"] + " /><a class='uptuser' href='javascript:void(0);'>密码重置</a><a class='deluser'  href='javascript:void(0);'>删除</a></td>";
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

    //绑定删除学生事件
    $(".table").on("click", ".deluser", function () {
        var sno = $(this).parent().find("input[type='hidden']").val();
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
                url: 'DelUser',
                data: 'sno=' + sno,
                type: 'post',
                cache: false,
                success: function (data) {
                    data = eval("(" + data + ")");
                    var $toast = toastr['success'](data["msg"]);
                    getStudent(); //重新查询首页，从第一页开始
                }
            });
        });
    });

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
                getStudent(page);
            }
        }
    };
    $("#pager").bootstrapPaginator(params);

    //重置登录密码
    $(".table").on("click", ".uptuser", function () {
        var sno = $(this).parent().find("input[type='hidden']").val();
        $.ajax({
            type: "post",
            url: "/Admin/UptUser",
            data:"sno="+sno,
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
        getStudent();
    });
});