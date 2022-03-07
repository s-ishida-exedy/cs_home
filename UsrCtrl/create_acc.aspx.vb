Imports System.Data.SqlClient
Imports mod_function

Partial Class create_acc
    Inherits System.Web.UI.Page
    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下
        Dim strSQL As String = ""
        Dim IntCnt As Integer = 0
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '入力値取得
        Dim strID = Trim(TextBox1.Text)
        Dim strPass1 = Trim(TextBox2.Text)
        Dim strPass2 = Trim(TextBox3.Text)
        Dim strName = Trim(TextBox4.Text)
        Dim strMail = Trim(TextBox5.Text)
        Dim strInsPass As String = ""

        '入力チェック
        If strPass1 <> strPass2 Then
            objLbl.Text = "入力されたパスワードが異なっています。"
            Return
        End If
        If Left(TextBox1.Text, 1) <> "T" And Left(TextBox1.Text, 3) <> "EXL" Then
            objLbl.Text = "社員番号の形式が間違っています。"
            Return
        End If

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        '既存データの確認
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM M_EXL_USR "
        strSQL = strSQL & "WHERE uid = '" & TextBox1.Text & "' "

        cnn.Open()

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            IntCnt = dataread("RecCnt")
        End While

        dataread.Close()
        dbcmd.Dispose()

        If IntCnt > 0 Then
            '存在すればエラー
            objLbl.Text = "入力された社員番号は既に登録されています。。"

            cnn.Close()
        Else
            'パスワード暗号化
            Dim origByte As Byte() = System.Text.Encoding.UTF8.GetBytes(strPass1)

            Dim sha256 As System.Security.Cryptography.SHA256 = New System.Security.Cryptography.SHA256CryptoServiceProvider()
            Dim hashValue As Byte() = sha256.ComputeHash(origByte)
            'byte型配列を16進数の文字列に変換
            Dim result As New System.Text.StringBuilder()
            Dim b As Byte
            For Each b In hashValue
                result.Append(b.ToString("x2"))
            Next

            strInsPass = result.ToString

            '登録実行
            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_USR "
            strSQL = strSQL & " VALUES( "
            strSQL = strSQL & "'" & strID & "' "
            strSQL = strSQL & ",'" & strInsPass & "' "
            strSQL = strSQL & ",'" & strName & "' "
            strSQL = strSQL & ",'' "
            strSQL = strSQL & ",'usr' "
            strSQL = strSQL & ",'" & strMail & "' "
            strSQL = strSQL & ") "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()

            'ログイン画面に戻る
            Response.Redirect("../login.aspx")
        End If



    End Sub

End Class
