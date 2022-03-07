<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admin_top.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>
<html lang="ja">
<head>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>ポータルサイト(管理者)</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="../css/style.css">
<script src="../js/openclose.js"></script>
<script src="../js/fixmenu.js"></script>
<script src="../js/fixmenu_pagetop.js"></script>
<script src="../js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="./js/default.js"></script>
<script>
    // メニュークリック時の画面遷移
    $(function () {
        $('ul li').off("click").on("click", function () {
            var arr = $(this).data('id');
            if (arr[0] == 'home' || arr[0] == 'aaa' || arr[0] == 'undefined') {
                if (arr[0] == 'home') {
                    window.location.href = encodeURIComponent(arr[1]);
                    return false;
                } else {
                    return false;
                }
            } else {
                return false;
            };
        });
    });
</script>
<style type="text/css">
        input {
            display: none;
        }
        label {
            display: block;
            margin: 0 0 4px 0;
            padding : 15px;
            line-height: 1;
            color :#fff;
            background: linear-gradient(#9d9980, #8c876c);
            cursor :pointer;
        }
        td{
            text-align: center ;		/*文字を左寄せ*/
        }
        .topic {
            border-collapse: collapse;
            margin: 0 auto;
            padding: 0;
            width: 300px;
            table-layout: fixed;
            color: #4CAF50;
        }

        .topic tr {
            background-color: #fff;
            padding: .2em;
            border-bottom: 1px dotted #8BC34A;
        }
        .topic th,
        .topic td {
            padding: .5em .5em .5em .5em;
            color: #666;
        }
        .topic th{
            width: 40px;
            text-align: center;
        }
        .txt{
            text-align: left;
            font-size: 1em;
        }
        .txtth{
            font-size: 1em;
        }
        .hTD1{
            width :25%;
        }
        .c a {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0 auto;
            padding: 0.5em 1em;
            width: 100px;
            color: #000000;
            font-size: 12px;
            font-weight: 200;
            border: 2px solid #ffffff;
            border-radius: 4px;
            text-decoration: none;
            transition: all 0.1s;
        }
        .c a::after {
            content: '';
            width: 5px;
            height: 5px;
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
            transform: rotate(45deg);
        }
        .c a:hover {
            color: #000000;
            text-decoration: none;
            background-color: #ffffff;
            border: 2px solid #000000;
        }
        .c a:hover::after {
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
        }
</style>
</head>

<body class="c2">

<form id="form1" runat="server">

<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.htmlで行う -->
    <!-- #Include File="admin_header.aspx" -->

<div id="contents" class="inner">
<div id="contents-in">

<div id="main">
</div>
</div>
</div>


<footer>

<div id="footermenu" class="inner">
        <a href ="../start.aspx">ポータルメニューへ</a>
</div>
<!--/footermenu-->

<div id="copyright">
</div>

</footer>

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
