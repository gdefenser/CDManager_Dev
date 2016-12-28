var timerID = null;
var timerRunning = false;


//使用时需要设置一下几个控件 
var btn_disable = $("#btnExe"); //执行按钮
var obj_progresstext = $("#progressbar_text"); //进度条文本 
var obj_progressbar = $("#progressbar_bar"); //进度条背景
var obj_tips = $("#tips"); //进度条说明提示

(document).ready(function () {
    $("#Butto1").click(function () {
        var file = $("#FileUpload").val();
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


    //设置进度条 
    function setProcessBar(exeFlag, exeMax) {
        $("#progressbar_text").innerHTML = parseInt(roundFun(exeFlag / exeMax, 2) * 100) + "%";
        $("#progressbar_bar").style.width = parseInt(roundFun(exeFlag / exeMax, 2) * 100) + "%";
        $("#tips").innerHTML = exeFlag + "/" + exeMax;
    }

    //取值 
    function roundFun(number, X) {
        X = (!X ? 2 : X);
        return Math.round(number * Math.pow(10, X)) / Math.pow(10, X);
    }

    //禁用按钮 
    function disablebtn(btn) {
        btn.disabled = true;
        btn.value = "执行中..";
    }

    //停止执行进度条 
    function stopstep() {
        if (timerRunning) {
            timerRunning = false;
        }
        if (btn_disable.disabled) {
            btn_disable.disabled = false;
            btn_disable.value = "开始导入数据";
            obj_tips.innerHTML = "数据导入完成！";
        }
    }

    stopstep();

    function postExecute(exeMax, exeFlag) {
        ajaxObj.get("ProcessExecute.aspx?exeMax=" + exeMax + "&exeFlag=" + exeFlag, callback_InsertData);
    }

    function callback_InsertData(obj) {
        if (obj.responseText) {
            var str = obj.responseText; //"true|"+recordcount + "|" + page
            var strarr = str.split("|");
            var flag = strarr[0];
            var exeMax = parseInt(strarr[1]); //1000
            var exeFlag = parseInt(strarr[2]); //1	
            var Msg = strarr[3];

            if (flag == "true") {
                setProcessBar(exeFlag, exeMax); //设置进度条           
                if (exeFlag < exeMax) {
                    timerID = postExecute(exeMax, exeFlag + 1);
                    timerRunning = true;
                }
                else { stopstep(); }
            }
            else {
                stopstep();
                obj_tips.innerHTML = "执行错误！错误原因：<br />" + Msg + "";
            }
        }
    }
});