<%@ Page Language="VB" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="bcp_export.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>BCP(海外出荷案件)</title>
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

    .example01 {
      position: relative;
      }

    .example01 p {
      position: absolute;
      top: 23%;
      left: 14%;
      -ms-transform: translate(-50%,-50%);
      -webkit-transform: translate(-50%,-50%);
      transform: translate(-50%,-50%);
      margin:0;
      padding:0;
      /*文字の装飾は省略*/
      }

    .example02 {
      position: relative;
      }

    .example02 p {
      position: absolute;
      top: 19%;
      left: 36%;
      -ms-transform: translate(-50%,-50%);
      -webkit-transform: translate(-50%,-50%);
      transform: translate(-50%,-50%);
      margin:0;
      padding:0;
      /*文字の装飾は省略*/
      }

    .example03 {
      position: relative;
      }

    .example03 p {
      position: absolute;
      top: 67%;
      left: 60%;
      -ms-transform: translate(-50%,-50%);
      -webkit-transform: translate(-50%,-50%);
      transform: translate(-50%,-50%);
      margin:0;
      padding:0;
      /*文字の装飾は省略*/
      }

    .example04 {
      position: relative;
      }

    .example04 p {
      position: absolute;
      top: 67%;
      left: 80%;
      -ms-transform: translate(-50%,-50%);
      -webkit-transform: translate(-50%,-50%);
      transform: translate(-50%,-50%);
      margin:0;
      padding:0;
      /*文字の装飾は省略*/
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
        var url = 'bcp_export_data.aspx?q='


        if (document.getElementById('TextBox1').value == '' || document.getElementById('TextBox2').value == '') {
            window.alert('日付けが未設定です');
        } else if (document.getElementById('TextBox1').value > document.getElementById('TextBox2').value) {
            window.alert('開始日が終了日を超えています');
        } else {
            var result = confirm('別ウインドウで表示します。');
            if (result) {
                document.body.onclick = mess;
                function mess(e) {
                    var o = e ? e.target : event.srcElement;
                    if (o.tagName == 'A')
                        if (o.innerHTML == '大阪') {
                            document.getElementById('port').value = 'OSAKA';
                        } else if (o.innerHTML == '神戸') {
                            document.getElementById('port').value = 'KOBE';
                        } else if (o.innerHTML == '四日市') {
                            document.getElementById('port').value = 'YOKKAICHI';
                        } else if (o.innerHTML == '名古屋') {
                            document.getElementById('port').value = 'NAGOYA';
                        }
                }
                window.open(url, null);
            }
            else {
            }
        }
        }
  
    </script>

    <script>
        $(document).ready(function () {
            let ele = document.getElementById('port');
            const displayOriginal = ele.style.display;
            ele.style.display = 'none';
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
            <td style="width:400px;Font-Size:25px;" >
                <h2>BCP(海外出荷案件)</h2>
                <asp:TextBox ID="port" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>


    <table>
        <tr>
            <td rowspan="2">
            <div class="example01">  
                <div class="example02">
                    <div class="example03">
                        <div class="example04">
                                <asp:Image ID="Image1" runat="server" ImageUrl="images/mapforpt.png" style="margin-left:30px;width:950px;height:450px;" />
<%--                          <p><asp:Button class="button001"  ID="Button1" runat="server" Text="名古屋" Width="100px" Height="40px" AutoPostBack="True" Font-Size="19px" /></p>	--%>
                            <p><asp:Label ID="Label01" Font-Size="35px" class="label001" runat="server" Text="A"><a href="javascript:void(0);" onclick="LinkClick()">名古屋</a></asp:Label></p>
                        </div>
<%--                        <p><asp:Button class="button001"  ID="Button2" runat="server" Text="四日市" Width="100px" Height="40px" AutoPostBack="True" Font-Size="19px" /></p>	--%>
                            <p><asp:Label ID="Label5" Font-Size="35px" class="label001" runat="server" Text="A"><a href="javascript:void(0);" onclick="LinkClick()">四日市</a></asp:Label></p>
                    </div>
<%--                    <p><asp:Button class="button001"  ID="Button3" runat="server" Text="大阪" Width="100px" Height="40px" AutoPostBack="True" Font-Size="19px" /></p>	--%>
                        <p><asp:Label ID="Label6" Font-Size="35px" class="label001" runat="server" Text="A"><a href="javascript:void(0);" onclick="LinkClick()">大阪</a></asp:Label></p>
                </div>
<%--                <p><asp:Button class="button001"  ID="Button4" runat="server" Text="神戸" Width="100px" Height="40px" AutoPostBack="True" Font-Size="19px" /></p>	--%>
                    <p><asp:Label ID="Label7" Font-Size="35px" class="label001" runat="server" Text="A"><a href="javascript:void(0);" onclick="LinkClick()">神戸</a></asp:Label></p>
            </div>
            </td>

            <td class ="design02"  style="width:270px;Font-Size:18px;text-align:left;" >
                &nbsp;&nbsp;
                <asp:Label  ID="Label4" runat="server" Text="[期間指定]" Height="13px"  Font-Size="18px" />
                <p>&nbsp;&nbsp;開始日&nbsp;:&nbsp;<asp:TextBox class="button01"  ID="TextBox1" runat="server"  type="date" Width="120px" Height="18px" AutoPostBack="false" Font-Size="18px"></asp:TextBox></p>
                <p>&nbsp;&nbsp;終了日&nbsp;:&nbsp;<asp:TextBox class="button01"  ID="TextBox2" runat="server"  type="date" Width="120px" Height="18px" AutoPostBack="false" Font-Size="18px"></asp:TextBox></p>
                <p> &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Font-Size="8" Text="※日付を選択後にチェックを入れる" style="Font-Size:15px"></asp:Label></p>                           
                <p>&nbsp;&nbsp;<asp:Label  ID="Label3" runat="server" Text="" Height="13px"  Font-Size="18px" ForeColor="Red" Font-Bold="true" /></p>   
            </td>
        </tr>
        <tr>   
            <td style="height:320px">                    
            </td>
        </tr>


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
<%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05,iif(len(REV_ETD)=10,REV_ETD,ETD) AS ETD02,iif(len(REV_ETD)=10,ETD,'') AS ETD03,iif(len(REV_ETA)=10,REV_ETA,ETA) AS ETA02,iif(len(REV_ETA)=10,ETA,'') AS ETA03 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE ([REV_STATUS] = @REV_STATUS) AND CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530')  ORDER BY ETD02,INVOICE_NO ">
<SelectParameters>
<asp:ControlParameter ControlID="DropDownList2" Name="REV_STATUS" PropertyName="SelectedValue" Type="String" />
</SelectParameters>
</asp:SqlDataSource>--%>
    
<%--未使用？--%>
<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05,iif(len(REV_ETD)=10,REV_ETD,ETD) AS ETD02,iif(len(REV_ETD)=10,ETD,'') AS ETD03,iif(len(REV_ETA)=10,REV_ETA,ETA) AS ETA02,iif(len(REV_ETA)=10,ETA,'') AS ETA03 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530') ORDER BY ETD02,INVOICE_NO "
UpdateCommand="UPDATE T_EXL_SHIPPINGMEMOLIST SET [DATE_GETBL]=@DATE_GETBL, [DATE_ONBL]=@DATE_ONBL, [REV_SALESDATE]=@REV_SALESDATE, [REV_STATUS]=@REV_STATUS WHERE INVOICE_NO=@INVOICE_NO"
></asp:SqlDataSource>    

</form>

</body>
</html>
