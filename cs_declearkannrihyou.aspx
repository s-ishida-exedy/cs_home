<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cs_declearkannrihyou.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(輸出申告管理表)</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
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

        .button04 a {
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

        .button04 a::after {
            content: '';
            width: 5px;
            height: 5px;
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
            transform: rotate(45deg);
        }

        .button04 a:hover {
            color: #000000;
            text-decoration: none;
            background-color: #ffffff;
            border: 2px solid #000000;
        }

        .button04 a:hover::after {
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
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

<table>
    <tr>
        <td style="width:300px;Font-Size:25px;" >
            <h2>特定輸出管理表</h2>
        </td>
        <td style="width:400px;Font-Size:13px;" >
        </td>
        <td style="width:80px;" >
            <asp:Button class="btn00"  ID="Button4" runat="server" Text="切替" Width="50px" Height="30px" AutoPostBack="True" Font-Size="13px"/>       
            :<asp:Label id="Label5" Text="申告未" Font-Size="10" runat="server"></asp:Label>
        </td>
        <td style="width:100px;Font-Size:13px;" >
        </td>
    </tr>
</table>

<asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

    <table>
        <tr>
            <td style="width:1000px;Font-Size:15px;" >
                <p><asp:Label Font-Size="10" ID="Label1" runat="server" Text="・編集ボタンを押し、輸出管理番号を入力してください。"></asp:Label></p>
                <asp:Label Font-Size="10" ID="Label2" runat="server" Text="・委託案件、キャンセル案件がある場合は、チェックを入れたあとに委託登録、キャンセル登録のボタンを押してください。"></asp:Label>
            </td>
        </tr>
    </table>

    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" width="120" Font-Size="10" runat="server" Text="委託登録" AutoPostBack="True" />
            </td>
            <td>
                <asp:Button ID="Button2" width="120" Font-Size="10" runat="server" Text="キャンセル登録" AutoPostBack="True" />
            </td>
                <td style="width:1000px;Font-Size:15px;">
            </td>
        </tr>
    </table>




    <div class="wrapper" id="main2">
    <table class="sticky">
    <thead class="fixed">

    </thead>

    <tbody>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "1250px" DataSourceID="SqlDataSource1" DataKeyNames="BOOKING_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >

    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="#ffffff" />
    <Columns>

    <asp:TemplateField>
    <ItemTemplate>

    <asp:CheckBox ID="cb" Checked="false" runat="server"  />

    </ItemTemplate>
    </asp:TemplateField>
            
    <asp:TemplateField HeaderText="">
    <ItemTemplate>
    <asp:Button runat="server" CommandName="Edit" Text="編集" />
    </ItemTemplate>

    <EditItemTemplate>
    <asp:Button runat="server" CommandName="Update" Text="保存" />
    <asp:Button runat="server" CommandName="Cancel" Text="戻る" />

    </EditItemTemplate>
    </asp:TemplateField>

    <asp:BoundField DataField="TDATE" HeaderText="追加日" SortExpression="TDATE" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="CUT" HeaderText="1.通関日" SortExpression="CUT" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="CUST" HeaderText="2.客先" SortExpression="CUST" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="SUMMARY_INVO" HeaderText="3.INVOICE NO." SortExpression="SUMMARY_INVO" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="LOADING_PORT" HeaderText="4.積出港" SortExpression="LOADING_PORT" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="DESTINATION" HeaderText="5.仕向地" SortExpression="DESTINATION" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="KANNRINO" HeaderText="6.輸出申告番号" SortExpression="KANNRINO" >
    </asp:BoundField>
    <asp:BoundField DataField="BOOKING_NO" HeaderText="7.BKG#" SortExpression="BOOKING_NO" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="REV_KANNRINO" HeaderText="13.修正_輸出申告番号" SortExpression="REV_KANNRINO" />

    </Columns>
    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    <RowStyle BackColor="#DCDCDC" ForeColor="Black" />
    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT TDATE,CUT ,CUST,SUMMARY_INVO,LOADING_PORT,DESTINATION,KANNRINO,BOOKING_NO,REV_KANNRINO FROM [T_EXL_DECKANRIHYO] WHERE ([KANNRINO] ='' or [KANNRINO] IS NULL) AND [IFLG] ='0'"
    UpdateCommand="UPDATE T_EXL_DECKANRIHYO SET [KANNRINO]=@KANNRINO, [REV_KANNRINO]=@REV_KANNRINO WHERE BOOKING_NO=@BOOKING_NO"
    ></asp:SqlDataSource>
    </tbody>
    </table>
    </div>
</asp:Panel>  

<asp:Panel ID="Panel2" runat="server"  Font-Size="12px" Visible ="false">

    <table>
        <tr>
            <td style="width:1000px;Font-Size:15px;" >
                <p><asp:Label Font-Size="10" ID="Label3" runat="server" Text="・8.区分が委託、ｷｬﾝｾﾙになっている案件はチェックを入れて管理表に表示するボタンを押すと、特定輸出管理表に表示される"></asp:Label></p>
                <p><asp:Label Font-Size="10" ID="Label6" runat="server" Text="　※6.輸出申告番号が入力されている案件は、編集ボタンで輸出申告番号を消すと管理表に表示される。"></asp:Label></p>
                <asp:Label Font-Size="10" ID="Label4" runat="server" Text="・輸出許可書の許可後訂正を行った際は編集ボタンを押して修正後の管理番号を登録"></asp:Label>
            </td>
        </tr>
    </table>

    <table>
        <tr>
            <td  >
                <asp:Button ID="Button3" width="120" Font-Size="10" runat="server" Text="管理表に表示する" AutoPostBack="True" />
                <asp:Button ID="Button5" runat="server" Text="エクセル出力" />
            </td>
            <td style="width:1000px;Font-Size:15px;">
            </td>
        </tr>
    </table>

    <div class="wrapper" id="main2">
    <table class="sticky">
    <thead class="fixed">
    </thead>

    <tbody>

    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width = "1550px" DataSourceID="SqlDataSource2" DataKeyNames="BOOKING_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >

    <HeaderStyle BackColor="#ffffe0" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="#ffffe0" />
    <Columns>

    <asp:TemplateField>
    <ItemTemplate>

    <asp:CheckBox ID="cb" Checked="false" runat="server"  />

    </ItemTemplate>
    </asp:TemplateField>
            
    <asp:TemplateField HeaderText="EDITMENU">
    <ItemTemplate>
    <asp:Button runat="server" CommandName="Edit" Text="編集" />
    </ItemTemplate>

    <EditItemTemplate>
    <asp:Button runat="server" CommandName="Update" Text="保存" />
    <asp:Button runat="server" CommandName="Cancel" Text="戻る" />

    </EditItemTemplate>
    </asp:TemplateField>

    <asp:BoundField DataField="TDATE" HeaderText="追加日" SortExpression="TDATE" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="CUT" HeaderText="1.通関日" SortExpression="CUT" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="CUST" HeaderText="2.客先" SortExpression="CUST" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="SUMMARY_INVO" HeaderText="3.INVOICE NO." SortExpression="SUMMARY_INVO" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="LOADING_PORT" HeaderText="4.積出港" SortExpression="LOADING_PORT" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="DESTINATION" HeaderText="5.仕向地" SortExpression="DESTINATION" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="KANNRINO" HeaderText="6.輸出申告番号" SortExpression="KANNRINO" >
    </asp:BoundField>
    <asp:BoundField DataField="BOOKING_NO" HeaderText="7.BKG#" SortExpression="BOOKING_NO" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="IFLG" HeaderText="8.区分" SortExpression="IFLG" ReadOnly="true" >
    </asp:BoundField>
    <asp:BoundField DataField="REV_KANNRINO" HeaderText="13.修正_輸出申告番号" SortExpression="REV_KANNRINO" />
    </Columns>
    <FooterStyle BackColor="#ffffe0" ForeColor="Black" />
    <PagerStyle BackColor="#ffffe0" ForeColor="Black" HorizontalAlign="Center" />
    <RowStyle BackColor="#ffffe0" ForeColor="Black" />
    <SelectedRowStyle BackColor="#ffffe0" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#ffffe0" />
    <SortedAscendingHeaderStyle BackColor="#ffffe0" />
    <SortedDescendingCellStyle BackColor="#ffffe0" />
    <SortedDescendingHeaderStyle BackColor="#ffffe0" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT TDATE,CUT,CUST,SUMMARY_INVO,LOADING_PORT,DESTINATION,KANNRINO,BOOKING_NO,REV_KANNRINO,case IFLG WHEN '0' THEN '自社通関' WHEN '1' THEN '委託' WHEN '2' THEN 'ｷｬﾝｾﾙ' END AS IFLG FROM [T_EXL_DECKANRIHYO] WHERE ([KANNRINO] <>'' or [KANNRINO] IS NOT NULL AND [IFLG] <>'0' ) or (([KANNRINO] ='' or [KANNRINO] IS NULL) AND [IFLG] <>'0')  "

    UpdateCommand="UPDATE T_EXL_DECKANRIHYO SET [KANNRINO]=@KANNRINO, [REV_KANNRINO]=@REV_KANNRINO WHERE BOOKING_NO=@BOOKING_NO"
    ></asp:SqlDataSource>
    </tbody>
    </table>
    </div>

</asp:Panel>  


<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
