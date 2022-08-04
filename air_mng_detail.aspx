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
        .ta3 th{
            background: #e6e6fa;
        }
        .err{
            color:red;
            font-weight :700;
        }
        .txtb{
            padding: 5px;
            font-size :small ;
        }
        .txtb_r{
            text-align :right ;
            padding: 5px;
            font-size :small ;
        }
        .date2{
            padding: 5px;
            font-size :small ;
        }
        .drpb{
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
                    <asp:CheckBox ID="CheckBox1" runat="server" Text ="EXCEL再出力" Font-Size="Small"  />&nbsp;
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
                <th>依頼者</th>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" Width="233px" CssClass="txtb"></asp:TextBox>
                </td>
                <th>部署</th>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" Width="233px" CssClass="txtb"></asp:TextBox>
                </td>
                <th>作成者</th>
                <td >
                    <asp:TextBox ID="TextBox8" runat="server" Width="233px" CssClass="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>客先</th>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="233px" CssClass="txtb"></asp:TextBox>
                </td>
                <th>IVNO</th>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="233px" CssClass="txtb"></asp:TextBox>
                </td>
                <th>SNNO</th>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server" Width="120px" CssClass="txtb"></asp:TextBox>
                    <asp:Button ID="Button3" runat="server" Text="SN情報取得" Font-Size="Small" Width ="100px"/>
                </td>
            </tr>
            <tr>
                <th>CUT日</th>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server" Width="233px" class="date2"></asp:TextBox>
                </td>
                <th>搬入日</th>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server" Width="233px" class="date2"></asp:TextBox>
                </td>
                <th>到着日</th>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server" Width="233px" class="date2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>建値</th>
                <td>
                    <asp:DropDownList ID="DropDownList4" runat="server" Width ="180px" CssClass="drpb">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="02">02:C&F</asp:ListItem>
                        <asp:ListItem Value="03">03:FOB</asp:ListItem>
                        <asp:ListItem Value="04">04:CIF&I</asp:ListItem>
                        <asp:ListItem Value="05">05:CIF&C</asp:ListItem>
                        <asp:ListItem Value="06">06:C&I</asp:ListItem>
                        <asp:ListItem Value="07">07:EX-GO</asp:ListItem>
                        <asp:ListItem Value="08">08:CIP</asp:ListItem>
                        <asp:ListItem Value="09">09:DDU</asp:ListItem>
                        <asp:ListItem Value="10">10:DDP</asp:ListItem>
                        <asp:ListItem Value="11">11:FAS</asp:ListItem>
                        <asp:ListItem Value="12">12:EX-WORKS</asp:ListItem>
                        <asp:ListItem Value="13">13:CFR</asp:ListItem>
                        <asp:ListItem Value="14">14:FCA</asp:ListItem>
                        <asp:ListItem Value="15">15:CPT</asp:ListItem>
                        <asp:ListItem Value="16">16:CIP</asp:ListItem>
                        <asp:ListItem Value="17">17:DAF</asp:ListItem>
                        <asp:ListItem Value="18">18:DES</asp:ListItem>
                        <asp:ListItem Value="19">19:DEQ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>Trade Term</th>
                <td >
                    <asp:TextBox ID="TextBox17" runat="server" Width="233px" CssClass="txtb"></asp:TextBox>
                </td>
                <th>出荷方法</th>
                <td >
                    <asp:DropDownList ID="DropDownList6" runat="server" Width ="180px" CssClass="drpb">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="01">01:20 FTコンテナ</asp:ListItem>
                        <asp:ListItem Value="02">02:40 FTコンテナ</asp:ListItem>
                        <asp:ListItem Value="03">03:CFS</asp:ListItem>
                        <asp:ListItem Value="04">04:LOOSE</asp:ListItem>
                        <asp:ListItem Value="05">05:AIR</asp:ListItem>
                        <asp:ListItem Value="06">06:COURIER SERVICE</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>通貨</th>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="" CssClass="txtb"></asp:Label>
<%--                    <asp:DropDownList ID="DropDownList5" runat="server" Width ="180px" AutoPostBack="True" CssClass="drpb">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="01">JPY</asp:ListItem>
                        <asp:ListItem Value="02">US$</asp:ListItem>
                        <asp:ListItem Value="03">EUR</asp:ListItem>
                        <asp:ListItem Value="04">A$</asp:ListItem>
                        <asp:ListItem Value="05">INR</asp:ListItem>
                        <asp:ListItem Value="06">THB</asp:ListItem>
                        <asp:ListItem Value="07">NT$</asp:ListItem>
                        <asp:ListItem Value="08">MYR</asp:ListItem>
                        <asp:ListItem Value="09">CNY</asp:ListItem>
                        <asp:ListItem Value="10">IDR</asp:ListItem>
                        <asp:ListItem Value="11">NR\</asp:ListItem>
                        <asp:ListItem Value="12">WON</asp:ListItem>
                        <asp:ListItem Value="13">MRK</asp:ListItem>
                        <asp:ListItem Value="14">NZ$</asp:ListItem>
                    </asp:DropDownList>--%>
                </td>
                <th>レート</th>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="" CssClass="txtb_r"></asp:Label>
                    <%--<asp:TextBox ID="TextBox16" runat="server" Width="233px" CssClass="txtb_r"></asp:TextBox>--%>
                </td>
                <th>場所</th>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server" Width ="180px" CssClass="drpb">
                        <asp:ListItem Value="本社">本社</asp:ListItem>
                        <asp:ListItem Value="上野">上野</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>海貨業者</th>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" Width="233px" CssClass="txtb"></asp:TextBox>
                </td>
                <th>書類作成</th>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width ="180px" CssClass="drpb">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="作成済み">作成済み</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>集荷</th>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width ="180px" CssClass="drpb">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="集荷済み">集荷済み</asp:ListItem>
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
