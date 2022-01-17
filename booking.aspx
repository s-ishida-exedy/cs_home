<%@ Page Language="VB" AutoEventWireup="false" CodeFile="booking.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(BOOKING SHEET)</title>
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
            width: 400px;
            text-align:right;
        }
        .auto-style4 {
            text-align:left;
            font-size:larger;
            font-weight : 700;
            width: 400px;
        }
        .auto-style7 {
            width: 480px;
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
                    <asp:Label ID="Label3" runat="server" Text="Booking Sheet" ></asp:Label>    
                </td>
                <td class="auto-style7">
                    
                </td>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Labe2" ></asp:Label>
                </td>
            </tr>
        </table>
<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SEQ_NO02,CUST_CD" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="1980px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowSorting="True">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="ｽﾃｰﾀｽ" >
                <HeaderStyle Width="60px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="Forwarder" HeaderText="Forwarder" SortExpression="Forwarder" >
                <HeaderStyle Width="50px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="CUST_CD" HeaderText="客先" ReadOnly="True" SortExpression="CUST_CD" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="CONSIGNEE" HeaderText="荷受先名" SortExpression="CONSIGNEE" >
                <HeaderStyle Width="250px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" >
                <HeaderStyle Width="100px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="INVOICE_NO" HeaderText="IVNO" SortExpression="INVOICE_NO" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="CUT_DATE" HeaderText="ｶｯﾄ日" SortExpression="CUT_DATE" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="ETD" HeaderText="出港日" SortExpression="ETD" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="ETA" HeaderText="到着日" SortExpression="ETA" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="TWENTY_FEET" HeaderText="20FT" SortExpression="TWENTY_FEET" >
                <HeaderStyle Width="80px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="FOURTY_FEET" HeaderText="40FT" SortExpression="FOURTY_FEET" >
                <HeaderStyle Width="80px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="LCL_QTY" HeaderText="LCL" SortExpression="LCL_QTY" >
                <HeaderStyle Width="80px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="BOOKING_NO" HeaderText="ﾌﾞｯｷﾝｸﾞNO" SortExpression="BOOKING_NO" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="VESSEL_NAME" HeaderText="船名" SortExpression="VESSEL_NAME" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="VOYAGE_NO" HeaderText="VoyNo" SortExpression="VOYAGE_NO" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
<%--            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />--%>
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [t_booking]"></asp:SqlDataSource>
    
        <br />
    
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
