<%@ Page Language="VB" AutoEventWireup="false" CodeFile="register_csstatus.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(マスタ変更_委託案件登録)</title>
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



.button018 a {
    position: relative;
    display: flex;
    justify-content: space-around;
    align-items: center;
    margin: 0 auto;
    max-width: 225px;
    padding: 10px 0px 10px 25px;
    color: #313131;
    transition: 0.3s ease-in-out;
    font-weight: 500;
}

.button018 a:before, .button018 a:after {
  content: "";
  position: absolute;
  display: block;
  top: 50%;
}
.button018 a:before {
  width: 0.5rem;
  height: 0.5rem;
  left: 1.1rem;
  border-top: solid 2px #fff;
  border-right: solid 2px #fff;
  z-index: 2;
  transform: translateY(-50%) rotate(45deg);
  transition: all 0.3s;
}
.button018 a:after {
  left: 0;
  background: #6bb6ff;
  z-index: 1;
  width: 3rem;
  height: 3rem;
  border-radius: 4rem;
  transform: translateY(-50%);
  transition: all 0.5s;
}
.button018 a span {
  position: relative;
  transition: all 0.3s;
  z-index: 3;
}

.button018 a:hover span {
  color: #fff;
}
.button018 a:hover:before {
  left: 2rem;
}
.button018 a:hover:after {
  right: 0;
  width: 100%;
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


        .auto-style1 {
            width: 200px;
        }


        .btn-radius-gradient-wrap{

            cursor : pointer;


        }

        /*.DropDown
{

                        cursor : pointer;
}*/


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


<table >

<tr>



<td style="width:250px;Font-Size:25px;" >

<h2>マスタ・委託案件登録ページ</h2>

</td>





</tr>

</table>

<table>

<tr>

<td>


<asp:Label ID="Label1" runat="server" Text="＜委託マスタのメンテナンス＞"></asp:Label>

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


<asp:DropDownList ID="DropDownList6" runat="server" Width="150px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource7" DataTextField="C" DataValueField="C" AppendDataBoundItems="true">

<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>

<asp:Button  ID="Button1" runat="server" CssClass ="" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>



</td>

<td style="width:200px;Font-Size:15px;" >

<asp:DropDownList ID="DropDownList1" runat="server" Width="150px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="INVNO" DataValueField="INVNO" AppendDataBoundItems="true">

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

<asp:DropDownList ID="DropDownList4" runat="server" Width="150px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource6" DataTextField="C" DataValueField="C" AppendDataBoundItems="true">

<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>
<asp:Button class="btn-radius-gradient-wrap"  ID="Button3" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>

</td>

<td style="width:200px;Font-Size:15px;" >

<asp:DropDownList ID="DropDownList2" runat="server" Width="150px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="CUST_CD" DataValueField="CUST_CD" AppendDataBoundItems="true" >
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


<asp:DropDownList ID="DropDownList5" runat="server" Width="150px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource5" DataTextField="FORWARDER02" DataValueField="FORWARDER02" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>
   
<asp:Button class="btn-radius-gradient-wrap"  ID="Button5" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
   
</td>

<td style="width:200px;Font-Size:15px;" >

<asp:DropDownList ID="DropDownList3" runat="server" Width="150px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="FORWARDER" DataValueField="FORWARDER" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>



<asp:Button class="btn-radius-gradient-wrap"  ID="Button6" runat="server" Text="削除" Width="100" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>
</td>

</tr>


    </table>


<table>

<tr>

<td>


<asp:Label ID="Label2" runat="server" Text="＜その他マスタのメンテナンス＞"></asp:Label>

</td>

</tr>

</table>



    <table class ="design02" >

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

<%--    <table class ="design02" >

<tr >

<td style="width:200px;Font-Size:15px;text-align:left;" >

5.LCL搬入先住所登録&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:

</td>


<td style="Font-Size:15px;" >


<asp:TextBox ID="TextBox1" runat="server" Width="450px" Height="40px"  CssClass="" Font-Size="12px" AutoPostBack="True"  AppendDataBoundItems="true" >

</asp:TextBox>
   
<asp:Button class="btn-radius-gradient-wrap"  ID="Button8" runat="server" Text="登録" Width="100px" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>



</td>

</tr>

<tr >


<td style="width:200px;Font-Size:15px;text-align:left;" >


</td>
    
<td style="Font-Size:15px;" >

<asp:DropDownList ID="DropDownList8" runat="server" Width="450px" Height="40px"  CssClass="DropDown" Font-Size="12px" AutoPostBack="True" DataSourceID="SqlDataSource8" DataTextField="ADDRESS" DataValueField="ADDRESS" AppendDataBoundItems="true" >
<asp:ListItem Text="Please select" Value="" />
</asp:DropDownList>

<asp:Button class="btn-radius-gradient-wrap"  ID="Button9" runat="server" Text="削除" Width="100" Height="40px" AutoPostBack="True" Font-Size="13px" ></asp:Button>



</td>


</tr>

</table>--%>

</div>

    
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop">
<a href="#">↑</a></p>   


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [INVNO] FROM [T_EXL_WORKSTATUS00 ] WHERE INVNO <>'' AND REGDATE  > GETDATE()-60 AND ID='001' ORDER BY [INVNO] "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CUST_CD] FROM [T_EXL_ITAKU] WHERE CUST_CD <>'' ORDER BY CUST_CD "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [FORWARDER] FROM [T_EXL_ITAKU] WHERE FORWARDER <>'' ORDER BY [FORWARDER] "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT left([CUST_CD],4) AS C FROM [T_BOOKING] ORDER BY C" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [FORWARDER02] FROM [T_EXL_CSANKEN] ORDER BY [FORWARDER02]"></asp:SqlDataSource>
 
    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:KBHWPA85ConnectionString %>" SelectCommand="SELECT DISTINCT [CUSTCODE] AS C FROM [T_SN_HD_TB] WHERE [NOKIYMD] > GETDATE()-360 ORDER BY C "></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:KBHWPA85ConnectionString %>" SelectCommand="SELECT DISTINCT [OLD_INVNO] AS C FROM [T_INV_HD_TB]  WHERE [BLDATE] BETWEEN GETDATE()-5 AND GETDATE()+60 ORDER BY C "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT DISTINCT [ADDRESS] FROM [T_EXL_LCLADDRESS] ORDER BY ADDRESS" ></asp:SqlDataSource>



</form>
</body>



</html>
