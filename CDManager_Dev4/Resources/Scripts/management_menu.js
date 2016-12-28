
$(document).ready(function () {
    var IsAll = false;
    var IsOpen_system = false;
    var IsOpen_msg = false;
    var IsOpen_book = false;
    var IsOpen_user = false;
    var IsOpen_security = false;

    var url = window.location.href;
    var s_url = url.split("/");
    var page_name = s_url[s_url.length - 1].replace(/^(.*)\..*/, "$1");

    var system = $.inArray(page_name, ["SystemManagement"]);
    var message = $.inArray(page_name, ["NewNotice", "EditNotice", "NoticeManagement", "NoticeDetail","NewMessage","SendedMessage","ReceivedMessage","MessageDialog"]);
    var book = $.inArray(page_name, ["NewBook", "BookManagement", "EditBook", "NewBookByExcel", "ApplyLogList", "CDDetail", "NewCDByExcel", "CDStatistics", "CDOnline"]);
    var user = $.inArray(page_name, ["NewAdmin", "EditAdmin", "AdminManagement", "NewReader", "EditReader", "ReaderManagement", "NewReaderByExcel", "ApplyAndDownload", "AdminStatistics"]);
    var security = $.inArray(page_name, ["BanIPManagement", "BanReaderManagement"]);

    if (system >= 0) {
        $("#system_item").show();
        $("#system_titleimg").html("▽");
        IsOpen_system = true;
    }
    else if (message >= 0) {
        $("#msg_item").show();
        $("#msg_titleimg").html("▽");
        IsOpen_msg = true;
    }
    else if (book >= 0) {
        $("#book_item").show();
        $("#book_titleimg").html("▽");
        IsOpen_book = true;
    }
    else if (user >= 0) {
        $("#user_item").show();
        $("#user_titleimg").html("▽");
        IsOpen_book = true;
    }
    else if (security >= 0) {
        $("#security_item").show();
        $("#security_titleimg").html("▽");
        IsOpen_book = true;
    }
    //    $("#all_title").click(function () {
    //        $("#system_item").toggle(800);
    //        $("#msg_item").toggle(800);
    //        $("#book_item").toggle(800);
    //        $("#user_item").toggle(800);
    //        if (IsAll) {
    //            $("#system_titleimg").html("◇");
    //            $("#msg_titleimg").html("◇");
    //            $("#book_titleimg").html("◇");
    //            $("#user_titleimg").html("◇");

    //            IsAll = false;
    //            IsOpen_system = false;
    //            IsOpen_msg = false;
    //            IsOpen_book = false;
    //            IsOpen_user = false;

    //            $("#all_title").html("<span id='all_titleimg'>◇</span>全部打开");
    //        }
    //        else {
    //            $("#system_titleimg").html("▽");
    //            $("#msg_titleimg").html("▽");
    //            $("#book_titleimg").html("▽");
    //            $("#user_titleimg").html("▽");

    //            IsAll = true;
    //            IsOpen_system = true;
    //            IsOpen_msg = true;
    //            IsOpen_book = true;
    //            IsOpen_user = true;

    //            $("#all_title").html("<span id='all_titleimg'>▽</span>全部关闭");
    //        }
    //    });

    $("#system_title").click(function () {
        $("#system_item").toggle(300);
        if (IsOpen_system) {
            $("#system_titleimg").html("◇");
            IsOpen_system = false;
        }
        else {
            $("#system_titleimg").html("▽");
            IsOpen_system = true;
        }
    });

    $("#msg_title").click(function () {
        $("#msg_item").toggle(300);
        if (IsOpen_msg) {
            $("#msg_titleimg").html("◇");
            IsOpen_msg = false;
        }
        else {
            $("#msg_titleimg").html("▽");
            IsOpen_msg = true;
        }
    });

    $("#book_title").click(function () {
        $("#book_item").toggle(300);
        if (IsOpen_book) {
            $("#book_titleimg").html("◇");
            IsOpen_book = false;
        }
        else {
            $("#book_titleimg").html("▽");
            IsOpen_book = true;
        }
    });

    $("#user_title").click(function () {
        $("#user_item").toggle(300);
        if (IsOpen_user) {
            $("#user_titleimg").html("◇");
            IsOpen_user = false;
        }
        else {
            $("#user_titleimg").html("▽");
            IsOpen_user = true;
        }
    });

    $("#security_title").click(function () {
        $("#security_item").toggle(300);
        if (IsOpen_user) {
            $("#security_titleimg").html("◇");
            IsOpen_user = false;
        }
        else {
            $("#security_titleimg").html("▽");
            IsOpen_user = true;
        }
    });
});