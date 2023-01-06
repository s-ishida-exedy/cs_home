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
            width: 350px;
        }
        .second-cell {
            width: 750px;
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
        .txtb{
            padding: 5px;
            font-size :small ;
            width :80px;
            text-align :center ;
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
    function setBg(tr, color) {
        tr.style.backgroundColor = color;
    }
</script>

    <script type="text/javascript">
      function LinkClick() {
          var url = 'm_booking_colorkbn.aspx?q='
          var result = confirm('別ウインドウでを開きます');

          if (result) {
              window.open(url, null);
          }
          else {
          }
      }
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
                <h2>Booking Sheet</h2> 
            </td>
            <td class="second-cell">
                <asp:DropDownList ID="DropDownList1" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource3" DataTextField="STATUS" DataValueField="STATUS" AutoPostBack ="True" Width ="100px"></asp:DropDownList> 
                <asp:DropDownList ID="DropDownList2" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource2" DataTextField="Forwarder" DataValueField="Forwarder" AutoPostBack ="true" Width ="200px"></asp:DropDownList> 
                &nbsp;
                <asp:Label ID="Label1" runat="server" Text="客先CD:"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Class ="txtb"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="IVNO:"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Class ="txtb"></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" Text=" 絞込 " Font-Size="Small" Width ="80" />
                <asp:Button ID="Button2" runat="server" Text=" ﾘｾｯﾄ " Font-Size="Small" Width ="80" />
                <asp:Button class="button01"  ID="Button4" runat="server" Text="ｴｸｾﾙ出力" Width="80" AutoPostBack="True" Font-Size="Small" /> 
            </td>
            <td class="third-cell">
                <asp:Label ID="Label2" runat="server" Text="Labe2" ></asp:Label>
                <a href="javascript:void(0);" onclick="LinkClick()">BS色ﾏｽﾀ</a>  
            </td>
        </tr>
    </table>


<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>

<tbody>

<%--<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>


        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" width="2980px" DataKeyNames="SEQ_NO02,CUST_CD" DataSourceID="SqlDataSource1" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowColSizing="True">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="ｽﾃｰﾀｽ" >
                <HeaderStyle Width="100px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="Forwarder" HeaderText="海貨業者" SortExpression="Forwarder" >
                <ItemStyle Font-Size="Small"  />
                </asp:BoundField>
                <asp:BoundField DataField="CUST_CD" HeaderText="客先" SortExpression="CUST_CD" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="CONSIGNEE" HeaderText="荷受先名" SortExpression="CONSIGNEE" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" >
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
                <asp:BoundField DataField="FINAL_VAN" HeaderText="最終VAN">
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="TWENTY_FEET" HeaderText="20FT" SortExpression="TWENTY_FEET" >
                <HeaderStyle Width="100px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="FOURTY_FEET" HeaderText="40FT" SortExpression="FOURTY_FEET" >
                <HeaderStyle Width="100px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="LCL_QTY" HeaderText="LCL" SortExpression="LCL_QTY" >
                <HeaderStyle Width="100px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="BOOKING_NO" HeaderText="ﾌﾞｯｷﾝｸﾞNO" SortExpression="BOOKING_NO" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="VESSEL_NAME" HeaderText="船名" SortExpression="VESSEL_NAME" >
                <HeaderStyle Width="200px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="VOYAGE_NO" HeaderText="VoyNo" SortExpression="VOYAGE_NO" >
                <HeaderStyle Width="100px" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="SEQ_NO02" HeaderText="近鉄はHBLNO" SortExpression="SEQ_NO02" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="PONO" HeaderText="PONO" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="ROW_KBN" HeaderText="ROW_KBN" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>

            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>

</tbody>
</table>
</div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT 
    a.STATUS
  , a.Forwarder
  , a.CUST_CD 
  , a.CONSIGNEE
  , a.DESTINATION
  , a.INVOICE_NO 
  , a.CUT_DATE
  , a.ETD
  , a.ETA
  , a.TWENTY_FEET
  , a.FOURTY_FEET
  , a.LCL_QTY
  , a.BOOKING_NO
  , a.VESSEL_NAME
  , a.VOYAGE_NO
  , a.SEQ_NO02
  , a.PONO
  , MAX(TBL.VAN_DATE) AS FINAL_VAN 
  , a.ROW_KBN
FROM T_BOOKING a
INNER JOIN
  ( SELECT CUST_CD, INVOICE_NO, DAY01 AS VAN_DATE FROM T_BOOKING  
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY02 AS VAN_DATE FROM T_BOOKING  
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY03 AS VAN_DATE FROM T_BOOKING 
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY04 AS VAN_DATE FROM T_BOOKING 
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY05 AS VAN_DATE FROM T_BOOKING 
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY06 AS VAN_DATE FROM T_BOOKING 
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY07 AS VAN_DATE FROM T_BOOKING 
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY08 AS VAN_DATE FROM T_BOOKING 
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY09 AS VAN_DATE FROM T_BOOKING 
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY10 AS VAN_DATE FROM T_BOOKING 
    UNION ALL  
    SELECT CUST_CD, INVOICE_NO, DAY11 AS VAN_DATE FROM T_BOOKING) AS TBL  
	ON a.CUST_CD = TBL.CUST_CD AND a.INVOICE_NO = TBL.INVOICE_NO
WHERE 
  a.INVOICE_NO &lt;&gt; ''  
  AND TBL.VAN_DATE &lt;&gt; '' 
GROUP BY 
      a.STATUS  , a.Forwarder , a.CUST_CD , a.CONSIGNEE , a.DESTINATION
  , a.INVOICE_NO  , a.CUT_DATE , a.ETD , a.ETA , a.TWENTY_FEET
  , a.FOURTY_FEET , a.LCL_QTY , a.BOOKING_NO , a.VESSEL_NAME
  , a.VOYAGE_NO , a.SEQ_NO02 , a.PONO,ROW_KBN
"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [Forwarder] FROM [T_BOOKING]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT STATUS FROM T_BOOKING"></asp:SqlDataSource>
<%--</div>--%>
<!--/#main2-->


</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
