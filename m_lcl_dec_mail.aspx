<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_lcl_dec_mail.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(通関・LCL関係ｱﾄﾞﾚｽﾏｽﾀ)</title>
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
            width: 470px;
        }
        .second-cell {
            width: 730px;
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
       　/*ヘッダー固定用*/
        table{
          width: 100%;
        }

        th {
          position: sticky;
          top: 0;
          z-index: 0;
          background-color: #000084;
          color: #ffffff;
        }
        .wrapper {
          overflow: scroll;
          height: 460px;
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
                <h2>通関・LCL関係アドレスマスタ</h2> 
            </td>
            <td class="second-cell">
                <asp:Label ID="Label1" runat="server" Text="なし:"></asp:Label>
                <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="True" Width ="100px" DataSourceID="SqlDataSource2" DataTextField="KBN" DataValueField="KBN"></asp:DropDownList>&nbsp;
                <asp:Label ID="Label3" runat="server" Text="ﾒｰﾙｱﾄﾞﾚｽ:"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width ="300"></asp:TextBox>&nbsp;
                <asp:Label ID="Label2" runat="server" Text="場所:"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" Width ="100px" DataSourceID="SqlDataSource3" DataTextField="KBN" DataValueField="KBN"></asp:DropDownList>&nbsp;
                <br/>
                <asp:Button ID="Button1" runat="server" Text="検索" Font-Size="Small" Width ="80" />&nbsp;
                <asp:Button ID="Button2" runat="server" Text="ﾘｾｯﾄ" Font-Size="Small" Width ="80" />&nbsp;
                <asp:Button ID="Button3" runat="server" Text="新規登録" Font-Size="Small" Width ="80" />
            </td>
            <td class="third-cell">
                <a href="./start.aspx">ホームへ戻る</a>
            </td>
        </tr>
    </table>


<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>

<tbody>


    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" ShowHeaderWhenEmpty="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="edt" ImageUrl="~/icon/write.png" Text="編集" />
                </ItemTemplate>
                <HeaderStyle BackColor="#6B696B" />
            </asp:TemplateField>
            <asp:BoundField DataField="CODE" HeaderText="通番" ReadOnly="True" SortExpression="CODE" InsertVisible="False" >
            <HeaderStyle BackColor="#6B696B" Width="60px" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="MAIL_ADD" HeaderText="メールアドレス" SortExpression="MAIL_ADD" >
            <HeaderStyle BackColor="#6B696B" Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="KBN" HeaderText="場所" SortExpression="KBN" >
            <HeaderStyle BackColor="#6B696B" Width="60px" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TO_CC" HeaderText="海貨業者" SortExpression="TO_CC" >
            <HeaderStyle BackColor="#6B696B" Width="150px" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CODE, MAIL_ADD, 
CASE KBN
 WHEN '0' THEN '販促品'
 WHEN '1' THEN 'LCL展開'
 WHEN '2' THEN '郵船委託'
 WHEN '3' THEN '近鉄委託'
END AS KBN
,CASE TO_CC
 WHEN '0' THEN 'CC'
 WHEN '1' THEN '宛先'
END AS TO_CC
, REF
 FROM [M_EXL_LCL_DEC_MAIL]"></asp:SqlDataSource>
    
<%--    無し--%>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT
   KBN
FROM M_EXL_LCL_DEC_MAIL
"></asp:SqlDataSource>
    
    
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT 
CASE KBN
 WHEN '0' THEN '販促品'
 WHEN '1' THEN 'LCL展開'
 WHEN '2' THEN '郵船委託'
 WHEN '3' THEN '近鉄委託'
END AS KBN
FROM M_EXL_LCL_DEC_MAIL
ORDER BY KBN DESC"></asp:SqlDataSource>
    
    
<%--</div>--%>
<!--/#main2-->

</tbody>
</table>
</div>

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
