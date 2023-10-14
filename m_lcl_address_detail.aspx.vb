Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strNO As String = ""
        Dim strNAME As String = ""
        Dim strNAME01 As String = ""
        Dim strNAME02 As String = ""
        Dim strNAME03 As String = ""
        Dim strSQL As String = ""

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim strCode As String = ""
        Dim strMode As String = ""

        Label3.Text = ""

        Dim wday As String = ""
        Dim wday2 As String = ""
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim Kaika00 As String = ""

        Dim ts1 As New TimeSpan(80, 0, 0, 0)
        Dim ts2 As New TimeSpan(80, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1
        Dim fflg2 As String = ""



        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            strMode = Session("strMode")


            '接続文字列の作成
            Dim ConnectionString00 As String = String.Empty

            'SQL Server認証
            ConnectionString00 = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn00 = New SqlConnection(ConnectionString00)
            Dim Command00 = cnn00.CreateCommand

            'データベース接続を開く
            cnn00.Open()

            If strMode = "0" Then
                '更新モード　DBから値取得し、セット

                strNO = Session("NO")
                strNAME = Session("NAME")


                strSQL = ""
                strSQL = strSQL & "SELECT  "
                strSQL = strSQL & "  ADRS01 "
                strSQL = strSQL & "  , ADRS02 "
                strSQL = strSQL & "  , ADRS03 "
                strSQL = strSQL & "FROM M_EXL_LCL_WH "
                strSQL = strSQL & "WHERE WHCD = '" & strNO & "' "



                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn00)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    strNAME01 = dataread("ADRS01")
                    strNAME02 = dataread("ADRS02")
                    strNAME03 = dataread("ADRS03")
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()


                Label1.Text = strNO
                TextBox1.Text = strNAME01
                TextBox2.Text = strNAME02
                TextBox3.Text = strNAME03


                '登録ボタンを非表示
                Button1.Visible = False
            Else
                'CODEの現在値を取得する
                strSQL = ""
                strSQL = strSQL & "SELECT MAX(M_EXL_LCL_WH.WHCD) AS M "
                strSQL = strSQL & "FROM M_EXL_LCL_WH "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn00)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    '現在値＋１をセットする。
                    Label1.Text = dataread("M") + 1
                End While


                If Label1.Text < 10 Then
                    Label1.Text = "00" & Label1.Text
                ElseIf Label1.Text < 100 Then
                    Label1.Text = "0" & Label1.Text
                End If


                '更新ボタンを非表示
                Button2.Visible = False
                Button3.Visible = False
            End If



            cnn00.Close()
            cnn00.Dispose()

        End If


        Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
        Button2.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
        Button3.Attributes.Add("onclick", "return confirm('削除します。よろしいですか？');")

    End Sub

    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""

        Dim strNO As String = ""
        Dim strNAME As String = ""


        strNO = Label1.Text
        strNAME = TextBox1.Text

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        If strExecMode = "01" Then
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_LCL_WH SET"

            strSQL = strSQL & " ADRS01 = '" & TextBox1.Text & "' "
            strSQL = strSQL & ",ADRS02 = '" & TextBox2.Text & "' "
            strSQL = strSQL & ",ADRS03 = '" & TextBox3.Text & "' "

            strSQL = strSQL & "WHERE WHCD = '" & strNO & "' "

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_LCL_WH "
            strSQL = strSQL & "WHERE WHCD = '" & strNO & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_LCLCUSTPREADS "
            strSQL = strSQL & "WHERE ADDRESS = '" & strNO & "' "

        ElseIf strExecMode = "03" Then
            '登録
            strSQL = ""
            strSQL = strSQL & "INSERT INTO M_EXL_LCL_WH VALUES("
            strSQL = strSQL & "'" & Label1.Text & "' "
            strSQL = strSQL & ",'" & TextBox1.Text & "' "
            strSQL = strSQL & ",'" & TextBox2.Text & "' "
            strSQL = strSQL & ",'" & TextBox3.Text & "' ) "

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim address01 As String = TextBox1.Text
        Dim address02 As String = TextBox2.Text
        Dim address03 As String = TextBox3.Text

        If Len(address01) > 80 Then
            Label3.Text = "住所01が長すぎます。"
            chk_Nyuryoku = False
        ElseIf Len(address02) > 80 Then
            Label3.Text = "住所02が長すぎます。"
            chk_Nyuryoku = False
        ElseIf Len(address03) > 80 Then
            Label3.Text = "住所03が長すぎます。"
            chk_Nyuryoku = False
        End If

    End Function

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '更新ボタンクリックイベント

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '更新
        Call DB_access("01")        '更新モード


        Session.Remove("NO")
        Session.Remove("NAME")

        '元の画面に戻る
        Response.Redirect("m_lcl_address.aspx")
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード


        Session.Remove("NO")
        Session.Remove("NAME")

        '元の画面に戻る
        Response.Redirect("m_lcl_address.aspx")
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


        Session.Remove("strMode")

        Session.Remove("NO")
        Session.Remove("NAME")

        '元の画面に戻る
        Response.Redirect("m_lcl_address.aspx")

    End Sub

End Class

