<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ivhd_request.aspx.vb" Inherits="cs_home"EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(ｲﾝﾎﾞｲｽﾍｯﾀﾞ作成依頼)</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<%--Datepicker用--%>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
<script src="https://code.jquery.com/jquery-1.10.2.js"  integrity="sha256-it5nQKHTz+34HijZJQkpNBIHsjpV8b6QzMJs9tmOBSo="  crossorigin="anonymous"></script>
<script src="https://code.jquery.com/ui/1.11.4/jquery-ui.js" integrity="sha256-DI6NdAhhFRnO2k51mumYeDShet3I8AKCQf/tf7ARNhI=" crossorigin="anonymous"></script>
<%--Datepicker用--%>
<style type="text/css">
        .header-ta {
            width: 1300px;
        }
        .first-cell {
            text-align:left;
            font-size:25px;
            width: 400px;
        }
        .second-cell {
            width: 500px;
        }   
        .third-cell {
            width: 100px;
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
       　/*ヘッダー固定用*/
        .sticky{
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
          height: 470px;
          width: calc(100% - 360px);
        }
        .flex{
            display: flex;
        }
        .flex div{
           
        }
        .right{
            width: 360px;
            padding: 10px
        }
        .tab1 {
            border-collapse:collapse;
            border-spacing:0px;
            border:1px solid #000000;
            width:360px;
        }
        .td1{
            border:1px solid #000000;
            width:120px;
            text-align:center;
        }
        .td2{
            border:1px solid #000000;
            width:100px;
            text-align:center;
        }
        .td3{
            border:1px solid #000000;
            width:60px;
            text-align:center;
        }
        .td4{
            border:1px solid #000000;
            width:40px;
            text-align:center;
        }
        .err{
            color:red;
            font-weight :700;
        }
        .DropDownList{
            text-align :center;
            font-size :small;
        }
        .txtb{
            padding: 5px;
            font-size :small ;
            width :100px;
        }
        .txtb2{
            padding: 5px;
            font-size :small ;
            width :60px;
            text-align :center ;
        }
        .lblA{
            width:120px;            
        }
        .lblB{
            font-size :small ;
            width:60px;
        }
        .tab2 {
            border-collapse:collapse;
            border-spacing:0px;
            width:360px;
        }
        .txtIV{
            padding: 5px;
            width :60px;
            text-align :center ;
        }
        .txtb{
            padding: 5px;
            font-size :small ;
        }
</style>
<script>
    // カレンダー
    jQuery(function ($) {
        $(".date2").datepicker({
            dateFormat: 'yy/mm/dd',
            showButtonPanel: true
        });
    });
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
<form id="form1" runat="server" autocomplete="off">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
<% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
    <!-- #Include File="header/header.aspx" -->
<% Else %>
    <!-- #Include File="header/exl_header.aspx" -->
<% End If %>
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>インボイスヘッダ作成依頼</h2>  
            </td>
            <td class="second-cell">
                <asp:Button ID="Button14" runat="server" Text=" ﾘｾｯﾄ " Font-Size="Small" Width ="80" />
                &nbsp;&nbsp;
                <asp:Label ID="Label12" runat="server" Text="" Class="err"></asp:Label>
            </td>
            <td class="third-cell">
                <a href="./start.aspx">ホームへ戻る</a>
            </td>
        </tr>
    </table>
<div class="flex">

<div class="wrapper">
<table class="sticky">
<thead class="fixed">
</thead>
<tbody>
<%--<div id="main2" style="width:100%;height:450px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="900px" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" 
        ShowHeaderWhenEmpty="True">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>

                <asp:BoundField DataField="IV_CODE" HeaderText="IV_CODE" SortExpression="IV_CODE" ReadOnly="true" InsertVisible="False" >
                </asp:BoundField>
                <asp:BoundField DataField="CUST_CD" HeaderText="客先" SortExpression="CUST_CD" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="IVNO" HeaderText="IVNO" SortExpression="IVNO" > 
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="IV_RATE" HeaderText="ﾚｰﾄ" SortExpression="IV_RATE" >
                <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="REQUESTER" HeaderText="依頼者" SortExpression="REQUESTER" >
                <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="REQ_DATE" HeaderText="依頼日" SortExpression="REQ_DATE" >
                <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS" HeaderText="ｽﾃｰﾀｽ" SortExpression="STATUS" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="sup" Text="対応" />
                    </ItemTemplate>
                    <ItemStyle Font-Size="Small" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CausesValidation="false" CommandName="upd" Text="更新" />
                    </ItemTemplate>
                    <ItemStyle Font-Size="Small" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button4" runat="server" CausesValidation="false" CommandName="fin" Text="完了" />
                    </ItemTemplate>
                    <ItemStyle Font-Size="Small" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="del" Text="削除" OnClientClick ="return confirm('本当に削除しても良いですか？');" />
                    </ItemTemplate>
                    <ItemStyle Font-Size="Small" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
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

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="
SELECT 
  MAX(IV_CODE) AS IV_CODE
  , CUST_CD
  , IVNO
  , IV_RATE
  , B.unam AS REQUESTER
  , MAX(REQ_DATE) AS REQ_DATE
  , CASE STATUS 
      WHEN '0' THEN '依頼中'
      WHEN '1' THEN '回答済'
      WHEN '2' THEN '差戻し'
    END AS STATUS
FROM
  T_EXL_IVHD_REQ A
  INNER JOIN dbo.M_EXL_USR B
  ON A.REQUESTER = B.uid
WHERE
  STATUS IN ('0','1','2')
GROUP BY 
    CUST_CD
  , IVNO
  , IV_RATE
  , B.unam
  , STATUS
"></asp:SqlDataSource>
<%--</div>--%>

<!--/#main2-->

</tbody>
</table>
<div class="right">
    <table class="tab2">
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="元のIVNO：" CssClass ="lblA"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMoto" runat="server" Class ="txtIV" placeholder="入力" Width ="60px"></asp:TextBox>
                <asp:Label ID="lblMoto" runat="server" Text="" CssClass ="lblA" Width ="60px"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnRate" runat="server" Text="ﾚｰﾄ表示" width="60px" Font-Size="Small" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="元のレート：" CssClass ="lblA"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="" CssClass ="lblA   "></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnIns" runat="server" Text="登 録" width="60px" Font-Size="Small" />
            </td>
            <td>
                <asp:Button ID="btnBack" runat="server" Text="差 戻" width="60px" Font-Size="Small" />
            </td>
        </tr>    
    </table>
    
    <table class="tab1">
        <tr>
            <th>SNNO</th>
            <th colspan ="2">ﾚｰﾄ</th>
            <th>IVNO</th>
        </tr>
        <tr>
            <td class ="td1">
                <asp:TextBox ID="txtSN01" runat="server"  Class ="txtb" placeholder="入力してください" Width="100px"></asp:TextBox>
                <asp:Label ID="lblSN01" runat="server" Text="" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td4">
                <asp:Button ID="btnHyouji01" runat="server" Text="表示" Font-Size ="Smaller" Width ="30px" />
            </td>
            <td class ="td2">
                <asp:Label ID="lblRate01" runat="server" Text="-----" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td3">       
                <asp:TextBox ID="txtIV01" runat="server"  Class ="txtb2" Width="40px"></asp:TextBox>         
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:TextBox ID="txtSN02" runat="server"  Class ="txtb" placeholder="入力してください" Width="100px"></asp:TextBox>
                <asp:Label ID="lblSN02" runat="server" Text="" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td4">
                <asp:Button ID="btnHyouji02" runat="server" Text="表示" Font-Size ="Smaller" Width ="30px" />
            </td>
            <td class ="td2">
                <asp:Label ID="lblRate02" runat="server" Text="-----" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td3">       
                <asp:TextBox ID="txtIV02" runat="server"  Class ="txtb2" Width="40px"></asp:TextBox>         
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:TextBox ID="txtSN03" runat="server"  Class ="txtb" placeholder="入力してください" Width="100px"></asp:TextBox>
                <asp:Label ID="lblSN03" runat="server" Text="" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td4">
                <asp:Button ID="btnHyouji03" runat="server" Text="表示" Font-Size ="Smaller" Width ="30px" />
            </td>
            <td class ="td2">
                <asp:Label ID="lblRate03" runat="server" Text="-----" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td3">       
                <asp:TextBox ID="txtIV03" runat="server"  Class ="txtb2" Width="40px"></asp:TextBox>         
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:TextBox ID="txtSN04" runat="server"  Class ="txtb" placeholder="入力してください" Width="100px"></asp:TextBox>
                <asp:Label ID="lblSN04" runat="server" Text="" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td4">
                <asp:Button ID="btnHyouji04" runat="server" Text="表示" Font-Size ="Smaller" Width ="30px" />
            </td>
            <td class ="td2">
                <asp:Label ID="lblRate04" runat="server" Text="-----" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td3">       
                <asp:TextBox ID="txtIV04" runat="server"  Class ="txtb2" Width="40px"></asp:TextBox>         
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:TextBox ID="txtSN05" runat="server"  Class ="txtb" placeholder="入力してください" Width="100px"></asp:TextBox>
                <asp:Label ID="lblSN05" runat="server" Text="" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td4">
                <asp:Button ID="btnHyouji05" runat="server" Text="表示" Font-Size ="Smaller" Width ="30px" />
            </td>
            <td class ="td2">
                <asp:Label ID="lblRate05" runat="server" Text="-----" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td3">       
                <asp:TextBox ID="txtIV05" runat="server"  Class ="txtb2" Width="40px"></asp:TextBox>         
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:TextBox ID="txtSN06" runat="server"  Class ="txtb" placeholder="入力してください" Width="100px"></asp:TextBox>
                <asp:Label ID="lblSN06" runat="server" Text="" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td4">
                <asp:Button ID="btnHyouji06" runat="server" Text="表示" Font-Size ="Smaller" Width ="30px" />
            </td>
            <td class ="td2">
                <asp:Label ID="lblRate06" runat="server" Text="-----" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td3">       
                <asp:TextBox ID="txtIV06" runat="server"  Class ="txtb2" Width="40px"></asp:TextBox>         
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:TextBox ID="txtSN07" runat="server"  Class ="txtb" placeholder="入力してください" Width="100px"></asp:TextBox>
                <asp:Label ID="lblSN07" runat="server" Text="" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td4">
                <asp:Button ID="btnHyouji07" runat="server" Text="表示" Font-Size ="Smaller" Width ="30px" />
            </td>
            <td class ="td2">
                <asp:Label ID="lblRate07" runat="server" Text="-----" CssClass ="lblB"></asp:Label>
            </td>
            <td class ="td3">       
                <asp:TextBox ID="txtIV07" runat="server"  Class ="txtb2" Width="40px"></asp:TextBox>         
            </td>
        </tr>
    </table>

    <table class="tab2">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="メッセージ：" CssClass ="lblA"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Width ="360" Height ="70" CssClass ="txtb"></asp:TextBox>
            </td>
        </tr>
    </table>

</div>

</div>

</div>
<!--/#FLEX-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a>
</p>

</form>

</body>
</html>
