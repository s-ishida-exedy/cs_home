    <%@ Page Language="VB" AutoEventWireup="false" CodeFile="anken_kanryo_all.aspx.vb" Inherits="yuusen" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(完了報告)</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>

    <style type="text/css">
        #form1
            {
            background-color : #ffffff;
            color : #000000;
            }
            body {
            background-color: #ffffff;
            }
            A.sample1:link { color: blue;}
            A.sample1:visited { color: blue;}
            A.sample1:active { color: blue;}
            A.sample1:hover { color: blue;}

        table{
            width: 100%;
        }
        th {
            position: sticky;
            top: 0;
            z-index: 0;
            background-color: #FFFFFF;
            color: #000000;
        }
        .wrapper {
            overflow: scroll;
            height: 300px;
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


        .button03 a {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0 auto;
            padding: 0.5em 2em;
            width: 80px;
            color: #696969;
            font-size: 13px;
            font-weight: 700;

        }

        .button03 a::after {
            content: '';
            width: 5px;
            height: 5px;
            border-top: 1px solid #696969;
            border-right: 1px solid #696969;
            transform: rotate(45deg);
        }

        .button03 a:hover {

            text-decoration: none;
            border-width: 4px;
            text-align: center

        }


        .DropDown
        {
            background-color: #ffffff;
            border-color: #000000;
            border-style: solid;
            border: none;
            color: #000000;
            text-align: center;
            border-bottom: inset 2px #ffffff;
                    cursor : pointer;
        }

        .DropDown:hover {
            text-align: center;
            border-bottom: inset 2px #000000;
        }

        .btn00
        {
            cursor : pointer;
        }

        .button01
        {
            background-color: #ffffff;
            border:none;
            color: #000000;
            border-bottom: inset 2px #ffffff;
                    cursor : pointer;
        }

        .button01:hover {
            text-align: center;
            border-bottom: inset 2px #000000;
        }

        .table01
        {
            /*background:linear-gradient(transparent 90%, #6fbfd1 0%);*/
            background: -webkit-gradient(linear, left top, right bottom,
            from(rgba(255,153,0,1)),
            color-stop(50%, rgba(255,153,0,1)),
            to(rgba(255,255,255,1)));
            background: -moz-linear-gradient(left,
            #6fbfd1,
            #6fbfd1 50%,
            #ffffff);
            background: linear-gradient(transparent 10%, to right,
            #6fbfd1,
            #6fbfd1 50%,
            #ffffff );
            color: #ffffff;
        }

        h1 {
            position: relative;
            display: inline-block;
            padding: 0 143px;
            text-align: center;
        }

        h1:before, h1:after {
            content: '';
            position: absolute;
            top: 50%;
            display: inline-block;
            width:100px;
            height: 2px;
            background-color: black;
        }

        h1:before {
            left:0;
        }
        h1:after {
            right: 0;
        }

        .button04 a {
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

        .button04 a::after {
            content: '';
            width: 5px;
            height: 5px;
            border-top: 2px solid #000000;
            border-right: 2px solid #000000;
            transform: rotate(45deg);
        }

        .button04 a:hover {
            color: #000000;
            text-decoration: none;
            background-color: #ffffff;
            border: 2px solid #000000;
        }

        .button04 a:hover::after {
        border-top: 2px solid #000000;
        border-right: 2px solid #000000;
        }



    </style>
 <%--<script>
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
</script>--%>
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
<% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
    <!-- #Include File="header/header.aspx" -->
<% Else %>
    <!-- #Include File="header/exl_header.aspx" -->
<% End If %>


    <div id="contents2" class="inner2">
        <table style="height:10px;" >
            <tr>
                <td style="width:250px;Font-Size:25px;" >
                    <h2>完了報告</h2>
                </td>
                <td style="width:250px;" >

                <div style="background-color:none;text-align: center;border:groove">
                    <asp:Label ID="Label6" runat="server" Text="本日全案件完了" ></asp:Label>
                </div>
                </td>
                <td style="width:750px;" >
                </td>
            </tr>
        </table>    

        <table style="height:10px;">
            <tr>
                <td style="width:1000px;" >
                <%--<asp:Button  ID="Button2" CssClass="btn00" runat="server" Text="フォルダ作成登録" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" />--%> 
                <asp:Button  ID="Button1" CssClass="btn00" runat="server" Text="案件作成" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" />
<%--                <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="C" DataValueField="C" AppendDataBoundItems="true">
                <asp:ListItem Text="Select" Value="" />
                </asp:DropDownList>--%>
                </td>
                <td style="width:300px;" >
                    <asp:Label ID="Label10" runat="server" Text="AAA" ></asp:Label>
                </td>
            </tr>
        </table>


        <asp:Panel ID="Panel1" runat="server"  Font-Size="12px" >
            <div class="wrapper">
                <table class="sticky" >
                    <thead class="fixed">
                    </thead>

                    <tbody>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource7" Width="2350px" Height="100px" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px">
                        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
                        <HeaderStyle CssClass="Freezing"></HeaderStyle>

                        <Columns>

                        <asp:TemplateField ShowHeader="False" HeaderText="KD">
                            <HeaderStyle Width="30px" />
                            <ItemTemplate>
                            <asp:CheckBox ID="cb01" Checked="false"  runat="server" CausesValidation="false" CommandName="cb01" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False" HeaderText="ｱﾌﾀ">
                            <HeaderStyle Width="30px" />
                            <ItemTemplate>
                            <asp:CheckBox ID="cb02" Checked="false"  runat="server" CausesValidation="false" CommandName="cb02" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False" HeaderText="登録">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="edt" Text="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                        <asp:TemplateField ShowHeader="False" HeaderText="完 了">
                            <HeaderStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text="" CausesValidation="false" CommandName="edt5"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False" HeaderText="C S">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="edt2" Text="更新" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                        <asp:TemplateField ShowHeader="False" HeaderText="確 認" >
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text="" CausesValidation="false" CommandName="edt6"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                        <asp:BoundField DataField="DAY11" HeaderText="バン日" SortExpression="DAY11" >
                        <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FLG01" HeaderText="バン時間" SortExpression="FLG01" >
                        <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FORWARDER02" HeaderText="海貨業者" SortExpression="FORWARDER02" >
                        <HeaderStyle Width="140px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST" HeaderText="客先コード" SortExpression="CUST" >
                        <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DESTINATION" HeaderText="仕向地" SortExpression="DESTINATION" >
                        <HeaderStyle Width="260px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="INVOICE" HeaderText="INVOICE" SortExpression="INVOICE" >
                        <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CUT_DATE" HeaderText="CUT" SortExpression="CUT_DATE" >
                        <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ETD" HeaderText="ETD" SortExpression="ETD" >
                        <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CONTAINER" HeaderText="コンテナ" SortExpression="CONTAINER" >
                        <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STATUS" HeaderText="進捗状況" SortExpression="STATUS" >
                        <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BOOKING_NO" HeaderText="BOOKING_NO" SortExpression="BOOKING_NO" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VESSEL_NAME" HeaderText="船名" SortExpression="VESSEL_NAME" >
                        <HeaderStyle Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VOYAGE_NO" HeaderText="VOYAGE_NO" SortExpression="VOYAGE_NO" >
                        <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PLACE_OF_RECEIPT" HeaderText="荷受港" SortExpression="PLACE_OF_RECEIPT" >
                        <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LOADING_PORT" HeaderText="積出港" SortExpression="LOADING_PORT" >
                        <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DISCHARGING_PORT" HeaderText="揚港" SortExpression="DISCHARGING_PORT" >
                        <HeaderStyle Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PLACE_OF_DELIVERY" HeaderText="配送先" SortExpression="PLACE_OF_DELIVERY" >
                        <HeaderStyle Width="230px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="FLG04" HeaderText="FLG04" SortExpression="FLG04" >
                        <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FLG05" HeaderText="FLG05" SortExpression="FLG05" >
                        <HeaderStyle Width="100px" />
                        </asp:BoundField>



                        <asp:TemplateField ShowHeader="False" HeaderText="ｴﾗｰ解除">
                        <HeaderStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CausesValidation="false" CommandName="edt3" Text="更新" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="ﾁｪｯｸ表示">
                        <HeaderStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Button ID="Button4" runat="server" CausesValidation="false" CommandName="edt4" Text="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="削除">
                        <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <asp:Button ID="Button5" runat="server" CausesValidation="false" CommandName="edt5" Text="削除" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="FLG03" HeaderText="FLG03" SortExpression="FLG03" />
                        <asp:BoundField DataField="DAY10" HeaderText="最終" SortExpression="DAY10" />
                        <asp:BoundField DataField="DAY09" HeaderText="ROWNO" SortExpression="DAY09" />
                        <asp:BoundField DataField="FINALVANDATE" HeaderText="FINALVANDATE" SortExpression="FINALVANDATE" />

                        <asp:TemplateField ShowHeader="False" HeaderText="最終・途中">
                        <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:Button ID="Button6" runat="server" CausesValidation="false" CommandName="edt6" Text="変更" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        </asp:GridView>

                    </tbody>
                </table>


                
        <table style="height:20px;">
        </table>

        <table>
            <tr>
                <td style="width:150px;Font-Size:15px;" >
                    <asp:Label ID="Label5" runat="server" Text="<備考記載欄>"></asp:Label>
                </td>
                <td style="width:800px;Font-Size:15px;" >
                </td>
            </tr>
        </table>

        <table>
        </table>

        <table>
            <tr>
                <td style="width:100px;Font-Size:15px; height:60px;" >
                    <asp:TextBox ID="TextBox1" runat="server" Width="500px" Height="80px" TextMode="MultiLine" CssClass="" Font-Size="12px" AutoPostBack="True"  AppendDataBoundItems="true" ></asp:TextBox>
                </td>
                    <td style="width:600px;Font-Size:25px;" >
                </td>
            </tr>
        </table>




            </div>
        </asp:Panel>
                            

    <asp:Panel ID="Panel3" runat="server"  Font-Size="20px" Visible ="false">

        
        <table style="width:1500px;height:10px;">
        </table>
        <table style="width:1500px;height:10px;">
            <tr>
                <td style="width:1500px;" >
                    <asp:Label ID="Label12" runat="server" Text="データ未更新です。8:30以降の場合、異常報告ボタンを押してください。"></asp:Label>
                    <asp:Button ID="Button5"  CssClass ="btn00" runat="server" Text="異常報告" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" />
                </td>
            </tr>
        </table>

    </asp:Panel>


        
    <asp:Panel ID="Panel2" runat="server"  Font-Size="20px" Visible ="false">

        
        <table style="width:1500px;height:10px;">
        </table>
        <table style="width:1500px;height:10px;">
            <tr>
                <td style="width:1500px;" >
                    <asp:Label ID="Label1" runat="server" Text="Bookingシート更新中のため操作できません。 更新時間："></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="08:00-08:10"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="11:50-12:00"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="14:55-15:05"></asp:Label>
                </td>
            </tr>
        </table>

    </asp:Panel>

        <table style="height:10px;">
        </table>
    </div>
    
        <footer>

        <div id="footermenu" class="inner">
        </div>
        <!--/footermenu-->

        <div id="copyright">
        </div>

        </footer>

        <!--/#contents2-->

        <!--ページの上部に戻る「↑」ボタン-->
        <p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>


        <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_CSKANRYO] WHERE DAY08 ='' or DAY08 IS NULL ORDER BY FLG01,CUT_DATE,DAY10 "></asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [INVOICE] AS C FROM [T_EXL_CSANKEN] WHERE [INVOICE] IS NOT NULL and [INVOICE] <>'' ORDER BY C "></asp:SqlDataSource>



    </form> 

</body>

</html>
