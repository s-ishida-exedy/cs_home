Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strValue As String = ""
        Dim strVal As String = ""

        Label1.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            Dim strCode As String = Session("strCode")
            Dim strMode As String = Session("strMode")

            If strMode = "01" Then

                '接続文字列の作成
                Dim ConnectionString As String = String.Empty
                'SQL Server認証
                ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
                'SqlConnectionクラスの新しいインスタンスを初期化
                Dim cnn = New SqlConnection(ConnectionString)

                'データベース接続を開く
                cnn.Open()

                strSQL = ""
                strSQL = strSQL & "SELECT * FROM T_EXL_AIR_MANAGE "
                strSQL = strSQL & "WHERE AIR_CODE = '" & strCode & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    DropDownList1.SelectedValue = dataread("DOC_FIN").ToString
                    DropDownList2.SelectedValue = dataread("PICKUP").ToString
                    DropDownList3.SelectedValue = dataread("PLACE").ToString
                    DropDownList4.SelectedValue = dataread("TATENE").ToString
                    DropDownList5.SelectedValue = dataread("CURRENCY").ToString
                    DropDownList6.SelectedValue = dataread("SHUK_METH").ToString

                    TextBox1.Text = dataread("REQUESTED_DATE").ToString
                    TextBox2.Text = dataread("CREATED_DATE").ToString
                    TextBox3.Text = dataread("ETD").ToString
                    TextBox4.Text = dataread("CUST_CD").ToString
                    TextBox5.Text = dataread("IVNO").ToString
                    TextBox6.Text = dataread("REQUESTER").ToString
                    TextBox7.Text = dataread("DEPARTMENT").ToString
                    TextBox8.Text = dataread("AUTHOR").ToString
                    TextBox9.Text = dataread("SHIPPING_COMPANY").ToString
                    TextBox10.Text = dataread("REMARKS").ToString
                    TextBox11.Text = dataread("SNNO").ToString
                    TextBox12.Text = dataread("CUT_DATE").ToString
                    TextBox13.Text = dataread("HAN_DATE").ToString
                    TextBox14.Text = dataread("ETA").ToString
                    If dataread("RATE").Equals(DBNull.Value) Then
                        TextBox16.Text = ""
                    Else
                        TextBox16.Text = dataread("RATE")
                    End If

                    TextBox17.Text = dataread("TRADE_TERM").ToString
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()
                cnn.Close()
                cnn.Dispose()
            End If

            'モードによりボタン名称を変更する
            If strMode = "01" Then
                Button1.Text = "更　　新"
                Button1.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
                Button2.Attributes.Add("onclick", "return confirm('削除します。よろしいですか？');")
                Button2.Visible = True
            Else
                Button1.Text = "登　　録"
                Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
                Button2.Visible = False
            End If

        End If

    End Sub

    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録
        Dim strSQL As String
        Dim strVal As String = ""
        Dim dtNow As DateTime = DateTime.Now

        'パラメータ取得
        Dim strCode As String = Session("strCode")
        Dim strMode As String = Session("strMode")

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        If strMode = "01" And strExecMode = "01" Then
            'データ更新
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_AIR_MANAGE "
            strSQL = strSQL & "SET "
            strSQL = strSQL & "  REQUESTED_DATE =  '" & LTrim(RTrim(TextBox1.Text)) & "' "
            strSQL = strSQL & "  , CREATED_DATE =  '" & LTrim(RTrim(TextBox2.Text)) & "' "
            strSQL = strSQL & "  , ETD =  '" & LTrim(RTrim(TextBox3.Text)) & "' "
            strSQL = strSQL & "  , CUST_CD =  '" & LTrim(RTrim(TextBox4.Text)) & "' "
            strSQL = strSQL & "  , IVNO =  '" & LTrim(RTrim(TextBox5.Text)) & "' "
            strSQL = strSQL & "  , REQUESTER =  '" & LTrim(RTrim(TextBox6.Text)) & "' "
            strSQL = strSQL & "  , DEPARTMENT =  '" & LTrim(RTrim(TextBox7.Text)) & "' "
            strSQL = strSQL & "  , AUTHOR =  '" & LTrim(RTrim(TextBox8.Text)) & "' "
            strSQL = strSQL & "  , DOC_FIN =  '" & DropDownList1.SelectedValue & "' "
            strSQL = strSQL & "  , SHIPPING_COMPANY =  '" & LTrim(RTrim(TextBox9.Text)) & "' "
            strSQL = strSQL & "  , PICKUP =  '" & DropDownList2.SelectedValue & "' "
            strSQL = strSQL & "  , PLACE =  '" & DropDownList3.SelectedValue & "' "
            strSQL = strSQL & "  , REMARKS =  '" & LTrim(RTrim(TextBox10.Text)) & "' "
            strSQL = strSQL & "  , UPD_DATE = '" & dtNow & "' "
            strSQL = strSQL & "  , UPD_PERSON = '" & Session("UsrId") & "' "
            strSQL = strSQL & "  , SNNO = '" & LTrim(RTrim(TextBox11.Text)) & "' "  'SNNO
            strSQL = strSQL & "  , CUT_DATE = '" & LTrim(RTrim(TextBox12.Text)) & "' "  'CUT日
            strSQL = strSQL & "  , HAN_DATE = '" & LTrim(RTrim(TextBox13.Text)) & "' "  '搬入日
            strSQL = strSQL & "  , ETA = '" & LTrim(RTrim(TextBox14.Text)) & "' "  '到着日
            strSQL = strSQL & "  , TATENE = '" & DropDownList4.SelectedValue & "' "   '建値
            strSQL = strSQL & "  , TRADE_TERM = '" & LTrim(RTrim(TextBox17.Text)) & "' "  'Trade Term
            strSQL = strSQL & "  , CURRENCY = '" & DropDownList5.SelectedValue & "' "   '通貨
            strSQL = strSQL & "  , RATE = '" & LTrim(RTrim(TextBox16.Text)) & "' "  'レート
            strSQL = strSQL & "  , SHUK_METH = '" & DropDownList6.SelectedValue & "' "  '出荷方法
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "       AIR_CODE =  '" & strCode & "' "
        ElseIf strMode = "01" And strExecMode = "02" Then
            strSQL = ""
            strSQL = strSQL & "DELETE FROM  T_EXL_AIR_MANAGE "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "       AIR_CODE =  '" & strCode & "' "
        Else
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_AIR_MANAGE "
            strSQL = strSQL & "VALUES ( "
            strSQL = strSQL & "    '" & LTrim(RTrim(TextBox1.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox2.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox3.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox4.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox5.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox6.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox7.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox8.Text)) & "' "
            strSQL = strSQL & "  , '" & DropDownList1.SelectedValue & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox9.Text)) & "' "
            strSQL = strSQL & "  , '" & DropDownList2.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList3.SelectedValue & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox10.Text)) & "' "
            strSQL = strSQL & "  , '" & dtNow & "' "
            strSQL = strSQL & "  , '" & Session("UsrId") & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox11.Text)) & "' "  'SNNO
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox12.Text)) & "' "  'CUT日
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox13.Text)) & "' "  '搬入日
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox14.Text)) & "' "  '到着日
            strSQL = strSQL & "  , '" & DropDownList4.SelectedValue & "' "   '建値
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox17.Text)) & "' "  'Trade Term
            strSQL = strSQL & "  , '" & DropDownList5.SelectedValue & "' "   '通貨
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox16.Text)) & "' "  'レート
            strSQL = strSQL & "  , '" & DropDownList6.SelectedValue & "' "  '出荷方法
            strSQL = strSQL & ") "
        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        '必須チェック
        If Trim(TextBox1.Text) = "" Then
            Label1.Text = "依頼日は必須入力です。"
            chk_Nyuryoku = False
        End If
        If Trim(TextBox2.Text) = "" Then
            Label1.Text = "作成日は必須入力です。"
            chk_Nyuryoku = False
        End If
        If Trim(TextBox3.Text) = "" Then
            Label1.Text = "ETDは必須入力です。"
            chk_Nyuryoku = False
        End If
        If Trim(TextBox4.Text) = "" Then
            Label1.Text = "客先コードは必須入力です。"
            chk_Nyuryoku = False
        End If
        If DropDownList4.SelectedValue = "" Then
            Label1.Text = "建値は必須入力です。"
            chk_Nyuryoku = False
        End If
        If TextBox11.Text = "" Then
            Label1.Text = "SNNOは必須入力です。"
            chk_Nyuryoku = False
        End If
        If TextBox12.Text = "" Then
            Label1.Text = "CUT日は必須入力です。"
            chk_Nyuryoku = False
        End If
        If TextBox13.Text = "" Then
            Label1.Text = "搬入日は必須入力です。"
            chk_Nyuryoku = False
        End If
        If TextBox14.Text = "" Then
            Label1.Text = "到着日は必須入力です。"
            chk_Nyuryoku = False
        End If
        If TextBox17.Text = "" Then
            Label1.Text = "Trade Termは必須入力です。"
            chk_Nyuryoku = False
        End If
        If TextBox16.Text = "" Then
            Label1.Text = "レートは必須入力です。"
            chk_Nyuryoku = False
        End If
        If DropDownList5.SelectedValue = "" Then
            Label1.Text = "通貨は必須選択です。"
            chk_Nyuryoku = False
        End If
        If DropDownList4.SelectedValue = "" Then
            Label1.Text = "建値は必須選択です。"
            chk_Nyuryoku = False
        End If
        If DropDownList6.SelectedValue = "" Then
            Label1.Text = "出荷方法は必須選択です。"
            chk_Nyuryoku = False
        End If

        '全角入力チェック
        If HankakuEisuChk(TextBox4.Text) = False And Trim(TextBox4.Text) <> "" Then
            Label1.Text = "客先コードに全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox5.Text) = False And Trim(TextBox5.Text) <> "" Then
            Label1.Text = "IVNOに全角文字が使用されています。"
            chk_Nyuryoku = False
        End If

        '桁数チェック
        If Len(TextBox4.Text) <> 4 Then
            Label1.Text = "客先コードは４桁で入力してください。"
            chk_Nyuryoku = False
        End If
        If TextBox5.Text <> "" And Len(TextBox5.Text) <> 4 Then
            Label1.Text = "IVNOは４桁で入力してください。"
            chk_Nyuryoku = False
        End If

    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '更新（登録）ボタンクリックイベント
        Dim dbcmd As SqlCommand
        Dim dataread As SqlDataReader
        Dim StrSQL As String = ""

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        'パラメータ取得
        Dim strMode As String = Session("strMode")

        '登録時、存在チェック
        If strMode = "02" Then
            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            StrSQL = ""
            StrSQL = StrSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_AIR_MANAGE "
            StrSQL = StrSQL & "WHERE ETD = '" & Trim(TextBox3.Text) & "'"
            StrSQL = StrSQL & " AND IVNO = '" & Trim(TextBox5.Text) & "'"

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(StrSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            Dim intCnt As Integer = 0
            While (dataread.Read())
                intCnt = dataread("RecCnt")
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            If intCnt > 0 Then
                Label1.Text = "ETD:" & Trim(TextBox1.Text) & "、IVNO:" & Trim(TextBox5.Text) & "は既に登録済みです。"
                Return
            End If
        End If

        '更新(登録)
        Call DB_access("01")

        '元の画面に戻る
        Response.Redirect("air_management.aspx")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")

        '元の画面に戻る
        Response.Redirect("air_management.aspx")

    End Sub

    Private Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        '通貨変更時、最新のレートを貿易システムから取得し、テキストボックスに表示する。
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPA85;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim strYear As String = dt1.ToString("yyyy")
        Dim strMonth As String = dt1.ToString("MM")

        'データベース接続を開く
        cnn.Open()

        TextBox16.Text = ""

        strSQL = ""
        strSQL = strSQL & "SELECT RATE FROM M_RATE_TB "
        strSQL = strSQL & "WHERE CRY_YEAR = '" & strYear & "' "
        strSQL = strSQL & "AND   CRY_MONTH = '" & strMonth & "' "
        strSQL = strSQL & "AND   CRYCD = '" & DropDownList5.SelectedValue & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            TextBox16.Text = dataread("RATE").ToString
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub
End Class

