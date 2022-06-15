<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lcl_arange.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ポータルサイト(LCL出荷案件一覧)</title>
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
          background-color: #ffffff;
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

        .btn00
        {
            cursor : pointer;
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

    <table >
        <tr>
            <td style="width:250px;Font-Size:25px;" >
                <h2>LCL出荷準備進捗</h2>
            </td>
            <td style="width:300px;Font-Size:15px;" >
                <asp:Label id="Label1" Text="＜ラベル＞" runat="server"></asp:Label>
            </td>
            <td style="width:50px;Font-Size:15px;" >
            </td>
            <td style="width:80px;" >
<%--                <asp:Button class="btn00"  ID="Button2" runat="server" Text="切替" Width="50px" Height="30px" AutoPostBack="True" Font-Size="13px"/>       
                :<asp:Label id="Label3" Text="進捗" Font-Size="10" runat="server"></asp:Label>--%>
            </td>
            <td style="width:80px;" >
                <div class="button04">
                    <a href="lcl_tenkai.aspx?id={0}">展開済案件</a>
                </div>
            </td>
            <td style="width:80px;" >
                <div class="button04">
                    <a href="lcl_arange_all.aspx?id={0}">状況確認</a>
                </div>
            </td>
        </tr>
    </table>

    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

        <table >
            <tr>
            <td style="width:320px;" >
            <p><asp:Button class="btn00"  ID="Button1" runat="server" Text="追加" Width="75px" Height="40px" AutoPostBack="True" Font-Size="13px"/></p>
            <asp:Label ID="Label2" runat="server"　Font-Size="11px" Text="※チェックを入れてボタンを押すと作業メニューに追加される"></asp:Label>
            </td>  
                <td style="width:150px;Font-Size:12px;" >
                    <div style="background-color:DarkGray;">
                        <p>・グレー:書類作成済</p>
                    </div>
                    <div style="background-color:LightBlue;">
                        <p>・ブルー:手配依頼済</p>
                    </div>
                </td>
                <td style="width:150px;Font-Size:12px;" >
                    <div style="background-color:Salmon;">
                        <p>・レッド:これから手配が必要</p>
                        </div>
                            <div style="background-color:red;color:White;">
                        <p>・AC要:BOOKING未確定</p>
                    </div>
                </td>
                <td style="width:500px;" >
                </td>
            </tr>
        </table>


    
        <div class="wrapper">
        <table class="sticky">
        <thead class="fixed">

        </thead>

        <tbody>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1350px" BackColor="White" BorderColor="#555555" BorderStyle="none" BorderWidth="3px" CellPadding="3" GridLines="Both">

        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK" > </HeaderStyle>
        <HeaderStyle CssClass="Freezing"></HeaderStyle>

        <Columns>

        <asp:TemplateField>
        <ItemTemplate>

        <asp:CheckBox ID="cb" Checked="false" runat="server"  />

        </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="CONSIGNEE" HeaderText="客先" SortExpression="CONSIGNEE" />
        <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" />
        <asp:BoundField DataField="CUST_CD" HeaderText="客先コード" SortExpression="CUSTCODE" />
        <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE_NO" SortExpression="INVOICE_NO" />
        <asp:BoundField DataField="OFFICIAL_QUOT" HeaderText="建値" SortExpression="TATENE" />
        <asp:BoundField DataField="CUT_DATE2" HeaderText="搬入日" SortExpression="CUT2" />
        <asp:BoundField DataField="CUT_DATE" HeaderText="CUT" SortExpression="CUT" />
        <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" />
        <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA" />
        <asp:BoundField DataField="LCL_QTY" HeaderText="荷量" SortExpression="VOLUME" />
        <asp:BoundField DataField="BOOKING_NO" HeaderText="ブッキング＃" SortExpression="BOOKING_NO" />

        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>

        </tbody>
        </table>
        </div>

    </asp:Panel>

</div>


    <asp:Panel ID="Panel3" runat="server"  Font-Size="20px" Visible ="false">

        
        <table style="width:1500px;height:10px;">
        </table>
        <table style="width:1500px;height:10px;">
            <tr>
                <td style="width:1500px;" >
                    <asp:Label ID="Label12" runat="server" Text="データ未更新です。8:30以降の場合、異常報告ボタンを押してください。"></asp:Label>
                    <asp:Button ID="Button5"  CssClass ="btn00" runat="server" Text="異常報告" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" />
                </td>
            </tr>
        </table>

    </asp:Panel>


            <asp:Panel ID="Panel2" runat="server"  Font-Size="20px" Visible ="false">

        
        <table style="width:1500px;height:10px;">
        </table>
        <table style="width:1500px;height:10px;">
            <tr>
                <td style="width:1500px;" >
                    <asp:Label ID="Label15" runat="server" Text="Bookingシート更新中のため操作できません。 更新時間："></asp:Label>
                    <asp:Label ID="Label16" runat="server" Text="08:00-08:10"></asp:Label>
                    <asp:Label ID="Label17" runat="server" Text="11:50-12:00"></asp:Label>
                    <asp:Label ID="Label18" runat="server" Text="14:55-15:05"></asp:Label>
                </td>
            </tr>
        </table>

    </asp:Panel>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CONSIGNEE], [CUST_CD], [DESTINATION], [INVOICE_NO], [CUT_DATE], [CUT_DATE]AS CUT_DATE2, [ETD], [ETA], [LCL_QTY], [OFFICIAL_QUOT],[BOOKING_NO] FROM [T_BOOKING] WHERE [LCL_QTY] like '%M3%' AND [CUT_DATE] <>'' AND [CUT_DATE] IS NOT NULL AND [CUT_DATE] > GETDATE()-3 AND [CUT_DATE] < GETDATE()+45  ORDER BY [CUT_DATE]  "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CONSIGNEE], [CUST_CD], [DESTINATION], [INVOICE_NO], [CUT_DATE], [CUT_DATE]AS CUT_DATE2, [ETD], [ETA], [LCL_QTY], [OFFICIAL_QUOT],[BOOKING_NO] FROM [T_BOOKING] WHERE [LCL_QTY] like '%M3%' AND [CUT_DATE] =''  AND [CUT_DATE] IS NOT NULL AND [ETD] < GETDATE()+45   ORDER BY [ETD]  "></asp:SqlDataSource>
    



<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
