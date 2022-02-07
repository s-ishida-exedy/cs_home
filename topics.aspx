<%@ Page Language="VB" AutoEventWireup="false" CodeFile="topics.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(トピックス一覧)</title>
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
                    <asp:Label ID="Label3" runat="server" Text="【トピックス一覧】" ></asp:Label>    
                </td>
                <td class="auto-style7">
                    <asp:Button ID="Button2" runat="server" Text="新規登録" style="width:164px" />
                </td>
                <td class="auto-style2">
                    <a href="#" onclick="window.history.back(); return false;">前のページに戻る</a>
                </td>
            </tr>
        </table>
<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="auto-style6" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="edt" Text="編集" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="INFO_NO" HeaderText="INFO_NO" SortExpression="INFO_NO" InsertVisible="False" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="INFO_DATE" HeaderText="投稿日" SortExpression="INFO_DATE">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="INFO_TIME" HeaderText="時間" SortExpression="INFO_TIME">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CREATE_NM" HeaderText="投稿者" SortExpression="CREATE_NM">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="INFO_HEADER" HeaderText="タイトル" SortExpression="INFO_HEADER">
                </asp:BoundField>
                <asp:BoundField DataField="INFO_DETAIL" HeaderText="内容" SortExpression="INFO_DETAIL">
                </asp:BoundField>
                <asp:BoundField DataField="INFO_KBN" HeaderText="INFO_KBN" SortExpression="INFO_KBN" >
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" 
SelectCommand="SELECT * FROM T_EXL_TOPICS  
WHERE INFO_DATE &gt; DATEADD(DAY, -30, GETDATE())
ORDER BY INFO_DATE, INFO_TIME">
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
