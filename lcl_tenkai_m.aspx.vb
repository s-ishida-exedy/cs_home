


Imports System.Data.SqlClient
Imports System.Data
Imports mod_function
Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text
Imports System.IO
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String


    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.Cells(20).Text = "1" Then
            e.Row.BackColor = Drawing.Color.DarkGray
        End If

        e.Row.Cells(7).Visible = False
        e.Row.Cells(8).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False
        e.Row.Cells(13).Visible = False
        e.Row.Cells(14).Visible = False
        e.Row.Cells(20).Visible = False

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""

        Dim dt1 As DateTime = DateTime.Now

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim intCnt As Long

        'データベース接続を開く
        cnn.Open()

        '表示ボタン　FLG03は表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then

                'FIN_FLGを更新
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET FLG03 ='1' "
                strSQL = strSQL & ",FLG05 ='" & dt1.ToShortDateString & "' "
                strSQL = strSQL & "WHERE CUST = '" & GridView1.Rows(I).Cells(5).Text & "'"
                strSQL = strSQL & "AND ETD = '" & GridView1.Rows(I).Cells(10).Text & "'"
                strSQL = strSQL & "AND LCL_SIZE = '" & GridView1.Rows(I).Cells(12).Text & "'"

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(GridView1.Rows(I).Cells(7).Text, 1)
                Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(6).Text, 4), 1)
                Call GET_IVDATA3(Left(GridView1.Rows(I).Cells(6).Text, 4), GridView1.Rows(I).Cells(7).Text, 1)

                strSQL = ""
                strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_LCLCUSTPREADS WHERE "
                strSQL = strSQL & "T_EXL_LCLCUSTPREADS.CUSTCODE = '" & GridView1.Rows(I).Cells(5).Text & "' "

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
                    strSQL = strSQL & "UPDATE T_EXL_LCLCUSTPREADS SET ADDRESS ='" & GridView1.Rows(I).Cells(19).Text & "' "
                    strSQL = strSQL & "WHERE CUSTCODE = '" & GridView1.Rows(I).Cells(5).Text & "'"
                Else
                    strSQL = ""
                    strSQL = strSQL & "INSERT INTO T_EXL_LCLCUSTPREADS VALUES("
                    strSQL = strSQL & "'" & GridView1.Rows(I).Cells(5).Text & "' "
                    strSQL = strSQL & ",'" & GridView1.Rows(I).Cells(19).Text & "' "
                    strSQL = strSQL & ")"
                End If
                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Else

            End If
        Next

        GridView1.DataBind()

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""

        'データベース接続を開く
        cnn.Open()

        '非表示ボタン　FLG03は非表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then

                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET FLG03 ='' "
                strSQL = strSQL & "WHERE CUST = '" & GridView1.Rows(I).Cells(5).Text & "'"
                strSQL = strSQL & "AND ETD = '" & GridView1.Rows(I).Cells(10).Text & "'"
                strSQL = strSQL & "AND LCL_SIZE = '" & GridView1.Rows(I).Cells(12).Text & "'"

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(GridView1.Rows(I).Cells(7).Text, 2)
                Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(6).Text, 4), 2)
                Call GET_IVDATA3(Left(GridView1.Rows(I).Cells(6).Text, 4), GridView1.Rows(I).Cells(7).Text, 2)
            Else
            End If
        Next

        GridView1.DataBind()

    End Sub

    Private Sub GET_IVDATA(bkgno As String, A As String)

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
            If A = "1" Then
                Call INS_LCL(strinv, bkgno)
            Else
                Call DEL_LCL(strinv, bkgno)
            End If
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
                    Call DEL_LCL(strinv, bkgno)
                End If
            End While
            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GET_IVDATA3(strinv As String, strbkg As String, A As String)

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

        If strbkg = "" Or IsNothing(strbkg) = True Then
        Else
            If A = "1" Then
                Call INS_LCL(strinv, strbkg)
            Else
                Call DEL_LCL(strinv, strbkg)
            End If
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

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

        End If

        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub DEL_LCL(strinv As String, bkgno As String)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

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
                strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '005' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Else
            End If
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容

        Dim strfrom2 As String = GET_ToAddress(1, 1)
        Dim strto2 As String = GET_ToAddress(1, 0)

        Dim strfrom As String = "r-fukao@exedy.com"
        Dim strto As String = "r-fukao@exedy.com"

        'メールの件名
        Dim subject As String = "TEST<通知>LCL案件展開　変更・追加・連絡" '"【AIR " & strIrai & "依頼" & Session("strCust") & "向け】"

        'メールの本文
        Dim body As String = "<html><body><p>各位<p>お世話になっております。<p>LCL出荷案件の内容に変更、追加がございましたのでご連絡いたします。</p>下記URLに重量の登録をお願いいたします。</p>http://kbhwpm01/exp/cs_home/lcl_tenkai.aspx</p></body></html>" ' UriBodyC()

        Dim t As String = "<html><body><Table border='1' style='Font-Size:11px;'><tr><td>備考</td><td>客先</td><td>IN_NO</td><td>カット日</td><td>出港日</td><td>M3</td><td>重量</td><td>荷量</td><td>引取希望日</td><td></td><td>搬入希望日</td><td></td><td>搬入先</td></tr>"

        For I = 0 To GridView1.Rows.Count - 1
            t = t & "<tr><td >" & GridView1.Rows(I).Cells(2).Text & "</td><td>" & GridView1.Rows(I).Cells(5).Text & "</td><td>" & GridView1.Rows(I).Cells(6).Text & "</td><td>" & GridView1.Rows(I).Cells(9).Text & "</td><td>" & GridView1.Rows(I).Cells(11).Text & "</td><td>" & GridView1.Rows(I).Cells(12).Text & "</td><td>" & GridView1.Rows(I).Cells(13).Text & "</td><td>" & GridView1.Rows(I).Cells(14).Text & "</td><td>" & GridView1.Rows(I).Cells(15).Text & "</td><td>" & GridView1.Rows(I).Cells(16).Text & "</td><td>" & GridView1.Rows(I).Cells(17).Text & "</td><td>" & GridView1.Rows(I).Cells(18).Text & "</td><td>" & GridView1.Rows(I).Cells(19).Text & "</td></tr>"
        Next

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "====内容==================================</p>" & TextBox1.Text.Replace(vbCrLf, "<br/>") & "</p>" ' UriBodyC()

        body = body & body2

        body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))


        ' 宛先情報  
        message.To.Add(MailboxAddress.Parse(strto))

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

        TextBox1.Text = ""


    End Sub


    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Button3.Attributes.Add("onclick", "return confirm('メールを送付します。よろしいですか？');")

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


End Class
