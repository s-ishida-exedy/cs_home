<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sales_comfirm.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(海外売上確定チェック)</title>
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
            width: 250px;
        }
        .auto-style7 {
            width: 730px;
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
                <asp:Label ID="Label3" runat="server" Text="【海外売上確定チェック】" ></asp:Label>    
            </td>
            <td class="auto-style7">
                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" AutoPostBack="true" Width ="120px">
                    <asp:ListItem Selected="True" Value="H">本社</asp:ListItem>
                    <asp:ListItem Value="U">上野</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="cal" />
                &nbsp;
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="cal" />
                &nbsp;
                <asp:Button ID="Button3" runat="server" Text="検　索" />
            </td>
            <td class="auto-style2">
                    <a href="#" onclick="window.history.back(); return false;">前のページに戻る</a>
            </td>
        </tr>
    </table>
<div id="main2" style="width:100%;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1280px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="BLDATE" HeaderText="BLDATE" ReadOnly="True" SortExpression="BLDATE" />
                <asp:BoundField DataField="OLD_INVNO" HeaderText="インボイス番号" SortExpression="OLD_INVNO" />
                <asp:BoundField DataField="CUSTNAME" HeaderText="客先" SortExpression="CUSTNAME" />
                <asp:BoundField DataField="REGPERSON" HeaderText="登録者CD" SortExpression="REGPERSON" />
                <asp:BoundField DataField="REGNAME" HeaderText="登録者" SortExpression="REGPERSON" />
                <asp:BoundField DataField="CUTDATE" HeaderText="カット日" ReadOnly="True" SortExpression="CUTDATE" />
                <asp:BoundField DataField="ALLOUTSTAMP" HeaderText="一括出力日" SortExpression="ALLOUTSTAMP" />
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
</div>
<!--/#main2-->

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KBHWPA85ConnectionString %>" SelectCommand="SELECT
  FORMAT(T_INV_HD_TB.BLDATE,'yyyy/MM/dd') AS BLDATE
  , T_INV_HD_TB.OLD_INVNO
  , T_INV_HD_TB.CUSTNAME
  , T_INV_HD_TB.REGPERSON
  , ''  AS REGNAME
  , FORMAT(T_INV_HD_TB.CUTDATE,'yyyy/MM/dd') AS CUTDATE
  , T_INV_HD_TB.ALLOUTSTAMP
FROM
    T_INV_HD_TB 
    INNER JOIN T_INV_BD_TB 
    ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO
GROUP BY
  T_INV_HD_TB.BLDATE
  , T_INV_HD_TB.OLD_INVNO
  , T_INV_HD_TB.CUSTCODE
  , T_INV_HD_TB.CUSTNAME
  , T_INV_HD_TB.REGPERSON
  , T_INV_HD_TB.CUTDATE
  , T_INV_HD_TB.ALLOUTSTAMP
  , T_INV_HD_TB.SALESFLG 
HAVING
    T_INV_HD_TB.CUSTCODE &lt;&gt; '111' And T_INV_HD_TB.CUSTCODE &lt;&gt; 'A121'
    AND T_INV_HD_TB.SALESFLG Is Null
ORDER BY  T_INV_HD_TB.BLDATE;
"></asp:SqlDataSource>

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
