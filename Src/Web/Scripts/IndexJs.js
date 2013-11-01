﻿

$(function () {
    /*查找ID为.child li的ID，并修改它的class（类）为colse类*/
    $('.child li').attr('class', 'close');

    $(".parent li").each(function () {
        var key = $(this).attr('class');
        key = '.' + key + ' li';

        $(this).mouseover(function () {
            $(key).each(function () {
                $(this).attr('class', 'open');
            });
        });

        $(this).mouseout(function () {
            $(key).each(function () {
                $(this).attr('class', 'close');
            });
        });
    });
    /*实现展开收缩符号功能.......................................................*/
    menulist();

    $('#faq dt').click(function () {
        icoPlusAndMinus();
    });

    function icoPlusAndMinus() {
        var icoclass = $('.ico').attr('class');
        if (icoclass.indexOf('icoMinus') == -1) {
            $('.ico').addClass('icoMinus');
        }
        else {
            $('.ico').removeClass('icoMinus');
        }
    }
});

/*实现展开收缩符号功能.......................................................*/
function getObject(objectId) {
    if (document.getElementById && document.getElementById(objectId)) {
        return document.getElementById(objectId);
    } else if (document.all && document.all(objectId)) {
        return document.all(objectId);
    } else if (document.layers && document.layers[objectId]) {
        return document.layers[objectId];
    } else {
        return false;
    }
}

function showHide(e, objname) {
    var obj = getObject(objname);
    if (obj.style.display == "none") {
        obj.style.display = "block";
        e.className = "minus";
    } else {
        obj.style.display = "none";
        e.className = "plus";
    }
}


/*列表项的js*/
var lastFaqClick = null;
function menulist() {
    var faq = document.getElementById("faq");
    var dls = faq.getElementsByTagName("dl");
    for (var i = 0, dl; dl = dls[i]; i++) {
        var dt = dl.getElementsByTagName("dt")[0];//取得标题
        dt.id = "faq_dt_" + (Math.random() * 100);
        dt.onclick = function () {
            var p = this.parentNode;//取得父节点
            if (lastFaqClick != null && lastFaqClick.id != this.id) {
                var dds = lastFaqClick.parentNode.getElementsByTagName("dd");
                for (var i = 0, dd; dd = dds[i]; i++)
                    dd.style.display = 'none';
            }
            lastFaqClick = this;
            var dds = p.getElementsByTagName("dd");//取得对应子节点，也就是说明部分
            var tmpDisplay = 'none';
            if (gs(dds[0], 'display') == 'none')
                tmpDisplay = 'block';
            for (var i = 0; i < dds.length; i++)
                dds[i].style.display = tmpDisplay;
        }
    }
}

function gs(d, a) {
    if (d.currentStyle) {
        var curVal = d.currentStyle[a]
    } else {
        var curVal = document.defaultView.getComputedStyle(d, null)[a]
    }
    return curVal;
}

/*导航高亮*/
var $c = function (array) { var nArray = []; for (var i = 0; i < array.length; i++) nArray.push(array[i]); return nArray; };
Array.prototype.each = function (func) {
    for (var i = 0, l = this.length; i < l; i++) { func(this[i], i); };
};
document.getElementsByClassName = function (cn) {
    var hasClass = function (w, Name) {
        var hasClass = false;
        w.className.split(' ').each(function (s) {
            if (s == Name) hasClass = true;
        });
        return hasClass;
    };
    var elems = document.getElementsByTagName("*") || document.all;
    var elemList = [];
    $c(elems).each(function (e) {
        if (hasClass(e, cn)) { elemList.push(e); }
    })
    return $c(elemList);
};
function change_bg(obj) {
    var a = document.getElementsByClassName("nav")[0].getElementsByTagName("a");
    for (var i = 0; i < a.length; i++) { a[i].className = ""; }
    obj.className = "current";
}