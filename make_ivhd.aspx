<%@ Page Language="VB" AutoEventWireup="false" CodeFile="make_ivhd.aspx.vb" Inherits="yuusen" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(IVヘッダー取り込みファイル作成)</title>
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


        .button03 a {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0 auto;
            padding: 0.5em 2em;
            width: 80px;
            color: #696969;
            font-size: 13px;
            font-weight: 700;

        }

        .button03 a::after {
            content: '';
            width: 5px;
            height: 5px;
            border-top: 1px solid #696969;
            border-right: 1px solid #696969;
            transform: rotate(45deg);
        }

        .button03 a:hover {

            text-decoration: none;
            border-width: 4px;
            text-align: center

        }


        .DropDown
        {
            background-color: #ffffff;
            border-color: #000000;
            border-style: solid;
            border: none;
            color: #000000;
            text-align: center;
            border-bottom: inset 2px #ffffff;
                    cursor : pointer;
        }

        .DropDown:hover {
            text-align: center;
            border-bottom: inset 2px #000000;
        }

        .btn00
        {
            cursor : pointer;
        }

        .button01
        {
            background-color: #ffffff;
            border:none;
            color: #000000;
            border-bottom: inset 2px #ffffff;
                    cursor : pointer;
        }

        .button01:hover {
            text-align: center;
            border-bottom: inset 2px #000000;
        }

        .table01
        {
            /*background:linear-gradient(transparent 90%, #6fbfd1 0%);*/
            background: -webkit-gradient(linear, left top, right bottom,
            from(rgba(255,153,0,1)),
            color-stop(50%, rgba(255,153,0,1)),
            to(rgba(255,255,255,1)));
            background: -moz-linear-gradient(left,
            #6fbfd1,
            #6fbfd1 50%,
            #ffffff);
            background: linear-gradient(transparent 10%, to right,
            #6fbfd1,
            #6fbfd1 50%,
            #ffffff );
            color: #ffffff;
        }

        h1 {
            position: relative;
            display: inline-block;
            padding: 0 143px;
            text-align: center;
        }

        h1:before, h1:after {
            content: '';
            position: absolute;
            top: 50%;
            display: inline-block;
            width:100px;
            height: 2px;
            background-color: black;
        }

        h1:before {
            left:0;
        }
        h1:after {
            right: 0;
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

    <script type="text/javascript">
      function LinkClick() {
          var url = 'm_kaika_jpn_eg.aspx?q='
          var result = confirm('別ウインドウでを開きます');

          if (result) {
              window.open(url, null);
          }
          else {
          }
      }
    </script>

</head>


<body class="c2">

<form id="form1" runat="server">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->

    <div id="contents2" class="inner2">
        <table style="height:10px;">
            <tr>
                <td style="width:550px;Font-Size:25px;" >
                    <h2>IVヘッダー取り込みファイル作成</h2>
                </td>
                <td style="width:100px;" >
                </td>
                <td style="width:250px;">
                </td>
                <td style="width:150px;" >
                </td>
                <td style="width:150px;" >
                </td>
            </tr>
        </table>    

        <table style="Height:10px;">
            <tr>

            </tr>
        </table>


        <table style="height:10px;">
            <tr>
                <td style="width:300px;" >
                    <asp:Button ID="Button1" CssClass ="btn00" runat="server" Text="エクセル出力" />
                    <asp:Button ID="Button2" CssClass ="btn00" runat="server" Text="LCL表示" />
                    <asp:Button ID="Button3" CssClass ="btn00" runat="server" Text="FCL表示" />
                </td>
                <td style="width:100px;Font-Size:25px;" >
                    <div class="button04">
                        <a href="javascript:void(0);" onclick="LinkClick()">海貨御者ﾏｽﾀ登録</a>
                    </div>    
                </td>
                <td style="width:700px;Font-Size:25px;" >
                </td>
<%--                                    <a href="make_si.aspx?id={0}">SI</a>--%>
            </tr>
        </table>

        <table style="height:10px;">
            <tr>
                <td style="width:1500px;" >
                    <asp:Label ID="Label1" runat="server" Text="エラー ： 赤：日付異常 / 緑：全角 / 青：改行"></asp:Label>
                </td>
            </tr>
        </table>


        <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">
            <div class="wrapper">
                <table class="sticky">
                    <thead class="fixed">
                    </thead>

                    <tbody>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="8190" Height="100px" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px">
                        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
                        <HeaderStyle CssClass="Freezing"></HeaderStyle>

                        <Columns>


                        <asp:BoundField DataField="CUST_CD02" HeaderText="請求客先
                            コード" SortExpression="CUST_CD02"  >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ETD" HeaderText="Sailing On/About
                            (計上日)" SortExpression="ETD"  >
                        <HeaderStyle Width="130px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="Finaldestination
                            (届け先名)" SortExpression=""  >
                        <HeaderStyle Width="350px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="Finaldestination ADDRESS(届け先住所)" SortExpression="" >
                        <HeaderStyle Width="1200px" />
                        </asp:BoundField>


                        <asp:BoundField DataField="LOADING_PORT" HeaderText="PORTOF LOADING
                            (積み出し港)" SortExpression="LOADING_PORT" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DISCHARGING_PORT" HeaderText="PORT OF DEISCHARGE(揚地)" SortExpression="DISCHARGING_PORT" >
                        <HeaderStyle Width="150px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="PALECE OF DELIVERY(配送先)" SortExpression="PLACE_OF_DELIVERY" >
                        <HeaderStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PLACE_OF_RECEIPT" HeaderText="荷受地" SortExpression="PLACE_OF_RECEIPT" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="PLACE OF DELIVERY BY CARRIER(配送者責任送り先)" SortExpression="PLACE_OF_DELIVERY" >
                        <HeaderStyle Width="190px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CUT_DATE" HeaderText="CUT日" SortExpression="CUT_DATE" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ETA" HeaderText="到着日" SortExpression="ETA" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ETD" HeaderText="入出港日" SortExpression="ETD" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CUT_DATE" HeaderText="搬入日" SortExpression="CUT_DATE" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>


                        <asp:BoundField DataField="" HeaderText="出荷元
                            ストアコード" SortExpression="" >
                        <HeaderStyle Width="80px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="出荷方法" SortExpression="" >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="出荷拠点" SortExpression="" >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="VOYAGE_NO" HeaderText="voyage" SortExpression="VOYAGE_NO" >
                        <HeaderStyle Width="80px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="BOOK_TO" HeaderText="船社" SortExpression="BOOK_TO" >
                        <HeaderStyle Width="90px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="船社担当者" SortExpression="" >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Forwarder" HeaderText="乙仲名" SortExpression="Forwarder" >
                        <HeaderStyle Width="200px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="乙仲担当者" SortExpression="" >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="BOOKING_NO" HeaderText="booking no" SortExpression="BOOKING_NO" >
                        <HeaderStyle Width="100px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="VESSEL_NAME" HeaderText="船名(ocean vessll)" SortExpression="VESSEL_NAME" >
                        <HeaderStyle Width="150px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="consinerr name of SI" SortExpression="" >
                        <HeaderStyle Width="300px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="consiner address of SI" SortExpression="" >
                        <HeaderStyle Width="1100px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="place of delivery SI" SortExpression="PLACE_OF_DELIVERY" >
                        <HeaderStyle Width="140px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="Nortify address" SortExpression="" >
                        <HeaderStyle Width="1900px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="通関方法" SortExpression="" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="ベアリング帳票出力" SortExpression="" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="船積スケジュール登録" SortExpression="" >
                        <HeaderStyle Width="130px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="コンテナ情報登録" SortExpression="" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="INVOICE内訳自動計算" SortExpression="" >
                        <HeaderStyle Width="130px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="" HeaderText="海貨業者" SortExpression="" >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TWENTY_FEET" HeaderText="20Ft" SortExpression="TWENTY_FEET" >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="FOURTY_FEET" HeaderText="40Ft" SortExpression="FOURTY_FEET" >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="LCL_QTY" HeaderText="LCL/40Ft" SortExpression="LCL_QTY" >
                        <HeaderStyle Width="60px" />
                        </asp:BoundField>


                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
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

        <table style="height:10px;">
        </table>
    </div>
    
        <footer>

        <div id="footermenu" class="inner">
        </div>
        <!--/footermenu-->

        <div id="copyright">
        </div>

        </footer>

        <!--/#contents2-->

        <!--ページの上部に戻る「↑」ボタン-->
        <p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

   
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM T_BOOKING WHERE STATUS <> 'キャンセル' AND INVOICE_NO ='' AND BOOKING_NO <>'' "></asp:SqlDataSource>

        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM T_BOOKING WHERE STATUS <> 'キャンセル' AND INVOICE_NO ='' AND BOOKING_NO <>'' "></asp:SqlDataSource>--%>

       
<%--    SELECT T_BOOKING.STATUS, T_BOOKING.Forwarder, T_BOOKING.CUST_CD02, T_BOOKING.LOADING_PORT, T_BOOKING.DISCHARGING_PORT, T_BOOKING.PLACE_OF_DELIVERY, T_BOOKING.PLACE_OF_RECEIPT, T_BOOKING.CUT_DATE, T_BOOKING.ETA, T_BOOKING.ETD, T_BOOKING.ETD, T_BOOKING.TWENTY_FEET, T_BOOKING.FOURTY_FEET, T_BOOKING.LCL_QTY, T_BOOKING.VOYAGE_NO, T_BOOKING.BOOK_TO, T_BOOKING.BOOKING_NO, T_BOOKING.VESSEL_NAME FROM T_BOOKING--%>

    </form> 

</body>

</html>
