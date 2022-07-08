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
          width :820px;
        }
        /*追加*/
        .flex{
            display: flex;
        }
        .flex div{
           
        }
        .right{
            width: 360px;
            padding: 10px
        }
        .tab1 {
            border-collapse:collapse;
            border-spacing:0px;
            border:1px solid #000000;
            width:450px;
        }
        .tab1 th{
            border:1px solid #000000;
        }
        .td1{
            border:1px solid #000000;
            width:130px;
            text-align:center;
            font-weight:700;
        }
        .td2{
            border:1px solid #000000;
            width:250px;
            text-align:center;
            font-weight:700;
        }
        .td3{
            border:1px solid #000000;
            width:250px;
            text-align:center;
            font-weight:700;
        }
        .lblA{
            font-weight:700;
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
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>ﾌﾞｯｷﾝｸﾞｼｰﾄ vs I/Vﾍｯﾀﾞ 比較結果</h2>  
            </td>
            <td class="second-cell">
                <asp:Button ID="Button1" runat="server" Text="再読み込み" Width ="200px" Font-Size="Small"/>
            </td>
            <td class="third-cell">               
            </td>
        </tr>
    </table>

<%--    <asp:Panel ID="Panel1" runat="server"  Font-Size="14px">--%>

<div class="flex">
<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>

<tbody>

<%--<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="800px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="CUST_CD" SortExpression="CUST_CD" HeaderText="客先" >
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="INVOICE_NO" SortExpression="INVOICE_NO" HeaderText="IVNO" >
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:ButtonField DataTextField="INVOICE_NO" HeaderText="IVNO" Text="ボタン" CommandName="edt">
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:ButtonField>
                <asp:BoundField DataField="BLDATE" HeaderText="計上日" SortExpression="BLDATE">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="INVFROM" HeaderText="積出港" SortExpression="INVFROM">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="INVON" HeaderText="荷受地" SortExpression="INVON">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="CUTDATE" HeaderText="CUT日" SortExpression="CUTDATE">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="IOPORTDATE" HeaderText="ETD" SortExpression="IOPORTDATE">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="REACHDATE" HeaderText="ETA" SortExpression="REACHDATE">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="VOYAGENO" HeaderText="VOY#" SortExpression="VOYAGENO">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIPPER" HeaderText="船社" SortExpression="SHIPPER">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="BOOKINGNO" HeaderText="BKG#" SortExpression="BOOKINGNO">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIPPEDPER" HeaderText="船名" SortExpression="SHIPPEDPER">
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT
  CUST_CD
  , INVOICE_NO
  , BLDATE
  , INVFROM
  , INVON
  , CUTDATE
  , IOPORTDATE
  , REACHDATE
  , VOYAGENO
  , SHIPPER
  , BOOKINGNO
  , SHIPPEDPER 
FROM  T_EXL_BKGIV 
WHERE
  BLDATE &lt;&gt; '-' 
  OR INVFROM &lt;&gt; '-' 
  OR INVON &lt;&gt; '-' 
  OR CUTDATE &lt;&gt; '-' 
  OR IOPORTDATE &lt;&gt; '-' 
  OR REACHDATE &lt;&gt; '-' 
  OR VOYAGENO &lt;&gt; '-' 
  OR SHIPPER &lt;&gt; '-' 
  OR BOOKINGNO &lt;&gt; '-' 
  OR SHIPPEDPER &lt;&gt; '-' 
ORDER BY INVOICE_NO
"></asp:SqlDataSource>
    
<%--</div>--%>
<!--/#main2-->

</tbody>
</table>

<div class="right">
    <asp:Label ID="Label1" runat="server" Text="客先：" Class="lblA"></asp:Label>&nbsp;
    <asp:Label ID="Label2" runat="server" Text="" Class="lblA"></asp:Label><br/>
    <asp:Label ID="Label3" runat="server" Text="IVNO：" Class="lblA"></asp:Label>&nbsp;
    <asp:Label ID="Label4" runat="server" Text="" Class="lblA"></asp:Label>
    <table class="tab1">
        <tr>
            <th></th>
            <th>ﾌﾞｯｷﾝｸﾞｼｰﾄ</th>
            <th>ｲﾝﾎﾞｲｽﾍｯﾀﾞ</th>
        </tr>
        <tr>
            <td class ="td1">
                計上日
            </td>
            <td class ="td2">
                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class ="td1">
                積出港
            </td>
            <td class ="td2">
                <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
            </td>
        </tr>        <tr>
            <td class ="td1">
                荷受地
            </td>
            <td class ="td2">
                <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
            </td>
        </tr>        <tr>
            <td class ="td1">
                CUT日
            </td>
            <td class ="td2">
                <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
            </td>
        </tr>        <tr>
            <td class ="td1">
                ETD
            </td>
            <td class ="td2">
                <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
            </td>
        </tr>        <tr>
            <td class ="td1">
                ETA
            </td>
            <td class ="td2">
                <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
            </td>
        </tr>        <tr>
            <td class ="td1">
                VOYNO
            </td>
            <td class ="td2">
                <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
            </td>
        </tr>        <tr>
            <td class ="td1">
                船社
            </td>
            <td class ="td2">
                <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
            </td>
        </tr>        <tr>
            <td class ="td1">
                BKGNO
            </td>
            <td class ="td2">
                <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
            </td>
        </tr>        <tr>
            <td class ="td1">
                船名
            </td>
            <td class ="td2">
                <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td3">
                <asp:Label ID="Label24" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</div>

</div>

</div>
<!--/#FLEX-->
    <%--</asp:Panel>--%>

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
