<%@ Page Language="VB" AutoEventWireup="false" CodeFile="make_graph_nizoroi.aspx.vb" Inherits="yuusen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>荷揃え</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="css/style.css" />
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

    <%--    <script>
    Chart.plugins.register({
    afterDatasetsDraw: function (chart, easing) {
        // To only draw at the end of animation, check for easing === 1
        var ctx = chart.ctx;

        chart.data.datasets.forEach(function (dataset, i) {
            var meta = chart.getDatasetMeta(i);
            if (!meta.hidden) {
                meta.data.forEach(function (element, index) {
                    // Draw the text in black, with the specified font
                    ctx.fillStyle = 'rgb(0, 0, 0)';

                    var fontSize = 16;
                    var fontStyle = 'normal';
                    var fontFamily = 'Helvetica Neue';
                    ctx.font = Chart.helpers.fontString(fontSize, fontStyle, fontFamily);

                    // Just naively convert to string for now
                    var dataString = dataset.data[index].toString();

                    // Make sure alignment settings are correct
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'middle';

                    var padding = 5;
                    var position = element.tooltipPosition();
                    ctx.fillText(dataString, position.x, position.y - (fontSize / 2) - padding);
                });
            }
        });
    }
});
</script>
    --%>
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
                url: "./make_graph_nizoroi.aspx/getTrafficSourceData",
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
                    dataarr8.push(val.value8);
                    Labelarr.push(val.label);
                    //Colorarr.push(val.color);
                });
                var ctx = $("#myChart").get(0).getContext("2d");
                var config = {
                    type: 'horizontalBar',
                    data: {
                        datasets: [{
                            label: 'VAN済み',
                            type: 'horizontalBar',
                            fill: false,
                            //barThickness: 10,
                            data: dataarr2,
                            backgroundColor: 'rgba(0, 0, 255, 0.5)',
                            borderColor: 'rgb(54, 162, 235)',
                            cubicInterpolationMode: 'monotone',
                        }, {
                            label: '準備済み',
                            type: 'horizontalBar',
                            fill: false,
                            data: dataarr,
                            //barThickness: 10,
                            backgroundColor: 'rgba(0, 128, 0, 0.5)',
                            borderColor: 'rgb(54, 162, 235)',
                            cubicInterpolationMode: 'monotone',

                        }, {
                            label: '入庫済み',
                            type: 'horizontalBar',
                            data: dataarr3,
                            //barThickness: 10,
                            backgroundColor: 'rgba(255, 255, 0, 0.5)',
                            borderColor: 'rgb(54, 162, 235)',
                            cubicInterpolationMode: 'monotone',
                        }, {
                            label: '入庫待ち',
                            type: 'horizontalBar',
                            data: dataarr4,
                            //barThickness: 10,
                            backgroundColor: 'rgba(255, 165, 0, 0.5)',
                            borderColor: 'rgb(54, 162, 235)',
                            cubicInterpolationMode: 'monotone',
                        }],
                        labels: Labelarr

                    },




                    options: {
                        responsive: false,
                        scales: {                          // 軸設定
                            xAxes: [                           // Ｘ軸設定

                                {
                                    stacked: "100", //積み上げ棒グラフにする設定
                                    scaleLabel: {                 // 軸ラベル
                                        display: true,                // 表示設定
                                        labelString: '%',    // ラベル
                                        //fontColor: "red",             // 文字の色
                                        //fontSize: 16                  // フォントサイズ
                                    },
                                    gridLines: {                   // 補助線
                                        //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                                    },
                                    ticks: {                      // 目盛り
                                        //fontColor: "red",             // 目盛りの色
                                        //fontSize: 14                  // フォントサイズ
                                    }
                                }
                            ],
                            yAxes: [                           // Ｙ軸設定

                  {
                      stacked: "100", //積み上げ棒グラフにする設定
                      scaleLabel: {                  // 軸ラベル
                          display: true,                 // 表示の有無

                          //labelString: '受注台数',     // ラベル
                          fontFamily: "sans-serif",
                          fontColor: "blue",             // 文字の色
                          fontFamily: "sans-serif",
                          //fontSize: 16                   // フォントサイズ
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
                          //fontSize: 14                   // フォントサイズ
                      }
                  }
                            ]
                        }
                    }
                };







                var ctx2 = $("#myChart2").get(0).getContext("2d");
                var config2 = {
                    type: 'horizontalBar',
                    data: {
                        datasets: [{
                            label: 'VAN済み',
                            type: 'horizontalBar',
                            fill: false,
                            //barThickness: 10,
                            data: dataarr6,
                            backgroundColor: 'rgba(0, 0, 255, 0.5)',
                            borderColor: 'rgb(54, 162, 235)',
                            cubicInterpolationMode: 'monotone',
                        }, {
                            label: '準備済み',
                            type: 'horizontalBar',
                            fill: false,
                            data: dataarr5,
                            //barThickness: 10,
                            backgroundColor: 'rgba(0, 128, 0, 0.5)',
                            borderColor: 'rgb(54, 162, 235)',
                            cubicInterpolationMode: 'monotone',

                        }, {
                            label: '入庫済み',
                            type: 'horizontalBar',
                            data: dataarr7,
                            //barThickness: 10,
                            backgroundColor: 'rgba(255, 255, 0, 0.5)',
                            borderColor: 'rgb(54, 162, 235)',
                            cubicInterpolationMode: 'monotone',
                        }, {
                            label: '入庫待ち',
                            type: 'horizontalBar',
                            data: dataarr8,
                            //barThickness: 10,
                            backgroundColor: 'rgba(255, 165, 0, 0.5)',
                            borderColor: 'rgb(54, 162, 235)',
                            cubicInterpolationMode: 'monotone',
                        }],
                        labels: Labelarr

                    },




                    options: {
                        responsive: false,
                        scales: {                          // 軸設定
                            xAxes: [                           // Ｘ軸設定

                                {
                                    stacked: true, //積み上げ棒グラフにする設定
                                    scaleLabel: {                 // 軸ラベル
                                        display: true,                // 表示設定
                                        labelString: 'パレット',    // ラベル
                                        //fontColor: "red",             // 文字の色
                                        //fontSize: 16                  // フォントサイズ
                                    },
                                    gridLines: {                   // 補助線
                                        //color: "rgba(255, 0, 0, 0.2)", // 補助線の色
                                    },
                                    ticks: {                      // 目盛り
                                        //fontColor: "red",             // 目盛りの色
                                        //fontSize: 14                  // フォントサイズ
                                    }
                                }
                            ],
                            yAxes: [                           // Ｙ軸設定

                  {
                      stacked: true, //積み上げ棒グラフにする設定
                      scaleLabel: {                  // 軸ラベル
                          display: true,                 // 表示の有無

                          //labelString: '受注台数',     // ラベル
                          fontFamily: "sans-serif",
                          fontColor: "blue",             // 文字の色
                          fontFamily: "sans-serif",
                          //fontSize: 16                   // フォントサイズ
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
                          //fontSize: 14                   // フォントサイズ
                      }
                  }
                            ]
                        }
                    }
                };




                ctx.canvas.height = 550;
                ctx.canvas.width = 1200;


                ctx2.canvas.height = 550;
                ctx2.canvas.width = 1200;


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
        <!-- メニューの編集はheader.htmlで行う -->
        <!-- #Include File="header.html" -->

        <div id="contents2" class="inner2">
            <table>
                <tr>
                    <td style="width: 450px; font-size: 25px;">
                        <h2>充当率グラフ</h2>
                    </td>
                </tr>
            </table>


<table>
    <tr>
        <td style="width:120px;" >

 

        <asp:Label id="Label7" Text="＜　充当率　＞" Font-Size="15" runat="server"></asp:Label>

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


<table>
    <tr>
        <td style="width:120px;" >

  
        <asp:Label id="Label1" Text="＜　充当数　＞" Font-Size="15" runat="server"></asp:Label>

        </td>
    </tr>
</table>

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
