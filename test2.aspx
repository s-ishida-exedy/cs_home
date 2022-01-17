<%@ Page Language="VB" AutoEventWireup="false" CodeFile="test2.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
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
                window.location.href = './test.aspx?id=' + encodeURIComponent(arr[1]);
                return false;
            };
        });
    });
    $(function () {
        $('th').off("click").on("click",function () {
            var text = $(this).data('id');
            if (text == 'home' || text == 'aaa' || text == 'undefined') {
                return false;
            } else {
                window.location.href = './test.aspx?id=' + encodeURIComponent(text);
                return false;
            };
        });
    });
</script>
    <style type="text/css">
        .auto-style8 {
            width: 1024px;
        }
    </style>
</head>
<body class="c2">
<form id="form1" runat="server">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.htmlで行う -->
    <!-- #Include File="header.html" -->

    <div class="auto-style8">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SEQ_NO02,CUST_CD" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="1024px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="ｽﾃｰﾀｽ" />
                <asp:BoundField DataField="Forwarder" HeaderText="Forwarder" SortExpression="Forwarder" />
                <asp:BoundField DataField="SEQ_NO02" HeaderText="No" ReadOnly="True" SortExpression="SEQ_NO02" />
                <asp:BoundField DataField="CUST_CD" HeaderText="客先&lt;BR&gt;コード" ReadOnly="True" SortExpression="CUST_CD" HtmlEncode="False" />
                <asp:BoundField DataField="CONSIGNEE" HeaderText="荷受先名" SortExpression="CONSIGNEE" />
                <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" />
                <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE NO." SortExpression="INVOICE_NO" />
                <asp:BoundField DataField="OFFICIAL_QUOT" HeaderText="建値" SortExpression="OFFICIAL_QUOT" />
                <asp:BoundField DataField="CUT_DATE" HeaderText="カット&lt;BR&gt;日" SortExpression="CUT_DATE" HtmlEncode="False" />
                <asp:BoundField DataField="ETD" HeaderText="出港日" SortExpression="ETD" />
                <asp:BoundField DataField="ETA" HeaderText="到着日" SortExpression="ETA" />
                <asp:BoundField DataField="TWENTY_FEET" HeaderText="20'" SortExpression="TWENTY_FEET" />
                <asp:BoundField DataField="FOURTY_FEET" HeaderText="40'" SortExpression="FOURTY_FEET" />
                <asp:BoundField DataField="LCL_QTY" HeaderText="LCL&lt;BR&gt;物量" SortExpression="LCL_QTY" HtmlEncode="False" />
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

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [t_booking]"></asp:SqlDataSource>
    </div>
    
</form>

</body>
</html>
