<%@ Page Language="VB" AutoEventWireup="false" CodeFile="epa_request_detail_ins.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(EPA申請状況)</title>
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
<script src="//code.jquery.com/jquery-1.10.2.js"></script>
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
<%--Datepicker用--%>
<style type="text/css">
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
        .ta3 th{
            background: #e6e6fa;
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
<!-- メニューの編集はheader.htmlで行う -->
 <!-- #Include File="header.html" -->
       
<div id="contents2" class="inner2">
        <table>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label3" runat="server" Text="【EPA発給申請管理明細】" ></asp:Label>    
                </td>
                <td class="auto-style7">
                    <asp:Button ID="Button7" runat="server" Text="登　　録" style="width:164px" />
                </td>
                <td class="auto-style2">
                    <a href="#" onclick="window.history.back(); return false;">前のページに戻る</a>
                </td>
            </tr>
        </table>
<div id="main2" style="width:100%;height:500px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
        <table class="ta3">
            <tr>
                <th>ステータス</th>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="未" style="width:164px"></asp:Label>
                </td>
                <th>ETD</th>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server" style="width:195px" class="date2"></asp:TextBox>
                </td>
                <th>IV</th>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server" style="width:195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>客先</th>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server" style="width:195px"></asp:TextBox>
                </td>
                <th>客先ｺｰﾄﾞ</th>
                <td>
                    <asp:TextBox ID="TextBox15" runat="server" style="width:195px"></asp:TextBox>
                </td>
                <th>売上確定</th>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=" " style="width:164px"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>船名</th>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" style="width:195px"></asp:TextBox>
                </td>
                <th>ETA</th>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" style="width:195px" class="date2"></asp:TextBox>
                </td>
                <th>ｶｯﾄ日</th>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" style="width:195px" class="date2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>VoyNO</th>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" style="width:195px"></asp:TextBox>
                </td>
                <th>受付番号</th>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" style="width:195px"></asp:TextBox>
                </td>
                <th>IVNO(Full)</th>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" style="width:195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>申請日</th>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" style="width:195px" class="date2"></asp:TextBox>
                </td>
                <th>送付依頼日</th>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server" style="width:195px" class="date2"></asp:TextBox>
                </td>
                <th>受領日</th>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" style="width:195px" class="date2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>EPA送付日</th>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server" style="width:195px" class="date2"></asp:TextBox>
                </td>
                <th>TRK#</th>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server" style="width:195px"></asp:TextBox>
                </td>
                <th></th>
                <td>
               </td>
            </tr>
        </table>
</div>
<!--/#main2-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
