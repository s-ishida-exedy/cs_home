<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cs_manual_detail.aspx.vb" Inherits="cs_home" ValidateRequest="false"%>

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
        .header-ta {
            width: 1300px;
        }
        .first-cell {
            text-align:left;
            font-size:25px;
            width: 400px;
        }
        .second-cell {
            width: 200px;
        }   
        .second-cell2 {
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
        .txtb{
            padding: 5px;
            font-size :small ;
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
<!-- メニューの編集はheader.aspxで行う -->
    <!-- #Include File="header/header.aspx" -->
       
<div id="contents2" class="inner2">
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>ＣＳマニュアル詳細</h2> 
                </td>
                <td class="second-cell">
                    <asp:Button ID="Button7" runat="server" Text="更　新" style="width:164px" Font-Size="Small" />
                </td>
                <td class="second-cell2">
                    <asp:Label  ID="Label4" runat="server" Text="データ引用"   /> 
                    <asp:CheckBox   ID="CheckBox100" runat="server" />
                    <asp:DropDownList ID="DropDownList200" runat="server" Width ="90px" AutoPostBack="true" >
                    </asp:DropDownList>
                    <asp:Button ID="Button100" runat="server" Text="クリア" style="width:70px" Font-Size="Small" />
                    <asp:Label ID="Label3" runat="server" Text="Label" Class="err"></asp:Label>
                </td>
                <td class="third-cell">
                    <a href ="./cs_manual.aspx">一覧へ戻る</a>
                </td>
        </tr>
    </table>
<div id="main2" style="width:100%;height:600px;overflow:scroll;-webkit-overflow-scrolling:touch;border:solid 0px;">
        <table class="ta3">
            <tr>
                <th>新コード</th>
                <td>
<%--                    <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="233px" Class ="txtb"></asp:TextBox>--%>                
                    <asp:DropDownList ID="DropDownList100"  AutoPostBack="true" runat="server" Width ="90px" Height="35px" Font-Size="12.5">
                    </asp:DropDownList>
                </td>
                <th>国名</th>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Height="20px" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>略称</th>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Height="20px" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th >客先名</th>
                <td colspan="3">
                    <asp:TextBox ID="TextBox3" runat="server" Height="15px" Width="600px" Class ="txtb"></asp:TextBox>
                </td>
                <th>最終更新者</th>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="233px" Class ="txtb" disabled></asp:TextBox>
                </td>
            </tr>
        </table>

 

        <table class="ta3" style="display:none" >
            <tr>
                <th>客先</th>
                <td>
<%--                    <asp:TextBox ID="" runat="server" Height="20px" Width="233px" Class ="txtb" disabled="disabled"></asp:TextBox>--%>
                </td>
                <th>建値</th>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Height="20px" Width="233px" Class ="txtb" disabled="disabled"></asp:TextBox>
                </td>

                <th>B/L種類</th>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" Height="20px" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
        </table>

        <table class="ta3" >
            <tr>
                <th>Cust(ACCT)<br/>Name/Adress</th>
                <td >
                    <asp:TextBox ID="TextBox8" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
                <th>Consignee<br/>Name/Adress</th>
                <td>
                    <asp:TextBox ID="TextBox55" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
        </table>

        <table class="ta3" style="display:none">
            <tr>
                <th>乙仲情報</th>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb" disabled="disabled"></asp:TextBox>
                </td>
                <th>お客様要求事項</th>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb" disabled="disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Final<br/>Destination</th>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
                <th>ConsigneeName of S/I</th>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
        </table>

        <table class="ta3">

            <tr>
                <th style="background-color:#FF9E8C;color:white">Consignee of SI</th>
                <td>
                    <asp:TextBox ID="TextBox41" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
                <th style="background-color:#FF9E8C;color:white">Consignee of SI Address</th>
                <td >
                    <asp:TextBox ID="TextBox42" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="background-color:#FF9E8C;color:white">Final<br/>Destination</th>
                <td>
                    <asp:TextBox ID="TextBox43" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
                <th style="background-color:#FF9E8C;color:white">Final Destination Address</th>
                <td>
                    <asp:TextBox ID="TextBox44" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="background-color:#FF9E8C;color:white">NOTIFY</th>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
                <th style="background-color:#009966;color:white">B/L送付方法</th>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" Height="50px" Width="450px" TextMode="MultiLine" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
        </table>


        <table class="ta3">
            <tr>
                <th style="background-color:#009966;color:white">木材</th>
                <td>
                    <asp:DropDownList ID="DropDownList6" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">CO</th>
                <td >
                    <asp:DropDownList ID="DropDownList4" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
               <th style="background-color:#009966;color:white">エジプト査証</th>
                <td>
                    <asp:DropDownList ID="DropDownList16" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">EPA</th>
                <td >
                    <asp:DropDownList ID="DropDownList5" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">FTA</th>
                <td>
                    <asp:DropDownList ID="DropDownList14" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>


            </tr>

            <tr>
                <th style="background-color:#009966;color:white">適合証明書</th>
                <td>
                    <asp:DropDownList ID="DropDownList15" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">ERL</th>
                <td >
                    <asp:DropDownList ID="DropDownList9" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">ﾍﾞｯｾﾙ</th>
                <td >
                    <asp:DropDownList ID="DropDownList10" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">IV,PL郵送必要有無</th>
                <td>
                    <asp:DropDownList ID="DropDownList13" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">コンテナ清掃</th>
                <td>
                    <asp:DropDownList ID="DropDownList11" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
           <tr>
                <th style="background-color:#6666FF;color:white">BR帳票出力</th>
                <td>
                    <asp:DropDownList ID="DropDownList17" runat="server" Width ="80px">
                        <asp:ListItem Value="有り">有り</asp:ListItem>
                        <asp:ListItem Value="無し">無し</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#6666FF;color:white">IV内訳自動計算</th>
                <td>
                    <asp:DropDownList ID="DropDownList18" runat="server" Width ="80px">
                        <asp:ListItem Value="有り">有り</asp:ListItem>
                        <asp:ListItem Value="無し">無し</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <table class="ta3"  style="display:none">
 
            <tr>

                <th style="background-color:#6666FF;color:white">海貨業者</th>
                <td>
                    <asp:TextBox ID="TextBox47" runat="server" Height="20px" Width="233px" Class ="txtb"></asp:TextBox>
                </td>

                <th style="background-color:#009966;color:white">IV</th>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">PL</th>
                <td >
                    <asp:DropDownList ID="DropDownList2" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">BL</th>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                            <th style="background-color:#009966;color:white">LC取引</th>
                <td>
                    <asp:DropDownList ID="DropDownList12" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">検査</th>
                <td>
                    <asp:DropDownList ID="DropDownList8" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="background-color:#009966;color:white">ﾃﾞﾘﾊﾞﾘ</th>
                <td >
                    <asp:DropDownList ID="DropDownList7" runat="server" Width ="80px">
                        <asp:ListItem Value="○">○</asp:ListItem>
                        <asp:ListItem Value="×">×</asp:ListItem>
                    </asp:DropDownList>
                </td>
              </tr>
        </table>





        <table class="ta3" style="display:none" >
            <tr>
                <th>営業担当</th>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>営業担当2</th>
                <td>
                    <asp:TextBox ID="TextBox15" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>営業担当3</th>
                <td>
                    <asp:TextBox ID="TextBox16" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>営業担当4</th>
                <td>
                    <asp:TextBox ID="TextBox17" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>営業担当5</th>
                <td>
                    <asp:TextBox ID="TextBox18" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>客先担当者</th>
                <td>
                    <asp:TextBox ID="TextBox19" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>客先担当者2</th>
                <td>
                    <asp:TextBox ID="TextBox20" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>客先担当者3</th>
                <td>
                    <asp:TextBox ID="TextBox21" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>客先担当者4</th>
                <td>
                    <asp:TextBox ID="TextBox22" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>客先担当者5</th>
                <td>
                    <asp:TextBox ID="TextBox23" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>船曜日</th>
                <td>
                    <asp:TextBox ID="TextBox24" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>国・仕向地</th>
                <td>
                    <asp:TextBox ID="TextBox25" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>出荷区分</th>
                <td>
                    <asp:TextBox ID="TextBox26" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>出荷準備L/T</th>
                <td>
                    <asp:TextBox ID="TextBox27" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>指定港</th>
                <td>
                    <asp:TextBox ID="TextBox28" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>船社契約先/契約番号</th>
                <td>
                    <asp:TextBox ID="TextBox29" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>指定船社</th>
                <td>
                    <asp:TextBox ID="TextBox30" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>現地代理店</th>
                <td>
                    <asp:TextBox ID="TextBox31" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>コンテナサイズ/本数指定</th>
                <td>
                    <asp:TextBox ID="TextBox32" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>Remarks onBL</th>
                <td>
                    <asp:TextBox ID="TextBox33" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>IV番号ON BL</th>
                <td>
                    <asp:TextBox ID="TextBox34" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>HSコードonBL</th>
                <td>
                    <asp:TextBox ID="TextBox56" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>CNEE担当者</th>
                <td>
                    <asp:TextBox ID="TextBox35" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>CNEE担当者<br/>連絡先1</th>
                <td>
                    <asp:TextBox ID="TextBox36" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>CNEE担当者<br/>連絡先2</th>
                <td>
                    <asp:TextBox ID="TextBox37" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>CNEE担当者2</th>
                <td>
                    <asp:TextBox ID="TextBox38" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>CNEE担当者2<br/>連絡先1</th>
                <td>
                    <asp:TextBox ID="TextBox39" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>TAX ID</th>
                <td>
                    <asp:TextBox ID="TextBox40" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>

            </tr>
        </table>

        <table class="ta3" style="display:none">

            <tr>
                <th>宛先</th>
                <td>
                    <asp:TextBox ID="TextBox48" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>TO</th>
                <td>
                    <asp:TextBox ID="TextBox49" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>CC1</th>
                <td>
                    <asp:TextBox ID="TextBox50" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>CC2</th>
                <td>
                    <asp:TextBox ID="TextBox51" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>CC3</th>
                <td>
                    <asp:TextBox ID="TextBox52" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
                <th>海貨業者メール用</th>
                <td>
                    <asp:TextBox ID="TextBox53" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>宛名メール用</th>
                <td>
                    <asp:TextBox ID="TextBox54" runat="server" Width="233px" Class ="txtb"></asp:TextBox>
                </td>

            </tr>
        </table>


</div>
<!--/#main2-->

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [NEW_CODE] FROM [T_EXL_CSMANUAL] ORDER BY NEW_CODE ">
</asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CUSTCODE] FROM [T_EXL_CSMANUAL_ADDCUST] ORDER BY CUSTCODE ">
        </asp:SqlDataSource>

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
