Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Interop.Outlook
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Label12.Text = ""
        If IsPostBack Then
            ' そうでない時処理
            Dim ste As String = ""
        Else
            Dim ste2 As String = ""

            Button2.Visible = False
            TextBox6.Enabled = False
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
        If HankakuNumChk(TextBox9.Text) = False And Trim(TextBox1.Text) <> "" Then
            Label12.Text = "総重量に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(TextBox10.Text) = False And Trim(TextBox1.Text) <> "" Then
            Label12.Text = "個口に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(TextBox3.Text) = False And Trim(TextBox1.Text) <> "" Then
            Label12.Text = "ｻｲｽﾞ(縦)に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(TextBox4.Text) = False And Trim(TextBox1.Text) <> "" Then
            Label12.Text = "ｻｲｽﾞ(横)に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuNumChk(TextBox5.Text) = False And Trim(TextBox1.Text) <> "" Then
            Label12.Text = "ｻｲｽﾞ(高さ)に数字以外が使用されています。"
            chk_Nyuryoku = False
        End If

        '日付入力チェック
        If Chk_Hiduke(TextBox7.Text) = False And Trim(TextBox2.Text) <> "" Then
            Label12.Text = "集荷日の日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox8.Text) = False And Trim(TextBox2.Text) <> "" Then
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

        'ボタン表示制御
        Button1.Visible = False
        Button2.Visible = True

        '各コントロールを非活性にする
        DropDownList1.Enabled = False
        DropDownList2.Enabled = False
        TextBox1.Enabled = False
        DropDownList3.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        DropDownList4.Enabled = False
        CheckBox1.Enabled = False
        CheckBox2.Enabled = False
        CheckBox3.Enabled = False
        CheckBox4.Enabled = False
        CheckBox5.Enabled = False
        TextBox2.Enabled = False
        CheckBox6.Enabled = False
        TextBox6.Enabled = False

        Label12.Text = "下記内容を確認し、メール送信ボタンを押してください。"

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'メール送信ボタン
        Dim strName, strAdd, strDes As String

        '客先情報取得
        Call GET_CustInfo(TextBox1.Text, strName, strAdd, strDes)

        '本文作成
        Dim strbody As String = UriBodyC(strName, strAdd, strDes)

        'メール送付
        Dim app As New Microsoft.Office.Interop.Outlook.Application()
        Dim Mail As MailItem = app.CreateItem(OlItemType.olMailItem)

        '宛先
        Mail.To = ""

        If CheckBox6.Checked = True Then
            Mail.CC = TextBox6.Text
        Else
            Mail.CC = ""
        End If

        'BCCのアドレスをDBから取得
        Mail.BCC = GET_BccAddress(DropDownList2.SelectedValue.ToString)

        '件名
        Mail.Subject = " 【見積り依頼  " & TextBox1.Text & "向け】ETD:　" & TextBox7.Text

        '形式をHTML
        Mail.BodyFormat = OlBodyFormat.olFormatHTML
        Mail.HTMLBody = "<HTML><BODY>" & strbody & "</BODY></HTML>"

        'アドレス帳を用いてメールアドレスを名前解決
        Mail.Recipients.ResolveAll()

        If CheckBox5.Checked = True Then
            '自動送信がONになっている場合は、そのまま送信
            '送信実行
            Mail.Send()
        Else
            'メールを表示
            Mail.Display()
        End If

        'ボタン表示制御
        Button1.Visible = True
        Button2.Visible = False

        '各コントロールを活性にする
        DropDownList1.Enabled = True
        DropDownList2.Enabled = True
        TextBox1.Enabled = True
        DropDownList3.Enabled = True
        TextBox9.Enabled = True
        TextBox10.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox7.Enabled = True
        TextBox8.Enabled = True
        DropDownList4.Enabled = True
        CheckBox1.Enabled = True
        CheckBox2.Enabled = True
        CheckBox3.Enabled = True
        CheckBox4.Enabled = True
        CheckBox5.Enabled = True
        TextBox2.Enabled = True
        CheckBox6.Enabled = True
        TextBox6.Enabled = True

        Label12.Text = ""

    End Sub

    Public Function UriBodyC(strName As String, strAdd As String, strDes As String) As String
        'メール本文の作成
        Dim Bdy As String = ""

        Bdy = Bdy + "お世話になります。<BR>"
        Bdy = Bdy + "早速ですがAIRの見積りを依頼をさせていただきます。<BR>"
        Bdy = Bdy + "下記条件を元にお見積りをお願い申し上げます。<BR><BR>"

        '出荷場所
        If Me.DropDownList2.SelectedValue = 0 Then
            Bdy = Bdy + "集荷場所：〒572-0822　(株)エクセディ物流 1F<BR>"
            Bdy = Bdy + "住所: 大阪府寝屋川市木田元宮1 -30 - 1<BR><BR>"
        Else
            Bdy = Bdy + "集荷場所：〒518-0825　(株)エクセディ物流 上野出張所<BR>"
            Bdy = Bdy + "住所: 三重県伊賀市小田町2500番地<BR><BR>"
        End If

        'サイズ欄の文字列生成
        Dim strSize As String = ""
        strSize = TextBox3.Text & "×" & TextBox4.Text & "×" & TextBox5.Text & "cm"

        '表の作成
        Bdy = Bdy + "<table border = ""0"" style=""border-collapse: collapse;width:348pt; border: 0.5pt solid windowtext;"" width=""463"">"
        Bdy = Bdy + "<tr height=32>"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">客先コード</td>"
        Bdy = Bdy + "    <td colspan=2 width=338 style=""border: .5pt solid windowtext;text-align: center;"">"
        Bdy = Bdy + "" & TextBox1.Text & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">客先名</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;"" colspan=""2"" width=""338"">"
        Bdy = Bdy + "" & strName & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">住所</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;"" colspan=""2"" width=""338"">"
        Bdy = Bdy + "" & strAdd & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">仕向地</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"" width=""338"">"
        Bdy = Bdy + "" & strDes & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">輸送条件</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"" width=""338"">集荷～通関～航空輸送～通関～配達</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">建値</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">"
        Bdy = Bdy + "" & DropDownList3.SelectedValue & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">集荷日/時間</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"">"
        Bdy = Bdy + "" & TextBox7.Text & "</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"">"
        Bdy = Bdy + "" & DropDownList4.SelectedValue & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">到着希望日</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">"
        Bdy = Bdy + "" & TextBox8.Text & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">総重量/個数</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"">約 "
        Bdy = Bdy + "" & TextBox9.Text & " Kg</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"">"
        Bdy = Bdy + "" & TextBox10.Text & " 個口</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">サイズ</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">"
        Bdy = Bdy + "" & strSize & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">備考</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">危険品非該当</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">連絡事項</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;"" colspan=""2"">"
        Bdy = Bdy + "" & TextBox2.Text & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "</table>"
        Return Bdy
    End Function

    Private Sub CtrlCheckBox()
        'チェックボックスの制御
        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True Then
            'どれかのチェックがONの場合、添付ファイル有のチェックをON
            CheckBox4.Checked = True
            '添付ファイルありの場合は、自動送信OFF
            CheckBox5.Checked = False
            CheckBox5.Enabled = False
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            CheckBox4.Checked = False
            CheckBox5.Enabled = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Call CtrlCheckBox()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Call CtrlCheckBox()
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        Call CtrlCheckBox()
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        'CC追加チェックボックス制御
        If CheckBox6.Checked = True Then
            TextBox6.Enabled = True
        Else
            TextBox6.Enabled = False
        End If
    End Sub

    Private Function GET_BccAddress(strPlace As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_BccAddress = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT MAIL_ADD FROM M_EXL_AIR_MAIL "
        strSQL = strSQL & "WHERE PLACE = '" & strPlace & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_BccAddress += dataread("MAIL_ADD") + ";"

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function
End Class

