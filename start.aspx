﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="start.aspx.vb" Inherits="cs_home" %>

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
        .cop{
            font-size :1px;
        }
        .ta1 td {
            padding: 2px;
        }
</style>
</head>

<body class="c2">

<form id="form1" runat="server">

<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->

<div id="contents" class="inner">
<div id="contents-in">

<div id="main">

<section>
<h2>お知らせ</h2>

<table class="ta1">
<tr style="background-color: #eee;text-align: center;color: #666;font-weight: bold; ">
    <td rowspan ="2">
        業務
    </td>
    <td colspan ="2" class="hTD1">
        本日
    </td>
    <td rowspan ="2">
        翌営業日の予定
    </td>
    <td rowspan ="2">
        更新日時
    </td>
</tr>
<tr style="background-color: #eee;text-align: center;color: #666;font-weight: bold; ">
    <td class="hTD1">
        予定
    </td>
    <td class="hTD1">
        実績
    </td>
</tr>
<tr>
    <th>ＶＡＮ（本社　ＡＦ）</th>
    <td>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal23" runat="server"></asp:Literal>
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
        <asp:Literal ID="Literal24" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal17" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal18" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>書類作成（本社）</th>
    <td>
        <asp:Literal ID="Literal12" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal26" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal14" runat="server" Text="－"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal19" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>ＶＡＮ（上野）</th>
    <td>
        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal25" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal8" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal9" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>書類作成（上野）</th>
    <td>
        <asp:Literal ID="Literal20" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal27" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal21" runat="server" Text="－"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal22" runat="server"></asp:Literal>
    </td>
</tr>
<tr>
    <th>ＡＩＲ</th>
    <td>
        <asp:Literal ID="Literal3" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Literal ID="Literal28" runat="server"></asp:Literal>
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
        <asp:Label ID="Label11" runat="server" ></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label12" runat="server" Text="－"></asp:Label>
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
        <asp:Label ID="Label13" runat="server" Text="－"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label14" runat="server" Text="－"></asp:Label>
    </td>
    <td>
        <asp:Literal ID="Literal15" runat="server"></asp:Literal>
    </td>
</tr>
</table>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT
  INFO_DATE
  , INFO_TIME
  , INFO_HEADER
FROM
  T_EXL_INFOMATION "></asp:SqlDataSource>

</section>

</div>
<!--/#main-->

<div id="sub">

<div class="box1">
    <h2 class="mb10"><a href="situation.aspx">ＥＸＬ概況</a></h2>
    <asp:Image id="OKNGimg" class="imgOKNG" runat="server" ImageUrl="#" style="margin-top:8px;margin-left:70px;" />
</div>

<h2>CSチームトピックス</h2>

<div class="list-sub">
<table class="topic">
    <tbody>
        <tr>
            <th class="txtth"><asp:Label ID="Label1" runat="server" Text=""></asp:Label></th>
            <th class="txtth"><asp:Label ID="Label6" runat="server" Text=""></asp:Label></th>
            <td data-label="内容" class="txt">
                <asp:LinkButton ID="LinkButton1" runat="server" class="lnk"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <th class="txtth"><asp:Label ID="Label2" runat="server" Text=""></asp:Label></th>
            <th class="txtth"><asp:Label ID="Label7" runat="server" Text=""></asp:Label></th>
            <td data-label="内容" class="txt">
                <asp:LinkButton ID="LinkButton2" runat="server" class="lnk"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <th class="txtth"><asp:Label ID="Label3" runat="server" Text=""></asp:Label></th>
            <th class="txtth"><asp:Label ID="Label8" runat="server" Text=""></asp:Label></th>
            <td data-label="内容" class="txt">
                <asp:LinkButton ID="LinkButton3" runat="server" class="lnk"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <th class="txtth"><asp:Label ID="Label4" runat="server" Text=""></asp:Label></th>
            <th class="txtth"><asp:Label ID="Label9" runat="server" Text=""></asp:Label></th>
            <td data-label="内容" class="txt">
                <asp:LinkButton ID="LinkButton4" runat="server" class="lnk"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <th class="txtth"><asp:Label ID="Label5" runat="server" Text=""></asp:Label></th>
            <th class="txtth"><asp:Label ID="Label10" runat="server" Text=""></asp:Label></th>
            <td data-label="内容" class="txt">
                <asp:LinkButton ID="LinkButton5" runat="server" class="lnk"></asp:LinkButton>
            </td>
        </tr>
    </tbody>
</table>  
</div>
<p class="c"><a href="./topics.aspx">トピックス一覧</a></p>
<asp:Label ID="Label15" runat="server" Text="※トピックスはCS専用、他部署は閲覧不可です。"  Font-Size="Small"></asp:Label>
<asp:Label ID="Label16" runat="server" Text="※連絡事項など自由に使用してください。"  Font-Size="Small"></asp:Label>
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
    <a href ="admin/admin_top.aspx">管理者メニューへ</a>
</div>
<!--/footermenu-->

<div id="copyright">
    <small>Copyright&copy; <a href="start.aspx">CS PORTAL SITE</a> All Rights Reserved.</small>
    <span class="cop"><a href="https://template-party.com/" target="_blank">《Web Design:Template-Party》</a></span>
</div>

</footer>

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
