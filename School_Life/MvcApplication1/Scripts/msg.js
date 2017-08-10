var msg = {
    stuNo: {
        reg: "[0-9]{7,15}",
        nul: "学号不能为空",
        val: "学号不合法,应为7-15位数字"
    },
    sname: {
        reg: "[a-zA-Z\u4e00-\u9fa5][a-zA-Z0-9\u4e00-\u9fa5]{1,}",
        nul: "昵称不能为空",
        val: "昵称为2位及以上的字符"
    },
    pwd: {
        reg: "[0-9a-zA-Z]{5,17}",
        nul: "密码不能为空",
        val: "密码为5-17位的数字和字母"
    },
    QQ: {
        reg: "[1-9][0-9]{4,}",
        nul: "QQ号码不能为空",
        val: "QQ应为4位以上的数字"
    },
    title: {
        reg: "[a-zA-Z\u4e00-\u9fa5][a-zA-Z0-9\u4e00-\u9fa5]{5,20}",
        nul: "标题不能为空",
        val: "标题5-20位"
    },
    goodscat: {
        reg: "\w*",
        nul: "请选择类别",
        val: "请选择类别"
    },
    price: {
        reg: "[1-9][1-9]{0,4}.{1}[0-9]{2}",
        nul: "请输入价格",
        val: "价格不合法"
    },
    title: {
        reg: "[a-zA-Z\u4e00-\u9fa5][a-zA-Z0-9\u4e00-\u9fa5]{5,}",
        nul: "请输入新闻标题",
        val:"标题非法，请重新输入"
    },
    source:{
        reg: "[a-zA-Z\u4e00-\u9fa5][a-zA-Z0-9\u4e00-\u9fa5]{2,}",
        nul:"请输入新闻来源",
        val:"请正确输入"
    }
}