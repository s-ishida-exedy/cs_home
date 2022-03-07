<%@ Page Language="VB" AutoEventWireup="false" CodeFile="air_mng_detail.aspx.vb" Inherits="cs_home" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(AIR管理表詳細)</title>
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
                <h2>AIR管理表詳細</h2> 
                </td>
            <td class="second-cell">
                    <asp:Button ID="Button1" runat="server" Text="更　新" style="width:164px" Font-Size="Small" />&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="削　除" style="width:164px" Font-Size="Small" />&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="" Class="err"></asp:Label>
                </td>
            <td class="third-cell">
                <a href ="./air_management.aspx">一覧へ戻る</a>
            </td>
        </tr>
    </table>
<div id="main2" style="width:100%;height:480px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
        <table class="ta3">
            <tr>
                <th>依頼日</th>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="233px" class="date2"></asp:TextBox>
                </td>
                <th>作成日</th>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="233px" class="date2"></asp:TextBox>
                </td>
                <th>ETD</th>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="233px" class="date2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>客先</th>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>IVNO</th>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>依頼者</th>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>部署</th>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>作成者</th>
                <td >
                    <asp:TextBox ID="TextBox8" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>書類作成</th>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width ="180px">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="作成済み">作成済み</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>海貨業者</th>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>集荷</th>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width ="180px">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="集荷済み">集荷済み</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>場所</th>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server" Width ="180px">
                        <asp:ListItem Value="本社">本社</asp:ListItem>
                        <asp:ListItem Value="上野">上野</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>備考</th>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
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
