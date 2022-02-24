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
            Dim strEtd As String = Session("strEtd")
            Dim strIvno As String = Session("strIvno")
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
                strSQL = strSQL & "WHERE ETD = '" & strEtd & "' "
                strSQL = strSQL & "AND   IVNO = '" & strIvno & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    DropDownList1.SelectedValue = dataread("DOC_FIN").ToString
                    DropDownList2.SelectedValue = dataread("PICKUP").ToString
                    DropDownList3.SelectedValue = dataread("PLACE").ToString

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

        'パラメータ取得
        Dim strEtd As String = Session("strEtd")
        Dim strIvno As String = Session("strIvno")
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
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "       ETD =  '" & strEtd & "' "
            strSQL = strSQL & "  AND IVNO =  '" & strIvno & "' "
        ElseIf strMode = "01" And strExecMode = "02" Then
            strSQL = ""
            strSQL = strSQL & "DELETE FROM  T_EXL_AIR_MANAGE "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "       ETD =  '" & strEtd & "' "
            strSQL = strSQL & "  AND IVNO =  '" & strIvno & "' "
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
        If Len(TextBox5.Text) <> 4 Then
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
End Class

