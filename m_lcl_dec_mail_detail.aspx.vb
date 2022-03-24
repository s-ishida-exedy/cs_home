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
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
                strSQL = strSQL & "  , Case KBN "
                strSQL = strSQL & "   WHEN '0' THEN '販促品' "
                strSQL = strSQL & "   WHEN '1' THEN 'LCL展開' "
                strSQL = strSQL & "   WHEN '2' THEN '郵船委託' "
                strSQL = strSQL & "   WHEN '3' THEN '近鉄委託' "
                strSQL = strSQL & "   WHEN '4' THEN '日ト委託' "
                strSQL = strSQL & "   WHEN '5' THEN '日通委託' "
                strSQL = strSQL & "   WHEN '6' THEN 'LCL準備_C258' "
                strSQL = strSQL & "   WHEN '7' THEN 'LCL準備_C6G0' "
                strSQL = strSQL & "   End As KBN "
                strSQL = strSQL & "  , TO_CC "
                strSQL = strSQL & "  , REF "
                strSQL = strSQL & "FROM M_EXL_LCL_DEC_MAIL "
                strSQL = strSQL & "WHERE CODE = '" & strCode & "' "



                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    Label1.Text = dataread("CODE")
                    TextBox2.Text = dataread("MAIL_ADD")
                    DropDownList1.SelectedValue = dataread("KBN")
                    DropDownList2.SelectedValue = dataread("TO_CC")
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
                strSQL = strSQL & "  IDENT_CURRENT('M_EXL_LCL_DEC_MAIL') AS CODE "
                strSQL = strSQL & "FROM M_EXL_LCL_DEC_MAIL "

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
        Dim strkbn As String = ""
        Dim strref As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        Dim strCode = Label1.Text

        'ステータスのドロップダウン
        Select Case DropDownList1.SelectedValue
            Case "販促品"
                strkbn = "0"
                strref = "販促品確認"
            Case "LCL展開"
                strkbn = "1"
                strref = "LCL貨物引取・搬入連絡"
            Case "郵船委託"
                strkbn = "2"
                strref = "郵船委託"
            Case "近鉄委託"
                strkbn = "3"
                strref = "近鉄委託"
            Case "日ト委託"
                strkbn = "4"
                strref = "日ト委託"
            Case "日通委託"
                strkbn = "5"
                strref = "日通委託"
            Case "LCL準備_C258"
                strkbn = "6"
                strref = "LCL準備_C258"
            Case "LCL準備_C6G0"
                strkbn = "7"
                strref = "LCL準備_C6G0"
        End Select

        '画面入力情報を変数に代入
        Dim strMail As String = TextBox2.Text
        Dim strCc As String = DropDownList2.Text

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_LCL_DEC_MAIL SET"
            strSQL = strSQL & " MAIL_ADD = '" & strMail & "' "
            strSQL = strSQL & ",KBN = '" & strkbn & "' "
            strSQL = strSQL & ",TO_CC = '" & strCc & "' "
            strSQL = strSQL & ",REF = '" & strref & "' "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_LCL_DEC_MAIL "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "03" Then
            '登録
            strCode = Label1.Text

            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_LCL_DEC_MAIL VALUES("
            strSQL = strSQL & "'" & strMail & "' "
            strSQL = strSQL & ",'" & strkbn & "' "
            strSQL = strSQL & ",'" & strCc & "' "
            strSQL = strSQL & ",'" & strref & "' ) "

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
        If DropDownList2.Text = "" Then
            Label3.Text = "宛先：1 CC：0を入力してください。"
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
        Response.Redirect("m_lcl_dec_mail.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("m_lcl_dec_mail.aspx")
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
        Response.Redirect("m_lcl_dec_mail.aspx")

    End Sub
End Class

