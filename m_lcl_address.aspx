<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_lcl_address.aspx.vb" Inherits="cs_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ポータルサイト(LCLLCL搬入先ﾏｽﾀ)</title>
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
        .auto-style6 {
            margin-right: 7px;
        }

        table{
          width: 100%;
        }
        th {
          position: sticky;
          text-align:left;
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

        .button04 a {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0 auto;
            padding: 0.5em 1em;
            width: 150px;
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

        .btn00
        {
            cursor : pointer;
        }

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

    </style>


    <script type="text/javascript">
      function LinkClick() {
        var url = 'register_lcl_address.aspx?q=' 
        window.open(url, null);
      }

    </script>

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

        
    <table class="header-ta" >
        <tr>
            <td class="first-cell">
                <h2>LCL搬入先ﾏｽﾀ</h2> 
            </td>
            <td class="second-cell">
            </td>
            <td class="third-cell">
                <% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
                <div class="button04">
                    <a href="lcl_tenkai_m.aspx?id={0}">管理メニュ</a>
                </div>
                <% Else %>

                <% End If %>                
            </td>
                        <td class="third-cell">
                <% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
                <div class="button04">
                    <a href="lcl_arange.aspx?id={0}">手配状況</a>
                </div>          
                <% Else %>

                <% End If %>                
            </td>
        </tr>
    </table>


    <asp:Panel ID="Panel2" runat="server"  Font-Size="12px" >

        <table >
            <tr>
                <td style="width:800px;Font-Size:13px;" >
<%--                    <p><b><asp:Label Font-Size="11" id="Label2" Text="LCL貨物の搬入先。" runat="server"></asp:Label></b></p>--%>
                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="新規登録" Font-Size="Small" Width ="80" />
                </td>
            </tr>
        </table>

        <div class="wrapper">
        <table class="sticky" >
        <thead class="fixed">

        </thead>

        <tbody>



        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width = "1610px" DataSourceID="SqlDataSource1" DataKeyNames="WHCD" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >

        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>

        <Columns>


        <asp:TemplateField ShowHeader="False">
        <ItemTemplate>
        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="edt" ImageUrl="~/icon/write.png" Text="編集" width = "20" height = "20" />
        </ItemTemplate>
        <HeaderStyle BackColor="#6B696B" />
        <HeaderStyle Width="10px" />
        </asp:TemplateField>


        <asp:BoundField DataField="WHCD" HeaderText="NO" SortExpression="WHCD" >
            <HeaderStyle Width="50px" HorizontalAlign="Center"  />
            </asp:BoundField>
        <asp:BoundField DataField="ADRS01" HeaderText="住所01" SortExpression="ADRS01" >
            <HeaderStyle Width="450px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="ADRS02" HeaderText="住所02" SortExpression="ADRS02" >
            <HeaderStyle Width="550px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="ADRS03" HeaderText="住所03" SortExpression="ADRS03" >
            <HeaderStyle Width="550px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>


        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>

        </tbody>
        </table>
        </div>


        </asp:Panel>   

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [M_EXL_LCL_WH].[WHCD], [M_EXL_LCL_WH].[ADRS01], [M_EXL_LCL_WH].[ADRS02], [M_EXL_LCL_WH].[ADRS03] FROM [M_EXL_LCL_WH] ORDER BY WHCD"></asp:SqlDataSource>
 
    </div>

<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

</form>

</body>
</html>
