Imports System.Data.SqlClient
Imports System.Data
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Label12.Text = ""
        If IsPostBack Then
            ' そうでない時処理
        Else
            'メール確認画面から戻ってきた場合、セッション値をセットする
            If Session("strMode") = "0" Then
                DropDownList1.SelectedValue = Session("strIrai")
                DropDownList2.SelectedValue = Session("strPlac")
                TextBox1.Text = Session("strCust")
                DropDownList3.SelectedValue = Session("strImco")
                TextBox9.Text = Session("strWeit")
                TextBox10.Text = Session("strKogu")
                TextBox3.Text = Session("strTate")
                TextBox4.Text = Session("strYoko")
                TextBox5.Text = Session("strTaka")
                TextBox7.Text = Session("strPick")
                TextBox8.Text = Session("strArrD")
                DropDownList4.SelectedValue = Session("strPicT")
                If Session("strCWei") = "0" Then
                    CheckBox1.Checked = False
                Else
                    CheckBox1.Checked = True
                End If
                If Session("strCSiz") = "0" Then
                    CheckBox2.Checked = False
                Else
                    CheckBox2.Checked = True
                End If
                If Session("strCDat") = "0" Then
                    CheckBox3.Checked = False
                Else
                    CheckBox3.Checked = True
                End If
                If Session("strCC") = "" Then
                    CheckBox6.Checked = False
                Else
                    CheckBox6.Checked = True
                    TextBox6.Text = Session("strCC")
                End If
                TextBox2.Text = Session("strRenr")
            Else
                TextBox6.Enabled = False
            End If

        End If
        Button1.Attributes.Add("onclick", "return confirm('見積り依頼します。よろしいですか？');")
    End Sub

    Private Sub GET_CustInfo(strCust As String, ByRef strName As String, ByRef strAdd As String, ByRef strDes As String)
        '客先情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT CUST_NM, CUST_ADDRESS, DESTINATION "
        strSQL = strSQL & "FROM T_EXL_CSMANUAL WHERE NEW_CODE = '" & strCust & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strName = dataread("CUST_NM")
            strAdd = dataread("CUST_ADDRESS")
            strDes = dataread("DESTINATION")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        '全角入力チェック
        If HankakuEisuChk(TextBox1.Text) = False And Trim(TextBox1.Text) <> "" Then
            Label12.Text = "客先コードに全角文字が使用されています。"
            chk_Nyuryoku = False
        End If

        '桁数チェック
        If Len(TextBox1.Text) <> 4 And Trim(TextBox1.Text) <> "" Then
            Label12.Text = "客先コードが４桁ではありません。"
            chk_Nyuryoku = False
        End If

        '半角数字チェック
        If HankakuNumChk(TextBox9.Text) = False And Trim(TextBox9.Text) <> "" Then
            Label12.Text = "総重量に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(TextBox10.Text) = False And Trim(TextBox10.Text) <> "" Then
            Label12.Text = "個口に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(TextBox3.Text) = False And Trim(TextBox3.Text) <> "" Then
            Label12.Text = "ｻｲｽﾞ(縦)に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(TextBox4.Text) = False And Trim(TextBox4.Text) <> "" Then
            Label12.Text = "ｻｲｽﾞ(横)に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(TextBox5.Text) = False And Trim(TextBox5.Text) <> "" Then
            Label12.Text = "ｻｲｽﾞ(高さ)に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If

        '日付入力チェック
        If Chk_Hiduke(TextBox7.Text) = False And Trim(TextBox7.Text) <> "" Then
            Label12.Text = "集荷日の日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox8.Text) = False And Trim(TextBox8.Text) <> "" Then
            Label12.Text = "到着希望日の日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
        If Trim(TextBox7.Text) = "" Then
            Label12.Text = "集荷日が空白です。"
            chk_Nyuryoku = False
        End If
        If Trim(TextBox8.Text) = "" Then
            Label12.Text = "到着希望日が空白です。"
            chk_Nyuryoku = False
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'メール作成ボタンクリックイベント
        Dim strName, strAdd, strDes As String

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '客先情報取得
        Call GET_CustInfo(Trim(TextBox1.Text), strName, strAdd, strDes)

        If strName = "" Then
            Label12.Text = "客先コードが空白または間違っています。"
            Return
        End If

        '記載内容をセッションに入れる
        Session("strMode") = "0"        '確認画面へ画面遷移

        Session("strName") = strName
        Session("strAdd") = strAdd
        Session("strDes") = strDes

        Session("strIrai") = DropDownList1.SelectedValue
        Session("strPlac") = DropDownList2.SelectedValue
        Session("strCust") = TextBox1.Text
        Session("strImco") = DropDownList3.SelectedValue
        Session("strWeit") = TextBox9.Text
        Session("strKogu") = TextBox10.Text
        Session("strTate") = TextBox3.Text
        Session("strYoko") = TextBox4.Text
        Session("strTaka") = TextBox5.Text
        Session("strPick") = TextBox7.Text
        Session("strArrD") = TextBox8.Text
        Session("strPicT") = DropDownList4.SelectedValue
        If CheckBox1.Checked = False Then
            Session("strCWei") = "0"
        Else
            Session("strCWei") = "1"
        End If
        If CheckBox2.Checked = False Then
            Session("strCSiz") = "0"
        Else
            Session("strCSiz") = "1"
        End If
        If CheckBox3.Checked = False Then
            Session("strCDat") = "0"
        Else
            Session("strCDat") = "1"
        End If
        If CheckBox6.Checked = False Then
            Session("strCC") = ""
        Else
            Session("strCC") = TextBox6.Text
        End If
        Session("strRenr") = TextBox2.Text

        '添付ファイルをサーバーにアップロード
        Dim posted As HttpPostedFile = Request.Files("userfile")

        If Not posted.FileName = "" Then
            posted.SaveAs("C:\exp\cs_home\upload\" & System.IO.Path.GetFileName(posted.FileName))
            Session("strFile") = System.IO.Path.GetFileName(posted.FileName)
        End If

        '確認画面へ遷移
        Response.Redirect("air_est_comfirm.aspx")

    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        'CCアドレス追加チェック時の制御

        If CheckBox6.Checked = True Then
            TextBox6.Enabled = True
        Else
            TextBox6.Enabled = False
        End If

    End Sub

End Class

