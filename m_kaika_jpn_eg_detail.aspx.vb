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
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
                strSQL = strSQL & "  NAME_JPN "
                strSQL = strSQL & "  , NAME_EG "
                strSQL = strSQL & "FROM M_EXL_KAIKA_CHANGE "
                strSQL = strSQL & "WHERE NAME_JPN = '" & strCode & "' "


                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    TextBox1.Text = dataread("NAME_JPN")
                    TextBox2.Text = dataread("NAME_EG")
                End While

                TextBox1.ReadOnly = True

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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
            strSQL = strSQL & "UPDATE M_EXL_KAIKA_CHANGE SET"
            strSQL = strSQL & " NAME_EG = '" & streng & "' "
            strSQL = strSQL & "WHERE NAME_JPN = '" & Session("strCode") & "' "


        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_KAIKA_CHANGE "
            strSQL = strSQL & "WHERE NAME_JPN = '" & Session("strCode") & "' "

        ElseIf strExecMode = "03" Then
            '登録
            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_KAIKA_CHANGE VALUES("
            strSQL = strSQL & "'" & strjpn & "' "
            strSQL = strSQL & ",'" & streng & "' ) "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()


        cnn.Close()
        cnn.Dispose()

    End Sub



    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント



        '更新
        Call DB_access("01")        '更新モード


        Session.Remove("strMode")
        Session.Remove("strCode")

        '元の画面に戻る
        Response.Redirect("m_kaika_jpn_eg.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下



        '削除
        Call DB_access("02")        '削除モード


        Session.Remove("strMode")
        Session.Remove("strCode")

        '元の画面に戻る
        Response.Redirect("m_kaika_jpn_eg.aspx")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '入力チェック
        If chk_Nyuryoku() = False Then
            TextBox1.Text = ""
            Return
        End If

        '登録
        Call DB_access("03")        '登録モード


        Session.Remove("strMode")

        '元の画面に戻る
        Response.Redirect("m_kaika_jpn_eg.aspx")

    End Sub
    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim name As String = TextBox1.Text
        Dim cnt As Long


        Call name_check(cnt)
        If cnt > 0 Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('" & name & "は登録済みです。');</script>", False)
            chk_Nyuryoku = False
        End If



    End Function

    Private Sub name_check(ByRef cnt As Long)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""
        Dim strbkg As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim strname As String = TextBox1.Text


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        cnn.Open()

        strSQL = "SELECT Count(NAME_JPN) as cnt FROM [M_EXL_KAIKA_CHANGE] "
        strSQL = strSQL & "WHERE M_EXL_KAIKA_CHANGE.NAME_JPN = '" & strname & "' "

        'ＳＱＬコマンド作成
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行
        dataread = dbcmd.ExecuteReader()
        '結果を取り出す
        While (dataread.Read())
            cnt = dataread("cnt")
        End While


        cnn.Close()
        cnn.Dispose()

    End Sub
End Class

