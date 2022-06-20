﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="shelf_sagekeep_doc.aspx.vb" Inherits="yuusen" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(書庫保管リスト)</title>
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
            height: 400px;
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
        <table style="height:10px;">
            <tr>
                <td style="width:250px;Font-Size:25px;" >
                    <h2>書庫保管リスト</h2>
                </td>
                <td style="width:100px;" >
                </td>
                <td style="width:550px;">
                    <h1>フィルタ</h1>
                    <p></p>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" >
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>棚番号</asp:ListItem>
                    <asp:ListItem>業務区分</asp:ListItem>
                    <asp:ListItem>チーム</asp:ListItem>
                    <asp:ListItem>担当者</asp:ListItem>
                    <asp:ListItem>書類名</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="140px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" ></asp:DropDownList>
                    <asp:Button CssClass ="button01" ID="Button1" runat="server" Text="全件表示" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" />
                    <%--<asp:CheckBox   ID="CheckBox1" runat="server" Height="13px" Text="自社通関"  Font-Size="13px" />--%>
                </td>
                <td style="width:150px;" >
                </td>
                <td style="width:150px;" >
                    <div class="button04">
                    <a href="shelf_manage.aspx?id={0}">棚状況へ</a>
                    </div>
                </td>
            </tr>
        </table>    

        <table style="height:10px;">
            <tr>
                <td style="width:1500px;" >
                <%--<asp:Button  ID="Button2" CssClass="btn00" runat="server" Text="フォルダ作成登録" Width="120px" Height="30px" AutoPostBack="True" Font-Size="13px" />--%> 
                <asp:Button ID="Button2" runat="server" Text="エクセル出力" Width="120px" Height="30px" Font-Size="13px" />
                <p></p>
                <%--<asp:Label ID="Label1" runat="server" Text="※チェック後にボタンを押す。(チェックボックスが緑のものはフォルダ作成可能)"  Font-Size="10px"></asp:Label>--%>
                </td>
            </tr>
        </table>

        <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">
            <div class="wrapper">
                <table class="sticky">
                    <thead class="fixed">
                    </thead>

                    <tbody>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource7" Height="100px" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px">
                        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
                        <HeaderStyle CssClass="Freezing"></HeaderStyle>

                        <Columns>

                        <asp:BoundField DataField="SHELFNO" HeaderText="棚番号" SortExpression="棚番号" >
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DOC_ID" HeaderText="書類番号" SortExpression="書類番号" >
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="KBN02" HeaderText="業務区分" SortExpression="業務区分" >
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>
<%--                        <asp:BoundField DataField="NO01" HeaderText="シート" SortExpression="FORWARDER" />
                        <asp:BoundField DataField="NO02" HeaderText="海貨業者" SortExpression="FORWARDER02" />--%>
                        <asp:BoundField DataField="DEPARTMENT" HeaderText="部署" SortExpression="部署" >
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEAM" HeaderText="チーム" SortExpression="チーム" >
                        <HeaderStyle Width="50px" />
                        </asp:BoundField>
<%--                        <asp:BoundField DataField="PIC01" HeaderText="INVOICE" SortExpression="INVOICE" />--%>
                        <asp:BoundField DataField="PIC02" HeaderText="担当者" SortExpression="担当者" >
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DOCNAME" HeaderText="書類名" SortExpression="書類名" >
                        <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CONTENTS" HeaderText="内容" SortExpression="内容" >
                        <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DATE" HeaderText="登録日" SortExpression="登録日" >
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SHELF_LIFE" HeaderText="期限" SortExpression="期限" >
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="KBN01" HeaderText="補完区分" SortExpression="補完区分" >
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>

                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        </asp:GridView>

                    </tbody>
                </table>
            </div>
        </asp:Panel>
                            
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


        <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_DOC_BOX] ORDER BY DOC_ID"></asp:SqlDataSource>




        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_DOC_BOX] WHERE ([SHELFNO] = @SHELFNO) ORDER BY DOC_ID">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="SHELFNO" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_DOC_BOX] WHERE ([KBN02] = @KBN02) ORDER BY DOC_ID">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="KBN02" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_DOC_BOX] WHERE ([TEAM] = @TEAM) ORDER BY DOC_ID">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="TEAM" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_DOC_BOX] WHERE ([PIC02] = @PIC02) ORDER BY DOC_ID">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="PIC02" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>


         <asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_DOC_BOX] WHERE ([DOCNAME] = @DOCNAME) ORDER BY DOC_ID">
        <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList2" Name="DOCNAME" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>


   
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [SHELFNO] FROM [T_EXL_DOC_BOX]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [KBN02] FROM [T_EXL_DOC_BOX]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [TEAM] FROM [T_EXL_DOC_BOX]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [PIC02] FROM [T_EXL_DOC_BOX]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [DOCNAME] FROM [T_EXL_DOC_BOX]"></asp:SqlDataSource>





    </form> 

</body>

</html>
