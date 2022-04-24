Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Dim strMode As String = ""
        Dim lstrbokou As String = ""
        Dim lstrcust As String = ""
        Dim lstrinv As String = ""
        Dim lstrbkg As String = ""
        Dim lstrcut As String = ""
        Dim lstretd As String = ""
        Dim lstreta As String = ""
        Dim lstrm3 As String = ""
        Dim lstrwhg As String = ""
        Dim lstrpkg As String = ""
        Dim lstrp1 As String = ""
        Dim lstrp2 As String = ""
        Dim lstrm1 As String = ""
        Dim lstrm2 As String = ""
        Dim lstrpp As String = ""
        Dim lstrf3 As String = ""

        Label3.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            strMode = Session("strMode")

            If strMode = "0" Then

                lstrbokou = Session("lstrbokou")
                lstrcust = Session("lstrcust")
                lstrinv = Session("lstrinv")
                lstrbkg = Session("lstrbkg")
                lstrcut = Session("lstrcut")
                lstretd = Session("lstretd")
                lstreta = Session("lstreta")
                lstrm3 = Session("lstrm3")
                lstrwhg = Session("lstrwhg")
                lstrpkg = Session("lstrpkg")
                lstrp1 = Session("lstrp1")
                lstrp2 = Session("lstrp2")
                lstrm1 = Session("lstrm1")
                lstrm2 = Session("lstrm2")
                lstrpp = Session("lstrpp")
                lstrf3 = Session("lstrf3")


                TextBox1.Text = Replace(lstrbokou, "&nbsp;", "")
                TextBox2.Text = Replace(lstrcust, "&nbsp;", "")
                TextBox3.Text = Replace(lstrinv, "&nbsp;", "")
                Label1.Text = Replace(lstrbkg, "&nbsp;", "")
                TextBox5.Text = Replace(lstrcut, "&nbsp;", "")
                TextBox6.Text = Replace(lstretd, "&nbsp;", "")
                TextBox7.Text = Replace(lstreta, "&nbsp;", "")
                TextBox8.Text = Replace(lstrm3, "&nbsp;", "")
                TextBox9.Text = Replace(lstrwhg, "&nbsp;", "")
                TextBox10.Text = Replace(lstrpkg, "&nbsp;", "")
                TextBox11.Text = Replace(lstrp1, "&nbsp;", "")
                TextBox12.Text = Replace(lstrp2, "&nbsp;", "")
                TextBox13.Text = Replace(lstrm1, "&nbsp;", "")
                TextBox14.Text = Replace(lstrm2, "&nbsp;", "")
                TextBox15.Text = Replace(Replace(lstrpp, "&nbsp;", ""), "<br>", "__")
                Label2.Text = Replace(lstrf3, "&nbsp;", "")


            End If
        End If

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

        '画面入力情報を変数に代入
        Dim lstrbokou As String = TextBox1.Text
        Dim lstrcust As String = TextBox2.Text
        Dim lstrinv As String = TextBox3.Text
        Dim lstrbkg As String = Label1.Text
        Dim lstrcut As String = TextBox5.Text
        Dim lstretd As String = TextBox6.Text
        Dim lstreta As String = TextBox7.Text
        Dim lstrm3 As String = TextBox8.Text
        Dim lstrwhg As String = TextBox9.Text
        Dim lstrpkg As String = TextBox10.Text
        Dim lstrp1 As String = TextBox11.Text
        Dim lstrp2 As String = TextBox12.Text
        Dim lstrm1 As String = TextBox13.Text
        Dim lstrm2 As String = TextBox14.Text
        Dim lstrpp As String = TextBox15.Text


        If strExecMode = "01" Then
            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET"
            strSQL = strSQL & " CUST = '" & lstrcust & "' "
            strSQL = strSQL & ",INVOICE_NO = '" & lstrinv & "' "
            strSQL = strSQL & ",BOOKING_NO = '" & lstrbkg & "' "
            strSQL = strSQL & ",CUT_DATE = '" & lstrcut & "' "
            strSQL = strSQL & ",ETD = '" & lstretd & "' "
            strSQL = strSQL & ",ETA = '" & lstreta & "' "
            strSQL = strSQL & ",LCL_SIZE = '" & lstrm3 & "' "
            strSQL = strSQL & ",WEIGHT = '" & lstrwhg & "' "
            strSQL = strSQL & ",QTY = '" & lstrpkg & "' "
            strSQL = strSQL & ",PICKUP01 = '" & lstrp1 & "' "
            strSQL = strSQL & ",PICKUP02 = '" & lstrp2 & "' "
            strSQL = strSQL & ",MOVEIN01 = '" & lstrm1 & "' "
            strSQL = strSQL & ",MOVEIN02 = '" & lstrm2 & "' "
            strSQL = strSQL & ",OTHERS01 = '" & lstrbokou & "' "
            strSQL = strSQL & ",PICKINPLACE = '" & lstrpp & "' "
            strSQL = strSQL & "WHERE BOOKING_NO = '" & lstrbkg & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        ElseIf strExecMode = "02" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_LCLTENKAI "
            strSQL = strSQL & "WHERE BOOKING_NO = '" & lstrbkg & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()


            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '005' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & lstrbkg & "' "


            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If


        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True


    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント


        '更新
        Call DB_access("01")        '更新モード

        '元の画面に戻る
        Response.Redirect("lcl_tenkai_m.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("lcl_tenkai_m.aspx")
    End Sub

End Class

