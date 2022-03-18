<%@ Page Language="VB" AutoEventWireup="false" CodeFile="make_graph_minou02.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(KD未納金額グラフ)</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>



<%--    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.min.js" type="text/javascript"></script>--%>
<%--    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.5.1/chart.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.7.0/chart.min.js"></script>--%>
    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.min.js"></script>

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
          /*text-align: right;*/
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

        .AA {
                  text-align: right;
        }

        .BB {
                  text-align: center;
        }

        .color01 {
        background-color: red;  
        color: white;
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

 <script>
     $(document).ready(function () {

             $.ajax({
                    type: "POST",
                 //url: "getTrafficSourceData",
                    url: "./make_graph_minou02.aspx/getTrafficSourceData",
                    //data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
             });

             function OnSuccess_(response) {
                 var aData = response.d;
                 var dataarr = [];
                 var dataarr2 = [];
                 var dataarr3 = [];
                 var dataarr4 = [];
                 var dataarr5 = [];
                 var dataarr6 = [];
                 var Labelarr = [];
                 var Colorarr = [];
                 $.each(aData, function (inx, val) {
                     dataarr.push(val.value);
                     dataarr2.push(val.value2);
                     dataarr3.push(val.value3);
                     dataarr4.push(val.value4);
                     dataarr5.push(val.value5);
                     dataarr6.push(val.value6);
                     Labelarr.push(val.label);
                     //Colorarr.push(val.color);
                 });
                 var ctx = $("#myChart").get(0).getContext("2d");
                 var config = {
                     type: 'bar',
                     data: {
                         datasets: [{
                             label: '累積',
                             type: "line",
                             fill: false,
                             data: dataarr5,
                             //backgroundColor: 'rgba(255,0,0,0.5)',

                             pointRadius: 0,
                             pointHoverRadius: 0,
                             //pointHoverBackgroundColor: "rgba(75,192,192,1)",
                             //pointHoverBorderColor: "rgba(220,220,220,1)",
                             pointHitRadius: 10,

                             borderColor: 'rgb(255,0,0,0.5)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '未納',
                             data: dataarr,

                             backgroundColor: 'rgba(0, 162, 235, 0.5)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                             //背景色（ホバーしたときに）
                             hoverBackgroundColor: "rgba(51, 51, 255, 0.5)",
                             //枠線の色（ホバーしたときに）
                             hoverBorderColor: "rgba(54, 252, 235, 0.5)",

                         }, {
                             label: '赤処理',
                             data: dataarr2,

                             backgroundColor: 'rgba(255, 165, 0, 0.5)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                             //背景色（ホバーしたときに）
                             hoverBackgroundColor: "rgba(51, 51, 255, 0.5)",
                             //枠線の色（ホバーしたときに）
                             hoverBorderColor: "rgba(54, 252, 235, 0.5)",

                         }, {
                             label: '出港遅延',
                             data: dataarr3,

                             backgroundColor: 'rgba(54, 162, 0, 0.5)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                             //背景色（ホバーしたときに）
                             hoverBackgroundColor: "rgba(51, 51, 255, 0.5)",
                             //枠線の色（ホバーしたときに）
                             hoverBorderColor: "rgba(54, 252, 235, 0.5)",

                         }, {
                             label: '調整納期',
                             data: dataarr4,

                             backgroundColor: 'rgba(0, 0, 235, 0.5)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                             //背景色（ホバーしたときに）
                             hoverBackgroundColor: "rgba(51, 51, 255, 0.5)",
                             //枠線の色（ホバーしたときに）
                             hoverBorderColor: "rgba(54, 252, 235, 0.5)",

                         }],

                         labels: Labelarr

                     },




                     options: {
                         responsive: true,
                         title: {                           // タイトル
                             display: true,                     // 表示設定
                             fontSize: 18,                      // フォントサイズ
                             fontFamily: "sans-serif",
                             text: '未納金額（KD）'                   // タイトルのラベル
                         },
                         scales: {                          // 軸設定
                             xAxes: [                           // Ｘ軸設定
                                 {
                                     stacked: true, //積み上げ棒グラフにする設定
                                     scaleLabel: {                 // 軸ラベル
                                         display: true,                // 表示設定
                                         labelString: '客先',    // ラベル
                                         //fontColor: "red",             // 文字の色
                                         fontSize: 16                  // フォントサイズ
                                     },
                                     gridLines: {                   // 補助線
                                         //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                                     },
                                     ticks: {                      // 目盛り
                                         //fontColor: "red",             // 目盛りの色
                                         fontSize: 14                  // フォントサイズ
                                     },
                                 }
                             ],
                             yAxes: [                           // Ｙ軸設
                   {
                       stacked: true, //積み上げ棒グラフにする設定
                       scaleLabel: {                  // 軸ラベル
                           display: true,                 // 表示の有無
                           labelString: '受注金額（単位：百万円）',     // ラベル
                           fontFamily: "sans-serif",
                           fontColor: "blue",             // 文字の色
                           fontFamily: "sans-serif",
                           fontSize: 16                   // フォントサイズ


                       },

                       gridLines: {                   // 補助線
                           color: "rgba(0, 0, 255, 0.2)", // 補助線の色
                           zeroLineColor: "black"         // y=0（Ｘ軸の色）
                       },
                       ticks: {                       // 目盛り
                           min: 0,                        // 最小値
                           //max: 20,                       // 最大値
                           //stepSize: 5,                   // 軸間隔
                           fontColor: "blue",             // 目盛りの色
                           fontSize: 14                   // フォントサイズ
                       },


                   }
                             ]
                         }
                     }
                 };



                 var myPieChart = new Chart(ctx, config);
             }
             function OnErrorCall_(response) {
                 window.alert('エラーです');
             }
             e.preventDefault();
         });
     //});
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

    <table >
        <tr>
            <td style="width:450px;Font-Size:25px;" >
                <h2>未納金額（KD）</h2>
            </td>
            <asp:Button ID="Button1" runat="server" Text="EXELE出力" Visible="false" />
        </tr>
    </table>

    <table border ="1" class ="BB">
        <tr>
            <td>未納金額：<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> 百万円</td>
            <td>赤処理金額：<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label> 百万円</td>
            <td>出港遅延金額：<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label> 百万円</td>
            <td class="color01">未納金額合計：<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label> 百万円</td>
            <td>調整金額：<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label> 百万円</td>
        </tr>
    </table>

    <table>
        <tr>
            <canvas id="myChart" width="160" height="60"></canvas>
        </tr>
    </table>

    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px" class ="AA">

        <div class="wrapper" id="main2">
        <table class="sticky">
        <thead class="fixed">

        </thead>

        <tbody>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "1250px" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >

        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#ffffff" />
        <Columns>

        <asp:BoundField DataField="CUSTCODE" HeaderText="客先コード" SortExpression="CUSTCODE" >
        </asp:BoundField>
        <asp:BoundField DataField="MINOU" HeaderText="未納金額" SortExpression="MINOU" >
        </asp:BoundField>
        <asp:BoundField DataField="RED" HeaderText="赤処理金額" SortExpression="RED" >
        </asp:BoundField>
        <asp:BoundField DataField="DELAY" HeaderText="出港遅延" SortExpression="DELAY" >
        </asp:BoundField>
        <asp:BoundField DataField="ADJ" HeaderText="調整納期" SortExpression="ADJ" >
        </asp:BoundField>
        <asp:BoundField DataField="ACM" HeaderText="累積金額" SortExpression="ACM" >
        </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#DCDCDC" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [CUSTCODE], [MINOU], [RED], [DELAY], [ADJ], [ACM] FROM [T_EXL_GRAPH_MINOU_KD]  WHERE [CUSTCODE] <>'' "></asp:SqlDataSource>

        </tbody>
        </table>
        </div>
    </asp:Panel>  
</div>

    
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop">
<a href="#">↑</a></p>   



</form>
</body>



</html>
