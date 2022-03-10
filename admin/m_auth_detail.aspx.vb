Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strCODE As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strMode As String = ""
        Dim strRole As String = ""

        Label3.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            strMode = Session("strMode")

            If strMode = "0" Then
                '更新モード　DBから値取得し、セット
                strCODE = Session("strCODE")

                '接続文字列の作成
                Dim ConnectionString As String = String.Empty
                'SQL Server認証
                ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
                'SqlConnectionクラスの新しいインスタンスを初期化
                Dim cnn = New SqlConnection(ConnectionString)

                'データベース接続を開く
                cnn.Open()

                strSQL = ""
                strSQL = strSQL & "SELECT url, role "
                strSQL = strSQL & "FROM M_EXL_AUTH "
                strSQL = strSQL & "WHERE CODE = '" & strCODE & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    Label1.Text = dataread("url")
                    strRole = dataread("role")

                    Dim arr1() As String = strRole.Split(",")
                    For Each c In arr1
                        Select Case c
                            Case "admin"
                                CheckBox1.Checked = True
                            Case "csusr"
                                CheckBox2.Checked = True
                            Case "usr"
                                CheckBox3.Checked = True
                        End Select
                    Next

                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()
                cnn.Close()
                cnn.Dispose()

                '登録ボタンを非表示
                Button1.Visible = False
                TextBox1.Visible = False
            Else
                '更新ボタンを非表示
                Button7.Visible = False
                Button8.Visible = False
                Label1.Visible = False
            End If
        End If

        Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
        Button7.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
        Button8.Attributes.Add("onclick", "return confirm('削除します。よろしいですか？');")
    End Sub


    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strCODE As String = ""
        Dim strUrl As String = ""
        Dim strRole As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        strCODE = Session("strCODE")

        '画面入力情報を変数に代入
        If CheckBox1.Checked = True Then
            strRole = "admin,"
        End If
        If CheckBox2.Checked = True Then
            strRole = strRole & "csusr,"
        End If
        If CheckBox3.Checked = True Then
            strRole = strRole & "usr,"
        End If

        strRole = Mid(strRole, 1, Len(strRole) - 1)

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_AUTH SET"
            strSQL = strSQL & " role = '" & strRole & "' "
            strSQL = strSQL & "WHERE CODE = '" & strCODE & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_AUTH "
            strSQL = strSQL & "WHERE CODE = '" & strCODE & "' "

        ElseIf strExecMode = "03" Then
            '登録
            strUrl = TextBox1.Text

            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_AUTH VALUES("
            strSQL = strSQL & " '" & strUrl & "' "
            strSQL = strSQL & ",'" & strRole & "') "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim strMode As String = ""
        Dim strUrl As String = TextBox1.Text

        'パラメータ取得
        strMode = Session("strMode")

        '登録時のみチェック
        If strMode = "1" Then
            If TextBox1.Text = "" Then
                Label3.Text = "登録時、URLは必須です。"
                chk_Nyuryoku = False
            End If
        End If

        'チェックボックスの確認
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            Label3.Text = "権限の選択は必須です。"
            chk_Nyuryoku = False
        End If

    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return 
        End If

        '更新
        Call DB_access("01")        '更新モード

        '元の画面に戻る
        Response.Redirect("m_auth.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("m_auth.aspx")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '既存チェック
        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        Dim strURL = Trim(TextBox1.Text)

        strSQL = ""
        StrSQL = StrSQL & "SELECT COUNT(*) AS RecCnt "
        strSQL = strSQL & "FROM M_EXL_AUTH "
        strSQL = strSQL & "WHERE url = '" & strURL & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(StrSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        Dim intCnt As Integer = 0
        While (dataread.Read())
            intCnt = dataread("RecCnt")
        End While

        If intCnt > 0 Then
            Label3.Text = "このURLは既に登録されています。"
            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()
            Return
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '登録
        Call DB_access("03")        '登録モード

        '元の画面に戻る
        Response.Redirect("m_auth.aspx")

    End Sub
End Class

