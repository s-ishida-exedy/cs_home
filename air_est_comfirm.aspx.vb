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
        If IsPostBack Then
            ' そうでない時処理
        Else

        End If

        Literal1.Text = UriBodyC()
        Literal2.Text = Session("strCC")
        Literal4.Text = GET_CS_Member(6)
        Literal5.Text = "【AIR " & Session("strIrai") & "依頼" & Session("strCust") & "向け】"
        Literal6.Text = Session("strFile")
        If Session("strPlac") = "0" Then
            Literal3.Text = GET_BccAddress("0")
        ElseIf Session("strPlac") = "1" Then
            Literal3.Text = GET_BccAddress("1")
        End If

        Button1.Attributes.Add("onclick", "return confirm('メール送信します。よろしいですか？');")
    End Sub

    Private Sub GET_CustInfo(strCust As String, ByRef strName As String, ByRef strAdd As String, ByRef strDes As String)
        '客先情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT CUST_NM, CUST_ADDRESS, DESTINATION "
        strSQL = strSQL & "FROM T_EXL_CSMANUAL WHERE NEW_CODE = '" & strCust & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strName = dataread("CUST_NM")
            strAdd = dataread("CUST_ADDRESS")
            strDes = dataread("DESTINATION")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'メール送信ボタン
        Call Send_Mail()

        'セッションキーをクリアする
        Session.Remove("strTant")
        Session.Remove("strName")
        Session.Remove("strAdd")
        Session.Remove("strDes")
        Session.Remove("strIrai")
        Session.Remove("strPlac")
        Session.Remove("strCust")
        Session.Remove("strImco")
        Session.Remove("strWeit")
        Session.Remove("strKogu")
        Session.Remove("strTate")
        Session.Remove("strYoko")
        Session.Remove("strTaka")
        Session.Remove("strPick")
        Session.Remove("strArrD")
        Session.Remove("strPicT")
        Session.Remove("strCWei")
        Session.Remove("strCSiz")
        Session.Remove("strCDat")
        Session.Remove("strCC")
        Session.Remove("strRenr")
        Session.Remove("strMode")

        '前の画面へ遷移
        Response.Redirect("air_estimate.aspx")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Session("strMode") = "0"    '元に戻る画面遷移

        '前の画面へ遷移
        Response.Redirect("air_estimate.aspx")
    End Sub


    Public Function UriBodyC() As String
        'メール本文の作成
        Dim Bdy As String = ""

        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "このメールはシステムから送信されています。<br/>"
        Bdy = Bdy + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "お世話になります。<BR>"
        Bdy = Bdy + "早速ですがAIRの見積りを依頼をさせていただきます。<BR>"
        Bdy = Bdy + "下記条件を元にお見積りをお願い申し上げます。<BR><BR>"

        '出荷場所
        If Session("strPlac") = "本社" Then
            Bdy = Bdy + "集荷場所：〒572-0822　(株)エクセディ物流 1F<BR>"
            Bdy = Bdy + "住所: 大阪府寝屋川市木田元宮1 -30 - 1<BR><BR>"
        Else
            Bdy = Bdy + "集荷場所：〒518-0825　(株)エクセディ物流 上野出張所<BR>"
            Bdy = Bdy + "住所: 三重県伊賀市小田町2500番地<BR><BR>"
        End If

        'サイズ欄の文字列生成
        Dim strSize As String = ""
        strSize = Session("strTate") & "×" & Session("strYoko") & "×" & Session("strTaka") & "cm"

        '表の作成
        Bdy = Bdy + "<table border = ""0"" style=""border-collapse: collapse;width:348pt; border: 0.5pt solid windowtext;"" width=""463"">"
        Bdy = Bdy + "<tr height=32>"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">客先コード</td>"
        Bdy = Bdy + "    <td colspan=2 width=338 style=""border: .5pt solid windowtext;text-align: center;"">"
        Bdy = Bdy + "" & Session("strCust") & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">客先名</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;"" colspan=""2"" width=""338"">"
        Bdy = Bdy + "" & Session("strName") & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">住所</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;"" colspan=""2"" width=""338"">"
        Bdy = Bdy + "" & Session("strAdd") & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">仕向地</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"" width=""338"">"
        Bdy = Bdy + "" & Session("strDes") & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">輸送条件</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"" width=""338"">集荷～通関～航空輸送～通関～配達</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">建値</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">"
        Bdy = Bdy + "" & Session("strImco") & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">集荷日/時間</td>"
        If Session("strCDat") = "1" Then
            Bdy = Bdy + "<td style=""border: 0.5pt solid windowtext;text-align: center;"">添付参照</td>"
            Bdy = Bdy + "<td style=""border: 0.5pt solid windowtext;text-align: center;"">添付参照</td>"
        Else
            Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"">"
            Bdy = Bdy + "" & Session("strPick") & "</td>"
            Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"">"
            Bdy = Bdy + "" & Session("strPicT") & "</td>"
        End If
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">到着希望日</td>"
        If Session("strCDat") = "1" Then
            Bdy = Bdy + "<td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">添付参照</td>"
        Else
            Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">"
            Bdy = Bdy + "" & Session("strArrD") & "</td>"
        End If
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">総重量/個数</td>"
        If Session("strCWei") = "1" Then
            Bdy = Bdy + "<td style=""border: 0.5pt solid windowtext;text-align: center;"">添付参照</td>"
            Bdy = Bdy + "<td style=""border: 0.5pt solid windowtext;text-align: center;"">添付参照</td>"
        Else
            Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"">約 "
            Bdy = Bdy + "" & Session("strWeit") & " Kg</td>"
            Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"">"
            Bdy = Bdy + "" & Session("strKogu") & " 個口</td>"
        End If
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">サイズ</td>"
        If Session("strCSiz") = "1" Then
            Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">添付参照</td>"
        Else
            Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">"
            Bdy = Bdy + "" & strSize & "</td>"
        End If
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">備考</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;text-align: center;"" colspan=""2"">危険品非該当</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "<tr height=""32"" style=""height:24.0pt"">"
        Bdy = Bdy + "    <td height=32 width=125 style=""border: .5pt solid windowtext;text-align: center;"">連絡事項</td>"
        Bdy = Bdy + "    <td style=""border: 0.5pt solid windowtext;"" colspan=""2"">"
        Bdy = Bdy + "" & Session("strRenr") & "</td>"
        Bdy = Bdy + "</tr>"
        Bdy = Bdy + "</table><BR>"
        Bdy = Bdy + "以上、宜しくお願い致します。<BR>"
        Bdy = Bdy + "*******************************************<BR>"
        Bdy = Bdy + "" & GET_CS_Member(1) & "<BR>"
        Bdy = Bdy + "" & GET_CS_Member(2) & "<BR>"
        Bdy = Bdy + "" & GET_CS_Member(3) & "<BR>"
        Bdy = Bdy + "TEL:" & GET_CS_Member(4) & "<BR>"
        Bdy = Bdy + "FAX:" & GET_CS_Member(5) & "<BR>"
        Bdy = Bdy + "E-MAIL:" & GET_CS_Member(6) & "<BR>"
        Bdy = Bdy + "*******************************************"
        Return Bdy
    End Function

    Private Function GET_CS_Member(intMode As Integer) As String
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

        strSQL = strSQL & "SELECT * FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE NAME_AB = '" & Session("strTant") & "' "

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

    Private Function GET_BccAddress(strPlace As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        GET_BccAddress = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT MAIL_ADD FROM M_EXL_AIR_MAIL "
        strSQL = strSQL & "WHERE PLACE = '" & strPlace & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            GET_BccAddress += dataread("MAIL_ADD") + ","
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Sub Send_Mail()
        'メールの送信に必要な情報
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()


        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim strfrom As String = GET_CS_Member(6)
        Dim strto As String = GET_CS_Member(6)

        'メールの件名
        Dim strIrai As String = ""
        Select Case Session("strIrai")
            Case 0
                strIrai = "見積り"
            Case 1
                strIrai = "集荷"
            Case 2
                strIrai = "集荷見積り"
        End Select

        Dim subject As String = "【AIR " & strIrai & "依頼" & Session("strCust") & "向け】"

        'メールの本文
        Dim body As String = UriBodyC()

        Dim strFilePath As String = "C:\exp\cs_home\upload\" & Session("strFile")

        'Using stream = File.OpenRead(strFilePath)
        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))

        ' 宛先情報  
        message.To.Add(MailboxAddress.Parse(strto))
        If Session("strCC") <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = Session("strCC").Split(",")
            For Each c In strVal
                message.Cc.Add(New MailboxAddress("", c))
            Next
        End If

        strSQL = ""
        strSQL = strSQL & "SELECT MAIL_ADD FROM M_EXL_AIR_MAIL "
        strSQL = strSQL & "WHERE PLACE = '" & Session("strPlac") & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            message.Bcc.Add(New MailboxAddress("", dataread("MAIL_ADD")))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


        ' 表題  
        message.Subject = subject

        ' 本文
        Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        textPart.Text = body
        message.Body = textPart

        Dim multipart = New MimeKit.Multipart("mixed")
        multipart.Add(textPart)
        message.Body = multipart

        '添付ファイル
        If Session("strFile") <> "" Then
            Dim path = strFilePath     '添付したいファイル
            Dim attachment = New MimeKit.MimePart("application", "pdf") _
            With {
                .Content = New MimeKit.MimeContent(System.IO.File.OpenRead(path)),
                .ContentDisposition = New MimeKit.ContentDisposition(),
                .ContentTransferEncoding = MimeKit.ContentEncoding.Base64,
                .FileName = System.IO.Path.GetFileName(path)
            }
            multipart.Add(attachment)
        End If

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
        End Using
        '    stream.Dispose()
        'End Using

        File.Delete(strFilePath)

    End Sub

End Class

