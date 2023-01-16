﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_work00_detail.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(メール配信業務ﾏｽﾀ)</title>
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
            width: 520px;
        }
        .second-cell {
            width: 680px;
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
        .ta3 th{
            background: #e6e6fa;
        }
        .err{
            color:red;
            font-weight :700;
        }
        .txtb{
            width:300px;
            padding: 5px;
            font-size :small ;
        }
        .cmb{
            width:315px;
            padding: 5px;
            font-size :small ;
        }
                table{
          width: 100%;
        }
        th {
          position: sticky;
          top: 0;
          z-index: 0;
          background-color: #FFFFFF;
          color: #000000;
        }
        .wrapper {
          overflow: scroll;
          height: 300px;
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
<form id="form1" runat="server" autocomplete="off">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>メール配信業務ﾏｽﾀ詳細</h2>  
            </td>
            <td class="second-cell">
                <asp:Button ID="Button1" runat="server" Text="登　録" style="width:120px" Font-Size="Small" />&nbsp;
                <asp:Button ID="Button7" runat="server" Text="更　新" style="width:120px" Font-Size="Small" />&nbsp;
<%--                <asp:Button ID="Button8" runat="server" Text="削　除" style="width:120px" Font-Size="Small" />&nbsp;--%>
                <asp:Label ID="Label3" runat="server" Text="Label" Class="err"></asp:Label>
            </td>
            <td class="third-cell">
                <a href="./m_work00.aspx">一覧に戻る</a>
            </td>
        </tr>
    </table>
<div id="main2" style="width:100%; height:450px;border:None;">

    <table class="ta3">
        <tr>
            <th>ID</th>
            <td>
                <asp:TextBox ID="TextBox0" runat="server" class="txtb" style="width:150px"></asp:TextBox>    
            </td>
            <th>項目名</th>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" class="txtb" style="width:100px"></asp:TextBox>
            </td>
            <th>設定</th>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" class="cmb" style="width:100px">
                    <asp:ListItem>有効</asp:ListItem>
                    <asp:ListItem>無効</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>備考</th>
            <td  colspan="5">
                <asp:TextBox ID="TextBox2" runat="server" class="txtb" style="width:1000px"></asp:TextBox>                
            </td>
        </tr>
    </table>
    
    
    
    
    <asp:Label ID="Label1" runat="server" Text="＜メール送付先設定＞" Class="err"></asp:Label>

    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

    <div class="wrapper">
    <table class="sticky">
    <thead class="fixed">

    </thead>

    <tbody>


    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" Width = "800px" DataKeyNames="MAIL_ADD" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="3px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black"  ShowHeaderWhenEmpty="True">
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="#000084" />

    <Columns>



    <asp:TemplateField ShowHeader="False" HeaderText="設定">
        <HeaderStyle BackColor="#6B696B"  Width="60px" />
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList1" runat="server">
<%--                <asp:ListItem Value="4">なし</asp:ListItem>
                <asp:ListItem Value="1">宛先</asp:ListItem>
                <asp:ListItem Value="2">CC</asp:ListItem>
                <asp:ListItem Value="3">BCC</asp:ListItem>--%>
                </asp:DropDownList>

            </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>


    <asp:BoundField DataField="MAIL_ADD" HeaderText="アドレス" SortExpression="MAIL_ADD"  >
        <ItemStyle HorizontalAlign="left" />
        <HeaderStyle BackColor="#6B696B"  Width="150px" />
        </asp:BoundField>
    <asp:BoundField DataField="COMPANY" HeaderText="会社名" SortExpression="COMPANY"  >
        <HeaderStyle BackColor="#6B696B"  Width="120px" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    <asp:BoundField DataField="IN_CHARGE_NAME" HeaderText="担当者名" SortExpression="IN_CHARGE_NAME"  >
        <HeaderStyle BackColor="#6B696B"  Width="80px" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    <asp:BoundField DataField="FLG" HeaderText="FLG00" SortExpression="FLG"  >
        <HeaderStyle BackColor="#6B696B"  Width="10px" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>

    </asp:GridView>

    </tbody>
    </table>
    </div>

 
    </asp:Panel>  


        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT MAIL_ADD, COMPANY, IN_CHARGE_NAME, FLG FROM M_EXL_MAIL01"></asp:SqlDataSource>


</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
