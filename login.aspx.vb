Imports System.Data
Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If Page.IsPostBack = False Then
            Session.Abandon()
            If Request.QueryString("mode") = "timeout" Then     'タイムアウト
                objLbl.Text = "セッションが切れました。再ログインしてください。"
            End If
        End If
    End Sub

    Private Sub objBtn_Click(sender As Object, e As EventArgs) Handles objBtn.Click
        ' 入力されたユーザーID、パスワードでusrテーブル内のレコードを検索
        Dim strSQL As String = ""

        Dim ConnectionString As String = String.Empty
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        Dim objDb As New SqlConnection(ConnectionString)

        'パスワード暗号化
        Dim origByte As Byte() = System.Text.Encoding.UTF8.GetBytes(txtPass.Text)

        Dim sha256 As System.Security.Cryptography.SHA256 = New System.Security.Cryptography.SHA256CryptoServiceProvider()
        Dim hashValue As Byte() = sha256.ComputeHash(origByte)
        'byte型配列を16進数の文字列に変換
        Dim result As New System.Text.StringBuilder()
        Dim b As Byte
        For Each b In hashValue
            result.Append(b.ToString("x2"))
        Next

        Dim strPass As String = result.ToString

        strSQL = strSQL & "SELECT * FROM M_EXL_USR "
        strSQL = strSQL & "WHERE uid = '" & txtUsr.Text & "' "
        strSQL = strSQL & "AND passwd = '" & strPass & "' "

        Dim objCom As New SqlCommand(strSQL, objDb)

        objDb.Open()
        Dim objDr As SqlDataReader = objCom.ExecuteReader()
        If objDr.Read() Then
            ' 検索の結果、該当するレコードが存在した場合、認証は成功

            '現在保持しているセッション情報をクリアする。
            Session.RemoveAll()

            'ログイン情報をセッションに追加
            Session("UsrId") = txtUsr.Text
            Session("UsrName") = objDr("unam")
            Session("Role") = objDr("role")


            '＜本番用　ロールを確認し、ユーザー権限ごとにログイン先を設定する＞
            If objDr("role") = "usr" Then
                FormsAuthentication.SetAuthCookie(txtUsr.Text, False)　 'cookie設定
                Response.Redirect("exl_top.aspx")           'リダイレクト
            Else
                '＜開発用　実行したいaspxから実行すればその画面が立ち上がる＞
                '＜aspxが指定されてい無い場合はソリューションに設定されたスタートページ＞
                'FormsAuthentication.RedirectFromLoginPage(txtUsr.Text, False)


                ''AdminとCSユーザー
                FormsAuthentication.SetAuthCookie(txtUsr.Text, False)　 'cookie設定
                Response.Redirect("start.aspx")           'リダイレクト
            End If
        Else
            objLbl.Text = "正しいユーザーID、パスワードを入力してください"
        End If
        objDb.Close()
    End Sub

End Class
