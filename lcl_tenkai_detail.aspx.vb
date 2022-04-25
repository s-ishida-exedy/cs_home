Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strinv As String = ""
        Dim strbkg As String = ""
        Dim strSQL As String = ""
        Dim strMode As String = ""
        Dim strcust As String = ""
        Dim strcut As String = ""
        Dim stretd As String = ""
        Dim strM3 As String = ""
        Dim strwgt As String = ""
        Dim strpkg As String = ""
        Dim lstrin As String = ""
        Dim lstrdr As String = ""

        Label3.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得

            '更新モード　DBから値取得し、セット

            strcust = Session("lstrcust")
            strinv = Session("lstrinv")
            strbkg = Session("lstrbkg")
            strcut = Session("lstrcut")
            stretd = Session("lstretd")
            strM3 = Session("lstrM3")
            strwgt = Session("lstrwgt")
            strpkg = Session("lstrpkg")
            lstrin = Session("lstrin")
            lstrdr = Session("lstrdr")

            If strwgt = "&nbsp;" Or strwgt = "" Then

                strwgt = ""

            End If

            If strpkg = "&nbsp;" Or strpkg = "" Then

                strpkg = ""

            End If

            Label1.Text = strcust
            Label2.Text = strinv
            Label4.Text = strbkg
            Label5.Text = strcut
            Label6.Text = stretd
            Label7.Text = strM3
            TextBox5.Text = strwgt
            TextBox6.Text = strpkg
            Label9.Text = Replace(lstrin, "<br>", " ")


            Dim i As Long

            Dim intEtd As Integer
            Dim intCnt As Integer
            Dim stretdval As String
            Dim intEtd2 As Integer
            Dim strdr(3) As String

            i = 1
            intCnt = 0
            intEtd = 0
            intEtd2 = 0
            intCnt = InStr(intCnt + 1, lstrdr, "<br>")

            Do While intCnt > 0

                intEtd = intCnt
                stretdval = Mid(lstrdr, intEtd2 + 1, intEtd - 1 - intEtd2)
                strdr(i) = stretdval

                intEtd2 = intEtd + 3
                intCnt = InStr(intCnt + 1, lstrdr, "<br>")
                i = i + 1
            Loop

            TextBox1.Text = strdr(1)
            TextBox2.Text = strdr(2)
            TextBox3.Text = Replace(Right(lstrdr, Len(lstrdr) - intEtd2), "&nbsp;", "")

        End If



    End Sub

    Private Sub DB_access()
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

        Dim strinv As String = ""
        Dim strbkg As String = ""
        Dim strMode As String = ""
        Dim strcust As String = ""
        Dim strcut As String = ""
        Dim stretd As String = ""
        Dim strM3 As String = ""
        Dim strwgt As String = ""
        Dim strpkg As String = ""
        Dim lstrin As String = ""

        strcust = Label1.Text
        strinv = Label2.Text
        strbkg = Label4.Text
        strcut = Label5.Text
        stretd = Label6.Text
        strM3 = Label7.Text
        strwgt = TextBox5.Text
        strpkg = TextBox6.Text
        lstrin = Label9.Text

        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET"
        strSQL = strSQL & " WEIGHT = '" & strwgt & "' "
        strSQL = strSQL & ",QTY = '" & strpkg & "' "
        strSQL = strSQL & ",FLG04 = '" & TextBox1.Text & "__" & TextBox2.Text & "__" & TextBox3.Text & "' "

        strSQL = strSQL & "WHERE CUST = '" & strcust & "' "
        strSQL = strSQL & "AND INVOICE_NO = '" & strinv & "' "
        strSQL = strSQL & "AND BOOKING_NO = '" & strbkg & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True


    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント

        '更新
        Call DB_access()        '更新モード



        Session.Remove("lstrcust")
        Session.Remove("lstrinv")
        Session.Remove("lstrbkg")
        Session.Remove("lstrcut")
        Session.Remove("lstretd")
        Session.Remove("lstrM3")
        Session.Remove("lstrwgt")
        Session.Remove("lstrpkg")
        Session.Remove("lstrin")
        Session.Remove("lstrdr")


        '元の画面に戻る
        Response.Redirect("lcl_tenkai.aspx")
    End Sub

End Class

