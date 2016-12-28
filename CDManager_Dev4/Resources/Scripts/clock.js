$(document).ready(function () {
    showTime('clock_show', 0, 12);
    //数字<10加 "0" 函数(避免时间显示为8点1分3秒)
    function fillZero(v) {
        if (v < 10) {
            v = '0' + v;
        }
        return v;
    }

    function showTime(ev, date, type) {
        var d;
        //ev = '#'+ev;
        var Y, M, D, W, H, I, S,MS;
        var Week = ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'];
        if (date) {
            d = new Date(date * 1000);
        } else {
            d = new Date();
        }
        
        Y = d.getFullYear(); ;
        M = fillZero(d.getMonth() + 1);
        D = fillZero(d.getDate());
        W = Week[d.getDay()];
        H = fillZero(d.getHours());
        I = fillZero(d.getMinutes());
        S = fillZero(d.getSeconds());
        //alert(H+':'+I+':'+S);
        //document.write(H+':'+I+':'+S);
        if (type && type == 12) {
            if (H < 10 && H >= 6) {
                MS= '早上好,';
            } else if (H >= 10 && H < 12) {
                MS = '上午好,';
                H = fillZero(H);
            } else if (H >= 12 && H < 14) {
                MS = '中午好,';
            } else if (H >= 14 && H < 19) {
                MS = '下午好,';
            } else if (H >= 19 && H < 24) {
                MS = '晚上好,';
            } else if (H == 0) {
                MS = '深夜好,';
                H = '00';
            } else if (H > 0 && H < 6) {
                MS = '深夜好,';
            }
        }
        var showData = MS+'现在时间:' + Y + '年' + M + '月' + D + '日' + ' ' + H + ':' + I + ':' + S + ' ' + W
        $('#' + ev).html(showData);
        if (date) {
            date++;
        }
        setTimeout(function () { showTime(ev, date, type) }, 1000);
        //setTimeout(arguments.callee,1000);
    }
})