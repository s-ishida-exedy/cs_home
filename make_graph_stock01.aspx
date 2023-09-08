<%@ Page Language="VB" AutoEventWireup="false" CodeFile="make_graph_stock01.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>ポータルサイト(在庫推移グラフ)</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="css/style.css" />
    <script src="js/openclose.js"></script>
    <script src="js/fixmenu.js"></script>
    <script src="js/fixmenu_pagetop.js"></script>
    <script src="js/ddmenu_min.js"></script>
    <script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
    <script src="js/default.js"></script>


    <script src="scripts/Chart.min.js"></script>

    <style type="text/css">
        #form1 {
            background-color: #ffffff;
            color: #000000;
        }

        body {
            background-color: #ffffff;
        }

        A.sample1:link {
            color: blue;
        }

        A.sample1:visited {
            color: blue;
        }

        A.sample1:active {
            color: blue;
        }

        A.sample1:hover {
            color: blue;
        }

        .auto-style6 {
            margin-right: 7px;
        }

        table {
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
            height: 150px;
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

            h2:after {
                content: "";
                background-color: #6fbfd1;
                border-radius: 50%;
                opacity: 0.5;
                width: 20px;
                height: 20px;
                left: 25px;
                top: 15px;
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
                } else {
                    //window.alert('koko3');
                    window.location.href = './detail.aspx?id=' + encodeURIComponent(arr[1]);
                    return false;
                };
            });
        });


    </script>

    <script>
        $(document).ready(function () {


                var date = new Date();
                var y = date.getFullYear();
                var m = ("00" + (date.getMonth() + 1)).slice(-2);
                var d = ("00" + date.getDate()).slice(-2);
    
                var dt = [y, m, d].join("/");
            //window.alert(dt);
            

                $.ajax({
                    type: "POST",
                    //url: "getTrafficSourceData",
                    url: "./make_graph_stock01.aspx/getTrafficSourceData",
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
                var dataarri = [];
                var dataarrc = [];
                var NUM01 = [];

                var hanntei01 = [];
                var hanntei02 = [];
                var hanntei03 = [];
                var hanntei04 = [];
                var hanntei05 = [];
                var hanntei06 = [];

                var Labelarr = [];
                var Labelarr2 = [];
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
                    dataarr9.push(val.value9);
                    dataarr10.push(val.value10);
                    dataarr11.push(val.value11);
                    dataarr12.push(val.value12);
                    dataarr13.push(val.value13);
                    dataarr14.push(val.value14);
                    dataarr15.push(val.value15);
                    dataarr16.push(val.value16);
                    dataarr17.push(val.value17);
                    Labelarr.push(val.label);
                    Labelarr2.push(val.label);
                    //Colorarr.push(val.color);
                    dataarr18.push(val.value18);
                    dataarr19.push(val.value19);
                    dataarr20.push(val.value20);
                    dataarr21.push(val.value21);
                });

                for (var i = 0; i < Labelarr2.length; i++) {
                    if (Labelarr2[i] == dt) {
                        dataarri = i
                    } else {
                    }

                    dataarrc = i
                }


                if (dataarr18[dataarri] < dataarr15[dataarri]) {
                    hanntei01 = '○'
                } else {
                    hanntei01 = '×'
                };

                if (dataarr19[dataarri] < dataarr7[dataarri]) {
                    hanntei02 = '○'
                } else {
                    hanntei02 = '×'
                };

                if (dataarr20[dataarri] < dataarr8[dataarri]) {
                    hanntei03 = '○'
                } else {
                    hanntei03 = '×'
                };

                if (dataarr21[dataarri] < dataarr9[dataarri]) {
                    hanntei04 = '○'
                } else {
                    hanntei04 = '×'
                };


                function drawBackground(target) {
                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var left = xscale.getPixelForValue(dt)-1;  // 塗りつぶしを開始するラベル位置
                    var right = xscale.getPixelForValue(dt)+1; // 塗りつぶしを終了するラベル位置

                    var top = yscale.top;                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx.fillStyle = "rgba(230,0,51,0.8)";
                    ctx.fillRect(left, top, right - left, yscale.height);
                    ctx.save();

                    ctx.font = '14px sans-serif';
                    ctx.textAlign = 'center';
                    ctx.fillRect(left - 60, top + 30, 121, 20);
                    ctx.fillStyle = "White";
                    ctx.fillText('当日:' + dt, left, top + 40);

                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var top = yscale.getPixelForValue(dataarr15[dataarri]) - 1;  // 塗りつぶしを開始するラベル位置
                    var bottom = yscale.getPixelForValue(dataarr15[dataarri]) + 1; // 塗りつぶしを終了するラベル位置

                    var left = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
                    var right = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

                    var topX = yscale.top;                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx.fillStyle = "rgba(230,0,51,0.8)";
                    ctx.fillRect(left, top, right+14, 1.5);

                    ctx.font = '14px sans-serif';
                    ctx.textAlign = 'center';
                    ctx.fillRect(left +10, top-137, 140, 20);
                    ctx.fillStyle = "WHITE";
                    ctx.fillText('収容可能数：' + dataarr15[dataarri] + '千台', left + 80, topX - 4);

                    ctx.save();

                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var top2 = yscale.getPixelForValue(dataarr15[dataarri]) - 1;  // 塗りつぶしを開始するラベル位置
                    var bottom2 = yscale.getPixelForValue(dataarr15[dataarri]) + 1; // 塗りつぶしを終了するラベル位置

                    var left2 = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
                    var right2 = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx.fillStyle = "rgba(230,0,51,0.8)";
                    ctx.fillRect(left2 + 10, top2 - 135, 1.5, 135);
                    ctx.save();

                    ctx.font = '19px sans-serif';
                    ctx.textAlign = 'center';
                    ctx.fillStyle = "rgba(230,0,51,0.8)";
                    ctx.fillText('↓', left +10.5, top - 10);

                    ctx.save();

                    // 範囲を設定dataarrc
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var top = 20;  // 塗りつぶしを開始するラベル位置
                    var bottom = 100; // 塗りつぶしを終了するラベル位置

                    var left = xscale.getPixelForValue(Labelarr[dataarrc]);             // 塗りつぶしの基点（上端）
                    var right = xscale.getPixelForValue(Labelarr[dataarrc]);            // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定



                    if (hanntei01 == '○') {
                        ctx.fillStyle = "rgba(0,0,255,0.7)";
                    } else {

                    };

                    ctx.font = '25px sans-serif';
                    ctx.textAlign = 'center';
                    ctx.fillRect(right-340, top , 330, 40);
                    ctx.fillStyle = "White";
                    ctx.fillText('判定：' + hanntei01 + " / 余剰：" + (dataarr15[dataarri]-dataarr18[dataarri])+'千台', left - 180, top + 20);



                    ctx.save();

                }

                var ctx = $("#myChart").get(0).getContext("2d");


                var config = {
                    type: 'line',
                    data: {


                        datasets: [{
                            data: dataarr17,// エリア下端データセット
                            label: "A",
                            pointRadius: 0,
                            pointHitRadius: 0,
                            fill:false,// エリア下端はfalseにしておくと良い
                        },{
                            data: dataarr3,//エリア上端データセット
                            label: "4F",
                            pointRadius: 1,
                            pointHitRadius: 2,
                            //上端は下端の位置を設定する
                            //以下の場合は1つ前を意味する
                            fill: "-1",
                            backgroundColor: 'rgba(255, 165, 0, 0.8)',// エリアの色はこちらで指定する
                        }, {
                            data: dataarr2,//エリア上端データセット
                            label: "3F",
                            pointRadius: 1,
                            pointHitRadius: 2,
                            //上端は下端の位置を設定する
                            //以下の場合は1つ前を意味する
                            fill: "-1",
                            backgroundColor: 'rgba(0, 0, 255, 0.8)',// エリアの色はこちらで指定する
                        }, {
                            data: dataarr,//エリア上端データセット
                            label: "1F",
                            pointRadius: 1,
                            pointHitRadius: 2,
                            //上端は下端の位置を設定する
                            //以下の場合は1つ前を意味する
                            fill: "-1",
                            backgroundColor: 'rgba(0, 128, 0, 0.8)',// エリアの色はこちらで指定する
                        },{
                            data: dataarr14,//エリア上端データセット
                            label: "外部倉庫",
                            pointRadius: 1,
                            pointHitRadius: 2,

                            //上端は下端の位置を設定する
                            //以下の場合は1つ前を意味する
                            fill: "-1",
                            backgroundColor: 'rgba(0, 0, 0, 0.2)',// エリアの色はこちらで指定する
                        }],
                        labels: Labelarr
                    },
                    plugins: [{
                        afterDraw: drawBackground // ★
                    }],
                    options: {
                        legend: {
                            labels: {
                                filter: function(items) {
                                    return items.text != 'A';
                                    // return items.datasetIndex != 2;
                                }
                            }
                        },
                        title: {
                            display: true, // タイトルを表示する
                            text: 'EXLフロア別在庫推移（実績と予測）', // タイトルのテキスト
                            fontSize: 20
                        },
                        responsive: false,
                        scales: {                          // 軸設定
                            xAxes: [                           // Ｘ軸設定
                                {
                                    stacked: false, //積み上げ棒グラフにする設定
                                    scaleLabel: {                 // 軸ラベル
                                        display: false,                // 表示設定
                                        //labelString: '',    // ラベル
                                        //fontSize: 10                  // フォントサイズ
                                    },
                                    gridLines: {                   // 補助線
                                        display: false,                // 表示設定
                                        //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                                    },
                                    ticks: {                      // 目盛り
                                        //fontColor: "red",             // 目盛りの色

                                        //stepSize: 10,                   // 軸間隔
                                        fontSize: 14                  // フォントサイズ
                                    }
                                }
                            ],
                            yAxes: [                           // Ｙ軸設定
                                  {
                                      stacked: true, //積み上げ棒グラフにする設定
                                      scaleLabel: {                  // 軸ラベル
                                          display: true,                 // 表示の有無
                                          labelString: '単位：千台',     // ラベル
                                          fontFamily: "sans-serif",
                                          fontColor: "black",             // 文字の色
                                          fontFamily: "sans-serif",
                                          fontSize: 18                   // フォントサイズ
                                      },
                                      gridLines: {                   // 補助線
                                          color: "rgba(0, 0, 0, 0.2)", // 補助線の色
                                          //zeroLineColor: "black"         // y=0（Ｘ軸の色）
                                      },
                                      ticks: {                       // 目盛り
                                          min: 100,                        // 最小値
                                          max: 400,                       // 最大値
                                          //stepSize: 50000,                   // 軸間隔
                                          //callback: function(label, index, labels) { /* ここです */
                                          //    return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') ;
                                          //},

                                          fontColor: "rgba(0, 0, 0, 0.8)",             // 目盛りの色
                                          fontSize: 15                   // フォントサイズ
                                      }
                                  }
                            ]
                        }
                    }
                };

                '========================================================================================================================'

                function drawBackground3(target) {
                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var left = xscale.getPixelForValue(dt) - 1;  // 塗りつぶしを開始するラベル位置
                    var right = xscale.getPixelForValue(dt) + 1; // 塗りつぶしを終了するラベル位置

                    var top = yscale.top;                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx3.fillStyle = "rgba(230,0,51,0.8)";
                    ctx3.fillRect(left, top, right - left, yscale.height);
                    ctx3.save();

                    ctx3.font = '14px sans-serif';
                    ctx3.textAlign = 'center';
                    ctx3.fillRect(left - 60, top + 0, 121, 20);
                    ctx3.fillStyle = "White";
                    ctx3.fillText('当日:' + dt, left, top + 10);

                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var top = yscale.getPixelForValue(dataarr7[dataarri]) - 1;  // 塗りつぶしを開始するラベル位置
                    var bottom = yscale.getPixelForValue(dataarr7[dataarri]) + 1; // 塗りつぶしを終了するラベル位置

                    var left = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
                    var right = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

                    var topX = yscale.top;                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx3.fillStyle = "rgba(230,0,51,0.8)";
                    ctx3.fillRect(left, top, right + 14, 1.5);

                    ctx3.font = '14px sans-serif';
                    ctx3.textAlign = 'center';
                    ctx3.fillRect(left + 10, top - 130, 140, 20);
                    ctx3.fillStyle = "WHITE";
                    ctx3.fillText('収容可能数：' + dataarr7[dataarri] + '千台', left + 80, topX - 20);

                    ctx3.save();

                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var top2 = yscale.getPixelForValue(dataarr7[dataarri]) - 1;  // 塗りつぶしを開始するラベル位置
                    var bottom2 = yscale.getPixelForValue(dataarr7[dataarri]) + 1; // 塗りつぶしを終了するラベル位置

                    var left2 = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
                    var right2 = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx3.fillStyle = "rgba(230,0,51,0.8)";
                    ctx3.fillRect(left2 + 10, top2 - 110, 1.5, 110);
                    ctx3.save();

                    ctx3.font = '19px sans-serif';
                    ctx3.textAlign = 'center';
                    ctx3.fillStyle = "rgba(230,0,51,0.8)";
                    ctx3.fillText('↓', left + 10.5, top - 10);

                    ctx3.save();

                    // 範囲を設定dataarrc
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var top = 20;  // 塗りつぶしを開始するラベル位置
                    var bottom = 100; // 塗りつぶしを終了するラベル位置

                    var left = xscale.getPixelForValue(Labelarr[dataarrc]);             // 塗りつぶしの基点（上端）
                    var right = xscale.getPixelForValue(Labelarr[dataarrc]);            // 塗りつぶしの基点（上端）


                    if (hanntei02 == '○') {
                        ctx3.fillStyle = "rgba(0,0,255,0.7)";
                    } else {

                    };

                    // 塗りつぶす長方形の設定
                    ctx3.font = '25px sans-serif';
                    ctx3.textAlign = 'center';
                    ctx3.fillRect(right - 340, top, 330, 40);
                    ctx3.fillStyle = "White";
                    ctx3.fillText('判定：' + hanntei02 + " / 余剰：" + (dataarr7[dataarri]-dataarr19[dataarri]) + '千台', left - 180, top + 20);

                    ctx3.save();







                }

                var ctx3 = $("#myChart3").get(0).getContext("2d");

                var config3 = {
                    type: 'line',
                    data: {


                        datasets: [{
                            data: dataarr17,// エリア下端データセット
                            label: "A",
                            pointRadius: 0,
                            pointHitRadius: 0,
                            fill: false,// エリア下端はfalseにしておくと良い
                        }, {
                            data: dataarr,//エリア上端データセット
                            label: "1F",
                            pointRadius: 1,
                            pointHitRadius: 2,
                            //上端は下端の位置を設定する
                            //以下の場合は1つ前を意味する
                            fill: "-1",
                            backgroundColor: 'rgba(0, 128, 0, 0.8)',// エリアの色はこちらで指定する
                        }, {
                            data: dataarr4,//エリア上端データセット
                            label: "外部倉庫",
                            pointRadius: 1,
                            pointHitRadius: 2,

                            //上端は下端の位置を設定する
                            //以下の場合は1つ前を意味する
                            fill: "-1",
                            backgroundColor: 'rgba(0, 0, 0, 0.2)',// エリアの色はこちらで指定する
                        }],
                        labels: Labelarr
                    },
                    plugins: [{
                        afterDraw: drawBackground3 // ★
                    }],
                    options: {
                        legend: {
                            labels: {

                                filter: function (items) {
                                    return items.text != 'A';
                                    // return items.datasetIndex != 2;
                                }
                            }
                        },
                        title: {
                            display: true, // タイトルを表示する
                            text: 'EXL1F 在庫推移（実績と予測）', // タイトルのテキスト
                            fontSize: 20
                        },
                        responsive: false,
                        scales: {                          // 軸設定
                            xAxes: [                           // Ｘ軸設定
                                {
                                    stacked: false, //積み上げ棒グラフにする設定
                                    scaleLabel: {                 // 軸ラベル
                                        display: false,                // 表示設定
                                        //labelString: '',    // ラベル
                                        //fontSize: 10                  // フォントサイズ
                                    },
                                    gridLines: {                   // 補助線
                                        display: false,                // 表示設定
                                        //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                                    },
                                    ticks: {                      // 目盛り
                                        //fontColor: "red",             // 目盛りの色

                                        //stepSize: 10,                   // 軸間隔
                                        fontSize: 14                  // フォントサイズ
                                    }
                                }
                            ],
                            yAxes: [                           // Ｙ軸設定
                                  {
                                      stacked: true, //積み上げ棒グラフにする設定
                                      scaleLabel: {                  // 軸ラベル
                                          display: true,                 // 表示の有無
                                          labelString: '単位：千台',     // ラベル
                                          fontFamily: "sans-serif",
                                          fontColor: "black",             // 文字の色
                                          fontFamily: "sans-serif",
                                          fontSize: 18                   // フォントサイズ
                                      },
                                      gridLines: {                   // 補助線
                                          color: "rgba(0, 0, 0, 0.2)", // 補助線の色
                                          //zeroLineColor: "black"         // y=0（Ｘ軸の色）
                                      },
                                      ticks: {                       // 目盛り
                                          min: 40,                        // 最小値
                                          max: 80,                       // 最大値
                                          //stepSize: 50000,                   // 軸間隔
                                          //callback: function(label, index, labels) { /* ここです */
                                          //    return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') ;
                                          //},

                                          fontColor: "rgba(0, 0, 0, 0.8)",             // 目盛りの色
                                          fontSize: 15                   // フォントサイズ
                                      }
                                  }
                            ]
                        }
                    }
                };

'========================================================================================================================'

'========================================================================================================================'

function drawBackground4(target) {
    // 範囲を設定
    var xscale = target.scales["x-axis-0"];
    var yscale = target.scales["y-axis-0"];
    var left = xscale.getPixelForValue(dt) - 1;  // 塗りつぶしを開始するラベル位置
    var right = xscale.getPixelForValue(dt) + 1; // 塗りつぶしを終了するラベル位置

    var top = yscale.top;                      // 塗りつぶしの基点（上端）

    // 塗りつぶす長方形の設定
    ctx4.fillStyle = "rgba(230,0,51,0.8)";
    ctx4.fillRect(left, top, right - left, yscale.height);
    ctx4.save();

    ctx4.font = '14px sans-serif';
    ctx4.textAlign = 'center';
    ctx4.fillRect(left - 60, top + 5, 121, 20);
    ctx4.fillStyle = "White";
    ctx4.fillText('当日:' + dt, left, top + 15);

    // 範囲を設定
    var xscale = target.scales["x-axis-0"];
    var yscale = target.scales["y-axis-0"];
    var top = yscale.getPixelForValue(dataarr8[dataarri]) - 1;  // 塗りつぶしを開始するラベル位置
    var bottom = yscale.getPixelForValue(dataarr8[dataarri]) + 1; // 塗りつぶしを終了するラベル位置

    var left = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
    var right = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

    var topX = yscale.top;                      // 塗りつぶしの基点（上端）

    // 塗りつぶす長方形の設定
    ctx4.fillStyle = "rgba(230,0,51,0.8)";
    ctx4.fillRect(left, top, right + 14, 1.5);

    ctx4.font = '14px sans-serif';
    ctx4.textAlign = 'center';
    ctx4.fillRect(left + 10, top - 35, 140, 20);
    ctx4.fillStyle = "WHITE";
    ctx4.fillText('収容可能数：' + dataarr8[dataarri] + '千台', left + 80, topX - 25);

    ctx4.save();

    // 範囲を設定
    var xscale = target.scales["x-axis-0"];
    var yscale = target.scales["y-axis-0"];
    var top2 = yscale.getPixelForValue(dataarr8[dataarri]) - 1;  // 塗りつぶしを開始するラベル位置
    var bottom2 = yscale.getPixelForValue(dataarr8[dataarri]) + 1; // 塗りつぶしを終了するラベル位置

    var left2 = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
    var right2 = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

    // 塗りつぶす長方形の設定
    ctx4.fillStyle = "rgba(230,0,51,0.8)";
    ctx4.fillRect(left2 + 10, top2 - 30, 1.5, 30);
    ctx4.save();

    ctx4.font = '19px sans-serif';
    ctx4.textAlign = 'center';
    ctx4.fillStyle = "rgba(230,0,51,0.8)";
    ctx4.fillText('↓', left + 10.5, top - 10);

    ctx4.save();

    // 範囲を設定dataarrc
    var xscale = target.scales["x-axis-0"];
    var yscale = target.scales["y-axis-0"];
    var top = 20;  // 塗りつぶしを開始するラベル位置
    var bottom = 100; // 塗りつぶしを終了するラベル位置

    var left = xscale.getPixelForValue(Labelarr[dataarrc]);             // 塗りつぶしの基点（上端）
    var right = xscale.getPixelForValue(Labelarr[dataarrc]);            // 塗りつぶしの基点（上端）


    if (hanntei03 == '○') {
        ctx4.fillStyle = "rgba(0,0,255,0.7)";
    } else {

    };

    // 塗りつぶす長方形の設定
    ctx4.font = '25px sans-serif';
    ctx4.textAlign = 'center';
    ctx4.fillRect(right - 340, top, 330, 40);
    ctx4.fillStyle = "White";
    ctx4.fillText('判定：' + hanntei03 + " / 余剰：" + (dataarr8[dataarri] - dataarr20[dataarri]) + '千台', left - 180, top + 20);


    ctx4.save();

}

var ctx4 = $("#myChart4").get(0).getContext("2d");

var config4 = {
    type: 'line',
    data: {
        datasets: [{
            data: dataarr17,// エリア下端データセット
            label: "A",
            pointRadius: 0,
            pointHitRadius: 0,
            fill: false,// エリア下端はfalseにしておくと良い
        }, {
            data: dataarr2,//エリア上端データセット
            label: "3F",
            pointRadius: 1,
            pointHitRadius: 2,
            //上端は下端の位置を設定する
            //以下の場合は1つ前を意味する
            fill: "-1",
            backgroundColor: 'rgba(0, 0, 255, 0.8)',// エリアの色はこちらで指定する
        }],
        labels: Labelarr
    },
    plugins: [{
        afterDraw: drawBackground4 // ★
    }],
    options: {
        legend: {
            labels: {

                filter: function (items) {
                    return items.text != 'A';
                    // return items.datasetIndex != 2;
                }
            }
        },
        title: {
            display: true, // タイトルを表示する
            text: 'EXL3F 在庫推移（実績と予測）', // タイトルのテキスト
            fontSize: 20
        },
        responsive: false,
        scales: {                          // 軸設定
            xAxes: [                           // Ｘ軸設定
                {
                    stacked: false, //積み上げ棒グラフにする設定
                    scaleLabel: {                 // 軸ラベル
                        display: false,                // 表示設定
                        //labelString: '',    // ラベル
                        //fontSize: 10                  // フォントサイズ
                    },
                    gridLines: {                   // 補助線
                        display: false,                // 表示設定
                        //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                    },
                    ticks: {                      // 目盛り
                        //fontColor: "red",             // 目盛りの色
                        //stepSize: 10,                   // 軸間隔
                        fontSize: 14                  // フォントサイズ
                    }
                }
            ],
            yAxes: [                           // Ｙ軸設定
                  {
                      stacked: true, //積み上げ棒グラフにする設定
                      scaleLabel: {                  // 軸ラベル
                          display: true,                 // 表示の有無
                          labelString: '単位：千台',     // ラベル
                          fontFamily: "sans-serif",
                          fontColor: "black",             // 文字の色
                          fontFamily: "sans-serif",
                          fontSize: 18                   // フォントサイズ
                      },
                      gridLines: {                   // 補助線
                          color: "rgba(0, 0, 0, 0.2)", // 補助線の色
                          //zeroLineColor: "black"         // y=0（Ｘ軸の色）
                      },
                      ticks: {                       // 目盛り
                          min: 40,                        // 最小値
                          max: 80,                       // 最大値
                          //stepSize: 50000,                   // 軸間隔
                          //callback: function(label, index, labels) { /* ここです */
                          //    return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') ;
                          //},

                          fontColor: "rgba(0, 0, 0, 0.8)",             // 目盛りの色
                          fontSize: 15                   // フォントサイズ
                      }
                  }
            ]
        }
    }
};

'========================================================================================================================'

'========================================================================================================================'

function drawBackground5(target) {
    // 範囲を設定
    var xscale = target.scales["x-axis-0"];
    var yscale = target.scales["y-axis-0"];
    var left = xscale.getPixelForValue(dt) - 1;  // 塗りつぶしを開始するラベル位置
    var right = xscale.getPixelForValue(dt) + 1; // 塗りつぶしを終了するラベル位置

    var top = yscale.top;                      // 塗りつぶしの基点（上端）


    // 塗りつぶす長方形の設定
    ctx5.fillStyle = "rgba(230,0,51,0.8)";
    ctx5.fillRect(left, top, right - left, yscale.height);
    ctx5.save();

    ctx5.font = '14px sans-serif';
    ctx5.textAlign = 'center';
    ctx5.fillRect(left - 60, top + 0, 121, 20);
    ctx5.fillStyle = "White";
    ctx5.fillText('当日:' + dt, left, top + 10);


    // 範囲を設定
    var xscale = target.scales["x-axis-0"];
    var yscale = target.scales["y-axis-0"];
    var top = yscale.getPixelForValue(dataarr9[dataarri]) - 1;  // 塗りつぶしを開始するラベル位置
    var bottom = yscale.getPixelForValue(dataarr9[dataarri]) + 1; // 塗りつぶしを終了するラベル位置

    var left = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
    var right = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

    var topX = yscale.top;                      // 塗りつぶしの基点（上端）

    // 塗りつぶす長方形の設定
    ctx5.fillStyle = "rgba(230,0,51,0.8)";
    ctx5.fillRect(left, top, right + 14, 1.5);

    ctx5.font = '14px sans-serif';
    ctx5.textAlign = 'center';
    ctx5.fillRect(left + 10, top - 110, 140, 20);
    ctx5.fillStyle = "WHITE";
    ctx5.fillText('収容可能数：' + dataarr9[dataarri] + '千台', left + 80, topX - 14);

    ctx5.save();


    // 範囲を設定
    var xscale = target.scales["x-axis-0"];
    var yscale = target.scales["y-axis-0"];
    var top2 = yscale.getPixelForValue(dataarr9[dataarri]) - 1;  // 塗りつぶしを開始するラベル位置
    var bottom2 = yscale.getPixelForValue(dataarr9[dataarri]) + 1; // 塗りつぶしを終了するラベル位置

    var left2 = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
    var right2 = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

    // 塗りつぶす長方形の設定
    ctx5.fillStyle = "rgba(230,0,51,0.8)";
    ctx5.fillRect(left2 + 10, top2 - 105, 1.5, 105);
    ctx5.save();

    ctx5.font = '19px sans-serif';
    ctx5.textAlign = 'center';
    ctx5.fillStyle = "rgba(230,0,51,0.8)";
    ctx5.fillText('↓', left + 10.5, top - 10);

    ctx5.save();

    // 範囲を設定dataarrc
    var xscale = target.scales["x-axis-0"];
    var yscale = target.scales["y-axis-0"];
    var top = 20;  // 塗りつぶしを開始するラベル位置
    var bottom = 100; // 塗りつぶしを終了するラベル位置

    var left = xscale.getPixelForValue(Labelarr[dataarrc]);             // 塗りつぶしの基点（上端）
    var right = xscale.getPixelForValue(Labelarr[dataarrc]);            // 塗りつぶしの基点（上端）



    if (hanntei04 == '○') {
        ctx5.fillStyle = "rgba(0,0,255,0.7)";
    } else {

    };

    // 塗りつぶす長方形の設定
    ctx5.font = '25px sans-serif';
    ctx5.textAlign = 'center';
    ctx5.fillRect(right - 340, top, 330, 40);
    ctx5.fillStyle = "White";
    ctx5.fillText('判定：' + hanntei04 + " / 余剰：" + (dataarr9[dataarri] - dataarr21[dataarri]) + '千台', left - 180, top + 20);


    ctx5.save();

}

var ctx5 = $("#myChart5").get(0).getContext("2d");

var config5 = {
    type: 'line',
    data: {
        datasets: [{
            data: dataarr17,// エリア下端データセット
            label: "A",
            pointRadius: 0,
            pointHitRadius: 0,
            fill: false,// エリア下端はfalseにしておくと良い
        }, {
            data: dataarr3,//エリア上端データセット
            label: "4F",
            pointRadius: 1,
            pointHitRadius: 2,
            //上端は下端の位置を設定する
            //以下の場合は1つ前を意味する
            fill: "-1",
            backgroundColor: 'rgba(255, 165, 0, 0.8)',// エリアの色はこちらで指定する
        }, {
            data: dataarr6,//エリア上端データセット
            label: "外部倉庫",
            pointRadius: 1,
            pointHitRadius: 2,

            //上端は下端の位置を設定する
            //以下の場合は1つ前を意味する
            fill: "-1",
            backgroundColor: 'rgba(0, 0, 0, 0.2)',// エリアの色はこちらで指定する
        }],
        labels: Labelarr
    },
    plugins: [{
        afterDraw: drawBackground5 // ★
    }],
    options: {
        legend: {
            labels: {
                filter: function (items) {
                    return items.text != 'A';
                    // return items.datasetIndex != 2;
                }
            }
        },
        title: {
            display: true, // タイトルを表示する
            text: 'EXL4F 在庫推移（実績と予測）', // タイトルのテキスト
            fontSize: 20
        },
        responsive: false,
        scales: {                          // 軸設定
            xAxes: [                           // Ｘ軸設定
                {
                    stacked: false, //積み上げ棒グラフにする設定
                    scaleLabel: {                 // 軸ラベル
                        display: false,                // 表示設定
                        //labelString: '',    // ラベル
                        //fontSize: 10                  // フォントサイズ
                    },
                    gridLines: {                   // 補助線
                        display: false,                // 表示設定
                        //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                    },
                    ticks: {                      // 目盛り
                        //fontColor: "red",             // 目盛りの色

                        //stepSize: 10,                   // 軸間隔
                        fontSize: 14                  // フォントサイズ
                    }
                }
            ],
            yAxes: [                           // Ｙ軸設定
                  {
                      stacked: true, //積み上げ棒グラフにする設定
                      scaleLabel: {                  // 軸ラベル
                          display: true,                 // 表示の有無
                          labelString: '単位：千台',     // ラベル
                          fontFamily: "sans-serif",
                          fontColor: "black",             // 文字の色
                          fontFamily: "sans-serif",
                          fontSize: 18                   // フォントサイズ
                      },
                      gridLines: {                   // 補助線
                          color: "rgba(0, 0, 0, 0.2)", // 補助線の色
                          //zeroLineColor: "black"         // y=0（Ｘ軸の色）
                      },
                      ticks: {                       // 目盛り
                          min: 100,                        // 最小値
                          max: 240,                       // 最大値
                          //stepSize: 50000,                   // 軸間隔
                          //callback: function(label, index, labels) { /* ここです */
                          //    return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') ;
                          //},

                          fontColor: "rgba(0, 0, 0, 0.8)",             // 目盛りの色
                          fontSize: 15                   // フォントサイズ
                      }
                  }
            ]
        }
    }
};

'========================================================================================================================'

                function drawBackground2(target) {
                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var left = xscale.getPixelForValue(dt) - 1;  // 塗りつぶしを開始するラベル位置
                    var right = xscale.getPixelForValue(dt) + 1; // 塗りつぶしを終了するラベル位置

                    var top = yscale.top;                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx2.fillStyle = "rgba(230,0,51,0.8)";
                    ctx2.fillRect(left, top, right - left, yscale.height);
                    ctx2.save();

                    ctx2.font = '14px sans-serif';
                    ctx2.textAlign = 'center';
                    ctx2.fillRect(left - 60, top + 30, 121 , 20);
                    ctx2.fillStyle = "White";
                    ctx2.fillText('当日:' + dt, left, top + 40);
                }

                //var ctx2 = $("#myChart2").get(0).getContext("2d");

                //var config2 = {
                //    type: 'line',
                //    data: {

                //        datasets: [{
                //            data: dataarr17,// エリア下端データセット
                //            label: "A",
                //            pointRadius: false,
                //            pointHitRadius: false,
                //            fill: false,// エリア下端はfalseにしておくと良い
                //        }, {
                //            data: dataarr13,//エリア上端データセット
                //            label: "溢れパレット枚数",
                //            pointRadius: 1,
                //            pointHitRadius: 2,
                //            //上端は下端の位置を設定する
                //            //以下の場合は1つ前を意味する
                //            fill: "-1",
                //            backgroundColor: 'rgba(255, 0, 0, 0.5)',// エリアの色はこちらで指定する
                //        }],
                //        labels: Labelarr
                //    },
                //    plugins: [{
                //        afterDraw: drawBackground2 // ★

                //    }],
                //    options: {
                //        legend: {
                //            labels: {

                //                filter: function (items) {
                //                    return items.text != 'A';
                //                    // return items.datasetIndex != 2;
                //                }
                //            }
                //        },
                //        title: {
                //            display: true, // タイトルを表示する
                //            text: '概算溢れパレット枚数（実績と予測）', // タイトルのテキスト
                //            fontSize: 20
                //        },
                //        responsive: false,
                //        scales: {                          // 軸設定
                //            xAxes: [                           // Ｘ軸設定
                //                {
                //                    stacked: false, //積み上げ棒グラフにする設定
                //                    scaleLabel: {                 // 軸ラベル
                //                        display: false,                // 表示設定
                //                        //labelString: '',    // ラベル
                //                        //fontSize: 10                  // フォントサイズ
                //                    },
                //                    gridLines: {                   // 補助線
                //                        display: false,                // 表示設定
                //                        //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                //                    },
                //                    ticks: {                      // 目盛り
                //                        //fontColor: "red",             // 目盛りの色

                //                        //stepSize: 10,                   // 軸間隔
                //                        fontSize: 14                  // フォントサイズ
                //                    }
                //                }
                //            ],
                //            yAxes: [                           // Ｙ軸設定
                //                  {
                //                      stacked: true, //積み上げ棒グラフにする設定
                //                      scaleLabel: {                  // 軸ラベル
                //                          display: true,                 // 表示の有無
                //                          labelString: '単位：枚',     // ラベル
                //                          fontFamily: "sans-serif",
                //                          fontColor: "black",             // 文字の色
                //                          fontFamily: "sans-serif",
                //                          fontSize: 18                   // フォントサイズ
                //                      },
                //                      gridLines: {                   // 補助線
                //                          color: "rgba(0, 0, 0, 0.2)", // 補助線の色
                //                          //zeroLineColor: "black"         // y=0（Ｘ軸の色）
                //                      },
                //                      ticks: {                       // 目盛り
                //                          min: 0,                        // 最小値
                //                          //max: 20,                       // 最大値
                //                          stepSize: 100,                   // 軸間隔
                //                          //callback: function(label, index, labels) { /* ここです */
                //                          //    return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') ;
                //                          //},

                //                          fontColor: "rgba(0, 0, 0, 0.8)",             // 目盛りの色
                //                          fontSize: 15                   // フォントサイズ
                //                      }
                //                  }
                //            ]
                //        }
                //    }
                //};

                ctx.canvas.height = 500;
                ctx.canvas.width = 1250;
                //ctx2.canvas.height = 300;
                //ctx2.canvas.width = 1250;
                ctx3.canvas.height = 300;
                ctx3.canvas.width = 1250;
                ctx4.canvas.height = 300;
                ctx4.canvas.width = 1250;
                ctx5.canvas.height = 300;
                ctx5.canvas.width = 1250;

                var myPieChart = new Chart(ctx, config);
                //var myPieChart2 = new Chart(ctx2, config2);
                var myPieChart3 = new Chart(ctx3, config3);
                var myPieChart4 = new Chart(ctx4, config4);
                var myPieChart5 = new Chart(ctx5, config5);

            }
            function OnErrorCall_(response) {
                window.alert('エラーです');
            }
            e.preventDefault();
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
    <table>
        <tr>
            <td style="width: 450px; font-size: 25px;">
                <h2>EXL在庫推移グラフ（実績と予測）</h2>
            </td>
        </tr>
    </table>
        
    <table border ="1" class ="BB">
        <tr>
            <asp:Button ID="Button1" runat="server" Text="明細ダウンロード" Visible="false" />
            <a href="./m_graph_stock_detail.aspx">コメント追加</a>
        </tr>
    </table>

    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">
        <table>
            <tr>
                <canvas id="myChart" ></canvas>
            </tr>
        </table>  
        <div style="text-align: center;">  
            <table>
                <tr>
                     <asp:Label ID="Label1" runat="server" Text="aaa" style="background:red;font-size:18px;color:white" ></asp:Label>
                </tr>
            </table>   
        </div>  

        <div class="wrapper">
        <table class="sticky" >
        <thead class="fixed">

        </thead>

        <tbody>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width = "1270px" DataSourceID="SqlDataSource4" DataKeyNames="PLACE" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >
        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
        <Columns>

        <asp:BoundField DataField="INS_DATE" HeaderText="追加日" SortExpression="INS_DATE"　>
            <HeaderStyle Width="70px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="PLACE" HeaderText="区分" SortExpression="PLACE" ReadOnly="true" >
            <HeaderStyle Width="30px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="COMMENT" HeaderText="コメント" SortExpression="COMMENT" ReadOnly="true" >
            <HeaderStyle Width="1000px" />
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

</div>

<div id="contents2" class="inner2">

    <table style="height:50px">
    </table>  

    <asp:Panel ID="Panel3" runat="server"  Font-Size="12px">
        <table>
            <tr>
                <canvas id="myChart3" ></canvas>
            </tr>
        </table> 
        <div style="text-align: center;">  
            <table>
                <tr>
                        <asp:Label ID="Label2" runat="server" Text="aaa" style="background:red;font-size:18px;color:white" ></asp:Label>
                </tr>
            </table>  
        </div>     



        <div class="wrapper">
        <table class="sticky" >
        <thead class="fixed">

        </thead>

        <tbody>

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width = "1270px" DataSourceID="SqlDataSource1" DataKeyNames="PLACE" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >
        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
        <Columns>

        <asp:BoundField DataField="INS_DATE" HeaderText="追加日" SortExpression="INS_DATE"　>
            <HeaderStyle Width="70px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="PLACE" HeaderText="区分" SortExpression="PLACE" ReadOnly="true" >
            <HeaderStyle Width="30px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="COMMENT" HeaderText="コメント" SortExpression="COMMENT" ReadOnly="true" >
            <HeaderStyle Width="1000px" />
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

</div>

<div id="contents2" class="inner2">

    <table style="height:50px">
    </table>  

    <asp:Panel ID="Panel4" runat="server"  Font-Size="12px">
        <table>
            <tr>
                <canvas id="myChart4" ></canvas>
            </tr>
        </table>   
        <div style="text-align: center;">  
            <table>
                <tr>
                        <asp:Label ID="Label3" runat="server" Text="aaa" style="background:red;font-size:18px;color:white" ></asp:Label>
                </tr>
            </table>   
        </div>     
        
        

        <div class="wrapper">
        <table class="sticky" >
        <thead class="fixed">

        </thead>

        <tbody>

        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width = "1270px" DataSourceID="SqlDataSource2" DataKeyNames="PLACE" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >
        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
        <Columns>

        <asp:BoundField DataField="INS_DATE" HeaderText="追加日" SortExpression="INS_DATE"　>
            <HeaderStyle Width="70px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="PLACE" HeaderText="区分" SortExpression="PLACE" ReadOnly="true" >
            <HeaderStyle Width="30px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="COMMENT" HeaderText="コメント" SortExpression="COMMENT" ReadOnly="true" >
            <HeaderStyle Width="1000px" />
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

</div>

<div id="contents2" class="inner2">

    <table style="height:50px">
    </table>  

    <asp:Panel ID="Panel5" runat="server"  Font-Size="12px">
        <table>
            <tr>
                <canvas id="myChart5" ></canvas>
            </tr>
        </table>  
        <div style="text-align: center;">  
            <table>
                <tr>
                        <asp:Label ID="Label4" runat="server" Text="aaa" style="background:red;font-size:18px;color:white" ></asp:Label>
                </tr>
            </table>   
        </div>      



        <div class="wrapper">
        <table class="sticky" >
        <thead class="fixed">

        </thead>

        <tbody>

        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" Width = "1270px" DataSourceID="SqlDataSource3" DataKeyNames="PLACE" BackColor="White" BorderColor="#555555" BorderStyle="None" BorderWidth="3px" CellPadding="3" ShowHeaderWhenEmpty="True" >
        <HeaderStyle BackColor="#326DB6" Font-Bold="True" ForeColor="BLACK"> </HeaderStyle>
        <Columns>

        <asp:BoundField DataField="INS_DATE" HeaderText="追加日" SortExpression="INS_DATE"　>
            <HeaderStyle Width="70px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="PLACE" HeaderText="区分" SortExpression="PLACE" ReadOnly="true" >
            <HeaderStyle Width="30px" />
<%--            <ItemStyle HorizontalAlign="Center" />--%>
            </asp:BoundField>
        <asp:BoundField DataField="COMMENT" HeaderText="コメント" SortExpression="COMMENT" ReadOnly="true" >
            <HeaderStyle Width="1000px" />
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

    <table style="height:50px">
    </table>  


<%--    <asp:Panel ID="Panel2" runat="server"  Font-Size="12px">
        <table>
            <tr>
                <canvas id="myChart2" ></canvas>
            </tr>
        </table>  
       
    </asp:Panel>--%>

</div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [INS_DATE], [PLACE], [COMMENT] FROM [T_EXL_GRAPH_STOCK_COM] WHERE PLACE = '1F' ORDER BY INS_DATE DESC "
    ></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [INS_DATE], [PLACE], [COMMENT] FROM [T_EXL_GRAPH_STOCK_COM] WHERE PLACE = '3F' ORDER BY INS_DATE DESC"
    ></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [INS_DATE], [PLACE], [COMMENT] FROM [T_EXL_GRAPH_STOCK_COM] WHERE PLACE = '4F' ORDER BY INS_DATE DESC"
    ></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:EXPDBConnectionString %>" SelectCommand="SELECT [INS_DATE], [PLACE], [COMMENT] FROM [T_EXL_GRAPH_STOCK_COM] WHERE PLACE = '全' ORDER BY INS_DATE DESC"
    ></asp:SqlDataSource>

    <!--/#contents2-->

    <!--ページの上部に戻る「↑」ボタン-->
    <p class="nav-fix-pos-pagetop">
        <a href="#">↑</a>
    </p>

</form>
</body>

</html>
