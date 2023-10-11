<%@ Page Language="VB" AutoEventWireup="false" CodeFile="make_graph_delaykin.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>ポータルサイト(月またぎ遅延金額グラフ)</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>


        <script src="scripts/Chart.min.js"></script>

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
                    url: "./make_graph_delaykin.aspx/getTrafficSourceData",
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
                 var dataarr7 = [];
                 var dataarr8 = [];

                 var dataarr10 = [];
                 var dataarr11 = [];
                 var dataarr12 = [];
                 var dataarr13 = [];

                 var Labelarr = [];
                 var Colorarr = [];
                 $.each(aData, function (inx, val) {
                     dataarr.push(val.value);
                     dataarr2.push(val.value2);
                     dataarr3.push(val.value3);
                     dataarr4.push(val.value4);
                     dataarr5.push(val.value5);
                     dataarr6.push(val.value6);
                     dataarr7.push(val.value7);
                     dataarr8.push(val.value8);

                     dataarr10.push(val.value10);
                     dataarr11.push(val.value11);
                     dataarr12.push(val.value12);
                     dataarr13.push(val.value13);

                     Labelarr.push(val.label);
                     //Colorarr.push(val.color);
                 });
                 var ctx = $("#myChart").get(0).getContext("2d");
                 var config = {
                     type: 'bar',
                     data: {
                         datasets: [{
                                 label: '受注比率',
                                 type: "line",
                                 fill: false,
                                 data: dataarr13,
                                 pointRadius: 0,
                                 pointHoverRadius: 0,
                                 pointHitRadius: 10,
                                 
                                 borderColor: 'rgb(255,0,0,0.5)',
                                 cubicInterpolationMode: 'monotone',
                                 yAxisID: "y-axis-2", // 追加
                         }, {
                             label: 'ＭＴ',
                             data: dataarr6,

                             backgroundColor: 'rgba(144, 255, 144, 0.9)',
                             borderColor: 'rgb(144, 255, 144)',
                             cubicInterpolationMode: 'monotone',
                             //背景色（ホバーしたときに）
                             //hoverBackgroundColor: "rgba(51, 51, 255, 0.5)",
                             //枠線の色（ホバーしたときに）
                             //hoverBorderColor: "rgba(54, 252, 235, 0.5)",
                             yAxisID: "y-axis-1", // 追加
                         }, {
                             label: 'ＡＴ',
                             data: dataarr2,

                             backgroundColor: 'rgba(255, 182, 193, 0.9)',
                             borderColor: 'rgb(255, 182, 193)',
                             cubicInterpolationMode: 'monotone',
                             //背景色（ホバーしたときに）
                             //hoverBackgroundColor: "rgba(51, 51, 255, 0.5)",
                             //枠線の色（ホバーしたときに）
                             //hoverBorderColor: "rgba(54, 252, 235, 0.5)",
                             yAxisID: "y-axis-1", // 追加
                         }, {
                             label: 'ＴＳ',
                             data: dataarr4,

                             backgroundColor: 'rgba(171, 225, 250, 0.9)',
                             borderColor: 'rgb(255, 182, 193)',
                             cubicInterpolationMode: 'monotone',
                             //背景色（ホバーしたときに）
                             //hoverBackgroundColor: "rgba(51, 51, 255, 0.5)",
                             //枠線の色（ホバーしたときに）
                             //hoverBorderColor: "rgba(54, 252, 235, 0.5)",
                             yAxisID: "y-axis-1", // 追加
                         }


                         ],

                         labels: Labelarr

                     },


                     options: {
                         responsive: true,
                         title: {                           // タイトル
                             display: true,                     // 表示設定
                             fontSize: 18,                      // フォントサイズ
                             fontFamily: "sans-serif",
                             text: '出港遅延による未納金額'                   // タイトルのラベル
                         },
                         scales: {                          // 軸設定
                             xAxes: [                           // Ｘ軸設定
                                 {
                                     stacked: true, //積み上げ棒グラフにする設定
                                     scaleLabel: {                 // 軸ラベル
                                         display: true,                // 表示設定
                                         labelString: '年月',    // ラベル
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
                             yAxes: [{
                       id: "y-axis-1",   // Y軸のID
                       position: "left", // どちら側に表示される軸か？
                       stacked: true, //積み上げ棒グラフにする設定
                       scaleLabel: {                  // 軸ラベル
                           display: true,                 // 表示の有無
                           labelString: '金額（単位：百万円）',     // ラベル
                           fontFamily: "sans-serif",
                           fontColor: "blue",             // 文字の色
                           fontFamily: "sans-serif",
                           fontSize: 16,                   // フォントサイズ

                           gridLines: {                   // 補助線
                           color: "rgba(0, 0, 255, 0.2)", // 補助線の色
                           zeroLineColor: "black"         // y=0（Ｘ軸の色）
                             },
                                 ticks: {                       // 目盛り
                             min: 0,                        // 最小値
                             //max: 20,                       // 最大値
                             //stepSize: 5,                   // 軸間隔
                             //fontColor: "blue",             // 目盛りの色
                             fontSize: 14                   // フォントサイズ
                         },
                       }}, {
                           id: "y-axis-2",   // Y軸のID
                           position: "right", // どちら側に表示される軸か？
                       scaleLabel: {                  // 軸ラベル
                           display: true,                 // 表示の有無
                           labelString: '受注金額比率（単位：％）',     // ラベル
                           fontFamily: "sans-serif",
                           fontColor: "blue",             // 文字の色
                           fontFamily: "sans-serif",
                           fontSize: 16,                   // フォントサイズ

                           gridLines: {                   // 補助線
                           ////color: "rgba(0, 0, 0, 0)", // 補助線の色
                           ////zeroLineColor: "black",         // y=0（Ｘ軸の色）
                           display: false
                           },

                           ticks: {                       // 目盛り
                             //min: 0,                        // 最小値
                             ////max: 20,                       // 最大値
                             //stepSize: 5,                   // 軸間隔
                             //fontColor: "blue",             // 目盛りの色
                               //fontSize: 14                   // フォントサイズ
                               display: false
                         },
                        },
                             }]
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
            <td style="width:450px;Font-Size:25px;" align="left" >
                <h2>遅延による未納金額</h2>
            </td>
        </tr>
    </table>

    <table >
        <tr>
            <td style="width:130px">
            <asp:Button ID="Button1" runat="server" Text="過去遅延実績" Visible="true" />
            </td>
            <td style="width:130px">
            <asp:Button ID="Button2" runat="server" Text="月またぎ遅延予測" Visible="true" />
            </td>
            <td style="width:800px">
            </td>
        </tr>
    </table>

    <table style="height:10px" >
    </table>

    <table >
        <tr>
            <td style="width:450px;Font-Size:23px;" align="left" >
                ＜遅延予測＞
            </td>
        </tr>
    </table>


    <table >
        <tr>
            <td style="width:450px;Font-Size:15px;" align="left" >
                ●集計
            </td>
        </tr>
    </table>
    <table border='1' style="width:200px;Font-Size:14px;" class ="sample1">
        <thead>
            <tr>
                <td colspan="1" style="width:50px;"><asp:Label ID="Label1" runat="server" Text="区分"></asp:Label></td>
                <td colspan="1" style="width:150px;"><asp:Label ID="Label2" runat="server" Text="未納予測金額（￥）"></asp:Label></td>
            </tr>
        </thead>
        <tbody>
            <tr >
                <td style="width:30px;background-color:lightgreen"><asp:Label ID="Label3" runat="server" Text="MT"></asp:Label></td>
                <td style="width:80px;background-color:lightgreen"><asp:Label ID="Label4" runat="server" Text="0"></asp:Label></td>
            </tr>
            <tr >
                <td style="width:30px;background-color:lightblue"><asp:Label ID="Label5" runat="server" Text="TS"></asp:Label></td>
                <td style="width:80px;background-color:lightblue"><asp:Label ID="Label6" runat="server" Text="0"></asp:Label></td>
            </tr>
            <tr >
                <td style="width:30px;background-color:lightpink"><asp:Label ID="Label7" runat="server" Text="AT"></asp:Label></td>
                <td style="width:80px;background-color:lightpink"><asp:Label ID="Label8" runat="server" Text="0"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td><asp:Label ID="Label9" runat="server" Text="合計"></asp:Label></td>
                <td><asp:Label ID="Label10" runat="server" Text=""></asp:Label></td>
            </tr>
        </tfoot>
    </table>

    <table >
        <tr>
            <td style="width:450px;Font-Size:15px;" align="left" >
                ●案件一覧 (実績 区分：赤)
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server"  Font-Size="12px" class ="AA" Height="200px">

        <div class="wrapper" id="main2" >
        <table class="sticky"   >
        <thead class="fixed" >

        </thead>
    
        <tbody >

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width = "1120px" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >

        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"  />
        <AlternatingRowStyle BackColor="#ffffff" />
        <Columns >

        <asp:BoundField DataField="KBN" HeaderText="区分" SortExpression="KBN" HtmlEncode="False" >
        <HeaderStyle Width="20px" />
        </asp:BoundField>
        <asp:BoundField DataField="COUNTRY" HeaderText="仕向国"　SortExpression="COUNTRY" HtmlEncode="False" >
        <HeaderStyle Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="DISTINATION" HeaderText="仕向地" SortExpression="DISTINATION" HtmlEncode="False" >
        <HeaderStyle Width="120px" />
        </asp:BoundField>
        <asp:BoundField DataField="CUSTCODE" HeaderText="客先コード" SortExpression="CUSTCODE" HtmlEncode="False" >
        <HeaderStyle Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="CUSTNAME" HeaderText="客先名" SortExpression="CUSTNAME" HtmlEncode="False" >
        <HeaderStyle Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="INVOICENO" HeaderText="INVOICENO" SortExpression="INVOICENO" HtmlEncode="False"  >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="ETD_B" HeaderText="ETD（遅延前）" SortExpression="ETD_B" HtmlEncode="False" >
        <HeaderStyle Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="ETD_A" HeaderText="ETD（遅延後）" SortExpression="ETD_A" HtmlEncode="False"  >
        <HeaderStyle Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="DDAYS" HeaderText="遅延日数" SortExpression="DDAYS" HtmlEncode="False"  >
        <HeaderStyle Width="40px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN" HeaderText="未納予測金額" SortExpression="KIN" HtmlEncode="False" >
        <HeaderStyle Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="BOOKING_NO" HeaderText="BOOKING_NO" SortExpression="BOOKING_NO" HtmlEncode="False" >
        <HeaderStyle Width="60px" />
        </asp:BoundField>
        </Columns>

        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_DELAY_FORCAST] WHERE CUSTCODE<>'対象外' "></asp:SqlDataSource>

        </tbody>
        </table>
        </div>
    </asp:Panel>  

</div>


<div id="contents2" class="inner2" >


    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px" class ="AA">

    <table >
        <tr>
            <td style="width:450px;Font-Size:23px;" align="left" >
                ＜遅延実績＞
            </td>
        </tr>
    </table>
    <table>
        <tr>

        <canvas id="myChart" width="160" height="60"></canvas>

        </tr>
    </table>




        <div class="wrapper" id="main2" >
        <table class="sticky"   >
        <thead class="fixed" >

        </thead>
    
        <tbody >

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "1300px" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >

        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"  />
        <AlternatingRowStyle BackColor="#ffffff" />
        <Columns >

        <asp:BoundField DataField="MONTH01" HeaderText="年月" SortExpression="MONTH01" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_ODR_A" HeaderText="受注金額<br/>（ＡＴ）"　SortExpression="KIN_ODR_A" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_DLY_A" HeaderText="遅延未納金額<br/>（ＡＴ）" SortExpression="KIN_DLY_A" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_DLY_A" HeaderText="未納金額比率<br/>（ＡＴ）" SortExpression="KIN_DLY_A" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_ODR_I" HeaderText="受注金額<br/>（ＴＳ）" SortExpression="KIN_ODR_I" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_DLY_I" HeaderText="遅延未納金額<br/>（ＴＳ）" SortExpression="KIN_DLY_I" HtmlEncode="False"  >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_DLY_I" HeaderText="未納金額比率<br/>（ＴＳ）" SortExpression="KIN_DLY_I" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_ODR_M" HeaderText="受注金額<br/>（ＭＴ）" SortExpression="KIN_ODR_M" HtmlEncode="False"  >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_DLY_M" HeaderText="遅延未納金額<br/>（ＭＴ）" SortExpression="KIN_DLY_M" HtmlEncode="False"  >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_DLY_M" HeaderText="未納金額比率<br/>（ＭＴ）" SortExpression="KIN_DLY_M" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_ODR_TOTAL" HeaderText="受注金額<br/>（ＡＬＬ）" SortExpression="KIN_ODR_TOTAL" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_DLY_TOTAL" HeaderText="遅延未納金額<br/>（ＡＬＬ）" SortExpression="KIN_DLY_TOTAL" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="KIN_DLY_TOTAL" HeaderText="未納金額比率<br/>（ＡＬＬ）" SortExpression="KIN_DLY_TOTAL" HtmlEncode="False" >
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        </Columns>
<%--        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#DCDCDC" ForeColor="Black" />--%>
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT * FROM [T_EXL_GRAPH_DELAYKIN] "></asp:SqlDataSource>

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
