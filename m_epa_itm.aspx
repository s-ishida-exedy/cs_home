<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_epa_itm.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(EPA対象品目ﾏｽﾀ)</title>
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
            width: 350px;
        }
        .second-cell {
            width: 850px;
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
          height: 480px;
        }
        .DropDownList{
            text-align :center;
            font-size :small ;
        }
        .txtb{
            font-size :small ;
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
                <h2>ＥＰＡ対象品目マスタ</h2> 
            </td>
            <td class="second-cell">
                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" Width ="120px" DataSourceID="SqlDataSource2" DataTextField="CODE_N" DataValueField="CODE" class="DropDownList"></asp:DropDownList>
                &nbsp;
                <asp:Label ID="Label1" runat="server" Text="客先品番:"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width ="200" CssClass="txtb"></asp:TextBox>&nbsp;
                <asp:Label ID="Label2" runat="server" Text="品名:"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width ="200" CssClass="txtb"></asp:TextBox>&nbsp;
                <br/>
                <asp:Label ID="Label3" runat="server" Text="判定番号:"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" Width ="100" CssClass="txtb"></asp:TextBox>&nbsp;
                &nbsp;
                <asp:Button ID="Button1" runat="server" Text="検索" Font-Size="Small" Width ="80" />
                <asp:Button ID="Button2" runat="server" Text="ﾘｾｯﾄ" Font-Size="Small" Width ="80" />&nbsp;&nbsp;
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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" DataKeyNames="UNF_CODE">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="edt" ImageUrl="~/icon/write.png" Text="編集" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UNF_CODE" HeaderText="UNF_CODE" SortExpression="UNF_CODE" InsertVisible="False" ReadOnly="True" >
            </asp:BoundField>
            <asp:BoundField DataField="CODE" HeaderText="協定" SortExpression="CODE" >
            <HeaderStyle Width="120px" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CST_ITM_CODE" HeaderText="客先品番" SortExpression="CST_ITM_CODE" >
            <HeaderStyle Width="180px" />
            </asp:BoundField>
            <asp:BoundField DataField="ITM_NAME" HeaderText="品名" SortExpression="ITM_NAME">
            <HeaderStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="ORI_ITM_NAME" HeaderText="出力品名" SortExpression="ORI_ITM_NAME">
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="ORI_JDG_NO" HeaderText="判定番号" SortExpression="ORI_JDG_NO">
            <HeaderStyle Width="120px" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="HS_CODE" HeaderText="HSｺｰﾄﾞ" SortExpression="HS_CODE">
            <HeaderStyle Width="80px" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="REMARKS" HeaderText="品名検索" SortExpression="REMARKS">
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="VACTC" HeaderText="VA/CTC" SortExpression="VACTC">
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#ffffff" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
<%--</div>--%>
<!--/#main2-->

</tbody>
</table>
</div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT 
  UNF_CODE
  , CASE ITM.CODE 
     WHEN '01' THEN '日メキシコ'
     WHEN '02' THEN '日マレーシア'
     WHEN '03' THEN '日チリ'
     WHEN '04' THEN '日タイ'
     WHEN '05' THEN '日インドネシア'
     WHEN '06' THEN '日ブルネイ'
     WHEN '07' THEN '日フィリピン'
     WHEN '08' THEN '日スイス'
     WHEN '09' THEN '日ベトナム'
     WHEN '10' THEN '日インド'
     WHEN '11' THEN '日ペルー'
     WHEN '12' THEN '日オーストラリア'
     WHEN '13' THEN '日モンゴル'
     WHEN '14' THEN '日アセアン'
     WHEN '15' THEN 'ＲＣＥＰ'
  END CODE
  , CST_ITM_CODE
  , ITM_NAME
  , ORI_ITM_NAME
  , ORI_JDG_NO
  , HS_CODE
  , CASE REMARKS
     WHEN '01' THEN '対象'
  END REMARKS
  , VACTC
FROM M_EXL_ORIGIN_ITM ITM
ORDER BY ITM.CODE "></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT
  ITM.CODE
  ,CASE ITM.CODE 
     WHEN '01' THEN '日メキシコ'
     WHEN '02' THEN '日マレーシア'
     WHEN '03' THEN '日チリ'
     WHEN '04' THEN '日タイ'
     WHEN '05' THEN '日インドネシア'
     WHEN '06' THEN '日ブルネイ'
     WHEN '07' THEN '日フィリピン'
     WHEN '08' THEN '日スイス'
     WHEN '09' THEN '日ベトナム'
     WHEN '10' THEN '日インド'
     WHEN '11' THEN '日ペルー'
     WHEN '12' THEN '日オーストラリア'
     WHEN '13' THEN '日モンゴル'
     WHEN '14' THEN '日アセアン'
     WHEN '15' THEN 'ＲＣＥＰ'
  END CODE_N
FROM M_EXL_ORIGIN_ITM ITM
ORDER BY ITM.CODE "></asp:SqlDataSource>

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
