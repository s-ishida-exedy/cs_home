<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eir_mail_comf.aspx.vb" Inherits="cs_home" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(EIR、Booking情報差異連絡)</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<%--Datepicker用--%>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
<script src="//code.jquery.com/jquery-1.10.2.js"></script>
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
<%--Datepicker用--%>
<style type="text/css">
        .header-ta {
            width: 1300px;
        }
        .first-cell {
            text-align:left;
            font-size:25px;
            width: 450px;
        }
        .second-cell {
            width: 700px;
        }   
        .third-cell {
            width: 150px;
            text-align:right;
        }
        .third-cell a {
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
        .third-cell a::after {
            content: '';
            width: 5px;
            height: 5px;
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
            transform: rotate(45deg);
        }
        .third-cell a:hover {
            color: #000000;
            text-decoration: none;
            background-color: #ffffff;
            border: 2px solid #000000;
        }
        .third-cell a:hover::after {
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
        }
        h2 {
            padding-left: 45px;
            position: relative;
            border-radius: 10px; /* 角を丸くする */
        }
        h2:before {
            content: "";
            background-color: #6fbfd1;
            border-radius: 50%;
            opacity: 0.5;
            width: 35px;
            height: 35px;
            left: 5px;
            top: 0px;
            position: absolute;
        }
        h2:after{
            content: "";
            background-color: #6fbfd1;
            border-radius: 50%;
            opacity: 0.5;
            width: 20px;
            height: 20px;
            left: 25px;
            top:15px;
            position: absolute;
        }
        .tab1{
            width:50%;
        }
        .td1 {
            width: 54pt;
            color: black;
            font-size: 10pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "ＭＳ ゴシック", monospace;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid black;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #87E7AD;
        }
        .td2 {
            width: 54pt;
            color: black;
            font-size: 10pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "ＭＳ ゴシック", monospace;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid black;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background:#ffffff;
        }
        .td3 {
            width: 54pt;
            color: black;
            font-size: 10pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "ＭＳ ゴシック", monospace;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid black;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background:#ffffff;
            width:25%;
        }
        .err{
            color:red;
            font-weight :700;
        }
        .titl{
            color:black;
            font-weight:600;
        }
</style>
<script>
    // カレンダー
    jQuery(function ($) {
        $(".date2").datepicker({
            dateFormat: 'yy/mm/dd',
            showButtonPanel: true
        });
    });
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
</script>
</head>
<body class="c2">
<form id="form1" runat="server" autocomplete="off">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
<% If Session("strRole") = "admin" Or Session("strRole") = "csusr" Then %>
    <!-- #Include File="header/header.aspx" -->
<% Else %>
    <!-- #Include File="header/exl_header.aspx" -->
<% End If %>
       
<div id="contents2" class="inner2">
        <table class="header-ta" >
            <tr>
                <td class="first-cell">
                    <h2>EIR、Booking情報差異連絡確認</h2> 
                </td>
                <td class="second-cell">
                    <asp:Button ID="Button1" runat="server" Text="メール送信" style="width:164px" Font-Size="Small" />&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="前のページに戻る" style="width:164px" Font-Size="Small" /><br />
                    <asp:Label ID="Label120" runat="server" Text="メール送信内容を確認し、「メール送信」ボタンを押してください。" Class="err"></asp:Label>
                </td>
                <td class="third-cell">
                    <a href="./start.aspx">ホームへ戻る</a>
                </td>
            </tr>
        </table>

<div>
    <asp:Label ID="Label2" runat="server" Text="【FROM】" Class="titl"></asp:Label>
    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
</div>
<div>
    <asp:Label ID="Label4" runat="server" Text="【TO：送り先を選択してください。(必須)】" Class="titl"></asp:Label>
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="MEMBER_NAME" DataValueField="CODE" RepeatDirection="Horizontal" CellSpacing ="5" CellPadding="5"></asp:CheckBoxList>
</div>
<div>
    <asp:Label ID="Label1" runat="server" Text="【CC：送り先を選択してください。(任意)】" Class="titl"></asp:Label>
    <asp:CheckBoxList ID="CheckBoxList2" runat="server" DataSourceID="SqlDataSource1" DataTextField="MEMBER_NAME" DataValueField="CODE" RepeatDirection="Horizontal" CellSpacing ="5" CellPadding="5"></asp:CheckBoxList>
</div>

<!--　メール件名 -->
<div>
    <asp:Label ID="Label5" runat="server" Text="【件名】" Class="titl"></asp:Label>
    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
</div>
<!--　メール本文 -->
<div>
    <asp:Label ID="Label3" runat="server" Text="【本文】" Class="titl"></asp:Label><br />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</div>
<!--　メール本文 --><!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CODE, MEMBER_NAME
FROM M_EXL_CS_MEMBER
WHERE PLACE LIKE '%H%'
AND CODE LIKE 'T%'
UNION
SELECT CODE, MEMBER_NAME
FROM M_EXL_CS_MEMBER
WHERE PLACE LIKE '%H%'
AND CODE LIKE 'E%'
"></asp:SqlDataSource>

</form>

</body>
</html>
