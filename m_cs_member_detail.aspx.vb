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

        Label3.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            strMode = Session("strMode")

            If strMode = "0" Then
                '更新モード　DBから値取得し、セット
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
                strSQL = strSQL & "SELECT  "
                strSQL = strSQL & "  CODE "
                strSQL = strSQL & "  , MEMBER_NAME "
                strSQL = strSQL & "  , NAME_AB "
                strSQL = strSQL & "  , CASE PLACE "
                strSQL = strSQL & "    WHEN 'H' THEN '本社'  "
                strSQL = strSQL & "    WHEN 'U' THEN '上野'  "
                strSQL = strSQL & "    WHEN 'HU' THEN '本社'  "
                strSQL = strSQL & "    END AS PLACE "
                strSQL = strSQL & "  , COMPANY "
                strSQL = strSQL & "  , TEAM "
                strSQL = strSQL & "  , TEL_NO "
                strSQL = strSQL & "  , FAX_NO "
                strSQL = strSQL & "  , E_MAIL  "
                strSQL = strSQL & "FROM M_EXL_CS_MEMBER "
                strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    DropDownList1.SelectedValue = dataread("PLACE")
                    Label1.Text = dataread("CODE")
                    TextBox2.Text = dataread("MEMBER_NAME")
                    TextBox3.Text = dataread("NAME_AB")
                    TextBox4.Text = dataread("COMPANY")
                    TextBox5.Text = dataread("TEAM")
                    TextBox6.Text = dataread("TEL_NO")
                    TextBox7.Text = dataread("FAX_NO")
                    TextBox8.Text = dataread("E_MAIL")
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
        Dim strVal As String = ""
        Dim strPlace As String = ""

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

        'ステータスのドロップダウン
        Select Case DropDownList1.SelectedValue
            Case "本社"
                strPlace = "H"
            Case "上野"
                strPlace = "U"
        End Select

        '画面入力情報を変数に代入
        Dim strName As String = TextBox2.Text
        Dim strNameAB As String = TextBox3.Text
        Dim strCompany As String = TextBox4.Text
        Dim strTeam As String = TextBox5.Text
        Dim strTel As String = TextBox6.Text
        Dim strFax As String = TextBox7.Text
        Dim strMail As String = TextBox8.Text

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_CS_MEMBER SET"
            strSQL = strSQL & " MEMBER_NAME = '" & strName & "' "
            strSQL = strSQL & ",NAME_AB = '" & strNameAB & "' "
            strSQL = strSQL & ",PLACE = '" & strPlace & "' "
            strSQL = strSQL & ",COMPANY = '" & strCompany & "' "
            strSQL = strSQL & ",TEAM = '" & strTeam & "' "
            strSQL = strSQL & ",TEL_NO = '" & strTel & "' "
            strSQL = strSQL & ",FAX_NO = '" & strFax & "' "
            strSQL = strSQL & ",E_MAIL = '" & strMail & "' "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_CS_MEMBER "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "03" Then
            '登録
            strCode = TextBox1.Text

            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_CS_MEMBER VALUES("
            strSQL = strSQL & " '" & strCode & "' "
            strSQL = strSQL & ",'" & strName & "' "
            strSQL = strSQL & ",'" & strNameAB & "' "
            strSQL = strSQL & ",'" & strPlace & "' "
            strSQL = strSQL & ",'" & strCompany & "' "
            strSQL = strSQL & ",'" & strTeam & "' "
            strSQL = strSQL & ",'" & strTel & "' "
            strSQL = strSQL & ",'" & strFax & "' "
            strSQL = strSQL & ",'" & strMail & "') "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim strMode As String = ""
        Dim address As String = TextBox8.Text
        Dim telno As String = TextBox6.Text
        Dim faxno As String = TextBox7.Text

        'パラメータ取得
        strMode = Session("strMode")

        '登録時のみチェック
        If strMode = "1" Then
            If TextBox1.Text = "" Then
                Label3.Text = "登録時、社員番号は必須です。"
                chk_Nyuryoku = False
            End If
            If Left(TextBox1.Text, 1) <> "T" And Left(TextBox1.Text, 3) <> "EXL" Then
                Label3.Text = "社員番号の形式が間違っています。"
                chk_Nyuryoku = False
            End If
        End If

        If telno <> "" Then
            If System.Text.RegularExpressions.Regex.IsMatch(telno, "\A0\d{1,4}-\d{1,4}-\d{4}\z") Then
            Else
                Label3.Text = "電話番号の形式が間違っています。"
                chk_Nyuryoku = False
            End If
        End If
        If faxno <> "" Then
            If System.Text.RegularExpressions.Regex.IsMatch(faxno, "\A0\d{1,4}-\d{1,4}-\d{4}\z") Then
            Else
                Label3.Text = "FAX番号の形式が間違っています。"
                chk_Nyuryoku = False
            End If
        End If
        If address <> "" Then
            If System.Text.RegularExpressions.Regex.IsMatch(
                address,
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
        Response.Redirect("m_cs_member.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("m_cs_member.aspx")
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

        Dim strCode = Session("strCode")

        strSQL = ""
        StrSQL = StrSQL & "SELECT COUNT(*) AS RecCnt "
        StrSQL = StrSQL & "FROM M_EXL_CS_MEMBER "
        StrSQL = StrSQL & "WHERE CODE = '" & strCode & "' "

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
            Label3.Text = "この社員番号は既に登録されています。"
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
        Response.Redirect("m_cs_member.aspx")

    End Sub
End Class

