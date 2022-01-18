<!DOCTYPE html>
<html lang="ja">
<head>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>手順書（海外）</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="css/style.css">
<script src="js/openclose.js"></script>
<script src="js/fixmenu.js"></script>
<script src="js/fixmenu_pagetop.js"></script>
<script src="js/ddmenu_min.js"></script>
<script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
<script src="js/default.js"></script>
<script>
    // メニュークリック時の画面遷移
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
                window.location.href = './test.aspx?id=' + encodeURIComponent(arr[1]);
                return false;
            };
        });
    });
    // エクセルファイルオープン
    function linkclick() {
        //var text = $(this).data('id');
        //window.alert(text);
        window.alert("koko");
        wshshell = new ActiveXObject("WScript.Shell")
        window.alert("koko1");
        wshshell.run("\\\\svnas201\\exd06100\\COMMON\\生産管理本部\\ＣＳチーム\\手順書\\新規_手順書\\2019年度手順書整備\\02.承認待ち\\手順書_【国内】LS5売上確認.xlsx")
//        wshshell.run("\\\\svnas201\\exd06100\\COMMON\\生産管理本部\\ＣＳチーム\\手順書\\新規_手順書\\2019年度手順書整備\\02.承認待ち\\" + text)

//        excel.Workbooks.Open("\\\\svnas201\\exd06100\\COMMON\\生産管理本部\\ＣＳチーム\\手順書\\新規_手順書\\2019年度手順書整備\\02.承認待ち\\" + text, 0, true);
        excel.Visible = true;
    }

    $('.exlink ').click(function () {
        window.alert("koko");
        var text = $('a', this).data('id')
        window.alert(text);
    })

</script>
</head>

<body class="c2">

<form id="form1" runat="server">

<!--PC用（901px以上端末）メニュー-->
<!-- インクルードファイルの指定 -->
<!-- メニューの編集はheader.htmlで行う -->
    <!-- #Include File="header.html" -->

<!--小さな端末用（900px以下端末）メニュー-->
<!--<nav id="menubar-s">
<ul>
<li><a href="./">ホーム<span>Home</span></a></li>
<li><a href="list_base.html">情報一覧<span>Category</span></a></li>
<li><a href="info.html">掲載のご案内<span>Information</span></a></li>
<li><a href="faq.html">よく頂く質問<span>Faq</span></a></li>
<li><a href="link.html">リンク<span>Link</span></a></li>
<li><a href="./?act=toiawase">お問い合わせ<span>Contact</span></a></li>
</ul>
</nav>-->

<div id="contents" class="inner">
<div id="contents-in">

<div id="main">

<section>

<h2>状況</h2>
<table class="ta2">
<tr>
    <td class ="exlink"><a href="javascript:linkclick()"  data-excel="手順書_AIR見積り.xlsx">AIR見積り</a></td>
    <td>AIR書類作成</td>
    <td>EAC向け未納リスト送付</td>
    <td>AT_ブッキング</td>
</tr>
<tr>
    <td>BLドラフト確認(ｵﾘｼﾞﾅﾙ以外)</td>
    <td>BLドラフト確認</td>
    <td>BL回収進捗確認</td>
    <td>BL処理 E-MAIL</td>
</tr>
<tr>
    <td>BL処理 FAX</td>
    <td>BL処理 郵便等</td>
    <td>CCISデータ取得、送付</td>
    <td>CCIS日別データ取得</td>
</tr>
</table>

</section>

</div>
<!--/#main-->

<div id="sub">

<div class="box1">
    <h2 class="mb10">状　　況</h2>
    <!-- <asp:Label class="lblSituation" runat="server" Text="No Good" ></asp:Label>　-->
    <asp:Image class="imgOKNG" runat="server" src="images/NGtouka.png" style="margin-top:8px;margin-left:70px;" />
</div>

<nav>
<label for="menu_bar01">情報一覧</label>
<input type="checkbox" id="menu_bar01" />
<ul class="submenu" id="links01">
    <li data-id="1-1"><a href="">受注平準化</a></li>
    <li data-id="http://kbhwpm01/exp/booking/booking.aspx"><a href="">ブッキング情報</a></li>
    <li data-id="2-1"><a href="">コンテナ平準化</a></li>
    <li data-id="2-1"><a href="">品揃え状況</a></li>
    <li data-id="http://kbhwpm01/exp/request_Home_CC.htm"><a href="">EXL-工場 必要リスト(CC)</a></li>
    <li data-id="http://kbhwpm01/exp/request_Home_CD.htm"><a href="">EXL-工場 必要リスト(CD)</a></li>
    <li data-id="http://kbhwpm01/exp/4f_floor.htm"><a href="">EXL4Fフロア状況</a></li>
    <li data-id="2-1"><a href="">バンニング平準化</a></li>
    <li data-id="2-1"><a href="">本社 積載率</a></li>
    <li data-id="2-1"><a href="">本社 積載率(全客先)</a></li>
    <li data-id="http://kbhwpm01/exp/Vanrep_グラフ01.htm"><a href="">本社_CO2排出量・積載率</a></li>
    <li data-id="2-1"><a href="">出荷進歩</a></li>
    <li data-id="http://kbhwpm01/exp/index6.html"><a href="">マリントラフィック</a></li>
</ul>
</nav>

<nav>
<label for="menu_bar02">ＣＳ業務</label>
<input type="checkbox" id="menu_bar02" />
<ul class="submenu" id="links02">
    <li><a href="./?act=list&kind=1">本日のバンニング予定</a></li>
    <li><a href="./?act=list&kind=2">本日の通関予定</a></li>
    <li><a href="./?act=list&kind=2">受注処理</a></li>
    <li><a href="./?act=list&kind=2">ＥＰＡ申請状況</a></li>
</ul>
</nav>

</div>
<!--/#sub-->

</div>
<!--/#contents-in-->

<div id="side">

</div>
<!--/#side-->

</div>
<!--/#contents-->

<footer>

<div id="footermenu" class="inner">

</div>
<!--/footermenu-->

<div id="copyright">
</div>

</footer>

<!--ページの上部に戻る「↑」ボタン-->
<p class="nav-fix-pos-pagetop"><a href="#">↑</a></p>

<!--メニュー開閉ボタン-->
<div id="menubar_hdr" class="close"></div>
<!--メニューの開閉処理条件設定　900px以下-->
<script>
if (OCwindowWidth() <= 900) {
	open_close("menubar_hdr", "menubar-s");
}
</script>

    </form>

</body>
</html>
