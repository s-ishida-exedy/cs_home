<%@ Page Language="VB" AutoEventWireup="false" CodeFile="register_csstatus.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>出荷案件進捗</title>
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

 
     .simple_square_btn4 {
	display: block;
	position: relative;
	width: 160px;
	padding: 0.1em;
	text-align: center;
	text-decoration: none;
	color: #1B1B1B;
	background: #fff;
	border-radius: 30px;
	border:1px solid #1B1B1B;
	-webkit-backface-visibility: hidden; 
	-moz-backface-visibility: hidden;
    backface-visibility: hidden;
}


.simple_square_btn4:hover {
	 cursor: pointer;
	 text-decoration: none;
	-webkit-animation: simple_square_btn4 0.4s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
	-moz-animation: simple_square_btn4 0.4s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
	        animation: simple_square_btn4 0.4s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
}
@-webkit-keyframes simple_square_btn4{
  0% {
    -webkit-transform: scale(1);
            transform: scale(1);
  }
  100% {
    -webkit-transform: scale(0.85);
            transform: scale(0.85);
  }
}
@-moz-keyframes simple_square_btn4{
  0% {
    -webkit-transform: scale(1);
            transform: scale(1);
  }
  100% {
    -webkit-transform: scale(0.85);
            transform: scale(0.85);
  }
}
@keyframes simple_square_btn4 {
  0% {
    -webkit-transform: scale(1);
            transform: scale(1);
  }
  100% {
    -webkit-transform: scale(0.85);
            transform: scale(0.85);
  }
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

<td width="600" border="1" >

<font size="6"  >
マスタ・委託案件登録ページ
</font>

</td>


<font size="4"  >

<td width="150">

<section>
<a href="anken_booking01.aspx?id={0}" class="simple_square_btn4">当日案件へ</a>
</section>


</td>

<td width="150">


<section>
<a href="anken_booking02.aspx?id={0}" class="simple_square_btn4">案件抽出</a>
</section>


</td>

</font>

</tr>

</table>





<table class ="design02" >

<tr>

<td>

<font size="3"  >

<h2 style="text-align:left">1.IVNOで委託登録と削除&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</h2>
</font>

</td>



<td>

<font size="3"  >




<asp:TextBox ID="TextBox1" runat="server"  Width="150px" Height="35px" AutoPostBack="True" Font-Size="13px" MaxLength="4">Input</asp:TextBox>
<asp:Button class="btn-radius-gradient-wrap"  ID="Button1" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
<asp:DropDownList ID="DropDownList1" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="ITK_INVNO" DataValueField="ITK_INVNO" AppendDataBoundItems="true">

<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>



<asp:Button class="btn-radius-gradient-wrap"  ID="Button2" runat="server" Text="削除" Width="100" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
   
</font>
</td>







</tr>


<tr>

<td>

<font size="3"  >
<h2 style="text-align:left">2.客先コードで委託登録と削除   :</h2>

</font>

</td>



<td>


<font size="3"  >

        <asp:DropDownList ID="DropDownList4" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource4" DataTextField="C" DataValueField="C" AppendDataBoundItems="true">

<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>

<%--<asp:TextBox ID="TextBox2" runat="server"  Width="150px" Height="35px" AutoPostBack="True" Font-Size="13px" MaxLength="4">Input</asp:TextBox>--%>
<asp:Button class="btn-radius-gradient-wrap"  ID="Button3" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
<asp:DropDownList ID="DropDownList2" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="CUST_CD" DataValueField="CUST_CD" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>



<asp:Button class="btn-radius-gradient-wrap"  ID="Button4" runat="server" Text="削除" Width="100" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>

</font>
</td>
</tr>



<tr>

<td>

<font size="3"  >

<h2 style="text-align:left">3.海貨業者で委託登録と削除&nbsp;&nbsp;&nbsp;&nbsp;:</h2>
</font>

</td>



<td>


<font size="3"  >


    <asp:DropDownList ID="DropDownList5" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource5" DataTextField="FORWARDER02" DataValueField="FORWARDER02" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>

<%--    <asp:TextBox ID="TextBox3" runat="server"  Width="150px" Height="35px" AutoPostBack="True" Font-Size="13px" MaxLength="20">Input</asp:TextBox>--%>
    <asp:Button class="btn-radius-gradient-wrap"  ID="Button5" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
   

<asp:DropDownList ID="DropDownList3" runat="server" Width="150px" Height="40px"  CssClass="ddl" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="FORWARDER" DataValueField="FORWARDER" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>



<asp:Button class="btn-radius-gradient-wrap"  ID="Button6" runat="server" Text="削除" Width="100" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
   

</font>
</tr>
</table>

</div>

    
        <!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop">
         </font><a href="#">↑</a></p>   



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [ITK_INVNO] FROM [T_EXL_CSWORKSTATUS] WHERE ITK_INVNO <>'' AND ITK_REGDATE > GETDATE()-60 "></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CUST_CD] FROM [T_EXL_ITAKU] WHERE CUST_CD <>'' "></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [FORWARDER] FROM [T_EXL_ITAKU] WHERE FORWARDER <>'' "></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT left([CUST_CD],4) AS C FROM [T_BOOKING]"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [FORWARDER02] FROM [T_EXL_CSANKEN]"></asp:SqlDataSource>
 

    </form>

</body>



</html>
