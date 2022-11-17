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
                    Labelarr.push(val.label);
                    //Colorarr.push(val.color);
                });

                //window.alert(Labelarr[1]);

                //window.alert(dataarr5[1]);
            


                function drawBackground(target) {
                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var left = xscale.getPixelForValue(dt)-1;  // 塗りつぶしを開始するラベル位置
                    var right = xscale.getPixelForValue(dt)+1; // 塗りつぶしを終了するラベル位置

                    var top = yscale.top;                      // 塗りつぶしの基点（上端）


                    // 塗りつぶす長方形の設定
                    ctx.fillStyle = "rgba(230,0,51)";
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
                    var top = yscale.getPixelForValue(dataarr5[1])-1;  // 塗りつぶしを開始するラベル位置
                    var bottom = yscale.getPixelForValue(dataarr5[1])+1; // 塗りつぶしを終了するラベル位置

                    var left = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
                    var right = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx.fillStyle = "rgba(230,0,51)";
                    ctx.fillRect(left, top, right+14, 1.5);


                    ctx.font = '14px sans-serif';
                    ctx.textAlign = 'center';
                    ctx.fillRect(left +10, top-165, 140, 20);
                    ctx.fillStyle = "White";
                    ctx.fillText('収容可能数：' + dataarr5[1] + '千台', left + 80, top - 155);

                    ctx.save();

                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var top2 = yscale.getPixelForValue(dataarr5[1]) - 1;  // 塗りつぶしを開始するラベル位置
                    var bottom2 = yscale.getPixelForValue(dataarr5[1]) + 1; // 塗りつぶしを終了するラベル位置

                    var left2 = xscale.getPixelForValue(Labelarr[0]);                      // 塗りつぶしの基点（上端）
                    var right2 = xscale.getPixelForValue(Labelarr[Labelarr.length - 4]);                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx.fillStyle = "rgba(230,0,51)";
                    ctx.fillRect(left2 + 10, top2 - 165, 1.5, 165);
                    ctx.save();


                    ctx.font = '19px sans-serif';
                    ctx.textAlign = 'center';
                    ctx.fillStyle = "rgba(230,0,51)";
                    ctx.fillText('↓', left +10.5, top - 10);

                    ctx.save();

                }


                var ctx = $("#myChart").get(0).getContext("2d");


                var config = {
                    type: 'line',
                    data: {


                        datasets: [{
                            data: dataarr6,// エリア下端データセット
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
                            fontSize: 18
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
                                          //max: 20,                       // 最大値
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






                function drawBackground2(target) {
                    // 範囲を設定
                    var xscale = target.scales["x-axis-0"];
                    var yscale = target.scales["y-axis-0"];
                    var left = xscale.getPixelForValue(dt) - 1;  // 塗りつぶしを開始するラベル位置
                    var right = xscale.getPixelForValue(dt) + 1; // 塗りつぶしを終了するラベル位置

                    var top = yscale.top;                      // 塗りつぶしの基点（上端）

                    // 塗りつぶす長方形の設定
                    ctx2.fillStyle = "rgba(230,0,51)";
                    ctx2.fillRect(left, top, right - left, yscale.height);
                    ctx2.save();

                    ctx2.font = '14px sans-serif';
                    ctx2.textAlign = 'center';
                    ctx2.fillRect(left - 60, top + 30, 121 , 20);
                    ctx2.fillStyle = "White";
                    ctx2.fillText('当日' + dt, left, top + 40);





                }


                var ctx2 = $("#myChart2").get(0).getContext("2d");


                var config2 = {
                    type: 'line',
                    data: {

                        datasets: [{
                            data: dataarr6,// エリア下端データセット
                            label: "A",
                            pointRadius: false,
                            pointHitRadius: false,
                            fill: false,// エリア下端はfalseにしておくと良い
                        }, {
                            data: dataarr7,//エリア上端データセット
                            label: "溢れパレット枚数",
                            pointRadius: 1,
                            pointHitRadius: 2,
                            //上端は下端の位置を設定する
                            //以下の場合は1つ前を意味する
                            fill: "-1",
                            backgroundColor: 'rgba(255, 0, 0, 0.5)',// エリアの色はこちらで指定する
                        }],
                        labels: Labelarr
                    },
                    plugins: [{
                        afterDraw: drawBackground2 // ★

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
                            text: '概算溢れパレット枚数（実績と予測）', // タイトルのテキスト
                            fontSize: 18
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
                                          labelString: '単位：枚',     // ラベル
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
                                          min: 0,                        // 最小値
                                          //max: 20,                       // 最大値
                                          stepSize: 100,                   // 軸間隔
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

                ctx.canvas.height = 580;
                ctx.canvas.width = 1250;
                ctx2.canvas.height = 580;
                ctx2.canvas.width = 1250;

                var myPieChart = new Chart(ctx, config);
                var myPieChart2 = new Chart(ctx2, config2);


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
    <table>
        <tr>
            <td style="width: 450px; font-size: 25px;">
                <h2>EXL在庫推移グラフ（実績と予測）</h2>
            </td>
        </tr>
    </table>
        

    <asp:Panel ID="Panel1" runat="server"  Font-Size="12px">
        <table>
            <tr>
                <canvas id="myChart" ></canvas>
            </tr>
        </table>     
    </asp:Panel>




    <asp:Panel ID="Panel2" runat="server"  Font-Size="12px">
        <table>
            <tr>
                <canvas id="myChart2" ></canvas>
            </tr>
        </table>     
    </asp:Panel>


</div>


    <!--/#contents2-->

    <!--ページの上部に戻る「↑」ボタン-->
    <p class="nav-fix-pos-pagetop">
        <a href="#">↑</a>
    </p>



</form>
</body>



</html>
