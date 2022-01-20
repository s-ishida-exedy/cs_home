<%@ Page Language="VB" AutoEventWireup="false" CodeFile="epa_request.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(BOOKING SHEET)</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<style type="text/css">
        .auto-style1 {
            width: 1280px;
        }
        .auto-style2 {
            width: 400px;
            text-align:right;
        }
        .auto-style4 {
            text-align:left;
            font-size:larger;
            font-weight : 700;
            width: 400px;
        }
        .auto-style7 {
            width: 480px;
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
        <table class="auto-style1" >
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label3" runat="server" Text="【EPA発給申請管理】" ></asp:Label>    
                </td>
                <td class="auto-style7">
                    
                </td>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Labe2" ></asp:Label>
                </td>
            </tr>
        </table>
<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="2100px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" DataKeyNames="BLDATE,INV">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:Button ID="Button2" runat="server" CommandName="Update" Text="更新" />
                        <asp:Button ID="Button3" runat="server" CommandName="Cancel" Text="ｷｬﾝｾﾙ" />
                        <asp:Button ID="Button4" runat="server" CommandName="Delete" Text="削除" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="Edit" Text="編集" />
                    </ItemTemplate>
                    <HeaderStyle Width="200px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />
                <asp:TemplateField HeaderText="状態" SortExpression="STATUS">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="XmlDataSource1" DataTextField="text" DataValueField="value" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Code/status.xml"></asp:XmlDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="BLDATE" HeaderText="ETD" SortExpression="BLDATE"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="INV" HeaderText="IV" SortExpression="INV"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTNAME" HeaderText="客先" SortExpression="CUSTNAME"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTCODE" HeaderText="ｺｰﾄﾞ" SortExpression="CUSTCODE"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SALESFLG" HeaderText="売上" SortExpression="SALESFLG"  ReadOnly="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIPPEDPER" HeaderText="船名" SortExpression="SHIPPEDPER" >
                </asp:BoundField>
                <asp:TemplateField HeaderText="ETA" SortExpression="ETA">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Width="100px" Height="20px" Text='<%# Bind("ETA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ETA") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ｶｯﾄ日" SortExpression="CUTDATE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Width="100px" Height="20px" Text='<%# Bind("CUTDATE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("CUTDATE") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VoyNO" SortExpression="VOYAGENO">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Width="80px" Height="20px" Text='<%# Bind("VOYAGENO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("VOYAGENO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="受付番号" SortExpression="RECEIPT_NUMBER">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Width="110px" Height="20px" Text='<%# Bind("RECEIPT_NUMBER") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("RECEIPT_NUMBER") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IVNO_FULL" HeaderText="IVNO" SortExpression="IVNO_FULL" >
                </asp:BoundField>
                <asp:TemplateField HeaderText="申請日" SortExpression="APPLICATION_DATE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Width="100px" Height="20px" Text='<%# Bind("APPLICATION_DATE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("APPLICATION_DATE") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="送付依頼日" SortExpression="SENDING_REQ_DATE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Width="100px" Height="20px" Text='<%# Bind("SENDING_REQ_DATE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("SENDING_REQ_DATE") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="受領日" SortExpression="RECEIPT_DATE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Width="100px" Height="20px" Text='<%# Bind("RECEIPT_DATE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("RECEIPT_DATE") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EPA送付日" SortExpression="EPA_SEND_DATE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Width="100px" Height="20px" Text='<%# Bind("EPA_SEND_DATE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("EPA_SEND_DATE") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TRK#" SortExpression="TRK_NO">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Width="140px" Height="20px" Text='<%# Bind("TRK_NO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("TRK_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" 
SelectCommand="SELECT
  CASE STATUS
  	WHEN '01' THEN '未'
  	WHEN '02' THEN '済'
  	WHEN '03' THEN '対象ﾅｼ'
  	WHEN '04' THEN 'ｷｬﾝｾﾙ'
  	WHEN '09' THEN '再発給'
  END AS STATUS
  , BLDATE 
  , INV 
  , CUSTNAME 
  , CUSTCODE 
  , CASE SALESFLG
  		WHEN '1' THEN '済'
	END AS SALESFLG
  , SHIPPEDPER 
  , ETA 
  , CUTDATE 
  , VOYAGENO 
  , RECEIPT_NUMBER 
  , IVNO_FULL 
  , APPLICATION_DATE 
  , SENDING_REQ_DATE 
  , RECEIPT_DATE 
  , EPA_SEND_DATE 
  , TRK_NO 
FROM T_EXL_EPA_KANRI
ORDER BY BLDATE, INV" 
UpdateCommand="UPDATE T_EXL_EPA_KANRI 
SET
  STATUS = @STATUS
  , SHIPPEDPER = @SHIPPEDPER
  , ETA = @ETA
  , CUTDATE = @CUTDATE
  , VOYAGENO = @VOYAGENO
  , RECEIPT_NUMBER = @RECEIPT_NUMBER
  , IVNO_FULL = @IVNO_FULL
  , APPLICATION_DATE = @APPLICATION_DATE
  , SENDING_REQ_DATE = @SENDING_REQ_DATE
  , RECEIPT_DATE = @RECEIPT_DATE
  , EPA_SEND_DATE = @EPA_SEND_DATE
  , TRK_NO = @TRK_NO
WHERE STATUS = @STATUS AND BLDATE = @BLDATE">
            <UpdateParameters>
                <asp:Parameter Name="STATUS" />
                <asp:Parameter Name="SHIPPEDPER" />
                <asp:Parameter Name="ETA" />
                <asp:Parameter Name="CUTDATE" />
                <asp:Parameter Name="VOYAGENO" />
                <asp:Parameter Name="RECEIPT_NUMBER" />
                <asp:Parameter Name="IVNO_FULL" />
                <asp:Parameter Name="APPLICATION_DATE" />
                <asp:Parameter Name="SENDING_REQ_DATE" />
                <asp:Parameter Name="RECEIPT_DATE" />
                <asp:Parameter Name="EPA_SEND_DATE" />
                <asp:Parameter Name="TRK_NO" />
                <asp:Parameter Name="BLDATE" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
        <br />
    
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
