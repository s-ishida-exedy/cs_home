<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eir_comfirm_cs_detail.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(EIR、Booking情報差異連絡)</title>
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
            width: 600px;
        }
        .second-cell {
            width: 500px;
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
        .txtb{
            width:300px;
            padding: 5px;
            font-size :small ;
        }
        .cmb{
            width:315px;
            padding: 5px;
            font-size :small ;
        }
        .ta3{
            width:800px;
            margin:0px 0px 10px 10px;
        }
        .ta3 th{
            background: #e6e6fa;
            text-align :left;
        }
        .headTD0{
            background: #ffffff;
            width:150px;
        }
        .headTD1{
            background: #e6e6fa;
            font-weight :bold ;
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
<form id="form1" runat="server" autocomplete="off">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>EIR、Booking情報差異連絡 確認結果登録</h2>  
            </td>
            <td class="second-cell">
                &nbsp;<asp:Button ID="Button7" runat="server" Text="更　新" style="width:120px" Font-Size="Small" />&nbsp;
                <asp:Button ID="Button8" runat="server" Text="削　除" style="width:120px" Font-Size="Small" />&nbsp;
                <asp:Label ID="Label3" runat="server" Text="Label" Class="err"></asp:Label>
            </td>
            <td class="third-cell">
                <a href="./eir_comfirm_cs.aspx">一覧に戻る</a>
            </td>
        </tr>
    </table>
<div id="main2" style="width:100%; height:450px;border:None;">
        <table class="ta3">
            <tr>
                <td class="headTD0">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label> 
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <td class="headTD0">
                </td>
                <td class="headTD1">
                    <asp:Label ID="Label9" runat="server" Text="確定情報"></asp:Label>                  
                </td>
                <td class="headTD1">
                    <asp:Label ID="Label8" runat="server" Text="搬入票"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="Label10" runat="server" Text="VoyNo"></asp:Label>
                </th>
                <td>
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="Label11" runat="server" Text="船名"></asp:Label>
                </th>
                <td>
                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="Label13" runat="server" Text="BOOKING NO"></asp:Label>
                </th>
                <td>
                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="Label14" runat="server" Text="コンテナサイズ"></asp:Label>
                </th>
                <td>
                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="Label15" runat="server" Text="その他"></asp:Label>
                </th>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
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
