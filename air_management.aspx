<%@ Page Language="VB" AutoEventWireup="false" CodeFile="air_management.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(AIR管理表)</title>
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
            width: 220px;
        }
        .second-cell {
            width: 1010px;
        }   
        .third-cell {
            width: 120px;
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

        .DropDownList{
            width :100px;
            text-align :center;
            font-size:small ;
        }
        .date2{
            text-align :center;
            font-size:small ;
        }
        .txtb{
            padding: 2px;
            font-size :smaller ;
            width:100px;
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
                <h2>AIR管理表</h2> 
            </td>
            <td class="second-cell">
                <asp:DropDownList ID="DropDownList1" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource2" DataTextField="ETD" DataValueField="ETD" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource3" DataTextField="CUST_CD" DataValueField="CUST_CD" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList3" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource4" DataTextField="REQUESTER" DataValueField="REQUESTER" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList4" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource5" DataTextField="IVNO" DataValueField="IVNO" AutoPostBack="True"></asp:DropDownList>

                <asp:DropDownList ID="DropDownList5" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource6" DataTextField="ETD" DataValueField="ETD" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList6" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource7" DataTextField="CUST_CD" DataValueField="CUST_CD" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList7" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource8" DataTextField="REQUESTER" DataValueField="REQUESTER" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList ID="DropDownList8" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource9" DataTextField="IVNO" DataValueField="IVNO" AutoPostBack="True"></asp:DropDownList>
                <asp:TextBox ID="TextBox3" runat="server" Class ="txtb" placeholder="IVNO入力"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text=" ﾘｾｯﾄ " Font-Size="Small" Width ="80" />
                <asp:Button ID="Button7" runat="server" Text="検索" Font-Size="Small" Width ="80" />
                <asp:CheckBox ID="CheckBox1" runat="server" Font-Size="Small" Text =" 集荷済みも表示する"  AutoPostBack="True"/><br/>
                <asp:TextBox ID="TextBox1" runat="server" Class="date2" Width="130px"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="～"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Class="date2" Width="130px"></asp:TextBox>
                <asp:Button ID="Button6" runat="server" Text="期間指定してﾀﾞｳﾝﾛｰﾄﾞ" Font-Size="Small" Width="200px" />&nbsp;
                <asp:Button ID="Button5" runat="server" Text="前月分ﾀﾞｳﾝﾛｰﾄﾞ" Font-Size="Small" Width="120px" />&nbsp;
                <asp:Button ID="Button4" runat="server" Text="新規登録" Font-Size="Small" Width="120px" />
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

<!-- <div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;"> -->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="1680px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="書類" ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="doc_fin" Text="済" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="集荷" ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="pic_fin" Text="済" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="更新" ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CausesValidation="false" CommandName="update" Text="UP" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="REQUESTED_DATE" SortExpression="REQUESTED_DATE" HeaderText="依頼日" >
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CREATED_DATE" HeaderText="作成日" SortExpression="CREATED_DATE" >
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" >
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUST_CD" HeaderText="客先" SortExpression="CUST_CD" >
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IVNO" HeaderText="IVNO" SortExpression="IVNO" >
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="REQUESTER" HeaderText="依頼者" SortExpression="REQUESTER" >
                <HeaderStyle Width="120px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DEPARTMENT" HeaderText="部署" SortExpression="DEPARTMENT" >
                <HeaderStyle Width="180px" />
                </asp:BoundField>
                <asp:BoundField DataField="AUTHOR" HeaderText="作成者" SortExpression="AUTHOR" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DOC_FIN" HeaderText="書類" SortExpression="DOC_FIN" >
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIPPING_COMPANY" HeaderText="海貨業者" SortExpression="SHIPPING_COMPANY" >
                <HeaderStyle Width="90px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PICKUP" HeaderText="集荷" SortExpression="PICKUP" >
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PLACE" HeaderText="場所" SortExpression="PLACE" >
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="REMARKS" HeaderText="備考" SortExpression="REMARKS" >
                </asp:BoundField>
                <asp:BoundField DataField="AIR_CODE" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" 
            SelectCommand="SELECT
  REQUESTED_DATE
  , CREATED_DATE
  , ETD
  , CUST_CD
  , IVNO
  , REQUESTER
  , DEPARTMENT
  , AUTHOR
  , CASE DOC_FIN WHEN '作成済み' THEN '済' END AS DOC_FIN
  , SHIPPING_COMPANY
  , CASE PICKUP WHEN '集荷済み' THEN '済' END AS PICKUP
  , PLACE
  , REMARKS 
  , AIR_CODE
  , SHUK_METH
FROM
  T_EXL_AIR_MANAGE 
WHERE
  PICKUP = '' 
ORDER BY
  ETD
" CancelSelectOnNullParameter="False">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [ETD] FROM [T_EXL_AIR_MANAGE] WHERE  PICKUP = '' ORDER BY [ETD]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT CUST_CD  FROM  T_EXL_AIR_MANAGE WHERE  PICKUP = ''  ORDER BY  CUST_CD"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT REQUESTER FROM T_EXL_AIR_MANAGE WHERE PICKUP = ''"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT IVNO FROM T_EXL_AIR_MANAGE WHERE PICKUP = ''"></asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [ETD] FROM [T_EXL_AIR_MANAGE] ORDER BY [ETD]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT CUST_CD  FROM  T_EXL_AIR_MANAGE ORDER BY  CUST_CD"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT REQUESTER FROM T_EXL_AIR_MANAGE "></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT IVNO FROM T_EXL_AIR_MANAGE "></asp:SqlDataSource>
<%--</div>--%>  

<!-- SelectCommand="SELECT REQUESTED_DATE, CREATED_DATE, ETD, CUST_CD, IVNO, REQUESTER, DEPARTMENT, AUTHOR, DOC_FIN, SHIPPING_COMPANY, PICKUP, PLACE, REMARKS FROM T_EXL_AIR_MANAGE WHERE (DOC_FIN = ISNULL(@DOC_FIN, DOC_FIN)) AND (ETD = ISNULL(@ETD, ETD)) ORDER BY ETD" CancelSelectOnNullParameter="False"> -->


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
