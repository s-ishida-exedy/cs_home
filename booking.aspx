<%@ Page Language="VB" AutoEventWireup="false" CodeFile="booking.aspx.vb" Inherits="cs_home" %>

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
        .header-ta {
            width: 1300px;
        }
        .first-cell {
            text-align:left;
            font-size:25px;
            width: 250px;
        }
        .second-cell {
            width: 850px;
        }   
        .third-cell {
            width: 250px;
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

       　/*追加*/
        table{
          width: 100%;

        }

        th {
          position: sticky;
          top: 0;
          z-index: 0;
          background-color: #000084;
          color: #ffffff;
                    	/*border-top: 0px solid #999;
	border-left: 0px solid #999;*/
        }
        .wrapper {
          overflow: scroll;
          height: 500px;
        }
        /*追加*/


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
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>Booking Sheet</h2> 
            </td>
            <td class="second-cell">
                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource2" DataTextField="Forwarder" DataValueField="Forwarder" AutoPostBack ="true" Width ="200px"></asp:DropDownList> 
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [Forwarder] FROM [T_BOOKING]"></asp:SqlDataSource>
                &nbsp;
                <asp:Label ID="Label1" runat="server" Text="客先CD:"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width ="100"></asp:TextBox>&nbsp;
                <asp:Label ID="Label3" runat="server" Text="IVNO:"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width ="100"></asp:TextBox>&nbsp;
                <asp:Button ID="Button1" runat="server" Text=" 絞込 " Font-Size="Small" Width ="80" />
                <asp:Button ID="Button2" runat="server" Text=" ﾘｾｯﾄ " Font-Size="Small" Width ="80" />
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

<%--<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>


        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SEQ_NO02,CUST_CD" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="1980px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowSorting="True">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="ｽﾃｰﾀｽ" >
                <HeaderStyle Width="60px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="Forwarder" HeaderText="海貨業者" SortExpression="Forwarder" >
                <HeaderStyle Width="100px" />
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
