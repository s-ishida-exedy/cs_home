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

        Dim strIvno As String = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            Dim strCust As String = Session("strCust")
            Dim strTime As String = Session("strTime")

            '依頼者のメールアドレスを取得
            Literal2.Text = GET_USR_DATA()          'FROM

            Literal1.Text = UriBodyC()              '本文
            Literal5.Text = "【ご確認】" & strTime & "バンニング " & strCust & "向け"
        End If

        Button1.Attributes.Add("onclick", "return confirm('メール送信します。よろしいですか？');")
    End Sub

    Private Function GET_USR_DATA() As String
        '送信者のメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_USR_DATA = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "Select * FROM M_EXL_USR "
        strSQL = strSQL & "WHERE uid = '" & Session("UsrId") & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_USR_DATA = dataread("e_mail")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Function GET_CS_Member(intMode As Integer, strCode As String) As String
        'CSメンバー情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_CS_Member = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "Select * FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE CODE = '" & strCode & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            Select Case intMode
                Case 1
                    GET_CS_Member = dataread("COMPANY")
                Case 2
                    GET_CS_Member = dataread("TEAM")
                Case 3
                    GET_CS_Member = dataread("MEMBER_NAME")
                Case 4
                    GET_CS_Member = dataread("TEL_NO")
                Case 5
                    GET_CS_Member = dataread("FAX_NO")
                Case 6
                    GET_CS_Member = dataread("E_MAIL")
            End Select
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'メール送信ボタン
        Call Send_Mail()

        'セッションキーをクリアする
        Session.Remove("strMode")
        Session.Remove("strIdx")
        Session.Remove("strCust")
        Session.Remove("strTime")
        Session.Remove("strVoy1")
        Session.Remove("strVess1")
        Session.Remove("strBook1")
        Session.Remove("strCont1")
        Session.Remove("strVoy2")
        Session.Remove("strVess2")
        Session.Remove("strBook2")
        Session.Remove("strCont2")
        Session.Remove("strEtc1")
        Session.Remove("strEtc2")
        Session.Remove("strIvno")

        '前の画面へ遷移
        Response.Redirect("eir_comfirm.aspx")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Session("strMode") = "0"    '元に戻る画面遷移

        '前の画面へ遷移
        Response.Redirect("eir_comfirm.aspx")
    End Sub

    Public Function UriBodyC() As String
        'メール本文の作成（画面用）

        Dim Bdy As String = ""

        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "このメールはシステムから送信されています。<br/>"
        Bdy = Bdy + "心当たりが無い場合、ポータルサイト管理者までご連絡ください。<br/>"
        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "ＣＳチーム　担当者殿<BR>"
        Bdy = Bdy + "お世話になります。<BR>"
        Bdy = Bdy + "主題の件、下記ご確認お願いいたします。<BR>"

        '表の作成
        Bdy = Bdy + "<table class=""tab1"">"
        Bdy = Bdy + "    <tr>"
        Bdy = Bdy + "        <td class=""td3""></td>"
        Bdy = Bdy + "        <td class=""td1"">確定情報</td>"
        Bdy = Bdy + "        <td class=""td1"">搬入票</td>"
        Bdy = Bdy + "    </tr>"
        If Session("strVoy2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td class=""td3"">VoyNo</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strVoy1") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strVoy2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If Session("strVess2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td class=""td3"">船　名</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strVess1") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strVess2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If Session("strBook2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td class=""td3"">BOOKING NO</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strBook1") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strBook2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If Session("strCont2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td class=""td3"">コンテナサイズ</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strCont1") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strCont2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If Session("strEtc2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td class=""td3"">その他</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strEtc1") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & Session("strEtc2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        Bdy = Bdy + "</table><br/>"

        Bdy = Bdy + "以上、よろしくお願いします。"
        Bdy = Bdy + "<br/><br/>"
        Bdy = Bdy + "【ＣＳ担当者はＣＳポータルにログインし、対応してください。】"


        Return Bdy
    End Function

    Public Function UriBodyC2() As String
        'メール本文の作成（メール用）

        Dim Bdy As String = ""

        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "このメールはシステムから送信されています。<br/>"
        Bdy = Bdy + "心当たりが無い場合、ポータルサイト管理者までご連絡ください。<br/>"
        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "ＣＳチーム　担当者殿<BR>"
        Bdy = Bdy + "お世話になります。<BR>"
        Bdy = Bdy + "主題の件、下記ご確認お願いいたします。<BR>"

        '表の作成
        Bdy = Bdy + "<table width=75% border=""1"" style=""border-collapse: collapse"">"
        Bdy = Bdy + "    <tr>"
        Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#87E7AD"" style =""font-weight:bold""></td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">確定情報</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">搬入票</td>"
        Bdy = Bdy + "    </tr>"
        If Session("strVoy2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">VoyNo</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strVoy1") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strVoy2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If Session("strVess2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">船　名</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strVess1") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strVess2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If Session("strBook2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">BOOKING NO</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strBook1") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strBook2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If Session("strCont2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">コンテナサイズ</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strCont1") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strCont2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If Session("strEtc2") <> "" Then
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">その他</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strEtc1") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Session("strEtc2") & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        Bdy = Bdy + "</table><br/>"

        Bdy = Bdy + "以上、よろしくお願いします。"
        Bdy = Bdy + "<br/><br/>"
        Bdy = Bdy + "【ＣＳ担当者はＣＳポータルにログインし、対応してください。】"


        Return Bdy
    End Function

    Private Sub Send_Mail()
        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' 送信者
        Dim strfrom As String = GET_USR_DATA()

        'メールの件名
        Dim strCust As String = Session("strCust")
        Dim strTime As String = Session("strTime")
        Dim subject As String = "【ご確認】" & strTime & "バンニング " & strCust & "向け"

        'メールの本文
        Dim body As String = UriBodyC2()

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))

        'Toのメールアドレスを取得
        Dim strToAdr As String = ""
        For Each li As ListItem In CheckBoxList1.Items
            If li.Selected Then
                strToAdr = strToAdr & GET_CS_Member(6, li.Value) + ","
                ' TOをセット  
                message.To.Add(New MailboxAddress("", GET_CS_Member(6, li.Value)))
            End If
        Next

        If strToAdr = "" Then
            Label120.Text = "宛先が選択されていません。"
            Return
        End If

        'strToAdr = Mid(strToAdr, 1, Len(strToAdr) - 1)
        ' TOをセット  
        'message.To.Add(MailboxAddress.Parse(strToAdr))

        'Ccのメールアドレスを取得
        Dim strCcAdr As String = ""
        For Each li As ListItem In CheckBoxList2.Items
            If li.Selected Then
                strCcAdr = strCcAdr & GET_CS_Member(6, li.Value) + ","
                message.Cc.Add(New MailboxAddress("", GET_CS_Member(6, li.Value)))
            End If
        Next

        '依頼者のメールアドレスをCCに追加
        message.Cc.Add(New MailboxAddress("", strfrom))
        '        strCcAdr = strCcAdr & strfrom

        'strCcAdr = Mid(strCcAdr, 1, Len(strCcAdr) - 1)
        ' CCをセット
        'message.Cc.Add(MailboxAddress.Parse(strCcAdr))

        ' 表題  
        message.Subject = subject

        ' 本文
        'Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        'textPart.Text = body
        'message.Body = textPart

        Dim bodyBuilder As New BodyBuilder
        bodyBuilder.HtmlBody = body
        message.Body = bodyBuilder.ToMessageBody

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
        End Using

        'データをT_EXL_EIR_COMFに登録
        Dim strSubject As String = strTime & "バンニング " & strCust & "向け"
        Call DB_access(strSubject)

    End Sub

    Private Sub DB_access(strTitle As String)
        '画面入力情報をテーブルへ登録
        Dim strSQL As String
        Dim strVal As String = ""
        Dim strDate As String = Format(Now, "yyyy/MM/dd")

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
        Dim strRegP As String = Session("UsrId")
        Dim datRegT As DateTime = DateTime.Now

        Dim strVoy01 As String = Session("strVoy1")
        Dim strVoy02 As String = Session("strVoy2")
        Dim strVess01 As String = Session("strVess1")
        Dim strVess02 As String = Session("strVess2")
        Dim strBook01 As String = Session("strBook1")
        Dim strBook02 As String = Session("strBook2")
        Dim strCont01 As String = Session("strCont1")
        Dim strCont02 As String = Session("strCont2")
        Dim strEtc01 As String = Session("strEtc1")
        Dim strEtc02 As String = Session("strEtc2")
        Dim strIvno As String = Session("strIvno")

        'データ更新
        strSQL = ""
        strSQL = strSQL & "INSERT INTO T_EXL_EIR_COMF VALUES("
        strSQL = strSQL & " '" & strRegP & "' "
        strSQL = strSQL & " ,'" & datRegT & "' "
        strSQL = strSQL & " ,'" & strDate & "' "
        strSQL = strSQL & " ,'" & strTitle & "' "
        strSQL = strSQL & " ,'" & strVoy01 & "' "
        strSQL = strSQL & " ,'" & strVoy02 & "' "
        strSQL = strSQL & " ,'" & strVess01 & "' "
        strSQL = strSQL & " ,'" & strVess02 & "' "
        strSQL = strSQL & " ,'" & strBook01 & "' "
        strSQL = strSQL & " ,'" & strBook02 & "' "
        strSQL = strSQL & " ,'" & strCont01 & "' "
        strSQL = strSQL & " ,'" & strCont02 & "' "
        strSQL = strSQL & " ,'" & strEtc01 & "' "
        strSQL = strSQL & " ,'" & strEtc02 & "' "
        strSQL = strSQL & " ,'" & 0 & "' "
        strSQL = strSQL & " ,'' "
        strSQL = strSQL & " ,'" & datRegT & "' "
        strSQL = strSQL & " ,'" & strIvno & "' "
        strSQL = strSQL & ") "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
    End Sub
End Class

