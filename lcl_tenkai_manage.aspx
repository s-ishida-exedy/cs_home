<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lcl_tenkai_manage.aspx.vb" Inherits="cs_home" %>

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
          background-color: #FFFFFF;
          color: #000000;
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

*,
*:before,
*:after {
  -webkit-box-sizing: inherit;
  box-sizing: inherit;
}

html {
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  font-size: 62.5%;
}


a.btn--yellow.btn--border-dotted {
  border: 3px dotted #000;
}



section {
  max-width: 140px;
  margin: 0 auto;
}
a.btn_07 {
  display: flex;
  justify-content: center;
  align-items: center;
  background: #fff;
  border: 1px solid #000;
  box-sizing: border-box;
  width: 100%;
  height: 40px;
  padding: 0 25px;
  color: #000;
  font-size: 16px;
  text-align: left;
  text-decoration: none;
  position: relative;
  transition-duration: 0.2s;
}
a.btn_07:hover {
  background: #000;
  border: 1px solid #000;
  color: #fff;
}
a.btn_07:before {
  content: "";
  position: absolute;
  right: 0;
  bottom: 0;
  width: 0;
  height: 0;
  border-style: solid;
  border-width: 0 0 40px 50px;
  border-color: transparent transparent #000 transparent;
}
a.btn_07 span {
    position: absolute;
    bottom: 12px;
    right: 20px;
    display: inline-block;
}
a.btn_07 span:before {
    content: '';
    width: 16px;
    height: 16px;
    background: #ffffff;
    border-radius: 50%;
    position: absolute;
    top: 50%;
    left: 0;
    margin-top: -8px;
}
a.btn_07 span:after {
    content: '';
    width: 6px;
    height: 6px;
    border: 0;
    border-top: 2px solid #000;
    border-right: 2px solid #000;
    transform: rotate(45deg);
    position: absolute;
    top: 50%;
    left: 4px;
    margin-top: -3px;
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
<div class="auto-style8">
<table class="auto-style1" >
<tr>

<td width="300" border="1" >

<font size="6"  >
<%--        <h2><span>01</span>出荷案件管理</h2>--%>

<b>LCL出荷-編集用</b>

</font>
</td>




<td>

<%--                    <font size="2" style="background-color:DarkGray" Color="White" ><b>・グレー:書類作成済み（書類を作成し海貨業者へ送付済み）</b><br></font>
<font size="2" style="background-color:LightBlue"   ><b>・ブルー:手配依頼済み（引取り手配などが済んでいる）</b><br></font>
<font size="2" style="background-color:Salmon"  ><b>・レッド:これから手配が必要（当日～1週先の金曜日）</b></font><font size="2"> </font><br>--%>

</td>

<td>

<%--                    <font size="2" style="background-color:red" Color="White" ><b>・AC要:BOOKING未確定のため海貨御者に連絡する必要あり<br><br><br></b></font>--%>

</td>

    <td width="100" border="1" >

<asp:Button class="btn-radius-gradient-wrap"  ID="Button1" runat="server" Text="表示" Width="75px" Height="40px" AutoPostBack="True" Font-Size="13px" />

</td>

    <td width="100" border="1" >

<asp:Button class="btn-radius-gradient-wrap"  ID="Button2" runat="server" Text="非表示" Width="75px" Height="40px" AutoPostBack="True" Font-Size="13px" />

</td>


<td width="200">

<font size="4"  >



<%--<section>
<a href="lcl_arange.aspx?id={0}" class="btn_07">手配状況<span></span></a>
</section>--%>



</td>

</tr>

</table>



<font size="2">


<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>




<tbody>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "3000px" DataSourceID="SqlDataSource1" DataKeyNames="BOOKING_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >



<HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>


<Columns>

                                                            <asp:TemplateField>
                    <ItemTemplate>

                        <asp:CheckBox ID="cb" Checked="false" runat="server"  />

                    </ItemTemplate>
                </asp:TemplateField>


            <asp:TemplateField HeaderText="EDITMENU">
          <ItemTemplate>
            <asp:Button runat="server" CommandName="Edit" Text="編集" />
          </ItemTemplate>

                          <EditItemTemplate>
                                          <asp:Button runat="server" CommandName="Update" Text="保存" />
              <asp:Button runat="server" CommandName="Cancel" Text="戻る" />
            <asp:Button runat="server" CommandName="Delete" Text="削除" />

          </EditItemTemplate>
        </asp:TemplateField>


<%--<asp:BoundField DataField="CONSIGNEE" HeaderText="CONSIGNEE" SortExpression="CONSIGNEE" />
<asp:BoundField DataField="DESTINATION" HeaderText="DESTINATION" SortExpression="DESTINATION" />--%>
<asp:BoundField DataField="FLG05" HeaderText="更新日" SortExpression="FLG05" ReadOnly ="true"　 />
<asp:BoundField DataField="CUST" HeaderText="客先" SortExpression="CUST"　 />
<asp:BoundField DataField="INVOICE_NO" HeaderText="IN_NO" SortExpression="INVOICE_NO" />
<asp:BoundField DataField="BOOKING_NO" HeaderText="BKG_NO" SortExpression="BOOKING_NO" ReadOnly ="true" />
<asp:BoundField DataField="OFFICIAL_QUOT" HeaderText="TATENE" SortExpression="OFFICIAL_QUOT"/>
<asp:BoundField DataField="CUT_DATE" HeaderText="カット日" SortExpression="CUT_DATE" />
<asp:BoundField DataField="ETD" HeaderText="出港日" SortExpression="ETD" />
<asp:BoundField DataField="ETA" HeaderText="到着日" SortExpression="ETA" />
<asp:BoundField DataField="LCL_SIZE" HeaderText="M3" SortExpression="LCL_SIZE" />
<asp:BoundField DataField="WEIGHT" HeaderText="重量" SortExpression="WEIGHT" />
<asp:BoundField DataField="QTY" HeaderText="荷量" SortExpression="QTY" />
<asp:BoundField DataField="PICKUP01" HeaderText="引取希望日" SortExpression="PICKUP01" />
<asp:BoundField DataField="PICKUP02" HeaderText="" SortExpression="PICKUP02" />
<asp:BoundField DataField="MOVEIN01" HeaderText="搬入希望日" SortExpression="MOVEIN01" />
<asp:BoundField DataField="MOVEIN02" HeaderText="" SortExpression="MOVEIN02" />
<asp:BoundField DataField="OTHERS01" HeaderText="備考" SortExpression="OTHERS01" />
<asp:BoundField DataField="PICKINPLACE" HeaderText="搬入先" SortExpression="PICKINPLACE" />
<%--<asp:BoundField DataField="FLG01" HeaderText="搬入先" SortExpression="FLG01" />--%>
<%--<asp:BoundField DataField="FLG02" HeaderText="FLG02" SortExpression="FLG02" />
<asp:BoundField DataField="FLG03" HeaderText="FLG03" SortExpression="FLG03" />
<asp:BoundField DataField="FLG04" HeaderText="FLG04" SortExpression="FLG04" />
<asp:BoundField DataField="FLG05" HeaderText="FLG05" SortExpression="FLG05" />--%>





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

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CONSIGNEE], [DESTINATION], [CUST], [INVOICE_NO], [BOOKING_NO], [OFFICIAL_QUOT], [CUT_DATE], [ETD], [ETA], [LCL_SIZE], [WEIGHT], [QTY], [PICKUP01], [PICKUP02], [MOVEIN01], [MOVEIN02], [OTHERS01], [FLG01], [FLG02], [FLG03], [FLG04], [FLG05],[PICKINPLACE] FROM [T_EXL_LCLTENKAI]"
    UpdateCommand="UPDATE T_EXL_LCLTENKAI SET [CUST]=@CUST, [INVOICE_NO]=@INVOICE_NO,[OFFICIAL_QUOT]=@OFFICIAL_QUOT, [CUT_DATE]=@CUT_DATE, [ETD]=@ETD, [ETA]=@WEIGHT, [LCL_SIZE]=@LCL_SIZE, [WEIGHT]=@WEIGHT, [QTY]=@QTY, [PICKUP01]=@PICKUP01, [PICKUP02]=@PICKUP02, [MOVEIN01]=@MOVEIN01, [MOVEIN02]=@MOVEIN02, [OTHERS01]=@OTHERS01, [PICKINPLACE]=@PICKINPLACE, [FLG05]=format(GETDATE(),'yyyy/MM/dd') WHERE BOOKING_NO=@BOOKING_NO"
    DeleteCommand="DELETE FROM T_EXL_LCLTENKAI WHERE BOOKING_NO=@BOOKING_NO"></asp:SqlDataSource>
    
    
</div>
        

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
