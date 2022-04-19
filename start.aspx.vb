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
        Dim strTomm As String = GET_SHITEI_EIGYOBI(Now.ToString("yyyy/MM/dd"), 2)

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
                    If strTody = dataread("VAN_DATE") And Left(Right(dataread("CUST_NM"), 5), 1) = "K" Then
                        '当日　本社　AF
                        lngHAf += 1
                    ElseIf strTody = dataread("VAN_DATE") And Left(Right(dataread("CUST_NM"), 5), 1) <> "K" Then
                        '当日　本社　KD
                        lngHKd += 1
                    ElseIf strTomm = dataread("VAN_DATE") And Left(Right(dataread("CUST_NM"), 5), 1) = "K" Then
                        '翌日　本社　AF
                        lngHAf2 += 1
                    ElseIf strTomm = dataread("VAN_DATE") And Left(Right(dataread("CUST_NM"), 5), 1) <> "K" Then
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
End Class
