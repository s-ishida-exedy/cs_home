<%@ Page Language="VB" AutoEventWireup="false" CodeFile="iv_booking.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(BookingvsIVﾍｯﾀﾞ比較)</title>
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
            width: 600px;
        }
        .second-cell {
            width: 450px;
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
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>ﾌﾞｯｷﾝｸﾞｼｰﾄ vs I/Vﾍｯﾀﾞ 比較結果</h2>  
            </td>
            <td class="second-cell">
                <asp:CheckBox ID="CheckBox1" runat="server" Text ="確認済は表示しない" AutoPostBack="True" />
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
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="1500px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="FIN_FLG" HeaderText="確認&lt;BR&gt;状況" HtmlEncode="False" SortExpression="FIN_FLG">
                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="客先コード" SortExpression="客先コード" HeaderText="客先&lt;BR&gt;コード" HtmlEncode="False" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="IVNo" DataTextField="IVNo" HeaderText="IVNo" DataNavigateUrlFormatString="iv_booking_detail.aspx?id={0}">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="計上日" HeaderText="計上日" ReadOnly="True" SortExpression="計上日" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="積出港" HeaderText="積出港" ReadOnly="True" SortExpression="積出港" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="揚地" HeaderText="揚地" SortExpression="揚地" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="配送先" HeaderText="配送先" SortExpression="配送先" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="荷受地" HeaderText="荷受地" SortExpression="荷受地" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="配送先責任送り先" HeaderText="配送先&lt;BR&gt;責任&lt;BR&gt;送り先" SortExpression="配送先責任送り先" HtmlEncode="False" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="カット日" HeaderText="カット日" SortExpression="カット日" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="到着日" HeaderText="到着日" SortExpression="到着日" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="入出港日" HeaderText="入出港日" SortExpression="入出港日" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="VOYAGENo" HeaderText="VOYAGE&lt;BR&gt;No" SortExpression="VOYAGENo" ReadOnly="True" HtmlEncode="False" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="船社" HeaderText="船社" SortExpression="船社" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="ﾌﾞｯｷﾝｸﾞNo" HeaderText="ﾌﾞｯｷﾝｸﾞNo" ReadOnly="True" SortExpression="ﾌﾞｯｷﾝｸﾞNo">
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="船名" HeaderText="船名" ReadOnly="True" SortExpression="船名">
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
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
  CUST_CD AS 客先コード
, INVOICE_NO AS IVNo
, CASE ETD WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 計上日
, CASE LOADING_PORT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 積出港
, CASE DISCHARGING_PORT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 揚地
, CASE PLACE_OF_DELIVERY WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 配送先
, CASE PLACE_OF_RECEIPT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 荷受地
, CASE PLACE_CARRIER WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 配送先責任送り先
, CASE CUT_DATE WHEN '1' THEN 'ＮＧ' ELSE '-' END AS カット日
, CASE ETA WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 到着日
, CASE IOPORTDATE WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 入出港日
, CASE VOYAGE_NO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS VOYAGENo
, CASE BOOK_TO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 船社
, CASE BOOKING_NO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS ﾌﾞｯｷﾝｸﾞNo
, CASE VESSEL_NAME WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 船名
, CASE FIN_FLG WHEN '1' THEN '確認済' ELSE '未' END AS FIN_FLG 
FROM T_COMPARE_INV_HD
WHERE SHIP_METHOD = '0'
ORDER BY INVOICE_NO "></asp:SqlDataSource>
    
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
