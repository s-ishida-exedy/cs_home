<%@ Page Language="VB" AutoEventWireup="false" CodeFile="air_exclusive_sn.aspx.vb" Inherits="cs_home"EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(AIR専用客先オーダー確認 SN明細)</title>
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
            width: 500px;
        }   
        .ex-cell{
            width: 300px;
            border-collapse: separate;
            border-spacing: 0;
            border: solid 1px #778ca3;
            padding: 2px;
        }
        .third-cell {
            width: 100px;
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
       　/*ヘッダー固定用*/
        .sticky{
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
          height: 470px;
          width: calc(100% - 360px);
        }
        .tab1 {
            border-collapse:collapse;
            border-spacing:0px;
            border:1px solid #000000;
            width:350px;
        }

        .td1{
            border:1px solid #000000;
            width:190px;
            text-align:center;
        }
        .td2{
            border:1px solid #000000;
            width:90px;
            text-align:center;
        }
        .td3{
            border:1px solid #000000;
            width:70px;
            text-align:center;
        }
        .err{
            color:red;
            font-weight :700;
        }
        .DropDownList{
            text-align :center;
            font-size :small;
        }
        .txtb{
            padding: 5px;
            font-size :small ;
        }
        .txtb2{
            padding: 5px;
            font-size :small ;
            text-align :center ;
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
<form id="form1" runat="server" autocomplete="off">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>AIR専用客先ＳＮ明細</h2>  
            </td>
            <td class="second-cell">
            </td>
            <td class="third-cell">
                <a href="./air_exclusive.aspx">一覧へ戻る</a>
            </td>
        </tr>
    </table>
<%--<div class="wrapper">
<table class="sticky">
<thead class="fixed">
</thead>
<tbody>--%>
<%--<div id="main2" style="width:100%;height:450px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1280px" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" 
        ShowHeaderWhenEmpty="True" DataKeyNames="SALESNOTENO">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>

                <asp:BoundField DataField="SALESNOTENO" HeaderText="SALESNOTENO" SortExpression="SALESNOTENO" ReadOnly="True" >
                </asp:BoundField>
                <asp:BoundField DataField="ORDERKEY" HeaderText="ORDERKEY" SortExpression="ORDERKEY" >
                </asp:BoundField>
                <asp:BoundField DataField="PLNO" HeaderText="PLNO" SortExpression="PLNO" > 
                </asp:BoundField>
                <asp:BoundField DataField="CUSTCODE" HeaderText="CUSTCODE" SortExpression="CUSTCODE" />
                <asp:BoundField DataField="CUSTMPN" HeaderText="CUSTMPN" SortExpression="CUSTMPN" >
                </asp:BoundField>
                <asp:BoundField DataField="EXDNO" HeaderText="EXDNO" SortExpression="EXDNO" >
                </asp:BoundField>
                <asp:BoundField DataField="ORDERNO" HeaderText="ORDERNO" SortExpression="ORDERNO" >
                </asp:BoundField>
                <asp:BoundField DataField="PRODNAME" HeaderText="PRODNAME" SortExpression="PRODNAME" />
                <asp:BoundField DataField="NOKIYMD" HeaderText="NOKIYMD" SortExpression="NOKIYMD" />
                <asp:BoundField DataField="QTY" HeaderText="QTY" SortExpression="QTY" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LEFTQTY" HeaderText="LEFTQTY" SortExpression="LEFTQTY" >
                <ItemStyle HorizontalAlign="Right" />
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
  a.SALESNOTENO
  , b.ORDERKEY
  , a.PLNO
  , a.CUSTCODE
  , b.CUSTMPN
  , b.EXDNO
  , a.ORDERNO
  , b.PRODNAME
  , a.NOKIYMD
  , b.QTY
  , b.LEFTQTY 
FROM
  dbo.V_T_SN_HD_TB a 
  inner join dbo.V_T_SN_BD_TB b 
    on a.SALESNOTENO = b.SALESNOTENO 
  inner join T_EXL_AIR_EXC_ODR c 
    on a.SALESNOTENO = c.SALESNOTENO 
  inner join T_EXL_AIR_EXCLUSIVE d 
    on c.ODR_CTL_NO = d.ODR_CTL_NO "></asp:SqlDataSource>
<%--</div>--%>

<!--/#main2-->

</tbody>
</table>

</div>



</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a>
</p>

</form>

</body>
</html>
