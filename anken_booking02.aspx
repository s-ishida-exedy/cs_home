<%@ Page Language="VB" AutoEventWireup="false" CodeFile="anken_booking02.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ポータルサイト(案件抽出)</title>
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
            background-color: #6fbfd1;
            color: #ffffff;
        }
        .wrapper {
            overflow: scroll;
            height: 300px;
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

        .design01 {
            width: 65%;
            text-align: center;
            border-collapse: collapse;
            border-spacing: 0;
            border: solid 1px #778ca3;
            background: #6fbfd1;
        }
        .design01 tr {
            border-top: dashed 1px #778ca3;
        }
        .design01 th {
            padding: 0px;
            background: #e9faf9;
        }
        .design01 td {
            padding: 0px;
        }

        .design02 {
            width: 65%;
            text-align: center;
            border-collapse: collapse;
            border-spacing: 0;
            border: solid 1px #778ca3;
        }
        .design02 tr {
            border-top: dashed 1px #778ca3;
        }
        .design02 th {
            padding: 0px;
            background: #e9faf9;
        }
        .design02 td {
            padding: 0px;
        }

        .header-ta {
            width: 1300px;
        }
        .first-cell {
            text-align:left;
            font-size:25px;
            width: 400px;
        }
        .second-cell {
            width: 700px;
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

        if (document.getElementById("Label22").innerText  == '未') {
            document.getElementById("Label22").style.color = 'red';
            document.getElementById("Label22").style.fontWeight = "bold";
        } else  {
            document.getElementById("A01").style.color = 'black';
        };

    });

</script>


</head>
<body class="c2">
<form id="form1" runat="server">


<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
<% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
    <!-- #Include File="header/header.aspx" -->
<% Else %>
    <!-- #Include File="header/exl_header.aspx" -->
<% End If %>

<div id="contents2" class="inner2">

    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>案件抽出</h2> 
            </td>
            <td class="second-cell">
            </td>
            <td class="third-cell">
                <% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
                    <a href="./anken_booking.aspx">案件管理表へ</a>
                <% Else %>

                <% End If %>                
            </td>
        </tr>
    </table>

    <table style="width:320px;" >
        <tr>
            <asp:Label  ID="Label10" runat="server" Text="<<登録、フォルダ作成が完了してます>>" Font-Size="15px" forecolor="red" Font-Bold="true" />
            <asp:Label  ID="Label11" runat="server" Text="<<未登録です。登録しチェックを押してください。>>" Font-Size="15px"  Font-Bold="true" />

        </tr>
    </table>

    <table style="width:1000px;height:10px;" >
        <tr>
            <td style="width:20px;" class ="design01" >
                <asp:Label  ID="Label5" runat="server" ForeColor ="white" Text="KD"  Height="13px" AutoPostBack="True" Font-Size="15px" /> 
            </td>   
            <td style="width:30px;" class ="design02" >
                <asp:CheckBox   ID="CheckBox1" runat="server" Height="13px" AutoPostBack="True" Font-Size="15px" />
                <asp:Label  ID="Label3" runat="server"  Height="13px" AutoPostBack="True" Font-Size="15px" /> 
            </td>   
            <td style="width:20px;" class ="design01">
                <asp:Label  ID="Label6" runat="server"  ForeColor ="white"  Text="ｱﾌﾀ"  Height="13px" AutoPostBack="True" Font-Size="15px" />
            </td>   
            <td style="width:30px;" class ="design02" > 
                <asp:CheckBox   ID="CheckBox2" runat="server" Height="13px" AutoPostBack="True" Font-Size="15px" />
                <asp:Label  ID="Label4" runat="server"  Height="13px" AutoPostBack="True" Font-Size="15px" /> 
            </td>
            <td style="width:20px"class ="design01" >
                <asp:Label  ID="Label8" runat="server"  ForeColor ="white"  Text="判定"  Height="13px" AutoPostBack="True" Font-Size="15px" />                           
            </td>                   
            <td style="width:20px" class ="design02"　>
                <asp:Label ID="Label7" runat="server" Font-Size="20px"/>
            </td>
            <td style="width:30px"class ="design01" >
                <asp:Label  ID="Label21" runat="server"  ForeColor ="white"  Text="メール"  Height="13px" AutoPostBack="True" Font-Size="15px" />                           
            </td>                   
            <td style="width:20px" class ="design02"　>
                <asp:Label ID="Label22" runat="server"  Height="13px" AutoPostBack="True" Font-Size="15px" /> 
            </td>
            <td style="width:20px;" >
            </td>
            <td style="width:70px;" >
                <% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
                    <asp:Button ID="Button1"  CssClass ="btn00" runat="server" Text="フォルダ作成" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" />
                <% Else %>
                <% End If %> 
            </td>                                                                                           
            <td style="width:70px;" >
                <% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
                    <asp:Button ID="Button4"  CssClass ="btn00" runat="server" Text="メール" Width="75px" Height="40px" AutoPostBack="True" Font-Size="13px" />
                <% Else %>
                <% End If %> 
            </td>       
            <td style="width:100px;Font-Size:25px;" >
                <% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
                    <div class="button04">
                        <a href="javascript:void(0);" onclick="LinkClick()">アドレス登録</a>
                    </div>      
                <% Else %>
                <% End If %>                
            </td>
        </tr>
    </table>

    <table style="width:320px;height:20px;" >
        <tr>
            <p><asp:Label  ID="Label9" runat="server" Text="※作業完了後、チェックを入れてください。" Font-Size="12px" /></p>
            <p><asp:Label  ID="Label14" runat="server" Text="※LCL案件は委託メールが作成されません。" Font-Size="12px" /></p>
            <asp:Label  ID="Label13" runat="server" Text="※作業は8:30以降にしてください。" Font-Size="12px" ForeColor="red" />
        </tr>
    </table>

    <table style="width:700px;height:10px;">
        <tr>
            <td style="width:100px;" >
                <asp:Button ID="Button3" CssClass ="btn00" runat="server" Text="登録" Width="75px" Height="40px" AutoPostBack="True" Font-Size="13px" />
            </td>
            <td style="width:100px;" >
                <asp:Button ID="Button2"  CssClass ="btn00" runat="server" Text="解除" Width="75px" Height="40px" AutoPostBack="True" Font-Size="13px" />
            </td>
            <td style="width:500px; font-size:10px;" >
                <asp:Label ID="Label1" runat="server" Text="１．ＬＳ７、９（限定・試作）・ＤＮＸ混載出荷　が同梱されている案件にチェックを入れる。"></asp:Label><br>
                <asp:Label ID="Label2" runat="server" Text="２．登録ボタンを押す。"></asp:Label><br>
            </td>
        </tr>
    </table>



    <asp:Label  ID="Label20" runat="server" Text="<<本日は対象案件なしです。>></br><<そのままチェックを入れてください。>>" Font-Size="15px"  Font-Bold="true" ForeColor="red" />


    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

    <div class="wrapper">
    <table class="sticky">
    <thead class="fixed">

    </thead>

    <tbody>
                                 
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#6fbfd1" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" DataSourceID="SqlDataSource1" Width="927px">
    <HeaderStyle BackColor="#6fbfd1" Font-Bold="True" ForeColor="BLACK" />
    <HeaderStyle CssClass="Freezing" />
    <Columns>

    <asp:TemplateField>

    <ItemTemplate>
    <asp:CheckBox ID="cb" runat="server" Checked="false"  />
    </ItemTemplate>

    </asp:TemplateField>

    <asp:BoundField DataField="FLG02" HeaderText="状況" SortExpression="FLG02" />
    <asp:BoundField DataField="FORWARDER" HeaderText="シート" SortExpression="FORWARDER" />
    <asp:BoundField DataField="FORWARDER02" HeaderText="海貨業者" SortExpression="FORWARDER02" />
    <asp:BoundField DataField="CUST" HeaderText="客先コード" SortExpression="CUST" />
    <asp:BoundField DataField="INVOICE" HeaderText="INVOICE" SortExpression="INVOICE" />
    <asp:BoundField DataField="STATUS" HeaderText="進捗状況" SortExpression="STATUS" />
    <asp:BoundField DataField="CUT_DATE" HeaderText="CUT" SortExpression="CUT_DATE" />
    <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" />
    <asp:BoundField DataField="BOOKING_NO" HeaderText="BOOKING_NO" SortExpression="BOOKING_NO" />
    <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA" />
    <asp:BoundField DataField="BOOK_TO" HeaderText="BOOK_TO" SortExpression="BOOK_TO" />
    <asp:BoundField DataField="VESSEL_NAME" HeaderText="船名" SortExpression="VESSEL_NAME" />
    <asp:BoundField DataField="VOYAGE_NO" HeaderText="VOYAGE_NO" SortExpression="VOYAGE_NO" />
    <asp:BoundField DataField="PLACE_OF_RECEIPT" HeaderText="荷受港" SortExpression="PLACE_OF_RECEIPT" />
    <asp:BoundField DataField="LOADING_PORT" HeaderText="積出港" SortExpression="LOADING_PORT" />
    <asp:BoundField DataField="DISCHARGING_PORT" HeaderText="揚港" SortExpression="DISCHARGING_PORT" />
    <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="配送先" SortExpression="PLACE_OF_DELIVERY" />
    <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" />
    <asp:BoundField DataField="LCL_QTY" HeaderText="出荷形態" SortExpression="LCL_QTY" />

    </Columns>
    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

    </asp:GridView>

    </tbody>
    </table>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_CSANKEN] WHERE FLG01 = '1' ORDER BY CUT_DATE"></asp:SqlDataSource>

    </div>

    </asp:Panel>   


    <asp:Panel ID="Panel2" runat="server"  Font-Size="30px">

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

        <asp:Panel ID="Panel3" runat="server"  Font-Size="20px" Visible ="false">

        
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

</div>

<div id="contents2" class="inner2">


    <asp:Panel ID="Panel4" runat="server"  Font-Size="12px">

    <asp:Label ID="Label19" runat="server" Text="＜過去履歴＞"></asp:Label>

    <div class="wrapper">
    <table class="sticky">
    <thead class="fixed">

    </thead>

    <tbody>
                                 
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="#6fbfd1" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" DataSourceID="SqlDataSource2" Width="980px">
    <HeaderStyle BackColor="#6fbfd1" Font-Bold="True" ForeColor="BLACK" />
    <HeaderStyle CssClass="Freezing" />
    <Columns>


    <asp:BoundField DataField="STATUS" HeaderText="状況" SortExpression="STATUS" />
    <asp:BoundField DataField="BKGSHEET" HeaderText="シート" SortExpression="BKGSHEET" />
    <asp:BoundField DataField="FORWARDER" HeaderText="海貨御者" SortExpression="FORWARDER" />
    <asp:BoundField DataField="CUST" HeaderText="客先コード" SortExpression="CUST" />
    <asp:BoundField DataField="INVOICE" HeaderText="INVOICE" SortExpression="INVOICE" />
    <asp:BoundField DataField="CUT" HeaderText="CUT" SortExpression="CUT" />
    <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" />
    <asp:BoundField DataField="BKGNO" HeaderText="BKGNO" SortExpression="BKGNO" />
    <asp:BoundField DataField="SHIP_TYPE" HeaderText="登録・更新日" SortExpression="SHIP_TYPE" />

    </Columns>
    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

    </asp:GridView>

    </tbody>
    </table>

    </asp:Panel>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_ANKEN_HISTORY] ORDER BY SHIP_TYPE "></asp:SqlDataSource>

    </div>




</div>

<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>
</form>


</body>
</html>
