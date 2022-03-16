<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eir_comfirm.aspx.vb" Inherits="cs_home" Async="true" %>

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
        .ta2{
            margin:0px 0px 5px 0px;
        }
        .ta2, .ta2 td{
            border: none;
            text-align:left ;
            padding :5px 5px 5px 10px;
        }
        .ta3{
            width:800px;
            margin:0px 0px 10px 10px;
        }
        .ta3 th{
            background: #e6e6fa;
            text-align :left;
        }
        .table01{
            margin:0px 0px 0px 10px;
        }
        .headTD0{
            background: #ffffff;
            width:150px;
        }
        .headTD1{
            background: #e6e6fa;
            font-weight :bold ;
        }
        .err{
            color:red;
            font-weight :700;
        }
        .DropDownList{
            text-align :center;
            Font-Size="Small"
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
<form id="form1" runat="server" autocomplete="off" enctype="multipart/form-data">
<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.aspxで行う -->
<% If Session("strRole") = "admin" Or Session("strRole") = "csusr" Then %>
    <!-- #Include File="header/header.aspx" -->
<% Else %>
    <!-- #Include File="header/exl_header.aspx" -->
<% End If %>
       
<div id="contents2" class="inner2">
        <table class="header-ta" >
            <tr>
                <td class="first-cell">
                    <h2>EIR、Booking情報差異連絡</h2> 
                </td>
                <td class="second-cell">
                </td>
                <td class="third-cell">
                    <% If Session("strRole") = "admin" Or Session("strRole") = "csusr" Then %>
                        <a href="./start.aspx">ホームへ戻る</a>
                    <% Else %>
                        <a href="./exl_top.aspx">ホームへ戻る</a>
                    <% End If %>                    
                </td>
            </tr>
        </table>
<div id="main2" style="width:65%;height:450px;border:None;">
        <table class="ta2">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="客先："></asp:Label>&nbsp;
                    <asp:DropDownList ID="DropDownList1" class="DropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource3" DataTextField="data_val" DataValueField="data_val" Width="180px"></asp:DropDownList>&nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="客先コード："></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>&nbsp;
                    <asp:Label ID="Label17" runat="server" Text="時間(HH:MM)："></asp:Label>
                    <asp:TextBox ID="TextBox8" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
                    <br/>
                    <asp:Button ID="Button2" runat="server" Text="確認" Font-Size="Small" width="80px" />&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="ﾘｾｯﾄ" Font-Size="Small" width="80px" />&nbsp;
                    <asp:Label ID="Label16" runat="server" Text="※相違のある個所のみ入力してください。" Class="err"></asp:Label>
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
        <table class="table01">
            <tr>
                <td style="width:200px;text-align:left">
                    <asp:Button ID="Button1" runat="server" Text="メール作成" style="width:164px" Font-Size="Small" />                    
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Label" Class="err"></asp:Label>
                </td>
            </tr>
        </table>

</div>
<!--/#main2-->
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT uid, unam
FROM M_EXL_USR"></asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT
  RTRIM(CUST_NM) + ',' + VAN_TIME AS data_val
FROM
  T_EXL_VAN_SCH_DETAIL
WHERE
  VAN_DATE = CONVERT(nvarchar,GETDATE(),111) 
  AND PLACE = '0H'
ORDER BY VAN_TIME

"></asp:SqlDataSource>

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
