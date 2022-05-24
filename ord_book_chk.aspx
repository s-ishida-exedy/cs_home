<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ord_book_chk.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(ブッキング確認)</title>
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
<script src="https://code.jquery.com/jquery-1.10.2.js"  integrity="sha256-it5nQKHTz+34HijZJQkpNBIHsjpV8b6QzMJs9tmOBSo="  crossorigin="anonymous"></script>
<script src="https://code.jquery.com/ui/1.11.4/jquery-ui.js" integrity="sha256-DI6NdAhhFRnO2k51mumYeDShet3I8AKCQf/tf7ARNhI=" crossorigin="anonymous"></script>
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
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>ブッキング確認</h2>  
            </td>
            <td class="second-cell">
                <p><asp:Label ID="Label1" runat="server" Font-Size ="Small"  Text="※本日以降のETDの受注がブッキングデータに登録されていないＳＮを表示します。"></asp:Label></p>
                <p><asp:Label ID="Label2" runat="server" Font-Size ="Small"  Text="※ＳＮデータが表示された場合、ブッキングが必要です。"></asp:Label>
                &nbsp;&nbsp;<asp:CheckBox ID="CheckBox1" runat="server" autopostback="true" text=" 確認済みを表示する。" Font-Size ="Small" /></p>
            </td>
            <td class="third-cell">
                <a href="./start.aspx">ホームへ戻る</a>
            </td>
        </tr>
    </table>

<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>
<tbody>

<%--<div id="main2" style="width:100%;height:450px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1280px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowHeaderWhenEmpty="True" DataKeyNames="SALESNOTENO">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="upd" Text="確認済">
                <HeaderStyle Width="70px" />
                <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:BoundField DataField="SALESNOTENO" HeaderText="セールスノートNO" SortExpression="SALESNOTENO" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUST_GRP_CD" HeaderText="代表客先コード" SortExpression="CUST_GRP_CD" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTCODE" HeaderText="客先コード" SortExpression="CUSTCODE" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="NOKIYMD" HeaderText="PO納期" SortExpression="NOKIYMD" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PLNO" HeaderText="PLNO" SortExpression="PLNO">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ORDERNO" HeaderText="客先注文番号" SortExpression="ORDERNO" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="FINISHDATE" HeaderText="受注確定日" SortExpression="FINISHDATE" >
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
<%--</div>--%>

</tbody>
</table>
</div>
<!--/#main2-->

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT   SNH.SALESNOTENO  , CU.CUST_GRP_CD  , SNH.CUSTCODE
  , SNH.NOKIYMD  , SNH.PLNO  , SNH.ORDERNO  , SNH.FINISHDATE 
FROM V_T_SN_HD_TB SNH
INNER JOIN V_T_SN_BD_TB SNB 
ON SNH.SALESNOTENO = SNB.SALESNOTENO 
  INNER JOIN M_EXL_CUST CU
    ON SNH.CUSTCODE = CU.CUST_ANAME
WHERE SNB.LS = '2' 
AND   SNH.NOKIYMD &gt;= CONVERT(NVARCHAR,  GETDATE(), 111) 
AND   SNB.ERRCD = '' 
AND   SNH.FINISHDATE IS NOT NULL
AND   SNH.NOKIYMD &lt;&gt; '9999/12/31 0:00:00'
  AND SNB.LEFTQTY &gt; 0
  AND SNH.FINISHDATE &lt; '2022/05/20'
  AND (SNH.CUSTCODE NOT IN ('C255','E140','E155') AND SNH.CUSTCODE NOT LIKE 'A%' AND SNH.CUSTCODE NOT LIKE 'B%')"></asp:SqlDataSource>

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
