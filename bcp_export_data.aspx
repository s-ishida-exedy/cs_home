<%@ Page Language="VB" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="bcp_export_data.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>BCP詳細(海外出荷案件)</title>
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



    <script>
        $(document).ready(function () {
            if (document.getElementById("DropDownList2").value == 'Please select') {
                document.getElementById("DropDownList2").value = window.opener.document.getElementById("port").value;
                document.getElementById("TextBox1").value = window.opener.document.getElementById("TextBox1").value;
                document.getElementById("TextBox2").value = window.opener.document.getElementById("TextBox2").value;
                let hangoutButton = document.getElementById("Button1");
                hangoutButton.click();
            }
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
                <h2>BCP詳細(海外出荷案件)</h2>
            </td>
            <td style="width:270px;">
            </td>
        </tr>
    </table>

    <table style="height:10px;">
        <tr>
            <td style="width:600px;" >
                <asp:Button class="button010"  ID="Button4" runat="server" Text="エクセル出力" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
                &nbsp;&nbsp;&nbsp;&nbsp;
                開始日：<asp:TextBox class="button01"  ID="TextBox1" runat="server"  type="date" Width="120px" Height="18px" AutoPostBack="false" Font-Size="18px"></asp:TextBox>
                &nbsp;&nbsp;～&nbsp;&nbsp;
                終了日：<asp:TextBox class="button01"  ID="TextBox2" runat="server"  type="date" Width="120px" Height="18px" AutoPostBack="false" Font-Size="18px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="DropDown" Font-Size="14px" AppendDataBoundItems="true" >
                <asp:ListItem Text="Please select" Value="Please select" />
                <asp:ListItem Text="OSAKA" Value="OSAKA" />
                <asp:ListItem Text="KOBE" Value="KOBE" />
                <asp:ListItem Text="YOKKAICHI" Value="YOKKAICHI" />
                <asp:ListItem Text="NAGOYA" Value="NAGOYA" />

                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="表示"  Width="50px" />
            </td>
            <td style="width:150px;" >
            </td>
            <td style="width:180px;" >
            </td>
        <td style="width:80px;" >
        </td>
        </tr>
    </table>


    <table style="height:10px;">
    </table>

    <table >
        <tr>
            <td style="width:270px;Font-Size:11px;">
                ＜サマリ＞
            </td>
        </tr>
    </table>

    <table border='1' style="width:250px;Font-Size:11px;" class ="sample1">
        <thead>
            <tr >
                <td style="width:80px;"><asp:Label ID="Label01" runat="server" Text="港"></asp:Label></td>
                <td style="width:80px;"><asp:Label ID="Label02" runat="server" Text="コンテナ"></asp:Label></td>
                <td style="width:80px;"><asp:Label ID="Label03" runat="server" Text="金額"></asp:Label></td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style="width:80px;"><asp:Label ID="Label04" runat="server" Text="0"></asp:Label></td>
                <td style="width:80px;"><asp:Label ID="Label05" runat="server" Text="0"></asp:Label></td>
                <td style="width:80px;"><asp:Label ID="Label06" runat="server" Text="0"></asp:Label></td>
            </tr>
        </tbody>
    </table>

    <table style="height:10px;">
    </table>

    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

        <div class="wrapper">
        <table class="sticky">
        <thead class="fixed">

        </thead>

        <tbody>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" width="2000px" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />

        <Columns>

        <asp:BoundField DataField="PLACE_OF_RECEIPT" HeaderText="荷受港" SortExpression="PLACE_OF_RECEIPT" >
        <HeaderStyle Width="70px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="FORWARDER02" HeaderText="海貨業者" SortExpression="FORWARDER02" ReadOnly="true" >
        <HeaderStyle Width="110px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="CUST" HeaderText="客先" SortExpression="CUST" ReadOnly="true" >
        <HeaderStyle Width="40px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>


        <asp:BoundField DataField="INVOICE" HeaderText="IVNO" SortExpression="INVOICE" ReadOnly="true" >
        <HeaderStyle Width="170px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" ReadOnly="true" >
        <HeaderStyle Width="100px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="BOOKING_NO" HeaderText="BKG#" SortExpression="BOOKING_NO" ReadOnly="true" >
        <HeaderStyle Width="110px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="CONTAINER" HeaderText="コンテナ数" SortExpression="CONTAINER" ReadOnly="true" >
        <HeaderStyle Width="50px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="FIN_FLG" HeaderText="金額" SortExpression="FIN_FLG" ReadOnly="true" >
        <HeaderStyle Width="50px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="配送先" SortExpression="PLACE_OF_DELIVERY" >
        <HeaderStyle Width="170px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="FLG01" HeaderText="委託" SortExpression="FLG01" >
        <HeaderStyle Width="30px"/>
        <ItemStyle Font-Size="Small" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="FLG02" HeaderText="書類" SortExpression="FLG02" >
        <HeaderStyle Width="30px"/>
        <ItemStyle Font-Size="Small" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="FLG03" HeaderText="通関" SortExpression="FLG03" >
        <HeaderStyle Width="30px"/>
        <ItemStyle Font-Size="Small" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="FLG02" HeaderText="代替ルート" SortExpression="FLG02" >
        <HeaderStyle Width="200px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>

        <asp:BoundField DataField="FLG02" HeaderText="代替業者" SortExpression="FLG02" >
        <HeaderStyle Width="200px"/>
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>


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
<p class="nav-fix-pos-pagetop"><a href="#">↑href="#">↑</a></p>
        
<%--GV初期値--%>
<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT PLACE_OF_RECEIPT  ,FORWARDER02  ,CUST ,INVOICE  ,ETD ,BOOKING_NO  ,CONTAINER  ,FIN_FLG  ,PLACE_OF_DELIVERY  ,FLG01 ,FLG02,FLG03 ,FLG04  FROM T_EXL_CSKANRYO "
></asp:SqlDataSource>--%>
<%--GV初期値--%>
<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [T_EXL_CSANKEN].* FROM [T_EXL_CSANKEN] "
></asp:SqlDataSource>--%>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_CSANKEN] WHERE CUST ='' ORDER BY CUT_DATE"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT distinct PLACE_OF_RECEIPT FROM [T_EXL_CSANKEN] WHERE PLACE_OF_RECEIPT <>'' "></asp:SqlDataSource>

</form>

</body>
</html>
