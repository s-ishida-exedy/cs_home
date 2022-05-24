<%@ Page Language="VB" AutoEventWireup="false" CodeFile="doc_final.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(書類作成最終一覧)</title>
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
            width: 300px;
        }
        .second-cell {
            width: 780px;
        }   
        .third-cell {
            width: 220px;
            text-align:right;
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
                <h2>書類作成最終一覧</h2> 
            </td>
            <td class="second-cell">
                <asp:DropDownList ID="DropDownList2" class="DropDownList" runat="server" AppendDataBoundItems="True" AutoPostBack="True" Width ="164px" DataSourceID="SqlDataSource3" DataTextField="FINAL_VAN" DataValueField="FINAL_VAN"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList1" class="DropDownList" runat="server" AppendDataBoundItems="True" AutoPostBack="True" Width ="164px" DataSourceID="SqlDataSource2" DataTextField="Forwarder" DataValueField="Forwarder"></asp:DropDownList>&nbsp;
                <asp:Button ID="Button2" runat="server" Text="ﾘｾｯﾄ" Font-Size="Small" width="60px" />&nbsp;
                </td>
            <td class="third-cell">
            </td>
        </tr>
    </table>

<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>

<tbody>


<%--<div id="main2" style="width:100%;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1280px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="edt" Text="更新" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="Forwarder" HeaderText="場所" ReadOnly="True" SortExpression="Forwarder" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CUST_CD" HeaderText="客先" SortExpression="CUST_CD" ReadOnly="True" >
            </asp:BoundField>
            <asp:BoundField DataField="INVOICE_NO" HeaderText="IVNO" SortExpression="INVOICE_NO" ReadOnly="True" >
            </asp:BoundField>
            <asp:BoundField DataField="FINAL_VAN" HeaderText="最終VAN" SortExpression="FINAL_VAN" ReadOnly="True" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CUT_DATE" HeaderText="CUT日" SortExpression="CUT_DATE" ReadOnly="True" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" ReadOnly="True" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="書類">
            <HeaderStyle Width="100px" />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="
SELECT DISTINCT
CASE LEFT(Forwarder,2) 
WHEN 'AT' THEN '上野'
ELSE '本社'
END AS Forwarder
  , CUST_CD 
  , INVOICE_NO 
  , MAX(VAN_DATE) AS FINAL_VAN 
  , CUT_DATE
  , ETD
FROM 
  ( SELECT Forwarder, CUST_CD, INVOICE_NO, DAY01 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY02 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY03 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY04 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY05 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY06 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY07 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY08 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY09 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY10 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY11 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING) AS TBL  
WHERE 
  INVOICE_NO &lt;&gt; ''  
  AND VAN_DATE &lt;&gt; '' 
  AND VAN_DATE &gt;= CONVERT(NVARCHAR,  GETDATE(), 111)
  AND INVOICE_NO NOT LIKE '%ヒョウコウ%'
GROUP BY 
  Forwarder, CUST_CD , INVOICE_NO  , CUT_DATE  , ETD
ORDER BY FINAL_VAN, Forwarder DESC, CUT_DATE"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="
SELECT DISTINCT
CASE LEFT(Forwarder,2) 
WHEN 'AT' THEN '上野'
ELSE '本社'
END AS Forwarder
FROM 
  ( SELECT Forwarder, CUST_CD, INVOICE_NO, DAY01 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY02 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY03 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY04 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY05 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY06 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY07 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY08 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY09 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY10 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY11 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING) AS TBL  
WHERE 
  INVOICE_NO &lt;&gt; ''  
  AND VAN_DATE &lt;&gt; '' 
  AND VAN_DATE &gt;= CONVERT(NVARCHAR,  GETDATE(), 111)
  AND INVOICE_NO NOT LIKE '%ヒョウコウ%'
GROUP BY 
  Forwarder
ORDER BY Forwarder DESC"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="
SELECT DISTINCT
MAX(VAN_DATE) AS FINAL_VAN 
FROM 
  ( SELECT Forwarder, CUST_CD, INVOICE_NO, DAY01 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY02 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY03 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY04 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY05 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY06 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY07 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY08 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY09 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY10 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING 
    UNION ALL  
    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY11 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING) AS TBL  
WHERE 
  INVOICE_NO &lt;&gt; ''  
  AND VAN_DATE &lt;&gt; '' 
  AND VAN_DATE &gt;= CONVERT(NVARCHAR,  GETDATE(), 111)
  AND INVOICE_NO NOT LIKE '%ヒョウコウ%'
GROUP BY 
  Forwarder, CUST_CD , INVOICE_NO  , CUT_DATE  , ETD
ORDER BY FINAL_VAN"></asp:SqlDataSource>
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
