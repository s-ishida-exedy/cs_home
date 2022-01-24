<%@ Page Language="VB" AutoEventWireup="false" CodeFile="iv_booking_detail.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(BookingvsIVﾍｯﾀﾞ比較)</title>
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
        .table-style1{
            width: 1280px;
        }
    A.sample1:link { color: blue;}
    A.sample1:visited { color: blue;}
    A.sample1:active { color: blue;}
    A.sample1:hover { color: blue;}
    #tbl-bdr table,#tbl-bdr td,#tbl-bdr th {
    border-collapse: collapse;
    border:1px solid #333;
    height:50px;
    }
    #tbl-bdr th {
    border-collapse: collapse;
    border:1px solid #333;
    background: #fff5e5;
    }
    .auto-style8 {
        width: 1280px;
    }
    .style1 {
        width: 200px;
        font-size:large;
    }
    .style2 {
        width: 540px;
        background:#ffffff;
    }
    .style3 {
        width: 540px;
        background:#fffa00;
    }
    .style4 {
        width: 540px;
        text-align:center;
        font-size:x-large;
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
                    <asp:Label ID="Label48" runat="server" Text="ﾌﾞｯｷﾝｸﾞｼｰﾄ vs I/Vﾍｯﾀﾞ 比較明細" ></asp:Label>    
                </td>
                <td class="auto-style7">
                    
                </td>
                <td class="auto-style2">
                    <a href="#" onclick="window.history.back(); return false;">前のページに戻る</a>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Button ID="Button1" runat="server" Text="確認完了" Width ="150px" />
                </td>
                <td class="auto-style7">
                    <asp:Label ID="Label49" runat="server" Text="確認者：" Font-Size="Large"></asp:Label>
                    &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="Label46" runat="server" Text="Label" Font-Size="Large" ForeColor="Red" ></asp:Label>
                    &nbsp;<asp:Label ID="Label47" runat="server" Text="Label" Font-Size="Large"></asp:Label>
                </td>
            </tr>
        </table>
<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
        <div  id="tbl-bdr">
        <table class="table-style1">
            <tr>
                <th class="style1">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </th>
                <th class="style4">
                    ブッキングシート
                </th>
                <th class="style4">
                    インボイスヘッダー
                </th>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label4" runat="server" Text="計上日"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label7" runat="server" Text="" Font-Size ="X-Large" ></asp:Label></td>
                <td class="style12"><asp:Label ID="Label8" runat="server" Text="" Font-Size ="X-Large" ></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label2" runat="server" Text="積出港"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label9" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label10" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label5" runat="server" Text="揚地"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label6" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label11" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label12" runat="server" Text="配送先"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label13" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label14" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label15" runat="server" Text="荷受地"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label16" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label17" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label18" runat="server" Text="配送先責任送り先"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label19" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label20" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label21" runat="server" Text="カット日"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label22" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label23" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label24" runat="server" Text="到着日"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label25" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label26" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label27" runat="server" Text="入出港日"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label28" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label29" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label30" runat="server" Text="出荷方法"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label31" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label32" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label33" runat="server" Text="VOYAGENo"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label34" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label35" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label36" runat="server" Text="船社"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label37" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label38" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label39" runat="server" Text="ﾌﾞｯｷﾝｸﾞNo"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label40" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label41" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1"><asp:Label ID="Label42" runat="server" Text="船名"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label43" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
                <td class="style12"><asp:Label ID="Label44" runat="server" Text="" Font-Size ="X-Large"></asp:Label></td>
            </tr>

        </table>
        </div>
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
