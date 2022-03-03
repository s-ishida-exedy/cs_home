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
<%--Datepicker用--%>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
<script src="//code.jquery.com/jquery-1.10.2.js"></script>
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
<%--Datepicker用--%>
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
            width: 700px;
        }   
        .third-cell {
            width: 200px;
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
        .DropDownList{
            text-align :center;
            font-size :small;
        }
</style>
<script>
    // カレンダー
    jQuery(function ($) {
        $(".date2").datepicker({
            dateFormat: 'yy/mm/dd',
            showButtonPanel: true
        });
    });
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
                <h2>海外売上確定チェック</h2>  
            </td>
            <td class="second-cell">
                <asp:DropDownList ID="DropDownList1" class="DropDownList" runat="server" AppendDataBoundItems="True" AutoPostBack="true" Width ="100px">
                    <asp:ListItem Selected="True" Value="H">本社</asp:ListItem>
                    <asp:ListItem Value="U">上野</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource2" DataTextField="SHIPCD" DataValueField="SHIPCD" AutoPostBack ="true" Width ="150px"></asp:DropDownList> 
                <asp:TextBox ID="TextBox1" runat="server" Width="160px" Height="18px" class="date2" Font-Size="small"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="～"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="160px" Height="18px" class="date2" Font-Size="small"></asp:TextBox>
                <br />
                <asp:CheckBox ID="CheckBox1" runat="server" text="一括出力済みのみ表示する" Font-Size="Small" AutoPostBack ="true" />
                &nbsp;&nbsp;
                <asp:Button ID="Button3" runat="server" Text="検　索" Font-Size="Small" Width ="100px" />&nbsp;
                <asp:Button ID="Button1" runat="server" Text="リセット" Font-Size="Small" Width ="100px" />
            </td>
            <td class="third-cell">
                <a href="./start.aspx">ホームへ戻る</a>
            </td>
        </tr>
    </table>
<div id="main2" style="width:100%;height:450px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1280px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowHeaderWhenEmpty="True">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="BLDATE" HeaderText="BLDATE" ReadOnly="True" SortExpression="BLDATE" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OLD_INVNO" HeaderText="IVNO" SortExpression="OLD_INVNO" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTCODE" HeaderText="客先CD" SortExpression="CUSTCODE" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTNAME" HeaderText="客先" SortExpression="CUSTNAME" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIPCD" HeaderText="出荷方法" SortExpression="SHIPCD">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="REGPERSON" HeaderText="登録者CD" SortExpression="REGPERSON" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="REGNAME" HeaderText="登録者" SortExpression="REGPERSON" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUTDATE" HeaderText="カット日" ReadOnly="True" SortExpression="CUTDATE" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ALLOUTSTAMP" HeaderText="一括出力日" SortExpression="ALLOUTSTAMP" >
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
</div>
<!--/#main2-->

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KBHWPA85ConnectionString %>" SelectCommand="SELECT
  FORMAT(T_INV_HD_TB.BLDATE,'yyyy/MM/dd') AS BLDATE
  , T_INV_HD_TB.OLD_INVNO
  , T_INV_HD_TB.CUSTCODE
  , T_INV_HD_TB.CUSTNAME
  , CASE T_INV_HD_TB.SHIPCD 
      WHEN '01' THEN '20ft'
      WHEN '02' THEN '40ft'
      WHEN '03' THEN 'CFS'
      WHEN '05' THEN 'AIR'
      WHEN '06' THEN 'COURIER'
    END AS SHIPCD
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
  , T_INV_HD_TB.SHIPCD
  , T_INV_HD_TB.REGPERSON
  , T_INV_HD_TB.CUTDATE
  , T_INV_HD_TB.ALLOUTSTAMP
  , T_INV_HD_TB.SALESFLG 
HAVING
    T_INV_HD_TB.CUSTCODE &lt;&gt; '111' And T_INV_HD_TB.CUSTCODE &lt;&gt; 'A121'
    AND T_INV_HD_TB.SALESFLG Is Null
ORDER BY  T_INV_HD_TB.BLDATE"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KBHWPA85ConnectionString %>" SelectCommand="SELECT DISTINCT
  CASE T_INV_HD_TB.SHIPCD 
      WHEN '01' THEN '20ft'
      WHEN '02' THEN '40ft'
      WHEN '03' THEN 'CFS'
      WHEN '05' THEN 'AIR'
      WHEN '06' THEN 'COURIER'
    END AS SHIPCD
FROM
    T_INV_HD_TB 
    INNER JOIN T_INV_BD_TB 
    ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO
GROUP BY
  T_INV_HD_TB.BLDATE
  , T_INV_HD_TB.OLD_INVNO
  , T_INV_HD_TB.CUSTCODE
  , T_INV_HD_TB.CUSTNAME
  , T_INV_HD_TB.SHIPCD
  , T_INV_HD_TB.REGPERSON
  , T_INV_HD_TB.CUTDATE
  , T_INV_HD_TB.ALLOUTSTAMP
  , T_INV_HD_TB.SALESFLG 
HAVING
    T_INV_HD_TB.CUSTCODE &lt;&gt; '111' And T_INV_HD_TB.CUSTCODE &lt;&gt; 'A121'
    AND T_INV_HD_TB.SALESFLG Is Null
	AND SHIPCD IS NOT NULL"></asp:SqlDataSource>

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
