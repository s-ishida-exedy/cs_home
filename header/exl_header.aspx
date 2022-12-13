<ul class="menu2">
    <li data-id='["home","exl_top.aspx"]'><a href="#">ホーム<span>Home</span></a></li>
    <li class="menu__multi">
        <a href="#" class="init-bottom">情報一覧<span>Information</span></a>
        <ul class="menu__second-level">
            <!-- 第一階層 -->
            <li data-id='["home","make_graph_odr.aspx"]'><a href="#">受注台数</a></li>
            <li data-id='["home","make_graph_con.aspx"]'><a href="#">コンテナ本数(AF)</a></li>
            <li data-id='["home","booking.aspx"]'><a href="#">ブッキングシート</a></li>
            <li data-id='["home","make_graph_nizoroi.aspx"]'><a href="#">荷揃え状況(AF)</a></li>
            <li data-id='["home","4f_floor.aspx"]'><a href="#">４Ｆフロア状況</a></li>
            <li data-id='["aaa",""]'>
                <a href="#" class="init-right">バンニング</a>
                <ul class="menu__third-level">
                    <!-- 第二階層 -->
                    <li data-id='["","http://kbhwpm01/exp/display/60days_van.htm"]'><a href="#">60日先バンニング本数推移</a></li>
                    <li data-id='["home","van_sche.aspx"]'><a href="#">バンニングスケジュール</a></li>
                    <li data-id='["home","van_result.aspx"]'><a href="#">本社バンニング結果</a></li>
                </ul>
            </li>
            <li data-id='["home","make_graph_sintyoku.aspx"]'><a href="#">出荷進捗</a></li>
            <li data-id='["","http://kbhwpm01/exp/display/loading_efficiency_and_co2_emission.htm"]'><a href="#">本社_CO2排出量・積載率</a></li>
            <li data-id='["aaa",""]'>
                <a href="#" class="init-right">未納</a>
                <ul class="menu__third-level">
                    <!-- 第二階層 -->
                    <li data-id='["home","make_graph_minou01.aspx"]'><a href="#">アフタ未納金額</a></li>
                    <li data-id='["home","make_graph_minou02.aspx"]'><a href="#">ＫＤ未納金額</a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li class="menu__multi">
        <a href="#" class="init-bottom">業務<span>Work</span></a>
        <ul class="menu__second-level">
            <!-- 第一階層 -->
            <li data-id='["home","eir_comfirm.aspx"]'><a href="#">EIR、Booking情報差異連絡</a></li>
            <li data-id='["home","eir_comfirm_yard.aspx"]'><a href="#">EIR、Booking情報差異確認(ﾔｰﾄﾞ)</a></li>
            <li data-id='["home","lcl_tenkai.aspx"]'><a href="#">LCL展開済案件</a></li>
            <li data-id='["home","anken_booking02.aspx"]'><a href="#">LS7,9「試作限定」有無確認</a></li>
            <li data-id='["home","ivhd_request.aspx"]'><a href="#">追加ｲﾝﾎﾞｲｽﾍｯﾀﾞ作成依頼</a></li>
            <li data-id='["home","anken_kanryo.aspx"]'><a href="#">完了報告</a></li>
        </ul>
    </li>
    <li class="menu__multi">
        <a href="#" class="init-bottom">システム<span>System</span></a>
        <ul class="menu__second-level">
            <!-- 第一階層 -->
            <li data-id='["home","logoff.aspx"]'><a href="#">ログオフ</a></li>
        </ul>
    </li>
    <li class="menu__multi">
        <%  Dim strUsr As String = Session("UsrId") %>
        <%  Dim strNam As String = Session("UsrName") %>
        <%  Dim strRole As String = Session("Role") %>
        <a href="#" class="init-bottom">ログインユーザー情報<span>Login User</span></a>
        <ul class="menu__second-level">
            <!-- 第一階層 -->
            <li><a href="#"><%= strUsr %></a></li>
            <li><a href="#"><%= strNam %></a></li>
            <li><a href="#"><%= strRole %></a></li>
        </ul>
    </li>
    <li class="menu__multi">
    </li>
</ul>
