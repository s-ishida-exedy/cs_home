    <%@ Page Language="VB" AutoEventWireup="false" CodeFile="detail.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<script>
    $(document).ready(function () {
        var text = getParam('id');
        $('.result').text(text);
        if (text !== 'home') {
            $('iframe').attr('src', text);
            return false;   // hrefのクリックイベントキャンセル
        };
    });
    $(function () {
        $('ul li').off("click").on("click", function () {
            var arr = $(this).data('id');
            //var text = $(this).data('id');
            if (arr[0] == 'home' || arr[0] == 'aaa' || arr[0] == 'undefined') {
                if (arr[0] == 'home') {
                    window.location.href = encodeURIComponent(arr[1]);
                    return false;
                } else {
                    return false;
                }
            } else{
                //window.alert('koko3');
                window.location.href = './detail.aspx?id=' + encodeURIComponent(arr[1]);
                return false;
            };
        });
    });
    $(function () {
        $('th').off("click").on("click",function () {
            var text = $(this).data('id');
            if (text == 'home' || text == 'aaa' || text == 'undefined') {
                return false;
            } else {
                window.location.href = './detail.aspx?id=' + encodeURIComponent(text);
                return false;
            };
        });
    });
</script>
</head>
<body class="c2">
<form id="form1" runat="server">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
<% If Session("strRole") = "admin" Or Session("strRole") = "csusr" Then %>
    <!-- #Include File="header/header.aspx" -->
<% Else %>
    <!-- #Include File="header/exl_header.aspx" -->
<% End If %>
<div id="contents2" class="inner2">
<div id="main2" style="width:100%;overflow:scroll;-webkit-overflow-scrolling:touch;">
<%-- 画面表示する対象のWEBページをセット。縮小率を設定する。 --%>
    <iframe id="iframe" src="#" 
        style ="
            height:730px;
            transform:scale(0.8);
            -moz-transform:scale(0.8);
            -webkit-transform:scale(0.8);
            -o-transform:scale(0.8);
            -ms-transform:scale(0.8);
            transform-origin:0 0;
            -moz-transform-origin:0 0;
            -webkit-transform-origin:0 0;
            -o-transform-origin:0 0;
            -ms-transform-origin:0 0;
            /*border:solid 1px;*/
            margin-bottom:-200px;
            margin-right:-100%;
            width:200%;">
    </iframe>
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
