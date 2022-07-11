Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strCode As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strMode As String = ""
        Dim strCode02 As String = ""

        Label3.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            strMode = Session("strMode")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            If strMode = "0" Then
                '更新モード　DBから値取得し、セット
                strCode = Session("strCode")
                strCode02 = Session("strCode02")

                strSQL = ""
                strSQL = strSQL & "SELECT  "
                strSQL = strSQL & "  COLOR_NO "
                strSQL = strSQL & "  , COLOR_KBN "
                strSQL = strSQL & "FROM T_EXL_BOOKING_KBN "
                strSQL = strSQL & "WHERE COLOR_NO = '" & strCode & "' "
                strSQL = strSQL & "AND COLOR_KBN = '" & strCode02 & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    TextBox1.Text = dataread("COLOR_NO")
                    TextBox2.Text = dataread("COLOR_KBN")
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()
                cnn.Close()
                cnn.Dispose()

                '登録ボタンを非表示
                Button1.Visible = False
            Else


                '更新ボタンを非表示
                Button7.Visible = False
                Button8.Visible = False
            End If
        End If

        Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
        Button7.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
        Button8.Attributes.Add("onclick", "return confirm('削除します。よろしいですか？');")
    End Sub

    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        '画面入力情報を変数に代入
        Dim strjpn As String = TextBox1.Text
        Dim streng As String = TextBox2.Text


        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_BOOKING_KBN SET"
            strSQL = strSQL & " COLOR_NO = '" & streng & "' "
            strSQL = strSQL & "WHERE COLOR_NO = '" & Session("strCode") & "' "
            strSQL = strSQL & "AND COLOR_KBN = '" & Session("strCode02") & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_BOOKING_KBN "
            strSQL = strSQL & "WHERE COLOR_NO = '" & Session("strCode") & "' "
            strSQL = strSQL & "AND COLOR_KBN = '" & Session("strCode02") & "' "

        ElseIf strExecMode = "03" Then
            '登録
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_BOOKING_KBN VALUES("
            strSQL = strSQL & "'" & strjpn & "' "
            strSQL = strSQL & ",'" & streng & "' ) "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub



    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント



        '更新
        Call DB_access("01")        '更新モード


        Session.Remove("strMode")
        Session.Remove("strCode")

        '元の画面に戻る
        Response.Redirect("m_booking_colorkbn.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード


        Session.Remove("strMode")
        Session.Remove("strCode")

        '元の画面に戻る
        Response.Redirect("m_booking_colorkbn.aspx")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '入力チェック

        '登録
        Call DB_access("03")        '登録モード


        Session.Remove("strMode")

        '元の画面に戻る
        Response.Redirect("m_booking_colorkbn.aspx")

    End Sub
End Class

