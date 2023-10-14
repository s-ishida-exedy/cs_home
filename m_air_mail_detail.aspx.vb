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

                strSQL = ""
                strSQL = strSQL & "SELECT  "
                strSQL = strSQL & "  CODE "
                strSQL = strSQL & "  , MAIL_ADD "
                strSQL = strSQL & "  , CASE PLACE "
                strSQL = strSQL & "    WHEN '0' THEN '本社'  "
                strSQL = strSQL & "    WHEN '1' THEN '上野'  "
                strSQL = strSQL & "    END AS PLACE "
                strSQL = strSQL & "  , GYOSHA "
                strSQL = strSQL & "FROM M_EXL_AIR_MAIL "
                strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    DropDownList1.SelectedValue = dataread("PLACE")
                    Label1.Text = dataread("CODE")
                    TextBox2.Text = dataread("MAIL_ADD")
                    TextBox3.Text = dataread("GYOSHA")
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()
                cnn.Close()
                cnn.Dispose()

                '登録ボタンを非表示
                Button1.Visible = False
            Else
                'CODEの現在値を取得する
                strSQL = ""
                strSQL = strSQL & "SELECT DISTINCT "
                strSQL = strSQL & "  IDENT_CURRENT('M_EXL_AIR_MAIL') AS CODE "
                strSQL = strSQL & "FROM M_EXL_AIR_MAIL "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    '現在値＋１をセットする。
                    Label1.Text = dataread("CODE") + 1
                End While

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
        Dim strPlace As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        Dim strCode = Label1.Text

        'ステータスのドロップダウン
        Select Case DropDownList1.SelectedValue
            Case "本社"
                strPlace = "0"
            Case "上野"
                strPlace = "1"
        End Select

        '画面入力情報を変数に代入
        Dim strMail As String = TextBox2.Text
        Dim strGyosha As String = TextBox3.Text

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_AIR_MAIL SET"
            strSQL = strSQL & " MAIL_ADD = '" & strMail & "' "
            strSQL = strSQL & ",GYOSHA = '" & strGyosha & "' "
            strSQL = strSQL & ",PLACE = '" & strPlace & "' "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_AIR_MAIL "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "03" Then
            '登録
            strCode = Label1.Text

            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_AIR_MAIL VALUES("
            strSQL = strSQL & "'" & strMail & "' "
            strSQL = strSQL & ",'" & strPlace & "' "
            strSQL = strSQL & ",'" & strGyosha & "' ) "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim address As String = TextBox2.Text

        If address <> "" Then
            If System.Text.RegularExpressions.Regex.IsMatch(
                address,
                "\A[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\z",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
            Else
                Label3.Text = "メールアドレスの形式が間違っています。"
                chk_Nyuryoku = False
            End If
        Else
            Label3.Text = "メールアドレスは必須入力です。。"
            chk_Nyuryoku = False
        End If
        If TextBox3.Text = "" Then
            Label3.Text = "海貨業者は必須入力です。。"
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
        Response.Redirect("m_air_mail.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("m_air_mail.aspx")
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

        '登録
        Call DB_access("03")        '登録モード

        '元の画面に戻る
        Response.Redirect("m_air_mail.aspx")

    End Sub
End Class

