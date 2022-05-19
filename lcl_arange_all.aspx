<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lcl_arange_all.aspx.vb" Inherits="cs_home" %>

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

    <script type="text/javascript">
      function LinkClick2() {
          var url = 'm_lcl_dec_mail.aspx?q='
          var result = confirm('別ウインドウでメールアドレス管理ページを開きます');

          if (result) {
              window.open(url, null);
          }
          else {
          }
      }

    </script>

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
            <td style="width:10px;Font-Size:15px;" >
            </td>
            <td style="width:80px;" >
<%--                <asp:Button class="btn00"  ID="Button2" runat="server" Text="切替" Width="50px" Height="30px" AutoPostBack="True" Font-Size="13px"/>       
                :<asp:Label id="Label3" Text="進捗" Font-Size="10" runat="server"></asp:Label>--%>
            </td>
           <td style="width:70px;" >
                <div class="button04">
                    <a href="javascript:void(0);" onclick="LinkClick2()">メール登録</a>
                </div>  
            </td>
            <td style="width:70px;" >
                <div class="button04">
                    <a href="lcl_tenkai.aspx?id={0}">展開済案件</a>
                </div>
            </td>
            <td style="width:70px;" >
                <div class="button04">
                    <a href="lcl_arange.aspx?id={0}">進捗一覧</a>
                </div>
            </td>
        </tr>
    </table>



    <asp:Panel ID="Panel2" runat="server"  Font-Size="12px" >

    <table border="1" style="width:900px;Font-Size:11px;" >
        <tr>
            <td  colspan="2" >
                <div style="background-color:white;text-align: center;">
                    説明　※CUT日が空白の場合：C258 ETDの8日前、C255 ETDの10日前、その他 ETDの7日前に自動で設定
                </div>
            </td>
            <td >
                <div style="background-color:Khaki;text-align: center;">
                メール対応必要
                </div>
            </td>
        </tr>
        <tr>
            <td >
                <div style="background-color:lightgray;text-align: center;">
                書類送付済み
                </div>
            </td>
            <td >
                <div style="background-color:lightblue;text-align: center;">
                引取り依頼済み
                </div>
            </td>
            <td >
                <div style="background-color:Salmon;text-align: center;">
                今週引取り依頼必要
                </div>
            </td>
        </tr>
        <tr>
            <td >
                <div style="background-color:red;color:White;text-align: center;">
                AC要:引取り依頼が必要だがBooking未確定
                </div>
            </td>
            <td >
                <div style="background-color:red;color:White;text-align: center;">
                CUT日未記入:BooingシートにCUT日が入力されていない。
                </div>
            </td>
            <td >
                <div style="background-color:purple;color:White;text-align: center;">
                荷受地～仕向地までのどこかが空欄
                </div>
            </td>
        </tr>
    </table>
        	

    <div class="wrapper">
    <table class="sticky">
    <thead class="fixed">

    </thead>

    <tbody>

    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1700px" BackColor="White" BorderColor="#555555" BorderStyle="none" BorderWidth="3px" CellPadding="3" GridLines="Both">

    <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK" > </HeaderStyle>
    <HeaderStyle CssClass="Freezing"></HeaderStyle>

    <Columns>


        <asp:TemplateField ShowHeader="False">
        <ItemTemplate>
        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="edt" ImageUrl="~/icon/write.png" Text="編集" width = "15" height = "15" />
        </ItemTemplate>
        <HeaderStyle BackColor="#6B696B" />
        </asp:TemplateField>


    <asp:BoundField DataField="Forwarder" HeaderText="海貨" SortExpression="Forwarder" />

    <%--<asp:BoundField DataField="STATUS" HeaderText="状況" SortExpression="STATUS" />--%>
　　<%--<asp:BoundField DataField="CONSIGNEE" HeaderText="客先" SortExpression="CONSIGNEE" />--%>
　　<%--<asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" />--%>

    <asp:BoundField DataField="CUST_CD" HeaderText="客先コード" SortExpression="CUSTCODE" />
    <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE_NO" SortExpression="INVOICE_NO" />
    <asp:BoundField DataField="OFFICIAL_QUOT" HeaderText="建値" SortExpression="TATENE" />
    <asp:BoundField DataField="CUT_DATE2" HeaderText="搬入日" SortExpression="CUT2" />
    <asp:BoundField DataField="CUT_DATE" HeaderText="CUT" SortExpression="CUT" />
    <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" />
    <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA" />
    <asp:BoundField DataField="LCL_QTY" HeaderText="荷量" SortExpression="VOLUME" />
    <asp:BoundField DataField="BOOKING_NO" HeaderText="ブッキング＃" SortExpression="BOOKING_NO" />

    <asp:BoundField DataField="PLACE_OF_RECEIPT" HeaderText="荷受地" SortExpression="PLACE_OF_RECEIPT" />
    <asp:BoundField DataField="LOADING_PORT" HeaderText="積出港" SortExpression="LOADING_PORT" />
    <asp:BoundField DataField="DISCHARGING_PORT" HeaderText="揚げ港" SortExpression="DISCHARGING_PORT" />
    <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="仕向地" SortExpression="PLACE_OF_DELIVERY" />


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

                <asp:Panel ID="Panel1" runat="server"  Font-Size="20px" Visible ="false">

        
        <table style="width:1500px;height:10px;">
        </table>
        <table style="width:1500px;height:10px;">
            <tr>
                <td style="width:1500px;" >
                    <asp:Label ID="Label15" runat="server" Text="Bookingシート更新中"></asp:Label>
                    <asp:Label ID="Label16" runat="server" Text="08:00-08:10"></asp:Label>
                    <asp:Label ID="Label17" runat="server" Text="11:50-12:00"></asp:Label>
                    <asp:Label ID="Label18" runat="server" Text="14:55-15:05"></asp:Label>
                </td>
            </tr>
        </table>

    </asp:Panel>

</div>

<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CONSIGNEE], [CUST_CD], [DESTINATION], [INVOICE_NO], [CUT_DATE], [CUT_DATE]AS CUT_DATE2, [ETD], [ETA], [LCL_QTY], [OFFICIAL_QUOT],[BOOKING_NO] FROM [T_BOOKING] WHERE [LCL_QTY] like '%M3%' AND [CUT_DATE] <>'' AND [CUT_DATE] IS NOT NULL AND [CUT_DATE] > GETDATE()-3 AND [CUT_DATE] < GETDATE()+45  ORDER BY [CUT_DATE]  "></asp:SqlDataSource>--%>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CONSIGNEE], [CUST_CD], [DESTINATION], [INVOICE_NO], [CUT_DATE], [CUT_DATE]AS CUT_DATE2, [ETD], [ETA], [LCL_QTY], [OFFICIAL_QUOT],[BOOKING_NO] FROM [T_BOOKING] WHERE [LCL_QTY] like '%M3%' AND [ETD] < GETDATE()+40 AND [ETD] > GETDATE()-7 ORDER BY [ETD]  "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [Forwarder], [STATUS], [CUST_CD], [CONSIGNEE], [DESTINATION], [INVOICE_NO], [OFFICIAL_QUOT], [CUT_DATE], [CUT_DATE]AS CUT_DATE2, [ETD], [ETA], [TWENTY_FEET], [FOURTY_FEET], [LCL_QTY], [BOOKING_NO], [BOOK_TO], [VESSEL_NAME], [VOYAGE_NO], [PLACE_OF_RECEIPT], [LOADING_PORT], [DISCHARGING_PORT], [PLACE_OF_DELIVERY] FROM [T_BOOKING]  WHERE [LCL_QTY] like '%M3%' AND [ETD] < GETDATE()+40 AND [ETD] > GETDATE()-1 AND STATUS <>'キャンセル' ORDER BY [ETD] "></asp:SqlDataSource>







<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
