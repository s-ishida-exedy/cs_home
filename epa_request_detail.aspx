﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="epa_request_detail.aspx.vb" Inherits="cs_home" %>

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
            width: 700px;
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
        .ta3 th{
            background: #e6e6fa;
        }
        .err{
            color:red;
            font-weight :700;
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
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>EPA発給申請管理明細</h2>  
            </td>
            <td class="second-cell">
                <asp:Button ID="Button7" runat="server" Text="更　新" style="width:120px" Font-Size="Small" />&nbsp;
                <asp:Button ID="Button8" runat="server" Text="削　除" style="width:120px" Font-Size="Small" />&nbsp;
                <asp:Label ID="Label3" runat="server" Text="Label" Class="err"></asp:Label>
            </td>
            <td class="third-cell">
                <a href="./epa_request.aspx">一覧に戻る</a>
            </td>
        </tr>
    </table>
<div id="main2" style="width:100%; height:500px;border:None;">
        <table class="ta3">
            <tr>
                <th>ステータス</th>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" style="width:195px">
                        <asp:ListItem Value="01">未</asp:ListItem>
                        <asp:ListItem Value="02">済</asp:ListItem>
                        <asp:ListItem Value="03">対象ﾅｼ</asp:ListItem>
                        <asp:ListItem Value="04">ｷｬﾝｾﾙ</asp:ListItem>
                        <asp:ListItem Value="08">再発給</asp:ListItem>
                        <asp:ListItem Value="09">再発済</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>ETD</th>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label" style="width:164px"></asp:Label>
                </td>
                <th>IV</th>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Label" style="width:164px"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>客先</th>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Label" style="width:164px"></asp:Label>
                </td>
                <th>客先ｺｰﾄﾞ</th>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Label" style="width:164px"></asp:Label>
                </td>
                <th>売上確定</th>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Label" style="width:164px"></asp:Label>
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
