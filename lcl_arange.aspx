<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lcl_arange.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ポータルサイト(LCL出荷作業進捗)</title>
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
                    	/*border-top: 0px solid #999;
	border-left: 0px solid #999;*/
        }
        .wrapper {
          overflow: scroll;
          height: 450px;
        }





        h2 {
          position: relative;
          overflow: hidden;
          padding: 1.5rem 2rem 1.5rem 130px;
          border: 2px solid #000;
        }

        h2:before {
          position: absolute;
          top: -150%;
          left: -100px;
          width: 200px;
          height: 300%;
          content: '';
          -webkit-transform: rotate(25deg);
          transform: rotate(25deg);
          background: #000;
        }

        h2 span {
          font-size: 40px;
          font-size: 4rem;
          position: absolute;
          z-index: 1;
          top: 0;
          left: 0;
          display: block;
          padding-top: 3px;
          padding-left: 16px;
          color: #fff;
        }

        .simple_square_btn4 {
	display: block;
	position: relative;
	width: 160px;
	padding: 0.1em;
	text-align: center;
	text-decoration: none;
	color: #1B1B1B;
	background: #fff;
	border-radius: 30px;
	border:1px solid #1B1B1B;
	-webkit-backface-visibility: hidden; 
	-moz-backface-visibility: hidden;
    backface-visibility: hidden;
}
.simple_square_btn4:hover {
	 cursor: pointer;
	 text-decoration: none;
	-webkit-animation: simple_square_btn4 0.4s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
	-moz-animation: simple_square_btn4 0.4s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
	        animation: simple_square_btn4 0.4s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
}
@-webkit-keyframes simple_square_btn4{
  0% {
    -webkit-transform: scale(1);
            transform: scale(1);
  }
  100% {
    -webkit-transform: scale(0.85);
            transform: scale(0.85);
  }
}
@-moz-keyframes simple_square_btn4{
  0% {
    -webkit-transform: scale(1);
            transform: scale(1);
  }
  100% {
    -webkit-transform: scale(0.85);
            transform: scale(0.85);
  }
}
@keyframes simple_square_btn4 {
  0% {
    -webkit-transform: scale(1);
            transform: scale(1);
  }
  100% {
    -webkit-transform: scale(0.85);
            transform: scale(0.85);
  }
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
<!-- メニューの編集はheader.htmlで行う -->
 <!-- #Include File="header.html" -->
       
<div id="contents2" class="inner2">

<div>
<table >
<tr>

<td width="200" border="1" >

<font size="6"  >
<b>LCL出荷準備進捗

</font>
</td>

<td width="120" border="1" >

<asp:Button class="btn-radius-gradient-wrap"  ID="Button1" runat="server" Text="追加" Width="75px" Height="40px" AutoPostBack="True" Font-Size="13px" />

</td>


<td width="130">

<font size="2" style="background-color:DarkGray"  >・グレー:書類作成済<br></font>
<font size="2" style="background-color:LightBlue" >・ブルー:手配依頼済</font>
</td>

<td width="140">

<font size="2" style="background-color:Salmon"  >・レッド:これから手配が必要</font><font size="2"> </font><br>
<font size="2" style="background-color:red" Color="White" >・AC要:BOOKING未確定</font>

</td>

<td width="100">

<section>
<font size="2"  >
<a href="lcl_tenkai.aspx?id={0}" class="simple_square_btn4">展開済案件<span></span></a>
</section>
    
</font>
</td>

<td width="100">
<section>
<font size="2"  >
<a href="lcl_notcomfirmed.aspx?id={0}" class="simple_square_btn4">未確定案件<span></span></a>
                        
</font>
</section>

</td>

</tr>

</table>

<font size="2">


<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>

<tbody>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1250px" BackColor="White" BorderColor="#555555" BorderStyle="none" BorderWidth="3px" CellPadding="3" GridLines="Both">


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
</font>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CONSIGNEE], [CUST_CD], [DESTINATION], [INVOICE_NO], [CUT_DATE], [CUT_DATE]AS CUT_DATE2, [ETD], [ETA], [LCL_QTY], [OFFICIAL_QUOT],[BOOKING_NO] FROM [T_BOOKING] WHERE [LCL_QTY] like '%M3%' AND [CUT_DATE] <>'' AND [CUT_DATE] > GETDATE()-1 AND [CUT_DATE] < GETDATE()+45  ORDER BY [CUT_DATE]  "></asp:SqlDataSource>

</div>  

</div>

<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
