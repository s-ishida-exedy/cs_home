﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cs_manual_detail.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(CSマニュアル詳細)</title>
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
        .ta3 th{
            background: #e6e6fa;
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
        <table>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label3" runat="server" Text="【CSマニュアル詳細】" ></asp:Label>    
                </td>
                <td class="auto-style7">
                    <asp:Button ID="Button7" runat="server" Text="更　新" style="width:164px" />
                </td>
                <td class="auto-style2">
                    <a href ="./cs_manual.aspx">前のページに戻る</a>
                </td>
            </tr>
        </table>
<div id="main2" style="width:100%;height:1980px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 1px;">
        <table class="ta3">
            <tr>
                <th>新コード</th>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>客先コード(旧)（代表）</th>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>客先名</th>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>略称</th>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>建値</th>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>B/L種類</th>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>B/L送付方法</th>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <th>客先：住所</th>
                <td >
                    <asp:TextBox ID="TextBox8" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>CONSIGNEE</th>
                <td>
                    <asp:TextBox ID="TextBox55" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <th>ConsigneeName of S/I</th>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Final<br/>Destination</th>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <th>NOTIFY</th>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>乙仲情報</th>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <th>お客様要求事項</th>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>IV</th>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>PL</th>
                <td >
                    <asp:DropDownList ID="DropDownList2" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>BL</th>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>CO</th>
                <td >
                    <asp:DropDownList ID="DropDownList4" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>EPA</th>
                <td >
                    <asp:DropDownList ID="DropDownList5" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>木材</th>
                <td>
                    <asp:DropDownList ID="DropDownList6" runat="server" Width ="80px">
                        <asp:ListItem Value="01">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>ﾃﾞﾘﾊﾞﾘ</th>
                <td >
                    <asp:DropDownList ID="DropDownList7" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>検査</th>
                <td>
                    <asp:DropDownList ID="DropDownList8" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>ERL</th>
                <td >
                    <asp:DropDownList ID="DropDownList9" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>ﾍﾞｯｾﾙ</th>
                <td >
                    <asp:DropDownList ID="DropDownList10" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>営業担当</th>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>営業担当2</th>
                <td>
                    <asp:TextBox ID="TextBox15" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>営業担当3</th>
                <td>
                    <asp:TextBox ID="TextBox16" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>営業担当4</th>
                <td>
                    <asp:TextBox ID="TextBox17" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>営業担当5</th>
                <td>
                    <asp:TextBox ID="TextBox18" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>客先担当者</th>
                <td>
                    <asp:TextBox ID="TextBox19" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>客先担当者2</th>
                <td>
                    <asp:TextBox ID="TextBox20" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>客先担当者3</th>
                <td>
                    <asp:TextBox ID="TextBox21" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>客先担当者4</th>
                <td>
                    <asp:TextBox ID="TextBox22" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>客先担当者5</th>
                <td>
                    <asp:TextBox ID="TextBox23" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>船曜日</th>
                <td>
                    <asp:TextBox ID="TextBox24" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>国・仕向地</th>
                <td>
                    <asp:TextBox ID="TextBox25" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>出荷区分</th>
                <td>
                    <asp:TextBox ID="TextBox26" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>出荷準備L/T</th>
                <td>
                    <asp:TextBox ID="TextBox27" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>指定港</th>
                <td>
                    <asp:TextBox ID="TextBox28" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>船社契約先/契約番号</th>
                <td>
                    <asp:TextBox ID="TextBox29" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>指定船社</th>
                <td>
                    <asp:TextBox ID="TextBox30" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>現地代理店</th>
                <td>
                    <asp:TextBox ID="TextBox31" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>コンテナサイズ/本数指定</th>
                <td>
                    <asp:TextBox ID="TextBox32" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>Remarks onBL</th>
                <td>
                    <asp:TextBox ID="TextBox33" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>IV番号ON BL</th>
                <td>
                    <asp:TextBox ID="TextBox34" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>HSコードonBL</th>
                <td>
                    <asp:TextBox ID="TextBox56" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>CNEE担当者</th>
                <td>
                    <asp:TextBox ID="TextBox35" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>CNEE担当者<br/>連絡先1</th>
                <td>
                    <asp:TextBox ID="TextBox36" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>CNEE担当者<br/>連絡先2</th>
                <td>
                    <asp:TextBox ID="TextBox37" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>CNEE担当者2</th>
                <td>
                    <asp:TextBox ID="TextBox38" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>CNEE担当者2<br/>連絡先1</th>
                <td>
                    <asp:TextBox ID="TextBox39" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>TAX ID</th>
                <td>
                    <asp:TextBox ID="TextBox40" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>コンテナ清掃</th>
                <td>
                    <asp:DropDownList ID="DropDownList11" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>LC取引</th>
                <td>
                    <asp:DropDownList ID="DropDownList12" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>Consignee of SI</th>
                <td>
                    <asp:TextBox ID="TextBox41" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <th>Consignee of SI Address</th>
                <td >
                    <asp:TextBox ID="TextBox42" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Final<br/>Destination</th>
                <td>
                    <asp:TextBox ID="TextBox43" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <th>Final Destination Address</th>
                <td>
                    <asp:TextBox ID="TextBox44" runat="server" Height="50px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="ta3">
            <tr>
                <th>ベアリング<br/>帳票出力</th>
                <td>
                    <asp:TextBox ID="TextBox45" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>INVOICE内訳<br/>自動計算</th>
                <td>
                    <asp:TextBox ID="TextBox46" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>海貨業者</th>
                <td>
                    <asp:TextBox ID="TextBox47" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>宛先</th>
                <td>
                    <asp:TextBox ID="TextBox48" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>TO</th>
                <td>
                    <asp:TextBox ID="TextBox49" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>CC1</th>
                <td>
                    <asp:TextBox ID="TextBox50" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>CC2</th>
                <td>
                    <asp:TextBox ID="TextBox51" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>CC3</th>
                <td>
                    <asp:TextBox ID="TextBox52" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>海貨業者メール用</th>
                <td>
                    <asp:TextBox ID="TextBox53" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>宛名メール用</th>
                <td>
                    <asp:TextBox ID="TextBox54" runat="server" Width="233px"></asp:TextBox>
                </td>
                <th>IV,PL郵送の<br/>必要有無</th>
                <td>
                    <asp:DropDownList ID="DropDownList13" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>FTA</th>
                <td>
                    <asp:DropDownList ID="DropDownList14" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>適合証明書</th>
                <td>
                    <asp:DropDownList ID="DropDownList15" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>エジプト査証</th>
                <td>
                    <asp:DropDownList ID="DropDownList16" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
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