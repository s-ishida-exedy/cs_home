<%@ Page Language="VB" AutoEventWireup="false" CodeFile="air_exclusive.aspx.vb" Inherits="cs_home"EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ポータルサイト(AIR専用客先オーダー確認)</title>
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
            width: 500px;
        }   
        .ex-cell{
            width: 300px;
            border-collapse: separate;
            border-spacing: 0;
            border: solid 1px #778ca3;
            padding: 2px;
        }
        .third-cell {
            width: 100px;
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
       　/*ヘッダー固定用*/
        .sticky{
          width: 100%;
        }
        th {
          position: sticky;
          top: 0;
          z-index: 0;
          background-color: #000084;
          color: #ffffff;
        }
        .wrapper {
          overflow: scroll;
          height: 470px;
          width: calc(100% - 360px);
        }
        .flex{
            display: flex;
        }
        .flex div{
           
        }
        .right{
            width: 360px;
            padding: 10px
        }
        .tab1 {
            border-collapse:collapse;
            border-spacing:0px;
            border:1px solid #000000;
            width:350px;
        }

        .td1{
            border:1px solid #000000;
            width:190px;
            text-align:center;
        }
        .td2{
            border:1px solid #000000;
            width:90px;
            text-align:center;
        }
        .td3{
            border:1px solid #000000;
            width:70px;
            text-align:center;
        }
        .err{
            color:red;
            font-weight :700;
        }
        .DropDownList{
            text-align :center;
            font-size :small;
        }
        .txtb{
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
                <h2>AIR専用客先オーダー確認</h2>  
            </td>
            <td class="second-cell">
                <asp:Label ID="Label12" runat="server" Text="" Class="err"></asp:Label>
                <br/>
                <asp:CheckBox ID="CheckBox2" runat="server" text="IVNOが割り振られていない案件のみ表示" Font-Size="Small" AutoPostBack="true"  />
                &nbsp;&nbsp;
                <asp:Button ID="Button14" runat="server" Text=" ﾘｾｯﾄ " Font-Size="Small" Width ="80" />
            </td>
            <td class="ex-cell">
                &nbsp;
                <asp:Label ID="Label14" runat="server" Text="登録済みAIR専用客先：" Font-Size="Small"></asp:Label>
                <asp:DropDownList ID="DropDownList2" class="DropDownList" runat="server" DataSourceID="SqlDataSource3" DataTextField="CUST_CD" DataValueField="CUST_CD"></asp:DropDownList>
                <br/>
                &nbsp;
                <asp:TextBox ID="TextBox1" runat="server" Width="80px"></asp:TextBox>
                <asp:Button ID="Button12" runat="server" Text="追加" Width="70px" Font-Size="Small" />
                <asp:Button ID="Button13" runat="server" Text="削除" Width="70px" Font-Size="Small" />
            </td>
            <td class="third-cell">
                <a href="./start.aspx">ホームへ戻る</a>
            </td>
        </tr>
    </table>
<div class="flex">

<div class="wrapper">
<table class="sticky">
<thead class="fixed">
</thead>
<tbody>
<%--<div id="main2" style="width:100%;height:450px;overflow:scroll;-webkit-overflow-scrolling:touch;border:None;">--%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="900px" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" 
        ShowHeaderWhenEmpty="True">
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>

                <asp:ButtonField ButtonType="Button" CommandName="edt" Text="ｾｯﾄ">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>

                <asp:BoundField DataField="CUST_CD" HeaderText="客先" SortExpression="CUST_CD" ReadOnly="true" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="NOUKI" HeaderText="納期" SortExpression="NOUKI" ReadOnly="true" >
                <HeaderStyle Width="120px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LS_TYP" HeaderText="LS" SortExpression="LS_TYP" ReadOnly="true" > 
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CUST_ODR_NO" HeaderText="客先注文番号" SortExpression="CUST_ODR_NO" ReadOnly="true" />
                <asp:BoundField DataField="SALESNOTENO" HeaderText="SN＃" SortExpression="SALESNOTENO" ReadOnly="true" >
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="ODR_CTL_NO" HeaderText="受注管理番号" SortExpression="ODR_CTL_NO" ReadOnly="true" >
                <HeaderStyle Width="150px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IVNO" HeaderText="IVNO" SortExpression="IVNO" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="
        SELECT DISTINCT
  CUST_CD 
  , NOUKI
  , LS_TYP
  , CUST_ODR_NO
  , a.SALESNOTENO 
  , a.ODR_CTL_NO
  , b.IVNO
FROM
  T_EXL_AIR_EXC_ODR a
  LEFT JOIN T_EXL_AIR_EXCLUSIVE b
    ON a.ODR_CTL_NO = b.ODR_CTL_NO
  LEFT JOIN V_T_INV_HD_TB INV
    ON b.IVNO = INV.OLD_INVNO
WHERE 
  INV.SALESFLG IS NULL
ORDER BY NOUKI"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [M_EXL_AIR_EXC_CST]"></asp:SqlDataSource>
<%--</div>--%>

<!--/#main2-->

</tbody>
</table>
<div class="right">
    <asp:Label ID="Label13" runat="server" Text="EXL宛先："></asp:Label>&nbsp;
    <asp:TextBox ID="TextBox12" runat="server" Class ="txtb" ></asp:TextBox><br/>
    <asp:Label ID="Label1" runat="server" Text="更新対象："></asp:Label>&nbsp;
    <asp:Button ID="Button1" runat="server" Text="まとめて更新" width="200px" Font-Size="Small" />
    <table class="tab1">
        <tr>
            <th>受注管理番号</th>
            <th>IVNO</th>
            <th></th>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox2" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button2" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox3" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button3" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox4" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button4" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox5" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button5" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox6" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button6" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox7" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button7" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox8" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button8" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox9" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button9" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox10" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button10" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
        <tr>
            <td class ="td1">
                <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
            </td>
            <td class ="td2">
                <asp:TextBox ID="TextBox11" runat="server" Width ="60px"></asp:TextBox>
            </td>
            <td class ="td3">
                <asp:Button ID="Button11" runat="server" Text="削" Width ="30px" />
            </td>
        </tr>
    </table>
</div>

</div>

</div>
<!--/#FLEX-->

</div>
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a>
</p>

</form>

</body>
</html>
