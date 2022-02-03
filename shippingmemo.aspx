<%@ Page Language="VB" AutoEventWireup="false" CodeFile="shippingmemo.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>シッピングメモ記録</title>
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


    <table style="height:10px;">


        <tr>


            <td style="width:300px;Font-Size:25px;" >

            <h2>シッピングメモ記録</h2>

            </td>


                <td style="width:100px;" >
                </td>


              <td style="width:270px;">

                    <h1>
                        <asp:Label ID="Label1" runat="server" Text="フィルタ"></asp:Label>
                    </h1>
                    <p>
                    </p>


                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" >
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>未回収</asp:ListItem>
                    <asp:ListItem>修正状況</asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList ID="DropDownList2" runat="server" Width="140px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" ></asp:DropDownList>

                    <asp:Button CssClass ="button01" ID="Button2" runat="server" Text="全件表示" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" />



                </td>


             <td class ="design02"  style="width:270px;Font-Size:15px;text-align:left;" >

            <asp:Button class="button01"  ID="Button5" runat="server" Text="期間指定" Width="70px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
            <asp:CheckBox class="button01"  ID="CheckBox1" runat="server" Height="30px" AutoPostBack="True" Font-Size="13px" />

            <asp:TextBox class="button01"  ID="TextBox1" runat="server"  type="date" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px"></asp:TextBox>
            <asp:TextBox class="button01"  ID="TextBox2" runat="server"  type="date" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px"></asp:TextBox>


             <p> &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Font-Size="8" Text="※チェックを入れている間は期間指定が有効"></asp:Label></p>
             <p> &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Font-Size="8" Text=" ※表示の際は期間指定を都度クリック"></asp:Label></p>                                
            </td>


        </tr>

    </table>


    <table style="height:10px;">
        <tr>
            <td style="width:300px;" >

                <asp:Button class="button01"  ID="Button1" runat="server" Text="更新" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
                 <asp:Button class="button01"  ID="Button3" runat="server" Text="編集メニュ" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 
                  <asp:Button class="button01"  ID="Button4" runat="server" Text="エクセル出力" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" /> 


                <p></p>




            </td>

                <td style="width:150px;" >
                </td>
       



                <td style="width:180px;" >
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
<%--                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />--%>
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


                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width = "2500px"  DataKeyNames="INVOICE_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >


                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
<%--                    <AlternatingRowStyle BackColor="#DCDCDC" />--%>
                    <Columns>


            
<%--                    <asp:TemplateField HeaderText="EDITMENU">
                    <ItemTemplate>
                    <asp:Button runat="server" CommandName="Edit" Text="編集" />
                    </ItemTemplate>

                    <EditItemTemplate>
                    <asp:Button runat="server" CommandName="Update" Text="保存" />
                    <asp:Button runat="server" CommandName="Cancel" Text="戻る" />

                    </EditItemTemplate>
                    </asp:TemplateField>--%>



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
<%--                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />--%>
                    </asp:GridView>


                    </tbody>
                    </table>
                </div>
            </asp:Panel>




    
            <asp:Panel ID="Panel3" runat="server"  Font-Size="12px">

                <div class="wrapper">
                    <table class="sticky">
                        <thead class="fixed">

                        </thead>

                    <tbody>



                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width = "2500px" DataSourceID="SqlDataSource6" DataKeyNames="INVOICE_NO" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >


                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
<%--                    <AlternatingRowStyle BackColor="#DCDCDC" />--%>
                    <Columns>


            
                    <asp:TemplateField HeaderText="編集メニュ">
                    <ItemTemplate>
                    <asp:Button runat="server" CommandName="Edit" Text="編集" />
                    </ItemTemplate>

                    <EditItemTemplate>
                                            <asp:Button runat="server" CommandName="Update" Text="保存" />
                    <asp:Button runat="server" CommandName="Cancel" Text="戻る" />

                    </EditItemTemplate>
                    </asp:TemplateField>



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
<%--                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />--%>
                    </asp:GridView>

                    </tbody>
                    </table>
                </div>

 
            </asp:Panel>





            <table style="height:10px;">
            </table>


    
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
        
            
    
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530') ORDER BY ETD "
            ></asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [DATE_GETBL] FROM [T_EXL_SHIPPINGMEMOLIST]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [REV_STATUS] FROM [T_EXL_SHIPPINGMEMOLIST] WHERE REV_STATUS <>''"></asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_SHIPPINGMEMOLIST] WHERE [DATE_GETBL] ='' AND [REV_STATUS] <>'出港済み' AND CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530' )  ORDER BY ETD  ">
            <SelectParameters>
            <asp:Parameter DefaultValue="&amp;nbsp;" Name="DATE_GETBL" Type="String" />
            </SelectParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_SHIPPINGMEMOLIST] WHERE ([REV_STATUS] = @REV_STATUS) AND CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530')  ORDER BY ETD ">
            <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList2" Name="REV_STATUS" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
            </asp:SqlDataSource>
    
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT CUSTCODE,CUSTNAME,INVOICE_NO,ETD,MEMOFLG,SIFLG,DATE_GETBL,SHIP_TYPE,DATE_ONBL,ETA,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG,REV_ETD,REV_ETA,FLG01,FLG02,FLG03,FLG04,FLG05 FROM [T_EXL_SHIPPINGMEMOLIST] WHERE CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530') ORDER BY ETD "
            UpdateCommand="UPDATE T_EXL_SHIPPINGMEMOLIST SET [DATE_GETBL]=@DATE_GETBL, [DATE_ONBL]=@DATE_ONBL, [REV_SALESDATE]=@REV_SALESDATE, [REV_STATUS]=@REV_STATUS WHERE INVOICE_NO=@INVOICE_NO"
            ></asp:SqlDataSource>    

</form>

</body>
</html>
