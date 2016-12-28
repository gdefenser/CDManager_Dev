// 渐变弹出层
$(document).ready(function () {
    var IsOpen = false;
    $("#btnApply").click(function () {
        $("#ApplyDetail").toggle(1000);
        if (IsOpen) {
            $("#btnApply").val("马上申请");
            IsOpen = false;
        }
        else {
            $("#btnApply").val("取消申请");
            IsOpen = true;
        }
    })
});
