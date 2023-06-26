﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="epa_make_tsv.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(EPA発給TSVﾌｧｲﾙ作成)</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<style type="text/css">
        .header-ta {
            width: 1300px;
        }
        .first-cell {
            text-align:left;
            font-size:25px;
            width: 500px;
        }
        .second-cell {
            width: 600px;
        }   
        .third-cell {
            width: 200px;
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
        .ta3 TH  {
            width :300px;
            background-color: #eee;
        }
        .txtb{
            padding: 5px;
        }
        .err{
            color:red;
            font-weight :700;
            text-align :center ;
        }
</style>
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
</script>
</head>
<body class="c2">
<form id="form1" runat="server">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>EPA発給TSVﾌｧｲﾙ作成</h2> 
            </td>
            <td class="second-cell">
                <asp:Button ID="Button1" runat="server" Text="TSV出力" style="width:164px" Font-Size="Small" />&nbsp;
                <asp:Button ID="Button2" runat="server" Text="クリア" style="width:164px" Font-Size="Small" />&nbsp;
            </td>
            <td class="third-cell">
                <a href="./start.aspx">ホームへ戻る</a>
            </td>
        </tr>
    </table>
<div id="main2" style="width:100%;border:None;">
    <table class="ta3" style="width:60%">
        <tr>
            <th>協定名</th>
            <td>
                <asp:DropDownList ID="DropDownList1" Class ="txtb" runat="server" DataSourceID="SqlDataSource1" DataTextField="COUNTRY" DataValueField="CODE"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>インボイス番号</th>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Class ="txtb"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>第三国インボイス有無</th>
            <td>
                <asp:RadioButton ID="RadioButton1" runat="server" Text ="有" AutoPostBack="True" />&nbsp;&nbsp;
                <asp:RadioButton ID="RadioButton2" runat="server" Text ="無" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <th>第三国インボイス　名称</th>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Width ="400px" Class ="txtb"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>第三国インボイス　所在地</th>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Width ="400px" Height="60px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>第三国インボイス　国コード<br/>デフォルトはシンガポール</th>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Width ="400px" Class ="txtb"></asp:TextBox>
            </td>
        </tr>
    </table>
        <table class="ta3" style="width:60%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="" Class="err"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [M_EXL_EPA_COUNTRY] ORDER BY [CODE]"></asp:SqlDataSource>
    <a href="#">↑</a></p>

</form>

</body>
</html>
