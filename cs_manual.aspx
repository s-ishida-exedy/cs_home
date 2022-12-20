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
        .header-ta {
            width: 1300px;
        }
        .first-cell {
            text-align:left;
            font-size:25px;
            width: 300px;
        }
        .second-cell {
            width: 800px;
        }   
        .third-cell {
            width: 200px;
            text-align:right;
        }
        .third-cell a {
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
        .third-cell a::after {
            content: '';
            width: 5px;
            height: 5px;
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
            transform: rotate(45deg);
        }
        .third-cell a:hover {
            color: #000000;
            text-decoration: none;
            background-color: #ffffff;
            border: 2px solid #000000;
        }
        .third-cell a:hover::after {
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
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
       　/*GridViewヘッダー固定用*/
        table{
          width: 100%;
        }
        th {
          position: sticky;
          top: 0;
          z-index: 0;
          background-color: #000084;
          color: #ffffff;
        }
        .wrapper {
          overflow: scroll;
          height: 450px;
        }
        .err{
            color:red;
            font-weight :700;
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
            if (arr[0] == 'home' || arr[0] == 'aaa' || arr[0] == 'undefined') {
                if (arr[0] == 'home') {
                    window.location.href = encodeURIComponent(arr[1]);
                    return false;
                } else {
                    return false;
                }
            } else{
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
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>ＣＳマニュアル</h2>  
            </td>
            <td class="second-cell">
                <asp:Label ID="Label12" runat="server" Text="" Class="err"></asp:Label>
                <br/>
                <asp:Label ID="Label4" runat="server" Text="客先CD：" Font-Size="Small"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width ="60"></asp:TextBox>&nbsp;
                <asp:Button ID="Button1" runat="server" Text="表示" Font-Size="Small" />&nbsp;
                <asp:Button ID="Button2" runat="server" Text="詳細表示" Width ="60px" Font-Size="Small" />&nbsp;
                <asp:Button ID="Button3" runat="server" Text="新規登録" Width ="60px" Font-Size="Small" />&nbsp;
<%--                <asp:Label ID="Label1" runat="server" Text="※新規登録時、ﾍﾞｰｽの客先CD必須" Font-Size="Small"></asp:Label>&nbsp;--%>
                <asp:Button ID="Button4" runat="server" Text="エクセル出力" Width ="80px"　Font-Size="Small" />
            </td>
            <td class="third-cell">
                <a href="./start.aspx">ホームへ戻る</a>
            </td>
        </tr>
<%--        <tr>
            <td class="first-cell">

            </td>
            <td class="second-cell">
              <asp:Label ID="Label2" runat="server" Text="客先CD②：" Font-Size="Small"></asp:Label>
              <asp:DropDownList ID="DropDownList2" runat="server" Width="70px" Font-Size="Small" CssClass="DropDown" DataTextField="CUSTCODE" DataValueField="CUSTCODE" ></asp:DropDownList>
            </td>
        </tr>--%>
    </table>


    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">

<div class="wrapper">
<table class="sticky">
<thead class="fixed">

</thead>

<tbody>

<%--<div id="main2" style="width:auto; height:500px;overflow:scroll;border:None;">--%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"  Width="870px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3">
 
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

        <asp:TemplateField ShowHeader="False">
        <ItemTemplate>
        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="edt" ImageUrl="~/icon/write.png" Text="編集" width = "20" height = "20" />
        </ItemTemplate>
        <HeaderStyle BackColor="#6B696B" />
        </asp:TemplateField>

            <asp:BoundField DataField="NEW_CODE" HeaderText="客先ｺｰﾄﾞ" SortExpression="NEW_CODE" >
            <HeaderStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField DataField="CUST_AB" HeaderText="略称" SortExpression="CUST_AB" >
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="CUST_NM" HeaderText="荷受人名" SortExpression="CUST_NM" >
            <HeaderStyle Width="500px" />
            </asp:BoundField>
            <asp:BoundField DataField="OLD_CODE" HeaderText="国名" SortExpression="OLD_CODE" >
            <HeaderStyle Width="200px" />
            </asp:BoundField>

            <asp:BoundField DataField="BL_SEND" HeaderText="BL_SEND" SortExpression="BL_SEND" />
            <asp:BoundField DataField="CUST_ADDRESS" HeaderText="CUST_ADDRESS" SortExpression="CUST_ADDRESS" />
            <asp:BoundField DataField="CONSIGNEE" HeaderText="CONSIGNEE" SortExpression="CONSIGNEE" />
            <asp:BoundField DataField="CONSIGNEE_OF_SI" HeaderText="CONSIGNEE_OF_SI" SortExpression="CONSIGNEE_OF_SI" />
            <asp:BoundField DataField="CONSIGNEE_OF_SI_ADDRESS" HeaderText="CONSIGNEE_OF_SI_ADDRESS" SortExpression="CONSIGNEE_OF_SI_ADDRESS" />
            <asp:BoundField DataField="FINAL_DES" HeaderText="FINAL_DES" SortExpression="FINAL_DES" />
            <asp:BoundField DataField="FINAL_DES_ADDRESS" HeaderText="FINAL_DES_ADDRESS" SortExpression="FINAL_DES_ADDRESS" />
            <asp:BoundField DataField="NOTIFY" HeaderText="NOTIFY" SortExpression="NOTIFY" />
            <asp:BoundField DataField="WOOD_NECE" HeaderText="WOOD_NECE" SortExpression="WOOD_NECE" />
            <asp:BoundField DataField="CO_NECE" HeaderText="CO_NECE" SortExpression="CO_NECE" />
            <asp:BoundField DataField="DOC_OF_EGYPT" HeaderText="DOC_OF_EGYPT" SortExpression="DOC_OF_EGYPT" />
            <asp:BoundField DataField="EPA_NECE" HeaderText="EPA_NECE" SortExpression="EPA_NECE" />
            <asp:BoundField DataField="FTA" HeaderText="FTA" SortExpression="FTA" />
            <asp:BoundField DataField="CERTIFICATE_OF_CONFORMITY" HeaderText="CERTIFICATE_OF_CONFORMITY" SortExpression="CERTIFICATE_OF_CONFORMITY" />
            <asp:BoundField DataField="ERL_NECE" HeaderText="ERL_NECE" SortExpression="ERL_NECE" />
            <asp:BoundField DataField="VESS_NECE" HeaderText="VESS_NECE" SortExpression="VESS_NECE" />
            <asp:BoundField DataField="DOC_NECESSITY" HeaderText="DOC_NECESSITY" SortExpression="DOC_NECESSITY" />
            <asp:BoundField DataField="CONTAINER_CLEANING" HeaderText="CONTAINER_CLEANING" SortExpression="CONTAINER_CLEANING" />
            <asp:BoundField DataField="BEARING" HeaderText="BEARING" SortExpression="BEARING" />
            <asp:BoundField DataField="IV_AUTO_CALC" HeaderText="IV_AUTO_CALC" SortExpression="IV_AUTO_CALC" />




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
  , BEARING
  , IV_AUTO_CALC               
FROM
  T_EXL_CSMANUAL 
ORDER BY NEW_CODE
"></asp:SqlDataSource>
<%--</div>--%>
<!--/#main2-->

</tbody>
</table>
</div>

    </asp:Panel>

</div>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CUSTCODE] FROM [T_EXL_CSMANUAL_ADDCUST] ORDER BY CUSTCODE ">
        </asp:SqlDataSource>

<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
