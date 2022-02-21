<%@ Page Language="VB" AutoEventWireup="false" CodeFile="situation.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>
<html lang="ja">
<head>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>ポータルサイト（概況）</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="css/style.css">
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<script>
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
            } else {
                //window.alert('koko3');
                window.location.href = './detail.aspx?id=' + encodeURIComponent(arr[1]);
                return false;
            };
        });
    });
    $(function () {
        $('th').off("click").on("click", function () {
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
    .ta2 th {
	    width: 200px;		/*幅*/
	    text-align: center;	/*センタリング*/
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
    <!-- #Include File="header.html" -->

<div id="contents" class="inner">
<div id="contents-in">

<div id="main">

<section>

<!-- 以下担当者は2022/1/18現在 -->
<h2>ＥＸＬ概況</h2>
<table class="ta2">
<tr>
    <!-- 担当：西浦SM -->
    <th data-id='["home","./booking_situation.aspx"]'><a href="#">コンテナ確保状況</a></th>
    <td><asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
    <td><asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
</tr>
<tr>
    <!-- 担当：田邊G、尼嵜TL -->
    <th data-id='["home","./van_result.aspx"]'><a href="#">本社バンニング</a></th>
    <td><asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
    <td><asp:Literal ID="Literal4" runat="server"></asp:Literal></td>
</tr>
<tr>
    <!-- 担当：尼嵜TL -->
    <th data-id='["home","./4f_floor.aspx"]'><a href="#">４Ｆフロア状況</a></th>
    <td><asp:Literal ID="Literal5" runat="server"></asp:Literal></td>
    <td><asp:Literal ID="Literal6" runat="server"></asp:Literal></td>
</tr>
<tr>
    <!-- 担当：名村TL -->
    <th data-id='["home","./3f_konpo.aspx"]'><a href="#">３Ｆ梱包進捗状況</a></th>
    <td><asp:Literal ID="Literal9" runat="server"></asp:Literal></td>
    <td><asp:Literal ID="Literal10" runat="server"></asp:Literal></td>
</tr>
<tr>
    <!-- 担当：田邊G -->
    <th data-id='["home","./eed.aspx"]'><a href="#">EED即納状況</a></th>
    <td><asp:Literal ID="Literal7" runat="server"></asp:Literal></td>
    <td><asp:Literal ID="Literal8" runat="server"></asp:Literal></td>
</tr>
<tr>
    <th>　</th>
    <td>　</td>
    <td>　</td>
</tr>
</table>

</section>

</div>
<!--/#main-->

<div id="sub">

<div class="box1">
    <h2 class="mb10">ＥＸＬ概況</h2>
    <asp:Image class="imgOKNG" runat="server" src="images/NGtouka.png" style="margin-top:8px;margin-left:70px;" />
</div>
<h2>トピックス</h2>

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
