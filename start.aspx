<%@ Page Language="VB" AutoEventWireup="false" CodeFile="start.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>
<html lang="ja">
<head>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>ポータルサイト</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="css/style.css">
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
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
                window.location.href = './detail.aspx?id=' + encodeURIComponent(arr[1]);
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
</style>
</head>

<body class="c2">

<form id="form1" runat="server">

<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.htmlで行う -->
    <!-- #Include File="header.html" -->

<div id="contents" class="inner">
<div id="contents-in">

<div id="main">

<section>

<h2>お知らせ</h2>
<table class="ta1">
<!-- <caption>本日の業務予定 ／ 翌営業日の業務予定</caption> -->
<tr style="background-color: #eee;text-align: center;color: #666;font-weight: bold; ">
    <td>
        業務
    </td>
    <td>
        本日の予定
    </td>
    <td>
        翌営業日の予定
    </td>
    <td>
        更新日時
    </td>
</tr>
<tr>
    <th>ＶＡＮ（本社　ＡＦ）</th>
    <td>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal6" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal7" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>ＶＡＮ（本社　ＫＤ）</th>
    <td>
        <asp:Literal ID="Literal16" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal17" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal18" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>ＶＡＮ（上野）</th>
    <td>
        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal8" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal9" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>ＡＩＲ</th>
    <td>
        <asp:Literal ID="Literal3" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal10" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal11" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>自社通関</th>
    <td>
        <asp:Literal ID="Literal4" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal12" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal13" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>通関委託</th>
    <td>
        <asp:Literal ID="Literal5" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal14" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal15" runat="server"></asp:Literal>
    </td>
</tr>
</table>

</section>

</div>
<!--/#main-->

<div id="sub">

<div class="box1">
    <h2 class="mb10"><a href="situation.aspx">ＥＸＬ概況</a></h2>
    <asp:Image id="OKNGimg" class="imgOKNG" runat="server" ImageUrl="#" style="margin-top:8px;margin-left:70px;" />
</div>

<!-- インクルードファイルの指定 -->
<!-- サイドメニューの編集はsidemenu.htmlで行う -->
    <!-- #Include File="sidemenu.html" -->

</div>
<!--/#sub-->

</div>
<!--/#contents-in-->

<div id="side">

</div>
<!--/#side-->

</div>
<!--/#contents-->

<footer>

<div id="footermenu" class="inner">

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
