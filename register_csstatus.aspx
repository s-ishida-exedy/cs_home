<%@ Page Language="VB" AutoEventWireup="false" CodeFile="register_csstatus.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>マスタ変更_委託案件登録</title>
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
          height: 450px;
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




.design02 {
 width: 65%;
 text-align: center;
 border-collapse: collapse;
 border-spacing: 0;
 border: solid 1px #778ca3;
}
.design02 tr {
 border-top: dashed 1px #778ca3;
}
.design02 th {
 padding: 10px;
 background: #e9faf9;
}
.design02 td {
 padding: 10px;
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


<table >

<tr>



<td style="width:250px;Font-Size:25px;" >

<h2>マスタ・委託案件登録ページ</h2>

</td>





</tr>

</table>


<table class ="design02" >

<tr>

<td style="width:200px;Font-Size:15px;text-align:left;" >

1.IVNOで委託登録と削除&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:

</td>


<td style="width:200px;Font-Size:15px;" >


<%--<asp:TextBox ID="TextBox1" runat="server"  Width="150px" Height="35px" AutoPostBack="True" Font-Size="13px" MaxLength="4">Input</asp:TextBox>--%>


<asp:DropDownList ID="DropDownList6" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource7" DataTextField="C" DataValueField="C" AppendDataBoundItems="true">

<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>


<asp:Button class="btn-radius-gradient-wrap"  ID="Button1" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>


</td>

<td style="width:200px;Font-Size:15px;" >

<asp:DropDownList ID="DropDownList1" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="ITK_INVNO" DataValueField="ITK_INVNO" AppendDataBoundItems="true">

<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>



<asp:Button class="btn-radius-gradient-wrap"  ID="Button2" runat="server" Text="削除" Width="100" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>

</td>







</tr>


<tr>


<td style="width:200px;Font-Size:15px;text-align:left;" >

2.客先コードで委託登録と削除   :

</td>


<td style="width:200px;Font-Size:15px;" >

<asp:DropDownList ID="DropDownList4" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource6" DataTextField="C" DataValueField="C" AppendDataBoundItems="true">

<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>
<asp:Button class="btn-radius-gradient-wrap"  ID="Button3" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>

</td>

<td style="width:200px;Font-Size:15px;" >

<asp:DropDownList ID="DropDownList2" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="CUST_CD" DataValueField="CUST_CD" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>



<asp:Button class="btn-radius-gradient-wrap"  ID="Button4" runat="server" Text="削除" Width="100" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>


</td>
</tr>



<tr>


<td style="width:200px;Font-Size:15px;text-align:left;" >

3.海貨業者で委託登録と削除&nbsp;&nbsp;&nbsp;&nbsp;:

</td>

<td style="width:200px;Font-Size:15px;" >


<asp:DropDownList ID="DropDownList5" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource5" DataTextField="FORWARDER02" DataValueField="FORWARDER02" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>
   
<asp:Button class="btn-radius-gradient-wrap"  ID="Button5" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
   
</td>

<td style="width:200px;Font-Size:15px;" >

<asp:DropDownList ID="DropDownList3" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="FORWARDER" DataValueField="FORWARDER" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>



<asp:Button class="btn-radius-gradient-wrap"  ID="Button6" runat="server" Text="削除" Width="100" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
</td>

</tr>



<tr>

<td style="width:200px;Font-Size:15px;text-align:left;" >

4.カレンダマスタ更新&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:

</td>

<td style="width:200px;Font-Size:15px;" >


<asp:Button class="btn-radius-gradient-wrap"  ID="Button7" runat="server" Text="ファイルを開く" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>


</td>

<td style="width:200px;Font-Size:12px;" >

※半年に1回程度カレンダマスタを更新する。

</td>


</tr>


</table>

</div>

    
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop">
<a href="#">↑</a></p>   


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [ITK_INVNO] FROM [T_EXL_CSWORKSTATUS] WHERE ITK_INVNO <>'' AND ITK_REGDATE > GETDATE()-60 ORDER BY [ITK_INVNO] "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CUST_CD] FROM [T_EXL_ITAKU] WHERE CUST_CD <>'' ORDER BY CUST_CD "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [FORWARDER] FROM [T_EXL_ITAKU] WHERE FORWARDER <>'' ORDER BY [FORWARDER] "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT left([CUST_CD],4) AS C FROM [T_BOOKING] ORDER BY C" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [FORWARDER02] FROM [T_EXL_CSANKEN] ORDER BY [FORWARDER02]"></asp:SqlDataSource>
 
    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:KBHWPA85ConnectionString %>" SelectCommand="SELECT DISTINCT [CUSTCODE] AS C FROM [T_SN_HD_TB] WHERE [NOKIYMD] > GETDATE()-360 ORDER BY C "></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:KBHWPA85ConnectionString %>" SelectCommand="SELECT DISTINCT [OLD_INVNO] AS C FROM [T_INV_HD_TB]  WHERE [BLDATE] BETWEEN GETDATE()-5 AND GETDATE()+60 ORDER BY C "></asp:SqlDataSource>


</form>
</body>



</html>
