$(document).ready(function () {
    $("#Button1").click(function () {
        var file = $("#File1").val();
        if (file == "") {
            alert("请选择文件！");
        }
        else {
            alert(file);
            $.ajax({
                type: "POST",
                url: "ReaderUploadHandler.ashx",
                data: "upfile=" + file,
                beforeSend: function () { },
                success: function () { },
                complete: function () { },
                error: function () { }
            });
        }
    });
});