<%@ Page Language="VB" AutoEventWireup="false" CodeFile="exl_zaiko.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>
<html lang="ja">
<head>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>ポータルサイト（EXL在庫）</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="css/style.css">
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<script>
    // メニュークリック時の画面遷移
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
            } else {
                window.location.href = './detail.aspx?id=' + encodeURIComponent(arr[1]);
                return false;
            };
        });
    });

</script>
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
            width: 450px;
        }   
        .fourth-cell {
            width: 350px;
            text-align:right;
        }   
        .third-cell {
            width: 250px;
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
        /*table1設定*/
        .table1 {
	        table-layout: fixed;
	        width: 80%;
	        margin: 0 0 0 10px;
	        background: #fff;	/*背景色*/
	        color: #000;		/*文字色*/
        }
        .table1, .table1 td, .table1 th {
	        word-break: break-all;
	        border: 1px solid #ccc;	/*テーブルの枠線の幅、線種、色*/
	        padding: 10px;	/*ボックス内の余白*/
            text-align:center;
        }
        .table1 caption {
	        border: 1px solid #ccc;	/*テーブルの枠線の幅、線種、色*/
	        border-bottom: none;	/*下線だけ消す*/
	        text-align: center;		/*文字を左寄せ*/
	        background: #eee;		/*背景色*/
	        color: #666;			/*文字色*/
	        font-weight: bold;		/*太字に*/
	        padding: 10px;			/*ボックス内の余白*/
        }
        .tdh{
            width:200px;
        }
        .tdk{
            text-align:center;
            width:100px;
        }

</style>
</head>

<body class="c2">

<form id="form1" runat="server">

<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
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
                <h2>ＥＸＬ本社在庫状況</h2> 
            </td>
            <td class="second-cell">
                ※単位：拠点は間口数<%-- 、外部倉庫はパレット数--%>
            </td>
            <td class="fourth-cell">
                データ取得：<span id="view_time"></span></td>
            <td class="third-cell">
                <% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
                    <a href="./start.aspx">ホームへ戻る</a>
                <% Else %>
                    <a href="./exl_top.aspx">ホームへ戻る</a>
                <% End If %>                
            </td>
        </tr>
    </table>
    <%--現在時刻取得用スクリプト--%>
    <script type="text/javascript">
    document.getElementById("view_time").innerHTML = getNow();

    function getNow() {
	    var now = new Date();
	    var year = now.getFullYear();
	    var mon = now.getMonth()+1; //１を足すこと
	    var day = now.getDate();
	    var hour = now.getHours();
	    var min = now.getMinutes();
	    var sec = now.getSeconds();

	    //出力用
	    var s = year + "年" + mon + "月" + day + "日" + hour + "時" + min + "分" + sec + "秒"; 
	    return s;
    }
    </script>

<div id="main2" style="width:100%;">

<section>
<table class="table1">
<tr style="background-color: #eee;text-align: center;color: #666;font-weight: bold; ">
    <td colspan ="2" class="tdh">
        &nbsp;</td>
    <td class="tdh2">
        使用率
    </td>
    <td class="tdh2">
        許容間口</td>
    <td class="tdh2">
        使用間口</td>
    <td class="tdh2">
        <%--外部倉庫在庫数--%>
    </td>
</tr>
<tr>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=4F&shubetsu=">４Ｆ</a></td>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=4F&shubetsu=AF CD">ＣＤ</a></td>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        &nbsp;</td>
</tr>
<tr>
    <td class="tdk">&nbsp;</td>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=4F&shubetsu=AF CC">ＣＣ</a></td>
    <td>
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        &nbsp;</td>
</tr>
<tr>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=3F&shubetsu=">３Ｆ</a></td>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=3F&shubetsu=AFポリ">ＣＤ</a></td>
    <td>
        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        &nbsp;</td>
</tr>
<tr>
    <td class="tdk">&nbsp;</td>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=3F&shubetsu=AFアミ">ＣＣ</a></td>
    <td>
        <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        &nbsp;<asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        &nbsp;</td>
</tr>
<tr>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=1F&shubetsu=">１Ｆ</a></td>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=1F&shubetsu=OEM(ポリ)">ＣＤ</a></td>
    <td>
        <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        &nbsp;</td>
</tr>
<tr>
    <td class="tdk">&nbsp;</td>
    <td class="tdk"><a href ="./exl_zaiko_detail.aspx?place=1F&shubetsu=OEM(アミ)">ＣＣ</a></td>
    <td>
        <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        &nbsp;</td>
</tr>
</table>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT
  INFO_DATE
  , INFO_TIME
  , INFO_HEADER
FROM
  T_EXL_INFOMATION "></asp:SqlDataSource>

</section>

</div>
<!--/#main-->



</div>
<!--/#contents-in-->

<div id="side">

</div>
<!--/#side-->


<footer>

<div id="footermenu" class="inner">
</div>
<!--/footermenu-->

<div id="copyright">
    <small>Copyright&copy; <a href="start.aspx">CS PORTAL SITE</a> All Rights Reserved.</small>
    <span class="cop"><a href="https://template-party.com/" target="_blank">《Web Design:Template-Party》</a></span>
</div>

</footer>

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
