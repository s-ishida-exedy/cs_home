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

        Dim strOdrCtrl As String = Session("strOdrCtrl")
        Dim strCust As String = ""
        Dim strIvno As String = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            Call GET_CustInfo(strOdrCtrl, strCust, strIvno)

            Literal1.Text = UriBodyC()              '本文
            Literal2.Text = GET_CS_Member(6)        'CC
            Literal4.Text = Session("strTO")        'TO
            Literal5.Text = "AIR " & Mid(strCust, 1, Len(strCust) - 1) & "荷量確認" & Mid(strIvno, 1, Len(strIvno) - 1)
        End If

        Button1.Attributes.Add("onclick", "return confirm('メール送信します。よろしいですか？');")
    End Sub

    Private Sub GET_CustInfo(strOdrCtrl As String, ByRef strCust As String, ByRef strIvno As String)
        '登録情報を取得
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

        strSQL = strSQL & "SELECT DISTINCT CUST_CD, IVNO "
        strSQL = strSQL & "FROM T_EXL_AIR_EXC_ODR WHERE ODR_CTL_NO IN (" & strOdrCtrl & ") "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strCust += dataread("CUST_CD") & ","
            strIvno += dataread("IVNO") & ","
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
        Session.Remove("strOdrCtrl")

        '前の画面へ遷移
        Response.Redirect("air_exclusive.aspx")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '前の画面へ遷移
        Response.Redirect("air_exclusive.aspx")
    End Sub

    Public Function UriBodyC() As String
        'メール本文の作成（画面用）
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

        strSQL = strSQL & ""
        strSQL = strSQL & "SELECT DISTINCT "
        strSQL = strSQL & "  CUST_CD  "
        strSQL = strSQL & "  , NOUKI "
        strSQL = strSQL & "  , LS_TYP "
        strSQL = strSQL & "  , CUST_ODR_NO "
        strSQL = strSQL & "  , SALESNOTENO "
        strSQL = strSQL & "  , a.ODR_CTL_NO AS ODR_CTL_NO "
        strSQL = strSQL & "  , b.IVNO AS IVNO "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  T_EXL_AIR_EXC_ODR a "
        strSQL = strSQL & "  LEFT JOIN T_EXL_AIR_EXCLUSIVE b "
        strSQL = strSQL & "    ON a.ODR_CTL_NO = b.ODR_CTL_NO "
        strSQL = strSQL & "WHERE a.ODR_CTL_NO IN (" & Session("strOdrCtrl") & ") "
        strSQL = strSQL & "ORDER BY NOUKI "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Dim Bdy As String = ""

        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "このメールはシステムから送信されています。<br/>"
        Bdy = Bdy + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "お世話になります。<BR>"
        Bdy = Bdy + "ＡＩＲ専用客先オーダーを下記の通り登録しました。<BR>"

        '表の作成
        Bdy = Bdy + "<table class=""tab1"">"
        Bdy = Bdy + "    <tr>"
        Bdy = Bdy + "        <td class=""td1"">客先</td>"
        Bdy = Bdy + "        <td class=""td1"">納期</td>"
        Bdy = Bdy + "        <td class=""td1"">LS</td>"
        Bdy = Bdy + "        <td class=""td1"">客先注文番号</td>"
        Bdy = Bdy + "        <td class=""td1"">セールスノート</td>"
        Bdy = Bdy + "        <td class=""td1"">受注管理番号</td>"
        Bdy = Bdy + "        <td class=""td1"">インボイス番号</td>"
        Bdy = Bdy + "    </tr>"

        '結果を取り出す 
        While (dataread.Read())
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td class=""td2"">" & dataread("CUST_CD") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & dataread("NOUKI") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & dataread("LS_TYP") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & dataread("CUST_ODR_NO") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & dataread("SALESNOTENO") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & dataread("ODR_CTL_NO") & "</td>"
            Bdy = Bdy + "        <td class=""td2"">" & dataread("IVNO") & "</td>"
            Bdy = Bdy + "    </tr>"
        End While

        Bdy = Bdy + "</table>"

        Bdy = Bdy + "お手数ですが、明細登録および荷量の確認をお願いします。<BR>"
        Bdy = Bdy + "以上、よろしくお願いします。"


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        Return Bdy
    End Function

    Public Function UriBodyC2() As String
        'メール本文の作成（メール用）
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

        strSQL = strSQL & ""
        strSQL = strSQL & "SELECT DISTINCT "
        strSQL = strSQL & "  CUST_CD  "
        strSQL = strSQL & "  , NOUKI "
        strSQL = strSQL & "  , LS_TYP "
        strSQL = strSQL & "  , CUST_ODR_NO "
        strSQL = strSQL & "  , SALESNOTENO "
        strSQL = strSQL & "  , a.ODR_CTL_NO AS ODR_CTL_NO "
        strSQL = strSQL & "  , b.IVNO AS IVNO "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  T_EXL_AIR_EXC_ODR a "
        strSQL = strSQL & "  LEFT JOIN T_EXL_AIR_EXCLUSIVE b "
        strSQL = strSQL & "    ON a.ODR_CTL_NO = b.ODR_CTL_NO "
        strSQL = strSQL & "WHERE a.ODR_CTL_NO IN (" & Session("strOdrCtrl") & ") "
        strSQL = strSQL & "ORDER BY NOUKI "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Dim Bdy As String = ""

        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "このメールはシステムから送信されています。<br/>"
        Bdy = Bdy + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "お世話になります。<BR>"
        Bdy = Bdy + "ＡＩＲ専用客先オーダーを下記の通り登録しました。<BR>"

        '表の作成
        Bdy = Bdy + "<table width=75% border=""1"" style=""border-collapse: collapse"">"
        Bdy = Bdy + "    <tr>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">客先</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">納期</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">LS</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">客先注文番号</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">セールスノート</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">受注管理番号</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">インボイス番号</td>"
        Bdy = Bdy + "    </tr>"

        '結果を取り出す 
        While (dataread.Read())
            Bdy = Bdy + "    <tr>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & dataread("CUST_CD") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & dataread("NOUKI") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & dataread("LS_TYP") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & dataread("CUST_ODR_NO") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & dataread("SALESNOTENO") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & dataread("ODR_CTL_NO") & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & dataread("IVNO") & "</td>"
            Bdy = Bdy + "    </tr>"
        End While

        Bdy = Bdy + "</table>"

        Bdy = Bdy + "お手数ですが、明細登録および荷量の確認をお願いします。<BR>"
        Bdy = Bdy + "以上、よろしくお願いします。"


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

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

        strSQL = strSQL & "Select * FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE CODE = '" & Session("UsrId") & "' "

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

    Private Sub Send_Mail()
        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim strfrom As String = GET_CS_Member(6)

        'メールの件名
        Dim strOdrCtrl As String = Session("strOdrCtrl")
        Dim strCust As String = ""
        Dim strIvno As String = ""

        Call GET_CustInfo(strOdrCtrl, strCust, strIvno)
        Dim subject As String = "AIR " & Mid(strCust, 1, Len(strCust) - 1) & "荷量確認" & Mid(strCust, 1, Len(strIvno) - 1)

        'メールの本文
        Dim body As String = UriBodyC2()

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))

        ' 宛先情報  
        message.To.Add(MailboxAddress.Parse(Session("strTO")))
        message.Cc.Add(MailboxAddress.Parse(GET_CS_Member(6)))      'CCはログインしているCS担当者

        ' 表題  
        message.Subject = subject

        ' 本文
        Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        textPart.Text = body
        message.Body = textPart

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
        End Using
    End Sub

End Class

