﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="air_estimate.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(AIR見積り依頼)</title>
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
        .auto-style2 {
            width: 250px;
            text-align:right;
        }
        .auto-style4 {
            text-align:left;
            font-size:larger;
            font-weight : 700;
            width: 350px;
        }
        .auto-style7 {
            width: 680px;
        }   
        .ta3 th{
            background: #e6e6fa;
        }
        .err{
            color:red;
            font-weight :700;
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
<!-- メニューの編集はheader.htmlで行う -->
 <!-- #Include File="header.html" -->
       
<div id="contents2" class="inner2">
        <table>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label3" runat="server" Text="【AIR見積り依頼】" ></asp:Label>    
                </td>
                <td class="auto-style7">
                    <asp:Button ID="Button1" runat="server" Text="メール作成" style="width:164px" />
                    <asp:Button ID="Button2" runat="server" Text="メール送信" style="width:164px" />&nbsp;
                    <asp:Label ID="Label12" runat="server" Text="Label" Class="err"></asp:Label>
                </td>
                <td class="auto-style2">
                    <a href="./start.aspx">ホームへ戻る</a>
                </td>
            </tr>
        </table>
<div id="main2" style="width:100%;height:520px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
        <table class="ta3">
            <tr>
                <th>依頼内容</th>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="130px">
                        <asp:ListItem Value="0">見積り</asp:ListItem>
                        <asp:ListItem Value="1">集荷</asp:ListItem>
                        <asp:ListItem Value="2">集荷見積り</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>集荷場所</th>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="130px">
                        <asp:ListItem Value="0">本社</asp:ListItem>
                        <asp:ListItem Value="1">上野</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>客先コード</th>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="130px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>建値</th>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server" Width="130px">
                        <asp:ListItem Value="EX-WORKS">EX-WORKS</asp:ListItem>
                        <asp:ListItem Value="DDP">DDP</asp:ListItem>
                        <asp:ListItem Value="DDU">DDU</asp:ListItem>
                        <asp:ListItem Value="FOB">FOB</asp:ListItem>
                        <asp:ListItem Value="CIF">CIF</asp:ListItem>
                        <asp:ListItem Value="OTHER">OTHER</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>総重量</th>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" Width="104px"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="Kg"></asp:Label>
                </td>
                <th>個口</th>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server" Width="95px"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Text="個口"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>ｻｲｽﾞ(縦)</th>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="63px"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="cm"></asp:Label>
                </td>
                <th>ｻｲｽﾞ(横)</th>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="63px"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="cm"></asp:Label>
                </td>
                <th>ｻｲｽﾞ(高さ)</th>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="63px"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="cm"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>集荷日</th>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" Class="date2" Width="130px"></asp:TextBox>
                </td>
                <th>到着希望日</th>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server" Class="date2" Width="130px"></asp:TextBox>
                </td>
                <th>集荷時間</th>
                <td>
                    <asp:DropDownList ID="DropDownList4" runat="server" Width="130px">
                        <asp:ListItem Value="13:00">13:00</asp:ListItem>
                        <asp:ListItem Value="13:30">13:30</asp:ListItem>
                        <asp:ListItem Value="14:00">14:00</asp:ListItem>
                        <asp:ListItem Value="14:30">14:30</asp:ListItem>
                        <asp:ListItem Value="15:00">15:00</asp:ListItem>
                        <asp:ListItem Value="15:30">15:30</asp:ListItem>
                        <asp:ListItem Value="16:00">16:00</asp:ListItem>
                        <asp:ListItem Value="16:30">16:30</asp:ListItem>
                        <asp:ListItem Value="17:00">17:00</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>添付参照</th>
                <td>
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text ="：重量/個口は添付参照" />&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" Text ="：サイズは添付参照" />&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="true" text="：集荷日/到着日は添付参照" />
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>添付ファイル</th>
                <td>
                    <asp:CheckBox ID="CheckBox4" runat="server" Text="：添付ファイルあり" />
                </td>
                <th>メール送信</th>
                <td>
                    <asp:CheckBox ID="CheckBox5" runat="server" Text="：確認画面後、自動でメール送信されます。" />
                </td>
            </tr>
            <tr>
                <th>CC追加</th>
                <td>
                    <asp:CheckBox ID="CheckBox6" runat="server" AutoPostBack="true" Text="：CCアドレス追加" />
                </td>
                <th>CCアドレス</th>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" Width="200px"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="複数の場合「;」で区切る"></asp:Label>
                </td>
            </tr>

            <tr>
                <th>連絡事項</th>
                <td colspan ="3">
                    <asp:TextBox ID="TextBox2" runat="server" Class="date2" Width="1080px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>