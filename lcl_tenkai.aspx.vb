﻿


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

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Button5.Attributes.Add("onclick", "return confirm('メールを送付します。よろしいですか？');")
        Button1.Attributes.Add("onclick", "return confirm('メールを送付します。よろしいですか？');")
        Button6.Attributes.Add("onclick", "return confirm('メールを送付します。よろしいですか？');")
        GridView2.Columns(16).ItemStyle.Wrap = True

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
        Dim intval As Integer
        Dim intCnt As Integer

        Dim dt1 As DateTime = DateTime.Now.ToShortDateString

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(16).Text = Replace(e.Row.Cells(16).Text, "__", "<br>")
            e.Row.Cells(17).Text = Replace(e.Row.Cells(17).Text, "__", "<br>")

            intval = 0
            intCnt = InStr(intCnt + 1, e.Row.Cells(8).Text, "→")
            Do While intCnt > 0
                intval = intCnt
                intCnt = InStr(intCnt + 1, e.Row.Cells(8).Text, "→")
            Loop

            e.Row.Cells(8).Text = Mid(e.Row.Cells(8).Text, intval + 1, Len(e.Row.Cells(8).Text) - intval)

            e.Row.Cells(9).BackColor = Drawing.Color.Khaki
            e.Row.Cells(10).BackColor = Drawing.Color.Khaki
            e.Row.Cells(17).BackColor = Drawing.Color.Khaki

            If e.Row.Cells(15).Text = "" Or e.Row.Cells(15).Text = "&nbsp;" Then
            Else
                e.Row.Cells(15).BackColor = Drawing.Color.Red
                e.Row.Cells(15).ForeColor = Drawing.Color.White
                e.Row.Cells(15).Font.Bold = True
            End If

            If e.Row.Cells(9).Text = "" Or e.Row.Cells(9).Text = "&nbsp;" Then
                e.Row.Cells(9).BackColor = Drawing.Color.Salmon
            End If

            If e.Row.Cells(10).Text = "" Or e.Row.Cells(10).Text = "&nbsp;" Then
                e.Row.Cells(10).BackColor = Drawing.Color.Salmon
            End If

            If Replace(e.Row.Cells(17).Text, "<br>", "") = "" Or Replace(e.Row.Cells(17).Text, "<br>", "") = "&nbsp;" Then
                e.Row.Cells(17).BackColor = Drawing.Color.Salmon
            End If

        End If

        'e.Row.Cells(0).Width = 10
        ''e.Row.Cells(1).Width = 40
        ''e.Row.Cells(2).Width = 100
        'e.Row.Cells(1).Width = 30
        'e.Row.Cells(2).Width = 40
        'e.Row.Cells(3).Width = 100
        'e.Row.Cells(4).Width = 100
        'e.Row.Cells(5).Width = 70
        'e.Row.Cells(6).Width = 70
        'e.Row.Cells(7).Width = 70
        'e.Row.Cells(8).Width = 30
        'e.Row.Cells(9).Width = 50
        'e.Row.Cells(10).Width = 50
        'e.Row.Cells(11).Width = 70
        'e.Row.Cells(12).Width = 10
        'e.Row.Cells(13).Width = 70
        'e.Row.Cells(14).Width = 10
        'e.Row.Cells(15).Width = 110
        'e.Row.Cells(16).Width = 400
        'e.Row.Cells(17).Width = 300

        e.Row.Cells(3).Visible = False
        e.Row.Cells(4).Visible = False
        e.Row.Cells(7).Visible = False
        e.Row.Cells(18).Visible = False


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As ImageButton = e.Row.FindControl("ImageButton1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim kbn As String = "KD"
        Call niryoumail(kbn)

    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim kbn As String = "ｱﾌﾀ"
        Call niryoumail(kbn)

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Call dragemail()

    End Sub
    Private Sub niryoumail(kbn As String)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim struid As String = Session("UsrId")
        Dim strfrom As String = GET_from(struid)


        Dim strto As String = GET_ToAddress2("06", 1) '宛先
        strto = Left(strto, Len(strto) - 1)

        Dim strcc As String = GET_ToAddress2("06", 2) + GET_from(struid)  'CC 


        ''strto = GET_from(struid)
        ''strcc = GET_from(struid)

        'メールの件名
        Dim subject As String = "<通知>LCL案件展開　荷量追加 " & kbn '"【AIR " & strIrai & "依頼" & Session("strCust") & "向け】"

        'メールの本文
        Dim body As String = "<html><body><p>各位<p>お世話になっております。<p>荷量を追加いたしました。</p>http://k3hwpm01/exp/cs_home/lcl_tenkai.aspx</p></body></html>" ' UriBodyC()

        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;font-family:Meiryo UI;'><tr style='background-color: #6fbfd1;'><td>客先</td><td>IN_NO</td><td>カット日</td><td>出港日</td><td>M3</td><td>重量</td><td>荷量</td><td>引取希望日</td><td></td><td>搬入希望日</td><td></td><td>搬入先</td></tr>"

        GridView2.DataBind()

        For I = 0 To GridView2.Rows.Count - 1
            t = t & "<tr><td>" & GridView2.Rows(I).Cells(1).Text & "</td><td>" & GridView2.Rows(I).Cells(2).Text & "</td><td>" & GridView2.Rows(I).Cells(5).Text & "</td><td>" & GridView2.Rows(I).Cells(6).Text & "</td><td>" & GridView2.Rows(I).Cells(8).Text & "</td><td style='background-color: Khaki;'>" & Trim(GridView2.Rows(I).Cells(9).Text) & "</td><td style='background-color: Khaki;'>" & Trim(GridView2.Rows(I).Cells(10).Text) & "</td><td>" & GridView2.Rows(I).Cells(11).Text & "</td><td>" & GridView2.Rows(I).Cells(12).Text & "</td><td>" & GridView2.Rows(I).Cells(13).Text & "</td><td>" & GridView2.Rows(I).Cells(14).Text & "</td><td>" & Replace(GridView2.Rows(I).Cells(16).Text, "__", "<br>") & "</td>"
        Next

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "</p>" ' UriBodyC()

        body = body & body2

        body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

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
    Private Sub dragemail()

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim struid As String = Session("UsrId")
        Dim strfrom As String = GET_from(struid)


        Dim strto As String = GET_ToAddress2("06", 1) '宛先
        strto = Left(strto, Len(strto) - 1)

        Dim strcc As String = GET_ToAddress2("06", 2) + GET_from(struid)  'CC 

        'strto = GET_from(struid)
        'strcc = GET_from(struid)

        'メールの件名
        Dim subject As String = "<通知>LCL案件展開　ドレージ手配 "

        'メールの本文
        Dim body As String = "<html><body><p>各位<p>お世話になっております。<p>ドレージ手配が完了いたしました。</p>http://k3hwpm01/exp/cs_home/lcl_tenkai.aspx</p></body></html>" ' UriBodyC()

        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;font-family:Meiryo UI;'><tr style='background-color: #6fbfd1;'><td>客先</td><td>IN_NO</td><td>カット日</td><td>出港日</td><td>M3</td><td>重量</td><td>荷量</td><td>引取希望日</td><td></td><td>搬入希望日</td><td></td><td>搬入先</td><td>ドレージ</td></tr>"

        GridView2.DataBind()

        For I = 0 To GridView2.Rows.Count - 1
            t = t & "<tr><td>" & GridView2.Rows(I).Cells(1).Text & "</td><td>" & GridView2.Rows(I).Cells(2).Text & "</td><td>" & GridView2.Rows(I).Cells(5).Text & "</td><td>" & GridView2.Rows(I).Cells(6).Text & "</td><td>" & GridView2.Rows(I).Cells(8).Text & "</td><td>" & Trim(GridView2.Rows(I).Cells(9).Text) & "</td><td>" & Trim(GridView2.Rows(I).Cells(10).Text) & "</td><td>" & GridView2.Rows(I).Cells(11).Text & "</td><td>" & GridView2.Rows(I).Cells(12).Text & "</td><td>" & GridView2.Rows(I).Cells(13).Text & "</td><td>" & GridView2.Rows(I).Cells(14).Text & "</td><td>" & Replace(GridView2.Rows(I).Cells(16).Text, "__", "<br>") & "</td><td style='background-color: Khaki;'>" & Replace(GridView2.Rows(I).Cells(17).Text, "__", "<br>") & "</td>"
        Next

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "</p>" ' UriBodyC()

        body = body & body2

        body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

    Private Function GET_ToAddress2(strkbn As String, strtocc As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_ToAddress2 = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()


        strSQL = strSQL & "SELECT MAIL_ADD FROM M_EXL_MAIL01 "

        strSQL = strSQL & "WHERE TASK_CD = '" & strkbn & "' "
        strSQL = strSQL & "AND FLG = '" & strtocc & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_ToAddress2 += dataread("MAIL_ADD") + ","
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

    Private Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand

        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data0 = Me.GridView2.Rows(index).Cells(1).Text
            Dim data1 = Me.GridView2.Rows(index).Cells(2).Text
            Dim data2 = Me.GridView2.Rows(index).Cells(3).Text
            Dim data3 = Me.GridView2.Rows(index).Cells(5).Text
            Dim data4 = Me.GridView2.Rows(index).Cells(6).Text
            Dim data5 = Me.GridView2.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView2.Rows(index).Cells(9).Text
            Dim data7 = Me.GridView2.Rows(index).Cells(10).Text
            Dim data8 = Me.GridView2.Rows(index).Cells(16).Text
            Dim data9 = Me.GridView2.Rows(index).Cells(17).Text
            Dim data10 = Me.GridView2.Rows(index).Cells(18).Text

            Session("lstrcust") = data0
            Session("lstrinv") = data1
            Session("lstrbkg") = data2
            Session("lstrcut") = data3
            Session("lstretd") = data4
            Session("lstrM3") = data5
            Session("lstrwgt") = data6
            Session("lstrpkg") = data7
            Session("lstrin") = data8
            Session("lstrdr") = data9
            Session("lstflg01") = data10

            'Dim clientScript As String = "<script language='JavaScript'> window.open('shippingmemo_detail.aspx', '', 'width=1500,height=450','scrollbars=no','status=no','toolbar=no','location=no','menubar=no','resizable=no') <" + "/script>"
            'Dim startupScript As String = "<script language='JavaScript'>  window.open('shippingmemo_detail.aspx') <" + "/script>"

            'RegisterClientScriptBlock("client", clientScript)

            Response.Redirect("lcl_tenkai_detail.aspx")

        End If

    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound



        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strinv As String = ""
        Dim strbkg As String = ""


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
        ConnectionString00 = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

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


        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")

            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If


            If fflg2 = "" Then
                e.Row.Cells(0).Visible = False
            End If


        End If
        cnn00.Close()
        cnn00.Dispose()



    End Sub


    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        'GridViewのボタン押下処理



        If e.CommandName = "edt4" Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(3).Text

            Dim strSQL As String
            Dim dtNow As DateTime = DateTime.Now
            Dim strFlg As String = ""

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET "
            strSQL = strSQL & "FLG01 = '0' "
            strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "


            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
            cnn.Dispose()




            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('表示します。');</script>", False)


        End If

        'Grid再表示
        GridView2.DataBind()
        GridView1.DataBind()

    End Sub
End Class
