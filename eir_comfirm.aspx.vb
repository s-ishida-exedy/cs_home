Imports System.Data.SqlClient
Imports System.Data
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Label12.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList1.Items.Insert(0, "-select-") '先頭にタイトル行追加

            'メール確認画面から戻ってきた場合、セッション値をセットする
            If Session("strMode") = "0" Then
                TextBox9.Text = Session("strTant")
                DropDownList1.SelectedIndex = Session("strIdx")
                TextBox1.Text = Session("strCust")
                TextBox8.Text = Session("strTime")

                Label4.Text = Session("strVoy1")
                Label5.Text = Session("strVess1")
                Label6.Text = Session("strBook1")
                Label7.Text = Session("strCont1")
                TextBox2.Text = Session("strVoy2")
                TextBox3.Text = Session("strVess2")
                TextBox4.Text = Session("strBook2")
                TextBox5.Text = Session("strCont2")
                TextBox6.Text = Session("strEtc1")
                TextBox7.Text = Session("strEtc2")

            End If

        End If
        Button1.Attributes.Add("onclick", "return confirm('確認を依頼します。よろしいですか？');")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '確認ボタン押下
        'ドロップダウンで選択された客先コードからバンニングスケジュールを検索
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strCust As String = ""
        Dim strTime As String = ""
        Dim i As Integer = 0
        Dim strIvno As String = ""
        Dim strConSize As String = ""

        '初期化
        TextBox1.Text = ""
        TextBox8.Text = ""
        Label4.Text = ""
        TextBox2.Text = ""
        Label5.Text = ""
        TextBox3.Text = ""
        Label6.Text = ""
        TextBox4.Text = ""
        Label7.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""

        'ドロップダウンの内容を変数に格納
        Dim strVal() As String = DropDownList1.SelectedValue.Split(",")
        For Each c In strVal
            Select Case i
                Case 0
                    strCust = c
                    i += 1
                Case 1
                    strTime = c
            End Select
        Next

        If strCust = "-select-" And TextBox1.Text = "" Then
            Label16.Text = "客先コードが指定されていません。"
            Return
        ElseIf strCust = "-select-" And TextBox1.Text <> "" Then
            strCust = TextBox1.Text
            strTime = TextBox8.Text
        End If

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT IVNO, CON_SIZE FROM T_EXL_VAN_SCH_DETAIL "
        strSQL = strSQL & "WHERE CUST_NM like '%" & strCust & "%' "
        strSQL = strSQL & "AND   VAN_TIME = '" & strTime & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        If dataread.Read() = False Then
            Label16.Text = "対象データがありません。客先コード、時刻を確認してください。"
            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()
            Return
        Else
            strIvno = Replace(Left(dataread("IVNO"), 7), "IV-", "")
            strConSize = dataread("CON_SIZE")
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '取得したインボイスNOを元にT_BOOKINGよりデータ取得
        strSQL = ""
        strSQL = strSQL & "SELECT VOYAGE_NO, VESSEL_NAME, BOOKING_NO  "
        strSQL = strSQL & "FROM T_BOOKING "
        strSQL = strSQL & "WHERE INVOICE_NO like '%" & strIvno & "%' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            Label4.Text = dataread("VOYAGE_NO")
            Label5.Text = dataread("VESSEL_NAME")
            Label6.Text = dataread("BOOKING_NO")
        End While

        Label7.Text = strConSize

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'リセットボタン押下処理
        DropDownList1.SelectedIndex = 0
        TextBox1.Text = ""
        TextBox8.Text = ""
        Label4.Text = ""
        TextBox2.Text = ""
        Label5.Text = ""
        TextBox3.Text = ""
        Label6.Text = ""
        TextBox4.Text = ""
        Label7.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox9.Text = 0

    End Sub


    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        '必須チェック
        If TextBox9.Text = "" Then
            Label12.Text = "依頼者が入力されていません。"
            chk_Nyuryoku = False
        End If
        If TextBox2.Text = "" And TextBox3.Text = "" And TextBox4.Text = "" And TextBox5.Text = "" _
            And (TextBox6.Text = "" Or TextBox7.Text = "") Then
            Label12.Text = "差異項目が入力されていません。"
            chk_Nyuryoku = False
        End If

        '全角入力チェック
        If HankakuEisuChk(TextBox2.Text) = False And Trim(TextBox2.Text) <> "" Then
            Label12.Text = "VoyNOに全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox3.Text) = False And Trim(TextBox3.Text) <> "" Then
            Label12.Text = "船名に全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox4.Text) = False And Trim(TextBox4.Text) <> "" Then
            Label12.Text = "BOOKING NOに全角文字が使用されています。"
            chk_Nyuryoku = False
        End If

        '社員番号チェック
        If Left(TextBox9.Text, 1) <> "T" And Left(TextBox9.Text, 3) <> "EXL" Then
            Label12.Text = "社員番号の形式が間違っています。"
            chk_Nyuryoku = False
        End If

    End Function
    Private Function GET_USR_DATA(strCode As String) As String
        '依頼者の存在チェック
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim intCnt As Integer = 0
        GET_USR_DATA = "NG"

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "Select COUNT(*) AS RecCnt FROM M_EXL_USR "
        strSQL = strSQL & "WHERE uid = '" & strCode & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            intCnt = dataread("RecCnt")
        End While
        If intCnt > 0 Then
            GET_USR_DATA = "OK"
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'メール作成ボタンクリックイベント
        Dim strCust As String = ""
        Dim strTime As String = ""
        Dim i As Integer

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '依頼者社員番号の存在チェック
        If GET_USR_DATA(Trim(TextBox9.Text)) = "NG" Then
            Label12.Text = "社員番号が存在しません。"
            Return
        End If

        'ドロップダウンの内容を変数に格納
        Dim strVal() As String = DropDownList1.SelectedValue.Split(",")
        For Each c In strVal
            Select Case i
                Case 0
                    strCust = c
                    i += 1
                Case 1
                    strTime = c
            End Select
        Next

        If strCust = "-select-" And TextBox1.Text = "" Then
            Label16.Text = "客先コードが指定されていません。"
            Return
        ElseIf strCust = "-select-" And TextBox1.Text <> "" Then
            strCust = TextBox1.Text
            strTime = TextBox8.Text
        End If

        '記載内容をセッションに入れる
        Session("strMode") = "0"        '確認画面へ画面遷移

        Session("strTant") = Trim(TextBox9.Text)

        Session("strIdx") = DropDownList1.SelectedIndex
        Session("strCust") = strCust
        Session("strTime") = strTime

        Session("strVoy1") = Label4.Text
        Session("strVess1") = Label5.Text
        Session("strBook1") = Label6.Text
        Session("strCont1") = Label7.Text
        Session("strVoy2") = TextBox2.Text
        Session("strVess2") = TextBox3.Text
        Session("strBook2") = TextBox4.Text
        Session("strCont2") = TextBox5.Text
        Session("strEtc1") = TextBox6.Text
        Session("strEtc2") = TextBox7.Text

        '確認画面へ遷移
        Response.Redirect("eir_mail_comf.aspx")

    End Sub

End Class

