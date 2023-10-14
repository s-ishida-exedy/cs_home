Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strSEQ As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strMode As String = ""
        Dim intCnt As Integer = 0

        Label3.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            If Request.QueryString("seq") IsNot Nothing Then
                'GridViewのタイトルクリックによる遷移
                strSEQ = Request.QueryString("seq").ToString
                strMode = "99"
            Else
                '情報更新ボタン押下による遷移
                strSEQ = Session("strSEQ")
                strMode = Session("strMode")
            End If

            If strMode = "0" Or strMode = "99" Then
                '更新モード　DBから値取得し、セット

                '接続文字列の作成
                Dim ConnectionString As String = String.Empty
                'SQL Server認証
                ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
                'SqlConnectionクラスの新しいインスタンスを初期化
                Dim cnn = New SqlConnection(ConnectionString)
                Dim Command = cnn.CreateCommand

                'データベース接続を開く
                cnn.Open()

                strSQL = ""
                strSQL = strSQL & "SELECT *  "
                strSQL = strSQL & "FROM T_EXL_KNOWLEDGE "
                strSQL = strSQL & "WHERE SEQ_NO = '" & strSEQ & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    DropDownList1.SelectedValue = dataread("CAT_PRI")
                    DropDownList2.SelectedValue = dataread("CAT_SEC")
                    DropDownList3.SelectedValue = dataread("CAT_TER")
                    TextBox1.Text = dataread("KEYWORD")
                    TextBox2.Text = dataread("TITLE")
                    TextBox3.Text = dataread("CONTENTS")
                    intCnt = dataread("SHOW_CNT")
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()

                '登録ボタンを非表示
                Button1.Visible = False

                If strMode = "99" Then
                    'タイトルクリックで遷移した場合（閲覧モード）
                    '更新ボタンを非表示
                    Button7.Enabled = False
                    Button8.Enabled = False
                    DropDownList1.Enabled = False
                    DropDownList2.Enabled = False
                    DropDownList3.Enabled = False
                    TextBox1.Enabled = False
                    TextBox2.Enabled = False
                    TextBox3.Enabled = False

                    '閲覧回数を更新
                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_KNOWLEDGE SET "
                    strSQL = strSQL & "SHOW_CNT = " & intCnt + 1 & " "
                    strSQL = strSQL & "WHERE SEQ_NO = '" & strSEQ & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()
                End If

                cnn.Close()
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
        Dim strSEQ As String = Session("strSEQ")

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
        Dim strCode As String = DropDownList1.SelectedValue
        Dim strCode2 As String = DropDownList2.SelectedValue
        Dim strCode3 As String = DropDownList3.SelectedValue
        Dim strKey As String = TextBox1.Text
        Dim strTitle As String = TextBox2.Text
        Dim strDetail As String = TextBox3.Text

        '現在日時
        Dim dt1 As DateTime = DateTime.Now
        Dim strDate As String = dt1.ToString("yyyy/MM/dd HH:mm:ss")

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_KNOWLEDGE SET"
            strSQL = strSQL & " CAT_PRI = '" & strCode & "' "
            strSQL = strSQL & ",CAT_SEC = '" & strCode2 & "' "
            strSQL = strSQL & ",CAT_TER = '" & strCode3 & "' "
            strSQL = strSQL & ",KEYWORD = '" & strKey & "' "
            strSQL = strSQL & ",TITLE = '" & strTitle & "' "
            strSQL = strSQL & ",CONTENTS = '" & strDetail & "' "
            strSQL = strSQL & ",UPD_PERSON = '" & Session("UsrId") & "' "
            strSQL = strSQL & ",UPD_DATE = '" & strDate & "' "
            strSQL = strSQL & "WHERE SEQ_NO = '" & strSEQ & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_KNOWLEDGE "
            strSQL = strSQL & "WHERE SEQ_NO = '" & strSEQ & "' "

        ElseIf strExecMode = "03" Then
            '登録

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_KNOWLEDGE VALUES("
            strSQL = strSQL & " '" & strCode & "' "
            strSQL = strSQL & ",'" & strCode2 & "' "
            strSQL = strSQL & ",'" & strCode3 & "' "
            strSQL = strSQL & ",'" & strKey & "' "
            strSQL = strSQL & ",'" & strTitle & "' "
            strSQL = strSQL & ",'" & strDetail & "' "
            strSQL = strSQL & ",0 "
            strSQL = strSQL & ",'" & Session("UsrId") & "' "
            strSQL = strSQL & ",'" & strDate & "' "
            strSQL = strSQL & ",'' ,'') "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim strMode As String = ""
        Dim keyword As String = TextBox1.Text
        Dim title As String = TextBox2.Text
        Dim detail As String = TextBox3.Text

        '必須チェック
        If title = "" Then
            Label3.Text = "タイトルは必須入力です。"
            chk_Nyuryoku = False
        End If

        If detail = "" Then
            Label3.Text = "内容は必須入力です。"
            chk_Nyuryoku = False
        End If

        '文字数チェック
        If Len(Trim(keyword)) > 50 Then
            Label3.Text = "キーワードは50文字以内で入力してください。。"
            chk_Nyuryoku = False
        End If
        If Len(Trim(title)) > 50 Then
            Label3.Text = "タイトルは50文字以内で入力してください。。"
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

        Call Del_Session()            'セッション情報クリア

        '元の画面に戻る
        Response.Redirect("knowledge.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        Call Del_Session()            'セッション情報クリア

        '元の画面に戻る
        Response.Redirect("knowledge.aspx")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下
        Dim strSQL As String = ""

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '登録
        Call DB_access("03")        '登録モード

        Call Del_Session()            'セッション情報クリア

        '元の画面に戻る
        Response.Redirect("knowledge.aspx")

    End Sub

    Private Sub Del_Session()
        'セッション情報をクリア
        Session.Remove("strMode")
        Session.Remove("strSEQ")

    End Sub
End Class

