Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Web.UI.WebControls
Imports System.Activities.Expressions
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call GET_VAN_DATA()
        Dim strStatus As String = ""

        Call GET_STATUS(strStatus)

        Select Case strStatus
            Case "OK"
                Me.OKNGimg.ImageUrl = "images/OKtouka.png"
            Case "NG"
                Me.OKNGimg.ImageUrl = "images/NGtouka.png"
        End Select

        Call GET_TOPICS()
    End Sub

    Private Sub GET_STATUS(ByRef strStatus As String)
        '概況のステータスを取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT *  "
        strSQL = strSQL & "FROM T_EXL_POR_STATUS "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '初期値セット
        strStatus = "OK"

        '１件でもNGあればNGを返す
        While (dataread.Read())
            If Trim(dataread("DATA_OKNG")) = "NG" Then
                strStatus = "NG"
                Exit While
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Sub

    Private Sub GET_VAN_DATA()
        'バンニングスケジュール情報を取得する
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        Dim lngHAf As Long = 0      '本社AF　当日
        Dim lngHKd As Long = 0      '本社KD　当日
        Dim lngHAf2 As Long = 0　　 '本社AF　翌日
        Dim lngHKd2 As Long = 0     '本社KD　翌日
        Dim lngU As Long = 0        '上野　当日
        Dim lngU2 As Long = 0　　　 '上野　翌日
        Dim lngAir As Long = 0      'AIR　当日
        Dim lngAir2 As Long = 0     'AIR　翌日
        Dim lngJishT As Long = 0
        Dim lngJishT2 As Long = 0
        Dim lngTsukI As Long = 0
        Dim StrH As String = ""
        Dim StrU As String = ""
        Dim StrA As String = ""
        Dim StrJ As String = ""
        Dim StrT As String = ""

        Dim intToday As Integer = 0
        Dim strTody As String = Now.ToString("yyyy/MM/dd")
        Dim strTomm As String = GET_SHITEI_EIGYOBI(Now.ToString("yyyy/MM/dd"), 2, "01")

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_VAN_SCH_DETAIL "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Select Case dataread("PLACE")
                Case "0H"
                    If strTody = dataread("VAN_DATE") And Left(Right(Trim(dataread("CUST_NM")), 5), 1) = "K" Then
                        '当日　本社　AF
                        lngHAf += 1
                    ElseIf strTody = dataread("VAN_DATE") And Left(Right(trim(dataread("CUST_NM")), 5), 1) <> "K" Then
                        '当日　本社　KD
                        lngHKd += 1
                    ElseIf strTomm = dataread("VAN_DATE") And Left(Right(trim(dataread("CUST_NM")), 5), 1) = "K" Then
                        '翌日　本社　AF
                        lngHAf2 += 1
                    ElseIf strTomm = dataread("VAN_DATE") And Left(Right(trim(dataread("CUST_NM")), 5), 1) <> "K" Then
                        '翌日　本社　KD
                        lngHKd2 += 1
                    End If
                Case "1U"
                    If strTody = dataread("VAN_DATE") Then
                        '当日　上野
                        lngU += 1
                    ElseIf strTomm = dataread("VAN_DATE") Then
                        '翌日　上野
                        lngU2 += 1
                    End If
                Case "2A"
                    If strTody = dataread("VAN_DATE") Then
                        '当日　AIR
                        lngAir += 1
                    ElseIf strTomm = dataread("VAN_DATE") Then
                        '翌日　AIR
                        lngAir2 += 1
                    End If
            End Select
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '自社通関と通関委託の件数取得
        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_POR_CNT WHERE DATA_CD IN ('011','012','013') "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Select Case dataread("DATA_CD")
                Case "011"
                    lngJishT = dataread("DATA_CNT")
                Case "012"
                    lngJishT2 = dataread("DATA_CNT")
                Case "013"
                    lngTsukI = dataread("DATA_CNT")
            End Select
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        Literal1.Text = StrConv(lngHAf, VbStrConv.Wide) + "件"
        Literal6.Text = StrConv(lngHAf2, VbStrConv.Wide) + "件"

        Literal16.Text = StrConv(lngHKd, VbStrConv.Wide) + "件"
        Literal17.Text = StrConv(lngHKd2, VbStrConv.Wide) + "件"

        Literal2.Text = StrConv(lngU, VbStrConv.Wide) + "件"
        Literal8.Text = StrConv(lngU2, VbStrConv.Wide) + "件"

        Literal3.Text = StrConv(lngAir, VbStrConv.Wide) + "件"
        Literal10.Text = StrConv(lngAir2, VbStrConv.Wide) + "件"

        Literal4.Text = StrConv(lngJishT, VbStrConv.Wide) + "件"

        Literal5.Text = StrConv(lngTsukI, VbStrConv.Wide) + "件"

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        '更新日時取得処理
        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_DATA_UPD ORDER BY DATA_CD "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Select Case dataread("DATA_CD")
                Case "001"
                    StrH = dataread("DATA_UPD")
                Case "002"
                    StrU = dataread("DATA_UPD")
                Case "003"
                    StrA = dataread("DATA_UPD")
                Case "004"
                    StrJ = dataread("DATA_UPD")
                Case "005"
                    StrT = dataread("DATA_UPD")
            End Select
        End While

        Literal7.Text = StrH
        Literal18.Text = StrH
        Literal9.Text = StrU
        Literal11.Text = StrA
        Literal13.Text = StrJ
        Literal15.Text = StrT

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '書類最終情報取得処理
        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "   COUNT(CASE  WHEN AA.Forwarder = '上野' THEN 1 ELSE NULL END) AS Rec01 "
        strSQL = strSQL & "  ,COUNT(CASE  WHEN AA.Forwarder = '本社' THEN 1 ELSE NULL END) AS Rec02 "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "( "
        strSQL = strSQL & "SELECT DISTINCT "
        strSQL = strSQL & "  CASE LEFT(Forwarder,2)  "
        strSQL = strSQL & "    WHEN 'AT' THEN '上野' "
        strSQL = strSQL & "    ELSE '本社' "
        strSQL = strSQL & "  END AS Forwarder "
        strSQL = strSQL & "  , CUST_CD  "
        strSQL = strSQL & "  , INVOICE_NO  "
        strSQL = strSQL & "  , MAX(VAN_DATE) AS FINAL_VAN  "
        strSQL = strSQL & "  , CUT_DATE "
        strSQL = strSQL & "  , ETD "
        strSQL = strSQL & "FROM  "
        strSQL = strSQL & "  ( SELECT Forwarder, CUST_CD, INVOICE_NO, DAY01 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY02 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY03 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY04 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY05 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY06 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY07 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY08 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY09 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY10 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING  "
        strSQL = strSQL & "    UNION ALL   "
        strSQL = strSQL & "    SELECT Forwarder, CUST_CD, INVOICE_NO, DAY11 AS VAN_DATE, CUT_DATE  , ETD FROM T_BOOKING) AS TBL   "
        strSQL = strSQL & "WHERE  "
        strSQL = strSQL & "  INVOICE_NO <> ''   "
        strSQL = strSQL & "  AND VAN_DATE <> ''  "
        strSQL = strSQL & "  AND VAN_DATE >= CONVERT(NVARCHAR,  GETDATE(), 111) "
        strSQL = strSQL & "  AND INVOICE_NO NOT LIKE '%ヒョウコウ%' "
        strSQL = strSQL & "GROUP BY  "
        strSQL = strSQL & "  Forwarder, CUST_CD , INVOICE_NO  , CUT_DATE  , ETD "
        strSQL = strSQL & "  ) AA "
        strSQL = strSQL & "WHERE  "
        strSQL = strSQL & "  AA.FINAL_VAN = CONVERT(NVARCHAR,  GETDATE(), 111) "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Dim intRec01 As Integer = 0
        Dim intRec02 As Integer = 0

        While (dataread.Read())
            intRec01 = dataread("Rec01")
            intRec02 = dataread("Rec02")
        End While

        Literal12.Text = StrConv(intRec02, VbStrConv.Wide) + "件"
        Literal20.Text = StrConv(intRec01, VbStrConv.Wide) + "件"
        Literal19.Text = StrH
        Literal22.Text = StrH

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        'バンレポの作成状況を取得
        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "   COUNT(CASE  WHEN PLACE = '0H' AND SUBSTRING(RIGHT(CUST_NM,5),1,1) = 'K' THEN 1 ELSE NULL END) AS Rec01 "
        strSQL = strSQL & "  ,COUNT(CASE  WHEN PLACE = '0H' AND SUBSTRING(RIGHT(CUST_NM,5),1,1) <> 'K' THEN 1 ELSE NULL END) AS Rec02 "
        strSQL = strSQL & "  ,COUNT(CASE  WHEN PLACE = '1U' THEN 1 ELSE NULL END) AS Rec03 "
        strSQL = strSQL & "FROM  "
        strSQL = strSQL & "  T_EXL_VAN_SCH_DETAIL "
        strSQL = strSQL & "WHERE  "
        strSQL = strSQL & "  VAN_DATE = CONVERT(NVARCHAR,  GETDATE(), 111) "
        strSQL = strSQL & "  AND UPD_FLG = '1' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        intRec01 = 0
        intRec02 = 0
        Dim intRec03 As Integer = 0

        While (dataread.Read())
            intRec01 = dataread("Rec01")
            intRec02 = dataread("Rec02")
            intRec03 = dataread("Rec03")
        End While

        Literal23.Text = StrConv(intRec01, VbStrConv.Wide) + "件"
        Literal24.Text = StrConv(intRec02, VbStrConv.Wide) + "件"
        Literal25.Text = StrConv(intRec03, VbStrConv.Wide) + "件"

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '書類の作成状況を取得
        strSQL = ""
        strSQL = strSQL & "SELECT  "
        strSQL = strSQL & "   COUNT(CASE  WHEN PLACE = '本社' THEN 1 ELSE NULL END) AS Rec01 "
        strSQL = strSQL & "  ,COUNT(CASE  WHEN PLACE = '上野' THEN 1 ELSE NULL END) AS Rec02 "
        strSQL = strSQL & "FROM  "
        strSQL = strSQL & "  T_EXL_FIN_DOC "
        strSQL = strSQL & "WHERE  "
        strSQL = strSQL & "  FIN_VAN = CONVERT(NVARCHAR,  GETDATE(), 111) "
        strSQL = strSQL & "  AND UPD_FLG = '1' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        intRec01 = 0
        intRec02 = 0

        While (dataread.Read())
            intRec01 = dataread("Rec01")
            intRec02 = dataread("Rec02")
        End While

        Literal26.Text = StrConv(intRec01, VbStrConv.Wide) + "件"
        Literal27.Text = StrConv(intRec02, VbStrConv.Wide) + "件"

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        'ＡＩＲ書類の当日件数を取得
        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "   COUNT(*) AS Rec01 "
        strSQL = strSQL & "FROM  "
        strSQL = strSQL & "  T_EXL_AIR_MANAGE "
        strSQL = strSQL & "WHERE  "
        strSQL = strSQL & "  ETD = CONVERT(NVARCHAR,  GETDATE(), 111) "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        intRec01 = 0

        While (dataread.Read())
            intRec01 = dataread("Rec01")
        End While

        Literal3.Text = StrConv(intRec01, VbStrConv.Wide) + "件"

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        'ＡＩＲ書類の作成状況を取得
        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "   COUNT(*) AS Rec01 "
        strSQL = strSQL & "FROM  "
        strSQL = strSQL & "  T_EXL_AIR_MANAGE "
        strSQL = strSQL & "WHERE  "
        strSQL = strSQL & "  ETD = CONVERT(NVARCHAR,  GETDATE(), 111) "
        strSQL = strSQL & "  AND DOC_FIN = '作成済み' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        intRec01 = 0

        While (dataread.Read())
            intRec01 = dataread("Rec01")
        End While

        Literal28.Text = StrConv(intRec01, VbStrConv.Wide) + "件"


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '通関完了件数を取得
        strSQL = Make_TsuukanSQL()

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        intRec01 = 0

        While (dataread.Read())
            intRec01 = dataread("RecCnt")
        End While

        Label11.Text = StrConv(intRec01, VbStrConv.Wide) + "件"

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GET_TOPICS()
        'トピックスを取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim intCnt As Integer = 1
        Dim Linkbtn As New LinkButton

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT AA.* "
        strSQL = strSQL & "FROM (SELECT TOP 5 *   "
        strSQL = strSQL & "FROM T_EXL_TOPICS  "
        strSQL = strSQL & "WHERE INFO_DATE > DATEADD(DAY, -30, GETDATE()) "
        strSQL = strSQL & "AND   FIN_FLG = '0' "
        strSQL = strSQL & "ORDER BY INFO_DATE DESC, INFO_TIME DESC) AA "
        strSQL = strSQL & "ORDER BY INFO_DATE , INFO_TIME "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Select Case intCnt
                Case 1
                    Me.Label1.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label6.Text = dataread("CREATE_NM")
                    Me.LinkButton1.Text = dataread("INFO_HEADER")
                    Me.LinkButton1.ID = dataread("INFO_NO")
                Case 2
                    Me.Label2.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label7.Text = dataread("CREATE_NM")
                    Me.LinkButton2.Text = dataread("INFO_HEADER")
                    Me.LinkButton2.ID = dataread("INFO_NO")
                Case 3
                    Me.Label3.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label8.Text = dataread("CREATE_NM")
                    Me.LinkButton3.Text = dataread("INFO_HEADER")
                    Me.LinkButton3.ID = dataread("INFO_NO")
                Case 4
                    Me.Label4.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label9.Text = dataread("CREATE_NM")
                    Me.LinkButton4.Text = dataread("INFO_HEADER")
                    Me.LinkButton4.ID = dataread("INFO_NO")
                Case 5
                    Me.Label5.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label10.Text = dataread("CREATE_NM")
                    Me.LinkButton5.Text = dataread("INFO_HEADER")
                    Me.LinkButton5.ID = dataread("INFO_NO")
            End Select
            intCnt += 1
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Redirect_Detail(strId As String)
        'トピックス詳細画面へ遷移
        '表示モード
        Response.Redirect("topics_detail.aspx?strId=" & strId & "&strMode=01")
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        'トピックス1番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        'トピックス2番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        'トピックス3番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Sub LinkButton4_Click(sender As Object, e As EventArgs) Handles LinkButton4.Click
        'トピックス4番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Sub LinkButton5_Click(sender As Object, e As EventArgs) Handles LinkButton5.Click
        'トピックス5番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Function Make_TsuukanSQL() As String
        '通関完了のSQL文字列組立（長いのでこちらに切り出し）

        Dim strSQL As String = ""

        Make_TsuukanSQL = ""

        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "  COUNT(*) AS RecCnt "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "    (SELECT "
        strSQL = strSQL & "      T_BOOKING.INVOICE_NO "
        strSQL = strSQL & "      , T_BOOKING.Forwarder "
        strSQL = strSQL & "      , T_BOOKING.CUT_DATE  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      T_BOOKING  "
        strSQL = strSQL & "      LEFT JOIN (  "
        strSQL = strSQL & "        SELECT DISTINCT "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.BKGNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.ID = '003' "
        strSQL = strSQL & "      ) AS T1  "
        strSQL = strSQL & "        ON T_BOOKING.BOOKING_NO = T1.BKGNO  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      T_BOOKING.Forwarder Not Like 'AT_%'  "
        strSQL = strSQL & "      AND T_BOOKING.CUT_DATE = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_CSWORKDAY  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY_NO = (  "
        strSQL = strSQL & "            SELECT "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY_NO - 1  "
        strSQL = strSQL & "            FROM "
        strSQL = strSQL & "              T_EXL_CSWORKDAY  "
        strSQL = strSQL & "            WHERE "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY = CONVERT(VARCHAR,GETDATE(),111) "
        strSQL = strSQL & "          ) "
        strSQL = strSQL & "      )  "
        strSQL = strSQL & "      AND T1.BKGNO Is Not Null  "
        strSQL = strSQL & "    UNION ALL  "
        strSQL = strSQL & "    SELECT "
        strSQL = strSQL & "      T_BOOKING.INVOICE_NO "
        strSQL = strSQL & "      , T_BOOKING.Forwarder "
        strSQL = strSQL & "      , T_BOOKING.CUT_DATE  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      T_BOOKING  "
        strSQL = strSQL & "      LEFT JOIN (  "
        strSQL = strSQL & "        SELECT DISTINCT "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.BKGNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.ID = '003' "
        strSQL = strSQL & "      ) AS T1  "
        strSQL = strSQL & "        ON T_BOOKING.BOOKING_NO = T1.BKGNO  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      T_BOOKING.Forwarder Not Like 'AT_%'  "
        strSQL = strSQL & "      AND T_BOOKING.CUT_DATE = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_CSWORKDAY  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY_NO = (  "
        strSQL = strSQL & "            SELECT "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY_NO  "
        strSQL = strSQL & "            FROM "
        strSQL = strSQL & "              T_EXL_CSWORKDAY  "
        strSQL = strSQL & "            WHERE "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY = CONVERT(VARCHAR,GETDATE(),111) "
        strSQL = strSQL & "          ) "
        strSQL = strSQL & "      )  "
        strSQL = strSQL & "      AND T1.BKGNO Is Not Null  "
        strSQL = strSQL & "    UNION ALL  "
        strSQL = strSQL & "    SELECT "
        strSQL = strSQL & "      T_BOOKING.INVOICE_NO "
        strSQL = strSQL & "      , T_BOOKING.Forwarder "
        strSQL = strSQL & "      , T_BOOKING.CUT_DATE  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      T_BOOKING  "
        strSQL = strSQL & "      LEFT JOIN (  "
        strSQL = strSQL & "        SELECT DISTINCT "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.BKGNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.ID = '003' "
        strSQL = strSQL & "      ) AS T1  "
        strSQL = strSQL & "        ON T_BOOKING.BOOKING_NO = T1.BKGNO  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      T_BOOKING.Forwarder Like 'AT_%'  "
        strSQL = strSQL & "      AND T_BOOKING.CUT_DATE = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_CSWORKDAY  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY_NO = (  "
        strSQL = strSQL & "            SELECT "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY_NO + 1  "
        strSQL = strSQL & "            FROM "
        strSQL = strSQL & "              T_EXL_CSWORKDAY  "
        strSQL = strSQL & "            WHERE "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY = CONVERT(VARCHAR,GETDATE(),111) "
        strSQL = strSQL & "          ) "
        strSQL = strSQL & "      )  "
        strSQL = strSQL & "      AND T1.BKGNO Is Not Null  "
        strSQL = strSQL & "    UNION ALL  "
        strSQL = strSQL & "    SELECT "
        strSQL = strSQL & "      T_BOOKING.INVOICE_NO "
        strSQL = strSQL & "      , T_BOOKING.Forwarder "
        strSQL = strSQL & "      , T_BOOKING.CUT_DATE  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      T_BOOKING  "
        strSQL = strSQL & "      LEFT JOIN (  "
        strSQL = strSQL & "        SELECT DISTINCT "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.BKGNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.ID = '003' "
        strSQL = strSQL & "      ) AS T1  "
        strSQL = strSQL & "        ON T_BOOKING.BOOKING_NO = T1.BKGNO  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      T_BOOKING.Forwarder Like 'AT_%'  "
        strSQL = strSQL & "      AND T_BOOKING.CUT_DATE = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_CSWORKDAY  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY_NO = (  "
        strSQL = strSQL & "            SELECT "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY_NO  "
        strSQL = strSQL & "            FROM "
        strSQL = strSQL & "              T_EXL_CSWORKDAY  "
        strSQL = strSQL & "            WHERE "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY = CONVERT(VARCHAR,GETDATE(),111) "
        strSQL = strSQL & "          ) "
        strSQL = strSQL & "      )  "
        strSQL = strSQL & "      AND T1.BKGNO Is Not Null  "
        strSQL = strSQL & "    UNION ALL  "
        strSQL = strSQL & "    SELECT "
        strSQL = strSQL & "      T_BOOKING.INVOICE_NO "
        strSQL = strSQL & "      , T_BOOKING.Forwarder "
        strSQL = strSQL & "      , T_BOOKING.CUT_DATE  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      T_BOOKING  "
        strSQL = strSQL & "      LEFT JOIN (  "
        strSQL = strSQL & "        SELECT DISTINCT "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.BKGNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_WORKSTATUS00.ID = '003' "
        strSQL = strSQL & "      ) AS T1  "
        strSQL = strSQL & "        ON T_BOOKING.BOOKING_NO = T1.BKGNO  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      T_BOOKING.Forwarder Like 'AT_%'  "
        strSQL = strSQL & "      AND T_BOOKING.CUT_DATE = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          T_EXL_CSWORKDAY  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          T_EXL_CSWORKDAY.WORKDAY_NO = (  "
        strSQL = strSQL & "            SELECT "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY_NO - 1  "
        strSQL = strSQL & "            FROM "
        strSQL = strSQL & "              T_EXL_CSWORKDAY  "
        strSQL = strSQL & "            WHERE "
        strSQL = strSQL & "              T_EXL_CSWORKDAY.WORKDAY = CONVERT(VARCHAR,GETDATE(),111) "
        strSQL = strSQL & "          ) "
        strSQL = strSQL & "      )  "
        strSQL = strSQL & "      AND T1.BKGNO Is Not Null) AA "

        Make_TsuukanSQL = strSQL
    End Function
End Class
