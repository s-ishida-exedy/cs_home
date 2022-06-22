<%@ Page Language="VB" AutoEventWireup="false" CodeFile="shelf_manage.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(書庫管理)</title>
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
          height: 200px;
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

            .flex {
      display: flex;
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

    <script type="text/javascript">
      function LinkClick() {
          var url = 'shelf_list.aspx?q='
          var result = confirm('別ウインドウで棚の内容を表示します。');
          if (result) {
              document.body.onclick = mess;
              function mess(e) {
                  var o = e ? e.target : event.srcElement;
                  if (o.tagName == 'A')
                      document.getElementById('tana').value = o.innerHTML;
              }
              window.open(url, null);
          }
          else {
          }
      }
    </script>

    <script>
        $(document).ready(function () {
            let ele = document.getElementById('tana');
            const displayOriginal = ele.style.display;
            ele.style.display = 'none';
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
                <h2>書庫状況</h2> 
            </td>
            <td class="second-cell">
            </td>
            <td class="third-cell">
                <% If Session("Role") = "admin" Or Session("Role") = "csusr" Then %>
                    <%--<a href="./anken_booking.aspx">案件管理表へ</a>--%>
                <% Else %>

                <% End If %>                
            </td>
        </tr>
    </table>

    <table border='1' style="width:600px;height:50px;Font-Size:12px;" class ="sample1">
        <tr>
            <td align="center">
                収容可能数：<asp:Label ID="Label23" runat="server" Text="Label"></asp:Label>
            </td>
            <td align="center">
                 保管箱数：<asp:Label ID="Label24" runat="server" Text="Label"></asp:Label>
            </td>
            <td align="center">
                 保管可能残数：<asp:Label ID="Label35" runat="server" Text="Label"></asp:Label>
            </td>
            <td align="center">
                 期限切箱数：<asp:Label ID="Label34" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width:100px;" align="center">
                <div class="button04">
                    <a href="shelf_sagekeep_doc.aspx?id={0}">保管リストへ</a>
                </div>
            </td>


        </tr>
    </table>



                    <asp:TextBox ID="tana" runat="server"></asp:TextBox>

    <table align="center" border='0' style="width:1300px;Font-Size:10.5px;" class ="sample1">

            <tr >
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td style="width:20px;"><asp:Label ID="Label181" runat="server" Text="棚番"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label182" runat="server" Text="収容</br>可数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label183" runat="server" Text="収容数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label166" runat="server" Text="期限</br>切数"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td style="width:20px;"><asp:Label ID="Label171" runat="server" Text="棚番"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label172" runat="server" Text="収容</br>可数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label173" runat="server" Text="収容数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label174" runat="server" Text="期限</br>切数"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td style="width:20px;"><asp:Label ID="Label175" runat="server" Text="棚番"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label176" runat="server" Text="収容</br>可数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label177" runat="server" Text="収容数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label178" runat="server" Text="期限</br>切数"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td style="width:20px;"><asp:Label ID="Label179" runat="server" Text="棚番"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label180" runat="server" Text="収容</br>可数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label184" runat="server" Text="収容数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label185" runat="server" Text="期限</br>切数"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td style="width:20px;"><asp:Label ID="Label186" runat="server" Text="棚番"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label187" runat="server" Text="収容</br>可数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label188" runat="server" Text="収容数"></asp:Label></td>
                                <td style="width:20px;"><asp:Label ID="Label189" runat="server" Text="期限</br>切数"></asp:Label></td>
                            </tr>
                    </table>
                </td>
            </tr>



            <tr >
                <td style="width:20px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="A01" style="width:20px;"><asp:Label ID="Label1" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0001</a></asp:Label></td>
                                <td id="A02" style="width:20px;"><asp:Label ID="Label2" runat="server" Text="A"></asp:Label></td>
                                <td id="A03" style="width:20px;"><asp:Label ID="Label3" runat="server" Text="A"></asp:Label></td>
                                <td id="A04" style="width:20px;"><asp:Label ID="Label190" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="A05" style="width:20px;"><asp:Label ID="Label4" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0002</a></asp:Label></td>
                                <td id="A06" style="width:20px;"><asp:Label ID="Label5" runat="server" Text="A"></asp:Label></td>
                                <td id="A07" style="width:20px;"><asp:Label ID="Label6" runat="server" Text="A"></asp:Label></td>
                                <td id="A08" style="width:20px;"><asp:Label ID="Label191" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="A09" style="width:20px;"><asp:Label ID="Label7" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0003</a></asp:Label></td>
                                <td id="A10" style="width:20px;"><asp:Label ID="Label8" runat="server" Text="A"></asp:Label></td>
                                <td id="A11" style="width:20px;"><asp:Label ID="Label9" runat="server" Text="A"></asp:Label></td>
                                <td id="A12" style="width:20px;"><asp:Label ID="Label192" runat="server" Text="A"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="A13" style="width:20px;"><asp:Label ID="Label13" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0008</a></asp:Label></td>
                                <td id="A14" style="width:20px;"><asp:Label ID="Label14" runat="server" Text="A"></asp:Label></td>
                                <td id="A15" style="width:20px;"><asp:Label ID="Label15" runat="server" Text="A"></asp:Label></td>
                                <td id="A16" style="width:20px;"><asp:Label ID="Label194" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="A17" style="width:20px;"><asp:Label ID="Label16" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0009</a></asp:Label></td>
                                <td id="A18" style="width:20px;"><asp:Label ID="Label17" runat="server" Text="A"></asp:Label></td>
                                <td id="A19" style="width:20px;"><asp:Label ID="Label18" runat="server" Text="A"></asp:Label></td>
                                <td id="A20" style="width:20px;"><asp:Label ID="Label195" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="B01" style="width:20px;"><asp:Label ID="Label19" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0010</a></asp:Label></td>
                                <td id="B02" style="width:20px;"><asp:Label ID="Label20" runat="server" Text="A"></asp:Label></td>
                                <td id="B03" style="width:20px;"><asp:Label ID="Label21" runat="server" Text="A"></asp:Label></td>
                                <td id="B04" style="width:20px;"><asp:Label ID="Label196" runat="server" Text="A"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="B09" style="width:20px;"><asp:Label ID="Label25" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0008</a></asp:Label></td>
                                <td id="B10" style="width:20px;"><asp:Label ID="Label26" runat="server" Text="00"></asp:Label></td>
                                <td id="B11" style="width:20px;"><asp:Label ID="Label27" runat="server" Text="0"></asp:Label></td>
                                <td id="B12" style="width:20px;"><asp:Label ID="Label198" runat="server" Text="0"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="B13" style="width:20px;"><asp:Label ID="Label28" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0009</a></asp:Label></td>
                                <td id="B14" style="width:20px;"><asp:Label ID="Label29" runat="server" Text="A"></asp:Label></td>
                                <td id="B15" style="width:20px;"><asp:Label ID="Label30" runat="server" Text="A"></asp:Label></td>
                                <td id="B16" style="width:20px;"><asp:Label ID="Label199" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="B17" style="width:20px;"><asp:Label ID="Label31" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0010</a></asp:Label></td>
                                <td id="B18" style="width:20px;"><asp:Label ID="Label32" runat="server" Text="A"></asp:Label></td>
                                <td id="B19" style="width:20px;"><asp:Label ID="Label33" runat="server" Text="A"></asp:Label></td>
                                <td id="B20" style="width:20px;"><asp:Label ID="Label200" runat="server" Text="A"></asp:Label></td>
                            </tr>

                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="C01"style="width:20px;"><asp:Label ID="Label37" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0011</a></asp:Label></td>
                                <td id="C02"style="width:20px;"><asp:Label ID="Label38" runat="server" Text="A"></asp:Label></td>
                                <td id="C03"style="width:20px;"><asp:Label ID="Label39" runat="server" Text="A"></asp:Label></td>
                                <td id="C04"style="width:20px;"><asp:Label ID="Label202" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="C05"style="width:20px;"><asp:Label ID="Label40" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0012</a></asp:Label></td>
                                <td id="C06"style="width:20px;"><asp:Label ID="Label41" runat="server" Text="A"></asp:Label></td>
                                <td id="C07"style="width:20px;"><asp:Label ID="Label42" runat="server" Text="A"></asp:Label></td>
                                <td id="C08"style="width:20px;"><asp:Label ID="Label203" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="C09"style="width:20px;"><asp:Label ID="Label43" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0013</a></asp:Label></td>
                                <td id="C10"style="width:20px;"><asp:Label ID="Label44" runat="server" Text="A"></asp:Label></td>
                                <td id="C11"style="width:20px;"><asp:Label ID="Label45" runat="server" Text="A"></asp:Label></td>
                                <td id="C12"style="width:20px;"><asp:Label ID="Label204" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="C13"style="width:20px;"><asp:Label ID="Label46" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0014</a></asp:Label></td>
                                <td id="C14"style="width:20px;"><asp:Label ID="Label47" runat="server" Text="A"></asp:Label></td>
                                <td id="C15"style="width:20px;"><asp:Label ID="Label48" runat="server" Text="A"></asp:Label></td>
                                <td id="C16"style="width:20px;"><asp:Label ID="Label205" runat="server" Text="A"></asp:Label></td>
                            </tr>
                    </table>
                </td>
            </tr>


            <tr >
                <td style="width:20px;">
                   <table  border='1'>
                            <tr >
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="D13" style="width:20px;"><asp:Label ID="Label61" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0004</a></asp:Label></td>
                                <td id="D14" style="width:20px;"><asp:Label ID="Label62" runat="server" Text="A"></asp:Label></td>
                                <td id="D15" style="width:20px;"><asp:Label ID="Label63" runat="server" Text="A"></asp:Label></td>
                                <td id="D16" style="width:20px;"><asp:Label ID="Label210" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="D17" style="width:20px;"><asp:Label ID="Label64" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0005</a></asp:Label></td>
                                <td id="D18" style="width:20px;"><asp:Label ID="Label65" runat="server" Text="A"></asp:Label></td>
                                <td id="D19" style="width:20px;"><asp:Label ID="Label66" runat="server" Text="A"></asp:Label></td>
                                <td id="D20" style="width:20px;"><asp:Label ID="Label211" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="E01" style="width:20px;"><asp:Label ID="Label67" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0006</a></asp:Label></td>
                                <td id="E02" style="width:20px;"><asp:Label ID="Label68" runat="server" Text="A"></asp:Label></td>
                                <td id="E03" style="width:20px;"><asp:Label ID="Label69" runat="server" Text="A"></asp:Label></td>
                                <td id="E04" style="width:20px;"><asp:Label ID="Label212" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="E05" style="width:20px;"><asp:Label ID="Label70" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0007</a></asp:Label></td>
                                <td id="E06" style="width:20px;"><asp:Label ID="Label71" runat="server" Text="A"></asp:Label></td>
                                <td id="E07" style="width:20px;"><asp:Label ID="Label72" runat="server" Text="A"></asp:Label></td>
                                <td id="E08" style="width:20px;"><asp:Label ID="Label213" runat="server" Text="A"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="E09" style="width:20px;"><asp:Label ID="Label73" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0011</a></asp:Label></td>
                                <td id="E10" style="width:20px;"><asp:Label ID="Label74" runat="server" Text="A"></asp:Label></td>
                                <td id="E11" style="width:20px;"><asp:Label ID="Label75" runat="server" Text="A"></asp:Label></td>
                                <td id="E12" style="width:20px;"><asp:Label ID="Label214" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="E17" style="width:20px;"><asp:Label ID="Label79" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0012</a></asp:Label></td>
                                <td id="E18" style="width:20px;"><asp:Label ID="Label80" runat="server" Text="A"></asp:Label></td>
                                <td id="E19" style="width:20px;"><asp:Label ID="Label81" runat="server" Text="A"></asp:Label></td>
                                <td id="E20" style="width:20px;"><asp:Label ID="Label216" runat="server" Text="A"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="F05" style="width:20px;"><asp:Label ID="Label85" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0005</a></asp:Label></td>
                                <td id="F06" style="width:20px;"><asp:Label ID="Label86" runat="server" Text="A"></asp:Label></td>
                                <td id="F07" style="width:20px;"><asp:Label ID="Label87" runat="server" Text="A"></asp:Label></td>
                                <td id="F08" style="width:20px;"><asp:Label ID="Label218" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >  
                                <td id="F09" style="width:20px;"><asp:Label ID="Label10" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0006</a></asp:Label></td>
                                <td id="F10" style="width:20px;"><asp:Label ID="Label11" runat="server" Text="A"></asp:Label></td>
                                <td id="F11" style="width:20px;"><asp:Label ID="Label12" runat="server" Text="A"></asp:Label></td>
                                <td id="F12" style="width:20px;"><asp:Label ID="Label22" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="F13" style="width:20px;"><asp:Label ID="Label91" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0007</a></asp:Label></td>
                                <td id="F14" style="width:20px;"><asp:Label ID="Label92" runat="server" Text="A"></asp:Label></td>
                                <td id="F15" style="width:20px;"><asp:Label ID="Label93" runat="server" Text="A"></asp:Label></td>
                                <td id="F16" style="width:20px;"><asp:Label ID="Label220" runat="server" Text="A"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="G01" style="width:20px;"><asp:Label ID="Label97" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0015</a></asp:Label></td>
                                <td id="G02" style="width:20px;"><asp:Label ID="Label98" runat="server" Text="A"></asp:Label></td>
                                <td id="G03" style="width:20px;"><asp:Label ID="Label99" runat="server" Text="A"></asp:Label></td>
                                <td id="G04" style="width:20px;"><asp:Label ID="Label222" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="G05" style="width:20px;"><asp:Label ID="Label100" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0016</a></asp:Label></td>
                                <td id="G06" style="width:20px;"><asp:Label ID="Label101" runat="server" Text="A"></asp:Label></td>
                                <td id="G07" style="width:20px;"><asp:Label ID="Label102" runat="server" Text="A"></asp:Label></td>
                                <td id="G08" style="width:20px;"><asp:Label ID="Label223" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="G09" style="width:20px;"><asp:Label ID="Label103" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0017</a></asp:Label></td>
                                <td id="G10" style="width:20px;"><asp:Label ID="Label104" runat="server" Text="A"></asp:Label></td>
                                <td id="G11" style="width:20px;"><asp:Label ID="Label105" runat="server" Text="A"></asp:Label></td>
                                <td id="G12" style="width:20px;"><asp:Label ID="Label224" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="G13" style="width:20px;"><asp:Label ID="Label106" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0018</a></asp:Label></td>
                                <td id="G14" style="width:20px;"><asp:Label ID="Label107" runat="server" Text="00"></asp:Label></td>
                                <td id="G15" style="width:20px;"><asp:Label ID="Label108" runat="server" Text="0"></asp:Label></td>
                                <td id="G16" style="width:20px;"><asp:Label ID="Label225" runat="server" Text="0"></asp:Label></td>
                            </tr>
                    </table>
                </td>
            </tr>


            <tr >
                <td style="width:20px;">
                   <table  border='1'>
                            <tr >
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>

                <td style="width:20px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="I09" style="width:20px;"><asp:Label ID="Label133" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0013</a></asp:Label></td>
                                <td id="I10" style="width:20px;"><asp:Label ID="Label134" runat="server" Text="A"></asp:Label></td>
                                <td id="I11" style="width:20px;"><asp:Label ID="Label135" runat="server" Text="A"></asp:Label></td>
                                <td id="I12" style="width:20px;"><asp:Label ID="Label234" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="I17" style="width:20px;"><asp:Label ID="Label139" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">C0014</a></asp:Label></td>
                                <td id="I18" style="width:20px;"><asp:Label ID="Label140" runat="server" Text="A"></asp:Label></td>
                                <td id="I19" style="width:20px;"><asp:Label ID="Label141" runat="server" Text="A"></asp:Label></td>
                                <td id="I20" style="width:20px;"><asp:Label ID="Label236" runat="server" Text="A"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="J05" style="width:20px;"><asp:Label ID="Label145" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0001</a></asp:Label></td>
                                <td id="J06" style="width:20px;"><asp:Label ID="Label146" runat="server" Text="00"></asp:Label></td>
                                <td id="J07" style="width:20px;"><asp:Label ID="Label147" runat="server" Text="0"></asp:Label></td>
                                <td id="J08" style="width:20px;"><asp:Label ID="Label238" runat="server" Text="0"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="J09" style="width:20px;"><asp:Label ID="Label148" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0002</a></asp:Label></td>
                                <td id="J10" style="width:20px;"><asp:Label ID="Label149" runat="server" Text="A"></asp:Label></td>
                                <td id="J11" style="width:20px;"><asp:Label ID="Label150" runat="server" Text="A"></asp:Label></td>
                                <td id="J12" style="width:20px;"><asp:Label ID="Label239" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="J13" style="width:20px;"><asp:Label ID="Label151" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0003</a></asp:Label></td>
                                <td id="J14" style="width:20px;"><asp:Label ID="Label152" runat="server" Text="A"></asp:Label></td>
                                <td id="J15" style="width:20px;"><asp:Label ID="Label153" runat="server" Text="A"></asp:Label></td>
                                <td id="J16" style="width:20px;"><asp:Label ID="Label240" runat="server" Text="A"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="J17" style="width:20px;"><asp:Label ID="Label154" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">D0004</a></asp:Label></td>
                                <td id="J18" style="width:20px;"><asp:Label ID="Label155" runat="server" Text="A"></asp:Label></td>
                                <td id="J19" style="width:20px;"><asp:Label ID="Label156" runat="server" Text="A"></asp:Label></td>
                                <td id="J20" style="width:20px;"><asp:Label ID="Label241" runat="server" Text="A"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
            </tr>

            <tr style="height:50px;">
            </tr>

            <tr >
                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                                <td id="K01" style="width:20px;"><asp:Label ID="Label157" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">A0001</a></asp:Label></td>
                                <td id="K02" style="width:20px;"><asp:Label ID="Label158" runat="server" Text="00"></asp:Label></td>
                                <td id="K03" style="width:20px;"><asp:Label ID="Label159" runat="server" Text="0"></asp:Label></td>
                                <td id="K04" style="width:20px;"><asp:Label ID="Label242" runat="server" Text="0"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="K05" style="width:20px;"><asp:Label ID="Label160" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">A0002</a></asp:Label></td>
                                <td id="K06" style="width:20px;"><asp:Label ID="Label161" runat="server" Text="10"></asp:Label></td>
                                <td id="K07" style="width:20px;"><asp:Label ID="Label162" runat="server" Text="1"></asp:Label></td>
                                <td id="K08" style="width:20px;"><asp:Label ID="Label243" runat="server" Text="1"></asp:Label></td>
                            </tr>
                            <tr >
                                <td id="K09" style="width:20px;"><asp:Label ID="Label163" runat="server" Text=""><a href="javascript:void(0);" onclick="LinkClick()">A0003</a></asp:Label></td>
                                <td id="K10" style="width:20px;"><asp:Label ID="Label164" runat="server" Text="10"></asp:Label></td>
                                <td id="K11" style="width:20px;"><asp:Label ID="Label165" runat="server" Text="1"></asp:Label></td>
                                <td id="K12" style="width:20px;"><asp:Label ID="Label244" runat="server" Text="1"></asp:Label></td>
                            </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>

                <td style="width:20px;">
                    <table  border='1'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:1px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
                <td style="width:20px;">
                    <table  border='0'>
                            <tr >
                             </tr>
                    </table>
                </td>
            </tr>
    </table>

</div>

<script>

    $(document).ready(function () {


        if (document.getElementById("A03").innerText / document.getElementById("A02").innerText == 1) {
            document.getElementById("A01").style.backgroundColor = 'red';
        } else if (document.getElementById("A03").innerText / document.getElementById("A02").innerText > 0.5) {
            document.getElementById('A01').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("A07").innerText / document.getElementById("A06").innerText == 1) {
            document.getElementById("A05").style.backgroundColor = 'red';
        } else if (document.getElementById("A07").innerText / document.getElementById("A06").innerText > 0.5) {
            document.getElementById('A05').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("A11").innerText/document.getElementById("A10").innerText == 1) {
            document.getElementById("A09").style.backgroundColor = 'red';
        } else if (document.getElementById("A11").innerText / document.getElementById("A10").innerText > 0.5) {
            document.getElementById('A09').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("D15").innerText/document.getElementById("D14").innerText == 1) {
            document.getElementById("D13").style.backgroundColor = 'red';
        } else if (document.getElementById("D15").innerText / document.getElementById("D14").innerText > 0.5) {
            document.getElementById('D13').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("D19").innerText/document.getElementById("D18").innerText == 1) {
            document.getElementById("D17").style.backgroundColor = 'red';
        } else if (document.getElementById("D19").innerText / document.getElementById("D18").innerText > 0.5) {
            document.getElementById('D17').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("E03").innerText/document.getElementById("E02").innerText == 1) {
            document.getElementById("E01").style.backgroundColor = 'red';
        } else if (document.getElementById("E03").innerText / document.getElementById("E02").innerText > 0.5) {
            document.getElementById('E01').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("E07").innerText/document.getElementById("E06").innerText == 1) {
            document.getElementById("E05").style.backgroundColor = 'red';
        } else if (document.getElementById("E07").innerText / document.getElementById("E06").innerText > 0.5) {
            document.getElementById('E05').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("A15").innerText/document.getElementById("A14").innerText == 1) {
            document.getElementById("A13").style.backgroundColor = 'red';
        } else if (document.getElementById("A15").innerText / document.getElementById("A14").innerText >0.5) {
            document.getElementById('A13').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("A19").innerText/document.getElementById("A18").innerText == 1) {
            document.getElementById("A17").style.backgroundColor = 'red';
        } else if (document.getElementById("A19").innerText / document.getElementById("A18").innerText >0.5) {
            document.getElementById('A17').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("B03").innerText/document.getElementById("B02").innerText == 1) {
            document.getElementById("B01").style.backgroundColor = 'red';
        } else if (document.getElementById("B03").innerText / document.getElementById("B02").innerText >0.5) {
            document.getElementById('B01').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("E11").innerText/document.getElementById("E10").innerText == 1) {
            document.getElementById("E09").style.backgroundColor = 'red';
        } else if (document.getElementById("E11").innerText / document.getElementById("E10").innerText >0.5) {
            document.getElementById('E09').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("E19").innerText/document.getElementById("E18").innerText == 1) {
            document.getElementById("E17").style.backgroundColor = 'red';
        } else if (document.getElementById("E19").innerText / document.getElementById("E18").innerText >0.5) {
            document.getElementById('E17').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("I11").innerText/document.getElementById("I10").innerText == 1) {
            document.getElementById("I09").style.backgroundColor = 'red';
        } else if (document.getElementById("I11").innerText / document.getElementById("I10").innerText >0.5) {
            document.getElementById('I09').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("I19").innerText/document.getElementById("I18").innerText == 1) {
            document.getElementById("I17").style.backgroundColor = 'red';
        } else if (document.getElementById("I19").innerText / document.getElementById("I18").innerText >0.5) {
            document.getElementById('I17').style.backgroundColor = 'yellow';
        };

        //if (document.getElementById("B11").innerText/document.getElementById("B10").innerText == 1) {
        //    document.getElementById("B09").style.backgroundColor = 'red';
        //} else if (document.getElementById("B11").innerText / document.getElementById("B10").innerText >0.5) {
        //    document.getElementById('B09').style.backgroundColor = 'yellow';
        //};

        if (document.getElementById("B15").innerText/document.getElementById("B14").innerText == 1) {
            document.getElementById("B13").style.backgroundColor = 'red';
        } else if (document.getElementById("B15").innerText / document.getElementById("B14").innerText >0.5) {
            document.getElementById('B13').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("B19").innerText/document.getElementById("B18").innerText == 1) {
            document.getElementById("B17").style.backgroundColor = 'red';
        } else if (document.getElementById("B19").innerText / document.getElementById("B18").innerText >0.5) {
            document.getElementById('B17').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("F07").innerText/document.getElementById("F06").innerText == 1) {
            document.getElementById("F05").style.backgroundColor = 'red';
        } else if (document.getElementById("F07").innerText / document.getElementById("F06").innerText >0.5) {
            document.getElementById('F05').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("F15").innerText/document.getElementById("F14").innerText == 1) {
            document.getElementById("F13").style.backgroundColor = 'red';
        } else if (document.getElementById("F15").innerText / document.getElementById("F14").innerText >0.5) {
            document.getElementById('F13').style.backgroundColor = 'yellow';
        };

        //if (document.getElementById("J07").innerText/document.getElementById("J06").innerText == 1) {
        //    document.getElementById("J05").style.backgroundColor = 'red';
        //} else if (document.getElementById("J07").innerText / document.getElementById("J06").innerText >0.5) {
        //    document.getElementById('J05').style.backgroundColor = 'yellow';
        //};

        if (document.getElementById("J11").innerText/document.getElementById("J10").innerText == 1) {
            document.getElementById("J09").style.backgroundColor = 'red';
        } else if (document.getElementById("J11").innerText / document.getElementById("J10").innerText >0.5) {
            document.getElementById('J09').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("J15").innerText/document.getElementById("J14").innerText == 1) {
            document.getElementById("J13").style.backgroundColor = 'red';
        } else if (document.getElementById("J15").innerText / document.getElementById("J14").innerText >0.5) {
            document.getElementById('J13').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("J19").innerText/document.getElementById("J18").innerText == 1) {
            document.getElementById("J17").style.backgroundColor = 'red';
        } else if (document.getElementById("J19").innerText / document.getElementById("J18").innerText >0.5){
            document.getElementById('J17').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("C03").innerText/document.getElementById("C02").innerText == 1) {
            document.getElementById("C01").style.backgroundColor = 'red';
        } else if (document.getElementById("C03").innerText / document.getElementById("C02").innerText >0.5){
            document.getElementById('C01').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("C07").innerText/document.getElementById("C06").innerText == 1) {
            document.getElementById("C05").style.backgroundColor = 'red';
        } else if (document.getElementById("C07").innerText / document.getElementById("C06").innerText >0.5) {
            document.getElementById('C05').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("C11").innerText/document.getElementById("C10").innerText == 1) {
            document.getElementById("C09").style.backgroundColor = 'red';
        } else if (document.getElementById("C11").innerText / document.getElementById("C10").innerText >0.5) {
            document.getElementById('C09').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("C15").innerText/document.getElementById("C14").innerText == 1) {
            document.getElementById("C13").style.backgroundColor = 'red';
        } else if (document.getElementById("C15").innerText / document.getElementById("C14").innerText >0.5) {
            document.getElementById('C13').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("G03").innerText/document.getElementById("G02").innerText == 1) {
            document.getElementById("G01").style.backgroundColor = 'red';
        } else if (document.getElementById("G03").innerText / document.getElementById("G02").innerText >0.5) {
            document.getElementById('G01').style.backgroundColor = 'yellow';
        };

        if (document.getElementById("G07").innerText/document.getElementById("G06").innerText == 1) {
            document.getElementById("G05").style.backgroundColor = 'red';
        } else if (document.getElementById("G07").innerText / document.getElementById("G06").innerText >0.5) {
            document.getElementById('G05').style.backgroundColor = 'yellow';
        };


        if (document.getElementById("G11").innerText/document.getElementById("G10").innerText == 1) {
            document.getElementById("G09").style.backgroundColor = 'red';
        } else if (document.getElementById("G11").innerText / document.getElementById("G10").innerText >0.5) {
            document.getElementById('G09').style.backgroundColor = 'yellow';
        };

        //if (document.getElementById("G15").innerText/document.getElementById("G14").innerText == 1) {
        //    document.getElementById("G13").style.backgroundColor = 'red';
        //} else if (document.getElementById("G15").innerText / document.getElementById("G14").innerText >0.5) {
        //    document.getElementById('G13').style.backgroundColor = 'yellow';
        //};

        if (document.getElementById("F11").innerText / document.getElementById("F10").innerText == 1) {
            document.getElementById("F09").style.backgroundColor = 'red';
        } else if (document.getElementById("F11").innerText / document.getElementById("F10").innerText > 0.5) {
            document.getElementById('F09').style.backgroundColor = 'yellow';
        };

        //if (document.getElementById("H15").innerText/document.getElementById("H14").innerText == 1) {
        //    document.getElementById("H13").style.backgroundColor = 'red';
        //} else if (document.getElementById("H15").innerText / document.getElementById("H14").innerText >0.5) {
        //    document.getElementById('H13').style.backgroundColor = 'yellow';
        //};


        //if (document.getElementById("H19").innerText / document.getElementById("H18").innerText == 1) {
        //    document.getElementById("H17").style.backgroundColor = 'red';
        //} else if (document.getElementById("H19").innerText / document.getElementById("H18").innerText > 0.5) {
        //    document.getElementById('H17').style.backgroundColor = 'yellow';
        //};


        //if (document.getElementById("I03").innerText/document.getElementById("I02").innerText == 1) {
        //    document.getElementById("I01").style.backgroundColor = 'red';
        //} else if (document.getElementById("I03").innerText / document.getElementById("I02").innerText >0.5) {
        //    document.getElementById('I01').style.backgroundColor = 'yellow';
        //};


        //if (document.getElementById("C19").innerText/document.getElementById("C18").innerText == 1) {
        //    document.getElementById("C17").style.backgroundColor = 'red';
        //} else if (document.getElementById("C19").innerText / document.getElementById("C18").innerText >0.5) {
        //    document.getElementById('C17').style.backgroundColor = 'yellow';
        //};


        //if (document.getElementById("D03").innerText/document.getElementById("D02").innerText == 1) {
        //    document.getElementById("D01").style.backgroundColor = 'red';
        //} else if (document.getElementById("D03").innerText / document.getElementById("D02").innerText >0.5) {
        //    document.getElementById('D01').style.backgroundColor = 'yellow';
        //};

        //if (document.getElementById("D07").innerText/document.getElementById("D06").innerText == 1) {
        //    document.getElementById("D05").style.backgroundColor = 'red';
        //} else if (document.getElementById("D07").innerText / document.getElementById("D06").innerText >0.5) {
        //    document.getElementById('D05').style.backgroundColor = 'yellow';
        //};

        //if (document.getElementById("D11").innerText/document.getElementById("D10").innerText == 1) {
        //    document.getElementById("D09").style.backgroundColor = 'red';
        //} else if (document.getElementById("D11").innerText / document.getElementById("D10").innerText >0.5) {
        //    document.getElementById('D09').style.backgroundColor = 'yellow';
        //};

        //if (document.getElementById("G19").innerText/document.getElementById("G18").innerText == 1) {
        //    document.getElementById("G17").style.backgroundColor = 'red';
        //} else if (document.getElementById("G19").innerText / document.getElementById("G18").innerText >0.5) {
        //    document.getElementById('G17').style.backgroundColor = 'yellow';
        //};

        //if (document.getElementById("H03").innerText/document.getElementById("H02").innerText == 1) {
        //    document.getElementById("H01").style.backgroundColor = 'red';
        //} else if (document.getElementById("H03").innerText / document.getElementById("H02").innerText >0.5) {
        //    document.getElementById('H01').style.backgroundColor = 'yellow';
        //};

        //if (document.getElementById("H07").innerText/document.getElementById("H06").innerText == 1) {
        //    document.getElementById("H05").style.backgroundColor = 'red';
        //} else if (document.getElementById("H07").innerText / document.getElementById("H06").innerText >0.5) {
        //    document.getElementById('H05').style.backgroundColor = 'yellow';
        //};

        //if (document.getElementById("H11").innerText/document.getElementById("H10").innerText == 1) {
        //    document.getElementById("H09").style.backgroundColor = 'red';
        //} else if (document.getElementById("H11").innerText / document.getElementById("H10").innerText >0.5) {
        //    document.getElementById('H09').style.backgroundColor = 'yellow';
        //};


        //if (document.getElementById("K02").innerText == document.getElementById("K03").innerText) {
        //    document.getElementById("K01").style.backgroundColor = 'red';
        //} else {
        //    //document.getElementById('A1').style.backgroundColor = 'blue';
        //};

        if (document.getElementById("K07").innerText/document.getElementById("K06").innerText == 1) {
            document.getElementById("K05").style.backgroundColor = 'red';
        } else if (document.getElementById("K07").innerText / document.getElementById("K06").innerText >0.5) {
            document.getElementById("K05").style.backgroundColor = 'yellow';
        };

        if (document.getElementById("K11").innerText/document.getElementById("K10").innerText == 1) {
            document.getElementById("K09").style.backgroundColor = 'red';
        } else if (document.getElementById("K11").innerText/document.getElementById("K10").innerText >0.5) {
            document.getElementById("K09").style.backgroundColor = 'yellow';
        };

        document.getElementById("B09").style.backgroundColor = 'blue';
        document.getElementById("J05").style.backgroundColor = 'blue';
        document.getElementById("C13").style.backgroundColor = 'blue';
        document.getElementById("G13").style.backgroundColor = 'blue';
        document.getElementById("K01").style.backgroundColor = 'blue';

    });

</script>


    <div id="contents2" class="inner2">

        <asp:Label ID="Label36" runat="server" Text="●廃棄スケジュール"></asp:Label>
        <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">
            <div class="wrapper">
                <table class="sticky">
                    <thead class="fixed">
                    </thead>

                    <tbody>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="200px" Height="100px" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px">
                        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
                        <HeaderStyle CssClass="Freezing"></HeaderStyle>

                        <Columns>

                        <asp:BoundField DataField="SHELF_LIFE" HeaderText="廃棄期限" SortExpression="廃棄期限" >
                        <HeaderStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SHELF_LIFEのカウント" HeaderText="箱数" SortExpression="箱数" >
                        <HeaderStyle Width="10px" />
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

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT T_EXL_DOC_BOX.SHELF_LIFE, Count(T_EXL_DOC_BOX.SHELF_LIFE) AS SHELF_LIFEのカウント FROM T_EXL_DOC_BOX GROUP BY T_EXL_DOC_BOX.SHELF_LIFE ORDER BY T_EXL_DOC_BOX.SHELF_LIFE"></asp:SqlDataSource>


    

    
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop">
<a href="#">↑</a></p>   



</form>
</body>


</html>
