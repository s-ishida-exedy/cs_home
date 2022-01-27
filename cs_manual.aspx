<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cs_manual.aspx.vb" Inherits="cs_home" %>

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
        .auto-style1 {
            width: 1280px;
        }
        .auto-style2 {
            width: 200px;
            text-align:right;
        }
        .auto-style4 {
            text-align:left;
            font-size:larger;
            font-weight : 700;
            width: 200px;
        }
        .auto-style7 {
            width: 880px;
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
                <asp:Label ID="Label3" runat="server" Text="【CSマニュアル】" ></asp:Label>    
            </td>
            <td class="auto-style7">
                <asp:Label ID="Label4" runat="server" Text="客先CD："></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;
                <asp:Button ID="Button1" runat="server" Text="表示" />&nbsp;
                <asp:Button ID="Button2" runat="server" Text="詳細表示" />&nbsp;
                <asp:Button ID="Button3" runat="server" Text="新規登録" />&nbsp;
                <asp:Label ID="Label1" runat="server" Text="※新規登録時はﾍﾞｰｽの客先CDを入力してください。"></asp:Label>
            </td>
            <td class="auto-style2">
                <a href="#" onclick="window.history.back(); return false;">前のページに戻る</a>
            </td>
        </tr>
    </table>
<div id="main2" style="width:auto; height:500px;overflow:scroll;border:solid 1px;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"  Width="6500px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="NEW_CODE" HeaderText="新&lt;BR&gt;ｺｰﾄﾞ" SortExpression="NEW_CODE" HtmlEncode="False" >
            <HeaderStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField DataField="OLD_CODE" HeaderText="旧&lt;BR&gt;ｺｰﾄﾞ" SortExpression="OLD_CODE" HtmlEncode="False" >
            <HeaderStyle Width="250px" />
            </asp:BoundField>
            <asp:BoundField DataField="CUST_NM" HeaderText="客先名" SortExpression="CUST_NM" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="CUST_AB" HeaderText="略称" SortExpression="CUST_AB" >
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="INCOTEM" HeaderText="建値" SortExpression="INCOTEM" >
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="BL_TYPE" HeaderText="BL種類" SortExpression="BL_TYPE" >
            <HeaderStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="BL_SEND" HeaderText="BL&lt;BR&gt;送付方法" SortExpression="BL_SEND" HtmlEncode="False" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="CUST_ADDRESS" HeaderText="客先住所" SortExpression="CUST_ADDRESS" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="CONSIGNEE" HeaderText="CONSIGNEE" SortExpression="CONSIGNEE" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="CNEE_NM_SI" HeaderText="CNEE Name of SI" SortExpression="CNEE_NM_SI" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="FIN_DESTINATION" HeaderText="Final DESTINATION" SortExpression="FIN_DESTINATION" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="NOTIFY" HeaderText="NOTIFY" SortExpression="NOTIFY" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="IV_NECE" HeaderText="IV" SortExpression="IV_NECE" />
            <asp:BoundField DataField="PL_NECE" HeaderText="PL" SortExpression="PL_NECE" />
            <asp:BoundField DataField="BL_NECE" HeaderText="BL" SortExpression="BL_NECE" />
            <asp:BoundField DataField="CO_NECE" HeaderText="CO" SortExpression="CO_NECE" />
            <asp:BoundField DataField="EPA_NECE" HeaderText="EPA" SortExpression="EPA_NECE" />
            <asp:BoundField DataField="WOOD_NECE" HeaderText="木材" SortExpression="WOOD_NECE" />
            <asp:BoundField DataField="DELI_NECE" HeaderText="ﾃﾞﾘﾊﾞﾘ" SortExpression="DELI_NECE" />
            <asp:BoundField DataField="INSP_NECE" HeaderText="検査" SortExpression="INSP_NECE" />
            <asp:BoundField DataField="ERL_NECE" HeaderText="ERL" SortExpression="ERL_NECE" />
            <asp:BoundField DataField="VESS_NECE" HeaderText="ﾍﾞｯｾﾙ" SortExpression="VESS_NECE" />
            <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" >
            <HeaderStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="SHIPMENT_KBN" HeaderText="出荷区分" SortExpression="SHIPMENT_KBN" >
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="LT" HeaderText="LT" SortExpression="LT" />
            <asp:BoundField DataField="LC" HeaderText="LC" SortExpression="LC" />
            <asp:BoundField DataField="CONSIGNEE_OF_SI" HeaderText="CNEE OF SI" SortExpression="CONSIGNEE_OF_SI" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="CONSIGNEE_OF_SI_ADDRESS" HeaderText="CNEE OF SI Address" SortExpression="CONSIGNEE_OF_SI_ADDRESS" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="FINAL_DES" HeaderText="FINAL DES" SortExpression="FINAL_DES" >
            <HeaderStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="FINAL_DES_ADDRESS" HeaderText="FINAL DES ADDRESS" SortExpression="FINAL_DES_ADDRESS" >
            <HeaderStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="FORWARDER_NM" HeaderText="海貨業者" SortExpression="FORWARDER_NM" >
            <HeaderStyle Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="FORWARDER_STAFF_NM" HeaderText="担当" SortExpression="FORWARDER_STAFF_NM" >
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="DOC_NECESSITY" HeaderText="IV/PL送付" SortExpression="DOC_NECESSITY" />
            <asp:BoundField DataField="FTA" HeaderText="FTA" SortExpression="FTA" />
            <asp:BoundField DataField="CERTIFICATE_OF_CONFORMITY" HeaderText="適合証明" SortExpression="CERTIFICATE_OF_CONFORMITY" />
            <asp:BoundField DataField="DOC_OF_EGYPT" HeaderText="ｴｼﾞﾌﾟﾄ" SortExpression="DOC_OF_EGYPT" />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT
  NEW_CODE                                        
  , OLD_CODE                                      
  , CUST_NM                                       
  , CUST_AB                                       
  , INCOTEM                                       
  , BL_TYPE                                       
  , BL_SEND                                       
  , CUST_ADDRESS                                  
  , CONSIGNEE                                     
  , CNEE_NM_SI                                    
  , FIN_DESTINATION                               
  , NOTIFY                                        
  , FORWARDER_INFO                                
  , CUST_REQ                                      
  , IV_NECE                                       
  , PL_NECE                                       
  , BL_NECE                                       
  , CO_NECE                                       
  , EPA_NECE                                      
  , WOOD_NECE                                     
  , DELI_NECE                                     
  , INSP_NECE                                     
  , ERL_NECE                                      
  , VESS_NECE                                     
  , SHIP_DAY_OF_WEEK                              
  , DESTINATION                                   
  , SHIPMENT_KBN                                  
  , LT                                            
  , CONTAINER_CLEANING                            
  , LC                                            
  , CONSIGNEE_OF_SI                               
  , CONSIGNEE_OF_SI_ADDRESS                       
  , FINAL_DES                                     
  , FINAL_DES_ADDRESS                             
  , FORWARDER_NM                                  
  , FORWARDER_STAFF_NM                            
  , DOC_NECESSITY                                 
  , FTA                                           
  , CERTIFICATE_OF_CONFORMITY                     
  , DOC_OF_EGYPT                                  
FROM
  T_EXL_CSMANUAL 
ORDER BY NEW_CODE
"></asp:SqlDataSource>
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
