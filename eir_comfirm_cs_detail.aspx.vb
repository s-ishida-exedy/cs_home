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
            '更新モード　DBから値取得し、セット
            strCode = Session("strCode")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "SELECT * "
            strSQL = strSQL & "FROM T_EXL_EIR_COMF "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())
                Label1.Text = dataread("MAIL_TITLE") & " " & dataread("IVNO")
                Label4.Text = dataread("VOYNO01")
                Label5.Text = dataread("VESSEL01")
                Label6.Text = dataread("BOOKING01")
                Label7.Text = dataread("CONTAINER01")
                TextBox2.Text = dataread("VOYNO02")
                TextBox3.Text = dataread("VESSEL02")
                TextBox4.Text = dataread("BOOKING02")
                TextBox5.Text = dataread("CONTAINER02")
                TextBox6.Text = dataread("ETC01")
                TextBox7.Text = dataread("ETC02")
            End While

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

        Dim strCode = Session("strCode")

        '画面入力情報を変数に代入
        Dim strVoy As String = TextBox2.Text
        Dim strVESS As String = TextBox3.Text
        Dim strBook As String = TextBox4.Text
        Dim strCont As String = TextBox5.Text
        Dim strEtc01 As String = TextBox6.Text
        Dim strEtc02 As String = TextBox7.Text
        Dim strNow As String = DateTime.Now

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_EIR_COMF SET"
            strSQL = strSQL & " VOYNO02 = '" & strVoy & "' "
            strSQL = strSQL & ",VESSEL02 = '" & strVESS & "' "
            strSQL = strSQL & ",BOOKING02 = '" & strBook & "' "
            strSQL = strSQL & ",CONTAINER02 = '" & strCont & "' "
            strSQL = strSQL & ",ETC01 = '" & strEtc01 & "' "
            strSQL = strSQL & ",ETC02 = '" & strEtc02 & "' "
            strSQL = strSQL & ",STATUS = '" & 1 & "' "
            strSQL = strSQL & ",UPDPERSON = '" & Session("UsrId") & "' "
            strSQL = strSQL & ",UPDTIME = '" & strNow & "' "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_EIR_COMF "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "
        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        '全角入力チェック
        If HankakuEisuChk(TextBox2.Text) = False And Trim(TextBox2.Text) <> "" Then
            Label13.Text = "VoyNOに全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox3.Text) = False And Trim(TextBox3.Text) <> "" Then
            Label13.Text = "船名に全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox4.Text) = False And Trim(TextBox4.Text) <> "" Then
            Label13.Text = "BOOKING NOに全角文字が使用されています。"
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
        Response.Redirect("eir_comfirm_cs.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("eir_comfirm_cs.aspx")
    End Sub
End Class

