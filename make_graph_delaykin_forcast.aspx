<%@ Page Language="VB" AutoEventWireup="false" CodeFile="make_graph_delaykin_forcast.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(月またぎ遅延による未納予測)</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>


<%--        <script src="scripts/Chart.min.js"></script>--%>

    <style type="text/css">
       #form1
        {
            background-color : #ffffff;
            color : #000000;
        }
        body {
            background-color: #ffffff;
        }
        A.sample1:link { color: blue;}
        A.sample1:visited { color: blue;}
        A.sample1:active { color: blue;}
        A.sample1:hover { color: blue;}
        .auto-style6 {
            margin-right: 7px;
        }

        table{
          width: 100%;
          /*text-align: right;*/
        }
        th {
          position: sticky;
          top: 0;
          z-index: 0;
          background-color: #FFFFFF;
          color: #000000;
        }
        .wrapper {
          overflow: scroll;
          height: 400px;
        }

        .AA {
                  text-align: right;
        }


        .BB {
                  text-align: center;
        }

        .color01 {
        background-color: red;  
        color: white;
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


</script>



</head>
<body class="c2">
<form id="form1" runat="server">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->

       
<div id="contents2" class="inner2">


    <table >
        <tr>
            <td style="width:450px;Font-Size:25px;" align="left" >
                <h2>遅延による未納金額</h2>
            </td>
        </tr>
    </table>

    <table >
        <tr>

            <td style="width:130px">
            <asp:Button ID="Button2" runat="server" Text="月またぎ遅延予測" Visible="true" />
            </td>
            <td style="width:130px" >
            <asp:Button ID="Button1" runat="server" Text="過去遅延実績" Visible="false" />
            </td>
            <td style="width:800px">
            </td>
        </tr>
    </table>

    <table style="height:10px" >
    </table>

    <table >
        <tr>
            <td style="width:450px;Font-Size:23px;" align="left" >
                <asp:Label ID="Label11" runat="server" Text="＜遅延予測＞"></asp:Label>
            </td>
        </tr>
    </table>


    <table >
        <tr>
            <td style="width:450px;Font-Size:15px;" align="left" >
                ●集計
            </td>
        </tr>
    </table>
    <table border='1' style="width:200px;Font-Size:14px;" class ="sample1">
        <thead>
            <tr>
                <td colspan="1" style="width:50px;"><asp:Label ID="Label1" runat="server" Text="区分"></asp:Label></td>
                <td colspan="1" style="width:150px;"><asp:Label ID="Label2" runat="server" Text="未納予測金額（￥）"></asp:Label></td>
            </tr>
        </thead>
        <tbody>
            <tr >
                <td style="width:30px;background-color:lightgreen"><asp:Label ID="Label3" runat="server" Text="MT"></asp:Label></td>
                <td style="width:80px;background-color:lightgreen"><asp:Label ID="Label4" runat="server" Text="0"></asp:Label></td>
            </tr>
            <tr >
                <td style="width:30px;background-color:lightblue"><asp:Label ID="Label5" runat="server" Text="TS"></asp:Label></td>
                <td style="width:80px;background-color:lightblue"><asp:Label ID="Label6" runat="server" Text="0"></asp:Label></td>
            </tr>
            <tr >
                <td style="width:30px;background-color:lightpink"><asp:Label ID="Label7" runat="server" Text="AT"></asp:Label></td>
                <td style="width:80px;background-color:lightpink"><asp:Label ID="Label8" runat="server" Text="0"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td><asp:Label ID="Label9" runat="server" Text="合計"></asp:Label></td>
                <td><asp:Label ID="Label10" runat="server" Text=""></asp:Label></td>
            </tr>
        </tfoot>
    </table>

    <table >
        <tr>
            <td style="width:450px;Font-Size:15px;" align="left" >
                ●案件一覧 (実績 区分：赤)
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server"  Font-Size="12px" class ="AA" Height="200px">

        <div class="wrapper" id="main2" >
        <table class="sticky"   >
        <thead class="fixed" >

        </thead>
    
        <tbody >

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width = "1120px" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >

        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"  />
        <AlternatingRowStyle BackColor="#ffffff" />
        <Columns >

        <asp:BoundField DataField="KBN" HeaderText="区分" SortExpression="KBN" HtmlEncode="False" >
        <HeaderStyle Width="20px" />
        </asp:BoundField>
        <asp:BoundField DataField="COUNTRY" HeaderText="仕向国"　SortExpression="COUNTRY" HtmlEncode="False" >
        <HeaderStyle Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="DISTINATION" HeaderText="仕向地" SortExpression="DISTINATION" HtmlEncode="False" >
        <HeaderStyle Width="120px" />
        </asp:BoundField>
        <asp:BoundField DataField="CUSTCODE" HeaderText="客先コード" SortExpression="CUSTCODE" HtmlEncode="False" >
        <HeaderStyle Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="CUSTNAME" HeaderText="客先名" SortExpression="CUSTNAME" HtmlEncode="False" >
        <HeaderStyle Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="INVOICENO" HeaderText="INVOICENO" SortExpression="INVOICENO" HtmlEncode="False"  >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="ETD_B" HeaderText="ETD（遅延前）" SortExpression="ETD_B" HtmlEncode="False" >
        <HeaderStyle Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="ETD_A" HeaderText="ETD（遅延後）" SortExpression="ETD_A" HtmlEncode="False"  >
        <HeaderStyle Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="DDAYS" HeaderText="遅延日数" SortExpression="DDAYS" HtmlEncode="False"  >
        <HeaderStyle Width="40px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN" HeaderText="未納予測金額(\)" SortExpression="KIN" HtmlEncode="False" >
        <HeaderStyle Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="BOOKING_NO" HeaderText="BOOKING_NO" SortExpression="BOOKING_NO" HtmlEncode="False" >
        <HeaderStyle Width="60px" />
        </asp:BoundField>
        </Columns>

        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_DELAY_FORCAST] WHERE CUSTCODE<>'対象外' "></asp:SqlDataSource>

        </tbody>
        </table>
        </div>
    </asp:Panel>  

</div>




    
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop">
<a href="#">↑</a></p>   



</form>
</body>



</html>
