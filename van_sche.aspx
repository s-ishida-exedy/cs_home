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
        .header-ta {
            width: 1300px;
        }
        .first-cell {
            text-align:left;
            font-size:25px;
            width: 400px;
        }
        .second-cell {
            width: 680px;
        }   
        .third-cell {
            width: 220px;
            text-align:right;
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
       　/*GridViewヘッダー固定用*/
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
          height: 450px;
        }
        .DropDownList{
            text-align :center;
            font-size :small;
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
<% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
    <!-- #Include File="header/header.aspx" -->
<% Else %>
    <!-- #Include File="header/exl_header.aspx" -->
<% End If %>
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>バンニングスケジュール</h2> 
            </td>
            <td class="second-cell">
                <asp:Label ID="Label1" runat="server" Text="絞り込み："></asp:Label>
                <asp:DropDownList ID="DropDownList2" class="DropDownList" runat="server" AppendDataBoundItems="True" AutoPostBack="true" Width ="164px" DataSourceID="SqlDataSource3" DataTextField="VAN_DATE" DataValueField="VAN_DATE"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList1" class="DropDownList" runat="server" AppendDataBoundItems="True" AutoPostBack="true" Width ="164px" DataSourceID="SqlDataSource2" DataTextField="場所" DataValueField="場所"></asp:DropDownList>&nbsp;
                <asp:Button ID="Button2" runat="server" Text="ﾘｾｯﾄ" Font-Size="Small" width="60px" />&nbsp;
                <asp:Button ID="Button1" runat="server" Text="ファイルダウンロード" Font-Size="Small" width="160px"/>
            </td>
            <td class="third-cell">
                <asp:Label ID="Label2" runat="server" Text="Labe2" ></asp:Label>
            </td>
        </tr>
    </table>

<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>

<tbody>


<%--<div id="main2" style="width:100%;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1280px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="場所" HeaderText="場所" ReadOnly="True" SortExpression="場所" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="客先名" HeaderText="客先名" SortExpression="客先名" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="VAN日" HeaderText="VAN日" SortExpression="VAN日" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ｽﾀｰﾄ" HeaderText="ｽﾀｰﾄ" SortExpression="ｽﾀｰﾄ" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="コンテナサイズ" HeaderText="コンテナサイズ" SortExpression="コンテナサイズ" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="インボイスNO" HeaderText="インボイスNO" SortExpression="インボイスNO" />
            <asp:BoundField DataField="カット日" HeaderText="カット日" SortExpression="カット日" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ＥＴＤ" HeaderText="ＥＴＤ" SortExpression="ＥＴＤ" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="最終" HeaderText="最終" SortExpression="最終">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
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
  , '' AS 最終
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
