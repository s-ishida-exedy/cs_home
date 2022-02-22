﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="make_graph_sintyoku.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>受注台数グラフ</title>
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

         // 値取得
         var gData = [];
         gData[0] = $("#TextBox1").val();
         gData[1] = $("#TextBox2").val();
         gData[2] = $("#TextBox3").val();
         gData[3] = $("#TextBox4").val();
         gData[4] = $("#TextBox5").val();
         gData[5] = $("#TextBox6").val();
         gData[6] = $("#TextBox7").val();
         gData[7] = $("#TextBox8").val();
         gData[8] = $("#TextBox9").val();
         gData[9] = $("#TextBox10").val();
         gData[10] = $("#TextBox11").val();
         gData[11] = $("#TextBox12").val();
         gData[12] = $("#TextBox13").val();
         gData[13] = $("#TextBox14").val();
         gData[14] = $("#TextBox15").val();





                 $.ajax({
                     type: "POST",
                     //url: "getTrafficSourceData",
                     url: "./make_graph_sintyoku.aspx/getTrafficSourceData",
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
                     var dataarr9 = [];
                     var dataarr10 = [];
                     var dataarr11 = [];
                     var dataarr12 = [];
                     var dataarr13 = [];
                     var dataarr14 = [];
                     var dataarr15 = [];
                     var dataarr16 = [];
                     var dataarr17 = [];
                     var dataarr18 = [];
                     var dataarr19 = [];
                     var dataarr20 = [];
                     var dataarr21 = [];
                     var dataarr22 = [];
                     var dataarr23 = [];
                     var dataarr24 = [];
                     var dataarr25 = [];
                     var Labelarr = [];
                     var Labelarr2 = [];
                     var Labelarr3 = [];

                     var Colorarr = [];

                     $.each(aData, function (inx, val) {
                         dataarr.push(val.value);
                         dataarr2.push(val.value2);
                         dataarr3.push(val.value3);
                         dataarr4.push(val.value4);
                         dataarr5.push(val.value5);
                         dataarr6.push(val.value6);


                         Labelarr.push(val.label);

                         //window.alert('KK3');
                         //window.alert(Labelarr[0]);
                         //window.alert(Labelarr[1]);
                         //window.alert(Labelarr[2]);


                         lstr = Labelarr[0];
                         lstr2 = Labelarr[1];
                         lstr3 = Labelarr[2];

                         str = dataarr[0];
                         str1 = dataarr2[0];
                         str2 = dataarr3[0];
                         str3 = dataarr4[0];
                         str4 = dataarr5[0];
                         str5 = dataarr6[0];
  
                         str6 = dataarr[1];
                         str7 = dataarr2[1];
                         str8 = dataarr3[1];
                         str9 = dataarr4[1];
                         str10 = dataarr5[1];
                         str11 = dataarr6[1];

                         str12 = dataarr[2];
                         str13 = dataarr2[2];
                         str14 = dataarr3[2];
                         str15 = dataarr4[2];
                         str16 = dataarr5[2];
                         str17 = dataarr6[2];




                         //Colorarr.push(val.color);
                     });

                     //1____________________________________________________________________________________________

                 // 背景色                
                 function drawBackground(target) {
                     // 範囲を設定
                     var xscale = target.scales["x-axis-0"];
                     var yscale = target.scales["y-axis-0"];
                     var left = xscale.getPixelForValue(gData[0]);  // 塗りつぶしを開始するラベル位置
                     var right = xscale.getPixelForValue(gData[1]); // 塗りつぶしを終了するラベル位置
                     var top = yscale.top;                      // 塗りつぶしの基点（上端）

                     // 塗りつぶす長方形の設定
                     ctx.fillStyle = "rgba(255,0,0, 0.2)";
                     ctx.fillRect(left, top, right - left, yscale.height);

                     var xscale = target.scales["x-axis-0"];
                     var yscale = target.scales["y-axis-0"];
                     var left = xscale.getPixelForValue(gData[1]);  // 塗りつぶしを開始するラベル位置
                     var right = xscale.getPixelForValue(gData[2]); // 塗りつぶしを終了するラベル位置
                     var top = yscale.top;                      // 塗りつぶしの基点（上端）

                     // 塗りつぶす長方形の設定
                     ctx.fillStyle = "rgba(0,255,0, 0.2)";
                     ctx.fillRect(left, top, right - left, yscale.height);

                 }


                 var ctx = $("#myChart").get(0).getContext("2d");
                 var config = {
                     type: 'horizontalBar',
                     data: {
                         datasets: [{
                             label: '前月先行出荷',
                             type: 'horizontalBar',
                             fill: false,
                             data: [str],
                             backgroundColor: 'rgba(255, 255, 102)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',


                         }, {
                             label: '当月分（別月から調整）',
                             type: 'horizontalBar',
                             fill: false,
                             data: [str1],
                             backgroundColor: 'rgba(218, 165, 32)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         },{
                             label: '当月分（オリジナル）',
                             type: 'horizontalBar',
                             data: [str2],
                             backgroundColor: 'rgba(30, 144, 255)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '残金額',
                             type: 'horizontalBar',
                             data: [str3],
                             backgroundColor: 'rgba(255, 153, 201)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '先行出荷（翌月以降納期分）',
                             type: 'horizontalBar',
                             data: [str4],
                             backgroundColor: 'rgba(204, 51, 153)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '調整納期（当月から翌月以降）',
                             type: 'horizontalBar',
                             data: [str5],
                             backgroundColor: 'rgba(153, 204, 51)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }],
                         labels: [lstr]
                     },
                     plugins: [{
                         beforeDraw: drawBackground // ★
                     }],
                     options: {
                         responsive: true,
                         //legend: {
                         //    display: false
                         
                         //},
                         scales: {                          // 軸設定
                             xAxes: [                           // Ｘ軸設定
                                 {
                                     stacked: true, //積み上げ棒グラフにする設定
                                     scaleLabel: {                 // 軸ラベル
                                         display: true,                // 表示設定
                                         //labelString: '百万円',    // ラベル
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
                       stacked: true, //積み上げ棒グラフにする設定
                       scaleLabel: {                  // 軸ラベル
                           display: true,                 // 表示の有無
                           labelString: '受注台数',     // ラベル
                           fontFamily: "sans-serif",
                           fontColor: "blue",             // 文字の色
                           fontFamily: "sans-serif",
                           fontSize: 16                   // フォントサイズ
                       },
                       gridLines: {                   // 補助線
                           color: "rgba(0, 0, 255, 0.2)", // 補助線の色
                           //zeroLineColor: "black"         // y=0（Ｘ軸の色）
                       },
                       ticks: {                       // 目盛り
                           min: 0,                        // 最小値
                           //max: 20,                       // 最大値
                           //stepSize: 5,                   // 軸間隔
                           fontColor: "blue",             // 目盛りの色
                           fontSize: 14                   // フォントサイズ
                       }
                   }
                             ]
                         }
                     }
                 };







                     //2____________________________________________________________________________________________

                     // 背景色                
                 function drawBackground2(target2) {
                     // 範囲を設定
                     var xscale2 = target2.scales["x-axis-0"];
                     var yscale2 = target2.scales["y-axis-0"];
                     var left2 = xscale2.getPixelForValue(gData[0]);  // 塗りつぶしを開始するラベル位置
                     var right2 = xscale2.getPixelForValue(gData[1]); // 塗りつぶしを終了するラベル位置
                     var top2 = yscale2.top;                      // 塗りつぶしの基点（上端）

                     // 塗りつぶす長方形の設定
                     ctx2.fillStyle = "rgba(255,0,0, 0.2)";
                     ctx2.fillRect(left2, top2, right2 - left2, yscale2.height);

                     var xscale2 = target2.scales["x-axis-0"];
                     var yscale2 = target2.scales["y-axis-0"];
                     var left2 = xscale2.getPixelForValue(gData[1]);  // 塗りつぶしを開始するラベル位置
                     var right2 = xscale2.getPixelForValue(gData[2]); // 塗りつぶしを終了するラベル位置
                     var top2 = yscale2.top;                      // 塗りつぶしの基点（上端）

                     // 塗りつぶす長方形の設定
                     ctx2.fillStyle = "rgba(0,255,0, 0.2)";
                     ctx2.fillRect(left2, top2, right2 - left2, yscale2.height);

                 }


                 var ctx2 = $("#myChart2").get(0).getContext("2d");
                 var config2 = {
                     type: 'horizontalBar',
                     data: {
                         datasets: [{
                             label: '前月先行出荷',
                             type: 'horizontalBar',
                             fill: false,
                             data: [str6],
                             backgroundColor: 'rgba(255, 255, 102)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',

                         }, {
                             label: '当月分（別月から調整）',
                             type: 'horizontalBar',
                             fill: false,
                             data: [str7],
                             backgroundColor: 'rgba(218, 165, 32)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '当月分（オリジナル）',
                             type: 'horizontalBar',
                             data: [str8],
                             backgroundColor: 'rgba(30, 144, 255)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '残金額',
                             type: 'horizontalBar',
                             data: [str9],
                             backgroundColor: 'rgba(255, 153, 201)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '先行出荷（翌月以降納期分）',
                             type: 'horizontalBar',
                             data: [str10],
                             backgroundColor: 'rgba(204, 51, 153)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '調整納期（当月から翌月以降）',
                             type: 'horizontalBar',
                             data: [str11],
                             backgroundColor: 'rgba(153, 204, 51)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }],
                         labels: [lstr2]
                     },
                     plugins: [{
                         beforeDraw: drawBackground2 // ★
                     }],
                     options: {
                         responsive: true,
                         legend: {
                             display: false

                         },
                         scales: {                          // 軸設定
                             xAxes: [                           // Ｘ軸設定
                                 {
                                     stacked: true, //積み上げ棒グラフにする設定
                                     scaleLabel: {                 // 軸ラベル
                                         display: true,                // 表示設定
    
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
                       stacked: true, //積み上げ棒グラフにする設定
                       scaleLabel: {                  // 軸ラベル
                           display: true,                 // 表示の有無
                           labelString: '受注台数',     // ラベル
                           fontFamily: "sans-serif",
                           fontColor: "blue",             // 文字の色
                           fontFamily: "sans-serif",
                           fontSize: 16                   // フォントサイズ
                       },
                       gridLines: {                   // 補助線
                           color: "rgba(0, 0, 255, 0.2)", // 補助線の色
                           //zeroLineColor: "black"         // y=0（Ｘ軸の色）
                       },
                       ticks: {                       // 目盛り
                           min: 0,                        // 最小値
                           //max: 20,                       // 最大値
                           //stepSize: 5,                   // 軸間隔
                           fontColor: "blue",             // 目盛りの色
                           fontSize: 14                   // フォントサイズ
                       }
                   }
                             ]
                         }
                     }
                 };





                     //3____________________________________________________________________________________________

                     // 背景色                
                 function drawBackground3(target3) {
                     // 範囲を設定
                     var xscale2 = target3.scales["x-axis-0"];
                     var yscale2 = target3.scales["y-axis-0"];
                     var left2 = xscale2.getPixelForValue(gData[0]);  // 塗りつぶしを開始するラベル位置
                     var right2 = xscale2.getPixelForValue(gData[1]); // 塗りつぶしを終了するラベル位置
                     var top2 = yscale2.top;                      // 塗りつぶしの基点（上端）

                     // 塗りつぶす長方形の設定
                     ctx3.fillStyle = "rgba(255,0,0, 0.2)";
                     ctx3.fillRect(left2, top2, right2 - left2, yscale2.height);

                     var xscale2 = target3.scales["x-axis-0"];
                     var yscale2 = target3.scales["y-axis-0"];
                     var left2 = xscale2.getPixelForValue(gData[1]);  // 塗りつぶしを開始するラベル位置
                     var right2 = xscale2.getPixelForValue(gData[2]); // 塗りつぶしを終了するラベル位置
                     var top2 = yscale2.top;                      // 塗りつぶしの基点（上端）

                     // 塗りつぶす長方形の設定
                     ctx3.fillStyle = "rgba(0,255,0, 0.2)";
                     ctx3.fillRect(left2, top2, right2 - left2, yscale2.height);

                 }


                 var ctx3 = $("#myChart3").get(0).getContext("2d");
                 var config3 = {
                     type: 'horizontalBar',
                     data: {
                         datasets: [{
                             label: '前月先行出荷',
                             type: 'horizontalBar',
                             fill: false,
                             data: [str12],
                             backgroundColor: 'rgba(255, 255, 102)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',

                         }, {
                             label: '当月分（別月から調整）',
                             type: 'horizontalBar',
                             fill: false,
                             data: [str13],
                             backgroundColor: 'rgba(218, 165, 32)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '当月分（オリジナル）',
                             type: 'horizontalBar',
                             data: [str14],
                             backgroundColor: 'rgba(30, 144, 255)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '残金額',
                             type: 'horizontalBar',
                             data: [str15],
                             backgroundColor: 'rgba(255, 153, 201)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '先行出荷（翌月以降納期分）',
                             type: 'horizontalBar',
                             data: [str16],
                             backgroundColor: 'rgba(204, 51, 153)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }, {
                             label: '調整納期（当月から翌月以降）',
                             type: 'horizontalBar',
                             data: [str17],
                             backgroundColor: 'rgba(153, 204, 51)',
                             borderColor: 'rgb(54, 162, 235)',
                             cubicInterpolationMode: 'monotone',
                         }],
                         labels: [lstr3]
                     },
                     plugins: [{
                         beforeDraw: drawBackground3 // ★
                     }],
                     options: {
                         responsive: true,
                         legend: {
                             display: false

                         },
                         scales: {                          // 軸設定
                             xAxes: [                           // Ｘ軸設定
                                 {
                                     stacked: true, //積み上げ棒グラフにする設定
                                     scaleLabel: {                 // 軸ラベル
                                         display: true,                // 表示設定
                                         labelString: '百万円',    // ラベル
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
                       stacked: true, //積み上げ棒グラフにする設定
                       scaleLabel: {                  // 軸ラベル
                           display: true,                 // 表示の有無
                           labelString: '受注台数',     // ラベル
                           fontFamily: "sans-serif",
                           fontColor: "blue",             // 文字の色
                           fontFamily: "sans-serif",
                           fontSize: 16                   // フォントサイズ
                       },
                       gridLines: {                   // 補助線
                           color: "rgba(0, 0, 255, 0.2)", // 補助線の色
                           //zeroLineColor: "black"         // y=0（Ｘ軸の色）
                       },
                       ticks: {                       // 目盛り
                           min: 0,                        // 最小値
                           //max: 20,                       // 最大値
                           //stepSize: 5,                   // 軸間隔
                           fontColor: "blue",             // 目盛りの色
                           fontSize: 14                   // フォントサイズ
                       }
                   }
                             ]
                         }
                     }
                 };










                 var myPieChart = new Chart(ctx, config);
                 var myPieChart2 = new Chart(ctx2, config2);
                 var myPieChart3 = new Chart(ctx3, config3);

                 let ele = document.getElementById('TextBox1');
                 const displayOriginal = ele.style.display;
                 ele.style.display = 'none';

                 let ele2 = document.getElementById('TextBox2');
                 const displayOriginal2 = ele2.style.display;
                 ele2.style.display = 'none';

                 let ele3 = document.getElementById('TextBox3');
                 const displayOriginal3 = ele3.style.display;
                 ele3.style.display = 'none';

                 let ele4 = document.getElementById('TextBox4');
                 const displayOriginal4 = ele4.style.display;
                 ele4.style.display = 'none';

                 let ele5 = document.getElementById('TextBox5');
                 const displayOriginal5 = ele5.style.display;
                 ele5.style.display = 'none';

                 let ele6 = document.getElementById('TextBox6');
                 const displayOriginal6 = ele6.style.display;
                 ele6.style.display = 'none';

                 let ele7 = document.getElementById('TextBox7');
                 const displayOriginal7 = ele7.style.display;
                 ele7.style.display = 'none';

                 let ele8 = document.getElementById('TextBox8');
                 const displayOriginal8 = ele8.style.display;
                 ele8.style.display = 'none';

                 let ele9 = document.getElementById('TextBox9');
                 const displayOriginal9 = ele9.style.display;
                 ele9.style.display = 'none';

                 let ele10 = document.getElementById('TextBox10');
                 const displayOriginal10 = ele10.style.display;
                 ele10.style.display = 'none';

                 let ele11 = document.getElementById('TextBox11');
                 const displayOriginal11 = ele11.style.display;
                 ele11.style.display = 'none';

                 let ele12 = document.getElementById('TextBox12');
                 const displayOriginal12 = ele12.style.display;
                 ele12.style.display = 'none';

                 let ele13 = document.getElementById('TextBox13');
                 const displayOriginal13 = ele13.style.display;
                 ele13.style.display = 'none';

                 let ele14 = document.getElementById('TextBox14');
                 const displayOriginal14 = ele14.style.display;
                 ele14.style.display = 'none';

                 let ele15 = document.getElementById('TextBox15');
                 const displayOriginal15 = ele15.style.display;
                 ele15.style.display = 'none';


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
<!-- メニューの編集はheader.htmlで行う -->
 <!-- #Include File="header.html" -->
       
<div id="contents2" class="inner2">
<table >
<tr>
<td style="width:450px;Font-Size:25px;" >
<h2>受注台数グラフ</h2>
</td>
</tr>
</table>






<table>
<tr>
<td style="width:1500px;Font-Size:25px;height:18px;" >

<canvas id="myChart" width="160" height="25"></canvas>
    </td>


</tr>
</table>

<table>
<tr>


<td style="width:1500px;Font-Size:25px;height:18px;" >
    <canvas id="myChart2" width="160" height="22"></canvas>
            </td>


</tr>
</table>

<table>
<tr>

<td style="width:1500px;Font-Size:25px;height:18px;" >
        <canvas id="myChart3" width="160" height="22"></canvas>
                </td>

</tr>
</table>

</div>



<asp:TextBox ID="TextBox1" runat="server" Text="10"></asp:TextBox>
<asp:TextBox ID="TextBox2" runat="server" Text="50"></asp:TextBox>
<asp:TextBox ID="TextBox3" runat="server" Text="100"></asp:TextBox>
<asp:TextBox ID="TextBox4" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox5" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox6" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox7" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox8" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox9" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox10" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox11" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox12" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox13" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox14" runat="server" Text="aaa"></asp:TextBox>
<asp:TextBox ID="TextBox15" runat="server" Text="aaa"></asp:TextBox>
    
<!--/#contents2-->

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop">
<a href="#">↑</a></p>   



</form>
</body>



</html>
