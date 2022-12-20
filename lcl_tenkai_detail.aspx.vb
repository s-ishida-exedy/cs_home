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
        Dim lstflg01 As String = ""

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand



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

        '接続文字列の作成
        Dim ConnectionString00 As String = String.Empty

        'SQL Server認証
        ConnectionString00 = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn00 = New SqlConnection(ConnectionString00)
        Dim Command00 = cnn00.CreateCommand

        'データベース接続を開く
        cnn00.Open()

        strSQL = "SELECT CODE "
        strSQL = strSQL & "FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE CODE = '" & Session("UsrId") & "' "

        'ＳＱＬコマンド作成
        dbcmd = New SqlCommand(strSQL, cnn00)
        'ＳＱＬ文実行
        dataread = dbcmd.ExecuteReader()
        strbkg = ""
        '結果を取り出す
        While (dataread.Read())
            fflg2 = dataread("CODE")
        End While

        'クローズ処理
        dataread.Close()
        dbcmd.Dispose()

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
            lstflg01 = Session("lstflg01")

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

            If fflg2 = "" Then
                DropDownList1.Visible = False
            End If


            cnn00.Close()
            cnn00.Dispose()



            If lstflg01 = "1" Then
                DropDownList1.SelectedValue = "非表示"
            Else
                DropDownList1.SelectedValue = "表示"
            End If


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
        Dim lstrflg01 As String = ""

        strcust = Label1.Text
        strinv = Label2.Text
        strbkg = Label4.Text
        strcut = Label5.Text
        stretd = Label6.Text
        strM3 = Label7.Text
        strwgt = TextBox5.Text
        strpkg = TextBox6.Text
        lstrin = Label9.Text
        lstrflg01 = DropDownList1.SelectedValue

        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET"
        strSQL = strSQL & " WEIGHT = '" & strwgt & "' "
        strSQL = strSQL & ",QTY = '" & strpkg & "' "
        strSQL = strSQL & ",FLG04 = '" & TextBox1.Text & "__" & TextBox2.Text & "__" & TextBox3.Text & "' "

        If lstrflg01 = "表示" Then
            strSQL = strSQL & ",FLG01 = '0' "
        Else
            strSQL = strSQL & ",FLG01 = '1' "
        End If

        strSQL = strSQL & "WHERE CUST = '" & strcust & "' "



        strSQL = strSQL & "AND BOOKING_NO = '" & strbkg & "' "
        strSQL = strSQL & "AND FLG01 <> '1' "

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

