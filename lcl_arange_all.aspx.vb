Imports System.Data.SqlClient
Imports System.Data
Imports mod_function
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String




    Private Sub GET_IVDATA(bkgno As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        'strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE Decs "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv = Convert.ToString(dataread("OLD_INVNO"))        'ETD(計上日)
            Call INS_LCL(strinv, bkgno)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GET_IVDATA2(strinv As String, A As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim bkgno As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1


        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT distinct T_INV_HD_TB.BOOKINGNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.OLD_INVNO like '%" & strinv & "%' "
        strSQL = strSQL & "AND BOOKINGNO is not null "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            bkgno = Convert.ToString(dataread("BOOKINGNO"))        'ETD(計上日)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If bkgno = "" Or IsNothing(bkgno) = True Then
        Else

            strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "

            strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
            strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
            strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
            'strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
            'strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
            'strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE Decs "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            strDate = ""
            '結果を取り出す 
            While (dataread.Read())
                strinv = Convert.ToString(dataread("OLD_INVNO"))        'ETD(計上日)
                If A = "1" Then
                    Call INS_LCL(strinv, bkgno)
                Else
                End If
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

        End If

        cnn.Close()
        cnn.Dispose()

    End Sub
    Private Sub INS_LCL(strinv As String, bkgno As String)
        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand

        Dim intCnt As Long

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()


        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '005' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "



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

            strSQL = strSQL & "T_EXL_WORKSTATUS00.INVNO = '" & strinv & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.ID = '005' "

        Else

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
            strSQL = strSQL & " '005' "
            strSQL = strSQL & ",'" & strinv & "' "
            strSQL = strSQL & ",'" & bkgno & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ")"


        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Me.Label1.Text = ""

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strinv As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim ivno As String = ""
        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand
        Dim intCnt As Long
        Dim strkd As String
        Dim stram As String

        Dim dt1 As DateTime = DateTime.Now


        If Weekday(dt1) > 6 Then
            cno = 7 - Weekday(dt1) + 6
        Else
            cno = 6 - Weekday(dt1) + 7
        End If

        Dim ts1 As New TimeSpan(cno, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1

        '最終更新年月日を表示
        Me.Label1.Text = "手配対象期間：" & dt1.ToShortDateString & " (" & dt1.ToString("ddd") & ") " & "~ " & dt2.ToShortDateString & " (" & dt2.ToString("ddd") & ") "




        Dim strupddate00 As Date
        Dim strupddate01 As Date

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DATA_UPD.DATA_UPD FROM T_EXL_DATA_UPD "
        strSQL = strSQL & "WHERE T_EXL_DATA_UPD.DATA_CD ='008' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strupddate00 = Trim(dataread("DATA_UPD"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        Dim dt00 As String = dt1.ToShortDateString
        Dim dt01 As String = strupddate00.ToShortDateString



        If dt00 = dt01 Then
            Panel3.Visible = False
        Else
            Panel3.Visible = True
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub



    Private Sub GridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strinv As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String
        Dim aflg As Long = 0
        Dim dt1 As DateTime = DateTime.Now

        Dim ts01 As New TimeSpan(100, 0, 0, 0)
        Dim ts02 As New TimeSpan(100, 0, 0, 0)
        Dim dt02 As DateTime = dt1 + ts01
        Dim dt03 As DateTime = dt1 - ts01

        Dim dt002 As String
        Dim dt003 As String

        dt002 = dt02.ToString("yyyy/MM/dd")
        dt003 = dt03.ToString("yyyy/MM/dd")

        Dim ts03 As New TimeSpan(30, 0, 0, 0)
        Dim dt04 As DateTime = dt1 + ts03

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim str1 As String = Left(e.Row.Cells(2).Text, 4)
            Select Case str1
                Case "C258"
                    cno = 8
                Case "C255"
                    cno = 10
                Case Else
                    cno = 7
            End Select

            If e.Row.Cells(6).Text = "" Or e.Row.Cells(6).Text = "&nbsp;" Then

                Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(7).Text)
                Dim ts1 As New TimeSpan(cno, 0, 0, 0)
                Dim dt2 As DateTime = dt3 - ts1
                e.Row.Cells(10).Text = "CUT日未記入"

                e.Row.Cells(6).Text = dt2

            End If

        End If



        '搬入日作成

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        'ヘッダー以外に処理
        If e.Row.RowType = DataControlRowType.DataRow Then

            '対象の日付以下の日付の最大値を取得

            strSQL = "SELECT MAX(WORKDAY) AS WDAY01 FROM [T_EXL_CSWORKDAY] WHERE [T_EXL_CSWORKDAY].WORKDAY < '" & e.Row.Cells(6).Text & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())
                wday2 = dataread("WDAY01")
            End While

            If Weekday(dt1) > 6 Then
                cno = 7 - Weekday(dt1) + 6
            Else
                cno = 6 - Weekday(dt1) + 7
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Cells(5).Text = wday2

                Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(5).Text)
                Dim ts1 As New TimeSpan(cno, 0, 0, 0)
                Dim dt2 As DateTime = dt1 + ts1

                If dt3 < dt2 Then
                    e.Row.BackColor = Drawing.Color.Salmon
                    If (e.Row.Cells(10).Text.Length = 6) And dt3 < dt2 Then
                        e.Row.Cells(10).Text = "AC要"
                        e.Row.Cells(10).BackColor = Drawing.Color.Red
                        e.Row.Cells(10).ForeColor = Drawing.Color.White
                    End If
                End If
                e.Row.Cells(5).Text = e.Row.Cells(5).Text & " (" & dt3.ToString("ddd") & ")"
            End If

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

            If e.Row.Cells(10).Text = "CUT日未記入" Then
                e.Row.Cells(10).BackColor = Drawing.Color.Red
                e.Row.Cells(10).ForeColor = Drawing.Color.White
            End If
        End If


        'strSQL = "SELECT LCLARGD_INVNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].LCLARGD_INVNO = '" & Left(e.Row.Cells(3).Text, 4) & "' "
        strSQL = "SELECT INVNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].INVNO = '" & Left(e.Row.Cells(3).Text, 4) & "' "
        strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '004' "
        strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].REGDATE BETWEEN '" & dt003 & "' AND '" & dt002 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        aflg = 0
        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("INVNO")
            '書類作成状況
            If Left(e.Row.Cells(3).Text, 4) = strinv Then
                aflg = 1
                If e.Row.Cells(10).Text = "AC要" Then
                    e.Row.Cells(10).Text = " Booking依頼済み"

                End If
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()





        strSQL = "SELECT INVOICE_NO FROM [T_EXL_LCLTENKAI] WHERE [T_EXL_LCLTENKAI].INVOICE_NO = '" & e.Row.Cells(3).Text & "' "
        strSQL = strSQL & "AND [T_EXL_LCLTENKAI].FLG03 = '1' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("INVOICE_NO")
            '書類作成状況
            If e.Row.Cells(3).Text = strinv Then
                e.Row.BackColor = Drawing.Color.LightBlue
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()




        strSQL = "SELECT INVNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].INVNO = '" & Left(e.Row.Cells(3).Text, 4) & "' "
        strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '002' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO ='" & e.Row.Cells(10).Text & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("INVNO")
            '書類作成状況
            If Left(e.Row.Cells(3).Text, 4) = strinv Then
                e.Row.BackColor = Drawing.Color.LightGray
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

        Dim icnt As Integer = 0

        If e.Row.Cells(11).Text = "" Or e.Row.Cells(11).Text = "&nbsp;" Then
            icnt = 1
        End If

        If e.Row.Cells(12).Text = "" Or e.Row.Cells(12).Text = "&nbsp;" Then
            icnt = 1
        End If

        If e.Row.Cells(13).Text = "" Or e.Row.Cells(13).Text = "&nbsp;" Then
            icnt = 1
        End If

        If e.Row.Cells(14).Text = "" Or e.Row.Cells(14).Text = "&nbsp;" Then
            icnt = 1
        End If

        If icnt = 1 Then
            e.Row.Cells(1).BackColor = Drawing.Color.Purple
            e.Row.Cells(1).ForeColor = Drawing.Color.White
        End If





        If aflg = 1 Then
        Else
            If Left(e.Row.Cells(2).Text, 4) = "C6G0" Then
                e.Row.Cells(2).BackColor = Drawing.Color.LightGreen
            End If
            If Left(e.Row.Cells(2).Text, 4) = "C255" Or Left(e.Row.Cells(2).Text, 4) = "C257" Or Left(e.Row.Cells(2).Text, 4) = "C258" Then
                e.Row.Cells(2).BackColor = Drawing.Color.Khaki
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt05 As DateTime = DateTime.Parse(e.Row.Cells(7).Text)
            If dt04 < dt05 Then
                e.Row.Cells(7).BackColor = Drawing.Color.Gray
            End If
        End If



        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As ImageButton = e.Row.FindControl("ImageButton1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click



        Dim struid As String = Session("UsrId")
        Dim strfrom As String = GET_from(struid)
        '        Dim strto As String = "r-fukao@exedy.com,s-ishida@exedy.com"
        Dim strto As String = "r-fukao@exedy.com,r-fukao@exedy.com"

        Dim strsyomei As String = GET_syomei(struid)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容


        'メールの件名
        Dim subject As String = "【異常報告】Bookingシート未更新"

        'メールの本文
        Dim body As String = "<html><body>Bookingシート未更新です。</body></html>" ' UriBodyC()

        body = "<font size=" & Chr(34) & " 3" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))


        If strto <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strto.Split(",")
            For Each c In strVal
                message.To.Add(New MailboxAddress("", c))
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

        strSQL = strSQL & "SELECT MAIL_ADD FROM M_EXL_LCL_DEC_MAIL "
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

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim strcust = Me.GridView2.Rows(index).Cells(2).Text
            Dim striv = Me.GridView2.Rows(index).Cells(3).Text
            Dim strhan = Me.GridView2.Rows(index).Cells(5).Text
            Dim strcut = Me.GridView2.Rows(index).Cells(6).Text
            Dim stretd = Me.GridView2.Rows(index).Cells(7).Text
            Dim streta = Me.GridView2.Rows(index).Cells(8).Text
            Dim strniryou = Me.GridView2.Rows(index).Cells(9).Text
            Dim strbkg = Me.GridView2.Rows(index).Cells(10).Text



            Session("strMode") = "0"    '更新モード
            Session("strcust") = strcust
            Session("striv") = striv
            Session("strhan") = strhan
            Session("strcut") = strcut
            Session("stretd") = stretd
            Session("streta") = streta
            Session("strniryou") = strniryou
            Session("strbkg") = strbkg


            Response.Redirect("lcl_arange_all_detail.aspx")

        End If

    End Sub

End Class
