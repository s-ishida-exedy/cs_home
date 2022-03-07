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
        Dim strVal As String = ""

        Label3.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            strCode = Session("strCode")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "SELECT uid, unam, depart, role, e_mail "
            strSQL = strSQL & "FROM M_EXL_USR "
            strSQL = strSQL & "WHERE uid = '" & strCode & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())
                Label1.Text = dataread("uid")
                TextBox1.Text = dataread("unam")
                TextBox2.Text = dataread("depart")
                TextBox3.Text = dataread("e_mail")
                strVal = dataread("role")
            End While

            Dim strRole() As String = strVal.Split(",")

            For Each c In strRole
                If c = "admin" Then
                    RadioButton1.Checked = True
                ElseIf c = "csusr" Then
                    RadioButton2.Checked = True
                ElseIf c = "usr" Then
                    RadioButton3.Checked = True
                End If
            Next

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()
        End If

        Button7.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
        Button8.Attributes.Add("onclick", "return confirm('削除します。よろしいですか？');")
    End Sub


    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
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

        Dim strCode = Session("strCode")

        '画面入力情報を変数に代入
        Dim strName As String = TextBox1.Text
        Dim strDept As String = TextBox2.Text
        Dim strMail As String = TextBox3.Text
        If RadioButton1.Checked = True Then
            strRole = strRole & "admin,"
        End If
        If RadioButton2.Checked = True Then
            strRole = strRole & "csusr,"
        End If
        If RadioButton3.Checked = True Then
            strRole = strRole & "usr,"
        End If
        strRole = Mid(strRole, 1, Len(strRole) - 1)     '最後尾のカンマ削除

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_USR SET"
            strSQL = strSQL & " unam = '" & strName & "' "
            strSQL = strSQL & ",depart = '" & strDept & "' "
            strSQL = strSQL & ",role = '" & strRole & "' "
            strSQL = strSQL & ",e_mail = '" & strMail & "' "
            strSQL = strSQL & "WHERE uid = '" & strCode & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_USR "
            strSQL = strSQL & "WHERE uid = '" & strCode & "' "
        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim strName As String = TextBox1.Text
        Dim strAddress As String = TextBox3.Text

        If strName = "" Then
            Label3.Text = "社員名は必須入力です。"
            chk_Nyuryoku = False
        End If
        If strAddress = "" Then
            Label3.Text = "メールアドレスは必須入力です。"
            chk_Nyuryoku = False
        End If

        If strAddress <> "" Then
            If System.Text.RegularExpressions.Regex.IsMatch(
                strAddress,
                "\A[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\z",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
            Else
                Label3.Text = "メールアドレスの形式が間違っています。"
                chk_Nyuryoku = False
            End If
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
        Response.Redirect("m_user.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("m_user.aspx")
    End Sub
End Class

