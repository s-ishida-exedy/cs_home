<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eir_comfirm_yard.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(EIR,Booking差異 ｺﾝﾃﾅﾔｰﾄﾞ確認)</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
</head>
<body class="c2">
<form id="form1" runat="server">
<!--PC用（901px以上端末）メニュー-->
<%--コンテナヤードではマウス、キーボード操作を行わない為、ヘッダなし--%>
       
<div id="contents2" class="inner2">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Width="1300px" CellPadding="3" GridLines="Vertical" ForeColor="Black" BackColor="White"  >
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="VAN_DATE" HeaderText="VAN日" SortExpression="VAN_DATE" >
            <HeaderStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="MAIL_TITLE" HeaderText="タイトル" SortExpression="MAIL_TITLE" >
            <HeaderStyle Width="180px" />
            </asp:BoundField>
            <asp:BoundField DataField="VOYNO02" HeaderText="VoyNO" SortExpression="VOYNO02">
            </asp:BoundField>
            <asp:BoundField DataField="VESSEL02" HeaderText="本船名" SortExpression="VESSEL02">
            </asp:BoundField>
            <asp:BoundField DataField="BOOKING02" HeaderText="ブッキングNO" SortExpression="BOOKING02">
            </asp:BoundField>
            <asp:BoundField DataField="CONTAINER02" HeaderText="コンテナサイズ" SortExpression="CONTAINER02">
            </asp:BoundField>
            <asp:BoundField DataField="ETC01" HeaderText="その他(誤)" SortExpression="ETC01">
            </asp:BoundField>
            <asp:BoundField DataField="ETC02" HeaderText="その他(正)" SortExpression="ETC02">
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" 
SelectCommand="SELECT VAN_DATE, MAIL_TITLE, VOYNO02, VESSEL02, BOOKING02, CONTAINER02, ETC01, ETC02
FROM T_EXL_EIR_COMF
WHERE STATUS = '1'
AND VAN_DATE = CONVERT(NVARCHAR,  GETDATE(), 111)
ORDER BY MAIL_TITLE">
    </asp:SqlDataSource>
</div>    
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
