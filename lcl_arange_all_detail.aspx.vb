Imports System.Data.SqlClient
Imports mod_function
Imports MimeKit
'Imports MimeKit.Text
'Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strinv As String = ""
        Dim strbkg As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strMode As String = ""
        Dim strcust As String = ""
        Dim striv As String = ""
        Dim strhan As String = ""
        Dim strcut As String = ""
        Dim stretd As String = ""
        Dim stretd02 As Date
        Dim streta As String = ""
        Dim strniryou As String = ""

        Label3.Text = ""

        If IsPostBack Then



            ' そうでない時処理
        Else
            'パラメータ取得
            strMode = Session("strMode")
            strcust = Left(Session("strcust"), 4)
            striv = Session("striv")
            strhan = Session("strhan")
            strcut = Session("strcut")
            stretd = Session("stretd")
            streta = Session("streta")
            strniryou = Session("strniryou")
            strbkg = Trim(Session("strbkg"))
            stretd02 = Session("stretd")

            If strcust = "C258" Then
                If strMode = "0" Then

                    Label1.Text = "C258 CRF"

                    CheckBox1.Visible = False
                    Button7.Visible = False
                    TextBox2.Visible = False
                Else
                End If

            ElseIf strcust = "C253" Then
                If strMode = "0" Then

                    Label1.Text = "C253 CRF"

                    CheckBox1.Visible = False
                    Button7.Visible = False
                    TextBox2.Visible = False
                Else
                    End If

                ElseIf strcust = "C6G0" Then

                If strMode = "0" Then

                    Label1.Text = "C6G0 引取り日連絡メール"

                    TextBox2.Text = "島田TL殿、内田L殿"
                    TextBox2.Text = TextBox2.Text & vbLf
                    TextBox2.Text = TextBox2.Text & "いつもお世話になります。"
                    TextBox2.Text = TextBox2.Text & vbLf & vbLf
                    TextBox2.Text = TextBox2.Text & stretd02.ToString("MM") & "月のC6G0向け出荷に伴いまして"
                    TextBox2.Text = TextBox2.Text & vbLf
                    TextBox2.Text = TextBox2.Text & "下記の日程にて引取りに来られます。"
                    TextBox2.Text = TextBox2.Text & vbLf & vbLf
                    TextBox2.Text = TextBox2.Text & "前日までに引取り可能な状態にしていただくようお願いいたします。"
                    TextBox2.Text = TextBox2.Text & vbLf
                    TextBox2.Text = TextBox2.Text & "IV-" & striv
                    TextBox2.Text = TextBox2.Text & vbLf & vbLf
                    TextBox2.Text = TextBox2.Text & "※海貨業者引き取り日：" & strhan
                    TextBox2.Text = TextBox2.Text & vbLf & vbLf
                    TextBox2.Text = TextBox2.Text & "お手数ですが宜しくお願いいたします。"
                    CheckBox1.Visible = False

                Else
                End If
            ElseIf strcust = "C255" Then

                If strMode = "0" Then

                    Label1.Text = "C255 Booking依頼完了登録"
                    TextBox2.Visible = False
                    Button7.Visible = False
                    CheckBox1.Visible = False
                Else
                End If
            Else
                Response.Redirect("lcl_arange_all.aspx")
            End If
        End If

        Button7.Attributes.Add("onclick", "return confirm('メール送信します。よろしいですか？');")
        Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")

    End Sub

    Sub Mail01(struid As String, flg As Long, flg02 As Long)

        Dim subject As String = ""
        Dim body As String = ""
        Dim stretd As String = ""
        Dim striv As String = ""
        stretd = Session("stretd")
        striv = Session("striv")
        Dim l As Long = 1

        Dim strfrom As String = ""
        Dim strto As String = ""
        Dim strcc As String = ""

        'Dim strFilePath() As String

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        If flg = 1 Then 'C258

            strfrom = GET_from(struid)

            If flg02 = 1 Then
                strto = GET_ToAddress(8, 1)
                strto = Left(strto, Len(strto) - 1)
                strcc = GET_ToAddress(8, 0) + GET_from(struid)

            Else
                strto = GET_ToAddress(6, 1)
                strto = Left(strto, Len(strto) - 1)
                strcc = GET_ToAddress(6, 0) + GET_from(struid)
            End If

            Dim strsyomei As String = GET_syomei(struid)

            Dim f As String = ""

            'メールの件名
            subject = "【明細登録依頼】C258」NIJMEGEN向け　" & stretd & "ETD" & " IV-" & striv

            'メールの本文
            body = TextBox2.Text.Replace(vbCrLf, "<br/>")


            body = body & "以上、宜しくお願いします。お忙しい中とは存じますが、宜しくお願い申し上げます。<br></body></html>" & strsyomei

            body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
            body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

        ElseIf flg = 2 Then 'C6G0

            strfrom = GET_from(struid)
            strto = GET_ToAddress(7, 1)
            strto = Left(strto, Len(strto) - 1)
            strcc = GET_ToAddress(7, 0) + GET_from(struid)

            Dim strsyomei As String = GET_syomei(struid)

            Dim f As String = ""

            'メールの件名
            subject = "【ご確認】C6G0 搬入予定" & " IV-" & striv

            'メールの本文
            body = TextBox2.Text.Replace(vbCrLf, "<br/>")

            body = body & "以上、宜しくお願いします。お忙しい中とは存じますが、宜しくお願い申し上げます。<br></body></html>" & strsyomei

            body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
            body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

        End If

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))

        ' 宛先情報  
        If strto <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strto.Split(",")
            For Each c In strVal
                message.To.Add(New MailboxAddress("", c))
            Next
        End If


        If strcc <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strcc.Split(",")
            For Each c In strVal
                message.Cc.Add(New MailboxAddress("", c))
            Next
        End If

        ' 表題  
        message.Subject = subject

        ' 本文
        Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        textPart.Text = body
        message.Body = textPart

        Dim multipart = New MimeKit.Multipart("mixed")

        multipart.Add(textPart)

        message.Body = multipart

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
        End Using
        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('メールを送信しました。');</script>", False)

    End Sub

    Private Function GET_ToAddress(strkbn As String, strtocc As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_ToAddress = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "Select MAIL_ADD FROM M_EXL_LCL_DEC_MAIL "
        strSQL = strSQL & "WHERE kbn = '" & strkbn & "' "
        strSQL = strSQL & "AND TO_CC = '" & strtocc & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_ToAddress += dataread("MAIL_ADD") + ","
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Function GET_from(struid As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_from = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT e_mail FROM M_EXL_USR "
        strSQL = strSQL & "WHERE uid = '" & struid & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_from += dataread("e_mail")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Function GET_syomei(struid As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_syomei = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT MEMBER_NAME,COMPANY,TEAM,TEL_NO,E_MAIL FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE code = '" & struid & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_syomei += "<html><body>******************************<p></p>" + "" + dataread("MEMBER_NAME") + "<p></p>" + dataread("COMPANY") + "<p></p>" + dataread("TEL_NO") + "<p></p>" + dataread("E_MAIL") + "<p></p>" + "******************************</body></html>"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim strcust As String
        Dim flg As Long
        Dim flg02 As Long

        Dim striv As String = Session("striv")
        Dim strbkg As String = Trim(Session("strbkg"))

        Dim struid As String = Session("UsrId")
        strcust = Left(Session("strcust"), 4)

        If strcust = "C258" Then
            flg = 1
        ElseIf strcust = "C6G0" Then
            flg = 2
        End If

        If CheckBox1.Checked = True Then
            flg02 = 1
        Else
            flg02 = 2
        End If

        Call Mail01(struid, flg, flg02)
        Call reg_004(striv, strbkg)

        Session.Remove("strMode")
        Session.Remove("strcust")
        Session.Remove("striv")
        Session.Remove("strhan")
        Session.Remove("strcut")
        Session.Remove("stretd")
        Session.Remove("streta")
        Session.Remove("strniryou")
        Session.Remove("strbkg")

        '前の画面へ遷移
        Response.Redirect("lcl_arange_all.aspx")

    End Sub
    Private Sub reg_004(striv As String, strbkg As String)

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim intCnt As Long

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '004' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & striv & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & strbkg & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            intCnt = dataread("RecCnt")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If intCnt > 0 Then

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_WORKSTATUS00 SET "

            strSQL = strSQL & "T_EXL_WORKSTATUS00.INVNO = '" & striv & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.BKGNO = '" & strbkg & "' "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & striv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & strbkg & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.ID = '004' "

        Else

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
            strSQL = strSQL & " '004' "
            strSQL = strSQL & ",'" & striv & "' "
            strSQL = strSQL & ",'" & strbkg & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ",'" & Session("UsrId") & "_06" & "' "
            strSQL = strSQL & ")"

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        Dim strinv As String = ""
        Dim strbkg As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strMode As String = ""
        Dim strcust As String = ""
        Dim striv As String = ""
        Dim strhan As String = ""
        Dim strcut As String = ""
        Dim stretd As String = ""
        Dim stretd02 As Date
        Dim streta As String = ""
        Dim strniryou As String = ""

        'パラメータ取得
        strMode = Session("strMode")
        strcust = Left(Session("strcust"), 4)

        striv = Session("striv")
        strhan = Session("strhan")
        strcut = Session("strcut")
        stretd = Session("stretd")
        streta = Session("streta")
        strniryou = Session("strniryou")

        strbkg = Trim(Session("strbkg"))
        stretd02 = Session("stretd")

        If CheckBox1.Checked = True Then

            Label1.Text = "C258 ブッキング依頼メール（Ceva）"

            TextBox2.Text = "CEVA 小川様"
            TextBox2.Text = TextBox2.Text & vbLf
            TextBox2.Text = TextBox2.Text & "いつもお世話になります。"
            TextBox2.Text = TextBox2.Text & vbLf & vbLf
            TextBox2.Text = TextBox2.Text & stretd & "ETD希望でNIJMEGEN向けに出荷がございます。"
            TextBox2.Text = TextBox2.Text & vbLf & vbLf
            TextBox2.Text = TextBox2.Text & "お手数ですが手配をお願い申し上げます。"
            TextBox2.Text = TextBox2.Text & vbLf & vbLf
            TextBox2.Text = TextBox2.Text & "パスワード:exedy"
            TextBox2.Text = TextBox2.Text & vbLf
            TextBox2.Text = TextBox2.Text & "荷量は" & strniryou & "となります。"
            TextBox2.Text = TextBox2.Text & vbLf & vbLf
            TextBox2.Text = TextBox2.Text & "以上です。"

        Else

            Label1.Text = "C258 プロフォーマインボイス登録メール（EXL）"

            TextBox2.Text = "EXL 内田L殿"
            TextBox2.Text = TextBox2.Text & vbLf
            TextBox2.Text = TextBox2.Text & "いつもお世話になります。"
            TextBox2.Text = TextBox2.Text & vbLf & vbLf
            TextBox2.Text = TextBox2.Text & strcust & "向けに" & stretd & "ETDでオーダーがございます。"
            TextBox2.Text = TextBox2.Text & vbLf & vbLf
            TextBox2.Text = TextBox2.Text & "IV-" & striv & "にて登録致しました。"
            TextBox2.Text = TextBox2.Text & vbLf & vbLf
            TextBox2.Text = TextBox2.Text & "お手数ですがプロフォーマーインボイス用の仮登録をお願いします。"
            TextBox2.Text = TextBox2.Text & vbLf
            TextBox2.Text = TextBox2.Text & "Bookingに影響しますのでなるべく早くご対応いただければと存じます。 "
            TextBox2.Text = TextBox2.Text & vbLf & vbLf
            TextBox2.Text = TextBox2.Text & "以上、宜しくお願いします。"

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim strcust As String
        Dim flg As Long
        Dim flg02 As Long

        Dim striv As String = Session("striv")
        Dim strbkg As String = Trim(Session("strbkg"))

        Dim struid As String = Session("UsrId")
        strcust = Left(Session("strcust"), 4)

        If strcust = "C258" Then
            flg = 1
        ElseIf strcust = "C6G0" Then
            flg = 2
        End If

        If CheckBox1.Checked = True Then
            flg02 = 1
        Else
            flg02 = 2
        End If

        Call reg_004(striv, strbkg)

        Session.Remove("strMode")
        Session.Remove("strcust")
        Session.Remove("striv")
        Session.Remove("strhan")
        Session.Remove("strcut")
        Session.Remove("stretd")
        Session.Remove("streta")
        Session.Remove("strniryou")
        Session.Remove("strbkg")

        '前の画面へ遷移
        Response.Redirect("lcl_arange_all.aspx")



    End Sub
End Class

