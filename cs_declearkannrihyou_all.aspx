﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cs_declearkannrihyou_all.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(CSマニュアル)</title>
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
    <table >
        <tr>
            <td width="400">

                <font size="5"  >

                <asp:Label ID="Label3" runat="server" Text="登録履歴_特定輸出管理表" ></asp:Label>   

      </font>
                     
            </td>
            <td width="200">
 <%--               <asp:Label ID="Label4" runat="server" Text="客先CD："></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;--%>
                <asp:Button ID="Button1" runat="server" Text="管理表に表示する" />
<%--                <asp:Button ID="Button2" runat="server" Text="キャンセル登録" />--%>
            </td>




    <td width="100">


        <section>
            <font size="2"  >
<a href="cs_declearkannrihyou.aspx?id={0}" class="simple_square_btn4">特定輸出管理表へ<span></span></a>
</section>
    
                  </font>
    </td>
                </tr>
            </table>
                    <font size="2">
<div class="wrapper" id="main2">


    <table class="sticky">
      <thead class="fixed">

      </thead>




      <tbody>






  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "1250px" DataSourceID="SqlDataSource1" DataKeyNames="BOOKING_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >


                 <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
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

          </EditItemTemplate>
        </asp:TemplateField>



            <asp:BoundField DataField="TDATE" HeaderText="追加日" SortExpression="TDATE" ReadOnly="true" >
            </asp:BoundField>
            <asp:BoundField DataField="CUT" HeaderText="1.通関日" SortExpression="CUT" ReadOnly="true" >
            </asp:BoundField>
            <asp:BoundField DataField="CUST" HeaderText="2.客先" SortExpression="CUST" ReadOnly="true" >
            </asp:BoundField>
            <asp:BoundField DataField="SUMMARY_INVO" HeaderText="3.INVOICE NO." SortExpression="SUMMARY_INVO" ReadOnly="true" >
            </asp:BoundField>
<%--            <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE_NO" SortExpression="INVOICE_NO" ReadOnly="true" >
            </asp:BoundField>--%>
            <asp:BoundField DataField="LOADING_PORT" HeaderText="4.積出港" SortExpression="LOADING_PORT" ReadOnly="true" >
            </asp:BoundField>
            <asp:BoundField DataField="DESTINATION" HeaderText="5.仕向地" SortExpression="DESTINATION" ReadOnly="true" >
            </asp:BoundField>
            <asp:BoundField DataField="KANNRINO" HeaderText="6.輸出申告番号" SortExpression="KANNRINO" >
            </asp:BoundField>
            <asp:BoundField DataField="BOOKING_NO" HeaderText="7.BKG#" SortExpression="BOOKING_NO" ReadOnly="true" >
            </asp:BoundField>
            <asp:BoundField DataField="IFLG" HeaderText="8.FLG" SortExpression="IFLG" ReadOnly="true" >
            </asp:BoundField>
<%--            <asp:BoundField DataField="IV_COUNT" HeaderText="9.IV数" SortExpression="IV_COUNT" ReadOnly="true" >
            </asp:BoundField>
            <asp:BoundField DataField="CONTAINER" HeaderText="10.コンテナ" SortExpression="CONTAINER" ReadOnly="true" >
            </asp:BoundField>
            <asp:BoundField DataField="REF01" HeaderText="REF01" SortExpression="REF01" ReadOnly="true" />
            <asp:BoundField DataField="REF02" HeaderText="REF02" SortExpression="REF02" ReadOnly="true" />--%>
            <asp:BoundField DataField="REV_KANNRINO" HeaderText="13.修正_輸出申告番号" SortExpression="REV_KANNRINO" />
<%--            <asp:BoundField DataField="SALES" HeaderText="SALES" SortExpression="SALES" ReadOnly="true" />
            <asp:BoundField DataField="CHECK01" HeaderText="CHECK01" SortExpression="CHECK01" ReadOnly="true" />--%>
        </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT TDATE,CUT ,CUST,SUMMARY_INVO,LOADING_PORT,DESTINATION,KANNRINO,BOOKING_NO,REV_KANNRINO,IFLG FROM [T_EXL_DECKANRIHYO] WHERE ([KANNRINO] <>'' or [KANNRINO] IS NOT NULL AND [IFLG] <>'0' ) or (([KANNRINO] ='' or [KANNRINO] IS NULL) AND [IFLG] <>'0')  "
        UpdateCommand="UPDATE T_EXL_DECKANRIHYO SET [KANNRINO]=@KANNRINO, [REV_KANNRINO]=@REV_KANNRINO WHERE BOOKING_NO=@BOOKING_NO"
        ></asp:SqlDataSource>
                </tbody>
    </table>
  </div>

        </font>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
