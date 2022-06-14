Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call GET_PORTAL_DATA()

        Call GET_TOPICS()
    End Sub

    Private Sub GET_PORTAL_DATA()
        '概況ステータス情報を取得する
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
        strSQL = strSQL & "SELECT * FROM T_EXL_POR_STATUS "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Select Case dataread("DATA_CD")
                Case "001"
                    'Literal1.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    'Literal2.Text = "更新日時：" + dataread("DATA_UPD")
                Case "002"
                    Literal3.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    Literal4.Text = "更新日時：" + dataread("DATA_UPD")
                Case "003"
                    Literal5.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    Literal6.Text = "更新日時：" + dataread("DATA_UPD")
                Case "004"
                    Literal9.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    Literal10.Text = "更新日時：" + dataread("DATA_UPD")
                Case "005"
                    Literal7.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    Literal8.Text = "更新日時：" + dataread("DATA_UPD")
            End Select
        End While

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
        Session("strId") = strId
        Session("strMode") = "01"           '表示モード
        Response.Redirect("topics_detail.aspx")
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
