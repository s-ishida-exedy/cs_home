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
                ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
                'SqlConnectionクラスの新しいインスタンスを初期化
                Dim cnn = New SqlConnection(ConnectionString)

                'データベース接続を開く
                cnn.Open()

                strSQL = ""
                strSQL = strSQL & "SELECT "
                strSQL = strSQL & "  CUST "
                strSQL = strSQL & " ,CUST_CD  "
                strSQL = strSQL & " ,CUST_ADD  "
                strSQL = strSQL & "FROM M_EXL_EPA_ADD "
                strSQL = strSQL & "WHERE CODE= " & strCode & " "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    Label1.Text = dataread("CUST")
                    TextBox2.Text = dataread("CUST_CD")
                    TextBox3.Text = dataread("CUST_ADD")
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        Dim strCode = Session("strCode")

        '画面入力情報を変数に代入
        Dim strCust As String = TextBox1.Text
        Dim strCustCd As String = TextBox2.Text
        Dim strCstAdd As String = TextBox3.Text

        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_EPA_ADD SET "
            strSQL = strSQL & "CUST_CD = '" & strCustCd & "' "
            strSQL = strSQL & ",CUST_ADD = '" & strCstAdd & "' "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_EPA_ADD "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        ElseIf strExecMode = "03" Then
            '登録
            strCode = TextBox1.Text

            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_EPA_ADD VALUES("
            strSQL = strSQL & " '" & strCust & "' "
            strSQL = strSQL & ",'" & strCustCd & "' "
            strSQL = strSQL & ",'" & strCstAdd & "') "

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
        Dim custcd As String = TextBox2.Text
        Dim address As String = TextBox3.Text

        'パラメータ取得
        strMode = Session("strMode")

        '登録時のみチェック
        If strMode = "1" Then
            If txtcust = "" Then
                Label3.Text = "客先略称は必須入力です。"
                chk_Nyuryoku = False
            End If
            If Len(txtcust) <> 3 Then
                Label3.Text = "客先略称の形式が間違っています（半角英文字3桁）"
                chk_Nyuryoku = False
            End If
            If txtcust <> "" And HankakuEisuChk(txtcust) = False Then
                Label3.Text = "客先略称に半角英数以外が使用されています。"
                chk_Nyuryoku = False
            End If
        End If

        If address = "" Then
            Label3.Text = "客先住所は必須入力です。"
            chk_Nyuryoku = False
        End If

        If custcd <> "" And Len(custcd) <> 4 Then
            Label3.Text = "客先コードの形式が間違っています（半角英数4桁）"
            chk_Nyuryoku = False
        End If

        If address <> "" And HankakuEisuChk(address) = False Then
            Label3.Text = "客先住所に半角英数以外が使用されています。"
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
        Response.Redirect("m_epa_cst_add.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("m_epa_cst_add.aspx")
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        Dim strCust = Trim(TextBox1.Text)
        Dim strCustCd = Trim(TextBox2.Text)

        strSQL = ""
        StrSQL = StrSQL & "SELECT COUNT(*) AS RecCnt "
        strSQL = strSQL & "FROM M_EXL_EPA_ADD "
        strSQL = strSQL & "WHERE CUST = '" & strCust & "' "
        strSQL = strSQL & "AND CUST_CD = '" & strCustCd & "' "

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
            Label3.Text = "この客先は既に登録されています。"
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
        Response.Redirect("m_epa_cst_add.aspx")

    End Sub
End Class

