﻿<%@ Page Language="VB" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="shippingmemo.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(シッピングメモ記録)</title>
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
            padding: 0 110px;
            text-align: center;
        }

        h1:before, h1:after {
            content: '';
            position: absolute;
            top: 50%;
            display: inline-block;
            width:80px;
            height: 2px;
            background-color: black;
        }

        h1:before {
            left:0;
        }
        h1:after {
            right: 0;
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
            padding: 10px;
            background: #e9faf9;
        }
        .design02 td {
            padding: 10px;
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
            <td style="width:400px;Font-Size:25px;" >
                <h2>シッピングメモ記録(3カ月前月初～)</h2>
            </td>
<%--            <td style="width:100px;" >
            </td>--%>
            <td style="width:270px;">
                <h1>
                    <asp:Label ID="Label1" runat="server" Text="フィルタ"></asp:Label>
                </h1>
                <p></p>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" >
                <asp:ListItem>--Select--</asp:ListItem>
                <asp:ListItem>未回収</asp:ListItem>
                <asp:ListItem>修正状況</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server" Width="140px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" ></asp:DropDownList>
                <asp:Button CssClass ="button01" ID="Button2" runat="server" Text="全件表示" Width="100px" Height="40px" Font-Size="13px" />
             </td>

             <td class ="design02"  style="width:270px;Font-Size:15px;text-align:left;" >
                    &nbsp;&nbsp;
                    <asp:Label  ID="Label4" runat="server" Text="[期間指定]" Height="13px"  Font-Size="13px" /> 
                    <asp:CheckBox   ID="CheckBox1" runat="server" Height="13px"  Font-Size="13px" AutoPostBack="true" />
                    <asp:TextBox class="button01"  ID="TextBox1" runat="server"  type="date" Width="120px" Height="13px" AutoPostBack="false" Font-Size="13px"></asp:TextBox>
                    <asp:TextBox class="button01"  ID="TextBox2" runat="server"  type="date" Width="120px" Height="13px" AutoPostBack="false" Font-Size="13px"></asp:TextBox>
                    <p> &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Font-Size="8" Text="※日付を選択後、チェックを入れる"></asp:Label></p>
                    <p> &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Font-Size="8" Text=" ※チェック後、左のフィルタで選択"></asp:Label></p>                                
             </td>
        </tr>
    </table>


    <table style="height:10px;">
        <tr>
            <td style="width:300px;" >
                <asp:Button class="button01"  ID="Button1" runat="server" Text="更新" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
                <asp:Button class="button01"  ID="Button4" runat="server" Text="エクセル出力" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
                <asp:Button class="button01"  ID="Button3" runat="server" Text="手動登録" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
                <p></p>
            </td>
            <td style="width:150px;" >
            </td>
            <td style="width:180px;" >
            </td>
        <td style="width:80px;" >
            <div class="button04">
                <a href="shippingmemo_dntget.aspx?id={0}">BL回収進捗</a>
            </div>
        </td>
        </tr>
    </table>


    <table style="height:10px;">
    </table>


    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

        <div class="wrapper">
        <table class="sticky">
        <thead class="fixed">

        </thead>

        <tbody>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "2500px" DataSourceID="SqlDataSource1" DataKeyNames="INVOICE_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >


        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <Columns>

        <asp:TemplateField ShowHeader="False">
        <ItemTemplate>
        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="edt" ImageUrl="~/icon/write.png" Text="編集" width = "20" height = "20" />
        </ItemTemplate>
        <HeaderStyle BackColor="#6B696B" />
        </asp:TemplateField>

        <asp:BoundField DataField="CUSTCODE" HeaderText="客先コード" SortExpression="CUSTCODE" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="CUSTNAME" HeaderText="客先名" SortExpression="CUSTNAME" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="INVOICE_NO" HeaderText="IVNO" SortExpression="INVOICE_NO" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="ETD02" HeaderText="最新ETD" SortExpression="ETD" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="ETD03" HeaderText="遅延前ETD" SortExpression="REV_ETD" ReadOnly="true" ></asp:BoundField>

        <asp:BoundField DataField="ETA02" HeaderText="最新ETA" SortExpression="ETA" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="ETA03" HeaderText="遅延前ETA" SortExpression="REV_ETA" ReadOnly="true" ></asp:BoundField>

        <asp:BoundField DataField="SHIP_TYPE" HeaderText="種類" SortExpression="SHIP_TYPE" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="DATE_GETBL" HeaderText="BL回収" SortExpression="DATE_GETBL" ></asp:BoundField>
        <asp:BoundField DataField="DATE_ONBL" HeaderText="BL上の日付" SortExpression="DATE_ONBL" ></asp:BoundField>

        <asp:BoundField DataField="REV_SALESDATE" HeaderText="修正後計上日" SortExpression="REV_SALESDATE" ></asp:BoundField>
        <asp:BoundField DataField="REV_STATUS" HeaderText="修正状況" SortExpression="REV_STATUS" ></asp:BoundField> <%--12--%>
        <asp:BoundField DataField="BOOKING_NO" HeaderText="BOOKING_NO" SortExpression="BOOKING_NO" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="VOY_NO" HeaderText="VOY_NO" SortExpression="VOY_NO" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="IV_BLDATE" HeaderText="IV_BLDATE" SortExpression="IV_BLDATE" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="KIN_GAIKA" HeaderText="金額（外貨）" SortExpression="KIN_GAIKA" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="RATE" HeaderText="レート" SortExpression="RATE" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="KIN_JPY" HeaderText="金額（JPY）" SortExpression="KIN_JPY" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="VESSEL" HeaderText="船名" SortExpression="VESSEL" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="LOADING_PORT" HeaderText="積出" SortExpression="LOADING_PORT" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="RECEIVED_PORT" HeaderText="荷受" SortExpression="RECEIVED_PORT" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="SHIP_PLACE" HeaderText="出荷拠点" SortExpression="SHIP_PLACE" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="CHECKFLG" HeaderText="確認" SortExpression="CHECKFLG" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="FLG01" HeaderText="ID" SortExpression="FLG01" ReadOnly="true" ></asp:BoundField> <%--24--%>
        <asp:BoundField DataField="FLG02" HeaderText="変更後VOY" SortExpression="FLG02" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="FLG03" HeaderText="変更後VESSEL" SortExpression="FLG03" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="FLG04" HeaderText="変更後LOAD" SortExpression="FLG04" ReadOnly="true" ></asp:BoundField>
<%--        <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" ReadOnly="true" ></asp:BoundField>
        <asp:BoundField DataField="REV_ETD" HeaderText="REV_ETD" SortExpression="REV_ETD" ReadOnly="true" ></asp:BoundField>--%>

        </Columns>

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
        
<%--GV初期値--%>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05,iif(len(REV_ETD)=10,REV_ETD,ETD) AS ETD02,iif(len(REV_ETD)=10,ETD,'') AS ETD03,iif(len(REV_ETA)=10,REV_ETA,ETA) AS ETA02,iif(len(REV_ETA)=10,ETA,'') AS ETA03 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530') AND ETD > DATEADD(day,1,EOMONTH(GETDATE()-90, -1)) ORDER BY ETD02,INVOICE_NO "
></asp:SqlDataSource>

<%--未使用？--%>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [DATE_GETBL] FROM [T_EXL_SHIPPINGMEMOLIST]"></asp:SqlDataSource>

<%--DL修正状況--%>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [REV_STATUS] FROM [T_EXL_SHIPPINGMEMOLIST] WHERE REV_STATUS <>''"></asp:SqlDataSource>

<%--GV未回収選択時--%>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05,iif(len(REV_ETD)=10,REV_ETD,ETD) AS ETD02,iif(len(REV_ETD)=10,ETD,'') AS ETD03,iif(len(REV_ETA)=10,REV_ETA,ETA) AS ETA02,iif(len(REV_ETA)=10,ETA,'') AS ETA03 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE [DATE_GETBL] ='' AND [REV_STATUS] <>'出港済み' AND CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530' )  ORDER BY ETD02,INVOICE_NO  ">
<SelectParameters>
<asp:Parameter DefaultValue="&amp;nbsp;" Name="DATE_GETBL" Type="String" />
</SelectParameters>
</asp:SqlDataSource>

<%--GV修正状況の選択自--%>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05,iif(len(REV_ETD)=10,REV_ETD,ETD) AS ETD02,iif(len(REV_ETD)=10,ETD,'') AS ETD03,iif(len(REV_ETA)=10,REV_ETA,ETA) AS ETA02,iif(len(REV_ETA)=10,ETA,'') AS ETA03 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE ([REV_STATUS] = @REV_STATUS) AND CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530')  ORDER BY ETD02,INVOICE_NO ">
<SelectParameters>
<asp:ControlParameter ControlID="DropDownList2" Name="REV_STATUS" PropertyName="SelectedValue" Type="String" />
</SelectParameters>
</asp:SqlDataSource>
    
<%--未使用？--%>
<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05,iif(len(REV_ETD)=10,REV_ETD,ETD) AS ETD02,iif(len(REV_ETD)=10,ETD,'') AS ETD03,iif(len(REV_ETA)=10,REV_ETA,ETA) AS ETA02,iif(len(REV_ETA)=10,ETA,'') AS ETA03 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530') ORDER BY ETD02,INVOICE_NO "
UpdateCommand="UPDATE T_EXL_SHIPPINGMEMOLIST SET [DATE_GETBL]=@DATE_GETBL, [DATE_ONBL]=@DATE_ONBL, [REV_SALESDATE]=@REV_SALESDATE, [REV_STATUS]=@REV_STATUS WHERE INVOICE_NO=@INVOICE_NO"
></asp:SqlDataSource>    

</form>

</body>
</html>
