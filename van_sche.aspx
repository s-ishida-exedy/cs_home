<%@ Page Language="VB" AutoEventWireup="false" CodeFile="van_sche.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(バンニングスケジュール)</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<style type="text/css">
        .auto-style1 {
            width: 1280px;
        }
        .auto-style2 {
            width: 300px;
            text-align:right;
        }
        .auto-style4 {
            text-align:left;
            font-size:larger;
            font-weight : 700;
            width: 300px;
        }
        .auto-style7 {
            width: 680px;
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
<!-- メニューの編集はheader.htmlで行う -->
 <!-- #Include File="header.html" -->
       
<div id="contents2" class="inner2">
    <table class="auto-style1" >
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label3" runat="server" Text="【バンニングスケジュール】" ></asp:Label>    
            </td>
            <td class="auto-style7">
                <asp:Button ID="Button1" runat="server" Text="ファイルダウンロード" />&nbsp;
                <asp:Label ID="Label1" runat="server" Text="絞り込み："></asp:Label>
                <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="True" AutoPostBack="true" Width ="164px" DataSourceID="SqlDataSource3" DataTextField="VAN_DATE" DataValueField="VAN_DATE"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" AutoPostBack="true" Width ="164px" DataSourceID="SqlDataSource2" DataTextField="場所" DataValueField="場所"></asp:DropDownList>
            </td>
            <td class="auto-style2">
                <asp:Label ID="Label2" runat="server" Text="Labe2" ></asp:Label>
            </td>
        </tr>
    </table>
<div id="main2" style="width:100%;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1280px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="場所" HeaderText="場所" ReadOnly="True" SortExpression="場所" />
            <asp:BoundField DataField="客先名" HeaderText="客先名" SortExpression="客先名" />
            <asp:BoundField DataField="VAN日" HeaderText="VAN日" SortExpression="VAN日" />
            <asp:BoundField DataField="ｽﾀｰﾄ" HeaderText="ｽﾀｰﾄ" SortExpression="ｽﾀｰﾄ" />
            <asp:BoundField DataField="コンテナサイズ" HeaderText="コンテナサイズ" SortExpression="コンテナサイズ" />
            <asp:BoundField DataField="インボイスNO" HeaderText="インボイスNO" SortExpression="インボイスNO" />
            <asp:BoundField DataField="カット日" HeaderText="カット日" SortExpression="カット日" />
            <asp:BoundField DataField="ＥＴＤ" HeaderText="ＥＴＤ" SortExpression="ＥＴＤ" />
        </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT
  CASE PLACE
  	WHEN '0H' THEN '本社'
  	WHEN '1U' THEN '上野'
  	WHEN '2A' THEN 'AIR'
  END AS 場所
  , CUST_NM AS 客先名
  , VAN_DATE AS VAN日
  , VAN_TIME AS ｽﾀｰﾄ
  , CON_SIZE AS コンテナサイズ
  , IVNO AS インボイスNO
  , CUT_DATE AS カット日
  , ETD AS ＥＴＤ
FROM
  T_EXL_VAN_SCH_DETAIL
ORDER BY PLACE"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT
  CASE PLACE
  	WHEN '0H' THEN '01:本社'
  	WHEN '1U' THEN '02:上野'
  	WHEN '2A' THEN '03:AIR'
  END AS 場所
FROM
  T_EXL_VAN_SCH_DETAIL
ORDER BY 場所"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT VAN_DATE FROM T_EXL_VAN_SCH_DETAIL
ORDER BY VAN_DATE"></asp:SqlDataSource>
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
