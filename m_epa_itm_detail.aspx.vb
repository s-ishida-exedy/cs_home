Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strMode As String = ""
        Dim strCode As String = ""

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
                strSQL = strSQL & "SELECT "
                strSQL = strSQL & "CASE ITM.CODE "
                strSQL = strSQL & "     WHEN '01' THEN '日メキシコ' "
                strSQL = strSQL & "     WHEN '02' THEN '日マレーシア' "
                strSQL = strSQL & "     WHEN '03' THEN '日チリ' "
                strSQL = strSQL & "     WHEN '04' THEN '日タイ' "
                strSQL = strSQL & "     WHEN '05' THEN '日インドネシア' "
                strSQL = strSQL & "     WHEN '06' THEN '日ブルネイ' "
                strSQL = strSQL & "     WHEN '07' THEN '日フィリピン' "
                strSQL = strSQL & "     WHEN '08' THEN '日スイス' "
                strSQL = strSQL & "     WHEN '09' THEN '日ベトナム' "
                strSQL = strSQL & "     WHEN '10' THEN '日インド' "
                strSQL = strSQL & "     WHEN '11' THEN '日ペルー' "
                strSQL = strSQL & "     WHEN '12' THEN '日オーストラリア' "
                strSQL = strSQL & "     WHEN '13' THEN '日モンゴル' "
                strSQL = strSQL & "     WHEN '14' THEN '日アセアン' "
                strSQL = strSQL & "     WHEN '15' THEN 'ＲＣＥＰ' "
                strSQL = strSQL & "  END CODE "
                strSQL = strSQL & " ,CST_ITM_CODE  "
                strSQL = strSQL & " ,ITM_NAME  "
                strSQL = strSQL & " ,ORI_ITM_NAME  "
                strSQL = strSQL & " ,ORI_JDG_NO  "
                strSQL = strSQL & " ,HS_CODE  "
                strSQL = strSQL & " ,REMARKS  "
                strSQL = strSQL & "FROM M_EXL_ORIGIN_ITM ITM "
                strSQL = strSQL & "WHERE UNF_CODE= " & strCode & " "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    Label1.Text = dataread("CODE")
                    TextBox1.Text = dataread("CST_ITM_CODE")
                    TextBox2.Text = dataread("ITM_NAME")
                    TextBox3.Text = dataread("ORI_ITM_NAME")
                    TextBox4.Text = dataread("ORI_JDG_NO")
                    TextBox5.Text = dataread("HS_CODE")
                    If dataread("REMARKS") = "" Then
                        DropDownList3.SelectedIndex = 0
                    Else
                        DropDownList3.SelectedIndex = 1
                    End If
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()
                cnn.Close()
                cnn.Dispose()

                '登録ボタンを非表示
                Button1.Visible = False
                DropDownList1.Visible = False
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

        '画面入力情報を変数に代入
        Dim strCstItm As String = TextBox1.Text
        Dim strItmNm As String = TextBox2.Text
        Dim strEpaNm As String = TextBox3.Text
        Dim strOriNo As String = TextBox4.Text
        Dim strHsCd As String = TextBox5.Text
        Dim strRema As String = ""
        If DropDownList3.SelectedIndex = 1 Then
            strRema = "01"
        End If

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_ORIGIN_ITM SET "
            strSQL = strSQL & "CST_ITM_CODE = '" & strCstItm & "' "
            strSQL = strSQL & ",ITM_NAME = '" & strItmNm & "' "
            strSQL = strSQL & ",ORI_ITM_NAME = '" & strEpaNm & "' "
            strSQL = strSQL & ",ORI_JDG_NO = '" & strOriNo & "' "
            strSQL = strSQL & ",HS_CODE = '" & strHsCd & "' "
            strSQL = strSQL & ",REMARKS = '" & strRema & "' "
            strSQL = strSQL & "WHERE UNF_CODE = '" & strCode & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_ORIGIN_ITM "
            strSQL = strSQL & "WHERE UNF_CODE = '" & strCode & "' "

        ElseIf strExecMode = "03" Then
            '登録
            Dim strCountry As String = DropDownList1.SelectedValue

            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_ORIGIN_ITM VALUES("
            strSQL = strSQL & " '" & strCountry & "' "
            strSQL = strSQL & ",'" & strCstItm & "' "
            strSQL = strSQL & ",'" & strItmNm & "' "
            strSQL = strSQL & ",'" & strEpaNm & "' "
            strSQL = strSQL & ",'" & strOriNo & "' "
            strSQL = strSQL & ",'" & strHsCd & "' "
            strSQL = strSQL & ",'" & strRema & "') "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim strMode As String = ""
        Dim txtcust As String = TextBox1.Text
        Dim lblcust As String = Label1.Text
        Dim strCstItm As String = TextBox1.Text
        Dim strItmNm As String = TextBox2.Text
        Dim strEpaNm As String = TextBox3.Text
        Dim strOriNo As String = TextBox4.Text
        Dim strHsCd As String = TextBox5.Text

        'パラメータ取得
        strMode = Session("strMode")

        '必須チェック
        If strCstItm = "" Then
            Label3.Text = "客先品番は必須入力です。"
            chk_Nyuryoku = False
        End If
        If strItmNm = "" Then
            Label3.Text = "品名は必須入力です。"
            chk_Nyuryoku = False
        End If
        If strEpaNm = "" Then
            Label3.Text = "EPA出力品名は必須入力です。"
            chk_Nyuryoku = False
        End If
        If strOriNo = "" Then
            Label3.Text = "判定番号は必須入力です。"
            chk_Nyuryoku = False
        End If
        If strHsCd = "" Then
            Label3.Text = "HSコードは必須入力です。"
            chk_Nyuryoku = False
        End If

        '半角英数チェック
        If HankakuEisuChk(strCstItm) = False Then
            Label3.Text = "客先品番に半角英数以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(strItmNm) = False Then
            Label3.Text = "品名に半角英数以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(strEpaNm) = False Then
            Label3.Text = "EPA出力品名に半角英数以外が使用されています。"
            chk_Nyuryoku = False
        End If

        '半角数字チェック
        If HankakuNumChk(strOriNo) = False Then
            Label3.Text = "判定番号に半角数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(strHsCd) = False Then
            Label3.Text = "HSコードに半角数字以外が使用されています。"
            chk_Nyuryoku = False
        End If

        '桁数チェック
        If Len(strOriNo) <> 10 Then
            Label3.Text = "判定番号は数字１０桁で入力してください。"
            chk_Nyuryoku = False
        End If
        If Len(strHsCd) <> 6 Then
            Label3.Text = "ＨＳコードは数字６桁で入力してください。"
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
        Response.Redirect("m_epa_itm.aspx")
    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("m_epa_itm.aspx")
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

        Dim strOri = Trim(TextBox4.Text)

        strSQL = ""
        StrSQL = StrSQL & "Select COUNT(*) As RecCnt "
        strSQL = strSQL & "FROM M_EXL_ORIGIN_ITM "
        strSQL = strSQL & "WHERE ORI_JDG_NO = '" & strOri & "' "

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
            Label3.Text = "この判定番号は既に登録されています。"
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
        Response.Redirect("m_epa_itm.aspx")

    End Sub
End Class

