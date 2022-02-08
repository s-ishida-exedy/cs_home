<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lcl_tenkai.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>LCL出荷_展開用</title>
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
          text-align:left;
          top: 0;
          z-index: 0;
          background-color: #6fbfd1;
          color: #ffffff;
        }
        .wrapper {
          overflow: scroll;
          height: 350px;
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











.CSSMarquee {
    max-width: 1200px;
    padding: 0.1em 0;
    margin: 0 auto;
    position: relative;
    overflow: hidden;
    text-align: left;
    border-top: 1px solid #000;
    border-bottom: 1px solid #000;
    color:#000;
    font-weight: bold;
}
.CSSMarquee p:after {
    content: "";
    white-space: nowrap;
}
.CSSMarquee p {
    margin: 0;
    padding-left: 100%;
    display: inline-block;
    white-space: nowrap;
    -webkit-animation-name:marqueeRL;
    -webkit-animation-timing-function:linear;
    -webkit-animation-duration:15s;
    -webkit-animation-iteration-count:infinite;
    -moz-animation-name:marqueeRL;
    -moz-animation-timing-function:linear;
    -moz-animation-duration:15s;
    -moz-animation-iteration-count:infinite;
    -ms-animation-name:marqueeRL;
    -ms-animation-timing-function:linear;
    -ms-animation-duration:15s;
    -ms-animation-iteration-count:infinite;
    -o-animation-name:marqueeRL;
    -o-animation-timing-function:linear;
    -o-animation-duration:15s;
    -o-animation-iteration-count:infinite;
    animation-name:marqueeRL;
    animation-timing-function:linear;
    animation-duration:15s;
    animation-iteration-count:infinite;
}
 
@-webkit-keyframes marqueeRL {
    from {-webkit-transform:translate(0);} to {-webkit-transform:translate(-100%);}
}
@-moz-keyframes marqueeRL {
    from {-moz-transform:translate(0);} to {-moz-transform:translate(-100%);}
}
@-ms-keyframes marqueeRL {
    from {-ms-transform:translate(0);} to {-ms-transform:translate(-100%);}
}
@-o-keyframes marqueeRL {
    from {-o-transform:translate(0);} to {-o-transform:translate(-100%);}
}
@keyframes marqueeRL {
    from {transform:translate(0);} to {transform:translate(-100%);}
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
<div >
<table >
<tr>



<td style="width:150px;Font-Size:25px;" >

<h2>LCL出荷-展開用</h2>

</td>


<td>


</td>

<td>



</td>

<td style="width:80px;" >


<div class="button04">
  <a href="lcl_arange.aspx?id={0}">手配状況</a>
</div>    


</td>

</tr>


<tr>
<td style="width:800px;Font-Size:13px;" >


<p><b><asp:Label Font-Size="11" id="Label1" Text="引き取り・搬入予定LCL貨物の情報を展開しておりますので、荷量の追記、トラック手配をお願いいたします。" runat="server"></asp:Label></b></p>
<p><asp:Label Font-Size="8" id="Label3" Text="・重量、荷量は編集ボタンを押して登録してください。" runat="server"></asp:Label></p>
<p><asp:Label Font-Size="8" id="Label2" Text="・出荷後、リストから削除されていきます。" runat="server"></asp:Label></p>
<asp:Label Font-Size="8" id="Label4" Text="・毎週水曜更新　（それ以外に追加・変更などの更新する場合はメールで通知します。）" runat="server"></asp:Label>


</td>

</tr>

</table>


<table>
<tr>
<td>
<div class="CSSMarquee"><p><asp:Label Font-Size="10" id="Label5" Text="更新情報：" runat="server"></asp:Label></p></div>
</td>
</tr>
</table>

<asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

<div class="wrapper">
<table class="sticky" >
<thead class="fixed">

</thead>


<tbody>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "2200px" DataSourceID="SqlDataSource1" DataKeyNames="BOOKING_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >

<HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>


<Columns>

<asp:TemplateField HeaderText="EDITMENU">
<ItemTemplate>
<asp:Button runat="server" CommandName="Edit" Text="編集" />
</ItemTemplate>

<EditItemTemplate>
<asp:Button runat="server" CommandName="Update" Text="保存" />
<asp:Button runat="server" CommandName="Cancel" Text="戻る" />

</EditItemTemplate>
</asp:TemplateField>


<%--<asp:BoundField DataField="CONSIGNEE" HeaderText="CONSIGNEE" SortExpression="CONSIGNEE" />
<asp:BoundField DataField="DESTINATION" HeaderText="DESTINATION" SortExpression="DESTINATION" />--%>
<asp:BoundField DataField="FLG05" HeaderText="更新日" SortExpression="FLG05" ReadOnly="true" />
<asp:BoundField DataField="FLG04" HeaderText="更新メモ" SortExpression="FLG04"　ReadOnly="true" />
<asp:BoundField DataField="CUST" HeaderText="客先" SortExpression="CUST"　 />
<asp:BoundField DataField="INVOICE_NO" HeaderText="IN_NO" SortExpression="INVOICE_NO" ReadOnly="true" />
<asp:BoundField DataField="BOOKING_NO" HeaderText="BKG_NO" SortExpression="BOOKING_NO" ReadOnly="true" />
<asp:BoundField DataField="OFFICIAL_QUOT" HeaderText="TATENE" SortExpression="OFFICIAL_QUOT"  ReadOnly="true" />
<asp:BoundField DataField="CUT_DATE" HeaderText="カット日" SortExpression="CUT_DATE" ReadOnly="true" />
<asp:BoundField DataField="ETD" HeaderText="出港日" SortExpression="ETD" ReadOnly="true" />
<asp:BoundField DataField="ETA" HeaderText="到着日" SortExpression="ETA" ReadOnly="true" />
<asp:BoundField DataField="LCL_SIZE" HeaderText="M3" SortExpression="LCL_SIZE" ReadOnly="true" />
<asp:BoundField DataField="WEIGHT" HeaderText="重量" SortExpression="WEIGHT" />
<asp:BoundField DataField="QTY" HeaderText="荷量" SortExpression="QTY" />
<asp:BoundField DataField="PICKUP01" HeaderText="引取希望日" SortExpression="PICKUP01"  ReadOnly="true"/>
<asp:BoundField DataField="PICKUP02" HeaderText="" SortExpression="PICKUP02" ReadOnly="true" />
<asp:BoundField DataField="MOVEIN01" HeaderText="搬入希望日" SortExpression="MOVEIN01"  ReadOnly="true"/>
<asp:BoundField DataField="MOVEIN02" HeaderText="" SortExpression="MOVEIN02"  ReadOnly="true"/>
<asp:BoundField DataField="OTHERS01" HeaderText="完了報告希望" SortExpression="OTHERS01" ReadOnly="true" />
<asp:BoundField DataField="PICKINPLACE" HeaderText="搬入先" SortExpression="PICKINPLACE" ReadOnly="true" />
<%--<asp:BoundField DataField="FLG01" HeaderText="搬入先" SortExpression="FLG01" />--%>
<%--<asp:BoundField DataField="FLG02" HeaderText="FLG02" SortExpression="FLG02" />
<asp:BoundField DataField="FLG03" HeaderText="FLG03" SortExpression="FLG03" />--%>


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

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CONSIGNEE], [DESTINATION], [CUST], [INVOICE_NO], [BOOKING_NO], [OFFICIAL_QUOT], [CUT_DATE], [ETD], [ETA], [LCL_SIZE], [WEIGHT], [QTY], [PICKUP01], [PICKUP02], [MOVEIN01], [MOVEIN02], [OTHERS01], [FLG01], [FLG02], [FLG03], [FLG04], [FLG05],[PICKINPLACE] FROM [T_EXL_LCLTENKAI] WHERE FLG03 = '1'"
UpdateCommand="UPDATE T_EXL_LCLTENKAI SET [CUST]=@CUST, [WEIGHT]=@WEIGHT, [QTY]=@QTY  WHERE BOOKING_NO=@BOOKING_NO"
></asp:SqlDataSource>
 
    
</div>
        

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
