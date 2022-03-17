<%@ Page Language="VB" AutoEventWireup="false" CodeFile="anken_booking.aspx.vb" Inherits="yuusen" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(出荷案件管理)</title>
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
                <td style="width:250px;Font-Size:25px;" >
                    <h2>出荷案件管理</h2>
                </td>
                <td style="width:100px;" >
                </td>
                <td style="width:550px;">
                    <h1>フィルタ</h1>
                    <p></p>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" >
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>進捗状況</asp:ListItem>
                    <asp:ListItem>シート</asp:ListItem>
                    <asp:ListItem>海貨業者</asp:ListItem>
                    <asp:ListItem>客先コード</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="140px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" ></asp:DropDownList>
                    <asp:Button CssClass ="button01" ID="Button1" runat="server" Text="全件表示" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" />
                </td>
                <td style="width:150px;" >
                </td>
                <td style="width:150px;" >
                    <div class="button04">
                    <a href="anken_booking02.aspx?id={0}">案件抽出</a>
                    </div>
                </td>
            </tr>
        </table>    

        <table style="height:10px;">
            <tr>
                <td style="width:1500px;" >
                <asp:Button  ID="Button2" CssClass="btn00" runat="server" Text="フォルダ作成登録" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
                <asp:Button  ID="Button3" CssClass="btn00" runat="server" Text="フォルダ作成" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
                <p></p>
                <asp:Label ID="Label1" runat="server" Text="※チェック後にボタンを押す。(チェックボックスが緑のものはフォルダ作成可能)"  Font-Size="10px"></asp:Label>
                </td>
            </tr>
        </table>

        <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">
            <div class="wrapper">
                <table class="sticky">
                    <thead class="fixed">
                    </thead>

                    <tbody>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource7" Width="2000px" Height="100px" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px">
                        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
                        <HeaderStyle CssClass="Freezing"></HeaderStyle>

                        <Columns>
                        <asp:TemplateField>
                        <ItemTemplate>
                        <asp:CheckBox ID="cb" Checked="false" runat="server"/>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="STATUS" HeaderText="進捗状況" SortExpression="STATUS" />
                        <asp:BoundField DataField="FORWARDER" HeaderText="シート" SortExpression="FORWARDER" />
                        <asp:BoundField DataField="FORWARDER02" HeaderText="海貨業者" SortExpression="FORWARDER02" />
                        <asp:BoundField DataField="CUST" HeaderText="客先コード" SortExpression="CUST" />
                        <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" />
                        <asp:BoundField DataField="INVOICE" HeaderText="INVOICE" SortExpression="INVOICE" />
                        <asp:BoundField DataField="CUT_DATE" HeaderText="CUT" SortExpression="CUT_DATE" />
                        <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" />
                        <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA" />
                        <asp:BoundField DataField="TWENTY_FEET" HeaderText="20Ft" SortExpression="TWENTY_FEET" />
                        <asp:BoundField DataField="FOURTY_FEET" HeaderText="40Ft" SortExpression="FOURTY_FEET" />
                        <asp:BoundField DataField="LCL_QTY" HeaderText="LCL/40Ft" SortExpression="LCL_QTY" />
                        <asp:BoundField DataField="CONTAINER" HeaderText="コンテナ" SortExpression="CONTAINER" />
                        <asp:BoundField DataField="DAY01" HeaderText="VAN01" SortExpression="DAY01" />
                        <asp:BoundField DataField="DAY02" HeaderText="VAN02" SortExpression="DAY02" />
                        <asp:BoundField DataField="DAY03" HeaderText="VAN03" SortExpression="DAY03" />
                        <asp:BoundField DataField="DAY04" HeaderText="VAN04" SortExpression="DAY04" />
                        <asp:BoundField DataField="DAY05" HeaderText="VAN05" SortExpression="DAY05" />
                        <asp:BoundField DataField="DAY06" HeaderText="VAN06" SortExpression="DAY06" />
                        <asp:BoundField DataField="DAY07" HeaderText="VAN07" SortExpression="DAY07" />
                        <asp:BoundField DataField="DAY08" HeaderText="VAN08" SortExpression="DAY08" />
                        <asp:BoundField DataField="DAY09" HeaderText="VAN09" SortExpression="DAY09" />
                        <asp:BoundField DataField="DAY10" HeaderText="VAN10" SortExpression="DAY10" />
                        <asp:BoundField DataField="DAY11" HeaderText="VAN11" SortExpression="DAY11" />
                        <asp:BoundField DataField="FINALVANDATE" HeaderText="最終バン日" SortExpression="FINALVANDATE" />
                        <asp:BoundField DataField="BOOKING_NO" HeaderText="BOOKING_NO" SortExpression="BOOKING_NO" />
                        <asp:BoundField DataField="BOOK_TO" HeaderText="BOOK_TO" SortExpression="BOOK_TO" />
                        <asp:BoundField DataField="VESSEL_NAME" HeaderText="船名" SortExpression="VESSEL_NAME" />
                        <asp:BoundField DataField="VOYAGE_NO" HeaderText="VOYAGE_NO" SortExpression="VOYAGE_NO" />
                        <asp:BoundField DataField="PLACE_OF_RECEIPT" HeaderText="荷受港" SortExpression="PLACE_OF_RECEIPT" />
                        <asp:BoundField DataField="LOADING_PORT" HeaderText="積出港" SortExpression="LOADING_PORT" />
                        <asp:BoundField DataField="DISCHARGING_PORT" HeaderText="揚港" SortExpression="DISCHARGING_PORT" />
                        <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="配送先" SortExpression="PLACE_OF_DELIVERY" />
                        <asp:BoundField DataField="FLG01" HeaderText="FLG01" SortExpression="FLG01" />
                        <asp:BoundField DataField="FLG02" HeaderText="FLG02" SortExpression="FLG02" />
                        <asp:BoundField DataField="FLG03" HeaderText="FLG03" SortExpression="FLG03" />
                        <asp:BoundField DataField="FLG04" HeaderText="FLG04" SortExpression="FLG04" />

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
                            
        <asp:Panel ID="Panel2" runat="server"  Font-Size="12px">
            <div class="wrapper">
                <table class="sticky">

                    <thead class="fixed">
                    </thead>

                    <tbody>
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"  Width="2000px" Height="100px" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px">
                    <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
                    <HeaderStyle CssClass="Freezing"></HeaderStyle>

                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <asp:CheckBox ID="cb" Checked="false" runat="server"  />
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="STATUS" HeaderText="進捗状況" SortExpression="STATUS" />
                    <asp:BoundField DataField="FORWARDER" HeaderText="シート" SortExpression="FORWARDER" />
                    <asp:BoundField DataField="FORWARDER02" HeaderText="海貨業者" SortExpression="FORWARDER02" />
                    <asp:BoundField DataField="CUST" HeaderText="客先コード" SortExpression="CUST" />
                    <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" />
                    <asp:BoundField DataField="INVOICE" HeaderText="INVOICE" SortExpression="INVOICE" />
                    <asp:BoundField DataField="CUT_DATE" HeaderText="CUT" SortExpression="CUT_DATE" />
                    <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" />
                    <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA" />
                    <asp:BoundField DataField="TWENTY_FEET" HeaderText="20Ft" SortExpression="TWENTY_FEET" />
                    <asp:BoundField DataField="FOURTY_FEET" HeaderText="40Ft" SortExpression="FOURTY_FEET" />
                    <asp:BoundField DataField="LCL_QTY" HeaderText="LCL/40Ft" SortExpression="LCL_QTY" />
                    <asp:BoundField DataField="CONTAINER" HeaderText="コンテナ" SortExpression="CONTAINER" />
                    <asp:BoundField DataField="DAY01" HeaderText="VAN01" SortExpression="DAY01" />
                    <asp:BoundField DataField="DAY02" HeaderText="VAN02" SortExpression="DAY02" />
                    <asp:BoundField DataField="DAY03" HeaderText="VAN03" SortExpression="DAY03" />
                    <asp:BoundField DataField="DAY04" HeaderText="VAN04" SortExpression="DAY04" />
                    <asp:BoundField DataField="DAY05" HeaderText="VAN05" SortExpression="DAY05" />
                    <asp:BoundField DataField="DAY06" HeaderText="VAN06" SortExpression="DAY06" />
                    <asp:BoundField DataField="DAY07" HeaderText="VAN07" SortExpression="DAY07" />
                    <asp:BoundField DataField="DAY08" HeaderText="VAN08" SortExpression="DAY08" />
                    <asp:BoundField DataField="DAY09" HeaderText="VAN09" SortExpression="DAY09" />
                    <asp:BoundField DataField="DAY10" HeaderText="VAN10" SortExpression="DAY10" />
                    <asp:BoundField DataField="DAY11" HeaderText="VAN11" SortExpression="DAY11" />
                    <asp:BoundField DataField="FINALVANDATE" HeaderText="最終バン日" SortExpression="FINALVANDATE" />
                    <asp:BoundField DataField="BOOKING_NO" HeaderText="BOOKING_NO" SortExpression="BOOKING_NO" />
                    <asp:BoundField DataField="BOOK_TO" HeaderText="BOOK_TO" SortExpression="BOOK_TO" />
                    <asp:BoundField DataField="VESSEL_NAME" HeaderText="船名" SortExpression="VESSEL_NAME" />
                    <asp:BoundField DataField="VOYAGE_NO" HeaderText="VOYAGE_NO" SortExpression="VOYAGE_NO" />
                    <asp:BoundField DataField="PLACE_OF_RECEIPT" HeaderText="荷受港" SortExpression="PLACE_OF_RECEIPT" />
                    <asp:BoundField DataField="LOADING_PORT" HeaderText="積出港" SortExpression="LOADING_PORT" />
                    <asp:BoundField DataField="DISCHARGING_PORT" HeaderText="揚港" SortExpression="DISCHARGING_PORT" />
                    <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="配送先" SortExpression="PLACE_OF_DELIVERY" />
                    <asp:BoundField DataField="FLG01" HeaderText="FLG01" SortExpression="FLG01" />
                    <asp:BoundField DataField="FLG02" HeaderText="FLG02" SortExpression="FLG02" />
                    <asp:BoundField DataField="FLG03" HeaderText="FLG03" SortExpression="FLG03" />
                    <asp:BoundField DataField="FLG04" HeaderText="FLG04" SortExpression="FLG04" />

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

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_CSANKEN] WHERE ([STATUS] = @STATUS) ORDER BY CUT_DATE">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="STATUS" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_CSANKEN] WHERE ([FORWARDER] = @FORWARDER) ORDER BY CUT_DATE">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="FORWARDER" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_CSANKEN] WHERE ([FORWARDER02] = @FORWARDER02) ORDER BY CUT_DATE">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="FORWARDER02" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_CSANKEN] WHERE ([CUST] = @CUST) ORDER BY CUT_DATE">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="CUST" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
   
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [STATUS] FROM [T_EXL_CSANKEN]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [FORWARDER] FROM [T_EXL_CSANKEN]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [FORWARDER02] FROM [T_EXL_CSANKEN]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [CUST] FROM [T_EXL_CSANKEN]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_CSANKEN]  ORDER BY CUT_DATE"></asp:SqlDataSource>

    </form> 

</body>

</html>
