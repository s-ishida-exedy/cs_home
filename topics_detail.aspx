<%@ Page Language="VB" AutoEventWireup="false" CodeFile="topics_detail.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>
<html lang="ja">
<head>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>ポータルサイト(トピックス)</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="css/style.css">
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<%--Datepicker用--%>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
<script src="https://code.jquery.com/jquery-1.10.2.js"  integrity="sha256-it5nQKHTz+34HijZJQkpNBIHsjpV8b6QzMJs9tmOBSo="  crossorigin="anonymous"></script>
<script src="https://code.jquery.com/ui/1.11.4/jquery-ui.js" integrity="sha256-DI6NdAhhFRnO2k51mumYeDShet3I8AKCQf/tf7ARNhI=" crossorigin="anonymous"></script>
<%--Datepicker用--%>
<script src="js/default.js"></script>
<script>
    // カレンダー
    jQuery(function ($) {
        $(".date2").datepicker({
            dateFormat: 'yy/mm/dd',
            showButtonPanel: true
        });
    });
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
        .txt1 .date2{
            text-align: center ;
            padding: 5px;
        }
        .td-left{
            text-align: left;
        }
        .ui-datepicker {
            font-size: 70%;
        }
        .err{
            color:red;
            font-weight :700;
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
        .txtb{
            padding: 5px;
        }
</style>
</head>

<body class="c2">

<form id="form1" runat="server" autocomplete="off">

<!--PC用（901px以上端末）メニュー-->
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->
<div id="contents" class="inner">
<div id="contents-in">

<div id="main">

<section>
<h2>トピックス</h2>
    <asp:Label ID="Label11" runat="server" Text="" Class="err"></asp:Label>
<table class="topicta">
<tr>
    <td width="100px">
        投稿日時：
    </td>
    <td class="td-left">
        <asp:TextBox ID="TextBox1" runat="server" class="date2"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" class="txt1" Width="66px"></asp:TextBox>
    </td>
    <td>
        投稿者：
        <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="MEMBER_NAME" DataValueField="NAME_AB" Width="170px" ></asp:DropDownList>
    </td>
</tr>
<tr>
    <td width="100px">
        タイトル：
    </td>
    <td colspan ="2" class="td-left">
        <asp:TextBox ID="TextBox3" runat="server" Width="556px" Class ="txtb"></asp:TextBox>
        ※25文字以内
    </td>
</tr>
</table>
<table class="topicta">
<tr>
    <td width="100px">
        内　　容：<br/>
        512文字以内
    </td>
    <td colspan ="2" class="td-left">
        <asp:TextBox ID="TextBox4" runat="server" Height="173px" TextMode="MultiLine" Width="751px" Class ="txtb"></asp:TextBox>
    </td>
</tr>
</table>
<table class="topicta2">
<tr>
    <td>
        <asp:Button ID="Button1" runat="server" Text="登　　録" width="150px"/>
    </td>
    <td>
        <asp:Button ID="Button2" runat="server" Text="更　　新" width="150px" />
    </td>
    <td>
        <asp:Button ID="Button3" runat="server" Text="削　　除" width="150px" />
    </td>
    <td class="c">
        <a href="./topics.aspx">一覧へ戻る</a>
    </td>
</tr>
</table>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT
  INFO_DATE
  , INFO_TIME
  , INFO_HEADER
FROM
  T_EXL_INFOMATION "></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT *
FROM M_EXL_CS_MEMBER
WHERE PLACE LIKE '%H%'
AND CODE LIKE 'T%'
UNION
SELECT *
FROM M_EXL_CS_MEMBER
WHERE PLACE LIKE '%H%'
AND CODE LIKE 'E%'
AND TEAM = 'CSチーム'"></asp:SqlDataSource>

</section>

</div>
<!--/#main-->

<div id="sub">

<div class="box1">
    <h2 class="mb10"><a href="situation.aspx">ＥＸＬ概況</a></h2>
    <asp:Image id="OKNGimg" class="imgOKNG" runat="server" ImageUrl="#" style="margin-top:8px;margin-left:70px;" />
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
