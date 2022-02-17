<%@ Page Language="VB" AutoEventWireup="false" CodeFile="grapf.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>22222</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/style.css"/>
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<script src="//cdn.jsdelivr.net/excanvas/r3/excanvas.js" type="text/javascript"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.7.1/chart.min.js"></script>


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.bundle.min.js" type="text/javascript"></script>
 
    
 <script>
     $(document).ready(function () {
         //window.alert('koko1');
         //$("#btnGeneratePieChart").on('click', function (e) {
         //    //window.alert('koko2');
         //    e.preventDefault();
             //window.alert('koko3');
             //var gData = [];
             ////window.alert('koko4');
             //gData[0] = $("#ddlyear").val();
             ////gData[1] = $("#ddlMonth").val();
             ////window.alert(gData[0]);
             ////window.alert('koko5');
             //var jsonData = JSON.stringify({
             //    gData: gData
             //});
             //window.alert(jsonData);
             //window.alert('koko6');

             $.ajax({
                    type: "POST",
                 //url: "getTrafficSourceData",
                    url: "./grapf.aspx/getTrafficSourceData",
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
                 var Labelarr = [];
                 var Colorarr = [];
                 $.each(aData, function (inx, val) {
                     dataarr.push(val.value);
                     dataarr2.push(val.value2);
                     Labelarr.push(val.label);
                     //Colorarr.push(val.color);
                 });
                 var ctx = $("#myChart").get(0).getContext("2d");
                 var config = {
                     type: 'bar',
                     data: {
                         datasets: [{
                             label: '基準線',
                             type: "line",
                             fill: false,
                             data: dataarr2,
                             backgroundColor: 'rgba(54, 162, 235, 0.5)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: 'コンテナ本数',
                             data: dataarr,
                             backgroundColor: 'rgba(54, 162, 235, 0.5)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }],
                         labels: Labelarr

                     },




                     options: {
                         responsive: true,
                         scales: {                          // 軸設定
                             xAxes: [                           // Ｘ軸設定
                                 {
                                     scaleLabel: {                 // 軸ラベル
                                         display: true,                // 表示設定
                                         labelString: '日付',    // ラベル
                                         //fontColor: "red",             // 文字の色
                                         fontSize: 16                  // フォントサイズ
                                     },
                                     gridLines: {                   // 補助線
                                         //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                                     },
                                     ticks: {                      // 目盛り
                                         //fontColor: "red",             // 目盛りの色
                                         fontSize: 14                  // フォントサイズ
                                     }
                                 }
                             ],
                             yAxes: [                           // Ｙ軸設定
                   {
                       scaleLabel: {                  // 軸ラベル
                           display: true,                 // 表示の有無
                           labelString: 'コンテナ本数',     // ラベル
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
                           max: 20,                       // 最大値
                           stepSize: 5,                   // 軸間隔
                           fontColor: "blue",             // 目盛りの色
                           fontSize: 14                   // フォントサイズ
                       }
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
<body>
<form id="form1" runat="server">
        <div>

            <br />
            <div id="canvas-holder" style="width: 100%">
                <canvas id="myChart" width="190" height="90"></canvas>
            </div>
        </div>
    </form>
</body>
</html>

