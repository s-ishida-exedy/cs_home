<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eir_comfirm_cs.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(EIR,Booking差異 CS対応)</title>
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
            width: 500px;
        }
        .second-cell {
            width: 600px;
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
       　/*GridViewヘッダー固定用*/
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
          height: 475px;
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
                <h2>EIR,Booking差異 CS対応</h2> 
            </td>
            <td class="second-cell">
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack ="true" Text ="「未対応」以外も表示する。"/>
                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack ="true" Text ="過去分も表示する。"/>
            </td>
            <td class="third-cell">
                <a href="./start.aspx">ホームへ戻る</a>
            </td>
        </tr>
    </table>
<%--<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>
<tbody>--%>
<%--<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Width="900px" CellPadding="4" GridLines="Vertical" ForeColor="#333333" >
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="edt" Text="編集">
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:BoundField DataField="CODE" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS" HeaderText="ｽﾃｰﾀｽ" SortExpression="STATUS" >
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="REGPERSON" HeaderText="依頼者" SortExpression="REGPERSON">
                <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="REGSTAMP" HeaderText="依頼日時" SortExpression="REGSTAMP">
                <HeaderStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="MAIL_TITLE" HeaderText="タイトル" SortExpression="MAIL_TITLE">
                <HeaderStyle Width="400px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" 
SelectCommand="SELECT a.CODE, a.REGPERSON, a.REGSTAMP, a.MAIL_TITLE, a.VOYNO01, a.VOYNO02, 
a.VESSEL01, a.VESSEL02, a.BOOKING01, a.BOOKING02, a.CONTAINER01, a.CONTAINER02, a.ETC01, a.ETC02, 
CASE a.STATUS 
 WHEN '0' THEN '未対応'
 WHEN '1' THEN '確認済み'
END AS STATUS
FROM T_EXL_EIR_COMF a
LEFT JOIN M_EXL_USR b
ON a.REGPERSON = b.uid
WHERE STATUS = '0'
AND VAN_DATE = CONVERT(NVARCHAR, GETDATE(), 111) 
ORDER BY REGSTAMP">
        </asp:SqlDataSource>
    
</tbody>
</table>
</div>

</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
