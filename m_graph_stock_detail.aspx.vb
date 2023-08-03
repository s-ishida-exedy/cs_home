Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strNO As String = ""
        Dim strNAME As String = ""
        Dim strNAME01 As String = ""
        Dim strNAME02 As String = ""
        Dim strNAME03 As String = ""
        Dim strSQL As String = ""

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim strCode As String = ""
        Dim strMode As String = ""

        Label3.Text = ""

        Dim wday As String = ""
        Dim wday2 As String = ""
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim Kaika00 As String = ""

        Dim ts1 As New TimeSpan(80, 0, 0, 0)
        Dim ts2 As New TimeSpan(80, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1
        Dim fflg2 As String = ""



        If IsPostBack Then
            ' そうでない時処理
        Else


        End If

        Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")


    End Sub

    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""

        Dim strNO As String = ""
        Dim strNAME As String = ""



        strNAME = TextBox1.Text

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        If strExecMode = "01" Then

        ElseIf strExecMode = "02" Then

        ElseIf strExecMode = "03" Then
            '登録
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_GRAPH_STOCK_COM VALUES("
            strSQL = strSQL & "'" & TextBox1.Text & "' "
            strSQL = strSQL & ",'" & DropDownList1.SelectedValue & "' "
            strSQL = strSQL & ",'" & TextBox3.Text & "' ) "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim address03 As String = TextBox3.Text

        If Len(address03) > 500 Then
            Label3.Text = "長すぎます。"
            chk_Nyuryoku = False
        End If

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '登録
        Call DB_access("03")        '登録モード

        '元の画面に戻る
        Response.Redirect("make_graph_stock01.aspx")

    End Sub

End Class

