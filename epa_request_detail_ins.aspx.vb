Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strEtd As String = ""
        Dim strIvno As String = ""
        Dim strCust As String = ""
        Dim strValue As String = ""
        Dim strStatus As String = ""
        Dim strVal As String = ""

        'HTML要素のIDを取得
        '「textbox2用」
        Dim id As String = TextBox2.ClientID
        Dim opt As String = "height=350,width=300,dependent=yes,location=no,menubar=no," +
                            "scrolbars=no,toolbar=no,status=no"

        'ポップアップカレンダーを開くスクリプトを作成
        Dim script As String = "var url = 'Calendar.aspx?id={0}&date=' + getElementById('{0}').value;" +
                               "window.open(url,'_blank', '{1}'); return false;"
        script = String.Format(script, id, opt)

        ' onclick 属性にスクリプトを設定する
        Button1.Attributes.Add("onclick", script)

        '「textbox3用」
        Dim id2 As String = TextBox3.ClientID
        Dim opt2 As String = "height=350,width=300,dependent=yes,location=no,menubar=no," +
                            "scrolbars=no,toolbar=no,status=no"

        'ポップアップカレンダーを開くスクリプトを作成
        Dim script2 As String = "var url = 'Calendar.aspx?id={0}&date=' + getElementById('{0}').value;" +
                               "window.open(url,'_blank', '{1}'); return false;"
        script = String.Format(script2, id2, opt2)

        ' onclick 属性にスクリプトを設定する
        Button2.Attributes.Add("onclick", script)

        '「TextBox7用」
        Dim id3 As String = TextBox7.ClientID
        Dim opt3 As String = "height=350,width=300,dependent=yes,location=no,menubar=no," +
                            "scrolbars=no,toolbar=no,status=no"

        'ポップアップカレンダーを開くスクリプトを作成
        Dim script3 As String = "var url = 'Calendar.aspx?id={0}&date=' + getElementById('{0}').value;" +
                               "window.open(url,'_blank', '{1}'); return false;"
        script = String.Format(script3, id3, opt3)

        ' onclick 属性にスクリプトを設定する
        Button3.Attributes.Add("onclick", script)

        '「TextBox8用」
        Dim id4 As String = TextBox8.ClientID
        Dim opt4 As String = "height=350,width=300,dependent=yes,location=no,menubar=no," +
                            "scrolbars=no,toolbar=no,status=no"

        'ポップアップカレンダーを開くスクリプトを作成
        Dim script4 As String = "var url = 'Calendar.aspx?id={0}&date=' + getElementById('{0}').value;" +
                               "window.open(url,'_blank', '{1}'); return false;"
        script = String.Format(script4, id4, opt4)

        ' onclick 属性にスクリプトを設定する
        Button4.Attributes.Add("onclick", script)

        '「TextBox9用」
        Dim id5 As String = TextBox9.ClientID
        Dim opt5 As String = "height=350,width=300,dependent=yes,location=no,menubar=no," +
                            "scrolbars=no,toolbar=no,status=no"

        'ポップアップカレンダーを開くスクリプトを作成
        Dim script5 As String = "var url = 'Calendar.aspx?id={0}&date=' + getElementById('{0}').value;" +
                               "window.open(url,'_blank', '{1}'); return false;"
        script = String.Format(script5, id5, opt5)

        ' onclick 属性にスクリプトを設定する
        Button5.Attributes.Add("onclick", script)

        '「TextBox10用」
        Dim id6 As String = TextBox10.ClientID
        Dim opt6 As String = "height=350,width=300,dependent=yes,location=no,menubar=no," +
                            "scrolbars=no,toolbar=no,status=no"

        'ポップアップカレンダーを開くスクリプトを作成
        Dim script6 As String = "var url = 'Calendar.aspx?id={0}&date=' + getElementById('{0}').value;" +
                               "window.open(url,'_blank', '{1}'); return false;"
        script = String.Format(script6, id6, opt6)

        ' onclick 属性にスクリプトを設定する
        Button6.Attributes.Add("onclick", script)

        '「TextBox12用」
        Dim id7 As String = TextBox12.ClientID
        Dim opt7 As String = "height=350,width=300,dependent=yes,location=no,menubar=no," +
                            "scrolbars=no,toolbar=no,status=no"

        'ポップアップカレンダーを開くスクリプトを作成
        Dim script7 As String = "var url = 'Calendar.aspx?id={0}&date=' + getElementById('{0}').value;" +
                               "window.open(url,'_blank', '{1}'); return false;"
        script = String.Format(script7, id7, opt7)

        ' onclick 属性にスクリプトを設定する
        Button8.Attributes.Add("onclick", script)

        Button7.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
    End Sub


    Private Sub DB_access(strMode As String)
        '画面入力情報をテーブルへ登録
        Dim strSQL As String
        Dim dbcmd As SqlCommand
        Dim strVal As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        'ステータスのドロップダウン
        Dim strDropDownList As String = "01"

        '画面入力情報を変数に代入
        Dim strSenmei As String = TextBox1.Text
        Dim strEta As String = TextBox2.Text
        Dim strCut As String = TextBox3.Text
        Dim strVoy As String = TextBox4.Text
        Dim strUketsuke As String = TextBox5.Text
        Dim strIvno As String = TextBox6.Text
        Dim strShinsei As String = TextBox7.Text
        Dim strSoufu As String = TextBox8.Text
        Dim strJuryo As String = TextBox9.Text
        Dim strSend As String = TextBox10.Text
        Dim strTrkno As String = TextBox11.Text

        Dim strEtd As String = TextBox12.Text
        Dim strIv As String = TextBox13.Text
        Dim strCust As String = TextBox14.Text
        Dim strCustCd As String = TextBox15.Text

        'データ更新
        strSQL = ""
        strSQL = strSQL & "INSERT INTO T_EXL_EPA_KANRI VALUES("
        strSQL = strSQL & " '" & strDropDownList & "' "
        strSQL = strSQL & ",'" & strEtd & "' "
        strSQL = strSQL & ",'" & strIv & "' "
        strSQL = strSQL & ",'" & strCust & "' "
        strSQL = strSQL & ",'" & strCustCd & "' "
        strSQL = strSQL & ",'' "
        strSQL = strSQL & ",'" & strSenmei & "' "
        strSQL = strSQL & ",'' "
        strSQL = strSQL & ",'" & strEta & "' "
        strSQL = strSQL & ",'" & strCut & "' "
        strSQL = strSQL & ",'" & strVoy & "' "
        strSQL = strSQL & ",'" & strUketsuke & "' "
        strSQL = strSQL & ",'" & strIvno & "' "
        strSQL = strSQL & ",'" & strShinsei & "' "
        strSQL = strSQL & ",'" & strSoufu & "' "
        strSQL = strSQL & ",'" & strJuryo & "' "
        strSQL = strSQL & ",'" & strSend & "' "
        strSQL = strSQL & ",'" & strTrkno & "') "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        '全角入力チェック
        If HankakuEisuChk(TextBox13.Text) = False And Trim(TextBox1.Text) <> "" Then
            MsgBox("ＩＶに全角文字が使用されています。")
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox14.Text) = False And Trim(TextBox1.Text) <> "" Then
            MsgBox("客先に全角文字が使用されています。")
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox15.Text) = False And Trim(TextBox1.Text) <> "" Then
            MsgBox("客先ｺｰﾄﾞに全角文字が使用されています。")
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox1.Text) = False And Trim(TextBox1.Text) <> "" Then
            MsgBox("船名に全角文字が使用されています。")
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox4.Text) = False And Trim(TextBox4.Text) <> "" Then
            MsgBox("VoyNOに全角文字が使用されています。")
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox5.Text) = False And Trim(TextBox5.Text) <> "" Then
            MsgBox("受付番号に全角文字が使用されています。")
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox6.Text) = False And Trim(TextBox6.Text) <> "" Then
            MsgBox("IVNO(Full)に全角文字が使用されています。")
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox11.Text) = False And Trim(TextBox11.Text) <> "" Then
            MsgBox("TRK#に全角文字が使用されています。")
            chk_Nyuryoku = False
        End If

        '日付入力チェック
        If Chk_Hiduke(TextBox12.Text) = False And Trim(TextBox2.Text) <> "" Then
            MsgBox("ETDの日付形式が間違っています。")
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox2.Text) = False And Trim(TextBox2.Text) <> "" Then
            MsgBox("ETAの日付形式が間違っています。")
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox3.Text) = False And Trim(TextBox3.Text) <> "" Then
            MsgBox("カット日の日付形式が間違っています。")
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox7.Text) = False And Trim(TextBox7.Text) <> "" Then
            MsgBox("申請日の日付形式が間違っています。")
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox8.Text) = False And Trim(TextBox8.Text) <> "" Then
            MsgBox("送付依頼日の日付形式が間違っています。")
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox9.Text) = False And Trim(TextBox9.Text) <> "" Then
            MsgBox("受領日の日付形式が間違っています。")
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox10.Text) = False And Trim(TextBox10.Text) <> "" Then
            MsgBox("EPA送付日の日付形式が間違っています。")
            chk_Nyuryoku = False
        End If
    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '登録ボタンクリックイベント

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '更新
        Call DB_access("01")        '更新モード

        '元の画面に戻る
        Response.Redirect("epa_request.aspx")
    End Sub
End Class

