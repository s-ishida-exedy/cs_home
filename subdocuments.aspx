﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="subdocuments.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>案件抽出</title>
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

                    <h2>案件抽出</h2>

                </td>






<td style="width:80px;" >




<%--<div class="button04">
<a href="anken_booking01.aspx?id={0}">当日案件へ</a>
</div>--%>

</td>

<td style="width:80px;" >

<div class="button04">
<a href="anken_booking.aspx?id={0}">全案件へ</a>
</div>

</td>


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

            <asp:Label ID="Label1" runat="server" Text="１．ＬＳ７、９（限定・試作）が同梱されている案件にチェックを入れる。"></asp:Label><br>
            <asp:Label ID="Label2" runat="server" Text="２．登録ボタンを押す。"></asp:Label><br>

            </td>
        </tr>
    </table>


<asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>

<tbody>
                                 
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "2500px" DataSourceID="SqlDataSource1" DataKeyNames="INVOICE_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >


                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
<%--                    <AlternatingRowStyle BackColor="#DCDCDC" />--%>
                    <Columns>


                    <asp:BoundField DataField="CUSTCODE" HeaderText="客先コード" SortExpression="CUSTCODE" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="CUSTNAME" HeaderText="客先名" SortExpression="CUSTNAME" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="INVOICE_NO" HeaderText="IVNO" SortExpression="INVOICE_NO" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="REV_ETD" HeaderText="REV_ETD" SortExpression="REV_ETD" ReadOnly="true" ></asp:BoundField>

                    <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="REV_ETA" HeaderText="REV_ETA" SortExpression="REV_ETA" ReadOnly="true" ></asp:BoundField>

                    <%--            <asp:BoundField DataField="MEMOFLG" HeaderText="メモ出力" SortExpression="MEMOFLG" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="SIFLG" HeaderText="SI出力" SortExpression="SIFLG" ReadOnly="true" ></asp:BoundField>--%>

                    <asp:BoundField DataField="SHIP_TYPE" HeaderText="種類" SortExpression="SHIP_TYPE" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="DATE_GETBL" HeaderText="BL回収" SortExpression="DATE_GETBL" ></asp:BoundField>
                    <asp:BoundField DataField="DATE_ONBL" HeaderText="BL上の日付" SortExpression="DATE_ONBL" ></asp:BoundField>

                    <asp:BoundField DataField="REV_SALESDATE" HeaderText="修正後計上日" SortExpression="REV_SALESDATE" ></asp:BoundField>
                    <asp:BoundField DataField="REV_STATUS" HeaderText="修正状況" SortExpression="REV_STATUS" ></asp:BoundField>
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

                    <%--            <asp:BoundField DataField="FLG01" HeaderText="FLG01" SortExpression="FLG01" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="FLG02" HeaderText="FLG02" SortExpression="FLG02" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="FLG03" HeaderText="FLG03" SortExpression="FLG03" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="FLG04" HeaderText="FLG04" SortExpression="FLG04" ReadOnly="true" ></asp:BoundField>
                    <asp:BoundField DataField="FLG05" HeaderText="FLG05" SortExpression="FLG05" ReadOnly="true" ></asp:BoundField>--%>


</Columns>
<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
<RowStyle BackColor="#FFFFFF" ForeColor="Black" />
<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

</asp:GridView>

</tbody>
</table>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE CUSTCODE in ('K53Y') AND [ETD] > GETDATE()-10 ORDER BY ETD"></asp:SqlDataSource>

<%--    CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530')--%>

</div>


</asp:Panel>   



</div>


<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>
</form>


</body>
</html>
