<%@ Page Language="VB" AutoEventWireup="false" CodeFile="epa_request.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(EPA申請状況)</title>
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
            width: 800px;
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
          height: 450px;
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
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>EPA発給申請管理</h2> 
            </td>
            <td class="second-cell">
                <asp:Button ID="Button2" runat="server" Text="追加登録" style="width:164px" Font-Size="Small" />
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
<%--<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="1800px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" DataKeyNames="BLDATE,INV">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="edt" Text="編集" />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="STATUS" HeaderText="状態" SortExpression="STATUS" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BLDATE" HeaderText="ETD" SortExpression="BLDATE"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="INV" HeaderText="IV" SortExpression="INV"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTNAME" HeaderText="客先" SortExpression="CUSTNAME"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTCODE" HeaderText="ｺｰﾄﾞ" SortExpression="CUSTCODE"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SALESFLG" HeaderText="売上" SortExpression="SALESFLG"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIPPEDPER" HeaderText="船名" SortExpression="SHIPPEDPER" >
                </asp:BoundField>
                <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA" >
                </asp:BoundField>
                <asp:BoundField DataField="CUTDATE" HeaderText="ｶｯﾄ日" SortExpression="CUTDATE" >
                </asp:BoundField>
                <asp:BoundField DataField="VOYAGENO" HeaderText="VoyNO" SortExpression="VOYAGENO" >
                </asp:BoundField>
                <asp:BoundField DataField="RECEIPT_NUMBER" HeaderText="受付番号" SortExpression="RECEIPT_NUMBER" >
                </asp:BoundField>
                <asp:BoundField DataField="IVNO_FULL" HeaderText="IVNO" SortExpression="IVNO_FULL" >
                </asp:BoundField>
                <asp:BoundField DataField="APPLICATION_DATE" HeaderText="申請日" SortExpression="APPLICATION_DATE" >
                </asp:BoundField>
                <asp:BoundField DataField="SENDING_REQ_DATE" HeaderText="送付依頼日" SortExpression="SENDING_REQ_DATE" >
                </asp:BoundField>
                <asp:BoundField DataField="RECEIPT_DATE" HeaderText="受領日" SortExpression="RECEIPT_DATE" >
                </asp:BoundField>
                <asp:BoundField DataField="EPA_SEND_DATE" HeaderText="EPA送付日" SortExpression="EPA_SEND_DATE" >
                </asp:BoundField>
                <asp:BoundField DataField="TRK_NO" HeaderText="TRK#" SortExpression="TRK_NO" >
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" 
SelectCommand="SELECT
  CASE STATUS
  	WHEN '01' THEN '未'
  	WHEN '02' THEN '済'
  	WHEN '03' THEN '対象ﾅｼ'
  	WHEN '04' THEN 'ｷｬﾝｾﾙ'
  	WHEN '09' THEN '再発給'
  END AS STATUS
  , BLDATE 
  , INV 
  , CUSTNAME 
  , CUSTCODE 
  , CASE SALESFLG
  		WHEN '1' THEN '済'
        ELSE ''
	END AS SALESFLG
  , SHIPPEDPER 
  , ETA 
  , CUTDATE 
  , VOYAGENO 
  , RECEIPT_NUMBER 
  , IVNO_FULL 
  , APPLICATION_DATE 
  , SENDING_REQ_DATE 
  , RECEIPT_DATE 
  , EPA_SEND_DATE 
  , TRK_NO 
FROM T_EXL_EPA_KANRI
ORDER BY BLDATE, INV">
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
